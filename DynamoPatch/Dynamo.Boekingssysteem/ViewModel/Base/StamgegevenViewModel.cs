using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem;

namespace Dynamo.Boekingssysteem.ViewModel.Base
{
    public class StamgegevenViewModel:EntityViewModel<Model.Base.StamgegevenBase>
    {
         public StamgegevenViewModel(Model.Base.StamgegevenBase stamgegeven):base(stamgegeven)
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
