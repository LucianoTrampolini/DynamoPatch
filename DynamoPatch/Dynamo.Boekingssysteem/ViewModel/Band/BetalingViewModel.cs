using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BetalingViewModel : EntityViewModel<Betaling>
    {
        public BetalingViewModel()
            : base(new Betaling())
        {
            Opmerking = "Betaling";
        }

        public BetalingViewModel(Betaling betaling)
            : base(betaling) {}

        public decimal Bedrag
        {
            get { return _entity.Bedrag; }
            set { _entity.Bedrag = value; }
        }

        public string BedragBetaald
        {
            get
            {
                return _entity.Bedrag < 0
                    ? (_entity.Bedrag * -1).GetDynamoBedrag()
                    : string.Empty;
            }
        }

        public string BedragTeBetalen
        {
            get
            {
                return _entity.Bedrag > 0
                    ? _entity.Bedrag.GetDynamoBedrag()
                    : string.Empty;
            }
        }

        public string Datum
        {
            get { return _entity.Datum.GetDynamoDatum(); }
        }

        public string GewijzigdDoor
        {
            get
            {
                return _entity.GewijzigdDoor == null
                    ? _entity.AangemaaktDoor.Naam
                    : _entity.GewijzigdDoor.Naam;
            }
        }

        public string GewijzigdOp
        {
            get { return _entity.Gewijzigd.GetDynamoDatum(); }
        }

        public string Opmerking
        {
            get { return _entity.Opmerking; }
            set { _entity.Opmerking = value; }
        }
    }
}