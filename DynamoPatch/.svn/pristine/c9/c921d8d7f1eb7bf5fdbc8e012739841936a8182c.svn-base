using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;
using System.Collections.ObjectModel;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class BandAfmeldenViewModel : EntityViewModel<Model.Planning>
    {
        private string _opmerkingen = "Afgebeld. ";

        private Model.Boeking CurrentBoeking
        {
            get 
            {
                var boeking = _entity.Boekingen.OrderByDescending(x => x.Id).FirstOrDefault();
                if (boeking == null)
                {
                    throw new Exception("Boeking is corrupt!");
                }
                return boeking;
            }
        }

        public string Opmerkingen
        {
            get { return _opmerkingen; }
            set 
            {
                if (_opmerkingen == value)
                {
                    return;
                }
                _opmerkingen = value;
                OnPropertyChanged("Opmerkingen");
            }
        
        }
        
        public BandAfmeldenViewModel(Model.Planning planning)
            : base(planning)
        {
            DisplayName = string.Format("{0} afmelden?", CurrentBoeking.BandNaam);
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => this.Bandafmelden())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private void Bandafmelden()
        {
            CurrentBoeking.DatumAfgezegd = DateTime.Now;
            CurrentBoeking.Opmerking += Opmerkingen;
            _entity.Beschikbaar = true;
            using (var repo = new PlanningRepository())
            {
                repo.Save(_entity);
            }

            new WebIntegrationHelper().PushPlanning(_entity.Datum);

            CloseCommand.Execute(null);
        }
    }
}
