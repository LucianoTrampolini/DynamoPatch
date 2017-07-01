using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;

namespace Dynamo.Boekingssysteem.ViewModel.ChangeLog
{
    public class ChangeLogViewModel : EntityViewModel<Model.ChangeLog>
    {
        public ChangeLogViewModel(Model.ChangeLog changeLog)
            : base(changeLog) {}

        public string Beheerder
        {
            get
            {
                return _entity.AangemaaktDoor == null
                    ? ""
                    : _entity.AangemaaktDoor.Naam;
            }
        }

        public string Datum
        {
            get { return _entity.Datum.GetDynamoDatumTijd(); }
        }

        public string Eigenschap
        {
            get { return _entity.Eigenschap; }
        }

        public string Entiteit
        {
            get { return _entity.Entiteit; }
        }

        public string NieuweWaarde
        {
            get { return _entity.NieuweWaarde; }
        }

        public string Omschrijving
        {
            get { return _entity.Omschrijving; }
        }

        public string OudeWaarde
        {
            get { return _entity.OudeWaarde; }
        }
    }
}