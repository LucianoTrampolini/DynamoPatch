using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.Controls.Melding
{
    public class MeldingHandler: IMeldingHandler
    {
        private System.Windows.Window _owner;
        public MeldingHandler(System.Windows.Window owner)
        {
            _owner = owner;
        }

        public bool ShowMeldingJaNee(string melding)
        {
            return MessageBox.Show(_owner, melding, StringResources.ApplictatieNaam, System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes;
        }


        public bool ShowMeldingOkCancel(string melding)
        {
            return MessageBox.Show(_owner, melding, StringResources.ApplictatieNaam, System.Windows.MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK;
        }


        public void ShowMeldingOk(string melding)
        {
            MessageBox.Show(_owner, melding, StringResources.ApplictatieNaam, System.Windows.MessageBoxButton.OK);
        }
    }
}
