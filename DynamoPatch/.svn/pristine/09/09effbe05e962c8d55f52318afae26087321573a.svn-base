using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using System.Data.Entity;

namespace Dynamo.BL
{
    public class BerichtRepository : RepositoryBase<Model.Bericht>
    {
        public override List<Model.Bericht> Load(System.Linq.Expressions.Expression<Func<Model.Bericht, bool>> expression)
        {
            return currentContext.Berichten.Include(b => b.AangemaaktDoor).Include(b => b.BerichtType).Include(b => b.BeheerderBerichten).Where(expression).ToList();
        }

        public ICollection<Model.Beheerder> GetBeheerders(List<int> list)
        {
            return currentContext.Beheerders.Where(b => list.Contains(b.Id)).ToList();
        }

        protected override void HandleComplexPropertyChanges(Model.Base.ModelBase entityBase)
        {
            var entity = entityBase as Model.Bericht;
            if (entity == null)
            {
                return;
            }
            foreach (var beheerderBericht in entity.BeheerderBerichten)
            {
                HandleChanges(beheerderBericht);
            }
        }

        public void BerichtGelezen(Model.Bericht bericht, Model.Beheerder beheerder)
        {
            var beheerderBericht = bericht.BeheerderBerichten.FirstOrDefault(b => b.BeheerderId == beheerder.Id && b.Gelezen==null);
            if (beheerderBericht != null)
            {
                beheerderBericht.Gelezen = DateTime.Now;
                Save(bericht);
            }
        }
    }
}
