using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows;
using Dynamo.Model;
using Dynamo.Model.Base;
using Dynamo.Model.Context;

namespace Dynamo.Converter
{
    class Program
    {
        private static Beheerder _conversieBeheerder = new Beheerder { Naam = "Herman" };
        private static DynamoContext dynamoContext = new DynamoContext();
        private static OleDbConnection _conn = null;
        private static Dictionary<int,Beheerder> _beheerders = new Dictionary<int,Beheerder>();
        private static Dictionary<int, Oefenruimte> _oefenruimtes = new Dictionary<int, Oefenruimte>();
        private static Dictionary<int, Dagdeel> _dagdelen = new Dictionary<int, Dagdeel>();
        private static Dictionary<string, Dagdeel> _dagdelenString = new Dictionary<string, Dagdeel>();
        private static Dictionary<int, Band> _bands = new Dictionary<int, Band>();
        private static Dictionary<string, Band> _incidenteleBands = new Dictionary<string, Band>();
        private static Dictionary<string, Taak> _taken = new Dictionary<string, Taak>();

        static void Main(string[] args)
        {
            try
            {
                _conn = new OleDbConnection(global::Dynamo.Converter.Properties.Settings.Default.DynamoConnectionString);
                _conn.Open();

                /*
                if (dynamoContext.Database != null)
                    dynamoContext.Database.Delete();

                dynamoContext.Beheerders.Add(_conversieBeheerder);
                dynamoContext.Instellingen.Add(new Instelling { VergoedingBeheerder = 5, WekenIncidenteleBandsBewaren = 52, WekenVooruitBoeken = 20,BedragBandWaarschuwing =100 });
                Console.WriteLine(string.Format("Beheerder System toegevoegd. Start conversie... {0} ", DateTime.Now.ToLongTimeString()));
                dynamoContext.SaveChanges();

                Console.WriteLine(string.Format("Stamgegevens converteren... {0} ", DateTime.Now.ToLongTimeString()));
                PerformanceCounter c = new PerformanceCounter();
                CreateStamgegevens();
                Console.WriteLine(string.Format("Stamgegevens geconverteerd... {0} ", DateTime.Now.ToLongTimeString()));
                Console.WriteLine(string.Format("Beheerders converteren... {0} ", DateTime.Now.ToLongTimeString()));
                ConvertBeheerders();
                Console.WriteLine(string.Format("Beheerders geconverteerd... {0} ", DateTime.Now.ToLongTimeString()));
                Console.WriteLine(string.Format("Contractbands converteren... {0} ", DateTime.Now.ToLongTimeString()));
                ConvertContractBands();
                Console.WriteLine(string.Format("Contractbands geconverteerd... {0} ", DateTime.Now.ToLongTimeString()));
                Console.WriteLine(string.Format("Gesloten dagen converteren... {0} ", DateTime.Now.ToLongTimeString()));
                ConvertGeslotenDagen();
                Console.WriteLine(string.Format("Gesloten dagen geconverteerd... {0} ", DateTime.Now.ToLongTimeString()));
                Console.WriteLine(string.Format("Incidentele bands converteren... {0} ", DateTime.Now.ToLongTimeString()));
                ConvertIncidenteleBands();
                Console.WriteLine(string.Format("Incidentele bands geconverteerd... {0} ", DateTime.Now.ToLongTimeString()));
                Console.WriteLine(string.Format("Memo's converteren... {0} ", DateTime.Now.ToLongTimeString()));
                ConvertMemos();
                Console.WriteLine(string.Format("Memo's geconverteerd... {0} ", DateTime.Now.ToLongTimeString()));
                 */

                InitStamGegevens();
                Console.WriteLine(string.Format("Logboek converteren... {0} ", DateTime.Now.ToLongTimeString()));
                ConvertLog();
                Console.WriteLine(string.Format("Logboek geconverteerd... {0} ", DateTime.Now.ToLongTimeString()));
                Console.WriteLine("Druk op een toets om de converter te beeindigen.");
                Console.Read();

            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("{0}", ex.ToString()));
            }
        }

