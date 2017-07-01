using System;

using Dynamo.BoekingsSysteem;
using Dynamo.Common.Constants;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class DagOefenruimteViewModel : ViewModelBase
    {
        #region Member fields

        private PlanningViewModel _avondBoeking;
        private DateTime _date;
        private readonly DateTime _initieleDatum;
        private PlanningViewModel _middagBoeking;
        private readonly Oefenruimte _oefenruimte;
        public EventHandler<DagOefenruimteChangingEventArgs> AvondBoekingChanging;
        public EventHandler<DagOefenruimteChangingEventArgs> MiddagBoekingChanging;

        #endregion

        public DagOefenruimteViewModel(DateTime date, Oefenruimte oefenruimte)
        {
            _date = date;
            _initieleDatum = date;
            _oefenruimte = oefenruimte;
        }

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

        public void WeekTerug()
        {
            ClearPlanning();
            _date = _date.AddDays(-7);
        }

        public void WeekVerder()
        {
            ClearPlanning();
            _date = _date.AddDays(7);
        }

        private void AvondChanging(object s, BoekingChangingEventArgs e)
        {
            if (AvondBoekingChanging != null)
            {
                AvondBoekingChanging(
                    s,
                    new DagOefenruimteChangingEventArgs(e)
                    {
                        Datum = _date,
                        Oefenruimte = _oefenruimte
                    });
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

        private void MiddagChanging(object s, BoekingChangingEventArgs e)
        {
            if (MiddagBoekingChanging != null)
            {
                MiddagBoekingChanging(
                    s,
                    new DagOefenruimteChangingEventArgs(e)
                    {
                        Datum = _date,
                        Oefenruimte = _oefenruimte
                    });
            }
        }
    }

    public class DagOefenruimteChangingEventArgs : BoekingChangingEventArgs
    {
        #region Member fields

        public DateTime Datum;
        public Oefenruimte Oefenruimte;

        #endregion

        public DagOefenruimteChangingEventArgs(BoekingChangingEventArgs e)
        {
            IsNieuweBoeking = e.IsNieuweBoeking;
            Planning = e.Planning;
        }
    }
}