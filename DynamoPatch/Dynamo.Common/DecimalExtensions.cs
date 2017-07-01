namespace Dynamo.Common
{
    public static class DecimalExtensions
    {
        public static string GetDynamoBedrag(this decimal bedrag)
        {
            return CommonMethods.GetBedrag(bedrag);
        }
    }
}