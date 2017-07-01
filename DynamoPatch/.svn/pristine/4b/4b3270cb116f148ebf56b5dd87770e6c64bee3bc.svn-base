using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;
using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class LogboekViewModel : WorkspaceViewModel
    {
        private PlanningsDagViewModel _planningsDag = null;

        private LogboekPlanningViewModel _oefenruimte1Middag = null;
        private LogboekPlanningViewModel _oefenruimte2Middag = null;
        private LogboekPlanningViewModel _oefenruimte3Middag = null;
        private LogboekPlanningViewModel _oefenruimte1Avond = null;
        private LogboekPlanningViewModel _oefenruimte2Avond = null;
        private LogboekPlanningViewModel _oefenruimte3Avond = null;

        public LogboekPlanningViewModel Oefenruimte1Middag
        {
            get { return _oefenruimte1Middag; }
            set 
            {
                _oefenruimte1Middag = value;
                OnPropertyChanged("Oefenruimte1Middag");
                OnPropertyChanged("Oefenruimte1MiddagVisible");
            }
        }

        public bool Oefenruimte1MiddagVisible
        {
            get { return _oefenruimte1Middag.Visible; }
        }

        public LogboekPlanningViewModel Oefenruimte2Middag
        {
            get { return _oefenruimte2Middag; }
            set
            {
                _oefenruimte2Middag = value;
                OnPropertyChanged("Oefenruimte2Middag");
                OnPropertyChanged("Oefenruimte2MiddagVisible");
            }
        }

        public bool Oefenruimte2MiddagVisible
        {
            get { return _oefenruimte2Middag.Visible; }
        }

        public LogboekPlanningViewModel Oefenruimte3Middag
        {
            get { return _oefenruimte3Middag; }
            set
            {
                _oefenruimte3Middag = value;
                OnPropertyChanged("Oefenruimte3Middag");
                OnPropertyChanged("Oefenruimte3MiddagVisible");
            }
        }

        public bool Oefenruimte3MiddagVisible
        {
            get { return _oefenruimte3Middag.Visible; }
        }

        public LogboekPlanningViewModel Oefenruimte1Avond
        {
            get { return _oefenruimte1Avond; }
            set
            {
                _oefenruimte1Avond = value;
                OnPropertyChanged("Oefenruimte1Avond");
                OnPropertyChanged("Oefenruimte1AvondVisible");
            }
        }

        public bool Oefenruimte1AvondVisible
        {
            get { return _oefenruimte1Avond.Visible; }
        }

        public LogboekPlanningViewModel Oefenruimte2Avond
        {
            get { return _oefenruimte2Avond; }
            set
            {
                _oefenruimte2Avond = value;
                OnPropertyChanged("Oefenruimte2Avond");
                OnPropertyChanged("Oefenruimte2AvondVisible");
            }
        }

        public bool Oefenruimte2AvondVisible
        {
            get { return _oefenruimte2Avond.Visible; }
        }

        public LogboekPlanningViewModel Oefenruimte3Avond
        {
            get { return _oefenruimte3Avond; }
            set
            {
                _oefenruimte3Avond = value;
                OnPropertyChanged("Oefenruimte3Avond");
                OnPropertyChanged("Oefenruimte3AvondVisible");
            }
        }

        public bool Oefenruimte3AvondVisible
        {
            get { return _oefenruimte3Avond.Visible; }
        }

        private DateTime _huidigeDatum;
        private CommandViewModel _vorigeDag;
        private CommandViewModel _volgendeDag;

        public DateTime HuidigeDatum
        {
            set
            {
                _huidigeDatum = value;
                OnPropertyChanged("Datum");
            }
        }

        public CommandViewModel VorigeDag
        {
            get
            {
                if (_vorigeDag == null)
                {
                    _vorigeDag = new CommandViewModel("<", new RelayCommand(param => this.VorigeDagCommand()));
                }
                return _vorigeDag;
            }
        }

        public CommandViewModel VolgendeDag
        {
            get
            {
                if (_volgendeDag == null)
                {
                    _volgendeDag = new CommandViewModel(">", new RelayCommand(param => this.VolgendeDagCommand()));
                }
                return _volgendeDag;
            }
        }

        public string Datum
        {
            get {
                SetPlanningsDag();
                return string.Format("{0}, {1}", _huidigeDatum.DagVanDeWeekVoluit(), _huidigeDatum.GetDynamoDatum());
            }
        }

        public string DagOpmerking
        {
            get { return _planningsDag.Opmerking; }
            set
            {
                if (_planningsDag.Opmerking== value)
                {
                    return;
                }
                _planningsDag.Opmerking = value;
                OnPropertyChanged("DagOpmerking");
            }
        }

        public string MiddagOpmerking
        {
            get { return _planningsDag.MiddagOpmerking; }
            set
            {
                if (_planningsDag.MiddagOpmerking == value)
                {
                    return;
                }
                _planningsDag.MiddagOpmerking = value;
                OnPropertyChanged("MiddagOpmerking");
            }
        }


        public string AvondOpmerking
        {
            get { return _planningsDag.AvondOpmerking; }
            set
            {
                if (_planningsDag.AvondOpmerking == value)
                {
                    return;
                }
                _planningsDag.AvondOpmerking = value;
                OnPropertyChanged("AvondOpmerking");
            }
        }


        public LogboekViewModel(DateTime datum)
        {
            _huidigeDatum = datum;
            DisplayName = StringResources.ButtonLogboek;
            OnPropertyChanged("Datum");
        }

        private void SetPlanningsDag()
        {
            SavePlanningsDag();
            using (var repo = new PlanningsDagRepository())
            {
                var planningsDag = repo.Load(pd => pd.Datum == _huidigeDatum).FirstOrDefault();
                _planningsDag = new PlanningsDagViewModel(planningsDag ?? new Model.PlanningsDag { Datum = _huidigeDatum });

                Oefenruimte1Middag = new LogboekPlanningViewModel(_planningsDag.GetEntity().Planningen.Where(x => x.OefenruimteId == 1 && x.DagdeelId == 2).FirstOrDefault());
                Oefenruimte2Middag = new LogboekPlanningViewModel(_planningsDag.GetEntity().Planningen.Where(x => x.OefenruimteId == 2 && x.DagdeelId == 2).FirstOrDefault());
                Oefenruimte3Middag = new LogboekPlanningViewModel(_planningsDag.GetEntity().Planningen.Where(x => x.OefenruimteId == 3 && x.DagdeelId == 2).FirstOrDefault());
                Oefenruimte1Avond = new LogboekPlanningViewModel(_planningsDag.GetEntity().Planningen.Where(x => x.OefenruimteId == 1 && x.DagdeelId == 3).FirstOrDefault());
                Oefenruimte2Avond = new LogboekPlanningViewModel(_planningsDag.GetEntity().Planningen.Where(x => x.OefenruimteId == 2 && x.DagdeelId == 3).FirstOrDefault());
                Oefenruimte3Avond = new LogboekPlanningViewModel(_planningsDag.GetEntity().Planningen.Where(x => x.OefenruimteId == 3 && x.DagdeelId == 3).FirstOrDefault());
            }
            
            OnPropertyChanged("DagOpmerking");
            OnPropertyChanged("MiddagOpmerking");
            OnPropertyChanged("AvondOpmerking");
        }

        private void SavePlanningsDag()
        {
            if (_planningsDag != null)
            {
                using (var repo = new PlanningsDagRepository())
                {
                    var planningsDag = repo.Load(pd => pd.Datum == _huidigeDatum).FirstOrDefault();
                    MergePlanningsDag(_planningsDag.GetEntity(), planningsDag);
                    repo.Save(_planningsDag.GetEntity());
                }
            }
        }

        private void MergePlanningsDag(Model.PlanningsDag oud, Model.PlanningsDag nieuw)
        {
            if (nieuw == null )
            {
                return;
            }

            foreach (var planning in nieuw.Planningen)
            { 
                var planningOud = oud.Planningen.FirstOrDefault(p=>p.Id==planning.Id);
                if (planningOud != null && planningOud.Gewijzigd != planning.Gewijzigd)
                {
                    planningOud.Gewijzigd = planning.Gewijzigd;
                    planningOud.GewijzigdDoorId = planning.GewijzigdDoorId;
                    planningOud.GeslotenId = planning.GeslotenId;
                    planningOud.Beschikbaar = planning.Beschikbaar;
                }
            }


            var boekingenNieuw = nieuw.Planningen.SelectMany(planning=>planning.Boekingen);
            var boekingenOud = oud.Planningen.SelectMany(planning=>planning.Boekingen);

            foreach (var boeking in boekingenNieuw.Where(b => b.Gewijzigd != null))
            {
                var boekingOud = boekingenOud.FirstOrDefault(b => b.Id == boeking.Id);
                if (boekingOud != null && (boekingOud.Gewijzigd == null ? DateTime.MinValue : boekingOud.Gewijzigd) < boeking.Gewijzigd)
                {
                    boekingOud.DatumAfgezegd = boeking.DatumAfgezegd;
                    boekingOud.Verwijderd = boeking.Verwijderd;
                    boekingOud.Gewijzigd = boeking.Gewijzigd;
                    boekingOud.GewijzigdDoor = boeking.GewijzigdDoor;
                }
            }
        }

        private void VolgendeDagCommand()
        {
            _huidigeDatum = _huidigeDatum.AddDays(1);
            OnPropertyChanged("Datum");
        }

        private void VorigeDagCommand()
        {
            _huidigeDatum= _huidigeDatum.AddDays(-1);
            OnPropertyChanged("Datum");
        }

        protected override void OnDispose()
        {
            SavePlanningsDag();
            base.OnDispose();
        }
    }
}