        private static void InitStamGegevens()
        {

            var cmd = new OleDbCommand("SELECT * FROM Beheerders", _conn);
            var da = new OleDbDataAdapter(cmd);
            var bheerders = new DynamoOud();

            da.Fill(bheerders, "Beheerders");
            foreach (var bheerder in bheerders.Beheerders)
            {
                var beheerders = dynamoContext.Beheerders;
                foreach (var beheerder in beheerders)
                {
                    if (beheerder.Naam == bheerder.Naam)
                    {
                        _beheerders.Add(bheerder.ID, beheerder);
                        break;
                    }
                }
            }

            foreach (var dagdeel in dynamoContext.Dagdelen)
            {
                if (dagdeel.Omschrijving == "Ochtend")
                {
                    _dagdelen.Add(3, dagdeel);
                }
                if (dagdeel.Omschrijving == "Middag")
                {
                    _dagdelen.Add(1, dagdeel);
                }
                if (dagdeel.Omschrijving == "Avond")
                {
                    _dagdelen.Add(2, dagdeel);
                }

            }
            foreach (var oefentruimte in dynamoContext.Oefenruimtes)
            {
                _oefenruimtes.Add(oefentruimte.Id, oefentruimte);
            }
            
        }

        private static void ConvertLog()
        {
            //Datum >=#1-1-2005# AND
            var cmd = new OleDbCommand("SELECT * FROM Log Where  Datum < #1-1-2011# ORDER BY Datum, Id", _conn);
            var da = new OleDbDataAdapter(cmd);
            var log = new DynamoOud();

            da.Fill(log, "Log");

            try
            {
                PlanningsDag planningsDag = null;
                DynamoOud logOpmerkingen = null;
                foreach (var logOud in log.Log)
                {
                    
                    if (planningsDag == null || planningsDag.Datum != logOud.Datum.Date)
                    {
                        if (planningsDag != null)
                        {
                            SavePlanningsDag(planningsDag);
                        }
                        planningsDag = new PlanningsDag();
                        planningsDag.Datum = logOud.Datum;
                        //cmd = new OleDbCommand("SELECT * FROM LogDagOpmerkingen WHERE Datum = #" + logOud.Datum.Date.ToShortDateString() + "#", _conn);
                        cmd = new OleDbCommand("SELECT * FROM LogDagOpmerkingen WHERE Datum = @Datum", _conn);
                        cmd.Parameters.Add("@Datum", logOud.Datum);
                        da = new OleDbDataAdapter(cmd);
                        logOpmerkingen = new DynamoOud();
                        da.Fill(logOpmerkingen, "LogDagOpmerkingen");
                        foreach (var opmerking in logOpmerkingen.LogDagOpmerkingen)
                        {
                            planningsDag.DagOpmerking = opmerking.IsDagOpmerkingNull() ? "" : opmerking.DagOpmerking;
                            planningsDag.MiddagOpmerking = opmerking.IsMiddagOpmerkingNull() ? "" : opmerking.MiddagOpmerking;
                            planningsDag.AvondOpmerking = opmerking.IsAvondOpmerkingNull() ? "" : opmerking.AvondOpmerking;
                        }
                    }
                    var planning = new Planning();
                    
                    planning.Datum = logOud.Datum.Date;
                    planning.Dagdeel = GetDagdeel(logOud.Dagdeel);
                    planning.Oefenruimte = GetOefenruimte(logOud.Oefenruimte);
                    planning.Aangemaakt = logOud.AangemaaktOp;
                    planning.AangemaaktDoor = GetBeheerder(logOud.AangemaaktID);
                    planning.Gewijzigd = logOud.GewijzigdOp;
                    planning.GewijzigdDoor = GetBeheerder(logOud.GewijzigdID);
                    planning.Beschikbaar = logOud.IsBandnaamNull() || logOud.Bandnaam == string.Empty;

                    cmd = new OleDbCommand("SELECT * FROM LogOpmerkingen WHERE LogId = " + logOud.ID.ToString(), _conn);
                    da = new OleDbDataAdapter(cmd);
                    logOpmerkingen = new DynamoOud();
                    da.Fill(logOpmerkingen, "LogOpmerkingen");
                    foreach (var opmerking in logOpmerkingen.LogOpmerkingen)
                    {
                        planning.Boekingen.Add(new Boeking
                        {
                            DatumGeboekt = opmerking.AangemaaktOp,
                            Band = opmerking.BandID == 0 ? GetBand(opmerking.IsBandNaamNull() ? "" : opmerking.BandNaam) : GetBand(opmerking.BandID),
                            BandNaam = opmerking.IsBandNaamNull() ? "" : opmerking.BandNaam,
                            Opmerking = opmerking.Opmerkingen,
                            Aangemaakt = opmerking.AangemaaktOp,
                            AangemaaktDoor = GetBeheerder(opmerking.AangemaaktID),
                            Gewijzigd = opmerking.GewijzigdOp,
                            GewijzigdDoor = GetBeheerder(opmerking.GewijzigdID),
                            DatumAfgezegd = (logOud.Bandnaam==opmerking.BandNaam) ? (DateTime?)null :opmerking.GewijzigdOp 
                        });
                    }

                    planningsDag.Planningen.Add(planning);
                }
                if (planningsDag != null)
                {
                    SavePlanningsDag(planningsDag);
                }
            }
            catch (Exception ex)
            {
                OpgetredenFout fout = new OpgetredenFout
                {
                    Entiteit = "Agenda",
                    Methode = "ConvertLog",
                    FoutMelding = ex.ToString()
                };
                SaveFout(fout);
            }

        }

