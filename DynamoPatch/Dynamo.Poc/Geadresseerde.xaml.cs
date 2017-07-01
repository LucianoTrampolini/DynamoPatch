using System.Windows.Controls;

namespace Dynamo.Poc
{
    /// <summary>
    /// Interaction logic for Geadresseerde.xaml
    /// </summary>
    public partial class Geadresseerde : UserControl
    {
        public Geadresseerde()
        {
            InitializeComponent();
        }

        public string Beheerder { get; set; }
    }
}