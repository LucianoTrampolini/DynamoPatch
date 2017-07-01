using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dynamo.BL;
using Dynamo.BL.Repository;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class VergoedingOverzichtViewModel : SubItemViewModel<Model.Vergoeding>
    {
        private BeheerderViewModel _beheerder;
        private CommandViewModel _vorigeMaand;
        private CommandViewModel _volgendeMaand;

        private DateTime _huidgeDatum = DateTime.Today;

        public ObservableCollection<VergoedingViewModel> Vergoedingen { get; set; }

        public string HuidigeMaand
        {
            get { return string.Format("{0} {1}", _huidgeDatum.MaandVoluit(), _huidgeDatum.Year); }
        }

        public CommandViewModel VorigeMaand
        {
            get
            {
                if (_vorigeMaand == null)
                {
                    _vorigeMaand = new CommandViewModel("<", new RelayCommand(param => this.VorigeMaandCommand()));
                }
                return _vorigeMaand;
            }
        }

        public CommandViewModel VolgendeMaand
        {
            get
            {
                if (_volgendeMaand == null)
                {
                    _volgendeMaand = new CommandViewModel(">", new RelayCommand(param => this.VolgendeMaandCommand()));
                }
                return _volgendeMaand;
            }
        }
        public VergoedingOverzichtViewModel(BeheerderViewModel beheerder)
        {
            if (beheerder == null)
            {
                throw new ArgumentNullException("beheerder");
            }

            _beheerder = beheerder;

            CreateVergoedingenCollection();

            this.DisplayName = string.Format("{0} {1}", StringResources.ButtonVergoedingen,_beheerder.Naam);
        }

        private void CreateVergoedingenCollection()
        {
            var datumVan = new DateTime(_huidgeDatum.Year, _huidgeDatum.Month, 1);
            var datumTot = new DateTime(_huidgeDatum.AddMonths(1).Year, _huidgeDatum.AddMonths(1).Month, 1);

            using (var repo = new VergoedingRepository())
            {

                List<VergoedingViewModel> all =
                       (from cust in repo.Load(x => x.BeheerderId == _beheerder.Id && x.Datum >= datumVan && x.Datum < datumTot && x.Verwijderd==false)
                        select new VergoedingViewModel(cust)).ToList();

                if (this.Vergoedingen != null)
                {
                    this.Vergoedingen.Clear();
                }

                this.Vergoedingen = new ObservableCollection<VergoedingViewModel>(all);
            }
            OnPropertyChanged("Vergoedingen");
            OnPropertyChanged("HuidigeMaand");
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => this.Verwijderen())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
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

        private void Verwijderen()
        {
            var vergoedingViewModel =Vergoedingen.Where(x => x.IsSelected).FirstOrDefault();
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
    }
}
