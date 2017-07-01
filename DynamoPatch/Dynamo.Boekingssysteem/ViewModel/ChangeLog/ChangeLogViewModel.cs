using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.ChangeLog
{
    public class ChangeLogViewModel: EntityViewModel<Model.ChangeLog>
    {
        public string Datum 
        {
            get { return _entity.Datum.GetDynamoDatumTijd(); }
        }

        public string Beheerder
        {
            get { return _entity.AangemaaktDoor == null ? "" : _entity.AangemaaktDoor.Naam; }
        }

        public string Entiteit
        {
            get { return _entity.Entiteit; }
        }

        public string Omschrijving
        {
            get { return _entity.Omschrijving; }
        }

        public string Eigenschap
        {
            get { return _entity.Eigenschap; }
        }

        public string OudeWaarde
        {
            get { return _entity.OudeWaarde; }
        }

        public string NieuweWaarde
        {
            get { return _entity.NieuweWaarde; }
        }

        public ChangeLogViewModel(Model.ChangeLog changeLog)
            : base(changeLog)
        { }
    }
}
