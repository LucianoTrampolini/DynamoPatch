using System;
using System.Linq;

using Dynamo.BL.Base;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Band
{
    public class HandleNaamChanged : BusinessRuleBase<Model.Band>
    {
        #region Member fields

        private readonly PlanningRepository _planningRepository;

        #endregion

        public HandleNaamChanged(IDynamoContext context)
            : base(context)
        {
            _planningRepository = new PlanningRepository(context);
        }

        public override bool Execute(Model.Band entity)
        {
            var planning =
                _planningRepository.Load(
                    x => x.Datum > DateTime.Today && x.Boekingen.Count(bo => bo.BandId == entity.Id) > 0)
                    .ToList();

            foreach (var p in planning)
            {
                p.Boekingen.First(x => x.BandId == entity.Id)
                    .BandNaam = entity.Naam;
                p.Boekingen.First(x => x.BandId == entity.Id)
                    .Verwijderd = entity.Verwijderd;
                _planningRepository.HandleChanges(p);
            }

            return true;
        }

        public override void OnDispose()
        {
            _planningRepository.Dispose();
        }
    }
}