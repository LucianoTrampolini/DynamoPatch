using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BL;
using Dynamo.BL.Repository;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class NieuweBetalingViewModel:BetalingViewModel
    {
        private BandViewModel _band;

        public string Omschrijving { get; protected set; }

        public decimal Betaald
        {
            get
            {
                return _entity.Bedrag * -1;
            }
            set
            {
                if (_entity.Bedrag== value * -1)
                    return;

                _entity.Bedrag = value * -1;

                OnPropertyChanged("Betaald");
            }
        }

        public NieuweBetalingViewModel(BandViewModel band)
            :base()
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

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => this.Betalen(), param=>this.KanBetalen())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private bool KanBetalen()
        {
            return Bedrag != 0;
        }

        private void Betalen()
        {
            using (var repo = new BetalingRepository())
            {
                repo.Save(_entity);
            }
            CloseCommand.Execute(null);
        }
    }
}
