using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Common.Constants;
using System.Collections.ObjectModel;
using Dynamo.Boekingssysteem.ViewModel.Beheerder;
using Dynamo.BL;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem;

namespace Dynamo.Boekingssysteem.ViewModel.Bericht
{
    public class EditBerichtViewModel: EditMemoViewModel
    {
        public EditBerichtViewModel()
            : base(new Model.Bericht())
        {
            _entity.BerichtTypeId = BerichtTypeConsts.Intern;
            _entity.Datum = DateTime.Now;
            using (var repo = new BeheerderRepository())
            {
                List<BeheerderViewModel> all =
                                (from cust in repo.Load()
                                 select new BeheerderViewModel(cust)).ToList();
                this.Beheerders = new ObservableCollection<BeheerderViewModel>(all);
            }
        }

        public ObservableCollection<BeheerderViewModel> Beheerders { get; private set; }

        public override bool GeadresseerdenVisible 
        { 
            get 
            {
                return false;// _entity.IsTransient(); 
            } 
        }

        public override bool IsReadOnly
        {
            get
            {
                return !_entity.IsTransient();
            }
        }
        public EditBerichtViewModel(Model.Bericht memo)
            : base(memo)
        {
            using (var repo = new BerichtRepository())
            {
                repo.BerichtGelezen(memo, Helper.CurrentBeheerder);
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOpslaan,
                    new RelayCommand(param => this.Opslaan(), param=>this.KanOpslaan())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private void Opslaan()
        {
            if (KanOpslaan())
            {
                using (var repo = new BerichtRepository())
                {
                    foreach (var item in Beheerders)//.Where(b=>b.IsSelected))
	                {
                        GetEntity().BeheerderBerichten.Add(new Model.BeheerderBericht { BeheerderId = item.Id });
	                }

                    repo.Save(GetEntity());
                }
                CloseCommand.Execute(null);
            }
        }

        private bool KanOpslaan()
        {
            return string.IsNullOrWhiteSpace(Tekst) == false && string.IsNullOrWhiteSpace(Titel) == false && Beheerders!=null;// && Beheerders.Count(b => b.IsSelected) > 0;
        }
    }
}
