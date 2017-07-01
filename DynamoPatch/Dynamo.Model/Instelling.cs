using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Instelling : ModelBase
    {
        public decimal BedragBandWaarschuwing { get; set; }
        public decimal VergoedingBeheerder { get; set; }
        public int WekenIncidenteleBandsBewaren { get; set; }
        public int WekenVooruitBoeken { get; set; }

        public override string GetKorteOmschrijving()
        {
            return "Instellingen";
        }
    }
}