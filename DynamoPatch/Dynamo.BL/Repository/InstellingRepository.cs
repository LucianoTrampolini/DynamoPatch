using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.Model;

namespace Dynamo.BL
{
    public class InstellingRepository : RepositoryBase<Model.Instelling>
    {
        public InstellingRepository()
            : base()
        { }

        public InstellingRepository(IDynamoContext context)
            : base(context)
        { }

        public override Model.Instelling Load(int Id)
        {
            return currentContext.Instellingen.FirstOrDefault();
        }
    }
}
