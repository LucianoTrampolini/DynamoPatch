using System;
using System.Globalization;

using Dynamo.Common.Properties;

namespace Dynamo.Common
{
    public static class DateExtensions
    {
        public static int DagVanDeWeek(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
            }
            throw new ArgumentOutOfRangeException("DagVanDeWeek");
        }

        public static string DagVanDeWeekVoluit(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return StringResources.DagMaandag;
                case DayOfWeek.Tuesday:
                    return StringResources.DagDinsdag;
                case DayOfWeek.Wednesday:
                    return StringResources.DagWoensdag;
                case DayOfWeek.Thursday:
                    return StringResources.DagDonderdag;
                case DayOfWeek.Friday:
                    return StringResources.DagVrijdag;
                case DayOfWeek.Saturday:
                    return StringResources.DagZaterdag;
                case DayOfWeek.Sunday:
                    return StringResources.DagZondag;
            }

            throw new ArgumentOutOfRangeException("DagVanDeWeekVoluit");
        }

        public static string GetDynamoDatum(this DateTime date)
        {
            return string.Format("{0}-{1}-{2}", date.Day.ToString("00"), date.Month.ToString("00"), date.Year);
        }

        public static string GetDynamoDatumTijd(this DateTime date)
        {
            return string.Format(
                "{0}-{1}-{2} {3}:{4}",
                date.Day.ToString("00"),
                date.Month.ToString("00"),
                date.Year,
                date.Hour,
                date.Minute.ToString("00"));
        }

        public static DateTime GetEersteDagVanDeWeek(this DateTime date)
        {
            var result = date;
            while (result.DagVanDeWeek() != 1)
            {
                result = result.AddDays(-1);
            }
            return result;
        }

        public static int GetIsoWeekNr(this DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static string MaandVoluit(this DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return StringResources.MaandJanuari;
                case 2:
                    return StringResources.MaandFebruari;
                case 3:
                    return StringResources.MaandMaart;
                case 4:
                    return StringResources.MaandApril;
                case 5:
                    return StringResources.MaandMei;
                case 6:
                    return StringResources.MaandJuni;
                case 7:
                    return StringResources.MaandJuli;
                case 8:
                    return StringResources.MaandAugustus;
                case 9:
                    return StringResources.MaandSeptember;
                case 10:
                    return StringResources.MaandOktober;
                case 11:
                    return StringResources.MaandNovember;
                case 12:
                    return StringResources.MaandDecember;
            }

            throw new ArgumentOutOfRangeException("MaandVoluit");
        }
    }
}