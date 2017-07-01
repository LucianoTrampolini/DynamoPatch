using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Constants;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class BeheerderOverzichtViewModel : WorkspaceViewModel
    {
        #region Member fields

        private Model.Beheerder _currentBeheerder;
        private bool _showInactieveBeheerders;

        #endregion

        public BeheerderOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonBeheerders;
            RefreshBeheerders();
        }

        public ObservableCollection<BeheerderViewModel> AlleBeheerders { get; private set; }

        public bool ShowInactieveBeheerders
        {
            get { return _showInactieveBeheerders; }
            set
            {
                _showInactieveBeheerders = value;
                RefreshBeheerders();
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonAanmelden,
                    new RelayCommand(param => Aanmelden(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonVergoedingen,
                    new RelayCommand(param => ShowVergoedingen(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => Verwijderen(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => EditBeheerder(), param => IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => NewBeheerder())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        protected override void OnSubViewModelClosed()
        {
            if (Helper.CurrentBeheerder == _currentBeheerder
                || _currentBeheerder == null)
            {
                RefreshBeheerders();
            }
            else
            {
                CloseCommand.Execute(null);
            }
        }

        private void Aanmelden()
        {
            _currentBeheerder = Helper.CurrentBeheerder;
            SwitchViewModel(new AanmeldenViewModel(AlleBeheerders.First(x => x.IsSelected)));
        }

        private void BeheerderDoubleClicked(object sender, EventArgs e)
        {
            Aanmelden();
        }

        private void EditBeheerder()
        {
            SwitchViewModel(
                new EditBeheerderViewModel(
                    AlleBeheerders.First(x => x.IsSelected)
                        .GetEntity()));
        }

        private bool IetsGeselecteerd()
        {
            return AlleBeheerders.Count(x => x.IsSelected) > 0;
        }

        private void NewBeheerder()
        {
            SwitchViewModel(new EditBeheerderViewModel());
        }

        private void RefreshBeheerders()
        {
            var selectedIId = 0;
            if (AlleBeheerders != null)
            {
                selectedIId = AlleBeheerders.FirstOrDefault(x => x.IsSelected)
                    .Id;
                AlleBeheerders.ToList()
                    .ForEach(b => b.DoubleClicked -= BeheerderDoubleClicked);
            }
            using (var repo = new BeheerderRepository())
            {
                List<BeheerderViewModel> all;
                if (_showInactieveBeheerders)
                {
                    all =
                        (from cust in repo.Load(b => b.Id > BeheerderConsts.AdminBeheerder)
                            select new BeheerderViewModel(cust)).ToList();
                }
                else
                {
                    all = (from cust in repo.Load() select new BeheerderViewModel(cust)).ToList();
                }

                all.ForEach(b => b.DoubleClicked += BeheerderDoubleClicked);
                AlleBeheerders = new ObservableCollection<BeheerderViewModel>(all);
                if (selectedIId > 0)
                {
                    var selected = AlleBeheerders.FirstOrDefault(x => x.Id == selectedIId);
                    if (selected != null)
                    {
                        selected.IsSelected = true;
                    }
                }
            }
            OnPropertyChanged("AlleBeheerders");
        }

        private void ShowVergoedingen()
        {
            SwitchViewModel(new VergoedingOverzichtViewModel(AlleBeheerders.First(x => x.IsSelected)));
        }

        private void Verwijderen()
        {
            var beheerderViewModel = AlleBeheerders.Where(x => x.IsSelected)
                .FirstOrDefault();
            if (beheerderViewModel != null)
            {
                if (Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionBeheerderVerwijderen))
                {
                    using (var repo = new BeheerderRepository())
                    {
                        repo.Delete(beheerderViewModel.GetEntity());
                    }
                    RefreshBeheerders();
                }
            }
        }
    }
}