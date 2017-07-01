using System;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Betaling : ModelBase
    {
        public virtual Band Band { get; set; }
        public int? BandId { get; set; }
        public decimal Bedrag { get; set; }
        public DateTime Datum { get; set; }
        public string Opmerking { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("BandId = {0}, Bedrag = {1}, Opmerking = {2}", BandId, Bedrag, Opmerking);
        }
    }
}