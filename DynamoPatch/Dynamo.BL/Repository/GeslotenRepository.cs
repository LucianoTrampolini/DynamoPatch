using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.BL.BusinessRules.Planning;
using Dynamo.Model;

namespace Dynamo.BL
{
    public class GeslotenRepository : RepositoryBase<Model.Gesloten>
    {
        public GeslotenRepository()
        { }

        public GeslotenRepository(IDynamoContext context)
            : base(context)
        { }

        public override void Save(Model.Gesloten entity, bool supressMessage = false)
        {
            SaveChanges(entity);
            using (var br = new BijwerkenPlanningGesloten(currentContext))
            {
                br.Execute(entity);
            }
        }

        public override List<Model.Gesloten> Load(System.Linq.Expressions.Expression<Func<Model.Gesloten, bool>> expression)
        {
            return currentContext.Gesloten.Where(expression).ToList();
        }

        public override List<Model.Gesloten> Load()
        {
            return currentContext.Gesloten.Where(x => x.Verwijderd == false).ToList();
        }

        public override void Delete(Model.Gesloten entity)
        {
            using (var br = new BijwerkenVerwijderGesloten(currentContext))
            {
                br.Execute(entity);
            }
            base.Delete(entity);
        }

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
    }
}
