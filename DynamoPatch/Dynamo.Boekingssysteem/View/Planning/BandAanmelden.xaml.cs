using System.Windows;
using System.Windows.Controls;

using Dynamo.Boekingssysteem.ViewModel.Planning;

namespace Dynamo.Boekingssysteem.View.Planning
{
    /// <summary>
    /// Interaction logic for BandAanmelden.xaml
    /// </summary>
    public partial class BandAanmelden : UserControl
    {
        public BandAanmelden()
        {
            InitializeComponent();
        }

        private void AutoCompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;
            if (autoCompleteBox == null)
                return;

            var cakeToMake = autoCompleteBox.DataContext as BandAanmeldenViewModel;
            if (cakeToMake == null)
                return;

            cakeToMake.BandNaamGetypt = autoCompleteBox.Text;
        }
    }
}