using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Constants;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class OntdubbelViewModel : SubItemViewModel<Model.Band>
    {
        #region Member fields

        private BandViewModel _bandNaar;
        private readonly BandViewModel _bandVan;

        #endregion

        public OntdubbelViewModel(BandViewModel band)
        {
            _bandVan = band;

            using (var bandRepository = new BandRepository())
            {
                List<BandViewModel> all =
                    (from bands in
                        bandRepository.Load(
                            x =>
                                x.Id != _bandVan.Id && x.BandTypeId == BandTypeConsts.Incidenteel
                                    && x.Verwijderd == false)
                        select new BandViewModel(bands)).ToList();
                AlleBands = new ObservableCollection<BandViewModel>(all);
            }
        }

        public ObservableCollection<BandViewModel> AlleBands { get; private set; }

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

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonOk,
                    new RelayCommand(param => Ontdubbelen(), param => KanOntdubbelen())),
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
                var boekingen = bandRepository.GetBoekingen(_bandVan.GetEntity())
                    .ToList();
                foreach (var boeking in boekingen)
                {
                    boeking.BandNaam = BandNaar.Naam;
                    boeking.BandId = BandNaar.Id;
                    bandRepository.SaveBoeking(boeking);
                }

                _bandVan.GetEntity()
                    .Verwijderd = true;
                bandRepository.Save(_bandVan.GetEntity());
            }
            CloseCommand.Execute(null);
        }
    }
}