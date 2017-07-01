using System;
using System.Collections.Generic;
using System.Linq;

using Dynamo.BL;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class BandAfmeldenViewModel : EntityViewModel<Model.Planning>
    {
        #region Member fields

        private string _opmerkingen = "Afgebeld. ";

        #endregion

        public BandAfmeldenViewModel(Model.Planning planning)
            : base(planning)
        {
            DisplayName = string.Format("{0} afmelden?", CurrentBoeking.BandNaam);
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

        private Boeking CurrentBoeking
        {
            get
            {
                var boeking = _entity.Boekingen.OrderByDescending(x => x.Id)
                    .FirstOrDefault();
                if (boeking == null)
                {
                    throw new Exception("Boeking is corrupt!");
                }
                return boeking;
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => Bandafmelden())),
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