        private static void ConvertMemos()
        {
            var cmd = new OleDbCommand("SELECT * FROM Memos", _conn);
            var da = new OleDbDataAdapter(cmd);
            var memos = new DynamoOud();

            da.Fill(memos, "Memos");
            foreach (var memoOud in memos.Memos)
            {
                try
                {
                    var bericht = new Bericht();
                    bericht.Titel = memoOud.Titel;
                    bericht.Tekst = memoOud.Tekst;
                    bericht.Datum = memoOud.AangemaaktOp;
                    bericht.BerichtTypeId = 3;
                    bericht.Aangemaakt = memoOud.AangemaaktOp;
                    bericht.AangemaaktDoor = GetBeheerder(memoOud.AangemaaktID);
                    bericht.Gewijzigd = memoOud.GewijzigdOp;
                    bericht.GewijzigdDoor = GetBeheerder(memoOud.GewijzigdID);

                    SaveBericht(bericht);
                }
                catch (Exception ex)
                {
                    OpgetredenFout fout = new OpgetredenFout
                    {
                        Entiteit = memoOud.ID.ToString(),
                        Methode = "ConvertMemos",
                        FoutMelding = ex.ToString()
                    };
                    SaveFout(fout);
                }
            }
        }

        private static void ConvertIncidenteleBands()
        {
            var cmd = new OleDbCommand("SELECT * FROM IncidenteleBands", _conn);
            var da = new OleDbDataAdapter(cmd);
            var contractBands = new DynamoOud();

            da.Fill(contractBands, "IncidenteleBands");
            foreach (var contractBand in contractBands.IncidenteleBands)
            {
                try
                {
                    var band = new Band();
                    band.Naam = contractBand.BandNaam;
                    band.Telefoon = contractBand.IsTelefoonNull() ? "" : contractBand.Telefoon;
                    band.BSNNummer = contractBand.IsIDNummerNull() ? "" : contractBand.IDNummer;
                    band.Kasten = 0;
                    band.BandTypeId = 2;
                    band.Aangemaakt = contractBand.AangemaaktOp;
                    band.AangemaaktDoor = GetBeheerder(contractBand.AangemaaktID);
                    band.Gewijzigd = contractBand.GewijzigdOp;
                    band.GewijzigdDoor = GetBeheerder(contractBand.GewijzigdID);
                    
                    SaveBand(band);
                    if (!_incidenteleBands.ContainsKey(band.Naam))
                    {
                        _incidenteleBands.Add(band.Naam, band);
                    }
                }
                catch (Exception ex)
                {
                    OpgetredenFout fout = new OpgetredenFout
                    {
                        Entiteit = contractBand.BandNaam,
                        Methode = "ConvertIncidenteleBands",
                        FoutMelding = ex.ToString()
                    };
                    SaveFout(fout);
                }
            }
        }

