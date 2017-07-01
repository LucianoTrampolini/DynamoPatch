using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Model;
using System.Data.Entity;

namespace Dynamo.BL.Repository
{
    public class BetalingRepository:RepositoryBase<Model.Betaling>
    {
        public BetalingRepository()
            : base()
        { }

        public BetalingRepository(IDynamoContext context)
            : base(context)
        { }

        public override List<Model.Betaling> Load(System.Linq.Expressions.Expression<Func<Model.Betaling, bool>> expression)
        {
            return currentContext.Betalingen.Include("AangemaaktDoor").Include("GewijzigdDoor").Include("Band").Where(expression).ToList();
        }
    }
}
