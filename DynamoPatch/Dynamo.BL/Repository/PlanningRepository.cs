using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.BL.Repository;
using Dynamo.BL.ResultMessages;
using Dynamo.Common;
using Dynamo.Common.Constants;
using Dynamo.Model;
using Dynamo.Model.Base;

namespace Dynamo.BL
{
    public class PlanningRepository : RepositoryBase<Planning>
    {
        public PlanningRepository() {}

        public PlanningRepository(IDynamoContext context)
            : base(context) {}

        public List<BandBoekingOverzichtMessage> GetBandBoekingOverzicht(Band band)
        {
            var returnValue = new List<BandBoekingOverzichtMessage>();
            var list = currentContext.Planning.Where(pl => pl.Boekingen.Any(boeking => boeking.BandId == band.Id))
                .OrderByDescending(x => x.Datum)
                .Take(10);
            foreach (var item in list)
            {
                var boeking = item.Boekingen.First(b => b.BandId == band.Id);

                returnValue.Add(
                    new BandBoekingOverzichtMessage
                    {
                        Datum = item.Datum.GetDynamoDatum(),
                        Opmerking = boeking.Opmerking
                    });
            }
            return returnValue;
        }

        public List<Band> GetBands()
        {
            return new BandRepository(currentContext).Load(x => x.Verwijderd == false)
                .OrderBy(x => x.Naam)
                .ToList();
        }

        public override List<Planning> Load(Expression<Func<Planning, bool>> expression)
        {
            return currentContext.Planning.Include(x => x.Boekingen)
                .Include("Boekingen.Band")
                .Include("Dagdeel")
                .Include("Oefenruimte")
                .Where(expression)
                .ToList();
        }

        public List<Gesloten> LoadGesloten(DateTime dateTime)
        {
            return
                new GeslotenRepository(currentContext).Load(
                    x =>
                        x.Verwijderd == false && x.DatumVan <= dateTime
                            && (x.DatumTot.HasValue == false || x.DatumTot.Value >= dateTime));
        }

        protected override void HandleComplexPropertyChanges(ModelBase entityBase)
        {
            var entity = entityBase as Planning;
            if (entity == null)
            {
                return;
            }
            var planningsDag = currentContext.PlanningsDagen.FirstOrDefault(x => x.Datum == entity.Datum);
            var planning = currentContext.Planning.FirstOrDefault(x => x.Id == entity.Id && x.Id > 0);

            if (planningsDag == null)
            {
                planningsDag = new PlanningsDag
                {
                    Datum = entity.Datum,
                    Planningen = new List<Planning>
                    {
                        entity
                    }
                };
                HandleChanges(planningsDag);
                currentContext.PlanningsDagen.Add(planningsDag);
            }
            else if (entity.IsTransient())
            {
                planningsDag.Planningen.Add(entity);
            }

            if (planning != null
                && entity.Boekingen.Any(b => b.IsTransient()))
            {
                planning.Boekingen.Add(entity.Boekingen.First(b => b.IsTransient()));
            }

            foreach (var boeking in entity.Boekingen)
            {
                HandleChanges(boeking);
                if (boeking.Band != null
                    && boeking.Band.BandTypeId == BandTypeConsts.Incidenteel
                    && boeking.Band.IsTransient())
                {
                    HandleChanges(boeking.Band);
                }
            }
        }
    }
}