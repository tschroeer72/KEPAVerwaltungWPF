using System.Data;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Models.Local;
using KEPAVerwaltungWPF.Models.Web;
using KEPAVerwaltungWPF.Views;
using Microsoft.EntityFrameworkCore;
using TblDbchangeLog = KEPAVerwaltungWPF.Models.Web.TblDbchangeLog;
using TblMeisterschaften = KEPAVerwaltungWPF.Models.Local.TblMeisterschaften;

namespace KEPAVerwaltungWPF.Services;

public class DBService(LocalDbContext _localDbContext, WebDbContext _webDbContext, CommonService _commonService)
{
    //private ClsDBWeb m_objDBWeb = new ClsDBWeb();

    public async Task<List<Mitgliederliste>> GetMitgliederlisteAsync()
    {
        List<Mitgliederliste> lstMitglieder = new List<Mitgliederliste>();

        try
        {
            var tv = await (_localDbContext.TblMitglieders.OrderBy(o => o.Nachname)
                .Select(s => s)).ToListAsync();

            foreach (var item in tv)
            {
                Mitgliederliste objItem = new Mitgliederliste();
                objItem.Initial = item.Nachname.Substring(0, 1);
                objItem.Anzeigename = item.Nachname + ", " + item.Vorname;
                objItem.Nachname = item.Nachname;
                objItem.Vorname = item.Vorname;
                objItem.ID = item.Id;
                lstMitglieder.Add(objItem);
            }

            //------------------

            //Testdaten
            // Mitgliederliste objItem = new Mitgliederliste();
            // objItem.Initial = "S";
            // objItem.Anzeigename = "Schröer, Thorsten";
            // objItem.Nachname = "Schröer";
            // objItem.Vorname = "Thorsten";
            // objItem.ID = 1;
            // lstMitglieder.Add(objItem);
            //
            // objItem = new Mitgliederliste();
            // objItem.Initial = "S";
            // objItem.Anzeigename = "Schmidt, Wolfgang";
            // objItem.Nachname = "Schmidt";
            // objItem.Vorname = "Wolfgang";
            // objItem.ID = 2;
            // lstMitglieder.Add(objItem);
            //
            // objItem = new Mitgliederliste();
            // objItem.Initial = "B";
            // objItem.Anzeigename = "Bohn, Karl-Heinz";
            // objItem.Nachname = "Bohn";
            // objItem.Vorname = "Karl-Heinz";
            // objItem.ID = 3;
            // lstMitglieder.Add(objItem);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetMitgliederliste", ex.ToString());
        }

        return lstMitglieder;
    }

    public async Task<List<EmailListe>> GetEMaillisteAsync(bool bAktiv = false)
    {
        List<EmailListe> lstEMails = new List<EmailListe>();

        try
        {
            if (bAktiv)
            {
                var lst = await (_localDbContext.TblMitglieders
                    .Where(w => w.PassivSeit == null && w.Email != null)
                    .Select(s => new { s.Vorname, s.Email })).ToListAsync();
                //lstEMails = lst.ToList();

                foreach (var item in lst)
                {
                    EmailListe objEMail = new EmailListe();
                    objEMail.Vorname = item.Vorname;
                    objEMail.EMail = item.Email;
                    lstEMails.Add(objEMail);
                }
            }
            else
            {
                var lst = await (_localDbContext.TblMitglieders
                    .Where(w => w.Email != null)
                    .Select(s => new { s.Vorname, s.Email })).ToListAsync();
                //lstEMails = lst.ToList();

                foreach (var item in lst)
                {
                    EmailListe objEMail = new EmailListe();
                    objEMail.Vorname = item.Vorname;
                    objEMail.EMail = item.Email;
                    lstEMails.Add(objEMail);
                }
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "GetEMailliste", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "GetEMailliste", ex.ToString());
        }

        return lstEMails;
    }

    /*
    public List<ClsDBChangeLog> GetDBChangeLog()
    {
        List<ClsDBChangeLog> lstListe = new List<ClsDBChangeLog>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var lst = from l in DBContext.tblDBChangeLog
                          orderby l.Zeitstempel
                          select l;

                foreach (var item in lst.ToList())
                {
                    ClsDBChangeLog objItem = new ClsDBChangeLog();
                    objItem.ID = item.ID;
                    objItem.Zeitstempel = item.Zeitstempel.Value;
                    objItem.Computername = item.Computername;
                    objItem.Changeype = item.Changetype;
                    objItem.Tablename = item.Tablename;
                    objItem.Command = item.Command;
                    lstListe.Add(objItem);
                }

            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetDBChangeLog", ex.ToString());
            objForm.ShowDialog();
        }
        return lstListe;
    }
    */

    public async Task<DateTime> GetDBVersionWebAsync()
    {
        DateTime dtVersion = DateTime.MinValue;

        try
        {
            var vers = await (from v in _webDbContext.TblDbchangeLogs
                orderby v.Zeitstempel
                select v).ToListAsync();

            dtVersion = vers[vers.Count() - 1].Zeitstempel;
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetDBVersionWeb", ex.ToString());
        }

        return dtVersion;
    }

    public async Task<DateTime> GetDBVersionLocalAsync()
    {
        DateTime dtVersion = DateTime.MinValue;

        try
        {
            var vers = await (from v in _localDbContext.TblDbchangeLogs
                orderby v.Zeitstempel
                select v).ToListAsync();

            dtVersion = vers[vers.Count() - 1].Zeitstempel.Value;
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetDBVersionLocal", ex.ToString());
        }

        return dtVersion;
    }

    public async Task<MitgliedDetails> GetMitgliedDetailsAsync(Int32 iID)
    {
        MitgliedDetails objMitglied = new();

        try
        {
            // var item = from m in _localDbContext.TblMitglieders
            //     where m.Id == iID
            //     select m;

            var item = await _localDbContext.TblMitglieders.Where(w => w.Id == iID).ToListAsync();

            var objM = item.First();

            objMitglied.ID = objM.Id;
            objMitglied.Anrede = objM.Anrede;
            objMitglied.Vorname = objM.Vorname;
            objMitglied.Nachname = objM.Nachname;
            objMitglied.Spitzname = objM.Spitzname;
            objMitglied.Straße = objM.Straße;
            objMitglied.PLZ = objM.Plz;
            objMitglied.Ort = objM.Ort;
            objMitglied.Geburtsdatum = objM.Geburtsdatum.Value;
            objMitglied.MitgliedSeit = objM.MitgliedSeit;
            objMitglied.PassivSeit = objM.PassivSeit;
            objMitglied.AusgeschiedenAm = objM.AusgeschiedenAm;
            objMitglied.TelefonPrivat = objM.TelefonPrivat;
            objMitglied.TelefonMobil = objM.TelefonMobil;
            objMitglied.EMail = objM.Email;
            objMitglied.Ehemaliger = objM.Ehemaliger;
            objMitglied.Notizen = objM.Notizen;
            objMitglied.Bemerkungen = objM.Bemerkungen;

            //-------------------

            //Testdaten
            // switch (iID)
            // {
            //     case 1:
            //         objMitglied.ID = 1;
            //         objMitglied.Anrede = "Herr";
            //         objMitglied.Vorname = "Thorsten";
            //         objMitglied.Nachname = "Schröer";
            //         objMitglied.Spitzname = "Thor";
            //         objMitglied.Straße = "Druckerkehre 3";
            //         objMitglied.PLZ = "12355";
            //         objMitglied.Ort = "Berlin";
            //         objMitglied.Geburtsdatum = new DateTime(1972, 1, 9);
            //         objMitglied.MitgliedSeit = new DateTime(2021, 8, 18);
            //         objMitglied.PassivSeit = DateTime.Now;
            //         objMitglied.AusgeschiedenAm = DateTime.Now;
            //         objMitglied.TelefonPrivat = "Test";
            //         objMitglied.TelefonMobil = "Test";
            //         objMitglied.EMail = "Test";
            //         objMitglied.Ehemaliger = false;
            //         objMitglied.Notizen = "Notizen TS";
            //         objMitglied.Bemerkungen = "Bemerkungen TS";
            //         break;
            //     case 2:
            //         objMitglied.ID = 3;
            //         objMitglied.Anrede = "Herr";
            //         objMitglied.Vorname = "Wolfgang";
            //         objMitglied.Nachname = "Schmidt";
            //         objMitglied.Spitzname = "";
            //         objMitglied.Straße = "Prierosser Straße 51";
            //         objMitglied.PLZ = "12355";
            //         objMitglied.Ort = "Berlin";
            //         objMitglied.Geburtsdatum = new DateTime(1950, 11, 1);
            //         objMitglied.MitgliedSeit = new DateTime(2021, 11, 25);
            //         objMitglied.PassivSeit = null;
            //         objMitglied.AusgeschiedenAm = null;
            //         objMitglied.TelefonPrivat = "";
            //         objMitglied.TelefonMobil = "";
            //         objMitglied.EMail = "";
            //         objMitglied.Ehemaliger = false;
            //         objMitglied.Notizen = "Notizen WS";
            //         objMitglied.Bemerkungen = "Bemerkungen WS";
            //         break;
            //     case 3:
            //         objMitglied.ID = 3;
            //         objMitglied.Anrede = "Herr";
            //         objMitglied.Vorname = "Karl-Heinz";
            //         objMitglied.Nachname = "Bohn";
            //         objMitglied.Spitzname = "Kalle";
            //         objMitglied.Straße = "Lorenzweg 3";
            //         objMitglied.PLZ = "12099";
            //         objMitglied.Ort = "Berlin";
            //         objMitglied.Geburtsdatum = new DateTime(1966, 5, 28);
            //         objMitglied.MitgliedSeit = new DateTime(1997, 7, 2);
            //         objMitglied.PassivSeit = null;
            //         objMitglied.AusgeschiedenAm = null;
            //         objMitglied.TelefonPrivat = "";
            //         objMitglied.TelefonMobil = "";
            //         objMitglied.EMail = "";
            //         objMitglied.Ehemaliger = false;
            //         objMitglied.Notizen = "Notizen KHB";
            //         objMitglied.Bemerkungen = "Bemerkungen KHB";
            //         break;
            // }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetMitgliedDetails", ex.ToString());
        }

        return objMitglied;
    }

    /*
    public Int32 GetIDFromTurboDBNumber(Int32 iTurboDBNumber)
    {
        Int32 intID = -1;

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var item = from m in DBContext.tblMitglieder
                           where m.TurboDBNummer == iTurboDBNumber
                           select m;

                var objM = item.First();

                intID = objM.ID;
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "ConvertTurboDBNumber2ID", ex.ToString());
            objForm.ShowDialog();
        }

        return intID;
    }
    */

    public async Task<List<MitgliedDetails>> GetMitgliedDetailsAsync()
    {
        List<MitgliedDetails> lstMitglieder = new List<MitgliedDetails>();

        try
        {
            // var items = from m in DBContext.tblMitglieder
            //     select m;

            var items = await _localDbContext.TblMitglieders.ToListAsync();
            foreach (var objM in items)
            {
                MitgliedDetails objMitglied = new MitgliedDetails();

                objMitglied.ID = objM.Id;
                objMitglied.Anrede = objM.Anrede;
                objMitglied.Vorname = objM.Vorname;
                objMitglied.Nachname = objM.Nachname;
                objMitglied.Spitzname = objM.Spitzname;
                objMitglied.Straße = objM.Straße;
                objMitglied.PLZ = objM.Plz;
                objMitglied.Ort = objM.Ort;
                objMitglied.Geburtsdatum = objM.Geburtsdatum.Value;
                objMitglied.MitgliedSeit = objM.MitgliedSeit;
                objMitglied.PassivSeit = objM.PassivSeit;
                objMitglied.AusgeschiedenAm = objM.AusgeschiedenAm;
                objMitglied.TelefonPrivat = objM.TelefonPrivat;
                objMitglied.TelefonMobil = objM.TelefonMobil;
                objMitglied.EMail = objM.Email;
                objMitglied.Ehemaliger = objM.Ehemaliger;
                objMitglied.Notizen = objM.Notizen;
                objMitglied.Bemerkungen = objM.Bemerkungen;

                lstMitglieder.Add(objMitglied);
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "GetMitgliedDetails", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "GetMitgliedDetails", ex.ToString());
        }

        return lstMitglieder;
    }

    public async Task<List<MitgliedDetails>> GetMitlgiederlisteDruckAsync(bool bAktiv = true)
    {
        List<MitgliedDetails> lstMitglieder = new List<MitgliedDetails>();

        try
        {
            if (bAktiv)
            {
                var items = await (_localDbContext.TblMitglieders
                    .Where(w => w.PassivSeit == null && w.Ehemaliger == false)
                    .OrderBy(o => o.Nachname).ThenBy(o => o.Vorname)
                    .Select(s => s)).ToListAsync();

                //var objM = item.First();
                foreach (var objM in items)
                {
                    MitgliedDetails objMitglied = new MitgliedDetails();
                    objMitglied.ID = objM.Id;
                    objMitglied.Anrede = objM.Anrede;
                    objMitglied.Vorname = objM.Vorname;
                    objMitglied.Nachname = objM.Nachname;
                    objMitglied.Spitzname = objM.Spitzname;
                    objMitglied.Straße = objM.Straße;
                    objMitglied.PLZ = objM.Plz;
                    objMitglied.Ort = objM.Ort;
                    objMitglied.Geburtsdatum = objM.Geburtsdatum.Value;
                    objMitglied.MitgliedSeit = objM.MitgliedSeit;
                    objMitglied.AusgeschiedenAm = objM.AusgeschiedenAm;
                    objMitglied.TelefonPrivat = objM.TelefonPrivat;
                    objMitglied.TelefonMobil = objM.TelefonMobil;
                    objMitglied.EMail = objM.Email;
                    objMitglied.Ehemaliger = objM.Ehemaliger;

                    lstMitglieder.Add(objMitglied);
                }
            }
            else
            {
                var items = await (_localDbContext.TblMitglieders
                    .OrderBy(o => o.Nachname).ThenBy(o => o.Vorname)
                    .Select(s => s)).ToListAsync();

                //var objM = item.First();
                foreach (var objM in items)
                {
                    MitgliedDetails objMitglied = new MitgliedDetails();
                    objMitglied.ID = objM.Id;
                    objMitglied.Anrede = objM.Anrede;
                    objMitglied.Vorname = objM.Vorname;
                    objMitglied.Nachname = objM.Nachname;
                    objMitglied.Spitzname = objM.Spitzname;
                    objMitglied.Straße = objM.Straße;
                    objMitglied.PLZ = objM.Plz;
                    objMitglied.Ort = objM.Ort;
                    objMitglied.Geburtsdatum = objM.Geburtsdatum.Value;
                    objMitglied.MitgliedSeit = objM.MitgliedSeit;
                    objMitglied.AusgeschiedenAm = objM.AusgeschiedenAm;
                    objMitglied.TelefonPrivat = objM.TelefonPrivat;
                    objMitglied.TelefonMobil = objM.TelefonMobil;
                    objMitglied.EMail = objM.Email;
                    objMitglied.Ehemaliger = objM.Ehemaliger;

                    lstMitglieder.Add(objMitglied);
                }
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "GetMitlgiederlisteDruck", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "GetMitlgiederlisteDruck", ex.ToString());
        }

        return lstMitglieder;
    }

    public async Task<List<Meisterschaftsdaten>> GetMeisterschaftenAsync()
    {
        List<Meisterschaftsdaten> lstMeisterschaft = new List<Meisterschaftsdaten>();

        try
        {
            var mst = await _localDbContext.TblMeisterschaftens
                .Join(_localDbContext.TblMeisterschaftstyps, mt => mt.MeisterschaftstypId, t => t.Id,
                    (mt, t) => new
                    {
                        mt.Id, mt.Bezeichnung, mt.Beginn, mt.Ende, mt.MeisterschaftstypId, t.Meisterschaftstyp,
                        mt.Aktiv,
                        mt.Bemerkungen
                    })
                .OrderBy(o => o.Beginn)
                .ToListAsync();

            foreach (var item in mst)
            {
                Meisterschaftsdaten objMeister = new Meisterschaftsdaten();
                objMeister.ID = item.Id;
                objMeister.Bezeichnung = item.Bezeichnung;
                objMeister.Beginn = item.Beginn;
                objMeister.Ende = item.Ende;
                objMeister.MeisterschaftstypID = item.MeisterschaftstypId;
                objMeister.Meisterschaftstyp = item.Meisterschaftstyp;
                objMeister.Aktiv = item.Aktiv;
                objMeister.Bemerkungen = item.Bemerkungen;

                lstMeisterschaft.Add(objMeister);
            }

            //--------------------

            //Testdaten
            // Meisterschaftsliste objMeister = new();
            // objMeister.ID = 1;
            // objMeister.Bezeichnung = "Testmeisterschaft 2023";
            // objMeister.Beginn = new DateTime(2023, 1, 1);
            // objMeister.Ende = new DateTime(2023, 12, 31);
            // objMeister.MeisterschaftstypID = 3;
            // objMeister.Meisterschaftstyp = "Kombimeisterschaft";
            // objMeister.Aktiv = 0;
            // objMeister.Bemerkungen = "";
            // lstMeisterschaft.Add(objMeister);
            //
            // objMeister = new();
            // objMeister.ID = 2;
            // objMeister.Bezeichnung = "Jahreswechseltunier 2024";
            // objMeister.Beginn = new DateTime(2024, 1, 3);
            // objMeister.Ende = new DateTime(2024, 1, 3);
            // objMeister.MeisterschaftstypID = 1;
            // objMeister.Meisterschaftstyp = "Kurztunier";
            // objMeister.Aktiv = 0;
            // objMeister.Bemerkungen = "";
            // lstMeisterschaft.Add(objMeister);
            //
            // objMeister = new();
            // objMeister.ID = 2;
            // objMeister.Bezeichnung = "Testmeisterschaft 2024";
            // objMeister.Beginn = new DateTime(2024, 1, 4);
            // objMeister.Ende = new DateTime(2024, 12, 31);
            // objMeister.MeisterschaftstypID = 2;
            // objMeister.Meisterschaftstyp = "Meisterschaft";
            // objMeister.Aktiv = 1;
            // objMeister.Bemerkungen = "";
            // lstMeisterschaft.Add(objMeister);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetMeisterschaften", ex.ToString());
        }

        var lstMeisterschaftSort = lstMeisterschaft.OrderByDescending(o => o.ID).ToList();
        return lstMeisterschaftSort;
    }

    public async Task<List<Meisterschaftstyp>> GetMeisterschaftstypenAsync()
    {
        List<Meisterschaftstyp> lstTypen = new();

        try
        {
            var mt = await _localDbContext.TblMeisterschaftstyps
                .Select(s => s).ToListAsync();

            foreach (var item in mt)
            {
                Meisterschaftstyp objTyp = new Meisterschaftstyp();
                objTyp.ID = item.Id;
                objTyp.Value = item.Meisterschaftstyp;

                lstTypen.Add(objTyp);
            }

            //---------------

            //Testdaten
            // Meisterschaftstyp objTyp = new();
            // objTyp.ID = 1;
            // objTyp.Value = "Kurztunier";
            // lstTypen.Add(objTyp);
            //
            // objTyp = new();
            // objTyp.ID = 2;
            // objTyp.Value = "Meisterschaft";
            // lstTypen.Add(objTyp);
            //
            // objTyp = new();
            // objTyp.ID = 3;
            // objTyp.Value = "Kombimeisterschaft";
            // lstTypen.Add(objTyp);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetMeisterschaftstypenAsync", ex.ToString());
        }

        return lstTypen;
    }

