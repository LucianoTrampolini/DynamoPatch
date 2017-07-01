using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Boekingssysteem.ViewModel.Planning;
using System.Collections.ObjectModel;
using Dynamo.BL;
using Dynamo.Common.Constants;
using Dynamo.BL.ResultMessages;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandBoekingOverzichtViewModel :SubItemViewModel<Model.Band>
    {
        public ObservableCollection<BandBoekingOverzichtMessage> AlleBoekingen { get; private set; }

        public BandBoekingOverzichtViewModel(Model.Band band)
        {
            using (var bandRepository = new PlanningRepository())
            {
                List<BandBoekingOverzichtMessage> all =
                   bandRepository.GetBandBoekingOverzicht(band);

                AlleBoekingen = new ObservableCollection<BandBoekingOverzichtMessage>(all);
            }
        }
    }
}