        private static void ConvertGeslotenDagen()
        {
            var cmd = new OleDbCommand("SELECT * FROM Gesloten", _conn);
            var da = new OleDbDataAdapter(cmd);
            var geslotenDagen = new DynamoOud();

            da.Fill(geslotenDagen, "Gesloten");
            
                try
                {
                    Gesloten gesloten = null;
                    foreach (var geslotenOud in geslotenDagen.Gesloten)
                    {
                        if (gesloten == null || gesloten.DatumVan != geslotenOud.DatumVan || gesloten.Reden != geslotenOud.Reden)
                        {
                            if (gesloten != null)
                            {
                                SaveGesloten(gesloten);
                            }
                            gesloten = new Gesloten();
                            gesloten.Reden = geslotenOud.Reden;
                            gesloten.DatumVan = geslotenOud.DatumVan;
                            gesloten.DatumTot = geslotenOud.DatumTot;
                            gesloten.Aangemaakt = geslotenOud.IsAangemaaktOpNull() ? DateTime.Now : geslotenOud.AangemaaktOp;
                            gesloten.AangemaaktDoor = GetBeheerder(geslotenOud.AangemaaktID);
                            gesloten.Gewijzigd = geslotenOud.IsGewijzigdOpNull() ? DateTime.Now : geslotenOud.GewijzigdOp;
                            gesloten.GewijzigdDoor = GetBeheerder(geslotenOud.GewijzigdID);
                        }

                        //var dDatum = geslotenOud.DatumVan.Date;
                        //while (dDatum <= geslotenOud.DatumTot)
                        //{
                        //    if (geslotenOud.Dag == dDatum.DagVanDeWeek())
                        //    {
                        //        gesloten.Dagen.Add(new GeslotenDag
                        //        {
                        //            Dagdeel = GetDagdeel(geslotenOud.Dagdeel),
                        //            Oefenruimte = GetOefenruimte(geslotenOud.Oefenruimte),
                        //            Datum = dDatum,
                        //            Aangemaakt = geslotenOud.AangemaaktOp,
                        //            AangemaaktDoor = GetBeheerder(geslotenOud.AangemaaktID),
                        //            Gewijzigd = geslotenOud.GewijzigdOp,
                        //            GewijzigdDoor = GetBeheerder(geslotenOud.GewijzigdID)
                        //        });
                        //    }
                        //    dDatum = dDatum.AddDays(1);
                        //}
                    }
                    if (gesloten != null)
                    {
                        SaveGesloten(gesloten);
                    }
                }
                catch (Exception ex)
                {
                    OpgetredenFout fout = new OpgetredenFout
                    {
                        Entiteit = "GeslotenDag",
                        Methode = "ConvertGeslotenDagen",
                        FoutMelding = ex.ToString()
                    };
                    SaveFout(fout);
                }
            
        }

        private static void ConvertVergoedingen(Beheerder beheerder, int oldId)
        {
            var cmd = new OleDbCommand("SELECT * FROM BeheerdersVergoedingen WHERE beheerderId = " + oldId.ToString(), _conn);
            var da = new OleDbDataAdapter(cmd);
            var vergoedingen = new DynamoOud();

            da.Fill(vergoedingen, "BeheerdersVergoedingen");
            var vergoedingID = 0;
            try
            {
                foreach (var bet in vergoedingen.BeheerdersVergoedingen)
                {
                    vergoedingID = bet.ID;
                    var vergoeding = new Vergoeding();

                    vergoeding.Bedrag = (decimal)bet.Bedrag;
                    vergoeding.Datum = bet.Datum;
                    vergoeding.Taak = GetTaak(bet.Taak);
                    vergoeding.Dagdeel = GetDagdeel(bet.Dagdeel);
                    vergoeding.Aangemaakt = bet.AangemaaktOp;
                    vergoeding.AangemaaktDoor = GetBeheerder(bet.AangemaaktID);
                    vergoeding.Gewijzigd = bet.GewijzigdOp;
                    vergoeding.GewijzigdDoor = GetBeheerder(bet.GewijzigdID);

                    beheerder.Vergoedingen.Add(vergoeding);
                }
            }
            catch (Exception ex)
            {
                var beheerderNaam = beheerder.Naam;
                OpgetredenFout fout = new OpgetredenFout
                {
                    Entiteit = string.Concat("VergoedingID =", vergoedingID, " Beheerder = ", beheerderNaam),
                    Methode = "ConvertVergoedingen",
                    FoutMelding = ex.ToString()
                };
                SaveFout(fout);
            }
        }

