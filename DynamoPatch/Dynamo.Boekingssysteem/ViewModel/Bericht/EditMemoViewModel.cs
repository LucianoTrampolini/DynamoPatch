using System;
using System.Collections.Generic;

using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Constants;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Bericht
{
    public class EditMemoViewModel : BerichtViewModel
    {
        public EditMemoViewModel()
            : base(new Model.Bericht())
        {
            _entity.BerichtTypeId = BerichtTypeConsts.Memo;
            _entity.Datum = DateTime.Today;
        }

        public EditMemoViewModel(Model.Bericht memo)
            : base(memo) {}

        public virtual bool GeadresseerdenVisible
        {
            get { return false; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
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
            return string.IsNullOrWhiteSpace(Tekst) == false && string.IsNullOrWhiteSpace(Titel) == false;
        }

        private void Opslaan()
        {
            if (KanOpslaan())
            {
                using (var repo = new BerichtRepository())
                {
                    repo.Save(GetEntity());
                }
                CloseCommand.Execute(null);
            }
        }
    }
}