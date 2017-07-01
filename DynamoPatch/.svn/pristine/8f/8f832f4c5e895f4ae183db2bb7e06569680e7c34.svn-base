using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Planning
{
    public class BijwerkenPlanningGesloten : BusinessRuleBase<Model.Gesloten>
    {
        private PlanningRepository _planningRepository = null;
        private InstellingRepository _instellingRepository = null;

        public BijwerkenPlanningGesloten(IDynamoContext context)
            :base(context) 
        {
            _planningRepository = new PlanningRepository(context);
            _planningRepository.AdminModus = true;
            _instellingRepository = new InstellingRepository(context);
        }

        private Model.Gesloten _entity;
        public override bool Execute(Model.Gesloten entity)
        {
            _entity = entity;
            var aantalWekenVooruit =_instellingRepository.Load(0).WekenVooruitBoeken;
            var returnValue = true;
            var datum = DateTime.Today;
            var datumEinde = datum.AddDays(aantalWekenVooruit * 7);
            
            if (entity.DatumTot.HasValue && datumEinde > entity.DatumTot)
            {
                datumEinde = entity.DatumTot.Value;
            }
            if (datumEinde < DateTime.Today)
            {
                return true;
            }

            var listDagen = GetListDagen();
            int aantalToegevoegd = 0;
            int aantalDagenTerugGeteld = 0;

            //Beginnen aan het einde zodat we niet onnodig veel queries aan het begin moeten doen...
            while (datumEinde > datum)
            {
                if (listDagen.Contains(datumEinde.DagVanDeWeek()))
                {
                    if (entity.Oefenruimte1)
                    {
                        if (entity.Middag)
                        {
                            aantalToegevoegd += GeslotenToegevoegd(datumEinde, 1, 2);
                        }
                        if (entity.Avond)
                        {
                            aantalToegevoegd += GeslotenToegevoegd(datumEinde, 1, 3);
                        }
                    }
                    if (entity.Oefenruimte2)
                    {
                        if (entity.Middag)
                        {
                            aantalToegevoegd += GeslotenToegevoegd(datumEinde, 2, 2);
                        }
                        if (entity.Avond)
                        {
                            aantalToegevoegd += GeslotenToegevoegd(datumEinde, 2, 3);
                        }
                    }
                    if (entity.Oefenruimte3)
                    {
                        if (entity.Middag)
                        {
                            aantalToegevoegd += GeslotenToegevoegd(datumEinde, 3, 2);
                        }
                        if (entity.Avond)
                        {
                            aantalToegevoegd += GeslotenToegevoegd(datumEinde, 3, 3);
                        }
                    }
                }
                datumEinde = datumEinde.AddDays(-1);
                aantalDagenTerugGeteld++;
                if (aantalDagenTerugGeteld > 7  )
                {
                    if (aantalToegevoegd == 0)
                    {
                        break;
                    }
                    else
                    {
                        aantalDagenTerugGeteld = 0;
                        aantalToegevoegd = 0;
                    }
                }
            }
            return returnValue;
        }

        private int GeslotenToegevoegd(DateTime datum, int oefenruimte, int dagdeel)
        {
            var planning = _planningRepository.Load(x => x.Datum == datum && x.DagdeelId == dagdeel && x.OefenruimteId == oefenruimte).FirstOrDefault();
            if (planning == null)
            {
                var p = new Model.Planning
                {
                    Datum = datum,
                    Dagdeel = _planningRepository.GetStamGegevens(Enum.Stamgegevens.Dagdelen).FirstOrDefault(x => x.Id == dagdeel) as Model.Dagdeel,
                    Oefenruimte = _planningRepository.GetOefenruimtes().FirstOrDefault(x => x.Id == oefenruimte),
                    Gesloten = _entity,

                };
                _planningRepository.Save(p);
                return 1;
            }
            else
            {
                planning.GeslotenId = _entity.Id;
                _planningRepository.Save(planning);
            }
            return 0;
        }

        private List<int> GetListDagen()
        {
            var list = new List<int>();

            if (_entity.Maandag)
                list.Add(1);
            if (_entity.Dinsdag)
                list.Add(2);
            if (_entity.Woensdag)
                list.Add(3);
            if (_entity.Donderdag)
                list.Add(4);
            if (_entity.Vrijdag)
                list.Add(5);
            if (_entity.Zaterdag)
                list.Add(6);
            if (_entity.Zondag)
                list.Add(7);

            return list;
        }

        public override void OnDispose()
        {
            _planningRepository.Dispose();
            _instellingRepository.Dispose();
        }
    }
}
