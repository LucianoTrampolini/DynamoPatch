using System;
using System.ComponentModel.DataAnnotations;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Boeking : ModelBase
    {
        public virtual Band Band { get; set; }
        public int? BandId { get; set; }

        [MaxLength(50)]
        public string BandNaam { get; set; }

        public DateTime? DatumAfgezegd { get; set; }
        public DateTime DatumGeboekt { get; set; }
        public string Opmerking { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("BandNaam = {0}, Opmerking = {1}", BandNaam, Opmerking);
        }
    }
}