        private static void ConvertBetalingen(Band band, int oldId)
        {
            var cmd = new OleDbCommand("SELECT * FROM Betalingen WHERE BandId = " + oldId.ToString(), _conn);
            var da = new OleDbDataAdapter(cmd);
            var betalingen = new DynamoOud();

            da.Fill(betalingen, "Betalingen");
            int betalingId = 0;

            try
            {
                foreach (var bet in betalingen.Betalingen)
                {
                    betalingId = bet.ID;

                    if (bet.IsTeBetalenNull()==false && bet.TeBetalen != 0)
                    {
                        var betaling = new Betaling();

                        betaling.Bedrag = (decimal)bet.TeBetalen;
                        betaling.Datum = bet.Datum;
                        betaling.Opmerking = bet.IsOpmerkingNull() ? "" : bet.Opmerking.Replace(Environment.NewLine, " ");
                        betaling.Aangemaakt = bet.AangemaaktOp;
                        betaling.AangemaaktDoor = GetBeheerder(bet.AangemaaktID);
                        betaling.Gewijzigd = bet.GewijzigdOp;
                        betaling.GewijzigdDoor = GetBeheerder(bet.GewijzigdID);

                        band.Betalingen.Add(betaling);
                    }
                    if (bet.IsBetaaldNull() == false && bet.Betaald != 0)
                    {
                        var betaling = new Betaling();

                        betaling.Bedrag = (decimal)bet.Betaald * -1;
                        betaling.Datum = bet.Datum;
                        betaling.Opmerking = bet.IsOpmerkingNull() ? "" : bet.Opmerking;
                        betaling.Aangemaakt = bet.AangemaaktOp;
                        betaling.AangemaaktDoor = GetBeheerder(bet.AangemaaktID);
                        betaling.Gewijzigd = bet.GewijzigdOp;
                        betaling.GewijzigdDoor = GetBeheerder(bet.GewijzigdID);

                        band.Betalingen.Add(betaling);
                    }
                }
                
            }
            catch (Exception ex)
            {
                var bandNaam = band.Naam;
                OpgetredenFout fout = new OpgetredenFout
                {
                    Entiteit = string.Concat("BetalingID =", betalingId, " Band = ", bandNaam),
                    Methode = "ConvertBetalingen",
                    FoutMelding = ex.ToString()
                };
                SaveFout(fout);
            }
        }
   
