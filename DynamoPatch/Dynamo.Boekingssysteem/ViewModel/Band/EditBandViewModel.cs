using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.BL.Enum;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Boekingssysteem.ViewModel.Planning;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;
using Dynamo.Common.Properties;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class EditBandViewModel : BandViewModel
    {
        #region Member fields

        private Contract _currentContract;
        private ObservableCollection<StamgegevenViewModel> _dagdelen;
        private ObservableCollection<OefenruimteViewModel> _oefenruimtes;
        private readonly BandRepository bandRepository = new BandRepository();

        #endregion

        public EditBandViewModel()
            : base(new Model.Band())
        {
            _entity.BandTypeId = 1; //TODO
            _entity.ContactPersonen.Add(new ContactPersoon());
            _entity.ContactPersonen.Add(new ContactPersoon());
        }

        public EditBandViewModel(Model.Band band)
            : base(band) {}

        public int Backline
        {
            get { return CurrentContract.Backline; }
            set
            {
                if (CurrentContract.Backline == value)
                    return;

                CurrentContract.Backline = value;

                OnPropertyChanged("Backline");
            }
        }

        public decimal Borg
        {
            get { return CurrentContract.Borg; }
            set
            {
                if (CurrentContract.Borg == value)
                    return;

                CurrentContract.Borg = value;

                OnPropertyChanged("Borg");
            }
        }

        public string Contactpersoon1Adres
        {
            get { return Contactpersoon1.Adres; }
            set
            {
                if (value == Contactpersoon1.Adres)
                    return;

                Contactpersoon1.Adres = value;

                OnPropertyChanged("Contactpersoon1Adres");
            }
        }

        public string Contactpersoon1Email
        {
            get { return Contactpersoon1.Email; }
            set
            {
                if (value == Contactpersoon1.Email)
                    return;

                Contactpersoon1.Email = value;
                OnPropertyChanged("Contactpersoon1Email");
            }
        }

        public string Contactpersoon1Naam
        {
            get { return Contactpersoon1.Naam; }
            set
            {
                if (value == Contactpersoon1.Naam)
                    return;

                Contactpersoon1.Naam = value;

                OnPropertyChanged("Contactpersoon1Naam");
            }
        }

        public string Contactpersoon1Plaats
        {
            get { return Contactpersoon1.Plaats; }
            set
            {
                if (value == Contactpersoon1.Plaats)
                    return;

                Contactpersoon1.Plaats = value;

                OnPropertyChanged("Contactpersoon1Plaats");
            }
        }

        public string Contactpersoon1Telefoon
        {
            get { return Contactpersoon1.Telefoon; }
            set
            {
                if (value == Contactpersoon1.Telefoon)
                    return;

                Contactpersoon1.Telefoon = value;

                OnPropertyChanged("Contactpersoon1Telefoon");
            }
        }

        public string Contactpersoon2Adres
        {
            get { return Contactpersoon2.Adres; }
            set
            {
                if (value == Contactpersoon2.Adres)
                    return;

                Contactpersoon2.Adres = value;

                OnPropertyChanged("Contactpersoon2Adres");
            }
        }

        public string Contactpersoon2Email
        {
            get { return Contactpersoon2.Email; }
            set
            {
                if (value == Contactpersoon2.Email)
                    return;

                Contactpersoon2.Email = value;
                OnPropertyChanged("Contactpersoon2Email");
            }
        }

        public string Contactpersoon2Naam
        {
            get { return Contactpersoon2.Naam; }
            set
            {
                if (value == Contactpersoon2.Naam)
                    return;

                Contactpersoon2.Naam = value;

                OnPropertyChanged("Contactpersoon2Naam");
            }
        }

        public string Contactpersoon2Plaats
        {
            get { return Contactpersoon2.Plaats; }
            set
            {
                if (value == Contactpersoon2.Plaats)
                    return;

                Contactpersoon2.Plaats = value;

                OnPropertyChanged("Contactpersoon2Plaats");
            }
        }

        public string Contactpersoon2Telefoon
        {
            get { return Contactpersoon2.Telefoon; }
            set
            {
                if (value == Contactpersoon2.Telefoon)
                    return;

                Contactpersoon2.Telefoon = value;

                OnPropertyChanged("Contactpersoon2Telefoon");
            }
        }

        public string ContractPer
        {
            get { return CurrentContract.BeginContract.GetDynamoDatum(); }
            set
            {
                if (value == CurrentContract.BeginContract.GetDynamoDatum())
                    return;

                if (CommonMethods.IsDynamoDatum(value))
                {
                    CurrentContract.BeginContract = CommonMethods.DynamoDatum2Datum(value);
                    OnPropertyChanged("ContractPer");
                }
            }
        }

        public bool Crash
        {
            get { return CurrentContract.Crash; }
            set
            {
                if (CurrentContract.Crash == value)
                    return;

                CurrentContract.Crash = value;

                OnPropertyChanged("Crash");
            }
        }

        public int DagdeelId
        {
            get { return CurrentContract.DagdeelId; }
            set
            {
                if (CurrentContract.DagdeelId == value)
                {
                    return;
                }
                CurrentContract.DagdeelId = value;
                CurrentContract.Dagdeel = Dagdelen.FirstOrDefault(x => x.Id == value)
                    .GetEntity() as Dagdeel;
                OnPropertyChanged("DagdeelId");
            }
        }

        public ObservableCollection<StamgegevenViewModel> Dagdelen
        {
            get
            {
                if (_dagdelen == null)
                {
                    List<StamgegevenViewModel> alleDagdelen =
                        (from cust in bandRepository.GetStamGegevens(Stamgegevens.Dagdelen)
                            .Where(x => x.Id > 1)
                            select new StamgegevenViewModel(cust)).ToList();

                    _dagdelen = new ObservableCollection<StamgegevenViewModel>(alleDagdelen);
                }
                return _dagdelen;
            }
        }

        public string EindeContract
        {
            get
            {
                return CurrentContract.EindeContract.HasValue
                    ? CurrentContract.EindeContract.Value.GetDynamoDatum()
                    : string.Empty;
            }
            set
            {
                if (CurrentContract.EindeContract.HasValue == false
                    || value != CurrentContract.EindeContract.Value.GetDynamoDatum())
                {
                    if (CommonMethods.IsDynamoDatum(value))
                    {
                        CurrentContract.EindeContract = CommonMethods.DynamoDatum2Datum(value);
                        OnPropertyChanged("EindeContract");
                    }
                }
            }
        }

        public int ExtraVersterkers
        {
            get { return CurrentContract.ExtraVersterkers; }
            set
            {
                if (CurrentContract.ExtraVersterkers == value)
                    return;

                CurrentContract.ExtraVersterkers = value;

                OnPropertyChanged("ExtraVersterkers");
            }
        }

        public bool IsNieuwContract
        {
            get { return CurrentContract.IsTransient(); }
        }

        public decimal MaandHuur
        {
            get { return CurrentContract.MaandHuur; }
            set
            {
                if (CurrentContract.MaandHuur == value)
                    return;

                CurrentContract.MaandHuur = value;

                OnPropertyChanged("MaandHuur");
            }
        }

        public int Microfoons
        {
            get { return CurrentContract.Microfoons; }
            set
            {
                if (CurrentContract.Microfoons == value)
                    return;

                CurrentContract.Microfoons = value;

                OnPropertyChanged("Microfoons");
            }
        }

        public int OefenDag
        {
            get { return CurrentContract.Oefendag; }
            set
            {
                if (value == CurrentContract.Oefendag)
                    return;

                CurrentContract.Oefendag = value;

                OnPropertyChanged("OefenDag");
            }
        }

        public List<KeyValuePair<int, string>> OefenDagen
        {
            get { return CommonMethods.GetOefenDagenList(); }
        }

        public int OefenruimteId
        {
            get { return CurrentContract.OefenruimteId; }
            set
            {
                if (CurrentContract.OefenruimteId == value)
                {
                    return;
                }
                CurrentContract.OefenruimteId = value;
                CurrentContract.Oefenruimte = Oefenruimtes.FirstOrDefault(x => x.Id == value)
                    .GetEntity();
                OnPropertyChanged("OefenruimteId");
            }
        }

        public ObservableCollection<OefenruimteViewModel> Oefenruimtes
        {
            get
            {
                if (_oefenruimtes == null)
                {
                    List<OefenruimteViewModel> alleOefenruimtes =
                        (from oefenruimte in bandRepository.GetOefenruimtes()
                            select new OefenruimteViewModel(oefenruimte)).ToList();

                    _oefenruimtes = new ObservableCollection<OefenruimteViewModel>(alleOefenruimtes);
                }
                return _oefenruimtes;
            }
        }

        private ContactPersoon Contactpersoon1
        {
            get { return _entity.ContactPersonen.First(); }
        }

        private ContactPersoon Contactpersoon2
        {
            get
            {
                if (_entity.ContactPersonen.Count == 1)
                {
                    _entity.ContactPersonen.Add(new ContactPersoon());
                }

                return _entity.ContactPersonen.Last();
            }
        }

        private Contract CurrentContract
        {
            get
            {
                if (_currentContract == null
                    && (GetEntity()
                        .Contracten == null || GetEntity()
                            .Contracten.Count() == 0))
                {
                    _currentContract = new Contract();
                    _entity.Contracten.Add(_currentContract);
                }
                else
                {
                    _currentContract = GetEntity()
                        .Contracten.FirstOrDefault(contract => contract.EindeContract == null) ?? GetEntity()
                            .Contracten.FirstOrDefault(
                                contract =>
                                    contract.BeginContract > DateTime.Today && contract.EindeContract <= DateTime.Today)
                            ?? GetEntity()
                                .Contracten.Last();
                }
                return _currentContract;
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOpslaan,
                    new RelayCommand(param => Opslaan(), param => KanOpslaan())),
                new CommandViewModel(
                    StringResources.ButtonNieuwContract,
                    new RelayCommand(param => NieuwContract(), param => KanVernieuwen())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        protected override void OnDispose()
        {
            bandRepository.Dispose();
        }

        private bool KanOpslaan()
        {
            return !string.IsNullOrWhiteSpace(Naam) && DagdeelId > 0 && OefenDag > 0 && OefenruimteId > 0;
        }

        private bool KanVernieuwen()
        {
            return _entity.Contracten.Last()
                .Id != 0;
        }

        private void NieuwContract()
        {
            if (
                !Helper.MeldingHandler.ShowMeldingOkCancel(
                    "Let op! Er wordt een nieuw contract klaar gezet op basis van het vorige. Het oude contract verloopt op het moment dat het nieuwe in gaat."))
            {
                //Geannuleerd...
                return;
            }

            var contract = CurrentContract;
            _entity.Contracten.Add(
                new Contract
                {
                    Backline = contract.Backline,
                    BandId = _entity.Id,
                    BeginContract = DateTime.Today,
                    Borg = contract.Borg,
                    Crash = contract.Crash,
                    DagdeelId = contract.DagdeelId,
                    ExtraVersterkers = contract.ExtraVersterkers,
                    MaandHuur = contract.MaandHuur,
                    Microfoons = contract.Microfoons,
                    Oefendag = contract.Oefendag,
                    OefenruimteId = contract.OefenruimteId
                });
            OnPropertyChanged("IsNieuwContract");
            OnPropertyChanged("ContractPer");
            OnPropertyChanged("EindeContract");
        }

        private void Opslaan()
        {
            bandRepository.Save(_entity);

            new WebIntegrationHelper().PushAllAsync();
            CloseCommand.Execute(null);
        }
    }
}