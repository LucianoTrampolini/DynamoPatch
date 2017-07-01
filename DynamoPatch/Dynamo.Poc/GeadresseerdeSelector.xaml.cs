using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Dynamo.Poc
{
    /// <summary>
    /// Interaction logic for GeadresseerdeSelector.xaml
    /// </summary>
    public partial class GeadresseerdeSelector : UserControl
    {
        public GeadresseerdeSelector()
        {
            InitializeComponent();
            using (var repo = new Dynamo.BL.BeheerderRepository())
            {
                BeheerdersString = new ObservableCollection<string>(repo.Load().Select(b => b.Naam));
            }
            DataContext = this;
        }

        private void AutoCompleteBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W && GeadresseerdenInput.SelectedItem !=null)
            {
                var g = new Geadresseerde();
                g.Beheerder = GeadresseerdenInput.SelectedItem.ToString(); 
                Geadresseerden.Children.Add(g);
            }
        }

        public ObservableCollection<string> BeheerdersString { get; private set; }

        private string _beheerder = "";
        public string Beheerder
        {
            get { return _beheerder; }
            set
            {

                _beheerder = value;

            }
        }

        public ICommand Confirm
        {
            get
            {
                return new RelayCommand(param => OnDefaultCommand());
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