        private static void ConvertContractBands()
        {
            var cmd = new OleDbCommand("SELECT * FROM ContractBands", _conn);
            var da = new OleDbDataAdapter(cmd);
            var contractBands = new DynamoOud();

            da.Fill(contractBands, "ContractBands");
            foreach (var contractBand in contractBands.ContractBands)
            {
                try
                {
                    var band = new Band();
                    band.Naam = contractBand.BandNaam;
                    band.ContactPersonen.Add(new ContactPersoon
                        {
                            Naam = contractBand.IsContactPersoon1Null() ? "" : contractBand.ContactPersoon1,
                            Telefoon = contractBand.IsMobiel1Null() ? contractBand.IsTelefoon1Null() ? "" : contractBand.Telefoon1 : contractBand.Mobiel1,
                            Email = "",
                            Adres = contractBand.IsAdres1Null() ? "" : contractBand.Adres1,
                            Plaats = contractBand.IsPostcode_Woonplaats1Null() ? "" : contractBand.Postcode_Woonplaats1
                        });

                    band.ContactPersonen.Add(new ContactPersoon
                    {
                        Naam = contractBand.IsContactPersoon2Null() ? "" : contractBand.ContactPersoon2,
                        Telefoon = contractBand.IsMobiel2Null() ? contractBand.IsTelefoon2Null() ? "" : contractBand.Telefoon2 : contractBand.Mobiel2,
                        Email = "",
                        Adres = contractBand.IsAdres2Null() ? "" : contractBand.Adres2,
                        Plaats = contractBand.IsPostcode_Woonplaats2Null() ? "" : contractBand.Postcode_Woonplaats2
                    });
                    band.Contracten.Add(new Contract
                    {
                        BeginContract = contractBand.ContractPer,
                        EindeContract = contractBand.EindDatumContract,
                        Borg = (decimal)contractBand.Borg,
                        MaandHuur = (decimal)contractBand.MaandHuur,
                        Oefendag = contractBand.Oefendag,
                        Oefenruimte = GetOefenruimte(contractBand.Oefenruimte),
                        Dagdeel = GetDagdeel(contractBand.Dagdeel),
                        Backline = contractBand.IsBacklineNull() ? 0 : contractBand.Backline,
                        Microfoons = contractBand.IsMicrofoonsNull() ? 0 : contractBand.Microfoons,
                        ExtraVersterkers = contractBand.IsExtraVersterkersNull() ? 0 : contractBand.ExtraVersterkers,
                        Crash = contractBand.IsCrashNull() == false && contractBand.Crash != 0
                    });

                    band.Kasten = contractBand.IsKastenNull() ? 0 : contractBand.Kasten;
                    band.Opmerkingen = contractBand.IsOpmerkingenNull() ? "" : contractBand.Opmerkingen;
                    band.BandTypeId = 1;
                    band.Aangemaakt = contractBand.AangemaaktOp;
                    band.AangemaaktDoor = GetBeheerder(contractBand.AangemaaktID);
                    band.Gewijzigd = contractBand.GewijzigdOp;
                    band.GewijzigdDoor = GetBeheerder(contractBand.GewijzigdID);

                    ConvertBetalingen(band, contractBand.ID);

                    SaveBand(band);
                    _bands.Add(contractBand.ID, band);
                    
                }
                catch (Exception ex)
                {
                    OpgetredenFout fout = new OpgetredenFout
                    {
                        Entiteit= contractBand.BandNaam,
                        Methode="ConvertContractBands",
                        FoutMelding = ex.ToString()
                    };
                    SaveFout(fout);
                }
            }
        }

        private static void ConvertBeheerders()
        {
            var cmd = new OleDbCommand("SELECT * FROM Beheerders", _conn);
            var da = new OleDbDataAdapter(cmd);
            var bheerders = new DynamoOud();

            da.Fill(bheerders, "Beheerders");
            foreach (var bheerder in bheerders.Beheerders)
            {
                try
                {
                    if (!bheerder.IsNaamNull())
                    {
                        var beheerder = new Beheerder();
                        beheerder.Naam = bheerder.Naam;
                        beheerder.Plaats = "";
                        beheerder.Telefoon = bheerder.IsTelefoonNull() ? "" : bheerder.Telefoon;
                        beheerder.Mobiel = bheerder.IsMobielNull() ? "" : bheerder.Mobiel;
                        beheerder.Email = bheerder.IseMailNull() ? "" : bheerder.eMail;
                        beheerder.Adres = bheerder.IsAdresNull() ? "" : bheerder.Adres;
                        beheerder.KleurAchtergrond = bheerder.Achtergrondkleur;
                        beheerder.KleurAchtergrondVelden = bheerder.Achtergrondkleurvelden;
                        beheerder.KleurKnoppen = bheerder.AchtergrondkleurKnoppen;
                        beheerder.KleurSelecteren = bheerder.Selecteerkleur;
                        beheerder.KleurTekst = bheerder.Voorgrondkleur;
                        beheerder.KleurTekstKnoppen = bheerder.VoorgrondkleurKnoppen;
                        beheerder.KleurTekstVelden = bheerder.Voorgrondkleurvelden;
                        beheerder.Aangemaakt = bheerder.AangemaaktOp;
                        beheerder.AangemaaktDoor = GetBeheerder(bheerder.AangemaaktID);
                        beheerder.Gewijzigd = bheerder.GewijzigdOp;
                        beheerder.GewijzigdDoor = GetBeheerder(bheerder.GewijzigdID);
                        beheerder.IsAdministrator = bheerder.Naam.ToLowerInvariant() == "john" || bheerder.Naam.ToLowerInvariant() == "goot";
                        ConvertVergoedingen(beheerder,bheerder.ID);
                        SaveBeheerder(beheerder);
                        _beheerders.Add(bheerder.ID, beheerder);
                    }
                }
                catch (Exception ex)
                {
                    OpgetredenFout fout = new OpgetredenFout
                    {
                        Entiteit = bheerder.Naam,
                        Methode = "ConvertBeheerders",
                        FoutMelding = ex.ToString()
                    };
                    SaveFout(fout);
                }
                
            }
        }

