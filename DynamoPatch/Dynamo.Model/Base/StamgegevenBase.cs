using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dynamo.Model.Base
{
    public class StamgegevenBase:ModelBase
    {
        public override string GetKorteOmschrijving()
        {
            return string.Format("Omschrijving = {0}", Omschrijving);
        }
        [MaxLength(50)]
        public string Omschrijving { get; set; }
        public DateTime GeldigVanaf { get; set; }
        public DateTime? GeldigTot { get; set; }
    }
}
