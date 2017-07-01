using System;
using System.ComponentModel.DataAnnotations;

using Dynamo.Common;
using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Gesloten : ModelBase
    {
        public bool Avond { get; set; }
        public DateTime? DatumTot { get; set; }
        public DateTime DatumVan { get; set; }
        public bool Dinsdag { get; set; }
        public bool Donderdag { get; set; }
        public bool Maandag { get; set; }
        public bool Middag { get; set; }
        public bool Oefenruimte1 { get; set; }
        public bool Oefenruimte2 { get; set; }
        public bool Oefenruimte3 { get; set; }

        [MaxLength(50)]
        public string Reden { get; set; }

        public bool Vrijdag { get; set; }
        public bool Woensdag { get; set; }
        public bool Zaterdag { get; set; }
        public bool Zondag { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format(
                "DatumVan = {0}, DatumTot = {1}",
                DatumVan,
                DatumTot.HasValue
                    ? DatumTot.Value.GetDynamoDatum()
                    : string.Empty);
        }
    }
}