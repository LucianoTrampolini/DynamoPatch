using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class PlanningsDagViewModel : EntityViewModel<PlanningsDag>
    {
        public PlanningsDagViewModel(PlanningsDag planningsDag)
            : base(planningsDag) {}

        public string AvondOpmerking
        {
            get { return _entity.AvondOpmerking; }
            set { _entity.AvondOpmerking = value; }
        }

        public string MiddagOpmerking
        {
            get { return _entity.MiddagOpmerking; }
            set { _entity.MiddagOpmerking = value; }
        }

        public string Opmerking
        {
            get { return _entity.DagOpmerking; }
            set { _entity.DagOpmerking = value; }
        }
    }
}