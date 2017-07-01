using System;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;
using Dynamo.Common.Properties;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class LogboekWeekOverzichtViewModel : WorkspaceViewModel
    {
        #region Member fields

        private CommandViewModel _dezeWeek;
        private DateTime _huidigeDatum = DateTime.Today;
        private CommandViewModel _volgendeWeek;
        private CommandViewModel _vorigeWeek;

        #endregion

        public LogboekWeekOverzichtViewModel()
        {
            _huidigeDatum = DateTime.Today;
            LoadPlanningsDagenDezeWeek();
            DisplayName = StringResources.ButtonWeekOverzicht;
            RequestClose += LogboekViewModel_RequestClose;
        }

        public ObservableCollection<DatumViewModel> Dagen { get; private set; }

        public CommandViewModel DezeWeek
        {
            get
            {
                if (_dezeWeek == null)
                {
                    _dezeWeek = new CommandViewModel("Home", new RelayCommand(param => DezeWeekCommand()));
                }
                return _dezeWeek;
            }
        }

        public DateTime HuidigeDatum
        {
            private get { return _huidigeDatum; }
            set
            {
                _huidigeDatum = value;
                OnPropertyChanged("Week");
            }
        }

        public string Jaar
        {
            get { return _huidigeDatum.Year.ToString(); }
        }

        public ObservableCollection<PlanningsDagViewModel> Opmerkingen { get; private set; }

        public CommandViewModel VolgendeWeek
        {
            get
            {
                if (_volgendeWeek == null)
                {
                    _volgendeWeek = new CommandViewModel(">", new RelayCommand(param => VolgendeWeekCommand()));
                }
                return _volgendeWeek;
            }
        }

        public CommandViewModel VorigeWeek
        {
            get
            {
                if (_vorigeWeek == null)
                {
                    _vorigeWeek = new CommandViewModel("<", new RelayCommand(param => VorigeWeekCommand()));
                }
                return _vorigeWeek;
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

        public event EventHandler<ShowLogboekEventArgs> OnShowLogboek;

        void datumvm_OnShowLogboek(object sender, ShowLogboekEventArgs e)
        {
            if (OnShowLogboek != null)
            {
                OnShowLogboek(sender, e);
            }
        }

        private void DezeWeekCommand()
        {
            HuidigeDatum = DateTime.Today;
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
                var planningsDagen = repo.Load(pd => pd.Datum >= _huidigeDatum && pd.Datum < datumTot)
                    .OrderBy(pd => pd.Datum)
                    .ToList();

                while (datumVan != datumTot)
                {
                    var planningsDag = planningsDagen.FirstOrDefault(p => p.Datum == datumVan);
                    if (planningsDag == null)
                    {
                        Opmerkingen.Add(
                            new PlanningsDagViewModel(
                                new PlanningsDag
                                {
                                    Datum = datumVan
                                }));
                    }
                    else
                    {
                        Opmerkingen.Add(new PlanningsDagViewModel(planningsDag));
                    }
                    datumVan = datumVan.AddDays(1);
                }
            }
            OnPropertyChanged("Opmerkingen");
        }

        private void LoadPlanningsDagenDezeWeek()
        {
            SetDagenVanDeWeek();
            LoadOpmerkingen();
        }

        private void LogboekViewModel_RequestClose(object sender, EventArgs e)
        {
            SavePlanningsDagenDezeWeek();
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
                datumvm.OnShowLogboek += datumvm_OnShowLogboek;
            }
            OnPropertyChanged("Dagen");
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

        private void VolgendeWeekCommand()
        {
            HuidigeDatum = HuidigeDatum.AddDays(7);
        }

        private void VorigeWeekCommand()
        {
            HuidigeDatum = HuidigeDatum.AddDays(-7);
        }
    }
}