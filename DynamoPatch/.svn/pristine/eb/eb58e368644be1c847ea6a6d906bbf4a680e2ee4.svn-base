using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Boekingssysteem.ViewModel.Base;
using System.Collections.ObjectModel;
using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BL.Enum;
using Dynamo.Common.Properties;
using System.Windows;
namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class AanmeldenViewModel : EntityViewModel<Model.Vergoeding>
    {
        private BeheerderViewModel _beheerder;
        
        public ObservableCollection<StamgegevenViewModel> Dagdelen { get; set; }
        public ObservableCollection<StamgegevenViewModel> Taken { get; set; }

        public int TaakId 
        {
            get 
            {
                return _entity.TaakId;
            }
            set 
            {
                if (_entity.TaakId == value)
                    return;

                _entity.TaakId = value;
                OnPropertyChanged("TaakId");
            }
        }

        public int DagdeelId
        {
            get
            {
                return _entity.DagdeelId;
            }
            set
            {
                if (_entity.DagdeelId == value)
                    return;

                _entity.DagdeelId = value;
                OnPropertyChanged("DagdeelId");
            }
        }

        public string BeheerderNaam 
        {
            get { return _beheerder.Naam; }
        }

        public AanmeldenViewModel(BeheerderViewModel beheerder)
            : base(new Model.Vergoeding())
        {
            if (beheerder == null)
            {
                throw new ArgumentNullException("beheerder");
            }

            _beheerder = beheerder;
            this.DisplayName = StringResources.ButtonAanmelden;
            using (var repo = new BeheerderRepository())
            {
                List<StamgegevenViewModel> alleDagdelen =
                        (from cust in repo.GetStamGegevens(Stamgegevens.Dagdelen)
                         select new StamgegevenViewModel(cust)).ToList();

                Dagdelen = new ObservableCollection<StamgegevenViewModel>(alleDagdelen);

                List<StamgegevenViewModel> alleTaken =
                        (from cust in repo.GetStamGegevens(Stamgegevens.Taken)
                         select new StamgegevenViewModel(cust)).ToList();

                Taken = new ObservableCollection<StamgegevenViewModel>(alleTaken);

            }
            _entity.Datum = DateTime.Today;
            TaakId = 1;
            DagdeelId = DateTime.Now.Hour < 18 ? 2 : 3;
            using (var repo = new InstellingRepository())
            {
                _entity.Bedrag = repo.Load(0).VergoedingBeheerder;
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => this.Aanmelden(), param=>this.KanAanmelden())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private bool KanAanmelden()
        {
            return (_entity.TaakId > 0 && _entity.DagdeelId > 0);
        }

        private void Aanmelden()
        {
            if (KanAanmelden())
            {
                using (var repo = new VergoedingRepository())
                {
                    _entity.Taak = repo.GetStamGegevens(Stamgegevens.Taken).FirstOrDefault(x => x.Id == _entity.TaakId) as Model.Taak;
                    _entity.Dagdeel = repo.GetStamGegevens(Stamgegevens.Dagdelen).FirstOrDefault(x => x.Id == _entity.DagdeelId) as Model.Dagdeel;
                    _entity.Beheerder = repo.GetBeheerder(_beheerder.Id);
                    repo.Save(_entity);
                    
                    if (repo.HasMelding)
                    {
                        Helper.MeldingHandler.ShowMeldingOk(repo.Melding);
                        return;
                    }

                    if (_entity.Taak.Omschrijving == "Beheer")
                    {
                        Helper.CurrentBeheerder = _entity.Beheerder;
                    }
                }
                CloseCommand.Execute(null);
            }
        }

        protected override void OnDispose()
        {
            Taken.Clear();
            Dagdelen.Clear();
        }
    }
}
