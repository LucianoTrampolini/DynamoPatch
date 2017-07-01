using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class PlanningsDagViewModel : EntityViewModel<Model.PlanningsDag>
    {
        public string Opmerking
        {
            get { return _entity.DagOpmerking; }
            set 
            {
                _entity.DagOpmerking = value;
            }
        }

        public string MiddagOpmerking
        {
            get { return _entity.MiddagOpmerking; }
            set
            {
                _entity.MiddagOpmerking = value;
            }
        }

        public string AvondOpmerking
        {
            get { return _entity.AvondOpmerking; }
            set
            {
                _entity.AvondOpmerking = value;
            }
        }
        public PlanningsDagViewModel(Model.PlanningsDag planningsDag)
            : base(planningsDag)
        { }

    }
}
