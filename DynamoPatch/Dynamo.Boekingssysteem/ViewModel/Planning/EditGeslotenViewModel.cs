using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem;
using Dynamo.BL;
using Dynamo.Common;
namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class EditGeslotenViewModel : GeslotenViewModel
    {
        public EditGeslotenViewModel()
            : base(new Model.Gesloten())
        { 
        }

        public EditGeslotenViewModel(Model.Gesloten gesloten)
            : base(gesloten)
        {
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
