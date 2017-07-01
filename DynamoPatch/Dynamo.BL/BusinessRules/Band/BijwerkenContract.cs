using System.Linq;

using Dynamo.BL.Base;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Band
{
    public class BijwerkenContract : BusinessRuleBase<Model.Band>
    {
        public BijwerkenContract(IDynamoContext context)
            : base(context) {}

        public override bool Execute(Model.Band entity)
        {
            //Als de band meerdere contracten heeft, even checken of de laatste nieuw is toegevoegd
            if (entity.Contracten.Count() > 1
                && entity.Contracten.Last()
                    .IsTransient())
            {
                //Een nieuw contract togevoegd!
                var oudContract = entity.Contracten.Last(contract => !contract.IsTransient());
                var nieuwContract = entity.Contracten.Last();
                if (oudContract != null)
                {
                    oudContract.EindeContract = nieuwContract.BeginContract.AddDays(-1);
                }
            }

            return true;
        }
    }
}