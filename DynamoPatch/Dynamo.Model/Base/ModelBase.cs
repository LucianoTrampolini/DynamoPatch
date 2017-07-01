using System;

namespace Dynamo.Model.Base
{
    public class ModelBase
    {
        public ModelBase()
        {
            Aangemaakt = DateTime.Now;
            Gewijzigd = DateTime.Now;
        }

        public DateTime Aangemaakt { get; set; }
        public virtual Beheerder AangemaaktDoor { get; set; }
        public int? AangemaaktDoorId { get; set; }
        public DateTime Gewijzigd { get; set; }
        public virtual Beheerder GewijzigdDoor { get; set; }
        public int? GewijzigdDoorId { get; set; }

        public int Id { get; set; }
        public bool Verwijderd { get; set; }

        public virtual string GetKorteOmschrijving()
        {
            throw new NotImplementedException("Implementeer in afgeleide");
        }

        public bool IsTransient()
        {
            return Id == 0;
        }
    }
}