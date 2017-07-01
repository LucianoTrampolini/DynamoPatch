using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.BL.BusinessRules.Planning;
using Dynamo.Model;

namespace Dynamo.BL.Repository
{
    public class GeslotenRepository : RepositoryBase<Gesloten>
    {
        public GeslotenRepository() {}

        public GeslotenRepository(IDynamoContext context)
            : base(context) {}

        public void BijwerkenGeslotenDagenPlanning()
        {
            var list = currentContext.Gesloten.ToList();

            foreach (var entity in list)
            {
                using (var br = new BijwerkenPlanningGesloten(currentContext))
                {
                    br.Execute(entity);
                }
            }
        }

        public override void Delete(Gesloten entity)
        {
            using (var br = new BijwerkenVerwijderGesloten(currentContext))
            {
                br.Execute(entity);
            }
            base.Delete(entity);
        }

        public override List<Gesloten> Load(Expression<Func<Gesloten, bool>> expression)
        {
            return currentContext.Gesloten.Where(expression)
                .ToList();
        }

        public override List<Gesloten> Load()
        {
            return currentContext.Gesloten.Where(x => x.Verwijderd == false)
                .ToList();
        }

        public override void Save(Gesloten entity, bool supressMessage = false)
        {
            SaveChanges(entity);
            using (var br = new BijwerkenPlanningGesloten(currentContext))
            {
                br.Execute(entity);
            }
        }
    }
}