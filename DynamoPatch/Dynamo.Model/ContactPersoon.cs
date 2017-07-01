using System.ComponentModel.DataAnnotations;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class ContactPersoon : ModelBase
    {
        [MaxLength(50)]
        public string Adres { get; set; }

        public virtual Band Band { get; set; }
        public int BandId { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        [MaxLength(50)]
        public string Plaats { get; set; }

        [MaxLength(15)]
        public string Telefoon { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("Band = {0}, Naam = {1}", Band.Naam, Naam);
        }
    }
}