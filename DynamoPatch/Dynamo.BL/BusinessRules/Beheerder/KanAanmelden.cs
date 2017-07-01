using System.Linq;

using Dynamo.BL.Base;
using Dynamo.BL.Repository;
using Dynamo.Common.Constants;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Beheerder
{
    public class KanAanmelden : BusinessRuleBase<Vergoeding>
    {
        #region Member fields

        private readonly VergoedingRepository _vergoedingRepository;

        #endregion

        public KanAanmelden(IDynamoContext context)
            : base(context)
        {
            _vergoedingRepository = new VergoedingRepository(context);
        }

        public override bool Execute(Vergoeding entity)
        {
            var vergoeding =
                _vergoedingRepository.Load(
                    verg => verg.Datum == entity.Datum && verg.DagdeelId == entity.DagdeelId && verg.Verwijderd == false);

            return !vergoeding.Any(
                verg =>
                    verg.TaakId == TaakConsts.Beheer && entity.TaakId == TaakConsts.Beheer
                        || (verg.BeheerderId == entity.Beheerder.Id));
        }

        public override void OnDispose()
        {
            _vergoedingRepository.Dispose();
        }
    }
}