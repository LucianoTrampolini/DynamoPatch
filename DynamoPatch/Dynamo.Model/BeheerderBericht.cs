using System;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class BeheerderBericht : ModelBase
    {
        public virtual Beheerder Beheerder { get; set; }
        public int BeheerderId { get; set; }
        public virtual Bericht Bericht { get; set; }
        public int BerichtId { get; set; }
        public DateTime? Gelezen { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("BeheerderId = {0}, BerichtId = {1}", BeheerderId, BerichtId);
        }
    }
}