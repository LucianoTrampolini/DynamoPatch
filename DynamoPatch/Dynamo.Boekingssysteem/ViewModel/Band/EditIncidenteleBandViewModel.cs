using System.Collections.Generic;

using Dynamo.BL;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class EditIncidenteleBandViewModel : BandViewModel
    {
        #region Member fields

        private readonly ViewModelBase _overzichtBoekingen;

        #endregion

        public EditIncidenteleBandViewModel(Model.Band band)
            : base(band)
        {
            _overzichtBoekingen = new BandBoekingOverzichtViewModel(band);
            OnPropertyChanged("OverzichtBoekingen");
        }

        public string BsnNummer
        {
            get { return _entity.BSNNummer; }
            set { _entity.BSNNummer = value; }
        }

        public ViewModelBase OverzichtBoekingen
        {
            get { return _overzichtBoekingen; }
        }

        /// <summary>
        /// Deze hide de telefoon van BandViewModel
        /// </summary>
        public new string Telefoon
        {
            get { return _entity.Telefoon; }
            set { _entity.Telefoon = value; }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOpslaan,
                    new RelayCommand(param => Opslaan(), param => KanOpslaan())),
                new CommandViewModel(
                    StringResources.ButtonOntdubbelen,
                    new RelayCommand(param => Ontdubbelen())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    new RelayCommand(param => Annuleren()))
            };
        }

        protected override void OnSubViewModelClosed()
        {
            if (_entity.Verwijderd)
            {
                CloseCommand.Execute(null);
            }
        }

        private void Annuleren()
        {
            CloseCommand.Execute(null);
        }

        private bool KanOpslaan()
        {
            return !string.IsNullOrWhiteSpace(Naam) && (!HeeftVerleden || !string.IsNullOrWhiteSpace(Opmerkingen));
        }

        private void Ontdubbelen()
        {
            SwitchViewModel(new OntdubbelViewModel(this));
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