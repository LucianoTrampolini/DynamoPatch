using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Constants;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Bericht
{
    public class BerichtOverzichtViewModel : WorkspaceViewModel
    {
        public BerichtOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonBerichten;
            RefreshBerichten();
        }

        public ObservableCollection<BerichtViewModel> AlleBerichten { get; private set; }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => Verwijderen(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => EditMemo(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => NewMemo())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        protected override void OnSubViewModelClosed()
        {
            RefreshBerichten();
        }

        private void EditMemo()
        {
            SwitchViewModel(
                new EditBerichtViewModel(
                    AlleBerichten.FirstOrDefault(x => x.IsSelected)
                        .GetEntity()));
        }

        private bool IetsGeselecteerd()
        {
            return AlleBerichten.Count(x => x.IsSelected) > 0;
        }

        private void MemoDoubleClicked(object sender, EventArgs e)
        {
            EditMemo();
        }

        private void NewMemo()
        {
            SwitchViewModel(new EditBerichtViewModel());
        }

        private void RefreshBerichten()
        {
            var selectedIId = 0;
            if (AlleBerichten != null)
            {
                var geselecteerdBericht = AlleBerichten.FirstOrDefault(x => x.IsSelected);
                if (geselecteerdBericht != null)
                {
                    selectedIId = geselecteerdBericht.Id;
                    AlleBerichten.ToList()
                        .ForEach(b => b.DoubleClicked -= MemoDoubleClicked);
                }
            }
            using (var repo = new BerichtRepository())
            {
                List<BerichtViewModel> all =
                    (from bericht in repo.Load(
                        x => x.BerichtTypeId == BerichtTypeConsts.Intern &&
                            x.Verwijderd == false &&
                            x.BeheerderBerichten.Where(
                                b => b.Verwijderd == false)
                                .Select(b => b.BeheerderId)
                                .Contains(Helper.CurrentBeheerder.Id)
                        )
                        .OrderByDescending(b => b.Datum)
                        select new BerichtViewModel(bericht)).ToList();

                all.ForEach(m => m.DoubleClicked += MemoDoubleClicked);
                AlleBerichten = new ObservableCollection<BerichtViewModel>(all);
            }
            if (selectedIId > 0)
            {
                var selected = AlleBerichten.FirstOrDefault(x => x.Id == selectedIId);
                if (selected != null)
                {
                    selected.IsSelected = true;
                }
            }
            OnPropertyChanged("AlleBerichten");
        }

        private void Verwijderen()
        {
            var berichtViewModel = AlleBerichten.Where(x => x.IsSelected)
                .FirstOrDefault();
            if (berichtViewModel != null)
            {
                if (Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionBerichtVerwijderen))
                {
                    using (var repo = new BerichtRepository())
                    {
                        berichtViewModel.GetEntity()
                            .BeheerderBerichten.First(b => b.BeheerderId == Helper.CurrentBeheerder.Id)
                            .Verwijderd = true;
                        repo.Save(berichtViewModel.GetEntity());
                    }
                    RefreshBerichten();
                }
            }
        }
    }
}