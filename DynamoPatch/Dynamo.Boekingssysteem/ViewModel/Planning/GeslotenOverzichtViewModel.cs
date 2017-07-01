using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class GeslotenOverzichtViewModel : WorkspaceViewModel
    {
        public ObservableCollection<GeslotenViewModel> AlleGesloten { get; private set; }
        public GeslotenOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonGeslotenDagen;
            RefreshGesloten();
        }

        private void RefreshGesloten()
        {
            var selectedIId = 0;
            if (AlleGesloten != null)
            {
                selectedIId = AlleGesloten.FirstOrDefault(x => x.IsSelected).Id;
            }
            var datumVanaf = DateTime.Today.AddDays(-28);
            using (var repo = new GeslotenRepository())
            {
                List<GeslotenViewModel> all =
                            (from cust in repo.Load(g => g.Verwijderd == false)// && (g.DatumTot.HasValue==false || g.DatumTot.Value > datumVanaf))
                             select new GeslotenViewModel(cust)).ToList();
                this.AlleGesloten = new ObservableCollection<GeslotenViewModel>(all);
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

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => this.Verwijderen(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => this.EditGesloten(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => this.NewGesloten())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private bool IetsGeselecteerd()
        {
            return this.AlleGesloten.Count(x => x.IsSelected) > 0;
        }

        private void NewGesloten()
        {
            SwitchViewModel(new EditGeslotenViewModel());
        }

        private void EditGesloten()
        {
            SwitchViewModel(new EditGeslotenViewModel(AlleGesloten.FirstOrDefault(x=>x.IsSelected).GetEntity()));
        }

        private void Verwijderen()
        {
            var geslotenViewModel = AlleGesloten.Where(x => x.IsSelected).FirstOrDefault();
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

        protected override void OnSubViewModelClosed()
        {
            RefreshGesloten();
        }
    }
}
