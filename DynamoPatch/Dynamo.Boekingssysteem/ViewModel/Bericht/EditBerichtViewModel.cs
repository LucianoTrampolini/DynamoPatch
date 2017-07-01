using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.Boekingssysteem.ViewModel.Beheerder;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Constants;
using Dynamo.Common.Properties;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Bericht
{
    public class EditBerichtViewModel : EditMemoViewModel
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
                Beheerders = new ObservableCollection<BeheerderViewModel>(all);
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

        public ObservableCollection<BeheerderViewModel> Beheerders { get; }

        public override bool GeadresseerdenVisible
        {
            get
            {
                return false; // _entity.IsTransient(); 
            }
        }

        public override bool IsReadOnly
        {
            get { return !_entity.IsTransient(); }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOpslaan,
                    new RelayCommand(param => Opslaan(), param => KanOpslaan())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private bool KanOpslaan()
        {
            return string.IsNullOrWhiteSpace(Tekst) == false && string.IsNullOrWhiteSpace(Titel) == false
                && Beheerders != null; // && Beheerders.Count(b => b.IsSelected) > 0;
        }

        private void Opslaan()
        {
            if (KanOpslaan())
            {
                using (var repo = new BerichtRepository())
                {
                    foreach (var item in Beheerders) //.Where(b=>b.IsSelected))
                    {
                        GetEntity()
                            .BeheerderBerichten.Add(
                                new BeheerderBericht
                                {
                                    BeheerderId = item.Id
                                });
                    }

                    repo.Save(GetEntity());
                }
                CloseCommand.Execute(null);
            }
        }
    }
}