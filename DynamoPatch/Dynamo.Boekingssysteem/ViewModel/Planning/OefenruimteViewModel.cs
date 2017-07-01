using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class OefenruimteViewModel : EntityViewModel<Model.Oefenruimte>
    {
        public OefenruimteViewModel(Model.Oefenruimte oefenruimte)
            : base(oefenruimte)
        { }

        public string Naam
        {
            get { return _entity.Naam; }
        }
    }
}
