using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class OpgetredenFout : ModelBase
    {
        public string Entiteit { get; set; }
        public string FoutMelding { get; set; }
        public string Methode { get; set; }

        public override string GetKorteOmschrijving()
        {
            return string.Format("Methode = {0}, Foutmelding = {1}", Methode, FoutMelding);
        }
    }
}