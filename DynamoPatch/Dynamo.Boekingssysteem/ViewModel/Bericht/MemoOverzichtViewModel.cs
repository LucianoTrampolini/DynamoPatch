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
    public class MemoOverzichtViewModel : WorkspaceViewModel
    {
        public MemoOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonMemos;
            RefreshMemos();
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
            RefreshMemos();
        }

        private void EditMemo()
        {
            SwitchViewModel(
                new EditMemoViewModel(
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
            SwitchViewModel(new EditMemoViewModel());
        }

        private void RefreshMemos()
        {
            var selectedIId = 0;
            if (AlleBerichten != null)
            {
                selectedIId = AlleBerichten.FirstOrDefault(x => x.IsSelected)
                    .Id;
                AlleBerichten.ToList()
                    .ForEach(b => b.DoubleClicked -= MemoDoubleClicked);
            }
            using (var repo = new BerichtRepository())
            {
                List<BerichtViewModel> all =
                    (from band in repo.Load(x => x.BerichtTypeId == BerichtTypeConsts.Memo && x.Verwijderd == false)
                        select new BerichtViewModel(band)).ToList();
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
                if (Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionMemoVerwijderen))
                {
                    using (var repo = new BerichtRepository())
                    {
                        repo.Delete(berichtViewModel.GetEntity());
                    }
                    RefreshMemos();
                }
            }
        }
    }
}