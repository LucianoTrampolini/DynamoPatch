using System;
using System.Linq;
using System.Windows.Input;

using Dynamo.BL;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.Common;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class PlanningViewModel : EntityViewModel<Model.Planning>
    {
        #region Member fields

        private RelayCommand _change;
        private readonly bool _gesloten;
        public EventHandler<BoekingChangingEventArgs> BoekingChanging;

        #endregion

        public PlanningViewModel(Model.Planning planning)
            : base(planning)
        {
            if (planning == null
                || planning.IsTransient())
            {
                return;
            }

            //TODO BLCONTROLLER
            //x => x.Verwijderd == false && x.DatumVan <= planning.Datum && (x.DatumTot.HasValue == false || x.DatumTot.Value >= planning.Datum)
            using (var planningRepository = new PlanningRepository())
            {
                var gesloten = planningRepository.LoadGesloten(planning.Datum);
                foreach (var g in gesloten)
                {
                    if (planning.DagdeelId == 2 && g.Middag
                        || planning.DagdeelId == 3 && g.Avond)
                    {
                        if (planning.OefenruimteId == 1 && g.Oefenruimte1
                            || planning.OefenruimteId == 2 && g.Oefenruimte2
                            || planning.OefenruimteId == 3 && g.Oefenruimte3)
                        {
                            if (planning.Datum.DagVanDeWeek() == 1 && g.Middag
                                || planning.Datum.DagVanDeWeek() == 2 && g.Dinsdag
                                || planning.Datum.DagVanDeWeek() == 3 && g.Woensdag
                                || planning.Datum.DagVanDeWeek() == 4 && g.Donderdag
                                || planning.Datum.DagVanDeWeek() == 5 && g.Vrijdag
                                || planning.Datum.DagVanDeWeek() == 6 && g.Zaterdag
                                || planning.Datum.DagVanDeWeek() == 7 && g.Zondag)
                            {
                                _gesloten = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public string BandNaam
        {
            get
            {
                if (_entity == null
                    || _entity.Boekingen == null
                    || !_entity.Boekingen.Any(x => x.DatumAfgezegd == null && !x.Verwijderd))
                {
                    return string.Empty;
                }
                return _entity.Boekingen.OrderByDescending(x => x.Id)
                    .First(x => x.DatumAfgezegd == null && !x.Verwijderd)
                    .BandNaam;
            }
        }

        public ICommand BoekingClick
        {
            get
            {
                if (_change == null)
                {
                    _change = new RelayCommand(param => RaisePlanningChangingEvent());
                }
                return _change;
            }
        }

        public bool NietGesloten
        {
            get
            {
                return _entity == null
                    ? true
                    : !_gesloten;
            }
        }

        protected override void OnDispose()
        {
            _entity = null;
        }

        private void RaisePlanningChangingEvent()
        {
            if (BoekingChanging != null)
            {
                BoekingChanging(
                    this,
                    new BoekingChangingEventArgs
                    {
                        IsNieuweBoeking = BandNaam == "",
                        Planning = _entity
                    });
            }
        }
    }

    public class BoekingChangingEventArgs : EventArgs
    {
        #region Member fields

        public bool IsNieuweBoeking;
        public Model.Planning Planning;

        #endregion
    }
}