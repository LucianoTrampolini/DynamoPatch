using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Constants;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandOverzichtViewModel : WorkspaceViewModel
    {
        public BandOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonBands;
            RefreshBands();
        }

        public ObservableCollection<BandViewModel> AlleBands { get; private set; }

        public override void OnDefaultCommand()
        {
            EditBand();
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonBetalen,
                    new RelayCommand(param => Betalen(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => Verwijderen(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => EditBand(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => NewBand())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        protected override void OnSubViewModelClosed()
        {
            RefreshBands();
        }

        private void BandDoubleClicked(object sender, EventArgs e)
        {
            Betalen();
        }

        private void Betalen()
        {
            SwitchViewModel(new BandBetalenViewModel(AlleBands.FirstOrDefault(x => x.IsSelected)));
        }

        private void EditBand()
        {
            SwitchViewModel(
                new EditBandViewModel(
                    AlleBands.FirstOrDefault(x => x.IsSelected)
                        .GetEntity()));
        }

        private bool IetsGeselecteerd()
        {
            return AlleBands.Count(x => x.IsSelected) > 0;
        }

        private void NewBand()
        {
            SwitchViewModel(new EditBandViewModel());
        }

        private void RefreshBands()
        {
            var selectedIId = 0;
            if (AlleBands != null)
            {
                selectedIId = AlleBands.FirstOrDefault(x => x.IsSelected)
                    .Id;
                AlleBands.ToList()
                    .ForEach(b => b.DoubleClicked -= BandDoubleClicked);
            }
            using (var bandRepository = new BandRepository())
            {
                //TODO expressie moet simpeler en 
                List<BandViewModel> all =
                    (from band in bandRepository.Load(
                        x =>
                            x.BandTypeId == BandTypeConsts.Contract &&
                                x.Verwijderd == false &&
                                (x.Contracten.Count(
                                    c => c.EindeContract.HasValue == false
                                        || c.EindeContract.Value >= DateTime.Now) > 0)
                                || x.Betalingen.Where(bt => bt.Verwijderd == false)
                                    .Sum(b => b.Bedrag) > 0
                        )
                        .OrderBy(b => b.Naam)
                        select new BandViewModel(band)).ToList();

                all.ForEach(b => b.DoubleClicked += BandDoubleClicked);
                AlleBands = new ObservableCollection<BandViewModel>(all);
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

        private void Verwijderen()
        {
            var bandViewModel = AlleBands.Where(x => x.IsSelected)
                .FirstOrDefault();
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
    }
}