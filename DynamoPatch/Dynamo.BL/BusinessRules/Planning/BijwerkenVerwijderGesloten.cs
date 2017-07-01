using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Planning
{
    public class BijwerkenVerwijderGesloten :BusinessRuleBase<Model.Gesloten>
    {
        private PlanningRepository _planningRepository = null;

        public BijwerkenVerwijderGesloten(IDynamoContext context)
            :base(context) 
        {
            _planningRepository = new PlanningRepository(context);
        }

        public override bool Execute(Model.Gesloten entity)
        {
            var planning = _planningRepository.Load(p => p.GeslotenId == entity.Id);

            foreach (Model.Planning p in planning)
            {
                p.Gesloten = null;
                _planningRepository.Save(p);
            }

            return true;
        }

        public override void OnDispose()
        {
            _planningRepository.Dispose();
        }
    }
}
