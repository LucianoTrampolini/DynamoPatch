using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.Common;
using Dynamo.Common.Properties;
using System.Collections.ObjectModel;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class LogboekWeekOverzichtViewModel : WorkspaceViewModel
    {
        private DateTime _huidigeDatum = DateTime.Today;
        private CommandViewModel _vorigeWeek;
        private CommandViewModel _volgendeWeek;
        private CommandViewModel _dezeWeek;

        public ObservableCollection<DatumViewModel> Dagen { get; private set; }
        public ObservableCollection<PlanningsDagViewModel> Opmerkingen { get; private set; }
        public event EventHandler<ShowLogboekEventArgs> OnShowLogboek;

        public DateTime HuidigeDatum
        {
            private get 
            {
                return _huidigeDatum;
            }
            set
            {
                _huidigeDatum = value;
                OnPropertyChanged("Week");
            }
        }

        public string Week
        {
            get
            {
                while (_huidigeDatum.DagVanDeWeek() != 1)
                {
                    _huidigeDatum = _huidigeDatum.AddDays(-1);
                }
                LoadPlanningsDagenDezeWeek();
                return string.Format("Week {0}", _huidigeDatum.GetIsoWeekNr());
            }
        }

        public string Jaar
        {
            get 
            {
                return _huidigeDatum.Year.ToString();
            }
        }

        public CommandViewModel VorigeWeek
        {
            get
            {
                if (_vorigeWeek == null)
                {
                    _vorigeWeek = new CommandViewModel("<", new RelayCommand(param => this.VorigeWeekCommand()));
                }
                return _vorigeWeek;
            }
        }

        public CommandViewModel VolgendeWeek
        {
            get
            {
                if (_volgendeWeek == null)
                {
                    _volgendeWeek = new CommandViewModel(">", new RelayCommand(param => this.VolgendeWeekCommand()));
                }
                return _volgendeWeek;
            }
        }
        public CommandViewModel DezeWeek
        {
            get
            {
                if (_dezeWeek == null)
                {
                    _dezeWeek = new CommandViewModel("Home", new RelayCommand(param => this.DezeWeekCommand()));
                }
                return _dezeWeek;
            }
        }

        private void VolgendeWeekCommand()
        {
            HuidigeDatum = HuidigeDatum.AddDays(7);
        }

        private void DezeWeekCommand()
        {
            HuidigeDatum = DateTime.Today;
        }

        private void VorigeWeekCommand()
        {
            HuidigeDatum = HuidigeDatum.AddDays(-7);
        }

        public LogboekWeekOverzichtViewModel()
        {
            _huidigeDatum = DateTime.Today;
            LoadPlanningsDagenDezeWeek();
            DisplayName = StringResources.ButtonWeekOverzicht;
            this.RequestClose += new EventHandler(LogboekViewModel_RequestClose);
        }

        private void LoadPlanningsDagenDezeWeek()
        {
            SetDagenVanDeWeek();
            LoadOpmerkingen();
        }


        private void LoadOpmerkingen()
        {
            if (Opmerkingen == null)
            {
                Opmerkingen = new ObservableCollection<PlanningsDagViewModel>();
            }
            Opmerkingen.Clear();

            using (var repo = new PlanningsDagRepository())
            {
                var datumVan = _huidigeDatum;
                var datumTot = _huidigeDatum.AddDays(7);
                var planningsDagen = repo.Load(pd => pd.Datum >= _huidigeDatum && pd.Datum < datumTot).OrderBy(pd=>pd.Datum).ToList();

                while (datumVan != datumTot)
                {
                    var planningsDag = planningsDagen.FirstOrDefault(p => p.Datum == datumVan);
                    if (planningsDag == null)
                    {
                        Opmerkingen.Add(new PlanningsDagViewModel(new Model.PlanningsDag{Datum = datumVan}));
                    }
                    else
                    { 
                        Opmerkingen.Add(new PlanningsDagViewModel(planningsDag));
                    }
                    datumVan=datumVan.AddDays(1);
                }
            }
            OnPropertyChanged("Opmerkingen");
        }

        private void SavePlanningsDagenDezeWeek()
        {
            //TODO Dit moet nog eens goed gaan werken...
            //using (var repo = new PlanningsDagRepository())
            //{

            //    foreach (var opmerking in Opmerkingen)
            //    {
            //        repo.Save(opmerking.GetEntity());
            //    }
            //}
        }

        /// <summary>
        /// Deze methode zet de huidigedatum op maandag en vult de datumviewmodels
        /// </summary>
        private void SetDagenVanDeWeek()
        {
            while (_huidigeDatum.DagVanDeWeek() != 1)
            {
                _huidigeDatum = _huidigeDatum.AddDays(-1);
            }
            ResetDagen();
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
            OnPropertyChanged("Dagen");
        }

        void datumvm_OnShowLogboek(object sender, ShowLogboekEventArgs e)
        {
            if (OnShowLogboek != null)
            {
                OnShowLogboek(sender, e);
            }
        }


        private void LogboekViewModel_RequestClose(object sender, EventArgs e)
        {
            SavePlanningsDagenDezeWeek();
        }
    }
}
