using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class BeheerderBericht :ModelBase
    {
        public override string GetKorteOmschrijving()
        {
            return string.Format("BeheerderId = {0}, BerichtId = {1}", BeheerderId, BerichtId);
        }

        public virtual Beheerder Beheerder { get; set; }
        public int BeheerderId { get; set; }
        public virtual Bericht Bericht { get; set; }
        public int BerichtId { get; set; }
        public DateTime? Gelezen { get; set; }
    }
}
