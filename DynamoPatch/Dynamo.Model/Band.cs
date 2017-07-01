using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Band : ModelBase
    {
        public override string GetKorteOmschrijving()
        {
            return string.Format("Naam = {0}", Naam);
        }

        public Band()
        {
            ContactPersonen = new List<ContactPersoon>();
            Contracten = new List<Contract>();
            Betalingen = new List<Betaling>();
        }

        [MaxLength(50)]
        public string Naam { get; set; }
        public virtual BandType BandType { get; set; }
        public int BandTypeId { get; set; }
        public virtual ICollection<Contract> Contracten { get; set; }
        public virtual ICollection<Betaling> Betalingen { get; set; }
        public int Kasten { get; set; }
        public string Opmerkingen { get; set; }
        public virtual ICollection<ContactPersoon> ContactPersonen { get; set; }

        //Voor incidentele bands
        [MaxLength(15)]
        public string Telefoon { get; set; }
        [MaxLength(20)]
        public string BSNNummer { get; set; }
        public bool HeeftVerleden { get; set; }
    }
}
