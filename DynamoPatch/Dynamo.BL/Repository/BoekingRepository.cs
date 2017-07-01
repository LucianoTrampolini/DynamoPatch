using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.Model;

namespace Dynamo.BL
{
    public class BoekingRepository : RepositoryBase<Boeking>
    {
        public override List<Boeking> Load(Expression<Func<Boeking, bool>> expression)
        {
            return currentContext.Boekingen.Where(expression)
                .ToList();
        }
    }
}