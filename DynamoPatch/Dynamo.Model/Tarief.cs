using System;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Tarief : ModelBase
    {
        public decimal Bedrag { get; set; }
        public DateTime? DatumTot { get; set; }
        public DateTime DatumVan { get; set; }
    }
}