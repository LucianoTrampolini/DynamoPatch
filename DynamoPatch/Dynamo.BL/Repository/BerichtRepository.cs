using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Dynamo.Model;
using Dynamo.Model.Base;

namespace Dynamo.BL
{
    public class BerichtRepository : RepositoryBase<Bericht>
    {
        public void BerichtGelezen(Bericht bericht, Beheerder beheerder)
        {
            var beheerderBericht =
                bericht.BeheerderBerichten.FirstOrDefault(b => b.BeheerderId == beheerder.Id && b.Gelezen == null);
            if (beheerderBericht != null)
            {
                beheerderBericht.Gelezen = DateTime.Now;
                Save(bericht);
            }
        }

        public ICollection<Beheerder> GetBeheerders(List<int> list)
        {
            return currentContext.Beheerders.Where(b => list.Contains(b.Id))
                .ToList();
        }

        public override List<Bericht> Load(Expression<Func<Bericht, bool>> expression)
        {
            return currentContext.Berichten.Include(b => b.AangemaaktDoor)
                .Include(b => b.BerichtType)
                .Include(b => b.BeheerderBerichten)
                .Where(expression)
                .ToList();
        }

        protected override void HandleComplexPropertyChanges(ModelBase entityBase)
        {
            var entity = entityBase as Bericht;
            if (entity == null)
            {
                return;
            }
            foreach (var beheerderBericht in entity.BeheerderBerichten)
            {
                HandleChanges(beheerderBericht);
            }
        }
    }
}