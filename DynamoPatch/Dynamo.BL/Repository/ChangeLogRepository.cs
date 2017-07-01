using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Dynamo.Model;

namespace Dynamo.BL
{
    public class ChangeLogRepository : RepositoryBase<ChangeLog>
    {
        public override List<ChangeLog> Load()
        {
            return currentContext.ChangeLog.Include("AangemaaktDoor")
                .OrderByDescending(x => x.Id)
                .Take(500)
                .ToList();
        }
    }
}