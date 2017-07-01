using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dynamo.Common;
using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class PlanningsDag : ModelBase
    {
        public override string GetKorteOmschrijving()
        {
            return string.Format("Datum = {0}", Datum.GetDynamoDatum());
        }

        public PlanningsDag()
        {
            Planningen = new List<Planning>();
        }

        public DateTime Datum { get; set; }
        public virtual ICollection<Planning> Planningen { get; set; }

        public string DagOpmerking { get; set; }
        [MaxLength(256)]
        public string MiddagOpmerking { get; set; }
        [MaxLength(256)]
        public string AvondOpmerking { get; set; }
    }
}
