using System.Windows;

using Dynamo.Common.Properties;

using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Dynamo.Boekingssysteem.Controls.Melding
{
    public class MeldingHandler : IMeldingHandler
    {
        #region Member fields

        private readonly Window _owner;

        #endregion

        public MeldingHandler(Window owner)
        {
            _owner = owner;
        }

        #region IMeldingHandler Members

        public bool ShowMeldingJaNee(string melding)
        {
            return MessageBox.Show(_owner, melding, StringResources.ApplictatieNaam, MessageBoxButton.YesNo)
                == MessageBoxResult.Yes;
        }

        public void ShowMeldingOk(string melding)
        {
            MessageBox.Show(_owner, melding, StringResources.ApplictatieNaam, MessageBoxButton.OK);
        }

        public bool ShowMeldingOkCancel(string melding)
        {
            return MessageBox.Show(_owner, melding, StringResources.ApplictatieNaam, MessageBoxButton.OKCancel)
                == MessageBoxResult.OK;
        }

        #endregion
    }
}