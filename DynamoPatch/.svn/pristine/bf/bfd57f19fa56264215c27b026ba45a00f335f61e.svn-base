using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dynamo.BL;
using Dynamo.Boekingssysteem.ViewModel.Band;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;
using Dynamo.Common.Properties;
using Dynamo.Common.Constants;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class BandAanmeldenViewModel : EntityViewModel<Model.Planning>
    {
        private string _bandNaam;
        private string _telefoon;
        private bool _slechteErvaringVisible = false;
        private string _bandOpmerking = "";
        private string _opmerkingen = "Geboekt. ";
        private int _oefenruimteId = 0;
        private int _dagdeelId = 0;
        public string DagdeelOefenruimte { get; private set; }

        public ObservableCollection<BandViewModel> Bands { get; private set; }
        public ObservableCollection<string> BandsString { get; private set; }
        
        public string BandNaam
        {
            get
            {
                return _bandNaam ?? BandNaamGetypt;
            }
            set 
            {
                if (value == _bandNaam)
                {
                    return;
                }
                _bandNaam = value;

                if (string.IsNullOrWhiteSpace(_bandNaam))
                {
                    Telefoon = string.Empty;
                    _slechteErvaringVisible = false;
                }
                else
                {
                    var band = Bands.Where(x => x.Naam.ToLower() == _bandNaam.ToLower()).FirstOrDefault();
                    if (band != null)
                    {
                        Telefoon = band.Telefoon;
                        _slechteErvaringVisible = band.HeeftVerleden;
                        _bandOpmerking = band.Opmerkingen;
                    }
                    else
                    {
                        _slechteErvaringVisible = false;
                    }

                }
                OnPropertyChanged("BandNaam");
                OnPropertyChanged("SlechteErvaringVisible");
                OnPropertyChanged("BandOpmerking");
            }
        }

        public bool SlechteErvaringVisible
        {
            get { return _slechteErvaringVisible; }
        }

        public string BandOpmerking
        {
            get { return _bandOpmerking; }
        }

        public string BandNaamGetypt { get; set; }

        public string Telefoon
        {
            get
            {
                return _telefoon;
            }
            set
            {
                if (value == _telefoon)
                {
                    return;
                }
                _telefoon = value;
                OnPropertyChanged("Telefoon");
            }
        }

        public string Opmerkingen
        {
            get
            {
                return _opmerkingen;
            }
            set
            {
                if (value == _opmerkingen)
                {
                    return;
                }
                _opmerkingen = value;
                OnPropertyChanged("Opmerkingen");
            }
        }

        public BandAanmeldenViewModel(Model.Planning planning, Model.Dagdeel dagdeel, DateTime date, Model.Oefenruimte oefenruimte)
            : base(planning)
        {
            using (var repo = new PlanningRepository())
            {
                if (_entity == null)
                {
                    _entity = new Model.Planning { Datum = date, Boekingen = new List<Model.Boeking>() };
                }

                _oefenruimteId = oefenruimte.Id;
                _dagdeelId = dagdeel.Id;
                DisplayName = string.Format("Boeking voor {0}, {1}", date.DagVanDeWeekVoluit(), date.GetDynamoDatum());
                DagdeelOefenruimte = string.Format("{0}, {1}", dagdeel.Omschrijving, oefenruimte.Naam);

                List<BandViewModel> all =
                    (from band in repo.GetBands()
                     select new BandViewModel(band)).ToList();
                this.Bands = new ObservableCollection<BandViewModel>(all);
                this.BandsString = new ObservableCollection<string>(all.Select(x => x.Naam));
            }
            OnPropertyChanged("Bands");
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => this.BandAanmelden(), param=>this.KanAanmelden())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private bool KanAanmelden()
        {
            return !string.IsNullOrWhiteSpace(BandNaam);
        }

        private void BandAanmelden()
        {
            using (var repo = new PlanningRepository())
            {
                Model.Band band;
                var bandViewModel = Bands.Where(x => x.Naam == BandNaam).FirstOrDefault();
                if (bandViewModel == null)
                {
                    band = new Model.Band
                    {
                        Naam = BandNaam,
                        Telefoon = Telefoon,
                        BandTypeId = BandTypeConsts.Incidenteel
                    };
                }
                else
                {
                    band = bandViewModel.GetEntity();
                    if (band.Telefoon != Telefoon)
                    {
                        band.Telefoon = Telefoon;
                        using (var bandrepo = new BandRepository())
                        {
                            bandrepo.Save(band);
                        }
                    }
                }

                var boeking = new Model.Boeking
                {
                    BandNaam = BandNaam,
                    Opmerking = string.Format("{0}{1} ", Opmerkingen, string.IsNullOrEmpty(band.Telefoon) ? "" : " " + band.Telefoon),
                    DatumGeboekt = DateTime.Today
                };

                if (band.IsTransient())
                {
                    boeking.Band = band;
                }
                else
                {
                    boeking.BandId = band.Id;
                }

                _entity.Boekingen.Add(boeking);
                _entity.OefenruimteId = _oefenruimteId;
                _entity.DagdeelId = _dagdeelId;
                _entity.Beschikbaar = false;

                repo.Save(_entity);
            }
            
            new WebIntegrationHelper().PushPlanning(_entity.Datum);

            CloseCommand.Execute(null);
        }
    }
}
