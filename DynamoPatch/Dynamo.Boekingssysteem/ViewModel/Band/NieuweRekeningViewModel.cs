namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class NieuweRekeningViewModel : NieuweBetalingViewModel
    {
        public NieuweRekeningViewModel(BandViewModel band)
            : base(band) {}

        public new decimal Betaald
        {
            get { return _entity.Bedrag; }
            set
            {
                if (_entity.Bedrag == value)
                    return;

                _entity.Bedrag = value;

                OnPropertyChanged("Betaald");
            }
        }
    }
}