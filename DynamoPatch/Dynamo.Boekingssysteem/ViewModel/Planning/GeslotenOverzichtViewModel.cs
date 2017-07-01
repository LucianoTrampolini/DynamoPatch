using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL.Repository;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class GeslotenOverzichtViewModel : WorkspaceViewModel
    {
        public GeslotenOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonGeslotenDagen;
            RefreshGesloten();
        }

        public ObservableCollection<GeslotenViewModel> AlleGesloten { get; private set; }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => Verwijderen(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => EditGesloten(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => NewGesloten())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        protected override void OnSubViewModelClosed()
        {
            RefreshGesloten();
        }

        private void EditGesloten()
        {
            SwitchViewModel(
                new EditGeslotenViewModel(
                    AlleGesloten.FirstOrDefault(x => x.IsSelected)
                        .GetEntity()));
        }

        private bool IetsGeselecteerd()
        {
            return AlleGesloten.Count(x => x.IsSelected) > 0;
        }

        private void NewGesloten()
        {
            SwitchViewModel(new EditGeslotenViewModel());
        }

        private void RefreshGesloten()
        {
            var selectedIId = 0;
            if (AlleGesloten != null)
            {
                selectedIId = AlleGesloten.FirstOrDefault(x => x.IsSelected)
                    .Id;
            }
            var datumVanaf = DateTime.Today.AddDays(-28);
            using (var repo = new GeslotenRepository())
            {
                List<GeslotenViewModel> all =
                    (from cust in repo.Load(g => g.Verwijderd == false)
                        // && (g.DatumTot.HasValue==false || g.DatumTot.Value > datumVanaf))
                        select new GeslotenViewModel(cust)).ToList();
                AlleGesloten = new ObservableCollection<GeslotenViewModel>(all);
                if (selectedIId > 0)
                {
                    var selected = AlleGesloten.FirstOrDefault(x => x.Id == selectedIId);
                    if (selected != null)
                    {
                        selected.IsSelected = true;
                    }
                }
            }
            OnPropertyChanged("AlleGesloten");
        }

        private void Verwijderen()
        {
            var geslotenViewModel = AlleGesloten.Where(x => x.IsSelected)
                .FirstOrDefault();
            if (geslotenViewModel != null)
            {
                if (Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionGeslotenVerwijderen))
                {
                    using (var repo = new GeslotenRepository())
                    {
                        repo.Delete(geslotenViewModel.GetEntity());
                    }
                    RefreshGesloten();
                }
            }
        }
    }
}