using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.BL;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    public class InstellingenViewModel : WorkspaceViewModel
    {
        public Model.Instelling _instelling;

        public int WekenIncidenteleBandsBewaren
        {
            get { return _instelling.WekenIncidenteleBandsBewaren; }
            set
            {
                if (value == _instelling.WekenIncidenteleBandsBewaren)
                    return;

                _instelling.WekenIncidenteleBandsBewaren = value;

                base.OnPropertyChanged("WekenIncidenteleBandsBewaren");
            }
        }

        public int WekenVooruitBoeken
        {
            get { return _instelling.WekenVooruitBoeken; }
            set
            {
                if (value == _instelling.WekenVooruitBoeken)
                    return;

                _instelling.WekenVooruitBoeken = value;

                base.OnPropertyChanged("WekenVooruitBoeken");
            }
        }

        public decimal VergoedingBeheerder
        {
            get
            {
                return _instelling.VergoedingBeheerder;
            }
            set
            {
                if (_instelling.VergoedingBeheerder == value)
                    return;

                _instelling.VergoedingBeheerder = value;

                OnPropertyChanged("VergoedingBeheerder");
            }
        }

        public decimal BedragBandWaarschuwing
        {
            get
            {
                return _instelling.BedragBandWaarschuwing;
            }
            set
            {
                if (_instelling.BedragBandWaarschuwing == value)
                    return;

                _instelling.BedragBandWaarschuwing = value;

                OnPropertyChanged("BedragBandWaarschuwing");
            }
        }

        public InstellingenViewModel()
        {
            DisplayName = StringResources.ButtonSysteemInstellingen;
            using (var repo = new InstellingRepository())
            {
                _instelling = repo.Load(0);
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
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private void Opslaan()
        {
            using (var repo = new InstellingRepository())
            {
                repo.Save(_instelling);
            }
            CloseCommand.Execute(null);
        }
    }
}
