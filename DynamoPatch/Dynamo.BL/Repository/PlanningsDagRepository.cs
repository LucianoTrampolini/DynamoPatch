using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.Model;
using Dynamo.Model.Base;

namespace Dynamo.BL
{
    public class PlanningsDagRepository : RepositoryBase<PlanningsDag>
    {
        #region Member fields

        private PlanningRepository _planningRepository;

        #endregion

        public PlanningsDagRepository() {}

        public PlanningsDagRepository(IDynamoContext context)
            : base(context) {}

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

        public override List<PlanningsDag> Load(Expression<Func<PlanningsDag, bool>> expression)
        {
            return currentContext.PlanningsDagen.Include("Planningen")
                .Include("Planningen.Dagdeel")
                .Include("Planningen.Boekingen")
                .Include("Planningen.Boekingen.AangemaaktDoor")
                .Include("Planningen.Boekingen.GewijzigdDoor")
                .Include("GewijzigdDoor")
                .Where(expression)
                .ToList();
        }

        public override void OnDispose()
        {
            if (_planningRepository != null)
            {
                _planningRepository.Dispose();
            }
        }

        protected override void HandleComplexPropertyChanges(ModelBase entity)
        {
            var planningsDag = entity as PlanningsDag;
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
    }
}