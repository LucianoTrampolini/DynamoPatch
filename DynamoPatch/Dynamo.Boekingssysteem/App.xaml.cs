using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

using Dynamo.Boekingssysteem.Controls.Melding;
using Dynamo.Boekingssysteem.View;
using Dynamo.BoekingsSysteem.ViewModel;

namespace Dynamo.Boekingssysteem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Member fields

        private HoofdSchermViewModel viewModel;

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage("NL-nl")));

            EventManager.RegisterClassHandler(
                typeof(TextBox),
                TextBox.GotFocusEvent,
                new RoutedEventHandler(TextBox_GotFocus));

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            base.OnStartup(e);

            HoofdScherm window = new HoofdScherm();
            Helper.MeldingHandler = new MeldingHandler(window);
            Helper.TaskbarIcon = window.tbi;

            window.PreviewKeyDown += window_PreviewKeyDown;
            viewModel = new HoofdSchermViewModel();

            viewModel.RequestClose += delegate { window.Close(); };
            window.Closed += delegate { viewModel.Dispose(); };
            window.DataContext = viewModel;
            window.Show();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Helper.MeldingHandler.ShowMeldingOk(e.ExceptionObject.ToString());
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox
                || e.OriginalSource is ComboBox)
            {
                return;
            }

            if (e.Key == Key.Home)
            {
                viewModel.HomeCommand();
                e.Handled = true;
            }
        }
    }
}