        private static Beheerder GetBeheerder(int p)
        {
            if (_beheerders.ContainsKey(p))
            {
                return _beheerders[p];
            }
            
            return _conversieBeheerder;
        }

        private static Oefenruimte GetOefenruimte(int p)
        {
            if (_oefenruimtes.ContainsKey(p))
            {
                return _oefenruimtes[p];
            }

            return null;
        }

        private static Dagdeel GetDagdeel(int p)
        {
            if (_dagdelen.ContainsKey(p))
            {
                return _dagdelen[p];
            }

            return null;
        }

        private static Dagdeel GetDagdeel(string p)
        {
            if (p.StartsWith("'s "))
            {
                p = p.Substring(3);
            }
            if (_dagdelenString.ContainsKey(p.ToLower()))
            {
                return _dagdelenString[p.ToLower()];
            }

            return null;
        }

        private static Band GetBand(int p)
        {
            if (_bands.ContainsKey(p))
            {
                return _bands[p];
            }

            return null;
        }

        private static Band GetBand(string p)
        {
            if (p.Length > 0 && _incidenteleBands.ContainsKey(p))
            {
                return _incidenteleBands[p];
            }

            return null;
        }
        
        private static Taak GetTaak(string p)
        {
            if (_taken.ContainsKey(p.ToLower()))
            {
                return _taken[p.ToLower()];
            }

            return null;
        }

        private static void SaveFout(OpgetredenFout fout)
        {
            dynamoContext.Dispose();
            dynamoContext = new DynamoContext();
            FillNewEntityDefaults(fout);
            dynamoContext.OpgetredenFouten.Add(fout);
            dynamoContext.SaveChanges();
        }

        private static void SaveBeheerder(Beheerder beheerder)
        {
            dynamoContext.Beheerders.Add(beheerder);
            dynamoContext.SaveChanges();
        }

        private static void SaveBand(Band band)
        {
            dynamoContext.Bands.Add(band);
            dynamoContext.SaveChanges();
        }

        private static void SaveBericht(Bericht bericht)
        {
            dynamoContext.Berichten.Add(bericht);
            dynamoContext.SaveChanges();
        }

        private static void SavePlanningsDag(PlanningsDag planningsDag)
        {
            dynamoContext.PlanningsDagen.Add(planningsDag);
            dynamoContext.SaveChanges();
        }

        private static void SaveGesloten(Gesloten gesloten)
        {
            dynamoContext.Gesloten.Add(gesloten);
            dynamoContext.SaveChanges();
        }

        private static void SaveBandType(BandType stamgegeven)
        {
            FillNewEntityDefaults(stamgegeven);
            stamgegeven.GeldigVanaf = new DateTime(2000, 1, 1);
            dynamoContext.BandTypes.Add(stamgegeven);
            dynamoContext.SaveChanges();
        }

