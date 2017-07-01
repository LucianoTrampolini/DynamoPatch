using System;
using System.Collections.Generic;

using Dynamo.BL.Repository;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class NieuweBetalingViewModel : BetalingViewModel
    {
        #region Member fields

        private readonly BandViewModel _band;

        #endregion

        public NieuweBetalingViewModel(BandViewModel band)
        {
            _band = band;
            _entity.Datum = DateTime.Today;

            _entity.BandId = _band.Id;
            Omschrijving = "Betaald";
        }

        public NieuweBetalingViewModel(BandViewModel band, BetalingViewModel betaling)
            : base(betaling.GetEntity())
        {
            _band = band;
            _entity.Datum = DateTime.Today;
            Omschrijving = "Betaald";
        }

        public decimal Betaald
        {
            get { return _entity.Bedrag * -1; }
            set
            {
                if (_entity.Bedrag == value * -1)
                    return;

                _entity.Bedrag = value * -1;

                OnPropertyChanged("Betaald");
            }
        }

        public string Omschrijving { get; protected set; }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => Betalen(), param => KanBetalen())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private void Betalen()
        {
            using (var repo = new BetalingRepository())
            {
                repo.Save(_entity);
            }
            CloseCommand.Execute(null);
        }

        private bool KanBetalen()
        {
            return Bedrag != 0;
        }
    }
}