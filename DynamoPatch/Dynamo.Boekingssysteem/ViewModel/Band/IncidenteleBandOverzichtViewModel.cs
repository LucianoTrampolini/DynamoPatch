using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Constants;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class IncidenteleBandOverzichtViewModel : WorkspaceViewModel
    {
        public ObservableCollection<BandViewModel> AlleBands { get; private set; }
        
        public IncidenteleBandOverzichtViewModel()
        {
            this.DisplayName = StringResources.ButtonIncidenteleBands;
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
            using (var repo = new BandRepository())
            {
                //TODO expressie moet simpeler
                List<BandViewModel> all =
                    (from band in repo.Load(x => x.BandTypeId == BandTypeConsts.Incidenteel && x.Verwijderd == false)
                     select new BandViewModel(band)).OrderBy(x => x.Naam).ToList();
                all.ForEach(b => b.DoubleClicked += BandDoubleClicked);
                this.AlleBands = new ObservableCollection<BandViewModel>(all);
            }

            if (selectedIId > 0)
            {
                var selected = AlleBands.LastOrDefault(x => x.Id == selectedIId);
                if (selected != null)
                {
                    selected.IsSelected = true;
                }
            }
            OnPropertyChanged("AlleBands");
        }

        private void BandDoubleClicked(object sender, EventArgs e)
        {
            EditBand();
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => this.Verwijderen(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => this.EditBand(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private void EditBand()
        {
            SwitchViewModel(new EditIncidenteleBandViewModel(AlleBands.LastOrDefault(x => x.IsSelected).GetEntity()));
        }

        private void Verwijderen()
        {
            var bandViewModel = AlleBands.Where(x => x.IsSelected).FirstOrDefault();
            if (bandViewModel != null)
            {
                if(Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionBandVerwijderen))
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

        private bool IetsGeselecteerd()
        {
            return this.AlleBands.Count(x => x.IsSelected) > 0;
        }
    }
}
