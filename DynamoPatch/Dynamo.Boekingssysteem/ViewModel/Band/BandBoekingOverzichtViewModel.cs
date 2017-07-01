using System.Collections.Generic;
using System.Collections.ObjectModel;

using Dynamo.BL;
using Dynamo.BL.ResultMessages;
using Dynamo.Boekingssysteem.ViewModel.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandBoekingOverzichtViewModel : SubItemViewModel<Model.Band>
    {
        public BandBoekingOverzichtViewModel(Model.Band band)
        {
            using (var bandRepository = new PlanningRepository())
            {
                List<BandBoekingOverzichtMessage> all =
                    bandRepository.GetBandBoekingOverzicht(band);

                AlleBoekingen = new ObservableCollection<BandBoekingOverzichtMessage>(all);
            }
        }

        public ObservableCollection<BandBoekingOverzichtMessage> AlleBoekingen { get; private set; }
    }
}