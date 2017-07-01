using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Common;
using Dynamo.BL;
using Newtonsoft.Json;
using System.Net.FtpClient;
using System.IO;
using System.Net.Sockets;

namespace Dynamo.Web
{
    public class Integration
    {
        public void PushWeekDataToServer(DateTime date)
        {
            var week = date.GetIsoWeekNr();

            var eersteDagVanDeWeek = date.GetEersteDagVanDeWeek();
            var laatsteDagVanDeWeek = eersteDagVanDeWeek.AddDays(6);

            var planning = new PlanningRepository().Load(x => x.Datum >= eersteDagVanDeWeek && x.Datum <= laatsteDagVanDeWeek);

            var weekData = new WeekData
            {
                week = week,
                start = eersteDagVanDeWeek,
                stamp = DateTime.Now,
                ruimtes = new Ruimtes{r1 = GetDefaultOefenruimteData(),r2=GetDefaultOefenruimteData(),r3=GetDefaultOefenruimteData()}
                
            };

            foreach (var item in planning.ToList())
            {
                if (!IsBeschikbaar(item))
                {
                    if (item.OefenruimteId == 1)
                    {
                        if (item.DagdeelId == 2)
                        {
                            weekData.ruimtes.r1.m[item.Datum.DagVanDeWeek() - 1] = IsGesloten(item.Gesloten, item.Datum, item.OefenruimteId, item.DagdeelId) ? 2 : 1;
                        }
                        else if (item.DagdeelId == 3)
                        {
                            weekData.ruimtes.r1.a[item.Datum.DagVanDeWeek() - 1] = IsGesloten(item.Gesloten, item.Datum, item.OefenruimteId, item.DagdeelId) ? 2 : 1;
                        }
                    }
                    else if (item.OefenruimteId == 2)
                    {
                        if (item.DagdeelId == 2)
                        {
                            weekData.ruimtes.r2.m[item.Datum.DagVanDeWeek() - 1] = IsGesloten(item.Gesloten, item.Datum, item.OefenruimteId, item.DagdeelId) ? 2 : 1;
                        }
                        else if (item.DagdeelId == 3)
                        {
                            weekData.ruimtes.r2.a[item.Datum.DagVanDeWeek() - 1] = IsGesloten(item.Gesloten, item.Datum, item.OefenruimteId, item.DagdeelId) ? 2 : 1;
                        }
                    }
                    else if (item.OefenruimteId == 3)
                    {
                        if (item.DagdeelId == 2)
                        {
                            weekData.ruimtes.r3.m[item.Datum.DagVanDeWeek() - 1] = IsGesloten(item.Gesloten, item.Datum, item.OefenruimteId, item.DagdeelId) ? 2 : 1;
                        }
                        else if (item.DagdeelId == 3)
                        {
                            weekData.ruimtes.r3.a[item.Datum.DagVanDeWeek() - 1] = IsGesloten(item.Gesloten, item.Datum, item.OefenruimteId, item.DagdeelId) ? 2 : 1;
                        }
                    }
                }
            }

            weekData.open = GetIsOpen(weekData);

            var serializedWeekData = JsonConvert.SerializeObject(weekData, Formatting.None, new JsonSerializerSettings { TypeNameHandling=TypeNameHandling.None});

            UploadData(serializedWeekData, string.Format("vrij-{0:yyyyMMdd}.js", eersteDagVanDeWeek));
            
        }

        public bool IsBeschikbaar(Model.Planning item)
        {
            if ((item.Boekingen.Count == 0 || item.Boekingen.All(boeking => boeking.DatumAfgezegd != null)) && !IsGesloten(item.Gesloten, item.Datum, item.OefenruimteId, item.DagdeelId))
            {
                return true;
            }
            return false;
        }

