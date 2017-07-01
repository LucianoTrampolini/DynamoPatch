using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem;
using System.Collections.ObjectModel;
using Dynamo.Common;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.BL;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Boekingssysteem.ViewModel.Beheerder;
using Dynamo.Common.Properties;
namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class PlanningOverichtViewModel : WorkspaceViewModel
    {
        private CommandViewModel _vorigeweek;
        private CommandViewModel _volgendeweek;
        private CommandViewModel _dezeweek;
        private DateTime _huidigeDatum;
        private List<Model.Oefenruimte> _oefenruimtes;
        private List<Model.Base.StamgegevenBase> _dagdelen;

        public ObservableCollection<DatumViewModel> Dagen { get; private set; }
        public ObservableCollection<DagOefenruimteViewModel> BoekingenOefenruimte1 { get; private set; }
        public ObservableCollection<DagOefenruimteViewModel> BoekingenOefenruimte2 { get; private set; }
        public ObservableCollection<DagOefenruimteViewModel> BoekingenOefenruimte3 { get; private set; }
        public event EventHandler<ShowLogboekEventArgs> OnShowLogboek;

        public string HuidigeWeek
        {
            get { return string.Format("Week {0}", _huidigeDatum.GetIsoWeekNr()); }
        }

        public string HuidigJaar
        {
            get { return _huidigeDatum.Year.ToString(); }
        }

        public DateTime HuidigeDatum
        {
            get { return _huidigeDatum; }
        }

        public CommandViewModel VorigeWeek 
        {
            get 
            {
                if (_vorigeweek == null)
                {
                    _vorigeweek = new CommandViewModel("<", new RelayCommand(param => this.SetVorigeWeek()));
                }
                return _vorigeweek;
            }
        }

        public CommandViewModel VolgendeWeek
        {
            get
            {
                if (_volgendeweek == null)
                {
                    _volgendeweek = new CommandViewModel(">",new RelayCommand(param => this.SetVolgendeWeek()));
                }
                return _volgendeweek;
            }
        }

        public CommandViewModel DezeWeek
        {
            get
            {
                if (_dezeweek == null)
                {
                    _dezeweek = new CommandViewModel("Home", new RelayCommand(param => this.SetDezeWeek()));
                }
                return _dezeweek;
            }
        }

        public PlanningOverichtViewModel()
        {
            DisplayName = StringResources.ButtonPlanning;
            SetVandaag();
        }

        private void SetVandaag()
        {
            _huidigeDatum = DateTime.Today.GetEersteDagVanDeWeek();
            
            LoadBasisGegevens();

            ShowPlanningForWeek();
        }

        private void LoadBasisGegevens()
        {
            using (var repo = new PlanningRepository())
            {
                _oefenruimtes = repo.GetOefenruimtes();
                if (_oefenruimtes.Count != 3)
                {
                    throw new Exception("Aantal oefenruimtes moet in deze versie exact 3 zijn!");
                }
                _dagdelen = repo.GetStamGegevens(BL.Enum.Stamgegevens.Dagdelen);
                if (_dagdelen.Count != 3)
                {
                    throw new Exception("Aantal dagdelen moet in deze versie exact 3 zijn!");
                }
            }
        }

        private void ShowPlanningForWeek()
        {
            ResetDagen();
            LoadPlanning();

            OnPropertyChanged("Dagen");
            OnPropertyChanged("BoekingenOefenruimte1");
            OnPropertyChanged("BoekingenOefenruimte2");
            OnPropertyChanged("BoekingenOefenruimte3");
            OnPropertyChanged("HuidigeWeek");
            OnPropertyChanged("HuidigJaar");
        }

        private void LoadPlanning()
        {
            ClearPlanning();

            var datumTot = _huidigeDatum.AddDays(7);
            using (var repo = new PlanningRepository())
            {
                var weekPlanning = repo.Load(x => x.Datum >= _huidigeDatum && x.Datum < datumTot);

                foreach (var item in weekPlanning)
                {
                    switch (item.OefenruimteId)
                    {
                        case 1:
                            //TODO
                            BoekingenOefenruimte1[item.Datum.DagVanDeWeek() - 1].SetPlanning(item);
                            break;
                        case 2:
                            //TODO
                            BoekingenOefenruimte2[item.Datum.DagVanDeWeek() - 1].SetPlanning(item);
                            break;
                        case 3:
                            //TODO
                            BoekingenOefenruimte3[item.Datum.DagVanDeWeek() - 1].SetPlanning(item);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("Oefenruimte: " + item.OefenruimteId.ToString());
                    }
                }
            }
        }

        private void ClearPlanning()
        {
            if (BoekingenOefenruimte1 == null)
            {
                BoekingenOefenruimte1 = GetEmptyDagOefenruimteCollection(_oefenruimtes[0]);
                BoekingenOefenruimte2 = GetEmptyDagOefenruimteCollection(_oefenruimtes[1]);
                BoekingenOefenruimte3 = GetEmptyDagOefenruimteCollection(_oefenruimtes[2]);
            }
        }

        private ObservableCollection<DagOefenruimteViewModel> GetEmptyDagOefenruimteCollection(Model.Oefenruimte oefenruimte)
        {
            var returnValue = new ObservableCollection<DagOefenruimteViewModel>
            {
                new DagOefenruimteViewModel(_huidigeDatum,oefenruimte),
                new DagOefenruimteViewModel(_huidigeDatum.AddDays(1),oefenruimte),
                new DagOefenruimteViewModel(_huidigeDatum.AddDays(2),oefenruimte),
                new DagOefenruimteViewModel(_huidigeDatum.AddDays(3),oefenruimte),
                new DagOefenruimteViewModel(_huidigeDatum.AddDays(4),oefenruimte),
                new DagOefenruimteViewModel(_huidigeDatum.AddDays(5),oefenruimte),
                new DagOefenruimteViewModel(_huidigeDatum.AddDays(6),oefenruimte)
            };

            foreach (var item in returnValue)
            {
                item.AvondBoekingChanging = OnAvondBoekingChanging;
                item.MiddagBoekingChanging = OnMiddagBoekingChanging;
            }

            return returnValue;
        }

        private void OnAvondBoekingChanging(object s, DagOefenruimteChangingEventArgs e)
        {
            if (e.Datum <= DateTime.Today.AddDays(-7))
            {
                return;
            }
            if (e.IsNieuweBoeking)
            {
                SwitchViewModel(new BandAanmeldenViewModel(e.Planning, _dagdelen[2] as Model.Dagdeel, e.Datum, e.Oefenruimte));
            }
            else 
            {
                SwitchViewModel(new BandAfmeldenViewModel(e.Planning));
            }
        }

        private void OnMiddagBoekingChanging(object s, DagOefenruimteChangingEventArgs e)
        {
            if (e.Datum <= DateTime.Today.AddDays(-7))
            {
                return;
            }
            if (e.IsNieuweBoeking)
            {
                SwitchViewModel(new BandAanmeldenViewModel(e.Planning, _dagdelen[1] as Model.Dagdeel, e.Datum, e.Oefenruimte));
            }
            else
            {
                SwitchViewModel(new BandAfmeldenViewModel(e.Planning));
            }
        }

        protected override void OnSubViewModelClosed()
        {
            LoadPlanning();
        }

        private void ResetDagen()
        {
            if (Dagen != null)
            {
                Dagen.Clear();
            }

            Dagen = new ObservableCollection<DatumViewModel>
            {
                new DatumViewModel(_huidigeDatum),
                new DatumViewModel(_huidigeDatum.AddDays(1)),
                new DatumViewModel(_huidigeDatum.AddDays(2)),
                new DatumViewModel(_huidigeDatum.AddDays(3)),
                new DatumViewModel(_huidigeDatum.AddDays(4)),
                new DatumViewModel(_huidigeDatum.AddDays(5)),
                new DatumViewModel(_huidigeDatum.AddDays(6))
            };
            foreach (var datumvm in Dagen)
            {
                datumvm.OnShowLogboek += new EventHandler<ShowLogboekEventArgs>(datumvm_OnShowLogboek);
            }
        }

        void datumvm_OnShowLogboek(object sender, ShowLogboekEventArgs e)
        {
            if (OnShowLogboek != null)
            {
                OnShowLogboek(sender, e);
            }
        }

        private void SetVorigeWeek()
        {
            _huidigeDatum = _huidigeDatum.AddDays(-7);
            BoekingenOefenruimte1.ToList().ForEach(x => x.WeekTerug());
            BoekingenOefenruimte2.ToList().ForEach(x => x.WeekTerug());
            BoekingenOefenruimte3.ToList().ForEach(x => x.WeekTerug());
            ShowPlanningForWeek();
        }

        private void SetVolgendeWeek()
        {
            _huidigeDatum = _huidigeDatum.AddDays(7);

            BoekingenOefenruimte1.ToList().ForEach(x => x.WeekVerder());
            BoekingenOefenruimte2.ToList().ForEach(x => x.WeekVerder());
            BoekingenOefenruimte3.ToList().ForEach(x => x.WeekVerder());
            ShowPlanningForWeek();
        }

        private void SetDezeWeek()
        {
            BoekingenOefenruimte1.ToList().ForEach(x => x.Reset());
            BoekingenOefenruimte2.ToList().ForEach(x => x.Reset());
            BoekingenOefenruimte3.ToList().ForEach(x => x.Reset());
            SetVandaag();
        }
    }
}
