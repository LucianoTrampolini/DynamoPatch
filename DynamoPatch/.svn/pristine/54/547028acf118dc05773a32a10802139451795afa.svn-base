using System;
using Dynamo.BoekingsSysteem;
using Dynamo.Common.Constants;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class DagOefenruimteViewModel : ViewModelBase
    {
        private PlanningViewModel _avondBoeking;
        private PlanningViewModel _middagBoeking;
        private DateTime _date;
        private DateTime _initieleDatum;
        private Model.Oefenruimte _oefenruimte;
        public EventHandler<DagOefenruimteChangingEventArgs> AvondBoekingChanging;
        public EventHandler<DagOefenruimteChangingEventArgs> MiddagBoekingChanging;

        public PlanningViewModel AvondBoeking
        {
            get
            {
                if (_avondBoeking == null)
                {
                    _avondBoeking = new PlanningViewModel(null);
                    _avondBoeking.BoekingChanging += AvondChanging;
                }
                return _avondBoeking;
            }
            private set
            {
                if (_avondBoeking == value)
                {
                    return;
                }
                _avondBoeking = value;
                if (_avondBoeking.BoekingChanging == null)
                {
                    _avondBoeking.BoekingChanging += AvondChanging;
                }
                OnPropertyChanged("AvondBoeking");
            }
        }

        private void AvondChanging(object s, BoekingChangingEventArgs e)
        {
            if (AvondBoekingChanging != null)
            {
                AvondBoekingChanging(s, new DagOefenruimteChangingEventArgs(e) { Datum = _date, Oefenruimte = _oefenruimte });
            }
        }

        private void MiddagChanging(object s, BoekingChangingEventArgs e)
        {
            if (MiddagBoekingChanging != null)
            {
                MiddagBoekingChanging(s, new DagOefenruimteChangingEventArgs(e) { Datum = _date, Oefenruimte = _oefenruimte });
            }
        }

        public PlanningViewModel MiddagBoeking
        {
            get
            {
                if (_middagBoeking == null)
                {
                    _middagBoeking = new PlanningViewModel(null);
                    _middagBoeking.BoekingChanging += MiddagChanging;
                }
                return _middagBoeking;
            }
            private set
            {
                if (_middagBoeking == value)
                {
                    return;
                }
                _middagBoeking = value;
                if (_middagBoeking.BoekingChanging == null)
                {
                    _middagBoeking.BoekingChanging += MiddagChanging;
                }
                OnPropertyChanged("MiddagBoeking");
            }
        }

        private void ClearPlanning()
        {
            if (AvondBoeking.GetEntity() != null)
            {
                AvondBoeking.Dispose();
                OnPropertyChanged("AvondBoeking");
            }
            if (MiddagBoeking.GetEntity() != null)
            {
                MiddagBoeking.Dispose();
                OnPropertyChanged("MiddagBoeking");
            }
        }

        public void WeekVerder()
        {
            ClearPlanning();
            _date = _date.AddDays(7);
        }

        public void WeekTerug()
        {
            ClearPlanning();
            _date = _date.AddDays(-7);
        }

        public void Reset()
        {
            ClearPlanning();
            _date = _initieleDatum;
        }

        public void SetPlanning(Model.Planning item)
        {
            if (item.DagdeelId == DagdeelConsts.Middag)
            {
                MiddagBoeking = new PlanningViewModel(item);
            }
            else if (item.DagdeelId == DagdeelConsts.Avond)
            {
                AvondBoeking = new PlanningViewModel(item);
            }
        }

        public DagOefenruimteViewModel(DateTime date, Model.Oefenruimte oefenruimte)
        {
            _date = date;
            _initieleDatum = date;
            _oefenruimte = oefenruimte;
        }
    }

    public class DagOefenruimteChangingEventArgs : BoekingChangingEventArgs
    {
        public DateTime Datum;
        public Model.Oefenruimte Oefenruimte;

        public DagOefenruimteChangingEventArgs(BoekingChangingEventArgs e)
        {
            IsNieuweBoeking = e.IsNieuweBoeking;
            Planning = e.Planning;
        }
    }
}
