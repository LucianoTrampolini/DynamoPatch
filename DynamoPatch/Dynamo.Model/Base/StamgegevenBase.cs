using System;
using System.ComponentModel.DataAnnotations;

namespace Dynamo.Model.Base
{
    public class StamgegevenBase : ModelBase
    {
        public DateTime? GeldigTot { get; set; }
        public DateTime GeldigVanaf { get; set; }

        [MaxLength(50)]
        public string Omschrijving { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("Omschrijving = {0}", Omschrijving);
        }
    }
}