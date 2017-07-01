using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using System.Data.Entity;

namespace Dynamo.BL
{
    public class ChangeLogRepository : RepositoryBase<Model.ChangeLog>
    {
        public override List<Model.ChangeLog> Load()
        {
            return currentContext.ChangeLog.Include("AangemaaktDoor").OrderByDescending(x => x.Id).Take(500).ToList<Model.ChangeLog>();
        }
    }
}
