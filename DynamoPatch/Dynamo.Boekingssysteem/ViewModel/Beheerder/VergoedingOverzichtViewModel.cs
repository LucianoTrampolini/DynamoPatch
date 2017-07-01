using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL.Repository;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;
using Dynamo.Common.Properties;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class VergoedingOverzichtViewModel : SubItemViewModel<Vergoeding>
    {
        #region Member fields

        private readonly BeheerderViewModel _beheerder;

        private DateTime _huidgeDatum = DateTime.Today;
        private CommandViewModel _volgendeMaand;
        private CommandViewModel _vorigeMaand;

        #endregion

        public VergoedingOverzichtViewModel(BeheerderViewModel beheerder)
        {
            if (beheerder == null)
            {
                throw new ArgumentNullException("beheerder");
            }

            _beheerder = beheerder;

            CreateVergoedingenCollection();

            DisplayName = string.Format("{0} {1}", StringResources.ButtonVergoedingen, _beheerder.Naam);
        }

        public string HuidigeMaand
        {
            get { return string.Format("{0} {1}", _huidgeDatum.MaandVoluit(), _huidgeDatum.Year); }
        }

        public ObservableCollection<VergoedingViewModel> Vergoedingen { get; set; }

        public CommandViewModel VolgendeMaand
        {
            get
            {
                if (_volgendeMaand == null)
                {
                    _volgendeMaand = new CommandViewModel(">", new RelayCommand(param => VolgendeMaandCommand()));
                }
                return _volgendeMaand;
            }
        }

        public CommandViewModel VorigeMaand
        {
            get
            {
                if (_vorigeMaand == null)
                {
                    _vorigeMaand = new CommandViewModel("<", new RelayCommand(param => VorigeMaandCommand()));
                }
                return _vorigeMaand;
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => Verwijderen())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private void CreateVergoedingenCollection()
        {
            var datumVan = new DateTime(_huidgeDatum.Year, _huidgeDatum.Month, 1);
            var datumTot = new DateTime(
                _huidgeDatum.AddMonths(1)
                    .Year,
                _huidgeDatum.AddMonths(1)
                    .Month,
                1);

            using (var repo = new VergoedingRepository())
            {
                List<VergoedingViewModel> all =
                    (from cust in
                        repo.Load(
                            x =>
                                x.BeheerderId == _beheerder.Id && x.Datum >= datumVan && x.Datum < datumTot
                                    && x.Verwijderd == false)
                        select new VergoedingViewModel(cust)).ToList();

                if (Vergoedingen != null)
                {
                    Vergoedingen.Clear();
                }

                Vergoedingen = new ObservableCollection<VergoedingViewModel>(all);
            }
            OnPropertyChanged("Vergoedingen");
            OnPropertyChanged("HuidigeMaand");
        }

        private void Verwijderen()
        {
            var vergoedingViewModel = Vergoedingen.Where(x => x.IsSelected)
                .FirstOrDefault();
            if (vergoedingViewModel != null)
            {
                if (Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionVergoedingVerwijderen))
                {
                    using (var repo = new VergoedingRepository())
                    {
                        repo.Delete(vergoedingViewModel.GetEntity());
                    }
                    Vergoedingen.Remove(vergoedingViewModel);
                    OnPropertyChanged("Vergoedingen");
                }
            }
        }

        private void VolgendeMaandCommand()
        {
            _huidgeDatum = _huidgeDatum.AddMonths(1);
            CreateVergoedingenCollection();
        }

        private void VorigeMaandCommand()
        {
            _huidgeDatum = _huidgeDatum.AddMonths(-1);
            CreateVergoedingenCollection();
        }
    }
}