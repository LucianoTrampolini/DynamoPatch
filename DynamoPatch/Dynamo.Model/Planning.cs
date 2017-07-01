using System;
using System.Collections.Generic;

using Dynamo.Common;
using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Planning : ModelBase
    {
        public Planning()
        {
            Boekingen = new List<Boeking>();
        }

        public bool Beschikbaar { get; set; }
        public virtual ICollection<Boeking> Boekingen { get; set; }
        public virtual Dagdeel Dagdeel { get; set; }
        public int DagdeelId { get; set; }
        public DateTime Datum { get; set; }
        public virtual Gesloten Gesloten { get; set; }
        public int? GeslotenId { get; set; }
        public virtual Oefenruimte Oefenruimte { get; set; }
        public int OefenruimteId { get; set; }
        public virtual PlanningsDag PlanningsDag { get; set; }
        public int PlanningsDagId { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("Datum = {0}, Dagdeel = {1}, {2}", Datum.GetDynamoDatum(), DagdeelId, OefenruimteId);
        }
    }
}