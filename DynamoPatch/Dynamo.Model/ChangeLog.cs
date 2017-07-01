using System;
using System.ComponentModel.DataAnnotations;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class ChangeLog : ModelBase
    {
        public DateTime Datum { get; set; }

        [MaxLength(50)]
        public string Eigenschap { get; set; }

        [MaxLength(50)]
        public string Entiteit { get; set; }

        public string NieuweWaarde { get; set; }
        public string Omschrijving { get; set; }
        public string OudeWaarde { get; set; }
    }
}