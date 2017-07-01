using System.Linq;

using Dynamo.BL.Base;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Beheerder
{
    class VerwerkAanmelding : BusinessRuleBase<Vergoeding>
    {
        #region Member fields

        private readonly PlanningsDagRepository _planningsDagRepository;

        #endregion

        public VerwerkAanmelding(IDynamoContext context)
            : base(context)
        {
            _planningsDagRepository = new PlanningsDagRepository(context);
        }

        public override bool Execute(Vergoeding entity)
        {
            var planningsDag = _planningsDagRepository.Load(x => x.Datum == entity.Datum && x.Verwijderd == false)
                .FirstOrDefault();

            if (planningsDag == null)
            {
                planningsDag = new PlanningsDag
                {
                    Datum = entity.Datum
                };
            }

            planningsDag.MiddagOpmerking += entity.DagdeelId == 2
                ? string.Format("{0} ", entity.Beheerder.Naam)
                : "";
            planningsDag.AvondOpmerking += entity.DagdeelId == 3
                ? string.Format("{0} ", entity.Beheerder.Naam)
                : "";
            _planningsDagRepository.Save(planningsDag);
            return true;
        }

        public override void OnDispose()
        {
            _planningsDagRepository.Dispose();
        }
    }
}