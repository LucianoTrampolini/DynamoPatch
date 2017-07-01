using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dynamo.Model;

namespace Dynamo.BL
{
    public class PlanningsDagRepository : RepositoryBase<Model.PlanningsDag>
    {
        private PlanningRepository _planningRepository = null;
        public PlanningRepository PlanningRepository
        {
            get
            {
                if (_planningRepository == null)
                {
                    _planningRepository = new PlanningRepository(currentContext);
                }
                return _planningRepository;
            }
        }

        public PlanningsDagRepository()
        { }

        public PlanningsDagRepository(IDynamoContext context)
            : base(context)
        { }

        public override List<Model.PlanningsDag> Load(System.Linq.Expressions.Expression<Func<Model.PlanningsDag, bool>> expression)
        {
            return currentContext.PlanningsDagen.Include("Planningen").Include("Planningen.Dagdeel").Include("Planningen.Boekingen").Include("Planningen.Boekingen.AangemaaktDoor").Include("Planningen.Boekingen.GewijzigdDoor").Include("GewijzigdDoor").Where(expression).ToList();
        }

        protected override void HandleComplexPropertyChanges(Model.Base.ModelBase entity)
        {
            var planningsDag = entity as Model.PlanningsDag;
            if (planningsDag == null)
            {
                return;
            }

            foreach (var planning in planningsDag.Planningen)
            {
                HandleChanges(planning);
                foreach (var boeking in planning.Boekingen)
                {
                    HandleChanges(boeking);
                }
            }
        }

        public override void OnDispose()
        {
            if (_planningRepository != null)
            {
                _planningRepository.Dispose();
            }
        }
    }
}