    public async Task<Int32> GetAktiveMeisterschaftsIDAsync()
    {
        Int32 intID = -1;

        try
        {
            // var item = (from m in DBContext.tblMeisterschaften
            //     orderby m.ID descending
            //     where m.Aktiv == 1
            //     select m).FirstOrDefault();

            AktiveMeisterschaft aktiveMeisterschaft = new();
            
            var item = await _localDbContext.TblMeisterschaftens
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync(f => f.Aktiv == 1);

            if (item != null)
            {
                intID = item.Id;
                
                aktiveMeisterschaft.ID = intID;
                aktiveMeisterschaft.Bezeichnung = item.Bezeichnung;
            }
            else
            {
                // var itm = (from m in DBContext.tblMeisterschaften
                //     orderby m.ID descending
                //     select m).FirstOrDefault();

                var itm = await _localDbContext.TblMeisterschaftens
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefaultAsync();

                intID = itm.Id;
                itm.Aktiv = 1;
                
                aktiveMeisterschaft.ID = intID;
                aktiveMeisterschaft.Bezeichnung = itm.Bezeichnung;
                await _localDbContext.SaveChangesAsync();
            }

            _commonService.AktiveMeisterschaft = aktiveMeisterschaft;
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "GetAktiveMeisterschaftsID", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "GetAktiveMeisterschaftsID", ex.ToString());
        }

