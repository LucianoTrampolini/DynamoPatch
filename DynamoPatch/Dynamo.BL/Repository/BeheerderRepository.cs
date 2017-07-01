using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.Common.Constants;
using Dynamo.Model;

namespace Dynamo.BL
{
    public class BeheerderRepository : RepositoryBase<Beheerder>
    {
        public BeheerderRepository() {}

        public BeheerderRepository(IDynamoContext context)
            : base(context) {}

        public bool AdminBeheerder { get; set; }

        public Beheerder CurrentBeheerder
        {
            get
            {
                if (AdminBeheerder)
                {
                    return Load(BeheerderConsts.AdminBeheerder);
                }
                var vergoeding = currentContext.Vergoedingen.OrderByDescending(x => x.Id)
                    .FirstOrDefault(x => x.TaakId == TaakConsts.Beheer && x.Verwijderd == false);
                if (vergoeding == null)
                {
                    return Load(BeheerderConsts.AdminBeheerder);
                }

                return vergoeding.Beheerder;
            }
        }

        public int GetAantalNieuweBerichten()
        {
            return
                currentContext.BeheerderBerichten.Count(b => b.BeheerderId == CurrentBeheerder.Id && b.Gelezen == null);
        }

        /// <summary>
        /// Als er een beheerder is ingelogd voor beheer en het dagdeel is >= (=omdat er soms ook alleen ingelogd wordt in de avond)
        /// </summary>
        /// <param name="dagdeelId"></param>
        /// <returns></returns>
        public bool IsBeheerderIngelogdVoorDagDeel(int dagdeelId)
        {
            return
                currentContext.Vergoedingen.Any(
                    v =>
                        v.Datum == DateTime.Today && v.Verwijderd == false && v.DagdeelId >= dagdeelId
                            && v.TaakId == TaakConsts.Beheer);
        }

        public override Beheerder Load(int Id)
        {
            return currentContext.Beheerders.Where(x => x.Id == Id)
                .FirstOrDefault();
        }

        public override List<Beheerder> Load(Expression<Func<Beheerder, bool>> expression)
        {
            return currentContext.Beheerders.Where(expression)
                .ToList();
        }

        public override List<Beheerder> Load()
        {
            //Bij het laden van de beheerders administrator met rust laten...
            return currentContext.Beheerders.Where(x => x.Id > BeheerderConsts.AdminBeheerder && x.Verwijderd == false)
                .ToList();
        }
    }
}