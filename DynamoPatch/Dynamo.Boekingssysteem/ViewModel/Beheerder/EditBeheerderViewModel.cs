using System.Collections.Generic;
using System.Windows.Media;

using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class EditBeheerderViewModel : BeheerderViewModel
    {
        /// <summary>
        /// Als dit viewmodel zonder beheerder wordt opgegeven, dan maken we gewoon een nieuwe...
        /// </summary>
        public EditBeheerderViewModel()
            : base(new Model.Beheerder())
        {
            _entity.KleurAchtergrond = Helper.ToOle(Colors.Black);
            ;
            _entity.KleurAchtergrondVelden = Helper.ToOle(Colors.Black);
            _entity.KleurKnoppen = Helper.ToOle(Colors.Black);
            _entity.KleurTekst = Helper.ToOle(Colors.White);
            ;
            _entity.KleurTekstVelden = Helper.ToOle(Colors.White);
            _entity.KleurTekstKnoppen = Helper.ToOle(Colors.White);
            _entity.KleurSelecteren = Helper.ToOle(Colors.DarkGray);
        }

        public EditBeheerderViewModel(Model.Beheerder beheerder)
            : base(beheerder) {}

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
            return string.IsNullOrWhiteSpace(Naam) == false;
        }

        private void Opslaan()
        {
            if (KanOpslaan())
            {
                using (var repo = new BeheerderRepository())
                {
                    repo.Save(_entity);
                }
                CloseCommand.Execute(null);
            }
        }
    }
}