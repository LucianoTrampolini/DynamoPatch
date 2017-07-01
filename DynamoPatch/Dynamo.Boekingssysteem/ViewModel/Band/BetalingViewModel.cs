using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Common;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BetalingViewModel :EntityViewModel<Model.Betaling>
    {
        public BetalingViewModel()
            :base(new Model.Betaling())
        {
            Opmerking = "Betaling";
        }

        public BetalingViewModel(Model.Betaling betaling)
            : base(betaling)
        { }

        public string Datum
        {
            get { return _entity.Datum.GetDynamoDatum(); }
        }

        public string BedragTeBetalen
        {
            get { return _entity.Bedrag > 0 ? _entity.Bedrag.GetDynamoBedrag() : string.Empty; }
        }

        public string BedragBetaald
        {
            get { return _entity.Bedrag < 0 ? (_entity.Bedrag * -1).GetDynamoBedrag() : string.Empty; }
        }

        public string Opmerking
        {
            get { return _entity.Opmerking; }
            set { _entity.Opmerking = value; }
        }

        public string GewijzigdDoor
        {
            get { return _entity.GewijzigdDoor == null ? _entity.AangemaaktDoor.Naam : _entity.GewijzigdDoor.Naam; }
        }

        public string GewijzigdOp
        {
            get { return _entity.Gewijzigd.GetDynamoDatum(); }
        }

        public decimal Bedrag
        {
            get { return _entity.Bedrag; }
            set { _entity.Bedrag = value; }
        }
    }
}
