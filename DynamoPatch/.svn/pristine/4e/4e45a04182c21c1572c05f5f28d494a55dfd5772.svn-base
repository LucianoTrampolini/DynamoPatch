using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.Common.Constants;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class BeheerderOverzichtViewModel : WorkspaceViewModel
    {
        private Model.Beheerder _currentBeheerder=null;
        private bool _showInactieveBeheerders = false;

        public ObservableCollection<BeheerderViewModel> AlleBeheerders { get; private set; }

        public bool ShowInactieveBeheerders
        {
            get
            {
                return _showInactieveBeheerders;    
            }
            set 
            {
                _showInactieveBeheerders = value;
                RefreshBeheerders();
            }
        }

        public BeheerderOverzichtViewModel()
        {
            this.DisplayName = StringResources.ButtonBeheerders;
            RefreshBeheerders();            
        }

        private void RefreshBeheerders()
        {
            var selectedIId = 0;
            if (AlleBeheerders != null)
            {
                selectedIId = AlleBeheerders.FirstOrDefault(x => x.IsSelected).Id;
                AlleBeheerders.ToList().ForEach(b => b.DoubleClicked -= BeheerderDoubleClicked);
            }
            using (var repo = new BeheerderRepository())
            {
                List<BeheerderViewModel> all;
                if (_showInactieveBeheerders)
                {
                    all = (from cust in repo.Load(b => b.Id > BeheerderConsts.AdminBeheerder) select new BeheerderViewModel(cust)).ToList();
                }
                else
                { 
                    all = (from cust in repo.Load() select new BeheerderViewModel(cust)).ToList();
                }
                
                all.ForEach(b => b.DoubleClicked += BeheerderDoubleClicked);
                this.AlleBeheerders = new ObservableCollection<BeheerderViewModel>(all);
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

        private void BeheerderDoubleClicked(object sender, EventArgs e)
        {
            Aanmelden();
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonAanmelden,
                    new RelayCommand(param => this.Aanmelden(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonVergoedingen,
                    new RelayCommand(param => this.ShowVergoedingen(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => this.Verwijderen(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => this.EditBeheerder(), param=>this.IetsGeselecteerd())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => this.NewBeheerder())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private void Verwijderen()
        {
            var beheerderViewModel = AlleBeheerders.Where(x => x.IsSelected).FirstOrDefault();
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

        private void NewBeheerder()
        {
            SwitchViewModel(new EditBeheerderViewModel());
        }

        private void EditBeheerder()
        {
            SwitchViewModel(new EditBeheerderViewModel(this.AlleBeheerders.First(x => x.IsSelected).GetEntity()));
        }

        private void Aanmelden()
        {
            _currentBeheerder = Helper.CurrentBeheerder;
            SwitchViewModel(new AanmeldenViewModel(this.AlleBeheerders.First(x => x.IsSelected)));
        }

        private void ShowVergoedingen()
        {
            SwitchViewModel(new VergoedingOverzichtViewModel(this.AlleBeheerders.First(x => x.IsSelected)));
        }

        protected override void OnSubViewModelClosed()
        {
            if (Helper.CurrentBeheerder == _currentBeheerder || _currentBeheerder==null)
            {
                RefreshBeheerders();
            }
            else
            {
                CloseCommand.Execute(null);
            }
        }

        private bool IetsGeselecteerd()
        {
            return this.AlleBeheerders.Count(x => x.IsSelected) > 0;
        }
    }
}
