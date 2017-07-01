using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.BoekingsSysteem;
using System.Collections.ObjectModel;
using Dynamo.BL;
using Dynamo.Common.Constants;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class OntdubbelViewModel:SubItemViewModel<Model.Band>
    {
        private BandViewModel _bandVan = null;
        private BandViewModel _bandNaar = null;
        public BandViewModel BandNaar 
        {
            get { return _bandNaar; }
            set 
            {
                if (value == _bandNaar)
                {
                    return;
                }

                _bandNaar = value;
                OnPropertyChanged("BandNaar");
            }
        }

        public ObservableCollection<BandViewModel> AlleBands { get; private set; }

        public OntdubbelViewModel(BandViewModel band)
        {
            _bandVan = band;

            using (var bandRepository = new BandRepository())
            {
                List<BandViewModel> all =
                    (from bands in bandRepository.Load(x => x.Id != _bandVan.Id && x.BandTypeId == BandTypeConsts.Incidenteel && x.Verwijderd == false)
                     select new BandViewModel(bands)).ToList();
                this.AlleBands = new ObservableCollection<BandViewModel>(all);
            }
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => this.Ontdubbelen(), param=>this.KanOntdubbelen())),
                new CommandViewModel(
                    StringResources.ButtonAnnuleren,
                    CloseCommand)
            };
        }

        private bool KanOntdubbelen()
        {
            return BandNaar != null;
        }

        private void Ontdubbelen()
        {
            using (var bandRepository = new BandRepository())
            {
                var boekingen = bandRepository.GetBoekingen(_bandVan.GetEntity()).ToList();
                foreach (var boeking in boekingen)
                {
                    boeking.BandNaam = BandNaar.Naam;
                    boeking.BandId = BandNaar.Id;
                    bandRepository.SaveBoeking(boeking);
                }

                _bandVan.GetEntity().Verwijderd = true;
                bandRepository.Save(_bandVan.GetEntity());
            }
            CloseCommand.Execute(null);
        }
    }
}
