using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.BL.BusinessRules.Band;
using Dynamo.BL.ResultMessages;
using Dynamo.Common.Constants;
using Dynamo.Model;
using Dynamo.Model.Base;

namespace Dynamo.BL
{
    public class BandRepository : RepositoryBase<Band>
    {
        public BandRepository() {}

        public BandRepository(IDynamoContext context)
            : base(context) {}

        public override void Delete(Band entity)
        {
            HandleContractChanges(entity);
            base.Delete(entity);
        }

        public override List<Band> Load(Expression<Func<Band, bool>> expression)
        {
            return currentContext.Bands.Include(band => band.ContactPersonen)
                .Include(band => band.Contracten)
                .Where(expression)
                .ToList();
        }

        public override Band Load(int Id)
        {
            return currentContext.Bands.FirstOrDefault(x => x.Id == Id);
        }

        public override List<Band> Load()
        {
            return currentContext.Bands.Where(x => x.Verwijderd == false)
                .ToList();
        }

        protected override void HandleComplexPropertyChanges(ModelBase entityBase)
        {
            var entity = entityBase as Band;
            if (entity == null)
            {
                return;
            }

            if (entity.BandTypeId == BandTypeConsts.Contract)
            {
                using (var bijwerkenContract = new BijwerkenContract(currentContext))
                {
                    bijwerkenContract.Execute(entity);
                }

                foreach (var contract in entity.Contracten)
                {
                    HandleChanges(contract);
                }
                foreach (var contactpersoon in entity.ContactPersonen)
                {
                    HandleChanges(contactpersoon);
                }

                HandleContractChanges(entity);
            }
        }

        private void HandleContractChanges(Band entity)
        {
            if (HasEntityChanged(entity))
            {
                using (var br = new HandleNaamChanged(currentContext))
                {
                    br.Execute(entity);
                }
            }

            using (var br = new BijwerkenPlanningEindDatumContract(currentContext))
            {
                br.Execute(entity);
            }

            using (var br = new BijwerkenPlanningContract(currentContext))
            {
                br.Execute(entity);
            }
            using (var br = new BijwerkenBetalingenContract(currentContext))
            {
                br.Execute(entity);
            }
        }

        #region Extra Methods

        public List<Boeking> GetBoekingen(Band band)
        {
            return currentContext.Boekingen.Where(x => x.BandId == band.Id)
                .ToList();
        }

        public void SaveBoeking(Boeking boeking)
        {
            SaveChanges(boeking);
        }

        public void BijwerkenContracten()
        {
            var list = Load(x => x.BandTypeId == BandTypeConsts.Contract && x.Verwijderd == false);
            foreach (var band in list)
            {
                HandleContractChanges(band);
            }
        }

        public List<OpenstaandBedragVoorBandMessage> GetOpenstaandeBedragenVoorDagdeel(int dagdeel)
        {
            var returnValue = new List<OpenstaandBedragVoorBandMessage>();

            var list = currentContext.Planning.Where(p => p.Datum == DateTime.Today && p.DagdeelId == dagdeel);
            foreach (Planning planning in list)
            {
                foreach (
                    Boeking boeking in
                        planning.Boekingen.Where(b => !b.Verwijderd && !b.DatumAfgezegd.HasValue && b.Band != null))
                {
                    var bedrag = boeking.Band.Betalingen.Where(b => !b.Verwijderd)
                        .Sum(b => b.Bedrag);
                    returnValue.Add(
                        new OpenstaandBedragVoorBandMessage
                        {
                            Bedrag = bedrag,
                            BandNaam = boeking.Band.Naam
                        });
                }
            }

            return returnValue;
        }

        #endregion
    }
}