using System;

using Dynamo.Model.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Base
{
    public class StamgegevenViewModel : EntityViewModel<StamgegevenBase>
    {
        public StamgegevenViewModel(StamgegevenBase stamgegeven) : base(stamgegeven)
        {
            if (stamgegeven == null)
            {
                throw new ArgumentNullException("stamgegeven");
            }
        }

        public string Omschrijving
        {
            get { return _entity.Omschrijving; }
        }
    }
}