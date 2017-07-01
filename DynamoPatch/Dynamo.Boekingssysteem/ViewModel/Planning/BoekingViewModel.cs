using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class BoekingViewModel : EntityViewModel<Boeking>
    {
        public BoekingViewModel(Boeking boeking)
            : base(boeking) {}

        public string AangemaaktDoor
        {
            get
            {
                return _entity.AangemaaktDoor != null
                    ? _entity.AangemaaktDoor.Naam
                    : "Herman";
            }
        }

        public string AangemaaktOp
        {
            get { return _entity.Aangemaakt.GetDynamoDatum(); }
        }

        public string BandNaam
        {
            get { return _entity.BandNaam; }
        }

        //Puur om de datum waarop is geboekt weer te geven. Zit eigenlijk in het planning object...
        public string Datum { get; set; }

        public string GewijzigdDoor
        {
            get
            {
                return _entity.GewijzigdDoor != null
                    ? _entity.GewijzigdDoor.Naam
                    : string.Empty;
            }
        }

        public string GewijzigdOp
        {
            get { return _entity.Gewijzigd.GetDynamoDatum(); }
        }

        public string Opmerking
        {
            get { return _entity.Opmerking; }
            set
            {
                if (value == _entity.Opmerking)
                {
                    return;
                }
                _entity.Opmerking = value;
                OnPropertyChanged("Opmerking");
            }
        }
    }
}