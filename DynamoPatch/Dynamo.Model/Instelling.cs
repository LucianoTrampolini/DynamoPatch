using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Instelling : ModelBase
    {
        public override string GetKorteOmschrijving()
        {
            return string.Format("Instellingen");
        }
        public int WekenIncidenteleBandsBewaren { get; set; }
        public int WekenVooruitBoeken { get; set; }
        public decimal VergoedingBeheerder { get; set; }
        public decimal BedragBandWaarschuwing { get; set; }
    }
}
