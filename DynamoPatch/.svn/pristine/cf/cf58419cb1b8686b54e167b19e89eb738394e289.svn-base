using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Dynamo.BoekingsSysteem.ViewModel;
using Dynamo.Boekingssysteem.View;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Controls;

namespace Dynamo.Boekingssysteem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private HoofdSchermViewModel viewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage("NL-nl")));

            EventManager.RegisterClassHandler(typeof(TextBox),
                TextBox.GotFocusEvent,
                new RoutedEventHandler(TextBox_GotFocus));

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            base.OnStartup(e);

            HoofdScherm window = new HoofdScherm();
            Helper.MeldingHandler = new Controls.Melding.MeldingHandler(window);
            Helper.TaskbarIcon = window.tbi;

            window.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(window_PreviewKeyDown);
            viewModel = new HoofdSchermViewModel();

            viewModel.RequestClose += delegate
            {
                window.Close();
            };
            window.Closed += delegate
            {
                viewModel.Dispose();
            };
            window.DataContext = viewModel;
            window.Show();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Helper.MeldingHandler.ShowMeldingOk(e.ExceptionObject.ToString());
        }

        void window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox || e.OriginalSource is ComboBox)
            {
                return;
            }

            if (e.Key == System.Windows.Input.Key.Home)
            {
                viewModel.HomeCommand();
                e.Handled = true;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
