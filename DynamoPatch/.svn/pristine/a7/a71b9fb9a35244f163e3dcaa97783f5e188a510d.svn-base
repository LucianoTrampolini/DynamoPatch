using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Bericht : ModelBase
    {
        public override string GetKorteOmschrijving()
        {
            return string.Format("Titel = {0}, BerichtType = {1}", Titel, BerichtTypeId);
        }

        public Bericht()
        {
            BeheerderBerichten = new List<BeheerderBericht>();
        }

        //Koppeltabel tussen bericht en beheerder...
        public virtual ICollection<BeheerderBericht> BeheerderBerichten { get; set; }
        public DateTime Datum { get; set; }
        public virtual BerichtType BerichtType { get; set; }
        public int BerichtTypeId { get; set; }
        public string Titel { get; set; }
        public string Tekst { get; set; }
    }
}