        private static void SaveDagdeel(Dagdeel stamgegeven)
        {
            FillNewEntityDefaults(stamgegeven);
            stamgegeven.GeldigVanaf = new DateTime(2000, 1, 1);
            dynamoContext.Dagdelen.Add(stamgegeven);
            dynamoContext.SaveChanges();
        }

        private static void SaveOefenruimte(Oefenruimte oefenruimte)
        {
            FillNewEntityDefaults(oefenruimte);
            dynamoContext.Oefenruimtes.Add(oefenruimte);
            dynamoContext.SaveChanges();
        }

        private static void SaveTaak(Taak taak)
        {
            FillNewEntityDefaults(taak);
            taak.GeldigVanaf = new DateTime(2000, 1, 1);
            dynamoContext.Taken.Add(taak);
            dynamoContext.SaveChanges();
        }

        private static void SaveBerichtType(BerichtType berichtType)
        {
            FillNewEntityDefaults(berichtType);
            berichtType.GeldigVanaf = new DateTime(2000, 1, 1);
            dynamoContext.BerichtTypes.Add(berichtType);
            dynamoContext.SaveChanges();
        }

        private static void FillNewEntityDefaults<T>(T entity) where T : ModelBase
        {
            entity.AangemaaktDoor = _conversieBeheerder;
            entity.GewijzigdDoor = _conversieBeheerder;
        }

        private static void CreateStamgegevens()
        {
            CreateBandTypes();
            CreateDagdelen();
            CreateOefenruimtes();
            CreateTaken();
            CreateBerichtTypes();
        }

        private static void CreateBandTypes()
        {
            var bandType = new BandType();
            bandType.Omschrijving = "Contract";
            SaveBandType(bandType);
            bandType = new BandType();
            bandType.Omschrijving = "Incidenteel";
            SaveBandType(bandType);
        }

        private static void CreateDagdelen()
        {
            var dagdeel = new Dagdeel();
            dagdeel.Omschrijving = "Ochtend";
            //SaveDagdeel(dagdeel);
            _dagdelen.Add(3, dagdeel);
            _dagdelenString.Add("ochtend", dagdeel);
            dagdeel = new Dagdeel();
            dagdeel.Omschrijving = "Middag";
            //SaveDagdeel(dagdeel);
            _dagdelen.Add(1,dagdeel);
            _dagdelenString.Add("middag", dagdeel);
            dagdeel = new Dagdeel();
            dagdeel.Omschrijving = "Avond";
            //SaveDagdeel(dagdeel);
            _dagdelen.Add(2,dagdeel);
            _dagdelenString.Add("avond", dagdeel);
        }

        private static void CreateOefenruimtes()
        {
            var oefenruimte = new Oefenruimte();
            oefenruimte.Naam = "Oefenruimte 1";
            SaveOefenruimte(oefenruimte);
            _oefenruimtes.Add(1, oefenruimte);

            oefenruimte = new Oefenruimte();
            oefenruimte.Naam = "Oefenruimte 2";
            SaveOefenruimte(oefenruimte);
            _oefenruimtes.Add(2, oefenruimte);

            oefenruimte = new Oefenruimte();
            oefenruimte.Naam = "Oefenruimte 3";
            SaveOefenruimte(oefenruimte);
            _oefenruimtes.Add(3, oefenruimte);
        }

        private static void CreateTaken()
        {
            var cmd = new OleDbCommand("SELECT Distinct Taak FROM BeheerdersVergoedingen", _conn);
            var da = new OleDbDataAdapter(cmd);
            var taken = new DataTable();

            da.Fill(taken);
            foreach (DataRow taakOud in taken.Rows)
            {
                var taak = new Taak { Omschrijving = taakOud[0].ToString() };
                SaveTaak(taak);
                _taken.Add(taak.Omschrijving.ToLower(), taak);
            }
        }

        private static void CreateBerichtTypes()
        {
            var berichtType = new BerichtType { Omschrijving ="Intern" };
            SaveBerichtType(berichtType);
            berichtType = new BerichtType { Omschrijving = "Website" };
            SaveBerichtType(berichtType);
            berichtType = new BerichtType { Omschrijving = "Memo" };
            SaveBerichtType(berichtType);
        }
    }
}
