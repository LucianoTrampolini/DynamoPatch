using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Band
{
    public class BijwerkenPlanningEindDatumContract : BusinessRuleBase<Model.Band>
    {
        private PlanningRepository _planningRepository = null;

        public BijwerkenPlanningEindDatumContract(IDynamoContext context)
            :base(context) 
        {
            _planningRepository = new PlanningRepository(context);
            _planningRepository.AdminModus = true;
        }

        public override bool Execute(Model.Band entity)
        {
            const int systeemBeheerderId = 1;
            //datum om te checken iets lager leggen, aangezien sommige bands er wat langer in blijven staan dan de bedoeling is...
            var datum = DateTime.Today.AddMonths(-1);
            foreach (var contract in entity.Contracten
                .Where(c => c.EindeContract.HasValue && c.EindeContract.Value > DateTime.Today.AddDays(-120)))
            {
                if (IsZelfdeAlsContractInDeToekomst(entity, contract))
                {
                    continue;
                }

                var eindDatum = contract.EindeContract <= DateTime.Today ? DateTime.Today : contract.EindeContract;
                var bandId = entity.Id;
                var planning = _planningRepository
                    .Load(x => x.Verwijderd == false 
                        && x.Beschikbaar == false 
                        && x.Datum > eindDatum 
                        && x.Boekingen
                        .Any(b => b.BandId == bandId 
                            && b.DatumAfgezegd == null 
                            && b.AangemaaktDoorId == systeemBeheerderId)
                            );
                if (planning != null)
                {
                    
                    foreach (var item in planning)
                    {
                        if (item.Datum.DagVanDeWeek() == contract.Oefendag && item.DagdeelId == contract.DagdeelId && item.OefenruimteId == contract.OefenruimteId)
                        {
                            var boeking = item.Boekingen.FirstOrDefault(x => x.BandId == bandId && x.DatumAfgezegd == null );

                            boeking.Verwijderd = true;
                            boeking.DatumAfgezegd = DateTime.Today;
                            item.Beschikbaar = true;
                        }
                        _planningRepository.Save(item);
                    }
                }
            }
            return true;
        }

        private bool IsZelfdeAlsContractInDeToekomst(Model.Band band, Contract contract)
        {
            var test = band.Contracten.FirstOrDefault(c => c.BeginContract > contract.BeginContract && c.DagdeelId == contract.DagdeelId && c.Oefendag == contract.Oefendag && c.OefenruimteId == contract.OefenruimteId);

            return test != null;
        }

        public override void OnDispose()
        {
            _planningRepository.Dispose();
        }
    }
}