        private bool IsGesloten(Model.Gesloten gesloten, DateTime date, int oefenruimte, int dagdeel)
        {
            var dagvandeweek = date.DagVanDeWeek();

            if (dagvandeweek == 6 && dagdeel == 3)
            {
                return true;
            }

            if(gesloten == null)
            {
                return false;
            }

            if(date.Date < gesloten.DatumVan.Date || date.Date > gesloten.DatumTot.GetValueOrDefault(new DateTime(2099,1,1)))
            {
                return false;
            }

            
            if((oefenruimte == 1 && gesloten.Oefenruimte1) || (oefenruimte == 2 && gesloten.Oefenruimte2) || (oefenruimte == 3 && gesloten.Oefenruimte3))
            {
                if (dagvandeweek == 1 && gesloten.Maandag)
                {
                    if (dagdeel == 2)
                    {
                        return gesloten.Middag;
                    }
                    return gesloten.Avond;
                }
                if (dagvandeweek == 2 && gesloten.Dinsdag)
                {
                    if (dagdeel == 2)
                    {
                        return gesloten.Middag;
                    }
                    return gesloten.Avond;
                }
                if (dagvandeweek == 3 && gesloten.Woensdag)
                {
                    if (dagdeel == 2)
                    {
                        return gesloten.Middag;
                    }
                    return gesloten.Avond;
                }
                if (dagvandeweek == 4 && gesloten.Donderdag)
                {
                    if (dagdeel == 2)
                    {
                        return gesloten.Middag;
                    }
                    return gesloten.Avond;
                }
                if (dagvandeweek == 5 && gesloten.Vrijdag)
                {
                    if (dagdeel == 2)
                    {
                        return gesloten.Middag;
                    }
                    return gesloten.Avond;
                }
                if (dagvandeweek == 6 && (gesloten.Zaterdag))
                {
                    if (dagdeel == 2)
                    {
                        return gesloten.Middag;
                    }
                    //zaterdagavond even hardcoded op true
                    return true;
                }
                if (dagvandeweek == 7 && gesloten.Zondag)
                {
                    if (dagdeel == 2)
                    {
                        return gesloten.Middag;
                    }
                    return gesloten.Avond;
                }

            }

            return false;
        }

        private void UploadData(string data, string filename)
        {

            return;
            try
            {
                using (var cl = new FtpClient())
                {
                    cl.Host = "db7842.web54.ixl.nu";
                    cl.Credentials = new System.Net.NetworkCredential { UserName = "dynamo-data@oefenruimtedynamo.nl", Password = "yrpSDnlm3U3hEUNwHrBh" };
                    cl.Connect();
                    using (Stream ostream = cl.OpenWrite(string.Concat("", filename)))
                    {
                        StreamWriter sw = new StreamWriter(ostream);
                        sw.Write(data);
                        sw.Flush();
                        ostream.Close();
                    }
                }
            }
            catch (IOException e)
            {
                if (e.InnerException is SocketException)
                {
                }
            }
            catch (Exception)
            { }
        }

        public void PushLastUpdateFile()
        {
            var serializedData = JsonConvert.SerializeObject(
                new LastUpdateFile { stamp = DateTime.Now }, 
                Formatting.None, 
                new JsonSerializerSettings 
                { 
                    TypeNameHandling = TypeNameHandling.None 
                });
            UploadData(serializedData, "lastupdate.js");
        }

        private int GetIsOpen(WeekData weekData)
        {
            if (weekData.ruimtes.r1.m.All(i => i == 2)
                && weekData.ruimtes.r1.a.All(i => i == 2)
                && weekData.ruimtes.r2.m.All(i => i == 2)
                && weekData.ruimtes.r2.a.All(i => i == 2)
                && weekData.ruimtes.r3.m.All(i => i == 2)
                && weekData.ruimtes.r3.a.All(i => i == 2)
                )
            {
                return 0;
            }
            return 1;
        }

        private OefenRuimteData GetDefaultOefenruimteData()
        {
            return new OefenRuimteData
            {
                m = new[] { 0, 0, 0, 0, 0, 0, 0 },
                a = new[] { 0, 0, 0, 0, 0, 2, 0 }
            };
        }

    }

    public class LastUpdateFile 
    {
        public DateTime stamp { get; set; }
    }

    public class WeekData
    {
        public int week { get; set; }
        public DateTime start { get; set; }
        public DateTime stamp { get; set; }
        public int open { get; set; }
        public Ruimtes ruimtes { get; set; }
        
    }

    public class Ruimtes
    {
        public OefenRuimteData r1 { get; set; }
        public OefenRuimteData r2 { get; set; }
        public OefenRuimteData r3 { get; set; }
    }

    public class OefenRuimteData
    {
        public int[] m { get; set; }
        public int[] a { get; set; }
    }
}
