using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Band
{
    public class BijwerkenPlanningContract : BusinessRuleBase<Model.Band>
    {
        private PlanningRepository _planningRepository = null;
        private InstellingRepository _instellingRepository = null;

        public BijwerkenPlanningContract(IDynamoContext context)
            :base(context) 
        {
            _planningRepository = new PlanningRepository(context);
            _planningRepository.AdminModus = true;
            _instellingRepository = new InstellingRepository(context);
        }

        public override bool Execute(Model.Band entity)
        {
            var aantalWekenVooruit = _instellingRepository.Load(0).WekenVooruitBoeken;
            var returnValue = true;
            
            if(entity.Contracten !=null)
            foreach (var contract in entity.Contracten.Where(c=>c.EindeContract.HasValue==false || c.EindeContract.Value.Date > DateTime.Today))
            {
                var datum = GetStartDatum(contract.Oefendag);
                var datumEinde = datum.AddDays(aantalWekenVooruit * 7);

                //Beginnen aan het einde zodat we niet onnodig veel queries aan het begin moeten doen...
                while (datumEinde > datum)
                {
                    if (datumEinde < contract.BeginContract || datumEinde > contract.EindeContract)
                    {
                        //We zitten buiten de datumrange van het contract, er uit...
                        break;
                    }
                    var planning = _planningRepository.Load(x => x.Datum == datumEinde && x.DagdeelId == contract.DagdeelId && x.OefenruimteId == contract.OefenruimteId).FirstOrDefault();
                    if (planning == null || planning.Beschikbaar)
                    {
                        if (planning == null)
                        {
                            planning = new Model.Planning
                            {
                                Datum = datumEinde,
                                DagdeelId = contract.DagdeelId,
                                OefenruimteId = contract.OefenruimteId,
                                Beschikbaar = false
                            };
                        }
                        else
                        {
                            //Als er al eens geboekt is geweest, dan natuurlijk niet nog een keertje...
                            if (planning.Boekingen.Any(b => b.BandId == entity.Id && !b.Verwijderd))
                            {
                                datumEinde = datumEinde.AddDays(-7);
                                continue;
                            }
                            else
                            {
                                //We gaan boeken, dus vast reserveren...
                                planning.Beschikbaar = false;
                            }
                        }
                        var boeking = new Model.Boeking
                        {
                            BandNaam = entity.Naam,
                            DatumGeboekt = DateTime.Now,
                            Opmerking = string.Format("Contract. {0}", GetTelefoonNr(entity))
                        };
                        if (entity.IsTransient())
                        {
                            boeking.Band = entity;
                        }
                        else
                        {
                            boeking.BandId = entity.Id;
                        }

                        planning.Boekingen.Add(boeking);
                        _planningRepository.Save(planning);
                    }
                    datumEinde = datumEinde.AddDays(-7);
                }
            }

            return returnValue;
        }

        private string GetTelefoonNr(Model.Band entity)
        {
            foreach (var item in entity.ContactPersonen)
            {
                if (!string.IsNullOrWhiteSpace(item.Telefoon))
                {
                    return item.Telefoon;
                }
            }
            return string.Empty;
        }

        private DateTime GetStartDatum(int oefendag)
        {
            var datum = DateTime.Today;
            while (datum.DagVanDeWeek() != oefendag)
            {
                datum = datum.AddDays(1);
            }
            return datum;
        }

        public override void OnDispose()
        {
            _instellingRepository.Dispose();
            _planningRepository.Dispose();
        }
    }
}
