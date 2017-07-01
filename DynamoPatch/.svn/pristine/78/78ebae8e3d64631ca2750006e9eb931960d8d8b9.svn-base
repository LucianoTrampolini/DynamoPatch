using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Dynamo.Common.Properties;

namespace Dynamo.Common
{
    public static class CommonMethods
    {
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("NL-nl");

        public static DateTime DynamoDatum2Datum(string dynamoDatum)
        {
            return DateTime.Parse(dynamoDatum, culture, DateTimeStyles.AssumeLocal);
        }

        public static bool IsDynamoDatum(string dynamoDatum)
        { 
            DateTime date;
            return DateTime.TryParse(dynamoDatum, culture, DateTimeStyles.AssumeLocal, out date);
        }

        public static string GetBedrag(decimal bedrag)
        {
            return bedrag.ToString("C", culture);
        }

        public static List<KeyValuePair<int, string>> GetOefenDagenList()
        {
            return new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int,string>(1,StringResources.DagMaandag),
                new KeyValuePair<int,string>(2,StringResources.DagDinsdag),
                new KeyValuePair<int,string>(3,StringResources.DagWoensdag),
                new KeyValuePair<int,string>(4,StringResources.DagDonderdag),
                new KeyValuePair<int,string>(5,StringResources.DagVrijdag),
                new KeyValuePair<int,string>(6,StringResources.DagZaterdag),
                new KeyValuePair<int,string>(7,StringResources.DagZondag)
            };
        }
    }
}
