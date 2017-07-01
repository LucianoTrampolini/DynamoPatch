using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class OefenruimteViewModel : EntityViewModel<Oefenruimte>
    {
        public OefenruimteViewModel(Oefenruimte oefenruimte)
            : base(oefenruimte) {}

        public string Naam
        {
            get { return _entity.Naam; }
        }
    }
}