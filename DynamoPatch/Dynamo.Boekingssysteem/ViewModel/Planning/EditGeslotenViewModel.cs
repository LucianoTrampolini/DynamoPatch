using System.Collections.Generic;

using Dynamo.BL.Repository;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class EditGeslotenViewModel : GeslotenViewModel
    {
        public EditGeslotenViewModel()
            : base(new Gesloten()) {}

        public EditGeslotenViewModel(Gesloten gesloten)
            : base(gesloten) {}

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
            return (DatumVan != string.Empty && Reden != string.Empty);
        }

        private void Opslaan()
        {
            using (var repo = new GeslotenRepository())
            {
                repo.Save(_entity);
            }
            CloseCommand.Execute(null);
        }
    }
}