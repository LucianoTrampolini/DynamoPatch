using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;

namespace Dynamo.BL
{
    public class BoekingRepository : RepositoryBase<Model.Boeking>
    {
        public override List<Model.Boeking> Load(System.Linq.Expressions.Expression<Func<Model.Boeking, bool>> expression)
        {
            return currentContext.Boekingen.Where(expression).ToList();
        }
    }
}