        return intID;
    }

    public async Task<Int32> GetLetzteMeisterschaftsIDAsync()
    {
        Int32 intID = -1;
        Int32 intAktiveID = await GetAktiveMeisterschaftsIDAsync();

        try
        {
            // var item = from m in DBContext.tblMeisterschaften
            //     orderby m.ID descending
            //     where m.ID != intAktiveID
            //     select m;

            var item = await _localDbContext.TblMeisterschaftens
                .OrderByDescending(o => o.Id)
                .Where(w => w.Id != intAktiveID)
                .ToListAsync();

            item.FirstOrDefault();
            if (item != null)
            {
                intID = item.ToList()[0].Id;
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "GetLetzteMeisterschaftsID", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "GetLetzteMeisterschaftsID", ex.ToString());
        }

        return intID;
    }

    public async Task<string> GetMeisterschaftsbezeichnungAsync(Int32 iID)
    {
        string strBezeichnung = "";

        try
        {
            if (iID == -1)
            {
                strBezeichnung = "Keine aktive Meisterschaft";
            }
            else
            {
                // var item = from m in DBContext.tblMeisterschaften
                //     where m.ID == iID
                //     select m;

                var item = await _localDbContext.TblMeisterschaftens
                    .Where(w => w.Id == iID)
                    .ToListAsync();

                item.FirstOrDefault();
                if (item == null)
                {
                    strBezeichnung = "Keine aktive Meisterschaft";
                }
                else
                {
                    strBezeichnung = item.ToList()[0].Bezeichnung;
                }
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "GetMeisterschaftsbezeichnung", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "GetMeisterschaftsbezeichnung", ex.ToString());
        }

        return strBezeichnung;
    }

    public async Task<List<AktiveSpieler>> GetAktiveMitgliederAsync()
    {
        List<AktiveSpieler> lstAktiveMitglieder = new();

        try
        {
            var items = await (_localDbContext.TblMitglieders
                .OrderBy(o => o.Nachname).ThenBy(t => t.Vorname)
                .Where(w => w.PassivSeit == null)
                .Select(s => s)).ToListAsync();

            foreach (var objM in items)
            {
                AktiveSpieler objMitglied = new AktiveSpieler();

                objMitglied.ID = objM.Id;
                objMitglied.Vorname = objM.Vorname;
                objMitglied.Nachname = objM.Nachname;
                objMitglied.Spitzname = objM.Spitzname;
                if (!string.IsNullOrEmpty(objM.Spitzname))
                {
                    objMitglied.Anzeigename = objM.Spitzname;
                }
                else
                {
                    objMitglied.Anzeigename = objM.Vorname;
                }

                lstAktiveMitglieder.Add(objMitglied);
            }

            //----------------------

            //Testdaten
            // AktiveSpieler objItem = new();
            // objItem.Anzeigename = "Schröer, Thorsten";
            // objItem.Nachname = "Schröer";
            // objItem.Vorname = "Thorsten";
            // objItem.Spitzname = "Thor";
            // objItem.ID = 1;
            // lstAktiveMitglieder.Add(objItem);
            //
            // objItem = new();
            // objItem.Anzeigename = "Schmidt, Wolfgang";
            // objItem.Nachname = "Schmidt";
            // objItem.Vorname = "Wolfgang";
            // objItem.ID = 2;
            // lstAktiveMitglieder.Add(objItem);
            //
            // objItem = new();
            // objItem.Anzeigename = "Bohn, Karl-Heinz";
            // objItem.Nachname = "Bohn";
            // objItem.Vorname = "Karl-Heinz";
            // objItem.Spitzname = "Kalle";
            // objItem.ID = 3;
            // lstAktiveMitglieder.Add(objItem);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetAktiveMitgliederAsync", ex.ToString());
        }

        return lstAktiveMitglieder;
    }

    public async Task<List<AktiveSpieler>> GetAktiveMitgliederAsync(Int32 iMeisterschaftsID)
    {
        List<AktiveSpieler> lstAktiveMitglieder = new List<AktiveSpieler>();

        try
        {
            var aktiveTeilnemer = await _localDbContext.TblTeilnehmers
                .Where(w => w.MeisterschaftsId == iMeisterschaftsID)
                .Select(s => s).ToListAsync();

            var items = _localDbContext.TblMitglieders
                .Where(w => w.PassivSeit == null) // Zuerst nur diesen Teil in SQL filtern
                .AsEnumerable() // Danach wird die restliche Filterung in Speicher durchgeführt
                .Where(w => aktiveTeilnemer.All(w2 => w2.SpielerId != w.Id))
                .OrderBy(o => o.Nachname).ThenBy(t => t.Vorname)
                .ToList();

            foreach (var objM in items)
            {
                AktiveSpieler objMitglied = new AktiveSpieler();

                objMitglied.ID = objM.Id;
                objMitglied.Vorname = objM.Vorname;
                objMitglied.Nachname = objM.Nachname;
                objMitglied.Spitzname = objM.Spitzname;
                if (!string.IsNullOrEmpty(objM.Spitzname))
                {
                    objMitglied.Anzeigename = objM.Spitzname;
                }
                else
                {
                    objMitglied.Anzeigename = objM.Vorname;
                }

                lstAktiveMitglieder.Add(objMitglied);
            }

            //----------------------

            //Testdaten
            // AktiveSpieler objItem = new();
            // objItem.Anzeigename = "Schröer, Thorsten";
            // objItem.Nachname = "Schröer";
            // objItem.Vorname = "Thorsten";
            // objItem.Spitzname = "Thor";
            // objItem.ID = 1;
            // lstAktiveMitglieder.Add(objItem);
            //
            // objItem = new();
            // objItem.Anzeigename = "Schmidt, Wolfgang";
            // objItem.Nachname = "Schmidt";
            // objItem.Vorname = "Wolfgang";
            // objItem.ID = 2;
            // lstAktiveMitglieder.Add(objItem);
            //
            // objItem = new();
            // objItem.Anzeigename = "Bohn, Karl-Heinz";
            // objItem.Nachname = "Bohn";
            // objItem.Vorname = "Karl-Heinz";
            // objItem.Spitzname = "Kalle";
            // objItem.ID = 3;
            // lstAktiveMitglieder.Add(objItem);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetAktiveMitgliederAsync(MeisterschaftID)", ex.ToString());
        }

        return lstAktiveMitglieder;
    }

    public async Task<List<AktiveSpieler>> GetAktiveTeilnehmerAsync(Int32 iMeisterschaftsID)
    {
        List<AktiveSpieler> lstAktiveMitglieder = new();

        try
        {
            var items = await (_localDbContext.TblTeilnehmers
                .Join(_localDbContext.TblMitglieders, tln => tln.SpielerId, mgl => mgl.Id,
                    (t, m) => new { t.MeisterschaftsId, m.Id, m.Vorname, m.Nachname, m.Spitzname })
                .Where(w => w.MeisterschaftsId == iMeisterschaftsID)
                .Select(s => s)).ToListAsync();

            foreach (var objM in items)
            {
                AktiveSpieler objMitglied = new AktiveSpieler();

                objMitglied.ID = objM.Id;
                objMitglied.Vorname = objM.Vorname;
                objMitglied.Nachname = objM.Nachname;
                objMitglied.Spitzname = objM.Spitzname;
                if (!string.IsNullOrEmpty(objM.Spitzname))
                {
                    objMitglied.Anzeigename = objM.Spitzname;
                }
                else
                {
                    objMitglied.Anzeigename = objM.Vorname;
                }

                lstAktiveMitglieder.Add(objMitglied);
            }

            //----------------------

            //Testdaten
            // AktiveSpieler objItem = new();
            // objItem.Anzeigename = "Schröer, Thorsten";
            // objItem.Nachname = "Schröer";
            // objItem.Vorname = "Thorsten";
            // objItem.Spitzname = "Thor";
            // objItem.ID = 1;
            // lstAktiveMitglieder.Add(objItem);
            //
            // objItem = new();
            // objItem.Anzeigename = "Schmidt, Wolfgang";
            // objItem.Nachname = "Schmidt";
            // objItem.Vorname = "Wolfgang";
            // objItem.ID = 2;
            // lstAktiveMitglieder.Add(objItem);
            //
            // objItem = new();
            // objItem.Anzeigename = "Bohn, Karl-Heinz";
            // objItem.Nachname = "Bohn";
            // objItem.Vorname = "Karl-Heinz";
            // objItem.Spitzname = "Kalle";
            // objItem.ID = 3;
            // lstAktiveMitglieder.Add(objItem);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetAktiveTeilnehmerAsync", ex.ToString());
        }

        return lstAktiveMitglieder;
    }


    /*
    public List<Cls9erRatten> Get9erRatten(Int32 iSpieltagID)
    {
        List<Cls9erRatten> lst9erRatten = new List<Cls9erRatten>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                //var items = (DBContext.tblSpieltag
                //    .Join(DBContext.tblMapSpieltag2Spiele, t => t.ID, map => map.SpieltagID, (t, map) => new { t.ID, t.MeisterschaftsID, t.Spieltag, map.SpieltagID, map.C9erRattenID })
                //    .Join(DBContext.tbl9erRatten, j => j.C9erRattenID, nr => nr.ID, (j, nr) => new { j.SpieltagID, j.C9erRattenID, nr.SpielerID, nr.Neuner, nr.Ratten })
                //    .Join(DBContext.tblMitglieder, j => j.SpielerID, m => m.ID, (j, m) => new { j.SpieltagID, j.C9erRattenID, j.SpielerID, m.Vorname, m.Spitzname, j.Neuner, j.Ratten })
                //    .Where(w => w.SpieltagID == iSpieltagID)
                //    .Select(s => s));

                var items = (DBContext.vw9erRatten
                    .Where(w => w.SpieltagID == iSpieltagID)
                    .Select(s => s));

                foreach (var objM in items)
                {
                    Cls9erRatten objNR = new Cls9erRatten();
                    objNR.ID = objM.C9erRattenID;
                    objNR.SpieltagID = iSpieltagID;
                    objNR.SpielerID = objM.SpielerID;
                    if (!string.IsNullOrEmpty(objM.Spitzname))
                    {
                        objNR.Spielername = objM.Spitzname;
                    }
                    else
                    {
                        objNR.Spielername = objM.Vorname;
                    }
                    objNR.Neuner = objM.Neuner;
                    objNR.Ratten = objM.Ratten;

                    lst9erRatten.Add(objNR);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "Get9erRatten", ex.ToString());
            objForm.ShowDialog();
        }
        return lst9erRatten;
    }
    */

    /*
    public List<ClsSpiel6TageRennen> GetSpiel6TageRennen(Int32 iSpieltagID)
    {
        List<ClsSpiel6TageRennen> lst6TageRennen = new List<ClsSpiel6TageRennen>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwSpiel6TageRennen
                    .Where(w => w.SpieltagID == iSpieltagID)
                    .Select(s => s));

                foreach (var objM in items)
                {
                    ClsSpiel6TageRennen obj6TR = new ClsSpiel6TageRennen();
                    obj6TR.ID = objM.C6TageRennenID;
                    obj6TR.SpieltagID = objM.SpieltagID;
                    obj6TR.Spieler1ID = objM.SpielerID1;
                    if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                    {
                        obj6TR.Spieler1Name = objM.Spieler1Spitzname;
                    }
                    else
                    {
                        obj6TR.Spieler1Name = objM.Spieler1Vorname;
                    }
                    obj6TR.Spieler2ID = objM.SpielerID2;
                    if (!string.IsNullOrEmpty(objM.Spieler2Spitzname))
                    {
                        obj6TR.Spieler2Name = objM.Spieler2Spitzname;
                    }
                    else
                    {
                        obj6TR.Spieler2Name = objM.Spieler2Vorname;
                    }
                    obj6TR.Runden = objM.Runden;
                    obj6TR.Punkte = objM.Punkte;
                    obj6TR.Spielnummer = objM.Spielnummer.Value;
                    obj6TR.Platz = objM.Platz.Value;

                    lst6TageRennen.Add(obj6TR);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetSpiel6TageRennen", ex.ToString());
            objForm.ShowDialog();
        }
        return lst6TageRennen;
    }
    */

    /*
    public List<ClsSpielBlitztunier> GetSpielBlitztunier(Int32 iSpieltagID)
    {
        List<ClsSpielBlitztunier> lstBlitz = new List<ClsSpielBlitztunier>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwSpielBlitztunier
                    .Where(w => w.SpieltagID == iSpieltagID)
                    .Select(s => s));

                foreach (var objM in items)
                {
                    ClsSpielBlitztunier objBlitz = new ClsSpielBlitztunier();
                    objBlitz.ID = objM.BlitztunierID;
                    objBlitz.SpieltagID = objM.SpieltagID;
                    objBlitz.Spieler1ID = objM.SpielerID1;
                    if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                    {
                        objBlitz.Spieler1Name = objM.Spieler1Spitzname;
                    }
                    else
                    {
                        objBlitz.Spieler1Name = objM.Spieler1Vorname;
                    }
                    objBlitz.Spieler2ID = objM.SpielerID2;
                    if (!string.IsNullOrEmpty(objM.Spieler2Spitzname))
                    {
                        objBlitz.Spieler2Name = objM.Spieler2Spitzname;
                    }
                    else
                    {
                        objBlitz.Spieler2Name = objM.Spieler2Vorname;
                    }
                    objBlitz.PunkteSpieler1 = objM.PunkteSpieler1;
                    objBlitz.PunkteSpieler2 = objM.PunkteSpieler2;

                    lstBlitz.Add(objBlitz);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetSpielBlitztunier", ex.ToString());
            objForm.ShowDialog();
        }
        return lstBlitz;
    }
    */

    /*
    public List<ClsSpielKombimeisterschaft> GetSpielKombimeisterschaft(Int32 iSpieltagID)
    {
        List<ClsSpielKombimeisterschaft> lstKombi = new List<ClsSpielKombimeisterschaft>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwSpielKombimeisterschaft
                    .Where(w => w.SpieltagID == iSpieltagID)
                    .Select(s => s));

                foreach (var objM in items)
                {
                    ClsSpielKombimeisterschaft objKombi = new ClsSpielKombimeisterschaft();
                    objKombi.ID = objM.KombimeisterschaftID;
                    objKombi.SpieltagID = objM.SpieltagID;
                    objKombi.Spieler1ID = objM.SpielerID1;
                    if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                    {
                        objKombi.Spieler1Name = objM.Spieler1Spitzname;
                    }
                    else
                    {
                        objKombi.Spieler1Name = objM.Spieler1Vorname;
                    }
                    objKombi.Spieler2ID = objM.SpielerID2;
                    if (!string.IsNullOrEmpty(objM.Spieler2Spitzname))
                    {
                        objKombi.Spieler2Name = objM.Spieler2Spitzname;
                    }
                    else
                    {
                        objKombi.Spieler2Name = objM.Spieler2Vorname;
                    }
                    objKombi.Spieler1Punkte3bis8 = objM.Spieler1Punkte3bis8;
                    objKombi.Spieler1Punkte5Kugeln = objM.Spieler1Punkte5Kugeln;
                    objKombi.Spieler2Punkte3bis8 = objM.Spieler2Punkte3bis8;
                    objKombi.Spieler2Punkte5Kugeln = objM.Spieler2Punkte5Kugeln;
                    objKombi.HinRueckrunde = objM.HinRückrunde;

                    lstKombi.Add(objKombi);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetSpielKombimeisterschaft", ex.ToString());
            objForm.ShowDialog();
        }
        return lstKombi;
    }
    */

    /*
    public List<ClsErgebnisKombimeisterschaftKreuztabelle> GetErgebnisKombimeisterschaft(Int32 iMeisterschaftsID)
    {
        List<ClsErgebnisKombimeisterschaftKreuztabelle> lstKombi = new List<ClsErgebnisKombimeisterschaftKreuztabelle>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwErgebnisKombimeisterschaft
                    .Where(w => w.MeisterschaftsID == iMeisterschaftsID)
                    .Select(s => s));

                foreach (var objM in items.ToList())
                {
                    ClsErgebnisKombimeisterschaftKreuztabelle objKombi = new ClsErgebnisKombimeisterschaftKreuztabelle();
                    objKombi.MeisterschaftsID = objM.MeisterschaftsID;
                    objKombi.Spieler1ID = objM.SpielerID1;
                    if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                    {
                        objKombi.Spieler1Name = objM.Spieler1Spitzname;
                    }
                    else
                    {
                        objKombi.Spieler1Name = objM.Spieler1Vorname;
                    }
                    objKombi.Spieler2ID = objM.SpielerID2;
                    if (!string.IsNullOrEmpty(objM.Spieler2Spitzname))
                    {
                        objKombi.Spieler2Name = objM.Spieler2Spitzname;
                    }
                    else
                    {
                        objKombi.Spieler2Name = objM.Spieler2Vorname;
                    }
                    objKombi.Spieler1Punkte3bis8 = objM.Spieler1Punkte3bis8;
                    objKombi.Spieler1Punkte5Kugeln = objM.Spieler1Punkte5Kugeln;
                    objKombi.Spieler2Punkte3bis8 = objM.Spieler2Punkte3bis8;
                    objKombi.Spieler2Punkte5Kugeln = objM.Spieler2Punkte5Kugeln; ;
                    objKombi.HinRueckrunde = objM.HinRückrunde;

                    lstKombi.Add(objKombi);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetErgebnisKombimeisterschaft", ex.ToString());
            objForm.ShowDialog();
        }
        return lstKombi;
    }
    */

    /*
    public List<ClsErgebnisKurztunierKreuztabelle> GetErgebnisKurztunier(Int32 iMeisterschaftsID)
    {
        List<ClsErgebnisKurztunierKreuztabelle> lstKurz = new List<ClsErgebnisKurztunierKreuztabelle>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwErgebnisKurztunier
                    .Where(w => w.MeisterschaftsID == iMeisterschaftsID)
                    .Select(s => s));

                foreach (var objM in items.ToList())
                {
                    ClsErgebnisKurztunierKreuztabelle objKurz = new ClsErgebnisKurztunierKreuztabelle();
                    objKurz.MeisterschaftsID = objM.MeisterschaftsID;
                    objKurz.Spieler1ID = objM.SpielerID1;
                    if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                    {
                        objKurz.Spieler1Name = objM.Spieler1Spitzname;
                    }
                    else
                    {
                        objKurz.Spieler1Name = objM.Spieler1Vorname;
                    }
                    objKurz.Spieler2ID = objM.SpielerID2;
                    if (!string.IsNullOrEmpty(objM.Spieler2Spitzname))
                    {
                        objKurz.Spieler2Name = objM.Spieler2Spitzname;
                    }
                    else
                    {
                        objKurz.Spieler2Name = objM.Spieler2Vorname;
                    }
                    objKurz.Spieler1Punkte = objM.PunkteSpieler1;
                    objKurz.Spieler2Punkte = objM.PunkteSpieler2;
                    objKurz.HinRueckrunde = objM.HinRückrunde;

                    lstKurz.Add(objKurz);
                }
            }
        }
        catch(Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetErgebnisKurztunier", ex.ToString());
            objForm.ShowDialog();
        }

        //Testdaten, falls keine echten Daten vorhanden
        if (lstKurz.Count == 0)
        {
            for(Int32 i = 0; i <= 1; i++)
            {
                for(Int32 j = 1; j <= 6; j++)
                {
                    for(Int32 k = 1;k <= 6; k++)
                    {
                        if (j != k)
                        {
                            ClsErgebnisKurztunierKreuztabelle objKurz = new ClsErgebnisKurztunierKreuztabelle();
                            objKurz.MeisterschaftsID = iMeisterschaftsID;
                            objKurz.Spieler1ID = j;
                            objKurz.Spieler1Name = "Spieler " + j.ToString();
                            objKurz.Spieler2ID = k;
                            objKurz.Spieler2Name = "Spieler " + k.ToString();
                            objKurz.Spieler1Punkte = 0;
                            objKurz.Spieler2Punkte = 0;
                            objKurz.HinRueckrunde = i;

                            lstKurz.Add(objKurz);
                        }
                    }
                }
            }
        }
        return lstKurz;
    }
    */

    /*
    public List<ClsErgebnisMeisterschaftKreuztabelle> GetErgebnisMeisterschaft(Int32 iMeisterschaftsID)
    {
        List<ClsErgebnisMeisterschaftKreuztabelle> lstMeister = new List<ClsErgebnisMeisterschaftKreuztabelle>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwErgebnisKurztunier
                    .Where(w => w.MeisterschaftsID == iMeisterschaftsID)
                    .Select(s => s));

                foreach (var objM in items.ToList())
                {
                    ClsErgebnisMeisterschaftKreuztabelle objMeister = new ClsErgebnisMeisterschaftKreuztabelle();
                    objMeister.MeisterschaftsID = objM.MeisterschaftsID;
                    objMeister.Spieler1ID = objM.SpielerID1;
                    if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                    {
                        objMeister.Spieler1Name = objM.Spieler1Spitzname;
                    }
                    else
                    {
                        objMeister.Spieler1Name = objM.Spieler1Vorname;
                    }
                    objMeister.Spieler2ID = objM.SpielerID2;
                    if (!string.IsNullOrEmpty(objM.Spieler2Spitzname))
                    {
                        objMeister.Spieler2Name = objM.Spieler2Spitzname;
                    }
                    else
                    {
                        objMeister.Spieler2Name = objM.Spieler2Vorname;
                    }
                    objMeister.Spieler1Punkte = objM.PunkteSpieler1;
                    objMeister.Spieler2Punkte = objM.PunkteSpieler2;
                    objMeister.HinRueckrunde = objM.HinRückrunde;

                    lstMeister.Add(objMeister);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetErgebnisMeisterschaft", ex.ToString());
            objForm.ShowDialog();
        }

        //Testdaten, falls keine echten Daten vorhanden
        if (lstMeister.Count == 0)
        {
            for (Int32 i = 0; i <= 1; i++)
            {
                for (Int32 j = 1; j <= 6; j++)
                {
                    for (Int32 k = 1; k <= 6; k++)
                    {
                        if (j != k)
                        {
                            ClsErgebnisMeisterschaftKreuztabelle objMeister = new ClsErgebnisMeisterschaftKreuztabelle();
                            objMeister.MeisterschaftsID = iMeisterschaftsID;
                            objMeister.Spieler1ID = j;
                            objMeister.Spieler1Name = "Spieler " + j.ToString();
                            objMeister.Spieler2ID = k;
                            objMeister.Spieler2Name = "Spieler " + k.ToString();
                            objMeister.Spieler1Punkte = 0;
                            objMeister.Spieler2Punkte = 0;
                            objMeister.HinRueckrunde = i;

                            lstMeister.Add(objMeister);
                        }
                    }
                }
            }
        }
        return lstMeister;
    }
    */

    public async Task<string> GetMeisterschaftsjahrAsync(Int32 iMeisterschaftsID)
    {
        string strJahr = string.Empty;

        try
        {
            // var mst = (from m in DBContext.tblMeisterschaften
            //     where m.ID == iMeisterschaftsID
            //     select m).FirstOrDefault();

            var mst = await _localDbContext.TblMeisterschaftens.FirstOrDefaultAsync(f => f.Id == iMeisterschaftsID);
            if (mst != null)
            {
                strJahr = mst.Beginn.Year.ToString();
                if (mst.Ende.HasValue)
                {
                    if (mst.Beginn.Year != mst.Ende.Value.Year)
                    {
                        strJahr += "/" + mst.Ende.Value.Year.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "GetMeisterschaftsjahr", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "GetMeisterschaftsjahr", ex.ToString());
        }

        return strJahr;
    }


    /*
    public List<ClsSpielMeisterschaft> GetSpielMeisterschaft(Int32 iSpieltagID)
    {
        List<ClsSpielMeisterschaft> lstMeisterschaft = new List<ClsSpielMeisterschaft>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwSpielMeisterschaft
                    .Where(w => w.SpieltagID == iSpieltagID)
                    .Select(s => s));

                foreach (var objM in items)
                {
                    ClsSpielMeisterschaft objMeisterschaft = new ClsSpielMeisterschaft();
                    objMeisterschaft.ID = objM.SpielMeisterschaftID;
                    objMeisterschaft.SpieltagID = objM.SpieltagID;
                    objMeisterschaft.Spieler1ID = objM.SpielerID1;
                    if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                    {
                        objMeisterschaft.Spieler1Name = objM.Spieler1Spitzname;
                    }
                    else
                    {
                        objMeisterschaft.Spieler1Name = objM.Spieler1Vorname;
                    }
                    objMeisterschaft.Spieler2ID = objM.SpielerID2;
                    if (!string.IsNullOrEmpty(objM.Spieler2Spitzname))
                    {
                        objMeisterschaft.Spieler2Name = objM.Spieler2Spitzname;
                    }
                    else
                    {
                        objMeisterschaft.Spieler2Name = objM.Spieler2Vorname;
                    }
                    objMeisterschaft.HolzSpieler1 = objM.HolzSpieler1;
                    objMeisterschaft.HolzSpieler2 = objM.HolzSpieler2;

                    lstMeisterschaft.Add(objMeisterschaft);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetSpielMeisterschaft", ex.ToString());
            objForm.ShowDialog();
        }
        return lstMeisterschaft;
    }
    */

    /*
    public List<ClsSpielPokal> GetSpielPokal(Int32 iSpieltagID)
    {
        List<ClsSpielPokal> lstPokal = new List<ClsSpielPokal>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwSpielPokal
                    .Where(w => w.SpieltagID == iSpieltagID)
                    .Select(s => s));

                foreach (var objM in items)
                {
                    ClsSpielPokal objPokal = new ClsSpielPokal();
                    objPokal.ID = objM.SpielPokalID;
                    objPokal.SpieltagID = objM.SpieltagID;
                    objPokal.SpielerID = objM.SpielerID;
                    if (!string.IsNullOrEmpty(objM.Spitzname))
                    {
                        objPokal.Spielername = objM.Spitzname;
                    }
                    else
                    {
                        objPokal.Spielername = objM.Vorname;
                    }
                    objPokal.Platzierung = objM.Platzierung;

                    lstPokal.Add(objPokal);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetSpielPokal", ex.ToString());
            objForm.ShowDialog();
        }
        return lstPokal;
    }
    */

    /*
    public List<ClsSpielSargKegeln> GetSpielSargKegeln(Int32 iSpieltagID)
    {
        List<ClsSpielSargKegeln> lstSargKegeln = new List<ClsSpielSargKegeln>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var items = (DBContext.vwSpielSargKegeln
                    .Where(w => w.SpieltagID == iSpieltagID)
                    .Select(s => s));

                foreach (var objM in items)
                {
                    ClsSpielSargKegeln objSarg = new ClsSpielSargKegeln();
                    objSarg.ID = objM.SpielSargKegelnID;
                    objSarg.SpieltagID = objM.SpieltagID;
                    objSarg.SpielerID = objM.SpielerID;
                    if (!string.IsNullOrEmpty(objM.Spitzname))
                    {
                        objSarg.Spielername = objM.Spitzname;
                    }
                    else
                    {
                        objSarg.Spielername = objM.Vorname;
                    }
                    objSarg.Platzierung = objM.Platzierung;

                    lstSargKegeln.Add(objSarg);
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetSpielSargKegeln", ex.ToString());
            objForm.ShowDialog();
        }
        return lstSargKegeln;
    }
    */

    /*
    public List<ClsStatistik9erRatten> GetStatistik9erRatten(Int32 iZeitbereich, out Dictionary<string, int> oNeunerkönig, out Dictionary<string, int> oRattenkönig, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistik9erRatten> lst9erRatten = new List<ClsStatistik9erRatten>();
        List<ClsStatistik9erRatten> lst9erRattenSortiert = new List<ClsStatistik9erRatten>();
        Dictionary<string, int> dicNeunerkönig = new Dictionary<string, int>();
        Dictionary<string, int> dicRattenkönig = new Dictionary<string, int>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var list9er = (DBContext.vwStatistik9er
                    .OrderBy(o => o.Spieltag)
                    .ThenByDescending(t => t.Neuner)
                    .Select(s => s));

                var listRatten = (DBContext.vwStatistikRatten
                    .OrderBy(o => o.Spieltag)
                    .ThenByDescending(t => t.Ratten)
                    .Select(s => s));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        list9er = list9er.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        listRatten = listRatten.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        list9er = list9er.Where(w => w.MeisterschaftsID == intID);
                        listRatten = listRatten.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        list9er = list9er.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        listRatten = listRatten.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                Int32 intIndex;
                //erst 9er
                foreach (var item in list9er.ToList())
                {
                    intIndex = lst9erRatten.FindIndex(f => f.Spieltag == item.Spieltag);
                    if (intIndex == -1)
                    {
                        ClsStatistik9erRatten objNR = new ClsStatistik9erRatten();
                        objNR.MeisterschaftsID = item.MeisterschaftsID;
                        objNR.Bezeichnung = item.Bezeichnung;
                        objNR.Beginn = item.Beginn;
                        objNR.Ende = item.Ende;
                        objNR.Spieltag = item.Spieltag;
                        objNR.Neuner = item.Neuner;
                        objNR.Neunerkönig = item.Nachname + ", " + item.Vorname;
                        lst9erRatten.Add(objNR);

                        if (!dicNeunerkönig.ContainsKey(objNR.Neunerkönig))
                        {
                            dicNeunerkönig.Add(objNR.Neunerkönig, 1);
                        }
                        else
                        {
                            dicNeunerkönig[objNR.Neunerkönig]++;
                        }
                    }
                    else
                    {
                        if (lst9erRatten[intIndex].Neuner == item.Neuner)
                        {
                            string strName = item.Nachname + ", " + item.Vorname;
                            lst9erRatten[intIndex].Neunerkönig += "\n" + strName;

                            if (!dicNeunerkönig.ContainsKey(strName))
                            {
                                dicNeunerkönig.Add(strName, 1);
                            }
                            else
                            {
                                dicNeunerkönig[strName]++;
                            }
                        }
                    }
                }

                //jetzt die Ratten
                foreach (var item in listRatten.ToList())
                {
                    intIndex = lst9erRatten.FindIndex(f => f.Spieltag == item.Spieltag);
                    if (intIndex == -1)
                    {
                        if (item.Ratten > 0)
                        {
                            ClsStatistik9erRatten objNR = new ClsStatistik9erRatten();
                            objNR.MeisterschaftsID = item.MeisterschaftsID;
                            objNR.Bezeichnung = item.Bezeichnung;
                            objNR.Beginn = item.Beginn;
                            objNR.Ende = item.Ende;
                            objNR.Spieltag = item.Spieltag;
                            objNR.Ratten = item.Ratten;
                            objNR.Rattenorden = item.Nachname + ", " + item.Vorname;
                            lst9erRatten.Add(objNR);

                            if (!dicRattenkönig.ContainsKey(objNR.Rattenorden))
                            {
                                dicRattenkönig.Add(objNR.Rattenorden, 1);
                            }
                            else
                            {
                                dicRattenkönig[objNR.Rattenorden]++;
                            }
                        }
                    }
                    else
                    {
                        if (item.Ratten > 0)
                        {
                            if (lst9erRatten[intIndex].Ratten == 0)
                            {
                                lst9erRatten[intIndex].Ratten = item.Ratten;
                                string strName = item.Nachname + ", " + item.Vorname;
                                lst9erRatten[intIndex].Rattenorden += strName;

                                if (!dicRattenkönig.ContainsKey(strName))
                                {
                                    dicRattenkönig.Add(strName, 1);
                                }
                                else
                                {
                                    dicRattenkönig[strName]++;
                                }
                            }
                            else
                            {
                                if (lst9erRatten[intIndex].Ratten == item.Ratten)
                                {
                                    lst9erRatten[intIndex].Ratten = item.Ratten;
                                    string strName = item.Nachname + ", " + item.Vorname;
                                    lst9erRatten[intIndex].Rattenorden += "\n" + strName;

                                    if (!dicRattenkönig.ContainsKey(strName))
                                    {
                                        dicRattenkönig.Add(strName, 1);
                                    }
                                    else
                                    {
                                        dicRattenkönig[strName]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            lst9erRattenSortiert = lst9erRatten.OrderBy(o => o.Spieltag).ToList();
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistik9erRatten", ex.ToString());
            objForm.ShowDialog();
        }

        oNeunerkönig = dicNeunerkönig;
        oRattenkönig = dicRattenkönig;

        return lst9erRattenSortiert;
    }
    */

    /*
    public List<ClsStatistik9er> GetStatistik9er(Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistik9er> lst9er = new List<ClsStatistik9er>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var list9er = (DBContext.vwStatistik9er
                    .OrderBy(o => o.Spieltag)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Neuner, s.Nachname, s.Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        list9er = list9er.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        list9er = list9er.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        list9er = list9er.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                var lst = from l in list9er
                          group l by new
                          {
                              l.MeisterschaftsID,
                              l.Spieltag,
                              l.Nachname,
                              l.Vorname
                          } into g
                          select new
                          {
                              MeisterschaftsID = g.Key.MeisterschaftsID,
                              Spieltag = g.Key.Spieltag,
                              Neuner = g.Sum(s => s.Neuner),
                              Nachname = g.Key.Nachname,
                              Vorname = g.Key.Vorname
                          };

                //var debug = lst.ToList();

                Int32 intIndex = -1;
                string strSpieler;
                foreach (var item in lst.ToList())
                {
                    strSpieler = item.Nachname + ", " + item.Vorname;
                    intIndex = lst9er.FindIndex(f => f.Spieler == strSpieler);
                    if (intIndex == -1)
                    {
                        ClsStatistik9er obj9er = new ClsStatistik9er();
                        obj9er.Spieler = strSpieler;
                        obj9er.Gesamt += item.Neuner;
                        obj9er.AnzTeilnahmen++;

                        switch (item.Neuner)
                        {
                            case 10:
                                obj9er.Zehn++;
                                break;
                            case 9:
                                obj9er.Neun++;
                                break;
                            case 8:
                                obj9er.Acht++;
                                break;
                            case 7:
                                obj9er.Sieben++;
                                break;
                            case 6:
                                obj9er.Sechs++;
                                break;
                            case 5:
                                obj9er.Fünf++;
                                break;
                            case 4:
                                obj9er.Vier++;
                                break;
                            case 3:
                                obj9er.Drei++;
                                break;
                            case 2:
                                obj9er.Zwei++;
                                break;
                            case 1:
                                obj9er.Eins++;
                                break;
                        }

                        lst9er.Add(obj9er);
                    }
                    else
                    {
                        lst9er[intIndex].Gesamt += item.Neuner;
                        lst9er[intIndex].AnzTeilnahmen++;

                        switch (item.Neuner)
                        {
                            case 10:
                                lst9er[intIndex].Zehn++;
                                break;
                            case 9:
                                lst9er[intIndex].Neun++;
                                break;
                            case 8:
                                lst9er[intIndex].Acht++;
                                break;
                            case 7:
                                lst9er[intIndex].Sieben++;
                                break;
                            case 6:
                                lst9er[intIndex].Sechs++;
                                break;
                            case 5:
                                lst9er[intIndex].Fünf++;
                                break;
                            case 4:
                                lst9er[intIndex].Vier++;
                                break;
                            case 3:
                                lst9er[intIndex].Drei++;
                                break;
                            case 2:
                                lst9er[intIndex].Zwei++;
                                break;
                            case 1:
                                lst9er[intIndex].Eins++;
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistik9er", ex.ToString());
            objForm.ShowDialog();
        }

        var lst9erSort = lst9er
            .OrderByDescending(o => o.Gesamt)
            .ToList();

        return lst9erSort;
    }
    */

    /*
    public List<ClsStatistikRatten> GetStatistikRatten(Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistikRatten> lstRatten = new List<ClsStatistikRatten>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var listRatten = (DBContext.vwStatistikRatten
                    .OrderBy(o => o.Spieltag)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Ratten, s.Nachname, s.Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        listRatten = listRatten.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        listRatten = listRatten.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        listRatten = listRatten.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                var lst = from l in listRatten
                          group l by new
                          {
                              l.MeisterschaftsID,
                              l.Spieltag,
                              l.Nachname,
                              l.Vorname
                          } into g
                          select new
                          {
                              MeisterschaftsID = g.Key.MeisterschaftsID,
                              Spieltag = g.Key.Spieltag,
                              Ratten = g.Sum(s => s.Ratten),
                              Nachname = g.Key.Nachname,
                              Vorname = g.Key.Vorname
                          };

                //var debug = lst.ToList();

                Int32 intIndex = -1;
                string strSpieler;
                foreach (var item in lst.ToList())
                {
                    strSpieler = item.Nachname + ", " + item.Vorname;
                    intIndex = lstRatten.FindIndex(f => f.Spieler == strSpieler);
                    if (intIndex == -1)
                    {
                        ClsStatistikRatten objRatten = new ClsStatistikRatten();
                        objRatten.Spieler = strSpieler;
                        objRatten.Gesamt += item.Ratten;
                        objRatten.AnzTeilnahmen++;

                        switch (item.Ratten)
                        {
                            case 10:
                                objRatten.Zehn++;
                                break;
                            case 9:
                                objRatten.Neun++;
                                break;
                            case 8:
                                objRatten.Acht++;
                                break;
                            case 7:
                                objRatten.Sieben++;
                                break;
                            case 6:
                                objRatten.Sechs++;
                                break;
                            case 5:
                                objRatten.Fünf++;
                                break;
                            case 4:
                                objRatten.Vier++;
                                break;
                            case 3:
                                objRatten.Drei++;
                                break;
                            case 2:
                                objRatten.Zwei++;
                                break;
                            case 1:
                                objRatten.Eins++;
                                break;
                        }

                        lstRatten.Add(objRatten);
                    }
                    else
                    {
                        lstRatten[intIndex].Gesamt += item.Ratten;
                        lstRatten[intIndex].AnzTeilnahmen++;

                        switch (item.Ratten)
                        {
                            case 10:
                                lstRatten[intIndex].Zehn++;
                                break;
                            case 9:
                                lstRatten[intIndex].Neun++;
                                break;
                            case 8:
                                lstRatten[intIndex].Acht++;
                                break;
                            case 7:
                                lstRatten[intIndex].Sieben++;
                                break;
                            case 6:
                                lstRatten[intIndex].Sechs++;
                                break;
                            case 5:
                                lstRatten[intIndex].Fünf++;
                                break;
                            case 4:
                                lstRatten[intIndex].Vier++;
                                break;
                            case 3:
                                lstRatten[intIndex].Drei++;
                                break;
                            case 2:
                                lstRatten[intIndex].Zwei++;
                                break;
                            case 1:
                                lstRatten[intIndex].Eins++;
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistikRatten", ex.ToString());
            objForm.ShowDialog();
        }

        var lstRattenSort = lstRatten
            .OrderByDescending(o => o.Gesamt)
            .ToList();

        return lstRattenSort;
    }
    */

    /*
    public List<ClsStatistikPokal> GetStatistikPokal(Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistikPokal> lstPokal = new List<ClsStatistikPokal>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var listPokal = (DBContext.vwStatistikPokal
                    .OrderBy(o => o.Spieltag)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Platzierung, s.Nachname, s.Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        listPokal = listPokal.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        listPokal = listPokal.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        listPokal = listPokal.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                //var debug = lst.ToList();

                Int32 intIndex = -1;
                string strSpieler;
                foreach (var item in listPokal.ToList())
                {
                    strSpieler = item.Nachname + ", " + item.Vorname;
                    intIndex = lstPokal.FindIndex(f => f.Spieler == strSpieler);
                    if (intIndex == -1)
                    {
                        ClsStatistikPokal objPokal = new ClsStatistikPokal();
                        objPokal.Spieler = strSpieler;
                        switch (item.Platzierung)
                        {
                            case 1:
                                objPokal.Eins++;
                                break;
                            case 2:
                                objPokal.Zwei++;
                                break;
                        }

                        lstPokal.Add(objPokal);
                    }
                    else
                    {
                        switch (item.Platzierung)
                        {
                            case 1:
                                lstPokal[intIndex].Eins++;
                                break;
                            case 2:
                                lstPokal[intIndex].Zwei++;
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistikPokal", ex.ToString());
            objForm.ShowDialog();
        }

        var lstPokalSort = lstPokal
            .OrderByDescending(o => o.Eins)
            .ThenByDescending(t => t.Zwei)
            .ToList();

        return lstPokalSort;
    }
    */

    /*
    public List<ClsStatistikSarg> GetStatistikSarg(Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistikSarg> lstSarg = new List<ClsStatistikSarg>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var listSarg = (DBContext.vwStatistikSarg
                    .OrderBy(o => o.Spieltag)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Platzierung, s.Nachname, s.Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        listSarg = listSarg.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        listSarg = listSarg.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        listSarg = listSarg.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                //var debug = lst.ToList();

                Int32 intIndex = -1;
                string strSpieler;
                foreach (var item in listSarg.ToList())
                {
                    strSpieler = item.Nachname + ", " + item.Vorname;
                    intIndex = lstSarg.FindIndex(f => f.Spieler == strSpieler);
                    if (intIndex == -1)
                    {
                        ClsStatistikSarg objSarg = new ClsStatistikSarg();
                        objSarg.Spieler = strSpieler;
                        objSarg.AnzTeilnahmen++;
                        switch (item.Platzierung)
                        {
                            case 1:
                                objSarg.Eins++;
                                break;
                            case 2:
                                objSarg.Zwei++;
                                break;
                            case 3:
                                objSarg.Drei++;
                                break;
                            case 4:
                                objSarg.Vier++;
                                break;
                            case 5:
                                objSarg.Fünf++;
                                break;
                            case 6:
                                objSarg.Sechs++;
                                break;
                            case 7:
                                objSarg.Sieben++;
                                break;
                            case 8:
                                objSarg.Acht++;
                                break;
                            case 9:
                                objSarg.Neun++;
                                break;
                            case 10:
                                objSarg.Zehn++;
                                break;
                        }

                        lstSarg.Add(objSarg);
                    }
                    else
                    {
                        lstSarg[intIndex].AnzTeilnahmen++;
                        switch (item.Platzierung)
                        {
                            case 1:
                                lstSarg[intIndex].Eins++;
                                break;
                            case 2:
                                lstSarg[intIndex].Zwei++;
                                break;
                            case 3:
                                lstSarg[intIndex].Drei++;
                                break;
                            case 4:
                                lstSarg[intIndex].Vier++;
                                break;
                            case 5:
                                lstSarg[intIndex].Fünf++;
                                break;
                            case 6:
                                lstSarg[intIndex].Sechs++;
                                break;
                            case 7:
                                lstSarg[intIndex].Sieben++;
                                break;
                            case 8:
                                lstSarg[intIndex].Acht++;
                                break;
                            case 9:
                                lstSarg[intIndex].Neun++;
                                break;
                            case 10:
                                lstSarg[intIndex].Zehn++;
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistikSarg", ex.ToString());
            objForm.ShowDialog();
        }


        var lstSargSort = lstSarg
            .OrderByDescending(o => o.Eins)
            .ThenByDescending(t => t.Zwei)
            .ThenByDescending(t => t.Drei)
            .ThenByDescending(t => t.Vier)
            .ThenByDescending(t => t.Fünf)
            .ThenByDescending(t => t.Sechs)
            .ThenByDescending(t => t.Sieben)
            .ThenByDescending(t => t.Acht)
            .ThenByDescending(t => t.Neun)
            .ThenByDescending(t => t.Zehn)
            .ToList();

        return lstSargSort;
    }
    */

    /*
    public List<ClsStatistik6TageRennenPlatzierung> GetStatistik6TageRennenPlatz(Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistik6TageRennenPlatzierung> lst6TR = new List<ClsStatistik6TageRennenPlatzierung>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                //Teil 1
                var list6TRS1 = (DBContext.vwStatistik6TageRennenSpieler1
                    .OrderBy(o => o.Spieltag)
                    .ThenBy(t => t.Spielnummer)
                    .ThenBy(t => t.Platz)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.Nachname, s.Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        list6TRS1 = list6TRS1.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        list6TRS1 = list6TRS1.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        list6TRS1 = list6TRS1.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                Int32 intIndex = -1;
                string strSpieler;
                foreach (var item in list6TRS1.ToList())
                {
                    strSpieler = item.Nachname + ", " + item.Vorname;
                    intIndex = lst6TR.FindIndex(f => f.Spieler == strSpieler);
                    if (intIndex == -1)
                    {
                        ClsStatistik6TageRennenPlatzierung obj6TR = new ClsStatistik6TageRennenPlatzierung();
                        obj6TR.Spieler = strSpieler;
                        obj6TR.AnzTeilnahmen++;

                        switch (item.Platz)
                        {
                            case 1:
                                obj6TR.Eins++;
                                break;
                            case 2:
                                obj6TR.Zwei++;
                                break;
                            case 3:
                                obj6TR.Drei++;
                                break;
                            case 4:
                                obj6TR.Vier++;
                                break;
                            case 5:
                                obj6TR.Fünf++;
                                break;
                            case 6:
                                obj6TR.Sechs++;
                                break;
                        }

                        lst6TR.Add(obj6TR);
                    }
                    else
                    {
                        lst6TR[intIndex].AnzTeilnahmen++;
                        switch (item.Platz)
                        {
                            case 1:
                                lst6TR[intIndex].Eins++;
                                break;
                            case 2:
                                lst6TR[intIndex].Zwei++;
                                break;
                            case 3:
                                lst6TR[intIndex].Drei++;
                                break;
                            case 4:
                                lst6TR[intIndex].Vier++;
                                break;
                            case 5:
                                lst6TR[intIndex].Fünf++;
                                break;
                            case 6:
                                lst6TR[intIndex].Sechs++;
                                break;
                        }
                    }
                }

                //Teil 2
                var list6TRS2 = (DBContext.vwStatistik6TageRennenSpieler2
                    .OrderBy(o => o.Spieltag)
                    .ThenBy(t => t.Spielnummer)
                    .ThenBy(t => t.Platz)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.Nachname, s.Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        list6TRS2 = list6TRS2.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        list6TRS2 = list6TRS2.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        list6TRS2 = list6TRS2.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                intIndex = -1;
                foreach (var item in list6TRS2.ToList())
                {
                    strSpieler = item.Nachname + ", " + item.Vorname;
                    intIndex = lst6TR.FindIndex(f => f.Spieler == strSpieler);
                    if (intIndex == -1)
                    {
                        ClsStatistik6TageRennenPlatzierung obj6TR = new ClsStatistik6TageRennenPlatzierung();
                        obj6TR.Spieler = strSpieler;
                        obj6TR.AnzTeilnahmen++;

                        switch (item.Platz)
                        {
                            case 1:
                                obj6TR.Eins++;
                                break;
                            case 2:
                                obj6TR.Zwei++;
                                break;
                            case 3:
                                obj6TR.Drei++;
                                break;
                            case 4:
                                obj6TR.Vier++;
                                break;
                            case 5:
                                obj6TR.Fünf++;
                                break;
                            case 6:
                                obj6TR.Sechs++;
                                break;
                        }

                        lst6TR.Add(obj6TR);
                    }
                    else
                    {
                        lst6TR[intIndex].AnzTeilnahmen++;
                        switch (item.Platz)
                        {
                            case 1:
                                lst6TR[intIndex].Eins++;
                                break;
                            case 2:
                                lst6TR[intIndex].Zwei++;
                                break;
                            case 3:
                                lst6TR[intIndex].Drei++;
                                break;
                            case 4:
                                lst6TR[intIndex].Vier++;
                                break;
                            case 5:
                                lst6TR[intIndex].Fünf++;
                                break;
                            case 6:
                                lst6TR[intIndex].Sechs++;
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistik6TageRennenPlatz", ex.ToString());
            objForm.ShowDialog();
        }

        var lst6TRSort = lst6TR
            .OrderByDescending(o => o.Eins)
            .ThenByDescending(t => t.Zwei)
            .ThenByDescending(t => t.Drei)
            .ThenByDescending(t => t.Vier)
            .ThenByDescending(t => t.Fünf)
            .ThenByDescending(t => t.Sechs)
            .ToList();

        return lst6TRSort;
    }
    */

    /*
    public List<ClsStatistik6TageRennenBesteMannschaft> GetStatistik6TageRennenBesteMannschaft(Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistik6TageRennenBesteMannschaft> lst6TR = new List<ClsStatistik6TageRennenBesteMannschaft> ();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var list6TR = (DBContext.vwSpiel6TageRennen
                    .OrderBy(o => o.Spieltag)
                    .ThenBy(t => t.Spielnummer)
                    .ThenBy(t => t.Platz)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.SpielerID1, s.Spieler1Nachname, s.Spieler1Vorname, s.SpielerID2, s.Spieler2Nachname, s.Spieler2Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        list6TR = list6TR.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        list6TR = list6TR.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        list6TR = list6TR.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                Int32 intIndex = -1;
                string strMannschaft;
                string strSpieler1;
                string strSpieler2;
                foreach (var item in list6TR.ToList())
                {
                    strSpieler1 = item.Spieler1Nachname + ", " + item.Spieler1Vorname;
                    strSpieler2 = item.Spieler2Nachname + ", " + item.Spieler2Vorname;
                    ClsStatistik6TageRennenBesteMannschaft obj6TR = new ClsStatistik6TageRennenBesteMannschaft();

                    if(string.Compare(item.Spieler1Nachname, item.Spieler2Nachname,true) < 0)
                    {
                        strMannschaft = strSpieler1 + " | " + strSpieler2;
                        obj6TR.Mannschaft = strMannschaft;
                        obj6TR.Spieler1ID = item.SpielerID1;
                        obj6TR.Spieler2ID = item.SpielerID2;
                    }
                    else
                    {
                        if(string.Compare(item.Spieler1Nachname, item.Spieler2Nachname, true) == 0)
                        {
                            if (string.Compare(item.Spieler1Vorname, item.Spieler2Vorname, true) < 0)
                            {
                                strMannschaft = strSpieler1 + " | " + strSpieler2;
                                obj6TR.Mannschaft = strMannschaft;
                                obj6TR.Spieler1ID = item.SpielerID1;
                                obj6TR.Spieler2ID = item.SpielerID2;
                            }
                            else
                            {
                                strMannschaft = strSpieler2 + " | " + strSpieler1;
                                obj6TR.Mannschaft = strMannschaft;
                                obj6TR.Spieler1ID = item.SpielerID2;
                                obj6TR.Spieler2ID = item.SpielerID1;
                            }
                        }
                        else
                        {
                            strMannschaft = strSpieler2 + " | " + strSpieler1;
                            obj6TR.Mannschaft = strMannschaft;
                            obj6TR.Spieler1ID = item.SpielerID2;
                            obj6TR.Spieler2ID = item.SpielerID1;
                        }
                    }

                    intIndex = lst6TR.FindIndex(f => f.Mannschaft == strMannschaft);
                    if(intIndex == -1)
                    {
                        obj6TR.Anzahl++;
                        switch (item.Platz)
                        {
                            case 1:
                                obj6TR.Eins++;
                                break;
                            case 2:
                                obj6TR.Zwei++;
                                break;
                            case 3:
                                obj6TR.Drei++;
                                break;
                            case 4:
                                obj6TR.Vier++;
                                break;
                            case 5:
                                obj6TR.Fünf++;
                                break;
                            case 6:
                                obj6TR.Sechs++;
                                break;
                        }

                        lst6TR.Add(obj6TR);
                    }
                    else
                    {
                        lst6TR[intIndex].Anzahl++;
                        switch (item.Platz)
                        {
                            case 1:
                                lst6TR[intIndex].Eins++;
                                break;
                            case 2:
                                lst6TR[intIndex].Zwei++;
                                break;
                            case 3:
                                lst6TR[intIndex].Drei++;
                                break;
                            case 4:
                                lst6TR[intIndex].Vier++;
                                break;
                            case 5:
                                lst6TR[intIndex].Fünf++;
                                break;
                            case 6:
                                lst6TR[intIndex].Sechs++;
                                break;
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistik6TageRennenBesteMannschaft", ex.ToString());
            objForm.ShowDialog();
        }

        var lst6TRSort = lst6TR
            .OrderByDescending(o => o.Eins)
            .ThenByDescending(t => t.Zwei)
            .ThenByDescending(t => t.Drei)
            .ThenByDescending(t => t.Vier)
            .ThenByDescending(t => t.Fünf)
            .ThenByDescending(t => t.Sechs)
            .ToList();

        return lst6TRSort;
    }
    */

    /*
    public Dictionary<string, List<ClsStatistik6TageRennenBesteMannschaft>> GetStatistik6TageRennenMannschaftMitglied(Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<ClsStatistik6TageRennenBesteMannschaft> lstMannschaftA = new List<ClsStatistik6TageRennenBesteMannschaft>();
        List<ClsStatistik6TageRennenBesteMannschaft> lstMannschaftB = new List<ClsStatistik6TageRennenBesteMannschaft>();
        Dictionary<string, List<ClsStatistik6TageRennenBesteMannschaft>> dictMannschaft = new Dictionary<string, List<ClsStatistik6TageRennenBesteMannschaft>>();

        try
        {
            using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
            {
                var list6TR = (DBContext.vwSpiel6TageRennen
                    .OrderBy(o => o.Spieltag)
                    .ThenBy(t => t.Spielnummer)
                    .ThenBy(t => t.Platz)
                    .Select(s => new { s.MeisterschaftsID, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.SpielerID1, s.Spieler1Nachname, s.Spieler1Vorname, s.SpielerID2, s.Spieler2Nachname, s.Spieler2Vorname }));

                switch (iZeitbereich)
                {
                    case 0: //laufende Meisterschaft
                        list6TR = list6TR.Where(w => w.MeisterschaftsID == Properties.Settings.Default.AktiveMeisterschaft);
                        break;
                    case 1: //letzte Meisterschaft
                        Int32 intID = GetLetzteMeisterschaftsID();
                        list6TR = list6TR.Where(w => w.MeisterschaftsID == intID);
                        break;
                    case 2: //Zeitbereich
                        list6TR = list6TR.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis);
                        break;
                    case 3: //Gesamt
                        break;
                }

                Int32 intIndex = -1;
                string strMannschaftA;
                string strMannschaftB;
                string strSpieler1;
                string strSpieler2;
                foreach (var item in list6TR.ToList())
                {
                    strSpieler1 = item.Spieler1Nachname + ", " + item.Spieler1Vorname;
                    strSpieler2 = item.Spieler2Nachname + ", " + item.Spieler2Vorname;

                    strMannschaftA = strSpieler1 + " | " + strSpieler2;
                    if(!dictMannschaft.ContainsKey(strSpieler1))
                    {
                        ClsStatistik6TageRennenBesteMannschaft obj6TR_M_A = new ClsStatistik6TageRennenBesteMannschaft();
                        obj6TR_M_A.Mannschaft = strMannschaftA;
                        obj6TR_M_A.Spieler1ID = item.SpielerID1;
                        obj6TR_M_A.Spieler2ID = item.SpielerID2;

                        obj6TR_M_A.Anzahl++;
                        switch (item.Platz)
                        {
                            case 1:
                                obj6TR_M_A.Eins++;
                                break;
                            case 2:
                                obj6TR_M_A.Zwei++;
                                break;
                            case 3:
                                obj6TR_M_A.Drei++;
                                break;
                            case 4:
                                obj6TR_M_A.Vier++;
                                break;
                            case 5:
                                obj6TR_M_A.Fünf++;
                                break;
                            case 6:
                                obj6TR_M_A.Sechs++;
                                break;
                        }

                        List<ClsStatistik6TageRennenBesteMannschaft> lstMannschaft = new List<ClsStatistik6TageRennenBesteMannschaft>();
                        lstMannschaft.Add(obj6TR_M_A);
                        dictMannschaft.Add(strSpieler1, lstMannschaft);
                    }
                    else
                    {
                        intIndex = dictMannschaft[strSpieler1].FindIndex(f => f.Mannschaft == strMannschaftA);
                        if (intIndex == -1)
                        {
                            ClsStatistik6TageRennenBesteMannschaft obj6TR_M_A = new ClsStatistik6TageRennenBesteMannschaft();
                            obj6TR_M_A.Mannschaft = strMannschaftA;
                            obj6TR_M_A.Spieler1ID = item.SpielerID1;
                            obj6TR_M_A.Spieler2ID = item.SpielerID2;

                            obj6TR_M_A.Anzahl++;
                            switch (item.Platz)
                            {
                                case 1:
                                    obj6TR_M_A.Eins++;
                                    break;
                                case 2:
                                    obj6TR_M_A.Zwei++;
                                    break;
                                case 3:
                                    obj6TR_M_A.Drei++;
                                    break;
                                case 4:
                                    obj6TR_M_A.Vier++;
                                    break;
                                case 5:
                                    obj6TR_M_A.Fünf++;
                                    break;
                                case 6:
                                    obj6TR_M_A.Sechs++;
                                    break;
                            }

                            dictMannschaft[strSpieler1].Add(obj6TR_M_A);
                        }
                        else
                        {
                            dictMannschaft[strSpieler1][intIndex].Anzahl++;
                            switch (item.Platz)
                            {
                                case 1:
                                    dictMannschaft[strSpieler1][intIndex].Eins++;
                                    break;
                                case 2:
                                    dictMannschaft[strSpieler1][intIndex].Zwei++;
                                    break;
                                case 3:
                                    dictMannschaft[strSpieler1][intIndex].Drei++;
                                    break;
                                case 4:
                                    dictMannschaft[strSpieler1][intIndex].Vier++;
                                    break;
                                case 5:
                                    dictMannschaft[strSpieler1][intIndex].Fünf++;
                                    break;
                                case 6:
                                    dictMannschaft[strSpieler1][intIndex].Sechs++;
                                    break;
                            }
                        }
                    }

                    if (strSpieler1 != strSpieler2)
                    {
                        strMannschaftB = strSpieler2 + " | " + strSpieler1;
                        if (!dictMannschaft.ContainsKey(strSpieler2))
                        {
                            ClsStatistik6TageRennenBesteMannschaft obj6TR_M_B = new ClsStatistik6TageRennenBesteMannschaft();
                            obj6TR_M_B.Mannschaft = strMannschaftB;
                            obj6TR_M_B.Spieler1ID = item.SpielerID2;
                            obj6TR_M_B.Spieler2ID = item.SpielerID1;

                            obj6TR_M_B.Anzahl++;
                            switch (item.Platz)
                            {
                                case 1:
                                    obj6TR_M_B.Eins++;
                                    break;
                                case 2:
                                    obj6TR_M_B.Zwei++;
                                    break;
                                case 3:
                                    obj6TR_M_B.Drei++;
                                    break;
                                case 4:
                                    obj6TR_M_B.Vier++;
                                    break;
                                case 5:
                                    obj6TR_M_B.Fünf++;
                                    break;
                                case 6:
                                    obj6TR_M_B.Sechs++;
                                    break;
                            }

                            List<ClsStatistik6TageRennenBesteMannschaft> lstMannschaft = new List<ClsStatistik6TageRennenBesteMannschaft>();
                            lstMannschaft.Add(obj6TR_M_B);
                            dictMannschaft.Add(strSpieler2, lstMannschaft);
                        }
                        else
                        {
                            intIndex = dictMannschaft[strSpieler2].FindIndex(f => f.Mannschaft == strMannschaftB);
                            if (intIndex == -1)
                            {
                                ClsStatistik6TageRennenBesteMannschaft obj6TR_M_B = new ClsStatistik6TageRennenBesteMannschaft();
                                obj6TR_M_B.Mannschaft = strMannschaftB;
                                obj6TR_M_B.Spieler1ID = item.SpielerID2;
                                obj6TR_M_B.Spieler2ID = item.SpielerID1;

                                obj6TR_M_B.Anzahl++;
                                switch (item.Platz)
                                {
                                    case 1:
                                        obj6TR_M_B.Eins++;
                                        break;
                                    case 2:
                                        obj6TR_M_B.Zwei++;
                                        break;
                                    case 3:
                                        obj6TR_M_B.Drei++;
                                        break;
                                    case 4:
                                        obj6TR_M_B.Vier++;
                                        break;
                                    case 5:
                                        obj6TR_M_B.Fünf++;
                                        break;
                                    case 6:
                                        obj6TR_M_B.Sechs++;
                                        break;
                                }

                                dictMannschaft[strSpieler2].Add(obj6TR_M_B);
                            }
                            else
                            {
                                dictMannschaft[strSpieler2][intIndex].Anzahl++;
                                switch (item.Platz)
                                {
                                    case 1:
                                        dictMannschaft[strSpieler2][intIndex].Eins++;
                                        break;
                                    case 2:
                                        dictMannschaft[strSpieler2][intIndex].Zwei++;
                                        break;
                                    case 3:
                                        dictMannschaft[strSpieler2][intIndex].Drei++;
                                        break;
                                    case 4:
                                        dictMannschaft[strSpieler2][intIndex].Vier++;
                                        break;
                                    case 5:
                                        dictMannschaft[strSpieler2][intIndex].Fünf++;
                                        break;
                                    case 6:
                                        dictMannschaft[strSpieler2][intIndex].Sechs++;
                                        break;
                                }
                            }
                        }
                    }




                    ////ClsStatistik6TageRennenBesteMannschaft obj6TR_M_A = new ClsStatistik6TageRennenBesteMannschaft();
                    //obj6TR_M_A.Mannschaft = strSpieler1 + " | " + strSpieler2;
                    //obj6TR_M_A.Spieler1ID = item.SpielerID1;
                    //obj6TR_M_A.Spieler2ID = item.SpielerID2;

                    //intIndex = lstMannschaftA.FindIndex(f => f.Mannschaft == obj6TR_M_A.Mannschaft);
                    //if (intIndex == -1)
                    //{
                    //    obj6TR_M_A.Anzahl++;
                    //    switch (item.Platz)
                    //    {
                    //        case 1:
                    //            obj6TR_M_A.Eins++;
                    //            break;
                    //        case 2:
                    //            obj6TR_M_A.Zwei++;
                    //            break;
                    //        case 3:
                    //            obj6TR_M_A.Drei++;
                    //            break;
                    //        case 4:
                    //            obj6TR_M_A.Vier++;
                    //            break;
                    //        case 5:
                    //            obj6TR_M_A.Fünf++;
                    //            break;
                    //        case 6:
                    //            obj6TR_M_A.Sechs++;
                    //            break;
                    //    }

                    //    lstMannschaftA.Add(obj6TR_M_A);
                    //    if (!dictMannschaft.ContainsKey(strSpieler1))
                    //    {
                    //        dictMannschaft.Add(strSpieler1, lstMannschaftA);
                    //    }
                    //    else
                    //    {
                    //        dictMannschaft[strSpieler1] = lstMannschaftA;
                    //    }
                    //}
                    //else
                    //{
                    //    dictMannschaft[strSpieler1][intIndex].Anzahl++;
                    //    switch (item.Platz)
                    //    {
                    //        case 1:
                    //            dictMannschaft[strSpieler1][intIndex].Eins++;
                    //            break;
                    //        case 2:
                    //            dictMannschaft[strSpieler1][intIndex].Zwei++;
                    //            break;
                    //        case 3:
                    //            dictMannschaft[strSpieler1][intIndex].Drei++;
                    //            break;
                    //        case 4:
                    //            dictMannschaft[strSpieler1][intIndex].Vier++;
                    //            break;
                    //        case 5:
                    //            dictMannschaft[strSpieler1][intIndex].Fünf++;
                    //            break;
                    //        case 6:
                    //            dictMannschaft[strSpieler1][intIndex].Sechs++;
                    //            break;
                    //    }
                    //}

                    //ClsStatistik6TageRennenBesteMannschaft obj6TR_M_B = new ClsStatistik6TageRennenBesteMannschaft();
                    //obj6TR_M_B.Mannschaft = strSpieler2 + " | " + strSpieler1;
                    //obj6TR_M_B.Spieler1ID = item.SpielerID2;
                    //obj6TR_M_B.Spieler2ID = item.SpielerID1;

                    //intIndex = lstMannschaftB.FindIndex(f => f.Mannschaft == obj6TR_M_B.Mannschaft);
                    //if (intIndex == -1)
                    //{
                    //    obj6TR_M_B.Anzahl++;
                    //    switch (item.Platz)
                    //    {
                    //        case 1:
                    //            obj6TR_M_B.Eins++;
                    //            break;
                    //        case 2:
                    //            obj6TR_M_B.Zwei++;
                    //            break;
                    //        case 3:
                    //            obj6TR_M_B.Drei++;
                    //            break;
                    //        case 4:
                    //            obj6TR_M_B.Vier++;
                    //            break;
                    //        case 5:
                    //            obj6TR_M_B.Fünf++;
                    //            break;
                    //        case 6:
                    //            obj6TR_M_B.Sechs++;
                    //            break;
                    //    }

                    //    lstMannschaftB.Add(obj6TR_M_B);
                    //    if (!dictMannschaft.ContainsKey(strSpieler2))
                    //    {
                    //        dictMannschaft.Add(strSpieler2, lstMannschaftB);
                    //    }
                    //    else
                    //    {
                    //        dictMannschaft[strSpieler2] = lstMannschaftB;
                    //    }
                    //}
                    //else
                    //{
                    //    dictMannschaft[strSpieler2][intIndex].Anzahl++;
                    //    switch (item.Platz)
                    //    {
                    //        case 1:
                    //            dictMannschaft[strSpieler2][intIndex].Eins++;
                    //            break;
                    //        case 2:
                    //            dictMannschaft[strSpieler2][intIndex].Zwei++;
                    //            break;
                    //        case 3:
                    //            dictMannschaft[strSpieler2][intIndex].Drei++;
                    //            break;
                    //        case 4:
                    //            dictMannschaft[strSpieler2][intIndex].Vier++;
                    //            break;
                    //        case 5:
                    //            dictMannschaft[strSpieler2][intIndex].Fünf++;
                    //            break;
                    //        case 6:
                    //            dictMannschaft[strSpieler2][intIndex].Sechs++;
                    //            break;
                    //    }
                    //}
                }
            }
        }
        catch(Exception ex)
        {
            FrmError objForm = new FrmError("ClsDB", "GetStatistik6TageRennenMannschaftMitglied", ex.ToString());
            objForm.ShowDialog();
        }

        Dictionary<string, List<ClsStatistik6TageRennenBesteMannschaft>> dictMannschaftTemp = new Dictionary<string, List<ClsStatistik6TageRennenBesteMannschaft>>();
        foreach (var item in dictMannschaft)
        {
            var sort = item.Value.OrderByDescending(o => o.Eins)
                .ThenByDescending(t => t.Zwei)
                .ThenByDescending(t => t.Drei)
                .ThenByDescending(t => t.Vier)
                .ThenByDescending(t => t.Fünf)
                .ThenByDescending(t => t.Sechs)
                .ToList();

            dictMannschaftTemp.Add(item.Key, sort);
        }


        var dictMannschaftSort = dictMannschaftTemp.OrderBy(o => o.Key).ToDictionary(o => o.Key, o => o.Value);

        return dictMannschaftSort;
    }
    */


    public async Task<List<StatistikSpielerErgebnisse>> GetStatistikSpielerErgebnisseAsync(Int32 iSpielerID)
    {
        List<StatistikSpielerErgebnisse> lstStat = new List<StatistikSpielerErgebnisse>();

        try
        {
            var erg = await _localDbContext.TblMeisterschaftens
                .Join(_localDbContext.TblSpieltags,
                    m => m.Id,
                    st => st.MeisterschaftsId,
                    (m, st) => new
                    {
                        m.Bezeichnung,
                        SpieltagID = st.Id,
                        st.Spieltag
                    }).ToListAsync();

            foreach (var item in erg)
            {
                StatistikSpielerErgebnisse objErg = new();
                objErg.Meisterschaft = item.Bezeichnung;
                objErg.Spieltag = item.Spieltag;

                Boolean bolFound = false;

                //Meisterschaft
                var sp = await (_localDbContext.VwSpielMeisterschafts
                    .Where(w => w.SpieltagId == item.SpieltagID &&
                                (w.SpielerId1 == iSpielerID || w.SpielerId2 == iSpielerID))
                    .FirstOrDefaultAsync());

                if (sp != null)
                {
                    bolFound = true;
                    if (sp.SpielerId1 == iSpielerID)
                    {
                        objErg.Gegenspieler = sp.Spieler2Nachname + ", " + sp.Spieler2Vorname;
                        objErg.Holz = sp.HolzSpieler1;
                    }
                    else
                    {
                        objErg.Gegenspieler = sp.Spieler1Nachname + ", " + sp.Spieler1Vorname;
                        objErg.Holz = sp.HolzSpieler2;
                    }

                    if (sp.HolzSpieler1 < sp.HolzSpieler2)
                    {
                        objErg.Ergebnis = 0;
                    }
                    else if (sp.HolzSpieler1 == sp.HolzSpieler2)
                    {
                        objErg.Ergebnis = 1;
                    }
                    else
                    {
                        objErg.Ergebnis = 2;
                    }
                }

                //6 Tage Rennen
                var str = await (_localDbContext.VwSpiel6TageRennens
                    .Where(w => w.SpieltagId == item.SpieltagID &&
                                (w.SpielerId1 == iSpielerID || w.SpielerId2 == iSpielerID))
                    .FirstOrDefaultAsync());

                if (str != null)
                {
                    bolFound = true;
                    objErg.SechsTageRennen_Runden = str.Runden;
                    objErg.SechsTageRennen_Punkte = str.Punkte;
                    objErg.SechsTageRennen_Platz = Convert.ToInt64(str.Platz);
                }

                //Sarg
                var s = await _localDbContext.VwSpielSargKegelns
                    .Where(w => w.SpieltagId == item.SpieltagID && w.SpielerId == iSpielerID)
                    .FirstOrDefaultAsync();

                if (s != null)
                {
                    bolFound = true;
                    objErg.Sarg = s.Platzierung;
                }

                //Pokal
                var p = await _localDbContext.VwSpielPokals
                    .Where(w => w.SpieltagId == item.SpieltagID && w.SpielerId == iSpielerID)
                    .FirstOrDefaultAsync();

                if (p != null)
                {
                    bolFound = true;
                    objErg.Pokal = p.Platzierung;
                }

                //9er + Ratten
                var nr = await _localDbContext.Vw9erRattens
                    .Where(w => w.SpieltagId == item.SpieltagID && w.SpielerId == iSpielerID)
                    .FirstOrDefaultAsync();

                if (nr != null)
                {
                    bolFound = true;
                    objErg.Neuner = nr.Neuner;
                    objErg.Ratten = nr.Ratten;
                }

                if (bolFound)
                {
                    lstStat.Add(objErg);
                }
            }


            //-----------------

            //Testdaten

            // StatistikSpielerErgebnisse objErg = new();
            // objErg.Meisterschaft = "Test";
            // objErg.Spieltag = DateTime.Now;
            // objErg.Gegenspieler = "Test";
            // objErg.Holz = -1;
            // objErg.Ergebnis = -1;
            //
            // objErg.SechsTageRennen_Runden = -1;
            // objErg.SechsTageRennen_Punkte = -1;
            // objErg.SechsTageRennen_Platz = -1;
            //
            // objErg.Sarg = 1;
            // objErg.Pokal = -1;
            // objErg.Neuner = -1;
            // objErg.Ratten = -1;
            //
            // lstStat.Add(objErg);
            // lstStat.Add(objErg);
            // lstStat.Add(objErg);
            // lstStat.Add(objErg);
            // lstStat.Add(objErg);
            // lstStat.Add(objErg);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistikSpielerErgebnisse", ex.ToString());
        }

        var lstStatSort = lstStat.OrderByDescending(o => o.Spieltag).ToList();

        return lstStatSort;
    }

    public async Task<List<StatistikSpieler>> GetStatistikSpielerAsync(Int32 iSpielerID)
    {
        StatistikSpieler objStat = new();

        try
        {
            //Meisterschaft

            //Holz Max
            var sp1MeisterMax = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.HolzSpieler1)
                .DefaultIfEmpty()
                .MaxAsync(m => m == null ? 0 : m);

            var sp2MeisterMax = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.HolzSpieler2)
                .DefaultIfEmpty()
                .MaxAsync(m => m == null ? 0 : m);

            objStat.HolzMeisterMax = sp1MeisterMax > sp2MeisterMax ? sp1MeisterMax : sp2MeisterMax;

            //Holz Min
            var sp1MeisterMin = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.HolzSpieler1)
                .DefaultIfEmpty()
                .MinAsync(m => m == null ? 0 : m);

            var sp2MeisterMin = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.HolzSpieler2)
                .DefaultIfEmpty()
                .MinAsync(m => m == null ? 0 : m);
            objStat.HolzMeisterMin = sp1MeisterMin < sp2MeisterMin ? sp1MeisterMin : sp2MeisterMin;

            var sp1MeisterSum = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.HolzSpieler1)
                .DefaultIfEmpty()
                .SumAsync(m => m == null ? 0 : m);

            var sp2MeisterSum = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.HolzSpieler2)
                .DefaultIfEmpty()
                .SumAsync(m => m == null ? 0 : m);

            var spc1Meister = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.SpielMeisterschaftId)
                .CountAsync();

            var spc2Meister = await _localDbContext.VwSpielMeisterschafts
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.SpielMeisterschaftId)
                .CountAsync();

            if (spc1Meister == 0 || spc2Meister == 0)
                objStat.HolzMeisterAVG = 0;
            else
                objStat.HolzMeisterAVG = (sp1MeisterSum + sp2MeisterSum) / (spc1Meister + spc2Meister);

            //Blitztunier

            //Holz Max
            var sp1BlitzMax = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.SpielerId1)
                .DefaultIfEmpty()
                .MaxAsync(m => m == null ? 0 : m);

            var sp2BlitzMax = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.SpielerId2)
                .DefaultIfEmpty()
                .MaxAsync(m => m == null ? 0 : m);

            objStat.HolzBlitzMax = sp1BlitzMax > sp2BlitzMax ? sp1BlitzMax : sp2BlitzMax;

            var sp1BlitzMin = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.SpielerId1)
                .DefaultIfEmpty()
                .MinAsync(m => m == null ? 0 : m);

            var sp2BlitzMin = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.SpielerId2)
                .DefaultIfEmpty()
                .MinAsync(m => m == null ? 0 : m);

            objStat.HolzBlitzMin = sp1BlitzMin < sp2BlitzMin ? sp1BlitzMin : sp2BlitzMin;

            var sp1BlitzSum = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.SpielerId1)
                .DefaultIfEmpty()
                .SumAsync(m => m == null ? 0 : m);

            var sp2BlitzSum = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.SpielerId2)
                .DefaultIfEmpty()
                .SumAsync(m => m == null ? 0 : m);

            var spc1Blitz = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId1 == iSpielerID)
                .Select(s => s.SpieltagId)
                .CountAsync();

            var spc2Blitz = await _localDbContext.VwSpielBlitztuniers
                .Where(w => w.SpielerId2 == iSpielerID)
                .Select(s => s.SpieltagId)
                .CountAsync();

            if (spc1Blitz == 0 || spc2Blitz == 0)
                objStat.HolzBlitzAVG = 0;
            else
                objStat.HolzBlitzAVG = (sp1BlitzSum + sp2BlitzSum) / (spc1Blitz + spc2Blitz);

            //Gesamt

            //Holz Summe
            objStat.HolzSumme = sp1MeisterSum + sp2MeisterSum + sp1BlitzSum + sp2BlitzSum;
            objStat.HolzMax = objStat.HolzMeisterMax > objStat.HolzBlitzMax
                ? objStat.HolzMeisterMax
                : objStat.HolzBlitzMax;
            objStat.HolzMin = objStat.HolzMeisterMin < objStat.HolzBlitzMin
                ? objStat.HolzMeisterMin
                : objStat.HolzBlitzMin;
            if (sp1MeisterSum == 0 || sp2MeisterSum == 0 || sp1BlitzSum == 0 || sp2BlitzSum == 0)
                objStat.HolzAVG = 0;
            else
                objStat.HolzAVG = (sp1MeisterSum + sp2MeisterSum + sp1BlitzSum + sp2BlitzSum) /
                                  (spc1Meister + spc2Meister + spc1Blitz + spc2Blitz);

            var ratten = await _localDbContext.Vw9erRattens
                .Where(w => w.SpielerId == iSpielerID)
                .Select(s => s.Ratten)
                .DefaultIfEmpty()
                .MaxAsync(m => m == null ? 0 : m);

            objStat.RattenMax = ratten;

            var neuner = await _localDbContext.Vw9erRattens
                .Where(w => w.SpielerId == iSpielerID)
                .Select(s => s.Neuner)
                .DefaultIfEmpty()
                .MaxAsync(m => m == null ? 0 : m);

            objStat.NeunerMax = neuner;

            var rattenSum = await _localDbContext.Vw9erRattens
                .Where(w => w.SpielerId == iSpielerID)
                .Select(s => s.Ratten)
                .DefaultIfEmpty()
                .SumAsync(m => m == null ? 0 : m);

            objStat.RattenSumme = rattenSum;

            var neunerSum = await _localDbContext.Vw9erRattens
                .Where(w => w.SpielerId == iSpielerID)
                .Select(s => s.Neuner)
                .DefaultIfEmpty()
                .SumAsync(m => m == null ? 0 : m);

            objStat.NeunerSumme = neunerSum;


            //----------------

            //Testdaten
            // objStat.HolzMeisterMax = -1;
            // objStat.HolzMeisterMin = -1;
            // objStat.HolzMeisterSumme = -1;
            // objStat.HolzMeisterAVG = -1.0;
            //
            // objStat.HolzBlitzMax = -1;
            // objStat.HolzBlitzMin = -1;
            // objStat.HolzBlitzSumme = -1;
            // objStat.HolzBlitzAVG = -1;
            //
            // objStat.HolzMax = -1;
            // objStat.HolzMin = -1;
            // objStat.HolzSumme = -1;
            // objStat.HolzAVG = -1;
            //
            // objStat.RattenMax = -1;
            // objStat.RattenSumme = -1;
            // objStat.NeunerMax = -1;
            // objStat.NeunerSumme = -1;
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistikSpieler", ex.ToString());
        }

        List<StatistikSpieler> objList = new();
        objList.Add(objStat);
        return objList;
    }


    // public async Task SaveMitglied(Int32 iID, string sAnrede, string sVorname, string sNachname, string sSpitzname,
    //     string sStrasse, string sPLZ, string sOrt, string sTelefonPrivat, string sTelefonMobil, DateTime dtGeburtsdatum,
    //     DateTime dtMitgliedSeit, object dtPassivSeit, object dtAusgeschiedenAm, string sEMail, bool bEhemaliger,
    //     string sNotizen, string sBemerkungen)
    public async Task SaveMitgliedAsync(MitgliedDetails currentMitglied)
    {
        string strSQL = string.Empty;
        StringBuilder sb = new StringBuilder();

        try
        {
            Models.Local.TblMitglieder objMitglied = new();
            Models.Local.TblDbchangeLog objLog = new();
            Models.Web.TblDbchangeLog objLogWeb = new();
            DateTime dtLogTimestamp = DateTime.Now;

            switch (currentMitglied.ID)
            {
                case -1:
                    objMitglied.Anrede = currentMitglied.Anrede;
                    objMitglied.Vorname = currentMitglied.Vorname;
                    objMitglied.Nachname = currentMitglied.Nachname;
                    objMitglied.Spitzname = currentMitglied.Spitzname;
                    objMitglied.Straße = currentMitglied.Straße;
                    objMitglied.Plz = currentMitglied.PLZ;
                    objMitglied.Ort = currentMitglied.Ort;
                    objMitglied.TelefonPrivat = currentMitglied.TelefonPrivat;
                    objMitglied.TelefonMobil = currentMitglied.TelefonMobil;
                    objMitglied.Geburtsdatum = currentMitglied.Geburtsdatum;
                    objMitglied.MitgliedSeit = currentMitglied.MitgliedSeit.Value;
                    if (currentMitglied.PassivSeit.HasValue)
                    {
                        objMitglied.PassivSeit = currentMitglied.PassivSeit;
                    }
                    else
                    {
                        objMitglied.PassivSeit = null;
                    }

                    if (currentMitglied.AusgeschiedenAm.HasValue)
                    {
                        objMitglied.AusgeschiedenAm = currentMitglied.AusgeschiedenAm;
                    }
                    else
                    {
                        objMitglied.AusgeschiedenAm = null;
                    }

                    objMitglied.Email = currentMitglied.EMail;
                    objMitglied.Ehemaliger = currentMitglied.Ehemaliger;
                    objMitglied.Notizen = currentMitglied.Notizen;
                    objMitglied.Bemerkungen = currentMitglied.Bemerkungen;

                    await _localDbContext.TblMitglieders.AddAsync(objMitglied);
                    await _localDbContext.SaveChangesAsync();

                    sb.Append(
                        "insert into tblMitglieder(Anrede, Vorname, Nachname, Spitzname, Straße, PLZ, Ort, TelefonPrivat, TelefonMobil, Geburtsdatum, MitgliedSeit, AusgeschiedenAm, PassivSeit, EMail, Ehemaliger, Notizen, Bemerkungen) ");
                    sb.Append("values(");
                    sb.Append("'").Append(currentMitglied.Anrede).Append("', ");
                    sb.Append("'").Append(currentMitglied.Vorname).Append("', ");
                    sb.Append("'").Append(currentMitglied.Nachname).Append("', ");
                    sb.Append("'").Append(currentMitglied.Spitzname).Append("', ");
                    sb.Append("'").Append(currentMitglied.Straße).Append("', ");
                    sb.Append("'").Append(currentMitglied.PLZ).Append("', ");
                    sb.Append("'").Append(currentMitglied.Ort).Append("', ");
                    sb.Append("'").Append(currentMitglied.TelefonPrivat).Append("', ");
                    sb.Append("'").Append(currentMitglied.TelefonMobil).Append("', ");
                    sb.Append("'").Append(currentMitglied.Geburtsdatum.ToString("yyyyMMdd")).Append("', ");
                    sb.Append("'").Append(currentMitglied.MitgliedSeit.Value.ToString("yyyyMMdd")).Append("', ");
                    if (currentMitglied.PassivSeit.HasValue)
                    {
                        sb.Append("'").Append(((DateTime)currentMitglied.PassivSeit.Value).ToString("yyyyMMdd"))
                            .Append("', ");
                    }
                    else
                    {
                        sb.Append("NULL, ");
                    }

                    if (currentMitglied.AusgeschiedenAm.HasValue)
                    {
                        sb.Append("'").Append((currentMitglied.AusgeschiedenAm.Value).ToString("yyyyMMdd"))
                            .Append("', ");
                    }
                    else
                    {
                        sb.Append("NULL, ");
                    }

                    sb.Append("'").Append(currentMitglied.EMail).Append("', ");
                    sb.Append(currentMitglied.Ehemaliger ? 1 : 0).Append(", ");
                    sb.Append("'").Append(currentMitglied.Notizen).Append("', ");
                    sb.Append("'").Append(currentMitglied.Bemerkungen).Append("' ");
                    sb.Append(")");
                    strSQL = sb.ToString();

                    objLog.Changetype = "insert";
                    objLog.Command = strSQL;
                    objLog.Tablename = "tblMitglieder";
                    objLog.Computername = Environment.MachineName;
                    objLog.Zeitstempel = dtLogTimestamp;

                    await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                    await _localDbContext.SaveChangesAsync();

                    //_webDbContext.SaveMitglied(iID, sAnrede, sVorname, sNachname, sSpitzname, sStrasse, sPLZ, sOrt, sTelefonPrivat, sTelefonMobil, dtGeburtsdatum, dtMitgliedSeit, dtPassivSeit, dtAusgeschiedenAm, sEMail, bEhemaliger, sNotizen, sBemerkungen);
                    //m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);

                    objLogWeb.Changetype = "insert";
                    objLogWeb.Command = strSQL;
                    objLogWeb.Tablename = "tblMitglieder";
                    objLogWeb.Computername = Environment.MachineName;
                    objLogWeb.Zeitstempel = dtLogTimestamp;

                    await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                    await _webDbContext.SaveChangesAsync();

                    break;
                default:
                    var objMitgliedSearch = await (_localDbContext.TblMitglieders
                        .Select(m => m)).SingleOrDefaultAsync(m => m.Id == currentMitglied.ID);

                    if (objMitgliedSearch != null)
                    {
                        objMitgliedSearch.Anrede = currentMitglied.Anrede;
                        objMitgliedSearch.Vorname = currentMitglied.Vorname;
                        objMitgliedSearch.Nachname = currentMitglied.Nachname;
                        objMitgliedSearch.Spitzname = currentMitglied.Spitzname;
                        objMitgliedSearch.Straße = currentMitglied.Straße;
                        objMitgliedSearch.Plz = currentMitglied.PLZ;
                        objMitgliedSearch.Ort = currentMitglied.Ort;
                        objMitgliedSearch.TelefonPrivat = currentMitglied.TelefonPrivat;
                        objMitgliedSearch.TelefonMobil = currentMitglied.TelefonMobil;
                        objMitgliedSearch.Geburtsdatum = currentMitglied.Geburtsdatum;
                        objMitgliedSearch.MitgliedSeit = currentMitglied.MitgliedSeit.Value;
                        if (currentMitglied.PassivSeit.HasValue)
                        {
                            objMitgliedSearch.PassivSeit = currentMitglied.PassivSeit;
                        }
                        else
                        {
                            objMitgliedSearch.PassivSeit = null;
                        }

                        if (currentMitglied.AusgeschiedenAm.HasValue)
                        {
                            objMitgliedSearch.AusgeschiedenAm = currentMitglied.AusgeschiedenAm;
                        }
                        else
                        {
                            objMitgliedSearch.AusgeschiedenAm = null;
                        }

                        objMitgliedSearch.Email = currentMitglied.EMail;
                        objMitgliedSearch.Ehemaliger = currentMitglied.Ehemaliger;
                        objMitgliedSearch.Notizen = currentMitglied.Notizen;
                        objMitgliedSearch.Bemerkungen = currentMitglied.Bemerkungen;
                        await _localDbContext.SaveChangesAsync();

                        sb.Append("update tblMitglieder ");
                        sb.Append(" set Anrede = ");
                        sb.Append("'").Append(currentMitglied.Anrede).Append("', ");
                        sb.Append("Vorname = ");
                        sb.Append("'").Append(currentMitglied.Vorname).Append("', ");
                        sb.Append("Nachname = ");
                        sb.Append("'").Append(currentMitglied.Nachname).Append("', ");
                        sb.Append("Spitzname = ");
                        sb.Append("'").Append(currentMitglied.Spitzname).Append("', ");
                        sb.Append("Straße = ");
                        sb.Append("'").Append(currentMitglied.Straße).Append("', ");
                        sb.Append("PLZ = ");
                        sb.Append("'").Append(currentMitglied.PLZ).Append("', ");
                        sb.Append("Ort = ");
                        sb.Append("'").Append(currentMitglied.Ort).Append("', ");
                        sb.Append("TelefonPrivat = ");
                        sb.Append("'").Append(currentMitglied.TelefonPrivat).Append("', ");
                        sb.Append("TelefonMobil = ");
                        sb.Append("'").Append(currentMitglied.TelefonMobil).Append("', ");
                        sb.Append("Geburtsdatum = ");
                        sb.Append("'").Append(currentMitglied.Geburtsdatum.ToString("yyyyMMdd")).Append("', ");
                        sb.Append("MitgliedSeit = ");
                        sb.Append("'").Append(currentMitglied.MitgliedSeit.Value.ToString("yyyyMMdd")).Append("', ");
                        sb.Append("PassivSeit = ");
                        if (currentMitglied.PassivSeit.HasValue)
                        {
                            sb.Append("'").Append((currentMitglied.PassivSeit.Value).ToString("yyyyMMdd"))
                                .Append("', ");
                        }
                        else
                        {
                            sb.Append("NULL, ");
                        }

                        sb.Append("AusgeschiedenAm = ");
                        if (currentMitglied.AusgeschiedenAm.HasValue)
                        {
                            sb.Append("'").Append((currentMitglied.AusgeschiedenAm.Value).ToString("yyyyMMdd"))
                                .Append("', ");
                        }
                        else
                        {
                            sb.Append("NULL, ");
                        }

                        sb.Append("EMail = ");
                        sb.Append("'").Append(currentMitglied.EMail).Append("', ");
                        sb.Append("Ehemaliger = ");
                        sb.Append(currentMitglied.Ehemaliger ? 1 : 0).Append(", ");
                        sb.Append("Notizen = ");
                        sb.Append("'").Append(currentMitglied.Notizen).Append("', ");
                        sb.Append("Bemerkungen = ");
                        sb.Append("'").Append(currentMitglied.Bemerkungen).Append("' ");
                        sb.Append("where ID = ").Append(objMitgliedSearch.Id.ToString());
                        strSQL = sb.ToString();

                        objLog = new();
                        objLog.Changetype = "update";
                        objLog.Command = strSQL;
                        objLog.Tablename = "tblMitglieder";
                        objLog.Computername = Environment.MachineName;
                        objLog.Zeitstempel = dtLogTimestamp;

                        await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                        await _localDbContext.SaveChangesAsync();

                        //m_objDBWeb.SaveMitglied(iID, sAnrede, sVorname, sNachname, sSpitzname, sStrasse, sPLZ, sOrt, sTelefonPrivat, sTelefonMobil, dtGeburtsdatum, dtMitgliedSeit, dtPassivSeit, dtAusgeschiedenAm, sEMail, bEhemaliger, sNotizen, sBemerkungen);
                        //m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);

                        objLogWeb = new();
                        objLogWeb.Changetype = "update";
                        objLogWeb.Command = strSQL;
                        objLogWeb.Tablename = "tblMitglieder";
                        objLogWeb.Computername = Environment.MachineName;
                        objLogWeb.Zeitstempel = dtLogTimestamp;

                        await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                        await _webDbContext.SaveChangesAsync();
                    }

                    break;
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "SaveMitglied", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "SaveMitglied", ex.ToString());
        }
    }

    // public Int32 SaveMeisterschaftsdaten(Int32 iID, string sBezeichnung, DateTime dtBeginn, object dtEnde, Int32 iTypID,
    //     Int32 iAktiv)
    public async Task<Int32> SaveMeisterschaftsdatenAsync(Meisterschaftsdaten currentMeisterschaft)
    {
        Int32 intID = -1;
        string strSQL = string.Empty;
        StringBuilder sb = new StringBuilder();
        AktiveMeisterschaft aktiveMeisterschaft = new();
        
        try
        {
            Models.Local.TblDbchangeLog objLog = new();
            Models.Web.TblDbchangeLog objLogWeb = new();
            DateTime dtLogTimestamp = DateTime.Now;

            switch (currentMeisterschaft.ID)
            {
                case -1:
                    // var a = (from mt in _localDbContext.TblMeisterschaftens
                    //          where mt.Aktiv == 1
                    //          select mt).ToList();
                    var meisterschaftNeu = await _localDbContext.TblMeisterschaftens
                        .Where(w => w.Aktiv == 1)
                        .ToListAsync();

                    foreach (var item in meisterschaftNeu)
                    {
                        item.Aktiv = 0;
                    }

                    TblMeisterschaften objMeisterNeu = new();
                    objMeisterNeu.Bezeichnung = currentMeisterschaft.Bezeichnung;
                    objMeisterNeu.Beginn = currentMeisterschaft.Beginn;
                    if (currentMeisterschaft.Ende.HasValue)
                    {
                        objMeisterNeu.Ende = currentMeisterschaft.Ende.Value;
                    }
                    else
                    {
                        objMeisterNeu.Ende = null;
                    }

                    objMeisterNeu.MeisterschaftstypId = currentMeisterschaft.MeisterschaftstypID;
                    objMeisterNeu.Aktiv = 1;

                    await _localDbContext.TblMeisterschaftens.AddAsync(objMeisterNeu);
                    await _localDbContext.SaveChangesAsync();
                    intID = objMeisterNeu.Id;

                    aktiveMeisterschaft.ID = intID;
                    aktiveMeisterschaft.Bezeichnung = currentMeisterschaft.Bezeichnung;
                    _commonService.AktiveMeisterschaft = aktiveMeisterschaft;
                    await SaveSettingsLocal("AktiveMeisterschaft", intID.ToString());
                    
                    sb.Append(
                        "insert into tblMeisterschaften(ID, Bezeichnung, Beginn, Ende, MeisterschaftstypID, Aktiv) ");
                    sb.Append("values(");
                    sb.Append(intID).Append(", ");
                    sb.Append("'").Append(currentMeisterschaft.Bezeichnung).Append("', ");
                    sb.Append("'").Append(currentMeisterschaft.Beginn.ToString("yyyyMMdd")).Append("', ");
                    if (currentMeisterschaft.Ende.HasValue)
                    {
                        sb.Append("'").Append(currentMeisterschaft.Ende.Value.ToString("yyyyMMdd")).Append("', ");
                    }
                    else
                    {
                        sb.Append("null").Append(", ");
                    }

                    sb.Append(currentMeisterschaft.MeisterschaftstypID.ToString()).Append(", ");
                    sb.Append("1)");
                    strSQL = sb.ToString();

                    objLog = new();
                    objLog.Changetype = "insert";
                    objLog.Command = strSQL;
                    objLog.Tablename = "tblMeisterschaften";
                    objLog.Computername = Environment.MachineName;
                    objLog.Zeitstempel = dtLogTimestamp;

                    await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                    await _localDbContext.SaveChangesAsync();

                    objLogWeb.Changetype = "insert";
                    objLogWeb.Command = strSQL;
                    objLogWeb.Tablename = "tblMeisterschaften";
                    objLogWeb.Computername = Environment.MachineName;
                    objLogWeb.Zeitstempel = dtLogTimestamp;

                    await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                    await _webDbContext.SaveChangesAsync();

                    break;
                default:
                    intID = currentMeisterschaft.ID;

                    if (currentMeisterschaft.Aktiv == 1)
                    {
                        // var a = (from mt in DBContext.tblMeisterschaften
                        //     where mt.Aktiv == 1
                        //     select mt).ToList();
                        var meisterschaftAktiv = await _localDbContext.TblMeisterschaftens
                            .Where(w => w.Aktiv == 1)
                            .ToListAsync();

                        foreach (var item in meisterschaftAktiv)
                        {
                            item.Aktiv = 0;
                        }
                    }

                    var objMeisterSearch = await _localDbContext.TblMeisterschaftens
                        .Select(m => m).SingleOrDefaultAsync((m => m.Id == currentMeisterschaft.ID));

                    objMeisterSearch.Bezeichnung = currentMeisterschaft.Bezeichnung;
                    objMeisterSearch.Beginn = currentMeisterschaft.Beginn;
                    if (currentMeisterschaft.Ende.HasValue)
                    {
                        objMeisterSearch.Ende = currentMeisterschaft.Ende.Value;
                    }
                    else
                    {
                        objMeisterSearch.Ende = null;
                    }

                    objMeisterSearch.MeisterschaftstypId = currentMeisterschaft.MeisterschaftstypID;
                    objMeisterSearch.Aktiv = currentMeisterschaft.Aktiv;

                    await _localDbContext.SaveChangesAsync();

                    sb.Append("update tblMeisterschaften ");
                    sb.Append("set Bezeichnung = ");
                    sb.Append("'").Append(currentMeisterschaft.Bezeichnung.Trim()).Append("', ");
                    sb.Append("Beginn = ");
                    sb.Append("'").Append(currentMeisterschaft.Beginn.ToString("yyyyMMdd")).Append("', ");
                    sb.Append("Ende = ");
                    if (currentMeisterschaft.Ende.HasValue)
                    {
                        sb.Append("'").Append(currentMeisterschaft.Ende.Value.ToString("yyyyMMdd")).Append("', ");
                    }
                    else
                    {
                        sb.Append("null").Append(", ");
                    }

                    sb.Append("MeisterschaftstypID = ");
                    sb.Append(currentMeisterschaft.MeisterschaftstypID.ToString()).Append(", ");
                    sb.Append("Aktiv = " + currentMeisterschaft.Aktiv.ToString()).Append(" ");
                    sb.Append("where ID = ").Append(intID.ToString());
                    strSQL = sb.ToString();

                    objLog = new();
                    objLog.Changetype = "update";
                    objLog.Command = strSQL;
                    objLog.Tablename = "tblMeisterschaften";
                    objLog.Computername = Environment.MachineName;
                    objLog.Zeitstempel = dtLogTimestamp;

                    await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                    await _localDbContext.SaveChangesAsync();

                    objLogWeb.Changetype = "update";
                    objLogWeb.Command = strSQL;
                    objLogWeb.Tablename = "tblMeisterschaften";
                    objLogWeb.Computername = Environment.MachineName;
                    objLogWeb.Zeitstempel = dtLogTimestamp;

                    await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                    await _webDbContext.SaveChangesAsync();

                    if (currentMeisterschaft.Aktiv == 1)
                    {
                        aktiveMeisterschaft.ID = intID;
                        aktiveMeisterschaft.Bezeichnung = currentMeisterschaft.Bezeichnung;
                        _commonService.AktiveMeisterschaft = aktiveMeisterschaft;

                        await SaveSettingsLocal("AktiveMeisterschaft", intID.ToString());
                    }
                    

                    break;
            }
        }
        catch (Exception ex)
        {
            //FrmError objForm = new FrmError("ClsDB", "SaveMeisterschaftsdaten", ex.ToString());
            //objForm.ShowDialog();
            ViewManager.ShowErrorWindow("DBService", "SaveMeisterschaftsdatenAsync", ex.ToString());
        }

        return intID;
    }

    private DataTable CreateDataTable<T>(IEnumerable<T> entities)
    {
        var dt = new DataTable();

        //creating columns
        foreach (var prop in typeof(T).GetProperties())
        {
            dt.Columns.Add(prop.Name, prop.PropertyType);
        }

        //creating rows
        foreach (var entity in entities)
        {
            var values = GetObjectValues(entity);
            dt.Rows.Add(values);
        }


        return dt;
    }

    private object[] GetObjectValues<T>(T entity)
    {
        var values = new List<object>();
        foreach (var prop in typeof(T).GetProperties())
        {
            values.Add(prop.GetValue(entity));
        }

        return values.ToArray();
    }

    private bool tablesAreTheSame(DataTable table1, DataTable table2)
    {
        // DataTable dt;
        // dt = getDifferentRecords(table1, table2);
        //
        // if (dt.Rows.Count == 0)
        //     return true;
        // else
        return false;
    }

    //private List<TblDbchangeLog> getDifferentRecords(DataTable FirstDataTable, DataTable SecondDataTable)
    private async Task<List<Models.Web.TblDbchangeLog>> getDifferentRecordsAsync()
    {
        var localDBChanges = await _localDbContext.TblDbchangeLogs.Where(w => w.Tablename != "init").ToListAsync();
        var webDBChanges = await _webDbContext.TblDbchangeLogs.Where(w => w.Tablename != "init").ToListAsync();

        // var differences = localDBChanges
        //     .Where(e => !webDBChanges.Any(e2 => e2.Zeitstempel == e.Zeitstempel))
        //     .ToList();
        var differences = webDBChanges
            .Where(w => !localDBChanges.Any(w2 => w2.Tablename == "init" && w2.Zeitstempel == w.Zeitstempel))
            .ToList();

        return differences;
    }

    public async Task<bool> ExistsSettingsWebAsync()
    {
        bool bolExists = false;

        var settings = await _webDbContext.TblSettings
            .Select(s => s)
            .ToListAsync();

        if (settings.Count > 0)
            bolExists = true;

        return bolExists;
    }

    public async Task SaveSettingsLocal(string sParametername, string sParametervalue)
    {
        StringBuilder sb = new StringBuilder();
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;
        
        var settings = await _localDbContext.TblSettings
            .Where(w => w.Parametername == sParametername)
            .Select(s => s)
            .FirstOrDefaultAsync();

        if (settings == null)
        {
            Models.Local.TblSetting objSetting = new();
            objSetting.Parametername = sParametername;
            objSetting.Parameterwert = sParametervalue;
            objSetting.Computername = Environment.MachineName;
            await _localDbContext.TblSettings.AddAsync(objSetting);
            await _localDbContext.SaveChangesAsync();
            
            sb.Append("insert into tblSettings(Parametername, Parameterwert, Computername) ");
            sb.Append("values(");
            sb.Append("'").Append(sParametername).Append("', ");
            sb.Append("'").Append(sParametervalue).Append("', ");
            sb.Append("'").Append(Environment.MachineName).Append("')");
            
            objLog.Changetype = "insert";
            objLog.Command = sb.ToString();
            objLog.Tablename = "tblSettings";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;

            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = sb.ToString();;
            objLogWeb.Tablename = "tblSettings";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;

            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            settings.Parametername = sParametername;
            await _localDbContext.SaveChangesAsync();
            
            sb.Append("update tblSettings set Parametername = ");
            sb.Append("'").Append(sParametername).Append("', ");
            sb.Append("Parameterwert = ");
            sb.Append("'").Append(sParametervalue).Append("', ");
            sb.Append("Computername = ");
            sb.Append("'").Append(Environment.MachineName).Append("' ");
            sb.Append("where Parametername = '").Append(sParametername). Append("'");
            
            objLog.Changetype = "update";
            objLog.Command = sb.ToString();
            objLog.Tablename = "tblSettings";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;

            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "update";
            objLogWeb.Command = sb.ToString();;
            objLogWeb.Tablename = "tblSettings";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;

            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }

        await SaveSettingsWebAsync();
    }
    
    public async Task SaveSettingsWebAsync()
    {
        var settings = await _localDbContext.TblSettings
            .Select(s => s)
            .ToListAsync();

        foreach (var item in settings)
        {
            switch (item.Parametername)
            {
                case "AktiveMeisterschaft":
                    var meisterschaftAktiv = await _webDbContext.TblMeisterschaftens
                        .Where(w => w.Aktiv == 1)
                        .ToListAsync();

                    foreach (var ma in meisterschaftAktiv)
                    {
                        ma.Aktiv = 0;
                    }
                    
                    int intID = Convert.ToInt32(item.Parameterwert);
                    var objMeisterSearch = await _webDbContext.TblMeisterschaftens
                        .Select(m => m).SingleOrDefaultAsync(m => m.Id == intID);
                    objMeisterSearch!.Aktiv = 1;
                    
                    var setting = await _webDbContext.TblSettings
                        .Select(s => s)
                        .SingleOrDefaultAsync(s => s.Parametername == "AktiveMeisterschaft");
                    setting!.Parameterwert = intID.ToString();
                    
                    await _webDbContext.SaveChangesAsync();
                    
                    break;
            }
        }
    }
    
    public async Task LoadSettingsWebAsync()
    {
        var settings = await _webDbContext.TblSettings
            .Select(s => s)
            .ToListAsync();

        foreach (var item in settings)
        {
            switch (item.Parametername)
            {
                case "AktiveMeisterschaft":
                    var meisterschaftAktiv = await _localDbContext.TblMeisterschaftens
                        .Where(w => w.Aktiv == 1)
                        .ToListAsync();

                    foreach (var ma in meisterschaftAktiv)
                    {
                        ma.Aktiv = 0;
                    }

                    int intID = Convert.ToInt32(item.Parameterwert);
                    var objMeisterSearch = await _localDbContext.TblMeisterschaftens
                        .Select(m => m).SingleOrDefaultAsync(m => m.Id == intID);
                    objMeisterSearch!.Aktiv = 1;
                    await _localDbContext.SaveChangesAsync();

                    AktiveMeisterschaft aktiveMeisterschaft = new();
                    aktiveMeisterschaft.ID = intID;
                    aktiveMeisterschaft.Bezeichnung = objMeisterSearch.Bezeichnung;
                    _commonService.AktiveMeisterschaft = aktiveMeisterschaft;
                    
                    break;
            }
        }
    }

    public async Task UpdateLocalDBAsync()
    {
        List<Models.Web.TblDbchangeLog> lstDiff = await getDifferentRecordsAsync();
        List<Models.Web.TblDbchangeLog> lstDiffOrdered = lstDiff.OrderBy(o => o.Zeitstempel).ToList();
        string strSQL = string.Empty;

        foreach (var row in lstDiffOrdered)
        {
            if (row.Tablename == "tblSettings") continue;

            if (strSQL != "init")
            {
                using (var transaction = await _localDbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await IdentityInsertAsync(row.Tablename);
                        strSQL = row.Command;
                        int intRowsAffected = await _localDbContext.Database.ExecuteSqlRawAsync(strSQL);
                        await IdentityInsertAsync(row.Tablename, "OFF");

                        Models.Local.TblDbchangeLog objLog = new Models.Local.TblDbchangeLog();
                        objLog.Computername = row.Computername;
                        objLog.Tablename = row.Tablename;
                        objLog.Changetype = row.Changetype;
                        objLog.Command = row.Command;
                        objLog.Zeitstempel = row.Zeitstempel;
                        await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                        await _localDbContext.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ViewManager.ShowErrorWindow("DBService", "UpdateLocalDBAsync",
                            strSQL + Environment.NewLine + Environment.NewLine + ex.ToString());
                    }
                }
            }
        }
    }

    private async Task IdentityInsertAsync(string sTablename, string sOnOff = "ON")
    {
        string strSQLIdentity;

        switch (sTablename)
        {
            case "tblMeisterschaften":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tbl9erRatten":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tblSpieltag":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tblSpiel6TageRennen":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tblSpielBlitztunier":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tblSpielKombimeisterschaft":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tblSpielMeisterschaft":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tblSpielPokal":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
            case "tblSpielSargKegeln":
                strSQLIdentity = "SET IDENTITY_INSERT " + sTablename + " " + sOnOff;
                await _localDbContext.Database.ExecuteSqlRawAsync(strSQLIdentity);
                break;
        }
    }

    public async Task SaveTeilnehmerAsync(Int32 iMeisterschaftsID, Int32 iTeilnehmerID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;

        Models.Local.TblTeilnehmer objTeilnehmer = new ();
        objTeilnehmer.MeisterschaftsId = iMeisterschaftsID;
        objTeilnehmer.SpielerId = iTeilnehmerID;

        await _localDbContext.TblTeilnehmers.AddAsync(objTeilnehmer);
        await _localDbContext.SaveChangesAsync();

        sb.Append("insert into tblTeilnehmer(MeisterschaftsID, SpielerID) ");
        sb.Append("values(");
        sb.Append(iMeisterschaftsID.ToString()).Append(", ");
        sb.Append(iTeilnehmerID.ToString());
        sb.Append(")");
        strSQL = sb.ToString();

        Models.Local.TblDbchangeLog objLog = new ();
        objLog.Changetype = "insert";
        objLog.Command = strSQL;
        objLog.Tablename = "tblTeilnehmer";
        objLog.Computername = Environment.MachineName;
        objLog.Zeitstempel = DateTime.Now;
        await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
        await _localDbContext.SaveChangesAsync();
        
        // m_objDBWeb.SaveTeilnehmer(iMeisterschaftsID, iTeilnehmerID);
        // m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command,
        //     objLog.Zeitstempel.Value);
        
        Models.Web.TblDbchangeLog objLogWeb = new ();
        objLogWeb.Changetype = "insert";
        objLogWeb.Command = strSQL;
        objLogWeb.Tablename = "tblTeilnehmer";
        objLogWeb.Computername = Environment.MachineName;
        objLogWeb.Zeitstempel = DateTime.Now;
        await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
        await _webDbContext.SaveChangesAsync();
    }

    public async Task DeleteTeilnehmerAsync(Int32 iMeisterschaftsID, Int32 iTeilnehmerID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;

        var t = await _localDbContext.TblTeilnehmers
            .Where(w => w.MeisterschaftsId == iMeisterschaftsID && w.SpielerId == iTeilnehmerID)
            .Select(s => s).FirstOrDefaultAsync();

        if (t != null)
        {
            _localDbContext.TblTeilnehmers.Remove(t);
            await _localDbContext.SaveChangesAsync();

            sb.Append("delete from tblTeilnehmer ");
            sb.Append("where ");
            sb.Append("MeisterschaftsID = ").Append(iMeisterschaftsID.ToString()).Append(" and ");
            sb.Append("SpielerID = ").Append(iTeilnehmerID.ToString());
            strSQL = sb.ToString();

            Models.Local.TblDbchangeLog objLog = new();
            objLog.Changetype = "delete";
            objLog.Command = strSQL;
            objLog.Tablename = "tblTeilnehmer";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = DateTime.Now;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            // m_objDBWeb.DeleteTeilnehmer(iMeisterschaftsID, iTeilnehmerID);
            // m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command,
            //     objLog.Zeitstempel.Value);

            Models.Web.TblDbchangeLog objLogWeb = new();
            objLogWeb.Changetype = "delete";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblTeilnehmer";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = DateTime.Now;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
    }

    public void SpieltagBeenden(Int32 iSpieltagID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objSpieltag = (DBContext.tblSpieltag
        //         .Where(w => w.ID == iSpieltagID)
        //         .Select(s => s)).First();
        //
        //     objSpieltag.InBearbeitung = false;
        //     DBContext.SaveChanges();
        //
        //     sb.Append("update tblSpieltag ");
        //     sb.Append("set InBearbeitung = 0 ");
        //     sb.Append("where ID = " + iSpieltagID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "update";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpieltag";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.SpieltagBeenden(iSpieltagID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public Int32 SaveSpieltag(Int32 iMeisterschaftsID, DateTime dtSpieltag)
    {
        Int32 intID = 0;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var checkSpieltag = (DBContext.tblSpieltag
        //                         .Where(w => w.Spieltag == dtSpieltag && w.MeisterschaftsID == iMeisterschaftsID)
        //                         .Select(s => s));
        //
        //     
        //     if (checkSpieltag.SingleOrDefault() == null)
        //     {
        //         tblSpieltag objSpieltag = new tblSpieltag();
        //         objSpieltag.MeisterschaftsID = iMeisterschaftsID;
        //         objSpieltag.Spieltag = dtSpieltag;
        //         objSpieltag.InBearbeitung = true;
        //
        //         DBContext.tblSpieltag.Add(objSpieltag);
        //         DBContext.SaveChanges();
        //         intID = objSpieltag.ID;
        //
        //         sb.Append("insert into tblSpieltag(ID, MeisterschaftsID, Spieltag, InBearbeitung) ");
        //         sb.Append("values(");
        //         sb.Append(intID.ToString()).Append(", ");
        //         sb.Append(iMeisterschaftsID.ToString()).Append(", ");
        //         sb.Append("'").Append(dtSpieltag.ToString("yyyyMMdd")).Append("', ");
        //         sb.Append("1)");
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "insert";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpieltag";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpieltag(intID, iMeisterschaftsID, dtSpieltag);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        //     else
        //     {
        //         //intID = ((tblSpieltag)checkSpieltag).ID;
        //         intID = checkSpieltag.ToArray()[0].ID;
        //
        //         checkSpieltag.ToList()[0].InBearbeitung = true;
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tblSpieltag ");
        //         sb.Append("set InBearbeitung = 1 ");
        //         sb.Append("where ID = ");
        //         sb.Append(intID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpieltag";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpieltag(intID, iMeisterschaftsID, dtSpieltag, true);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }

        return intID;
    }

    public void Save9erRatten(Int32 iSpieltagID, Int32 iSpielerID)
    {
        // Int32 int9erRattenID = -1;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     tbl9erRatten objNR = new tbl9erRatten();
        //     objNR.SpieltagID = iSpieltagID;
        //     objNR.SpielerID = iSpielerID;
        //     objNR.Neuner = 0;
        //     objNR.Ratten = 0;
        //
        //     DBContext.tbl9erRatten.Add(objNR);
        //     DBContext.SaveChanges();
        //     int9erRattenID = objNR.ID;
        //
        //     //tblMapSpieltag2Spiele objMAP = new tblMapSpieltag2Spiele();
        //     //objMAP.SpieltagID = iSpieltagID;
        //     //objMAP.C9erRattenID = int9erRattenID;
        //
        //     //DBContext.tblMapSpieltag2Spiele.Add(objMAP);
        //     //DBContext.SaveChanges();
        //
        //     //var objMap = (DBContext.tblMapSpieltag2Spiele
        //     //                    .Where(w => w.SpieltagID == iSpieltagID)
        //     //                    .Select(s => s)).First();
        //
        //     //objMap.C9erRattenID = int9erRattenID;
        //     //DBContext.SaveChanges();
        //
        //     sb.Append("insert into tbl9erRatten(ID, SpieltagID, SpielerID, Neuner, Ratten) ");
        //     sb.Append("values (");
        //     sb.Append(int9erRattenID.ToString()).Append(", ");
        //     sb.Append(iSpieltagID.ToString()).Append(", ");
        //     sb.Append(iSpielerID.ToString());
        //     sb.Append(", 0, 0)");
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "insert";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tbl9erRatten";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.Save9erRatten(int9erRattenID, iSpieltagID, iSpielerID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //
        //     //sb.Clear();
        //     //sb.Append("update tblMapSpieltag2Spiele");
        //     //sb.Append("set [9erRattenID] = ");
        //     //sb.Append(int9erRattenID.ToString()).Append(" ");
        //     //sb.Append("where SpieltagID = " + iSpieltagID.ToString());
        //     //strSQL = sb.ToString();
        //
        //     //objLog = new tblDBChangeLog();
        //     //objLog.Changetype = "update";
        //     //objLog.Command = strSQL;
        //     //objLog.Tablename = "tblMapSpieltag2Spiele";
        //     //objLog.Computername = Environment.MachineName;
        //     //objLog.Zeitstempel = DateTime.Now;
        //     //DBContext.tblDBChangeLog.Add(objLog);
        //     //DBContext.SaveChanges();
        //
        //     //m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void Delete9erRatten(Int32 iID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objNR = (DBContext.tbl9erRatten
        //         .Where(w => w.ID == iID)
        //         .Select(s => s)).SingleOrDefault();
        //
        //     DBContext.tbl9erRatten.Remove(objNR);
        //     DBContext.SaveChanges();
        //
        //     sb.Append("delete from tbl9erRatten ");
        //     sb.Append("where ID = " + iID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "delete";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tbl9erRatten";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.Delete9erRatten(iID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void DeleteSpiel6TageRennen(Int32 iID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var obj6TR = (DBContext.tblSpiel6TageRennen
        //         .Where(w => w.ID == iID)
        //         .Select(s => s)).SingleOrDefault();
        //
        //     DBContext.tblSpiel6TageRennen.Remove(obj6TR);
        //     DBContext.SaveChanges();
        //
        //     sb.Append("delete from tblSpiel6TageRennen ");
        //     sb.Append("where ID = " + iID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "delete";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpiel6TageRennen";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.DeleteSpiel6TageRennen(iID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void DeleteSpielBlitztunier(Int32 iID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objBlitz = (DBContext.tblSpielBlitztunier
        //         .Where(w => w.ID == iID)
        //         .Select(s => s)).SingleOrDefault();
        //
        //     DBContext.tblSpielBlitztunier.Remove(objBlitz);
        //     DBContext.SaveChanges();
        //
        //     sb.Append("delete from tblSpielBlitztunier ");
        //     sb.Append("where ID = " + iID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "delete";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielBlitztunier";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.DeleteSpielBlitztunier(iID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void DeleteSpielKombimeisterschaft(Int32 iID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objKombi = (DBContext.tblSpielKombimeisterschaft
        //         .Where(w => w.ID == iID)
        //         .Select(s => s)).SingleOrDefault();
        //
        //     DBContext.tblSpielKombimeisterschaft.Remove(objKombi);
        //     DBContext.SaveChanges();
        //
        //     sb.Append("delete from tblSpielKombimeisterschaft ");
        //     sb.Append("where ID = " + iID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "delete";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielKombimeisterschaft";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.DeleteSpielKombimeisterschaft(iID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void DeleteSpielMeisterschaft(Int32 iID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objMeisterschaft = (DBContext.tblSpielMeisterschaft
        //         .Where(w => w.ID == iID)
        //         .Select(s => s)).SingleOrDefault();
        //
        //     DBContext.tblSpielMeisterschaft.Remove(objMeisterschaft);
        //     DBContext.SaveChanges();
        //
        //     sb.Append("delete from tblSpielMeisterschaft ");
        //     sb.Append("where ID = " + iID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "delete";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielMeisterschaft";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.DeleteSpielMeisterschaft(iID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void DeleteSpielPokal(Int32 iID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objPokal = (DBContext.tblSpielPokal
        //         .Where(w => w.ID == iID)
        //         .Select(s => s)).SingleOrDefault();
        //
        //     DBContext.tblSpielPokal.Remove(objPokal);
        //     DBContext.SaveChanges();
        //
        //     sb.Append("delete from tblSpielPokal ");
        //     sb.Append("where ID = " + iID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "delete";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielPokal";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.DeleteSpielPokal(iID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void DeleteSpielSargKegeln(Int32 iID)
    {
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objSarg = (DBContext.tblSpielSargKegeln
        //         .Where(w => w.ID == iID)
        //         .Select(s => s)).SingleOrDefault();
        //
        //     DBContext.tblSpielSargKegeln.Remove(objSarg);
        //     DBContext.SaveChanges();
        //
        //     sb.Append("delete from tblSpielSargKegeln ");
        //     sb.Append("where ID = " + iID.ToString());
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "delete";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielSargKegeln";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.DeleteSpielSargKegeln(iID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public Int32 GetSpieltagInBearbeitung(ref DateTime dSpieltag)
    {
        Int32 intID = -1;

        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objSpieltag = (DBContext.tblSpieltag
        //                         .Where(w => w.InBearbeitung == true)
        //                         .Select(s => s)).SingleOrDefault();
        //
        //     if (objSpieltag != null)
        //     {
        //         intID = objSpieltag.ID;
        //         dSpieltag = objSpieltag.Spieltag;
        //     }
        // }

        return intID;
    }

    public void Save9erRattenEingabe(Int32 iNRID, Int32 i9er, Int32 iRatten)
    {
        // string strSQL = string.Empty;
        // StringBuilder sb = new StringBuilder();
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objNR = (DBContext.tbl9erRatten
        //                 .Select(m => m)).SingleOrDefault(m => m.ID == iNRID);
        //
        //     if (objNR.Neuner != i9er || objNR.Ratten != iRatten)
        //     {
        //         objNR.Neuner = i9er;
        //         objNR.Ratten = iRatten;
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tbl9erRatten ");
        //         sb.Append("set Neuner = " + i9er.ToString()).Append(", ");
        //         sb.Append("Ratten = " + iRatten.ToString()).Append(" ");
        //         sb.Append("where ID = " + iNRID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tbl9erRatten";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.Save9erRattenEingabe(iNRID, i9er, iRatten);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }
    }

    public void SaveSpiel6TageRennenEingabe(Int32 iSpiel6TageRennenID, Int32 iRunden, Int32 iPunkte, Int32 iSpielnummer)
    {
        // string strSQL = string.Empty;
        // StringBuilder sb = new StringBuilder();
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var obj6TageRennen = (DBContext.tblSpiel6TageRennen
        //                 .Select(m => m)).SingleOrDefault(m => m.ID == iSpiel6TageRennenID);
        //
        //     if (obj6TageRennen.Runden != iRunden || obj6TageRennen.Punkte != iPunkte)
        //     {
        //         obj6TageRennen.Runden = iRunden;
        //         obj6TageRennen.Punkte = iPunkte;
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tblSpiel6TageRennen ");
        //         sb.Append("set Runden = " + iRunden.ToString()).Append(", ");
        //         sb.Append("Punkte = " + iPunkte.ToString()).Append(", ");
        //         sb.Append("Spielnummer = " + iSpielnummer.ToString()).Append(" ");
        //         sb.Append("where ID = " + iSpiel6TageRennenID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpiel6TageRennen";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpiel6TageRennenEingabe(iSpiel6TageRennenID, iRunden, iPunkte, iSpielnummer);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }
    }

    public void SaveSpielBlitztunierEingabe(Int32 iSpielBlitztunierID, Int32 iPunkteSpieler1, Int32 iPunkteSpieler2,
        Int32 iHinRückrunde)
    {
        // string strSQL = string.Empty;
        // StringBuilder sb = new StringBuilder();
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objBlitz = (DBContext.tblSpielBlitztunier
        //                 .Select(m => m)).SingleOrDefault(m => m.ID == iSpielBlitztunierID);
        //
        //     if (objBlitz.PunkteSpieler1 != iPunkteSpieler1 || objBlitz.PunkteSpieler2 != iPunkteSpieler2)
        //     {
        //         objBlitz.PunkteSpieler1 = iPunkteSpieler1;
        //         objBlitz.PunkteSpieler2 = iPunkteSpieler2;
        //         objBlitz.HinRückrunde = iHinRückrunde;
        //
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tblSpielBlitztunier ");
        //         sb.Append("set PunkteSpieler1 = " + iPunkteSpieler1.ToString()).Append(", ");
        //         sb.Append("PunkteSpieler2 = " + iPunkteSpieler2.ToString()).Append(", ");
        //         sb.Append("HinRückrunde = " + iHinRückrunde.ToString()).Append(" ");
        //         sb.Append("where ID = " + iSpielBlitztunierID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpielBlitztunier";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpielBlitztunierEingabe(iSpielBlitztunierID, iPunkteSpieler1, iPunkteSpieler2, iHinRückrunde);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }
    }

    public void SaveSpielMeisterschaftEingabe(Int32 iSpielMeisterschaftID, Int32 iHolzSpieler1, Int32 iHolzSpieler2,
        Int32 iHinRückrunde)
    {
        // string strSQL = string.Empty;
        // StringBuilder sb = new StringBuilder();
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objMeisterschaft = (DBContext.tblSpielMeisterschaft
        //                 .Select(m => m)).SingleOrDefault(m => m.ID == iSpielMeisterschaftID);
        //
        //     if (objMeisterschaft.HolzSpieler1 != iHolzSpieler1 || objMeisterschaft.HolzSpieler2 != iHolzSpieler2)
        //     {
        //         objMeisterschaft.HolzSpieler1 = iHolzSpieler1;
        //         objMeisterschaft.HolzSpieler2 = iHolzSpieler2;
        //         objMeisterschaft.HinRückrunde = iHinRückrunde;
        //
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tblSpielMeisterschaft ");
        //         sb.Append("set HolzSpieler1 = " + iHolzSpieler1.ToString()).Append(", ");
        //         sb.Append("HolzSpieler2 = " + iHolzSpieler2.ToString()).Append(", ");
        //         sb.Append("HinRückrunde = " + iHinRückrunde.ToString()).Append(" ");
        //         sb.Append("where ID = " + iSpielMeisterschaftID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpielMeisterschaft";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpieMeisterschaftEingabe(iSpielMeisterschaftID, iHolzSpieler1, iHolzSpieler2, iHinRückrunde);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }
    }

    public void SaveSpielKombimeisterschaftEingabe(Int32 iSpielKombimeisterschaftID, Int32 iSpieler13bis8,
        Int32 iSpieler15Kugeln, Int32 iSpieler23bis8, Int32 iSpieler25Kugeln, Int32 iHinRückrunde)
    {
        // string strSQL = string.Empty;
        // StringBuilder sb = new StringBuilder();
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objKombi = (DBContext.tblSpielKombimeisterschaft
        //                 .Select(m => m)).SingleOrDefault(m => m.ID == iSpielKombimeisterschaftID);
        //
        //     if (objKombi.Spieler1Punkte3bis8 != iSpieler13bis8 || objKombi.Spieler1Punkte5Kugeln != iSpieler15Kugeln || objKombi.Spieler2Punkte3bis8 != iSpieler23bis8 || objKombi.Spieler2Punkte5Kugeln != iSpieler25Kugeln || objKombi.HinRückrunde != iHinRückrunde)
        //     {
        //         objKombi.Spieler1Punkte3bis8 = iSpieler13bis8;
        //         objKombi.Spieler1Punkte5Kugeln = iSpieler15Kugeln;
        //         objKombi.Spieler2Punkte3bis8 = iSpieler23bis8;
        //         objKombi.Spieler2Punkte5Kugeln = iSpieler25Kugeln;
        //         objKombi.HinRückrunde = iHinRückrunde;
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tblSpielKombimeisterschaft ");
        //         sb.Append("set Spieler1Punkte3bis8 = " + iSpieler13bis8.ToString()).Append(", ");
        //         sb.Append("Spieler1Punkte5Kugeln = " + iSpieler15Kugeln.ToString()).Append(", ");
        //         sb.Append("Spieler2Punkte3bis8 = " + iSpieler23bis8.ToString()).Append(", ");
        //         sb.Append("Spieler2Punkte5Kugeln = " + iSpieler25Kugeln.ToString()).Append(", ");
        //         sb.Append("HinRückrunde = " + iHinRückrunde.ToString()).Append(" ");
        //         sb.Append("where ID = " + iSpielKombimeisterschaftID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpielKombimeisterschaft";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpielKombimeisterschaftEingabe(iSpielKombimeisterschaftID, iSpieler13bis8, iSpieler15Kugeln, iSpieler23bis8, iSpieler25Kugeln, iHinRückrunde);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }
    }

    public void SaveSpielPokalEingabe(Int32 iSpielPokalID, Int32 iPlatzierung)
    {
        // string strSQL = string.Empty;
        // StringBuilder sb = new StringBuilder();
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objPokal = (DBContext.tblSpielPokal
        //                 .Select(m => m)).SingleOrDefault(m => m.ID == iSpielPokalID);
        //
        //     if (objPokal.Platzierung != iPlatzierung)
        //     {
        //         objPokal.Platzierung = iPlatzierung;
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tblSpielPokal ");
        //         sb.Append("set Platzierung = " + iPlatzierung.ToString()).Append(" ");
        //         sb.Append("where ID = " + iSpielPokalID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpielPokal";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpielPokalEingabe(iSpielPokalID, iPlatzierung);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }
    }

    public void SaveSpielSargKegelnEingabe(Int32 iSpielSargKegelnID, Int32 iPlatzierung)
    {
        // string strSQL = string.Empty;
        // StringBuilder sb = new StringBuilder();
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     var objSargKegeln = (DBContext.tblSpielSargKegeln
        //                 .Select(m => m)).SingleOrDefault(m => m.ID == iSpielSargKegelnID);
        //
        //     if (objSargKegeln.Platzierung != iPlatzierung)
        //     {
        //         objSargKegeln.Platzierung = iPlatzierung;
        //         DBContext.SaveChanges();
        //
        //         sb.Append("update tblSpielSargKegeln ");
        //         sb.Append("set Platzierung = " + iPlatzierung.ToString()).Append(" ");
        //         sb.Append("where ID = " + iSpielSargKegelnID.ToString());
        //         strSQL = sb.ToString();
        //
        //         tblDBChangeLog objLog = new tblDBChangeLog();
        //         objLog.Changetype = "update";
        //         objLog.Command = strSQL;
        //         objLog.Tablename = "tblSpielSargKegeln";
        //         objLog.Computername = Environment.MachineName;
        //         objLog.Zeitstempel = DateTime.Now;
        //         DBContext.tblDBChangeLog.Add(objLog);
        //         DBContext.SaveChanges();
        //
        //         m_objDBWeb.SaveSpielPokalEingabe(iSpielSargKegelnID, iPlatzierung);
        //         m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        //     }
        // }
    }

    public void SaveSpiel6TageRennen(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2, Int32 iSpielnummer)
    {
        // Int32 int6TageRennenID = -1;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     tblSpiel6TageRennen obj6TR = new tblSpiel6TageRennen();
        //     obj6TR.SpieltagID = iSpieltagID;
        //     obj6TR.SpielerID1 = iSpielerID1;
        //     obj6TR.SpielerID2 = iSpielerID2;
        //     obj6TR.Runden = 0;
        //     obj6TR.Punkte = 0;
        //     obj6TR.Spielnummer = iSpielnummer;
        //
        //     DBContext.tblSpiel6TageRennen.Add(obj6TR);
        //     DBContext.SaveChanges();
        //     int6TageRennenID = obj6TR.ID;
        //
        //     sb.Append("insert into tblSpiel6TageRennen(ID, SpieltagID, SpielerID1, SpielerID2, Runden, Punkte, Spielnummer) ");
        //     sb.Append("values (");
        //     sb.Append(int6TageRennenID.ToString()).Append(", ");
        //     sb.Append(iSpieltagID.ToString()).Append(", ");
        //     sb.Append(iSpielerID1.ToString()).Append(", ");
        //     sb.Append(iSpielerID2.ToString()).Append(", ");
        //     sb.Append("0, 0, ");
        //     sb.Append(iSpielnummer.ToString() + ")");
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "insert";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpiel6TageRennen";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.SaveSpiel6TageRennen(int6TageRennenID, iSpieltagID, iSpielerID1, iSpielerID2, iSpielnummer);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void SaveSpielBlitztunier(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2, Int32 iHinRueckrunde)
    {
        // Int32 intBlitztunierID = -1;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     tblSpielBlitztunier objBlitz = new tblSpielBlitztunier();
        //     objBlitz.SpieltagID = iSpieltagID;
        //     objBlitz.SpielerID1 = iSpielerID1;
        //     objBlitz.SpielerID2 = iSpielerID2;
        //     objBlitz.PunkteSpieler1 = 0;
        //     objBlitz.PunkteSpieler2 = 0;
        //     objBlitz.HinRückrunde = iHinRueckrunde;
        //
        //     DBContext.tblSpielBlitztunier.Add(objBlitz);
        //     DBContext.SaveChanges();
        //     intBlitztunierID = objBlitz.ID;
        //
        //     sb.Append("insert into tblSpielBlitztunier(ID, SpieltagID, SpielerID1, SpielerID2, PunkteSpieler1, PunkteSpieler2, HinRückrunde) ");
        //     sb.Append("values (");
        //     sb.Append(intBlitztunierID.ToString()).Append(", ");
        //     sb.Append(iSpieltagID.ToString()).Append(", ");
        //     sb.Append(iSpielerID1.ToString()).Append(", ");
        //     sb.Append(iSpielerID2.ToString()).Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append("0");
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "insert";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielBlitztunier";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.SaveSpielBlitztunier(intBlitztunierID, iSpieltagID, iSpielerID1, iSpielerID2, iHinRueckrunde);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void SaveSpielKombimeisterschaft(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2,
        Int32 iHinRueckrunde)
    {
        // Int32 intKombimeisterschaftID = -1;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     tblSpielKombimeisterschaft objKombi = new tblSpielKombimeisterschaft();
        //     objKombi.SpieltagID = iSpieltagID;
        //     objKombi.SpielerID1 = iSpielerID1;
        //     objKombi.SpielerID2 = iSpielerID2;
        //     objKombi.Spieler1Punkte3bis8 = 0;
        //     objKombi.Spieler1Punkte5Kugeln = 0;
        //     objKombi.Spieler2Punkte3bis8 = 0;
        //     objKombi.Spieler2Punkte5Kugeln = 0;
        //     objKombi.HinRückrunde = iHinRueckrunde;
        //
        //     DBContext.tblSpielKombimeisterschaft.Add(objKombi);
        //     DBContext.SaveChanges();
        //     intKombimeisterschaftID = objKombi.ID;
        //
        //     sb.Append("insert into tblSpielKombimeisterschaft(ID, SpieltagID, SpielerID1, SpielerID2, Spieler1Punkte3bis8, Spieler1Punkte5Kugeln, Spieler2Punkte3bis8, Spieler2Punkte5Kugeln, HinRückrunde) ");
        //     sb.Append("values (");
        //     sb.Append(intKombimeisterschaftID.ToString()).Append(", ");
        //     sb.Append(iSpieltagID.ToString()).Append(", ");
        //     sb.Append(iSpielerID1.ToString()).Append(", ");
        //     sb.Append(iSpielerID2.ToString()).Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append(iHinRueckrunde.ToString()).Append(")");
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "insert";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielKombimeisterschaft";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.SaveSpielKombimeisterschaft(intKombimeisterschaftID, iSpieltagID, iSpielerID1, iSpielerID2, iHinRueckrunde);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void SaveSpielMeisterschaft(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2, Int32 iHinRueckrunde)
    {
        // Int32 intSpielMeisterschaftID = -1;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     tblSpielMeisterschaft objMeisterschaft = new tblSpielMeisterschaft();
        //     objMeisterschaft.SpieltagID = iSpieltagID;
        //     objMeisterschaft.SpielerID1 = iSpielerID1;
        //     objMeisterschaft.SpielerID2 = iSpielerID2;
        //     objMeisterschaft.HolzSpieler1 = 0;
        //     objMeisterschaft.HolzSpieler2 = 0;
        //     objMeisterschaft.HinRückrunde = iHinRueckrunde;
        //
        //     DBContext.tblSpielMeisterschaft.Add(objMeisterschaft);
        //     DBContext.SaveChanges();
        //     intSpielMeisterschaftID = objMeisterschaft.ID;
        //
        //     sb.Append("insert into tblSpielMeisterschaft(ID, SpieltagID, SpielerID1, SpielerID2, HolzSpieler1, HolzSpieler2, HinRückrunde) ");
        //     sb.Append("values (");
        //     sb.Append(intSpielMeisterschaftID.ToString()).Append(", ");
        //     sb.Append(iSpieltagID.ToString()).Append(", ");
        //     sb.Append(iSpielerID1.ToString()).Append(", ");
        //     sb.Append(iSpielerID2.ToString()).Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append("0").Append(", ");
        //     sb.Append("0");
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "insert";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielMeisterschaft";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.SaveSpielMeisterschaft(intSpielMeisterschaftID, iSpieltagID, iSpielerID1, iSpielerID2, iHinRueckrunde);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void SaveSpielPokal(Int32 iSpieltagID, Int32 iSpielerID)
    {
        // Int32 intSpielPokalID = -1;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     tblSpielPokal objPokal = new tblSpielPokal();
        //     objPokal.SpieltagID = iSpieltagID;
        //     objPokal.SpielerID = iSpielerID;
        //     objPokal.Platzierung = 0;
        //
        //     DBContext.tblSpielPokal.Add(objPokal);
        //     DBContext.SaveChanges();
        //     intSpielPokalID = objPokal.ID;
        //
        //     sb.Append("insert into tblSpielPokal(ID, SpieltagID, SpielerID, Platzierung) ");
        //     sb.Append("values (");
        //     sb.Append(intSpielPokalID.ToString()).Append(", ");
        //     sb.Append(iSpieltagID.ToString()).Append(", ");
        //     sb.Append(iSpielerID.ToString()).Append(", ");
        //     sb.Append("0)");
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "insert";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielPokal";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.SaveSpielPokal(intSpielPokalID, iSpieltagID, iSpielerID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }

    public void SaveSpielSargKegeln(Int32 iSpieltagID, Int32 iSpielerID)
    {
        // Int32 intSpielPokalID = -1;
        // StringBuilder sb = new StringBuilder();
        // string strSQL = string.Empty;
        //
        // using (KEPAVerwaltungEntities DBContext = new KEPAVerwaltungEntities())
        // {
        //     tblSpielSargKegeln objSarg = new tblSpielSargKegeln();
        //     objSarg.SpieltagID = iSpieltagID;
        //     objSarg.SpielerID = iSpielerID;
        //     objSarg.Platzierung = 0;
        //
        //     DBContext.tblSpielSargKegeln.Add(objSarg);
        //     DBContext.SaveChanges();
        //     intSpielPokalID = objSarg.ID;
        //
        //     sb.Append("insert into tblSpielSargKegeln(ID, SpieltagID, SpielerID, Platzierung) ");
        //     sb.Append("values (");
        //     sb.Append(intSpielPokalID.ToString()).Append(", ");
        //     sb.Append(iSpieltagID.ToString()).Append(", ");
        //     sb.Append(iSpielerID.ToString()).Append(", ");
        //     sb.Append("0)");
        //     strSQL = sb.ToString();
        //
        //     tblDBChangeLog objLog = new tblDBChangeLog();
        //     objLog.Changetype = "insert";
        //     objLog.Command = strSQL;
        //     objLog.Tablename = "tblSpielSargKegeln";
        //     objLog.Computername = Environment.MachineName;
        //     objLog.Zeitstempel = DateTime.Now;
        //     DBContext.tblDBChangeLog.Add(objLog);
        //     DBContext.SaveChanges();
        //
        //     m_objDBWeb.SaveSpielSargKegeln(intSpielPokalID, iSpieltagID, iSpielerID);
        //     m_objDBWeb.SaveDBLog(objLog.Changetype, objLog.Computername, objLog.Tablename, objLog.Command, objLog.Zeitstempel.Value);
        // }
    }
}