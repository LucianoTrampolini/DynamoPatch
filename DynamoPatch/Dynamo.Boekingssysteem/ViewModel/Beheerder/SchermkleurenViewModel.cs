using System.Collections.Generic;
using System.Windows.Media;
using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class SchermkleurenViewModel:WorkspaceViewModel
    {
        private Model.Beheerder _currentBeheerder;

        public SchermkleurenViewModel()
        {
            DisplayName = StringResources.ButtonSchermkleuren;
            _currentBeheerder = Helper.CurrentBeheerder;
        }

        public Color Achtergrond
        { 
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurAchtergrond);
            }
            set
            {
                _currentBeheerder.KleurAchtergrond = Helper.ToOle(value);
            }
        }
        public Color Tekst
        {
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurTekst);
            }
            set
            {
                _currentBeheerder.KleurTekst = Helper.ToOle(value);
            }
        }
        public Color AchtergrondVelden
        {
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurAchtergrondVelden);
            }
            set
            {
                _currentBeheerder.KleurAchtergrondVelden = Helper.ToOle(value);
            }
        }
        public Color TekstVelden
        {
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurTekstVelden);
            }
            set
            {
                _currentBeheerder.KleurTekstVelden = Helper.ToOle(value);
            }
        }
        public Color Selecteren
        {
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurSelecteren);
            }
            set
            {
                _currentBeheerder.KleurSelecteren = Helper.ToOle(value);
            }
        }
        public Color AchtergrondKnoppen
        {
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurKnoppen);
            }
            set
            {
                _currentBeheerder.KleurKnoppen = Helper.ToOle(value);
            }
        }
        public Color TekstKnoppen
        {
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurTekstKnoppen);
            }
            set
            {
                _currentBeheerder.KleurTekstKnoppen = Helper.ToOle(value);
            }
        }
        public Color Knoppen
        {
            get
            {
                return Helper.GetColorFromInt(_currentBeheerder.KleurKnoppen);
            }
            set
            {
                _currentBeheerder.KleurKnoppen = Helper.ToOle(value);
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOpslaan,
                    new RelayCommand(param => this.Opslaan())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private void Opslaan()
        {
            using (var repo = new BeheerderRepository())
            {
                repo.Save(_currentBeheerder);
                Helper.CurrentBeheerder = repo.CurrentBeheerder;
            }
            CloseCommand.Execute(null);
        }
    }
}
