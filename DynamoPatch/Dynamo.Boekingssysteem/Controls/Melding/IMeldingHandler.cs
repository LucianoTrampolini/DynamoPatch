namespace Dynamo.Boekingssysteem.Controls.Melding
{
    public interface IMeldingHandler
    {
        bool ShowMeldingJaNee(string melding);
        void ShowMeldingOk(string melding);
        bool ShowMeldingOkCancel(string melding);
    }
}