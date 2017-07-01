using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.BL.Repository;
using Dynamo.Model;
using Dynamo.Common.Constants;

namespace Dynamo.BL.BusinessRules.Beheerder
{
    public class KanAanmelden :BusinessRuleBase<Model.Vergoeding>
    {
        private VergoedingRepository _vergoedingRepository = null;

        public KanAanmelden(IDynamoContext context)
            :base(context) 
        {
            _vergoedingRepository = new VergoedingRepository(context);
        }

        public override bool Execute(Vergoeding entity)
        {
            var vergoeding = _vergoedingRepository.Load(verg => verg.Datum == entity.Datum && verg.DagdeelId == entity.DagdeelId && verg.Verwijderd==false);
            var count =
            vergoeding.Count(verg => verg.TaakId == TaakConsts.Beheer && entity.TaakId == TaakConsts.Beheer || (verg.BeheerderId == entity.Beheerder.Id));
            return count == 0;
        }

        public override void OnDispose()
        {
            _vergoedingRepository.Dispose();
        }
    }
}
