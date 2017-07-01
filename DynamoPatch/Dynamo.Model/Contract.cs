using System;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Contract : ModelBase
    {
        public int Backline { get; set; }

        public virtual Band Band { get; set; }
        public int BandId { get; set; }
        public DateTime BeginContract { get; set; }
        public decimal Borg { get; set; }
        public bool Crash { get; set; }
        public virtual Dagdeel Dagdeel { get; set; }
        public int DagdeelId { get; set; }
        public DateTime? EindeContract { get; set; }
        public int ExtraVersterkers { get; set; }
        public decimal MaandHuur { get; set; }
        public int Microfoons { get; set; }
        public int Oefendag { get; set; }
        public virtual Oefenruimte Oefenruimte { get; set; }
        public int OefenruimteId { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("BandId = {0}", BandId);
            ;
        }
    }
}