using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class EditIncidenteleBandViewModel : BandViewModel
    {
        /// <summary>
        /// Deze hide de telefoon van BandViewModel
        /// </summary>
        public new string Telefoon
        {
            get
            {
                return _entity.Telefoon;
            }
            set 
            { 
                _entity.Telefoon = value; 
            }
        }

        public string BsnNummer
        {
            get
            {
                return _entity.BSNNummer;
            }
            set
            {
                _entity.BSNNummer = value;
            }
        }

        private ViewModelBase _overzichtBoekingen;
        public ViewModelBase OverzichtBoekingen
        {
            get { return _overzichtBoekingen; }
        }

        public EditIncidenteleBandViewModel(Model.Band band)
            : base(band)
        {
            _overzichtBoekingen = new BandBoekingOverzichtViewModel(band);
            OnPropertyChanged("OverzichtBoekingen");
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOpslaan,
                    new RelayCommand(param => this.Opslaan(), param=>this.KanOpslaan())),
                new CommandViewModel(
                    StringResources.ButtonOntdubbelen,
                    new RelayCommand(param => this.Ontdubbelen())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    new RelayCommand(param => this.Annuleren()))
            };
        }
        
        private void Ontdubbelen()
        {
            SwitchViewModel(new OntdubbelViewModel(this));
        }

        private void Annuleren()
        {
            CloseCommand.Execute(null);
        }

        protected override void OnSubViewModelClosed()
        {
            if (_entity.Verwijderd == true)
            {
                CloseCommand.Execute(null);
            }
        }

        private bool KanOpslaan()
        {
            return !string.IsNullOrWhiteSpace(Naam) && (!HeeftVerleden || !string.IsNullOrWhiteSpace(Opmerkingen));
        }

        private void Opslaan()
        {
            if (KanOpslaan())
            {
                using (var repo = new BandRepository())
                {
                    repo.Save(_entity);
                }
                CloseCommand.Execute(null);
            }
        }
    }
}
