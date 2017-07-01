using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using System.Collections.ObjectModel;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem;
using Dynamo.BL;
using System.Windows;
using Dynamo.Common.Constants;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandOverzichtViewModel : WorkspaceViewModel
    {
        public ObservableCollection<BandViewModel> AlleBands { get; private set; }

        public BandOverzichtViewModel()
        {
            this.DisplayName = StringResources.ButtonBands;
            RefreshBands();
        }

        private void RefreshBands()
        {
            var selectedIId = 0;
            if (AlleBands != null)
            {
                selectedIId = AlleBands.FirstOrDefault(x => x.IsSelected).Id;
                AlleBands.ToList().ForEach(b => b.DoubleClicked -= BandDoubleClicked);
            }
            using (var bandRepository = new BandRepository())
            {
                //TODO expressie moet simpeler en 
                List<BandViewModel> all =
                    (from band in bandRepository.Load(
                         x =>
                            x.BandTypeId == BandTypeConsts.Contract &&
                            x.Verwijderd == false &&
                            (x.Contracten.Count(c => c.EindeContract.HasValue == false
                                || c.EindeContract.Value >= DateTime.Now) > 0)
                            || x.Betalingen.Where(bt => bt.Verwijderd == false).Sum(b => b.Bedrag) > 0
                            ).OrderBy(b => b.Naam)
                     select new BandViewModel(band)).ToList();

                all.ForEach(b => b.DoubleClicked += BandDoubleClicked);
                this.AlleBands = new ObservableCollection<BandViewModel>(all);
            }
            if (selectedIId > 0)
            {
                var selected = AlleBands.FirstOrDefault(x => x.Id == selectedIId);
                if (selected != null)
                {
                    selected.IsSelected = true;
                }
            }
            OnPropertyChanged("AlleBands");
        }

        private void BandDoubleClicked(object sender, EventArgs e)
        {
            Betalen();
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonBetalen,
                    new RelayCommand(param => this.Betalen(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => this.Verwijderen(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => this.EditBand(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => this.NewBand())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private void NewBand()
        {
            SwitchViewModel(new EditBandViewModel());
        }

        private void EditBand()
        {
            SwitchViewModel(new EditBandViewModel(AlleBands.FirstOrDefault(x => x.IsSelected).GetEntity()));
        }

        private void Verwijderen()
        {
            var bandViewModel = AlleBands.Where(x => x.IsSelected).FirstOrDefault();
            if (bandViewModel != null)
            {
                if (Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionBandVerwijderen))
                {
                    using (var repo = new BandRepository())
                    {
                        repo.Delete(bandViewModel.GetEntity());
                    }
                    RefreshBands();
                }
            }
        }

        protected override void OnSubViewModelClosed()
        {
            RefreshBands();
        }

        public override void OnDefaultCommand()
        {
            EditBand();
        }

        private void Betalen()
        {
            SwitchViewModel(new BandBetalenViewModel(AlleBands.FirstOrDefault(x => x.IsSelected)));
        }

        private bool IetsGeselecteerd()
        {
            return this.AlleBands.Count(x => x.IsSelected) > 0;
        }
    }
}
