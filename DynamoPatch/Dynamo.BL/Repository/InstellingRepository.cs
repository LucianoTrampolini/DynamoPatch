using System.Linq;

using Dynamo.Model;

namespace Dynamo.BL
{
    public class InstellingRepository : RepositoryBase<Instelling>
    {
        public InstellingRepository() {}

        public InstellingRepository(IDynamoContext context)
            : base(context) {}

        public override Instelling Load(int Id)
        {
            return currentContext.Instellingen.FirstOrDefault();
        }
    }
}