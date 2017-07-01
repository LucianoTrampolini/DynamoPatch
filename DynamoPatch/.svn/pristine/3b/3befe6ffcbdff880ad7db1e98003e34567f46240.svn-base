using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Beheerder : ModelBase
    {
        public override string GetKorteOmschrijving()
        {
            return string.Format("Naam = {0}", Naam);
        }

        public Beheerder()
        {
            Vergoedingen = new List<Vergoeding>();
            Berichten = new List<Bericht>();
        }

        [MaxLength(30)]
        public string Naam { get; set; }
        [MaxLength(50)]
        public string Adres { get; set; }
        [MaxLength(10)]
        public string Postcode { get; set; }
        [MaxLength(50)]
        public string Plaats { get; set; }
        [MaxLength(15)]
        public string Telefoon { get; set; }
        [MaxLength(15)]
        public string Mobiel { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public virtual ICollection<Vergoeding> Vergoedingen { get; set; }
        public virtual ICollection<Bericht> Berichten { get; set; }

        public int KleurAchtergrond { get; set; }
        public int KleurTekst { get; set; }
        public int KleurAchtergrondVelden { get; set; }
        public int KleurTekstVelden { get; set; }
        public int KleurSelecteren { get; set; }
        public int KleurKnoppen { get; set; }
        public int KleurTekstKnoppen { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
