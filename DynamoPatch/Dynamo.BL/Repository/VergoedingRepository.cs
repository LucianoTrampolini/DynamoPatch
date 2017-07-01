using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.BL.BusinessRules.Beheerder;
using Dynamo.Common.Constants;
using Dynamo.Model;

namespace Dynamo.BL.Repository
{
    public class VergoedingRepository : RepositoryBase<Vergoeding>
    {
        public VergoedingRepository() {}

        public VergoedingRepository(IDynamoContext context)
            : base(context) {}

        public Beheerder GetBeheerder(int p)
        {
            return currentContext.Beheerders.FirstOrDefault(x => x.Id == p);
        }

        public Beheerder GetCurrentBeheerder()
        {
            var vergoeding = currentContext.Vergoedingen.OrderByDescending(x => x.Id)
                .FirstOrDefault(x => x.TaakId == TaakConsts.Beheer);
            if (vergoeding == null)
            {
                return null;
            }
            return vergoeding.Beheerder;
        }

        public override List<Vergoeding> Load(Expression<Func<Vergoeding, bool>> expression)
        {
            return currentContext.Vergoedingen.Include("Dagdeel")
                .Include("Taak")
                .Include("Beheerder")
                .Where(expression)
                .ToList();
        }

        public override void Save(Vergoeding entity, bool supressMessage = false)
        {
            bool goOn = false;
            using (var br = new KanAanmelden(currentContext))
            {
                goOn = br.Execute(entity);
            }

            if (goOn)
            {
                SaveChanges(entity);
                using (var br = new VerwerkAanmelding(currentContext))
                {
                    br.Execute(entity);
                }
            }
            else
            {
                HasMelding = true;
                Melding = "Er is al iemand aangemeld voor beheer of je bent al aangemeld voor dit dagdeel!";
            }
        }
    }
}