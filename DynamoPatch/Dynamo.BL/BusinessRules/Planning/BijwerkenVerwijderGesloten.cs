using Dynamo.BL.Base;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Planning
{
    public class BijwerkenVerwijderGesloten : BusinessRuleBase<Gesloten>
    {
        #region Member fields

        private readonly PlanningRepository _planningRepository;

        #endregion

        public BijwerkenVerwijderGesloten(IDynamoContext context)
            : base(context)
        {
            _planningRepository = new PlanningRepository(context);
        }

        public override bool Execute(Gesloten entity)
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