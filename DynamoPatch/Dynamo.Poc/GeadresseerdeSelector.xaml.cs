using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Dynamo.BL;

namespace Dynamo.Poc
{
    /// <summary>
    /// Interaction logic for GeadresseerdeSelector.xaml
    /// </summary>
    public partial class GeadresseerdeSelector : UserControl
    {
        #region Member fields

        private string _beheerder = "";

        #endregion

        public GeadresseerdeSelector()
        {
            InitializeComponent();
            using (var repo = new BeheerderRepository())
            {
                BeheerdersString = new ObservableCollection<string>(
                    repo.Load()
                        .Select(b => b.Naam));
            }
            DataContext = this;
        }

        public string Beheerder
        {
            get { return _beheerder; }
            set { _beheerder = value; }
        }

        public ObservableCollection<string> BeheerdersString { get; private set; }

        public ICommand Confirm
        {
            get { return new RelayCommand(param => OnDefaultCommand()); }
        }

        private void AutoCompleteBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W
                && GeadresseerdenInput.SelectedItem != null)
            {
                var g = new Geadresseerde();
                g.Beheerder = GeadresseerdenInput.SelectedItem.ToString();
                Geadresseerden.Children.Add(g);
            }
        }

        private void OnDefaultCommand()
        {
            if (Beheerder != null)
            {
                MessageBox.Show(Beheerder);
            }
        }
    }
}