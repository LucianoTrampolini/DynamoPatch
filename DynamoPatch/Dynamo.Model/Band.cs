using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Band : ModelBase
    {
        public Band()
        {
            ContactPersonen = new List<ContactPersoon>();
            Contracten = new List<Contract>();
            Betalingen = new List<Betaling>();
        }

        public virtual BandType BandType { get; set; }
        public int BandTypeId { get; set; }
        public virtual ICollection<Betaling> Betalingen { get; set; }

        [MaxLength(20)]
        public string BSNNummer { get; set; }

        public virtual ICollection<ContactPersoon> ContactPersonen { get; set; }
        public virtual ICollection<Contract> Contracten { get; set; }
        public bool HeeftVerleden { get; set; }
        public int Kasten { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        public string Opmerkingen { get; set; }

        //Voor incidentele bands
        [MaxLength(15)]
        public string Telefoon { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("Naam = {0}", Naam);
        }
    }
}