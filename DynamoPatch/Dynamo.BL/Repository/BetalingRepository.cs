using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.Model;

namespace Dynamo.BL.Repository
{
    public class BetalingRepository : RepositoryBase<Betaling>
    {
        public BetalingRepository() {}

        public BetalingRepository(IDynamoContext context)
            : base(context) {}

        public override List<Betaling> Load(Expression<Func<Betaling, bool>> expression)
        {
            return currentContext.Betalingen.Include("AangemaaktDoor")
                .Include("GewijzigdDoor")
                .Include("Band")
                .Where(expression)
                .ToList();
        }
    }
}