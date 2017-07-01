using System;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Vergoeding : ModelBase
    {
        public decimal Bedrag { get; set; }

        public virtual Beheerder Beheerder { get; set; }
        public int BeheerderId { get; set; }
        public virtual Dagdeel Dagdeel { get; set; }
        public int DagdeelId { get; set; }

        public DateTime Datum { get; set; }
        public virtual Taak Taak { get; set; }
        public int TaakId { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("Beheerder = {0}, Taak = {1}", BeheerderId, TaakId);
        }
    }
}