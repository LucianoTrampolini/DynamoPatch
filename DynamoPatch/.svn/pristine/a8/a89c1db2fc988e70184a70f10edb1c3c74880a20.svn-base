using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using System.Collections.ObjectModel;
using Dynamo.Common.Properties;
using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using System.Windows;
using Dynamo.Common.Constants;

namespace Dynamo.Boekingssysteem.ViewModel.Bericht
{
    public class MemoOverzichtViewModel : WorkspaceViewModel
    {
        public ObservableCollection<BerichtViewModel> AlleBerichten { get; private set; }
        public MemoOverzichtViewModel()
        {
            this.DisplayName = StringResources.ButtonMemos;
            RefreshMemos();
        }

        private void RefreshMemos()
        {
            var selectedIId = 0;
            if (AlleBerichten != null)
            {
                selectedIId = AlleBerichten.FirstOrDefault(x => x.IsSelected).Id;
                AlleBerichten.ToList().ForEach(b => b.DoubleClicked -= MemoDoubleClicked);
            }
            using (var repo = new BerichtRepository())
            {
                List<BerichtViewModel> all =
                    (from band in repo.Load(x => x.BerichtTypeId == BerichtTypeConsts.Memo && x.Verwijderd == false)
                     select new BerichtViewModel(band)).ToList();
                all.ForEach(m => m.DoubleClicked += MemoDoubleClicked);
                this.AlleBerichten = new ObservableCollection<BerichtViewModel>(all);
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

        private void MemoDoubleClicked(object sender, EventArgs e)
        {
            EditMemo();
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
                    new RelayCommand(param => this.EditMemo(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => this.NewMemo())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private void NewMemo()
        {
            SwitchViewModel(new EditMemoViewModel());
        }

        private void EditMemo()
        {
            SwitchViewModel(new EditMemoViewModel(AlleBerichten.FirstOrDefault(x => x.IsSelected).GetEntity()));
        }

        private bool IetsGeselecteerd()
        {
            return this.AlleBerichten.Count(x => x.IsSelected) > 0;
        }

        private void Verwijderen()
        {
            var berichtViewModel = AlleBerichten.Where(x => x.IsSelected).FirstOrDefault();
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

        protected override void OnSubViewModelClosed()
        {
            RefreshMemos();
        }
    }
}
