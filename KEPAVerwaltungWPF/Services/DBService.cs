using System.Data;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Windows.Documents;
using AutoMapper;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.DTOs.Ausgabe;
using KEPAVerwaltungWPF.DTOs.Ergebnisse;
using KEPAVerwaltungWPF.DTOs.Statistik;
using KEPAVerwaltungWPF.Models.Local;
using KEPAVerwaltungWPF.Models.Web;
using KEPAVerwaltungWPF.Views;
using Microsoft.EntityFrameworkCore;
using StatistikSpieler = KEPAVerwaltungWPF.DTOs.Statistik.StatistikSpieler;
using StatistikSpielerErgebnisse = KEPAVerwaltungWPF.DTOs.Statistik.StatistikSpielerErgebnisse;
using TblDbchangeLog = KEPAVerwaltungWPF.Models.Web.TblDbchangeLog;
using TblMeisterschaften = KEPAVerwaltungWPF.Models.Local.TblMeisterschaften;

namespace KEPAVerwaltungWPF.Services;

public class DBService(
    LocalDbContext _localDbContext,
    WebDbContext _webDbContext,
    CommonService _commonService) //, IMapper _mapper
{
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


    #region Hilfsmethoden

    public async Task<Zeitbereich?> GetZeitbereichAsync(int MeisterschaftsID)
    {
        var zb = await _localDbContext.TblMeisterschaftens
            .Where(w => w.Id == MeisterschaftsID)
            .Select(s => new Zeitbereich { Von = s.Beginn, Bis = s.Ende })
            .FirstOrDefaultAsync();

        return zb;
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

    private async Task<List<Models.Web.TblDbchangeLog>> getDifferentRecordsAsync()
    {
        // var localDBChanges = await _localDbContext.TblDbchangeLogs.Where(w => w.Tablename != "init").ToListAsync();
        // var webDBChanges = await _webDbContext.TblDbchangeLogs.Where(w => w.Tablename != "init").ToListAsync();
        //
        // var differences = webDBChanges
        //     .Where(w => !localDBChanges.Any(w2 => w2.Tablename == "init" && w2.Zeitstempel == w.Zeitstempel))
        //     .ToList();

        // Lade alle relevanten Einträge aus der lokalen und der Web-Datenbank
        var localDBChanges = await _localDbContext.TblDbchangeLogs
            .Where(w => w.Tablename != "init")
            .ToListAsync();

        var webDBChanges = await _webDbContext.TblDbchangeLogs
            .Where(w => w.Tablename != "init")
            .ToListAsync();

        // Finde die Unterschiede: Einträge, die in der Web-Datenbank sind, aber nicht in der lokalen Datenbank
        var differences = webDBChanges
            .Where(webEntry => !localDBChanges.Any(localEntry =>
                localEntry.Tablename == webEntry.Tablename &&
                localEntry.Zeitstempel == webEntry.Zeitstempel &&
                localEntry.Command == webEntry.Command))
            .ToList();
        
        return differences;
    }

    #endregion

    #region Settings und Splashscreen

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

    public async Task SaveSettingsLocalAsync(string sParametername, string sParametervalue)
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
            objLogWeb.Command = sb.ToString();
            ;
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
            sb.Append("where Parametername = '").Append(sParametername).Append("'");

            objLog.Changetype = "update";
            objLog.Command = sb.ToString();
            objLog.Tablename = "tblSettings";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;

            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "update";
            objLogWeb.Command = sb.ToString();
            ;
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
                    if (objMeisterSearch != null)
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
                    if (objMeisterSearch != null)
                    {
                        objMeisterSearch!.Aktiv = 1;
                        await _localDbContext.SaveChangesAsync();

                        AktiveMeisterschaft aktiveMeisterschaft = new();
                        aktiveMeisterschaft.ID = intID;
                        aktiveMeisterschaft.Bezeichnung = objMeisterSearch.Bezeichnung;
                        _commonService.AktiveMeisterschaft = aktiveMeisterschaft;
                    }

                    break;
            }
        }
    }

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

    public async Task UpdateLocalDBAsync()
    {
        List<Models.Web.TblDbchangeLog> lstDiff = await getDifferentRecordsAsync();
        List<Models.Web.TblDbchangeLog> lstDiffOrdered = lstDiff.OrderBy(o => o.Zeitstempel).ToList();
        string strSQL = string.Empty;

        foreach (var row in lstDiffOrdered)
        {
            //if (row.Tablename == "tblSettings") continue;

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

    #endregion

    #region Ergebniseingabe

    public async Task Delete9erRattenAsync(Int32 iID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var objNR = await _localDbContext.Tbl9erRattens
            .Where(w => w.Id == iID)
            .Select(s => s).SingleOrDefaultAsync();

        _localDbContext.Tbl9erRattens.Remove(objNR);
        await _localDbContext.SaveChangesAsync();

        sb.Append("delete from tbl9erRatten ");
        sb.Append("where ID = " + iID.ToString());
        strSQL = sb.ToString();

        objLog.Changetype = "delete";
        objLog.Command = strSQL;
        objLog.Tablename = "tbl9erRatten";
        objLog.Computername = Environment.MachineName;
        objLog.Zeitstempel = dtLogTimestamp;
        await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
        await _localDbContext.SaveChangesAsync();

        objLogWeb.Changetype = "delete";
        objLogWeb.Command = strSQL;
        objLogWeb.Tablename = "tbl9erRatten";
        objLogWeb.Computername = Environment.MachineName;
        objLogWeb.Zeitstempel = dtLogTimestamp;
        await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
        await _webDbContext.SaveChangesAsync();
    }

    public async Task DeleteSpielPokalAsync(Int32 iID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var objPokal = await _localDbContext.TblSpielPokals
            .Where(w => w.Id == iID)
            .Select(s => s).SingleOrDefaultAsync();

        if (objPokal != null)
        {
            _localDbContext.TblSpielPokals.Remove(objPokal);
            await _localDbContext.SaveChangesAsync();

            sb.Append("delete from tblSpielPokal ");
            sb.Append("where ID = " + iID.ToString());
            strSQL = sb.ToString();

            objLog.Changetype = "delete";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielPokal";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "delete";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielPokal";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteSpielSargKegelnAsync(Int32 iID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var objSarg = await _localDbContext.TblSpielSargKegelns
            .Where(w => w.Id == iID)
            .Select(s => s).SingleOrDefaultAsync();

        if (objSarg != null)
        {
            _localDbContext.TblSpielSargKegelns.Remove(objSarg);
            await _localDbContext.SaveChangesAsync();

            sb.Append("delete from tblSpielSargKegeln ");
            sb.Append("where ID = " + iID.ToString());
            strSQL = sb.ToString();

            objLog.Changetype = "delete";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielSargKegeln";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "delete";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielSargKegeln";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteSpiel6TageRennenAsync(Int32 iID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var obj6TR = await _localDbContext.TblSpiel6TageRennens
            .Where(w => w.Id == iID)
            .Select(s => s).SingleOrDefaultAsync();

        if (obj6TR != null)
        {
            _localDbContext.TblSpiel6TageRennens.Remove(obj6TR);
            await _localDbContext.SaveChangesAsync();

            sb.Append("delete from tblSpiel6TageRennen ");
            sb.Append("where ID = " + iID.ToString());
            strSQL = sb.ToString();

            objLog.Changetype = "delete";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpiel6TageRennen";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "delete";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpiel6TageRennen";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteSpielMeisterschaftAsync(Int32 iID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var objMeisterschaft = await _localDbContext.TblSpielMeisterschafts
            .Where(w => w.Id == iID)
            .Select(s => s).SingleOrDefaultAsync();

        if (objMeisterschaft != null)
        {
            _localDbContext.TblSpielMeisterschafts.Remove(objMeisterschaft);
            await _localDbContext.SaveChangesAsync();

            sb.Append("delete from tblSpielMeisterschaft ");
            sb.Append("where ID = " + iID.ToString());
            strSQL = sb.ToString();

            objLog.Changetype = "delete";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielMeisterschaft";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = DateTime.Now;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "delete";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielMeisterschaft";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteSpielBlitztunierAsync(Int32 iID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var objBlitz = await _localDbContext.TblSpielBlitztuniers
            .Where(w => w.Id == iID)
            .Select(s => s).SingleOrDefaultAsync();

        if (objBlitz != null)
        {
            _localDbContext.TblSpielBlitztuniers.Remove(objBlitz);
            await _localDbContext.SaveChangesAsync();

            sb.Append("delete from tblSpielBlitztunier ");
            sb.Append("where ID = " + iID.ToString());
            strSQL = sb.ToString();

            objLog.Changetype = "delete";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielBlitztunier";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "delete";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielBlitztunier";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteSpielKombimeisterschaftAsync(Int32 iID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var objKombi = await _localDbContext.TblSpielKombimeisterschafts
            .Where(w => w.Id == iID)
            .Select(s => s).SingleOrDefaultAsync();

        if (objKombi != null)
        {
            _localDbContext.TblSpielKombimeisterschafts.Remove(objKombi);
            await _localDbContext.SaveChangesAsync();

            sb.Append("delete from tblSpielKombimeisterschaft ");
            sb.Append("where ID = " + iID.ToString());
            strSQL = sb.ToString();

            objLog.Changetype = "delete";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielKombimeisterschaft";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "delete";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielKombimeisterschaft";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<AusgabeNeunerRatten>> GetAusgabe9erRattenAsync(int iSpieltagID)
    {
        List<AusgabeNeunerRatten> lstAusgabe = new();

        var lstNR = await _localDbContext.Vw9erRattens
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstNR)
        {
            AusgabeNeunerRatten objAusgabe = new();
            //objAusgabe.MeisterschaftsID = item.MeisterschaftsId;
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objAusgabe.Spielername = item.Spitzname;
            }
            else
            {
                objAusgabe.Spielername = item.Vorname;
            }

            objAusgabe.Neuner = item.Neuner;
            objAusgabe.Ratten = item.Ratten;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabePokal>> GetAusgabePokalAsync(int iSpieltagID)
    {
        List<AusgabePokal> lstAusgabe = new();

        var lstPokal = await _localDbContext.VwSpielPokals
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstPokal)
        {
            AusgabePokal objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objAusgabe.Spielername = item.Spitzname;
            }
            else
            {
                objAusgabe.Spielername = item.Vorname;
            }

            objAusgabe.Platzierung = item.Platzierung;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeSargkegeln>> GetAusgabeSargkegelnAsync(int iSpieltagID)
    {
        List<AusgabeSargkegeln> lstAusgabe = new();

        var lstSarg = await _localDbContext.VwSpielSargKegelns
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstSarg)
        {
            AusgabeSargkegeln objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objAusgabe.Spielername = item.Spitzname;
            }
            else
            {
                objAusgabe.Spielername = item.Vorname;
            }

            objAusgabe.Platzierung = item.Platzierung;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeSechsTageRennen>> GetAusgabe6TageRennenAsync(int iSpieltagID)
    {
        List<AusgabeSechsTageRennen> lstAusgabe = new();

        var lstNR = await _localDbContext.VwSpiel6TageRennens
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstNR)
        {
            AusgabeSechsTageRennen objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.Runden = item.Runden;
            objAusgabe.Punkte = item.Punkte;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeMeisterschaft>> GetAusgabeMeisterschaftAsync(int iSpieltagID)
    {
        List<AusgabeMeisterschaft> lstAusgabe = new();

        var lstM = await _localDbContext.VwSpielMeisterschafts
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstM)
        {
            AusgabeMeisterschaft objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.HolzSpieler1 = item.HolzSpieler1;
            objAusgabe.HolzSpieler2 = item.HolzSpieler2;
            objAusgabe.HinRueckrunde = item.HinRückrunde;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeBlitztunier>> GetAusgabeBlitztunierAsync(int iSpieltagID)
    {
        List<AusgabeBlitztunier> lstAusgabe = new();

        var lstBT = await _localDbContext.VwSpielBlitztuniers
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstBT)
        {
            AusgabeBlitztunier objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.PunkteSpieler1 = item.PunkteSpieler1;
            objAusgabe.PunkteSpieler2 = item.PunkteSpieler2;
            objAusgabe.HinRueckrunde = item.HinRückrunde;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeKombimeisterschaft>> GetAusgabeKombimeisterschaftAsync(int iSpieltagID)
    {
        List<AusgabeKombimeisterschaft> lstAusgabe = new();

        var lstKM = await _localDbContext.VwSpielKombimeisterschafts
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstKM)
        {
            AusgabeKombimeisterschaft objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.Spieler1Punkte3bis8 = item.Spieler1Punkte3bis8;
            objAusgabe.Spieler2Punkte3bis8 = item.Spieler2Punkte3bis8;
            objAusgabe.Spieler1Punkte5Kugeln = item.Spieler1Punkte5Kugeln;
            objAusgabe.Spieler2Punkte5Kugeln = item.Spieler2Punkte5Kugeln;
            objAusgabe.HinRueckrunde = item.HinRückrunde;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<NeunerRatten>> GetEingabe9erRattenAsync(int iSpieltagID)
    {
        List<NeunerRatten> lstEingabe = new();

        var lstNR = await _localDbContext.Vw9erRattens
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstNR)
        {
            NeunerRatten objEingabe = new();
            objEingabe.ID = item._9erRattenId;
            objEingabe.SpieltagID = item.SpieltagId;
            objEingabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objEingabe.Spielername = item.Spitzname;
            }
            else
            {
                objEingabe.Spielername = item.Vorname;
            }

            objEingabe.Neuner = item.Neuner;
            objEingabe.Ratten = item.Ratten;

            lstEingabe.Add(objEingabe);
        }

        return lstEingabe;
    }

    public async Task<List<SechsTageRennen>> GetEingabe6TageRennenAsync(int iSpieltagID)
    {
        List<SechsTageRennen> lstEingabe = new();

        var lstNR = await _localDbContext.VwSpiel6TageRennens
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstNR)
        {
            SechsTageRennen objEingabe = new();
            objEingabe.ID = item._6tageRennenId;
            objEingabe.SpieltagID = item.SpieltagId;
            objEingabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objEingabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objEingabe.Spieler1Name = item.Spieler1Vorname;
            }

            objEingabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objEingabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objEingabe.Spieler2Name = item.Spieler2Vorname;
            }

            objEingabe.Runden = item.Runden;
            objEingabe.Punkte = item.Punkte;

            lstEingabe.Add(objEingabe);
        }

        return lstEingabe;
    }

    public async Task<List<Meisterschaft>> GetEingabeMeisterschaftAsync(int iSpieltagID)
    {
        List<Meisterschaft> lstEingabe = new();

        var lstM = await _localDbContext.VwSpielMeisterschafts
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstM)
        {
            Meisterschaft objEingabe = new();
            objEingabe.ID = item.SpielMeisterschaftId;
            objEingabe.SpieltagID = item.SpieltagId;
            objEingabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objEingabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objEingabe.Spieler1Name = item.Spieler1Vorname;
            }

            objEingabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objEingabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objEingabe.Spieler2Name = item.Spieler2Vorname;
            }

            objEingabe.HolzSpieler1 = item.HolzSpieler1;
            objEingabe.HolzSpieler2 = item.HolzSpieler2;
            objEingabe.HinRueckrunde = item.HinRückrunde;

            lstEingabe.Add(objEingabe);
        }

        return lstEingabe;
    }

    public async Task<List<Blitztunier>> GetEingabeBlitztunierAsync(int iSpieltagID)
    {
        List<Blitztunier> lstEingabe = new();

        var lstBT = await _localDbContext.VwSpielBlitztuniers
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstBT)
        {
            Blitztunier objEingabe = new();
            objEingabe.ID = item.BlitztunierId;
            objEingabe.SpieltagID = item.SpieltagId;
            objEingabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objEingabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objEingabe.Spieler1Name = item.Spieler1Vorname;
            }

            objEingabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objEingabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objEingabe.Spieler2Name = item.Spieler2Vorname;
            }

            objEingabe.PunkteSpieler1 = item.PunkteSpieler1;
            objEingabe.PunkteSpieler2 = item.PunkteSpieler2;
            objEingabe.HinRueckrunde = item.HinRückrunde;

            lstEingabe.Add(objEingabe);
        }

        return lstEingabe;
    }

    public async Task<List<Kombimeisterschaft>> GetEingabeKombimeisterschaftAsync(int iSpieltagID)
    {
        List<Kombimeisterschaft> lstEingabe = new();

        var lstKM = await _localDbContext.VwSpielKombimeisterschafts
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstKM)
        {
            Kombimeisterschaft objEingabe = new();
            objEingabe.ID = item.KombimeisterschaftId;
            objEingabe.SpieltagID = item.SpieltagId;
            objEingabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objEingabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objEingabe.Spieler1Name = item.Spieler1Vorname;
            }

            objEingabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objEingabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objEingabe.Spieler2Name = item.Spieler2Vorname;
            }

            objEingabe.Spieler1Punkte3bis8 = item.Spieler1Punkte3bis8;
            objEingabe.Spieler2Punkte3bis8 = item.Spieler2Punkte3bis8;
            objEingabe.Spieler1Punkte5Kugeln = item.Spieler1Punkte5Kugeln;
            objEingabe.Spieler2Punkte5Kugeln = item.Spieler2Punkte5Kugeln;
            objEingabe.HinRueckrunde = item.HinRückrunde;

            lstEingabe.Add(objEingabe);
        }

        return lstEingabe;
    }

    public async Task<List<Pokal>> GetEingabePokalAsync(int iSpieltagID)
    {
        List<Pokal> lstEingabe = new();

        var lstPokal = await _localDbContext.VwSpielPokals
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstPokal)
        {
            Pokal objEingabe = new();
            objEingabe.ID = item.SpielPokalId;
            objEingabe.SpieltagID = item.SpieltagId;
            objEingabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objEingabe.Spielername = item.Spitzname;
            }
            else
            {
                objEingabe.Spielername = item.Vorname;
            }

            objEingabe.Platzierung = item.Platzierung;

            lstEingabe.Add(objEingabe);
        }

        return lstEingabe;
    }

    public async Task<List<Sargkegeln>> GetEingabeSargkegelnAsync(int iSpieltagID)
    {
        List<Sargkegeln> lstEingabe = new();

        var lstSarg = await _localDbContext.VwSpielSargKegelns
            .Where(w => w.SpieltagId == iSpieltagID)
            .Select(s => s)
            .ToListAsync();

        foreach (var item in lstSarg)
        {
            Sargkegeln objEingabe = new();
            objEingabe.ID = item.SpielSargKegelnId;
            objEingabe.SpieltagID = item.SpieltagId;
            objEingabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objEingabe.Spielername = item.Spitzname;
            }
            else
            {
                objEingabe.Spielername = item.Vorname;
            }

            objEingabe.Platzierung = item.Platzierung;

            lstEingabe.Add(objEingabe);
        }

        return lstEingabe;
    }

    public async Task SaveEingabeAsync(int iMeisterschaftsID, DateTime dtSpieltag, string sSpiel, object oEingabeliste,
        List<int> oIDsToDelete = null)
    {
        int intSpieltagID = -1;

        using (var transaction = await _localDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                intSpieltagID = await SaveSpieltagAsync(iMeisterschaftsID, dtSpieltag);

                switch (sSpiel)
                {
                    case "9er/Ratten":
                        var lstEingabeNR = oEingabeliste as List<NeunerRatten>;
                        foreach (var item in lstEingabeNR)
                            await Save9erRattenAsync(intSpieltagID, item.SpielerID, item.Neuner, item.Ratten);

                        if (oIDsToDelete != null && oIDsToDelete.Count > 0)
                            foreach (var del in oIDsToDelete)
                                await Delete9erRattenAsync(del);

                        break;
                    case "6-Tage-Rennen":
                        var lstEingabe6TR = oEingabeliste as List<SechsTageRennen>;
                        foreach (var item in lstEingabe6TR)
                            await SaveSpiel6TageRennenAsync(intSpieltagID, item.Spieler1ID, item.Spieler2ID,
                                item.Spielnr, item.Runden, item.Punkte);

                        if (oIDsToDelete != null && oIDsToDelete.Count > 0)
                            foreach (var del in oIDsToDelete)
                                await DeleteSpiel6TageRennenAsync(del);

                        break;
                    case "Pokal":
                        var lstEingabeP = oEingabeliste as List<Pokal>;
                        foreach (var item in lstEingabeP)
                            await SaveSpielPokalAsync(intSpieltagID, item.SpielerID, item.Platzierung);

                        if (oIDsToDelete != null && oIDsToDelete.Count > 0)
                            foreach (var del in oIDsToDelete)
                                await DeleteSpielPokalAsync(del);

                        break;
                    case "Sargkegeln":
                        var lstEingabeS = oEingabeliste as List<Sargkegeln>;
                        foreach (var item in lstEingabeS)
                            await SaveSpielSargKegelnAsync(intSpieltagID, item.SpielerID, item.Platzierung);

                        if (oIDsToDelete != null && oIDsToDelete.Count > 0)
                            foreach (var del in oIDsToDelete)
                                await DeleteSpielSargKegelnAsync(del);

                        break;
                    case "Meisterschaft":
                        var lstEingabeM = oEingabeliste as List<Meisterschaft>;
                        foreach (var item in lstEingabeM)
                            await SaveSpielMeisterschaftAsync(intSpieltagID, item.Spieler1ID, item.Spieler2ID,
                                item.HinRueckrunde, item.HolzSpieler1, item.HolzSpieler2);

                        if (oIDsToDelete != null && oIDsToDelete.Count > 0)
                            foreach (var del in oIDsToDelete)
                                await DeleteSpielMeisterschaftAsync(del);

                        break;
                    case "Blitztunier":
                        var lstEingabeBT = oEingabeliste as List<Blitztunier>;
                        foreach (var item in lstEingabeBT)
                            await SaveSpielBlitztunierAsync(intSpieltagID, item.Spieler1ID, item.Spieler2ID,
                                item.HinRueckrunde, item.PunkteSpieler1, item.PunkteSpieler2);

                        if (oIDsToDelete != null && oIDsToDelete.Count > 0)
                            foreach (var del in oIDsToDelete)
                                await DeleteSpielBlitztunierAsync(del);

                        break;
                    case "Kombimeisterschaft":
                        var lstEingabeKM = oEingabeliste as List<Kombimeisterschaft>;
                        foreach (var item in lstEingabeKM)
                            await SaveSpielKombimeisterschaftAsync(intSpieltagID, item.Spieler1ID, item.Spieler2ID,
                                item.HinRueckrunde, item.Spieler1Punkte3bis8, item.Spieler1Punkte5Kugeln,
                                item.Spieler2Punkte3bis8, item.Spieler2Punkte5Kugeln);

                        if (oIDsToDelete != null && oIDsToDelete.Count > 0)
                            foreach (var del in oIDsToDelete)
                                await DeleteSpielKombimeisterschaftAsync(del);

                        break;
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ViewManager.ShowErrorWindow("DBService", "SaveEingabeAsync", ex.ToString());
            }
        }
    }

    public async Task<Int32> SaveSpieltagAsync(Int32 iMeisterschaftsID, DateTime dtSpieltag)
    {
        Int32 intID = 0;
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtSpieltagSearch = dtSpieltag.Date;

        var checkSpieltag = await _localDbContext.TblSpieltags
            .Where(w => w.Spieltag == dtSpieltagSearch && w.MeisterschaftsId == iMeisterschaftsID)
            .Select(s => s)
            .SingleOrDefaultAsync();


        if (checkSpieltag == null)
        {
            Models.Local.TblSpieltag objSpieltag = new();
            objSpieltag.MeisterschaftsId = iMeisterschaftsID;
            objSpieltag.Spieltag = dtSpieltag;
            objSpieltag.InBearbeitung = true;

            await _localDbContext.TblSpieltags.AddAsync(objSpieltag);
            await _localDbContext.SaveChangesAsync();
            intID = objSpieltag.Id;

            sb.Append("insert into tblSpieltag(ID, MeisterschaftsID, Spieltag, InBearbeitung) ");
            sb.Append("values(");
            sb.Append(intID.ToString()).Append(", ");
            sb.Append(iMeisterschaftsID.ToString()).Append(", ");
            sb.Append("'").Append(dtSpieltag.ToString("yyyyMMdd")).Append("', ");
            sb.Append("0)");
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpieltag";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = DateTime.Now;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpieltag";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = DateTime.Now;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            //intID = ((tblSpieltag)checkSpieltag).ID;
            intID = checkSpieltag.Id;

            // checkSpieltag.InBearbeitung = true;
            // await _localDbContext.SaveChangesAsync();
            //
            // sb.Append("update tblSpieltag ");
            // sb.Append("set InBearbeitung = 1 ");
            // sb.Append("where ID = ");
            // sb.Append(intID.ToString());
            // strSQL = sb.ToString();
            //
            // objLog.Changetype = "update";
            // objLog.Command = strSQL;
            // objLog.Tablename = "tblSpieltag";
            // objLog.Computername = Environment.MachineName;
            // objLog.Zeitstempel = DateTime.Now;
            // await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            // await _localDbContext.SaveChangesAsync();
            //
            // objLogWeb.Changetype = "update";
            // objLogWeb.Command = strSQL;
            // objLogWeb.Tablename = "tblSpieltag";
            // objLogWeb.Computername = Environment.MachineName;
            // objLogWeb.Zeitstempel = DateTime.Now;
            // await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            // await _webDbContext.SaveChangesAsync();
        }

        return intID;
    }

    public async Task<int> GetSpieltagIDAsync(DateTime dtSpieltag)
    {
        var dtSpieltagSearch = dtSpieltag.Date;
        var st = await _localDbContext.TblSpieltags
            .Where(w => w.Spieltag == dtSpieltagSearch)
            .Select(s => s.Id)
            .SingleOrDefaultAsync();

        return st;
    }

    public async Task<List<Spieltage>> GetSpieltagListAsync(int iMeisterschaftsID)
    {
        var lstSpieltage = await _localDbContext.TblSpieltags
            .OrderByDescending(o => o.Spieltag)
            .Where(w => w.MeisterschaftsId == iMeisterschaftsID)
            .Select(s => new Spieltage { Id = s.Id, Spieltag = s.Spieltag })
            .ToListAsync();

        return lstSpieltage;
    }

    public async Task Save9erRattenAsync(Int32 iSpieltagID, Int32 iSpielerID, Int32 iNeuner, Int32 iRatten)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var nr = await _localDbContext.Tbl9erRattens
            .Where(w => w.SpieltagId == iSpieltagID && w.SpielerId == iSpielerID)
            .SingleOrDefaultAsync();

        if (nr == null)
        {
            Models.Local.Tbl9erRatten objNR = new();
            objNR.SpieltagId = iSpieltagID;
            objNR.SpielerId = iSpielerID;
            objNR.Neuner = iNeuner;
            objNR.Ratten = iRatten;
            await _localDbContext.Tbl9erRattens.AddAsync(objNR);
            await _localDbContext.SaveChangesAsync();

            sb.Append("insert into tbl9erRatten(ID, SpieltagID, SpielerID, Neuner, Ratten) ");
            sb.Append("values (");
            sb.Append(objNR.Id.ToString()).Append(", ");
            sb.Append(iSpieltagID.ToString()).Append(", ");
            sb.Append(iSpielerID.ToString()).Append(", ");
            sb.Append(iNeuner.ToString()).Append(", ");
            sb.Append(iRatten.ToString()).Append(")");
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tbl9erRatten";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tbl9erRatten";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            if (nr.Neuner != iNeuner || nr.Ratten != iRatten)
            {
                nr.Neuner = iNeuner;
                nr.Ratten = iRatten;
                await _localDbContext.SaveChangesAsync();

                sb.Append("update tbl9erRatten ");
                sb.Append("set Neuner = " + iNeuner.ToString()).Append(", ");
                sb.Append("Ratten = " + iRatten.ToString()).Append(" ");
                sb.Append("where ID = " + nr.Id.ToString());
                strSQL = sb.ToString();

                objLog.Changetype = "update";
                objLog.Command = strSQL;
                objLog.Tablename = "tbl9erRatten";
                objLog.Computername = Environment.MachineName;
                objLog.Zeitstempel = DateTime.Now;
                await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                await _localDbContext.SaveChangesAsync();

                objLogWeb.Changetype = "update";
                objLogWeb.Command = strSQL;
                objLogWeb.Tablename = "tbl9erRatten";
                objLogWeb.Computername = Environment.MachineName;
                objLogWeb.Zeitstempel = DateTime.Now;
                await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task SaveSpiel6TageRennenAsync(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2,
        Int32 iSpielnummer, Int32 iRunden, Int32 iPunkte)
    {
        Int32 int6TageRennenID = -1;
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var str = await _localDbContext.TblSpiel6TageRennens
            .Where(w => w.SpieltagId == iSpieltagID && w.SpielerId1 == iSpielerID1 && w.SpielerId2 == iSpielerID2)
            .SingleOrDefaultAsync();

        if (str == null)
        {
            Models.Local.TblSpiel6TageRennen obj6TR = new();
            obj6TR.SpieltagId = iSpieltagID;
            obj6TR.SpielerId1 = iSpielerID1;
            obj6TR.SpielerId2 = iSpielerID2;
            obj6TR.Runden = iRunden;
            obj6TR.Punkte = iPunkte;
            obj6TR.Spielnummer = iSpielnummer;

            await _localDbContext.TblSpiel6TageRennens.AddAsync(obj6TR);
            await _localDbContext.SaveChangesAsync();
            int6TageRennenID = obj6TR.Id;

            sb.Append(
                "insert into tblSpiel6TageRennen(ID, SpieltagID, SpielerID1, SpielerID2, Runden, Punkte, Spielnummer) ");
            sb.Append("values (");
            sb.Append(int6TageRennenID.ToString()).Append(", ");
            sb.Append(iSpieltagID.ToString()).Append(", ");
            sb.Append(iSpielerID1.ToString()).Append(", ");
            sb.Append(iSpielerID2.ToString()).Append(", ");
            sb.Append(iRunden.ToString()).Append(", ");
            sb.Append(iPunkte.ToString()).Append(", ");
            sb.Append(iSpielnummer.ToString() + ")");
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpiel6TageRennen";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpiel6TageRennen";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            if (str.Runden != iRunden || str.Punkte != iPunkte)
            {
                str.Runden = iRunden;
                str.Punkte = iPunkte;
                await _localDbContext.SaveChangesAsync();

                sb.Append("update tblSpiel6TageRennen ");
                sb.Append("set Runden = " + iRunden.ToString()).Append(", ");
                sb.Append("Punkte = " + iPunkte.ToString()).Append(", ");
                sb.Append("Spielnummer = " + iSpielnummer.ToString()).Append(" ");
                sb.Append("where ID = " + str.Id.ToString());
                strSQL = sb.ToString();

                objLog.Changetype = "update";
                objLog.Command = strSQL;
                objLog.Tablename = "tblSpiel6TageRennen";
                objLog.Computername = Environment.MachineName;
                objLog.Zeitstempel = dtLogTimestamp;
                await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                await _localDbContext.SaveChangesAsync();


                objLogWeb.Changetype = "update";
                objLogWeb.Command = strSQL;
                objLogWeb.Tablename = "tblSpiel6TageRennen";
                objLogWeb.Computername = Environment.MachineName;
                objLogWeb.Zeitstempel = dtLogTimestamp;
                await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task SaveSpielMeisterschaftAsync(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2,
        Int32 iHinRueckrunde, Int32 iHolzSpieler1, Int32 iHolzSpieler2)
    {
        Int32 intSpielMeisterschaftsID = -1;
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var m = await _localDbContext.TblSpielMeisterschafts
            .Where(w => w.SpieltagId == iSpieltagID && w.SpielerId1 == iSpielerID1 && w.SpielerId2 == iSpielerID2)
            .SingleOrDefaultAsync();

        if (m == null)
        {
            Models.Local.TblSpielMeisterschaft objM = new();
            objM.SpieltagId = iSpieltagID;
            objM.SpielerId1 = iSpielerID1;
            objM.SpielerId2 = iSpielerID2;
            objM.HolzSpieler1 = iHolzSpieler1;
            objM.HolzSpieler2 = iHolzSpieler2;
            objM.HinRückrunde = iHinRueckrunde;

            await _localDbContext.TblSpielMeisterschafts.AddAsync(objM);
            await _localDbContext.SaveChangesAsync();
            intSpielMeisterschaftsID = objM.Id;

            sb.Append(
                "insert into tblSpielMeisterschaft(ID, SpieltagID, SpielerID1, SpielerID2, HolzSpieler1, HolzSpieler2, HinRückrunde) ");
            sb.Append("values (");
            sb.Append(intSpielMeisterschaftsID.ToString()).Append(", ");
            sb.Append(iSpieltagID.ToString()).Append(", ");
            sb.Append(iSpielerID1.ToString()).Append(", ");
            sb.Append(iSpielerID2.ToString()).Append(", ");
            sb.Append(iHolzSpieler1.ToString()).Append(", ");
            sb.Append(iHolzSpieler2.ToString()).Append(", ");
            sb.Append(iHinRueckrunde.ToString());
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielMeisterschaft";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielMeisterschaft";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            if (m.HolzSpieler1 != iHolzSpieler1 || m.HolzSpieler2 != iHolzSpieler2)
            {
                m.HolzSpieler1 = iHolzSpieler1;
                m.HolzSpieler2 = iHolzSpieler2;
                m.HinRückrunde = iHinRueckrunde;

                await _localDbContext.SaveChangesAsync();

                sb.Append("update tblSpielMeisterschaft ");
                sb.Append("set HolzSpieler1 = " + iHolzSpieler1.ToString()).Append(", ");
                sb.Append("HolzSpieler2 = " + iHolzSpieler2.ToString()).Append(", ");
                sb.Append("HinRückrunde = " + iHinRueckrunde.ToString()).Append(" ");
                sb.Append("where ID = " + m.Id.ToString());
                strSQL = sb.ToString();

                objLog.Changetype = "update";
                objLog.Command = strSQL;
                objLog.Tablename = "tblSpielMeisterschaft";
                objLog.Computername = Environment.MachineName;
                objLog.Zeitstempel = dtLogTimestamp;
                await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                await _localDbContext.SaveChangesAsync();

                objLogWeb.Changetype = "update";
                objLogWeb.Command = strSQL;
                objLogWeb.Tablename = "tblSpielMeisterschaft";
                objLogWeb.Computername = Environment.MachineName;
                objLogWeb.Zeitstempel = dtLogTimestamp;
                await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task SaveSpielBlitztunierAsync(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2,
        Int32 iHinRueckrunde, Int32 iPunkteSpieler1, Int32 iPunkteSpieler2)
    {
        Int32 intSpielBlitztunierID = -1;
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var bt = await _localDbContext.TblSpielBlitztuniers
            .Where(w => w.SpieltagId == iSpieltagID && w.SpielerId1 == iSpielerID1 && w.SpielerId2 == iSpielerID2)
            .SingleOrDefaultAsync();

        if (bt == null)
        {
            Models.Local.TblSpielBlitztunier objBT = new();
            objBT.SpieltagId = iSpieltagID;
            objBT.SpielerId1 = iSpielerID1;
            objBT.SpielerId2 = iSpielerID2;
            objBT.PunkteSpieler1 = iPunkteSpieler1;
            objBT.PunkteSpieler2 = iPunkteSpieler2;
            objBT.HinRückrunde = iHinRueckrunde;

            await _localDbContext.TblSpielBlitztuniers.AddAsync(objBT);
            await _localDbContext.SaveChangesAsync();
            intSpielBlitztunierID = objBT.Id;

            sb.Append(
                "insert into tblSpielBlitztunier(ID, SpieltagID, SpielerID1, SpielerID2, PunkteSpieler1, PunkteSpieler2, HinRückrunde) ");
            sb.Append("values (");
            sb.Append(intSpielBlitztunierID.ToString()).Append(", ");
            sb.Append(iSpieltagID.ToString()).Append(", ");
            sb.Append(iSpielerID1.ToString()).Append(", ");
            sb.Append(iSpielerID2.ToString()).Append(", ");
            sb.Append(iPunkteSpieler1.ToString()).Append(", ");
            sb.Append(iPunkteSpieler2.ToString()).Append(", ");
            sb.Append(iHinRueckrunde.ToString()).Append(")");
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielBlitztunier";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielBlitztunier";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            if (bt.PunkteSpieler1 != iPunkteSpieler1 || bt.PunkteSpieler2 != iPunkteSpieler2)
            {
                bt.PunkteSpieler1 = iPunkteSpieler1;
                bt.PunkteSpieler2 = iPunkteSpieler2;
                bt.HinRückrunde = iHinRueckrunde;

                await _localDbContext.SaveChangesAsync();

                sb.Append("update tblSpielBlitztunier ");
                sb.Append("set PunkteSpieler1 = " + iPunkteSpieler1.ToString()).Append(", ");
                sb.Append("PunkteSpieler2 = " + iPunkteSpieler2.ToString()).Append(", ");
                sb.Append("HinRückrunde = " + iHinRueckrunde.ToString()).Append(" ");
                sb.Append("where ID = " + bt.Id.ToString());
                strSQL = sb.ToString();

                objLog.Changetype = "update";
                objLog.Command = strSQL;
                objLog.Tablename = "tblSpielBlitztunier";
                objLog.Computername = Environment.MachineName;
                objLog.Zeitstempel = dtLogTimestamp;
                await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                await _localDbContext.SaveChangesAsync();

                objLogWeb.Changetype = "update";
                objLogWeb.Command = strSQL;
                objLogWeb.Tablename = "tblSpielBlitztunier";
                objLogWeb.Computername = Environment.MachineName;
                objLogWeb.Zeitstempel = dtLogTimestamp;
                await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task SaveSpielKombimeisterschaftAsync(Int32 iSpieltagID, Int32 iSpielerID1, Int32 iSpielerID2,
        Int32 iHinRueckrunde, Int32 iSpieler13bis8,
        Int32 iSpieler15Kugeln, Int32 iSpieler23bis8, Int32 iSpieler25Kugeln)
    {
        Int32 intSpielKombimeisterschaftsID = -1;
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var km = await _localDbContext.TblSpielKombimeisterschafts
            .Where(w => w.SpieltagId == iSpieltagID && w.SpielerId1 == iSpielerID1 && w.SpielerId2 == iSpielerID2)
            .SingleOrDefaultAsync();

        if (km == null)
        {
            Models.Local.TblSpielKombimeisterschaft objKombi = new();
            objKombi.SpieltagId = iSpieltagID;
            objKombi.SpielerId1 = iSpielerID1;
            objKombi.SpielerId2 = iSpielerID2;
            objKombi.Spieler1Punkte3bis8 = iSpieler13bis8;
            objKombi.Spieler1Punkte5Kugeln = iSpieler15Kugeln;
            objKombi.Spieler2Punkte3bis8 = iSpieler23bis8;
            objKombi.Spieler2Punkte5Kugeln = iSpieler25Kugeln;
            objKombi.HinRückrunde = iHinRueckrunde;

            await _localDbContext.TblSpielKombimeisterschafts.AddAsync(objKombi);
            await _localDbContext.SaveChangesAsync();
            intSpielKombimeisterschaftsID = objKombi.Id;

            sb.Append(
                "insert into tblSpielKombimeisterschaft(ID, SpieltagID, SpielerID1, SpielerID2, Spieler1Punkte3bis8, Spieler1Punkte5Kugeln, Spieler2Punkte3bis8, Spieler2Punkte5Kugeln, HinRückrunde) ");
            sb.Append("values (");
            sb.Append(intSpielKombimeisterschaftsID.ToString()).Append(", ");
            sb.Append(iSpieltagID.ToString()).Append(", ");
            sb.Append(iSpielerID1.ToString()).Append(", ");
            sb.Append(iSpielerID2.ToString()).Append(", ");
            sb.Append(iSpieler13bis8.ToString()).Append(", ");
            sb.Append(iSpieler15Kugeln).Append(", ");
            sb.Append(iSpieler23bis8).Append(", ");
            sb.Append(iSpieler25Kugeln).Append(", ");
            sb.Append(iHinRueckrunde.ToString()).Append(")");
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielKombimeisterschaft";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielKombimeisterschaft";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            if (km.Spieler1Punkte3bis8 != iSpieler13bis8 || km.Spieler1Punkte5Kugeln != iSpieler15Kugeln ||
                km.Spieler2Punkte3bis8 != iSpieler23bis8 || km.Spieler2Punkte5Kugeln != iSpieler25Kugeln ||
                km.HinRückrunde != iHinRueckrunde)
            {
                km.Spieler1Punkte3bis8 = iSpieler13bis8;
                km.Spieler1Punkte5Kugeln = iSpieler15Kugeln;
                km.Spieler2Punkte3bis8 = iSpieler23bis8;
                km.Spieler2Punkte5Kugeln = iSpieler25Kugeln;
                km.HinRückrunde = iHinRueckrunde;
                await _localDbContext.SaveChangesAsync();

                sb.Append("update tblSpielKombimeisterschaft ");
                sb.Append("set Spieler1Punkte3bis8 = " + iSpieler13bis8.ToString()).Append(", ");
                sb.Append("Spieler1Punkte5Kugeln = " + iSpieler15Kugeln.ToString()).Append(", ");
                sb.Append("Spieler2Punkte3bis8 = " + iSpieler23bis8.ToString()).Append(", ");
                sb.Append("Spieler2Punkte5Kugeln = " + iSpieler25Kugeln.ToString()).Append(", ");
                sb.Append("HinRückrunde = " + iHinRueckrunde.ToString()).Append(" ");
                sb.Append("where ID = " + km.Id.ToString());
                strSQL = sb.ToString();

                objLog.Changetype = "update";
                objLog.Command = strSQL;
                objLog.Tablename = "tblSpielKombimeisterschaft";
                objLog.Computername = Environment.MachineName;
                objLog.Zeitstempel = dtLogTimestamp;
                await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                await _localDbContext.SaveChangesAsync();

                objLogWeb.Changetype = "update";
                objLogWeb.Command = strSQL;
                objLogWeb.Tablename = "tblSpielKombimeisterschaft";
                objLogWeb.Computername = Environment.MachineName;
                objLogWeb.Zeitstempel = dtLogTimestamp;
                await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task SaveSpielPokalAsync(Int32 iSpieltagID, Int32 iSpielerID, Int32 iPlatzierung)
    {
        Int32 intSpielPokalID = -1;
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var p = await _localDbContext.TblSpielPokals
            .Where(w => w.SpieltagId == iSpieltagID && w.SpielerId == iSpielerID)
            .SingleOrDefaultAsync();

        if (p == null)
        {
            Models.Local.TblSpielPokal objPokal = new();
            objPokal.SpieltagId = iSpieltagID;
            objPokal.SpielerId = iSpielerID;
            objPokal.Platzierung = iPlatzierung;

            await _localDbContext.TblSpielPokals.AddAsync(objPokal);
            await _localDbContext.SaveChangesAsync();
            intSpielPokalID = objPokal.Id;

            sb.Append("insert into tblSpielPokal(ID, SpieltagID, SpielerID, Platzierung) ");
            sb.Append("values (");
            sb.Append(intSpielPokalID.ToString()).Append(", ");
            sb.Append(iSpieltagID.ToString()).Append(", ");
            sb.Append(iSpielerID.ToString()).Append(", ");
            sb.Append(iPlatzierung.ToString()).Append(")");
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielPokal";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielPokal";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            if (p.Platzierung != iPlatzierung)
            {
                p.Platzierung = iPlatzierung;
                await _localDbContext.SaveChangesAsync();

                sb.Append("update tblSpielPokal ");
                sb.Append("set Platzierung = " + iPlatzierung.ToString()).Append(" ");
                sb.Append("where ID = " + p.Id.ToString());
                strSQL = sb.ToString();

                objLog.Changetype = "update";
                objLog.Command = strSQL;
                objLog.Tablename = "tblSpielPokal";
                objLog.Computername = Environment.MachineName;
                objLog.Zeitstempel = dtLogTimestamp;
                await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                await _localDbContext.SaveChangesAsync();

                objLogWeb.Changetype = "update";
                objLogWeb.Command = strSQL;
                objLogWeb.Tablename = "tblSpielPokal";
                objLogWeb.Computername = Environment.MachineName;
                objLogWeb.Zeitstempel = dtLogTimestamp;
                await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task SaveSpielSargKegelnAsync(Int32 iSpieltagID, Int32 iSpielerID, Int32 iPlatzierung)
    {
        Int32 intSpielSargkegelnID = -1;
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;
        Models.Local.TblDbchangeLog objLog = new();
        Models.Web.TblDbchangeLog objLogWeb = new();
        DateTime dtLogTimestamp = DateTime.Now;

        var sk = await _localDbContext.TblSpielSargKegelns
            .Where(w => w.SpieltagId == iSpieltagID && w.SpielerId == iSpielerID)
            .SingleOrDefaultAsync();

        if (sk == null)
        {
            Models.Local.TblSpielSargKegeln objSarg = new();
            objSarg.SpieltagId = iSpieltagID;
            objSarg.SpielerId = iSpielerID;
            objSarg.Platzierung = iPlatzierung;

            _localDbContext.TblSpielSargKegelns.AddAsync(objSarg);
            await _localDbContext.SaveChangesAsync();
            intSpielSargkegelnID = objSarg.Id;

            sb.Append("insert into tblSpielSargKegeln(ID, SpieltagID, SpielerID, Platzierung) ");
            sb.Append("values (");
            sb.Append(intSpielSargkegelnID.ToString()).Append(", ");
            sb.Append(iSpieltagID.ToString()).Append(", ");
            sb.Append(iSpielerID.ToString()).Append(", ");
            sb.Append("0)");
            strSQL = sb.ToString();

            objLog.Changetype = "insert";
            objLog.Command = strSQL;
            objLog.Tablename = "tblSpielSargKegeln";
            objLog.Computername = Environment.MachineName;
            objLog.Zeitstempel = dtLogTimestamp;
            await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
            await _localDbContext.SaveChangesAsync();

            objLogWeb.Changetype = "insert";
            objLogWeb.Command = strSQL;
            objLogWeb.Tablename = "tblSpielSargKegeln";
            objLogWeb.Computername = Environment.MachineName;
            objLogWeb.Zeitstempel = dtLogTimestamp;
            await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
            await _webDbContext.SaveChangesAsync();
        }
        else
        {
            if (sk.Platzierung != iPlatzierung)
            {
                sk.Platzierung = iPlatzierung;
                await _localDbContext.SaveChangesAsync();

                sb.Append("update tblSpielSargKegeln ");
                sb.Append("set Platzierung = " + iPlatzierung.ToString()).Append(" ");
                sb.Append("where ID = " + sk.Id.ToString());
                strSQL = sb.ToString();

                objLog.Changetype = "update";
                objLog.Command = strSQL;
                objLog.Tablename = "tblSpielSargKegeln";
                objLog.Computername = Environment.MachineName;
                objLog.Zeitstempel = DateTime.Now;
                await _localDbContext.TblDbchangeLogs.AddAsync(objLog);
                await _localDbContext.SaveChangesAsync();

                objLogWeb.Changetype = "update";
                objLogWeb.Command = strSQL;
                objLogWeb.Tablename = "tblSpielSargKegeln";
                objLogWeb.Computername = Environment.MachineName;
                objLogWeb.Zeitstempel = dtLogTimestamp;
                await _webDbContext.TblDbchangeLogs.AddAsync(objLogWeb);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }

    #endregion

    #region Ergebnisausgabe

    public async Task<List<ErgebnisKombimeisterschaftKreuztabelle>> GetErgebnisKombimeisterschaftAsync(
        Int32 iMeisterschaftsID)
    {
        List<ErgebnisKombimeisterschaftKreuztabelle>
            lstKombi = new List<ErgebnisKombimeisterschaftKreuztabelle>();

        try
        {
            var items = await _localDbContext.VwErgebnisKombimeisterschafts
                .Where(w => w.MeisterschaftsId == iMeisterschaftsID)
                .Select(s => s).ToListAsync();

            foreach (var objM in items)
            {
                ErgebnisKombimeisterschaftKreuztabelle objKombi = new();
                objKombi.MeisterschaftsID = objM.MeisterschaftsId;
                objKombi.Spieler1ID = objM.SpielerId1;
                if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                {
                    objKombi.Spieler1Name = objM.Spieler1Spitzname;
                }
                else
                {
                    objKombi.Spieler1Name = objM.Spieler1Vorname;
                }

                objKombi.Spieler2ID = objM.SpielerId2;
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
                ;
                objKombi.HinRueckrunde = objM.HinRückrunde;

                lstKombi.Add(objKombi);
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetErgebnisKombimeisterschaft", ex.Message);
        }

        return lstKombi;
    }

    public async Task<List<ErgebnisMeisterschaftKreuztabelle>> GetErgebnisMeisterschaftAsync(Int32 iMeisterschaftsID)
    {
        List<ErgebnisMeisterschaftKreuztabelle> lstMeister = new();

        try
        {
            var items = await _localDbContext.VwErgebnisKurztuniers
                .Where(w => w.MeisterschaftsId == iMeisterschaftsID)
                .Select(s => s).ToListAsync();

            foreach (var objM in items)
            {
                ErgebnisMeisterschaftKreuztabelle objMeister = new();
                objMeister.MeisterschaftsID = objM.MeisterschaftsId;
                objMeister.Spieler1ID = objM.SpielerId1;
                if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                {
                    objMeister.Spieler1Name = objM.Spieler1Spitzname;
                }
                else
                {
                    objMeister.Spieler1Name = objM.Spieler1Vorname;
                }

                objMeister.Spieler2ID = objM.SpielerId2;
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetErgebnisMeisterschaft", ex.Message);
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
                            ErgebnisMeisterschaftKreuztabelle objMeister = new();
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


    public async Task<List<ErgebnisKurztunierKreuztabelle>> GetErgebnisKurztunierAsync(Int32 iMeisterschaftsID)
    {
        List<ErgebnisKurztunierKreuztabelle> lstKurz = new();

        try
        {
            var items = await _localDbContext.VwErgebnisKurztuniers
                .Where(w => w.MeisterschaftsId == iMeisterschaftsID)
                .Select(s => s).ToListAsync();

            foreach (var objM in items)
            {
                ErgebnisKurztunierKreuztabelle objKurz = new();
                objKurz.MeisterschaftsID = objM.MeisterschaftsId;
                objKurz.Spieler1ID = objM.SpielerId1;
                if (!string.IsNullOrEmpty(objM.Spieler1Spitzname))
                {
                    objKurz.Spieler1Name = objM.Spieler1Spitzname;
                }
                else
                {
                    objKurz.Spieler1Name = objM.Spieler1Vorname;
                }

                objKurz.Spieler2ID = objM.SpielerId2;
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetErgebnisKurztunier", ex.Message);
        }

        //Testdaten, falls keine echten Daten vorhanden
        if (lstKurz.Count == 0)
        {
            for (Int32 i = 0; i <= 1; i++)
            {
                for (Int32 j = 1; j <= 6; j++)
                {
                    for (Int32 k = 1; k <= 6; k++)
                    {
                        if (j != k)
                        {
                            ErgebnisKurztunierKreuztabelle objKurz = new();
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

    public async Task<List<AusgabeNeunerRatten>> Get9erRattenFromSpieltageAsync(List<int> lstIDs)
    {
        List<AusgabeNeunerRatten> lstAusgabe = new();

        var lst9erRatten = await _localDbContext.Vw9erRattens
            .OrderByDescending(o => o.Spieltag)
            .Where(w => lstIDs.Contains(w.SpieltagId))
            .Select(s => s).ToListAsync();

        foreach (var item in lst9erRatten)
        {
            AusgabeNeunerRatten objAusgabe = new();
            //objAusgabe.MeisterschaftsID = item.MeisterschaftsId;
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objAusgabe.Spielername = item.Spitzname;
            }
            else
            {
                objAusgabe.Spielername = item.Vorname;
            }

            objAusgabe.Neuner = item.Neuner;
            objAusgabe.Ratten = item.Ratten;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeSechsTageRennen>> Get6TageRennenFromSpieltageAsync(List<int> lstIDs)
    {
        List<AusgabeSechsTageRennen> lstAusgabe = new();

        var lst6TageRennen = await _localDbContext.VwSpiel6TageRennens
            .OrderByDescending(o => o.Spieltag)
            .Where(w => lstIDs.Contains(w.SpieltagId))
            .Select(s => s).ToListAsync();

        foreach (var item in lst6TageRennen)
        {
            AusgabeSechsTageRennen objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.Runden = item.Runden;
            objAusgabe.Punkte = item.Punkte;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabePokal>> GetPokalFromSpieltageAsync(List<int> lstIDs)
    {
        List<AusgabePokal> lstAusgabe = new();

        var lstPokal = await _localDbContext.VwSpielPokals
            .OrderByDescending(o => o.Spieltag)
            .Where(w => lstIDs.Contains(w.SpieltagId))
            .Select(s => s).ToListAsync();

        foreach (var item in lstPokal)
        {
            AusgabePokal objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objAusgabe.Spielername = item.Spitzname;
            }
            else
            {
                objAusgabe.Spielername = item.Vorname;
            }

            objAusgabe.Platzierung = item.Platzierung;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeSargkegeln>> GetSargkegelnFromSpieltageAsync(List<int> lstIDs)
    {
        List<AusgabeSargkegeln> lstAusgabe = new();

        var lstSarg = await _localDbContext.VwSpielSargKegelns
            .OrderByDescending(o => o.Spieltag)
            .Where(w => lstIDs.Contains(w.SpieltagId))
            .Select(s => s).ToListAsync();

        foreach (var item in lstSarg)
        {
            AusgabeSargkegeln objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.SpielerID = item.SpielerId;
            if (!string.IsNullOrEmpty(item.Spitzname))
            {
                objAusgabe.Spielername = item.Spitzname;
            }
            else
            {
                objAusgabe.Spielername = item.Vorname;
            }

            objAusgabe.Platzierung = item.Platzierung;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeMeisterschaft>> GetMeisterschaftFromSpieltageAsync(List<int> lstIDs)
    {
        List<AusgabeMeisterschaft> lstAusgabe = new();

        var lstMeister = await _localDbContext.VwSpielMeisterschafts
            .OrderByDescending(o => o.Spieltag)
            .Where(w => lstIDs.Contains(w.SpieltagId))
            .Select(s => s).ToListAsync();

        foreach (var item in lstMeister)
        {
            AusgabeMeisterschaft objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.HolzSpieler1 = item.HolzSpieler1;
            objAusgabe.HolzSpieler2 = item.HolzSpieler2;
            objAusgabe.HinRueckrunde = item.HinRückrunde;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeKombimeisterschaft>> GetKombimeisterschaftFromSpieltageAsync(List<int> lstIDs)
    {
        List<AusgabeKombimeisterschaft> lstAusgabe = new();

        var lstMeister = await _localDbContext.VwSpielKombimeisterschafts
            .OrderByDescending(o => o.Spieltag)
            .Where(w => lstIDs.Contains(w.SpieltagId))
            .Select(s => s).ToListAsync();

        foreach (var item in lstMeister)
        {
            AusgabeKombimeisterschaft objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.Spieler1Punkte3bis8 = item.Spieler1Punkte3bis8;
            objAusgabe.Spieler2Punkte3bis8 = item.Spieler2Punkte3bis8;
            objAusgabe.Spieler1Punkte5Kugeln = item.Spieler1Punkte5Kugeln;
            objAusgabe.Spieler2Punkte5Kugeln = item.Spieler2Punkte5Kugeln;
            objAusgabe.HinRueckrunde = item.HinRückrunde;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    public async Task<List<AusgabeBlitztunier>> GetBlitztunierFromSpieltageAsync(List<int> lstIDs)
    {
        List<AusgabeBlitztunier> lstAusgabe = new();

        var lstBlitz = await _localDbContext.VwSpielBlitztuniers
            .OrderByDescending(o => o.Spieltag)
            .Where(w => lstIDs.Contains(w.SpieltagId))
            .Select(s => s).ToListAsync();

        foreach (var item in lstBlitz)
        {
            AusgabeBlitztunier objAusgabe = new();
            objAusgabe.SpieltagID = item.SpieltagId;
            objAusgabe.Spieltag = item.Spieltag;
            objAusgabe.Spieler1ID = item.SpielerId1;
            if (!string.IsNullOrEmpty(item.Spieler1Spitzname))
            {
                objAusgabe.Spieler1Name = item.Spieler1Spitzname;
            }
            else
            {
                objAusgabe.Spieler1Name = item.Spieler1Vorname;
            }

            objAusgabe.Spieler2ID = item.SpielerId2;
            if (!string.IsNullOrEmpty(item.Spieler2Spitzname))
            {
                objAusgabe.Spieler2Name = item.Spieler2Spitzname;
            }
            else
            {
                objAusgabe.Spieler2Name = item.Spieler2Vorname;
            }

            objAusgabe.PunkteSpieler1 = item.PunkteSpieler1;
            objAusgabe.PunkteSpieler2 = item.PunkteSpieler2;
            objAusgabe.HinRueckrunde = item.HinRückrunde;

            lstAusgabe.Add(objAusgabe);
        }

        return lstAusgabe;
    }

    #endregion

    #region Mitgliederverwaltung

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

    public async Task<List<EmailListe>> GetEMaillisteAsync(bool bAktiv = false)
    {
        List<EmailListe> lstEMails = new List<EmailListe>();

        try
        {
            if (bAktiv)
            {
                var lst = await (_localDbContext.TblMitglieders
                    .Where(w => w.PassivSeit == null && w.Email != null)
                    .Select(s => new { s.Vorname, s.Nachname, s.Email })).ToListAsync();
                //lstEMails = lst.ToList();

                foreach (var item in lst)
                {
                    EmailListe objEMail = new EmailListe();
                    objEMail.Name = item.Nachname + ", " + item.Vorname;
                    objEMail.EMail = item.Email;
                    lstEMails.Add(objEMail);
                }
            }
            else
            {
                var lst = await (_localDbContext.TblMitglieders
                    .Where(w => w.Email != null)
                    .Select(s => new { s.Vorname, s.Nachname, s.Email })).ToListAsync();
                //lstEMails = lst.ToList();

                foreach (var item in lst)
                {
                    EmailListe objEMail = new EmailListe();
                    objEMail.Name = item.Nachname + ", " + item.Vorname;
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

    #endregion

    #region Meisterschaftsverwaltung

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
            ViewManager.ShowErrorWindow("DBService", "GetMeisterschaftenAsync", ex.ToString());
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

    public async Task<string> GetMeisterschaftstypFromMeisterschaftByIDAsync(int iID)
    {
        string strMT = "n/a";

        try
        {
            var m = await _localDbContext.TblMeisterschaftens
                .Where(w => w.Id == iID)
                .Select(s => s).FirstOrDefaultAsync();

            if (m != null)
            {
                var mt = await _localDbContext.TblMeisterschaftstyps
                    .Where(w => w.Id == m.MeisterschaftstypId)
                    .Select(s => s).FirstOrDefaultAsync();

                if (mt != null)
                {
                    strMT = mt.Meisterschaftstyp;
                }
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetMeisterschaftstypFromMeisterschaftByIDAsync", ex.ToString());
        }

        return strMT;
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

    public async Task<string> GetMeisterschaftsjahrAsync(Int32 iMeisterschaftsID)
    {
        string strJahr = string.Empty;

        try
        {
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

    public async Task SaveTeilnehmerAsync(Int32 iMeisterschaftsID, Int32 iTeilnehmerID)
    {
        StringBuilder sb = new StringBuilder();
        string strSQL = string.Empty;

        Models.Local.TblTeilnehmer objTeilnehmer = new();
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

        Models.Local.TblDbchangeLog objLog = new();
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

        Models.Web.TblDbchangeLog objLogWeb = new();
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
                    await SaveSettingsLocalAsync("AktiveMeisterschaft", intID.ToString());

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

                    // ---------- WebDB
                    // Models.Web.TblMeisterschaften objWebMeister =
                    //     _mapper.Map<Models.Web.TblMeisterschaften>(objMeisterNeu);
                    // await _webDbContext.TblMeisterschaftens.AddAsync(objWebMeister);

                    Models.Web.TblMeisterschaften objMeisterWebNeu = new();
                    objMeisterWebNeu.Id = intID;
                    objMeisterWebNeu.Bezeichnung = currentMeisterschaft.Bezeichnung;
                    objMeisterWebNeu.Beginn = currentMeisterschaft.Beginn;
                    if (currentMeisterschaft.Ende.HasValue)
                    {
                        objMeisterWebNeu.Ende = currentMeisterschaft.Ende.Value;
                    }
                    else
                    {
                        objMeisterWebNeu.Ende = null;
                    }

                    objMeisterWebNeu.MeisterschaftstypId = currentMeisterschaft.MeisterschaftstypID;
                    objMeisterWebNeu.Aktiv = 1;
                    await _webDbContext.TblMeisterschaftens.AddAsync(objMeisterWebNeu);

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

                        await SaveSettingsLocalAsync("AktiveMeisterschaft", intID.ToString());
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

    #endregion

    #region Statistiken

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

    public async Task<List<Statistik9er>> GetStatistik9erAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null)
    {
        List<Statistik9er> lst9er = new();

        try
        {
            var list9er = await _localDbContext.VwStatistik9ers
                .OrderBy(o => o.Spieltag)
                .Select(s => new { s.MeisterschaftsId, s.Spieltag, s.Neuner, s.Nachname, s.Vorname })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    list9er = list9er.Where(w => w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    list9er = list9er.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    list9er = list9er.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
                    break;
                case 3: //Gesamt
                    break;
            }

            var lst = (from l in list9er
                group l by new
                {
                    l.MeisterschaftsId,
                    l.Spieltag,
                    l.Nachname,
                    l.Vorname
                }
                into g
                select new
                {
                    MeisterschaftsID = g.Key.MeisterschaftsId,
                    Spieltag = g.Key.Spieltag,
                    Neuner = g.Sum(s => s.Neuner),
                    Nachname = g.Key.Nachname,
                    Vorname = g.Key.Vorname
                }).ToList();

            //var debug = lst.ToList();

            Int32 intIndex = -1;
            string strSpieler;
            foreach (var item in lst)
            {
                strSpieler = item.Nachname + ", " + item.Vorname;
                intIndex = lst9er.FindIndex(f => f.Spieler == strSpieler);
                if (intIndex == -1)
                {
                    Statistik9er obj9er = new();
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistik9er", ex.ToString());
        }

        var lst9erSort = lst9er
            .OrderByDescending(o => o.Gesamt)
            .ToList();

        return lst9erSort;
    }

    public async Task<List<StatistikRatten>> GetStatistikRattenAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null)
    {
        List<StatistikRatten> lstRatten = new();

        try
        {
            var listRatten = await _localDbContext.VwStatistikRattens
                .OrderBy(o => o.Spieltag)
                .Select(s => new { s.MeisterschaftsId, s.Spieltag, s.Ratten, s.Nachname, s.Vorname })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    listRatten = listRatten.Where(w =>
                        w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    listRatten = listRatten.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    listRatten = listRatten.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
                    break;
                case 3: //Gesamt
                    break;
            }

            var lst = (from l in listRatten
                group l by new
                {
                    l.MeisterschaftsId,
                    l.Spieltag,
                    l.Nachname,
                    l.Vorname
                }
                into g
                select new
                {
                    MeisterschaftsID = g.Key.MeisterschaftsId,
                    Spieltag = g.Key.Spieltag,
                    Ratten = g.Sum(s => s.Ratten),
                    Nachname = g.Key.Nachname,
                    Vorname = g.Key.Vorname
                }).ToList();

            //var debug = lst.ToList();

            Int32 intIndex = -1;
            string strSpieler;
            foreach (var item in lst)
            {
                strSpieler = item.Nachname + ", " + item.Vorname;
                intIndex = lstRatten.FindIndex(f => f.Spieler == strSpieler);
                if (intIndex == -1)
                {
                    StatistikRatten objRatten = new();
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistikRattenAsync", ex.ToString());
        }

        var lstRattenSort = lstRatten
            .OrderByDescending(o => o.Gesamt)
            .ToList();

        return lstRattenSort;
    }

    public async Task<List<StatistikPokal>> GetStatistikPokalAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null)
    {
        List<StatistikPokal> lstPokal = new();

        try
        {
            var listPokal = await _localDbContext.VwStatistikPokals
                .OrderBy(o => o.Spieltag)
                .Select(s => new { s.MeisterschaftsId, s.Spieltag, s.Platzierung, s.Nachname, s.Vorname })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    listPokal = listPokal.Where(w =>
                        w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    listPokal = listPokal.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    listPokal = listPokal.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
                    break;
                case 3: //Gesamt
                    break;
            }

            //var debug = lst.ToList();

            Int32 intIndex = -1;
            string strSpieler;
            foreach (var item in listPokal)
            {
                strSpieler = item.Nachname + ", " + item.Vorname;
                intIndex = lstPokal.FindIndex(f => f.Spieler == strSpieler);
                if (intIndex == -1)
                {
                    StatistikPokal objPokal = new();
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistikPokalAsync", ex.ToString());
        }

        var lstPokalSort = lstPokal
            .OrderByDescending(o => o.Eins)
            .ThenByDescending(t => t.Zwei)
            .ToList();

        return lstPokalSort;
    }

    public async Task<List<StatistikSarg>> GetStatistikSargAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null)
    {
        List<StatistikSarg> lstSarg = new();

        try
        {
            var listSarg = await _localDbContext.VwStatistikSargs
                .OrderBy(o => o.Spieltag)
                .Select(s => new { s.MeisterschaftsId, s.Spieltag, s.Platzierung, s.Nachname, s.Vorname })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    listSarg = listSarg.Where(
                        w => w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    listSarg = listSarg.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    listSarg = listSarg.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
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
                    StatistikSarg objSarg = new();
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistikSargAsync", ex.ToString());
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

    public async Task<List<Statistik6TageRennenPlatzierung>> GetStatistik6TageRennenPlatzAsync(Int32 iZeitbereich,
        DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<Statistik6TageRennenPlatzierung> lst6TR = new();

        try
        {
            //Teil 1
            var list6TRS1 = await _localDbContext.VwStatistik6TageRennenSpieler1s
                .OrderBy(o => o.Spieltag)
                .ThenBy(t => t.Spielnummer)
                .ThenBy(t => t.Platz)
                .Select(s => new
                {
                    s.MeisterschaftsId, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.Nachname,
                    s.Vorname
                })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    list6TRS1 = list6TRS1.Where(w =>
                        w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    list6TRS1 = list6TRS1.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    list6TRS1 = list6TRS1.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
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
                    Statistik6TageRennenPlatzierung obj6TR = new();
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
            var list6TRS2 = await _localDbContext.VwStatistik6TageRennenSpieler2s
                .OrderBy(o => o.Spieltag)
                .ThenBy(t => t.Spielnummer)
                .ThenBy(t => t.Platz)
                .Select(s => new
                {
                    s.MeisterschaftsId, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.Nachname,
                    s.Vorname
                })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    list6TRS2 = list6TRS2.Where(w =>
                        w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    list6TRS2 = list6TRS2.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    list6TRS2 = list6TRS2.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
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
                    Statistik6TageRennenPlatzierung obj6TR = new();
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistik6TageRennenPlatzAsync", ex.ToString());
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

    public async Task<List<Statistik6TageRennenBesteMannschaft>> GetStatistik6TageRennenBesteMannschaftAsync(
        Int32 iZeitbereich, DateTime? dtVon = null, DateTime? dtBis = null)
    {
        List<Statistik6TageRennenBesteMannschaft> lst6TR = new();

        try
        {
            var list6TR = await _localDbContext.VwSpiel6TageRennens
                .OrderBy(o => o.Spieltag)
                .ThenBy(t => t.Spielnummer)
                .ThenBy(t => t.Platz)
                .Select(s => new
                {
                    s.MeisterschaftsId, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.SpielerId1,
                    s.Spieler1Nachname, s.Spieler1Vorname, s.SpielerId2, s.Spieler2Nachname, s.Spieler2Vorname
                })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    list6TR = list6TR.Where(w => w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    list6TR = list6TR.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    list6TR = list6TR.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
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
                Statistik6TageRennenBesteMannschaft obj6TR = new();

                if (string.Compare(item.Spieler1Nachname, item.Spieler2Nachname, true) < 0)
                {
                    strMannschaft = strSpieler1 + " | " + strSpieler2;
                    obj6TR.Mannschaft = strMannschaft;
                    obj6TR.Spieler1ID = item.SpielerId1;
                    obj6TR.Spieler2ID = item.SpielerId2;
                }
                else
                {
                    if (string.Compare(item.Spieler1Nachname, item.Spieler2Nachname, true) == 0)
                    {
                        if (string.Compare(item.Spieler1Vorname, item.Spieler2Vorname, true) < 0)
                        {
                            strMannschaft = strSpieler1 + " | " + strSpieler2;
                            obj6TR.Mannschaft = strMannschaft;
                            obj6TR.Spieler1ID = item.SpielerId1;
                            obj6TR.Spieler2ID = item.SpielerId2;
                        }
                        else
                        {
                            strMannschaft = strSpieler2 + " | " + strSpieler1;
                            obj6TR.Mannschaft = strMannschaft;
                            obj6TR.Spieler1ID = item.SpielerId2;
                            obj6TR.Spieler2ID = item.SpielerId1;
                        }
                    }
                    else
                    {
                        strMannschaft = strSpieler2 + " | " + strSpieler1;
                        obj6TR.Mannschaft = strMannschaft;
                        obj6TR.Spieler1ID = item.SpielerId2;
                        obj6TR.Spieler2ID = item.SpielerId1;
                    }
                }

                intIndex = lst6TR.FindIndex(f => f.Mannschaft == strMannschaft);
                if (intIndex == -1)
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
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistik6TageRennenBesteMannschaftAsync", ex.ToString());
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

    public async Task<Dictionary<string, List<Statistik6TageRennenBesteMannschaft>>>
        GetStatistik6TageRennenMannschaftMitgliedAsync(Int32 iZeitbereich, DateTime? dtVon = null,
            DateTime? dtBis = null)
    {
        List<Statistik6TageRennenBesteMannschaft> lstMannschaftA = new();
        List<Statistik6TageRennenBesteMannschaft> lstMannschaftB = new();
        Dictionary<string, List<Statistik6TageRennenBesteMannschaft>> dictMannschaft =
            new Dictionary<string, List<Statistik6TageRennenBesteMannschaft>>();

        try
        {
            var list6TR = await _localDbContext.VwSpiel6TageRennens
                .OrderBy(o => o.Spieltag)
                .ThenBy(t => t.Spielnummer)
                .ThenBy(t => t.Platz)
                .Select(s => new
                {
                    s.MeisterschaftsId, s.Spieltag, s.Spielnummer, s.Runden, s.Punkte, s.Platz, s.SpielerId1,
                    s.Spieler1Nachname, s.Spieler1Vorname, s.SpielerId2, s.Spieler2Nachname, s.Spieler2Vorname
                })
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    list6TR = list6TR.Where(w => w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    list6TR = list6TR.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    list6TR = list6TR.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
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
                if (!dictMannschaft.ContainsKey(strSpieler1))
                {
                    Statistik6TageRennenBesteMannschaft obj6TR_M_A = new();
                    obj6TR_M_A.Mannschaft = strMannschaftA;
                    obj6TR_M_A.Spieler1ID = item.SpielerId1;
                    obj6TR_M_A.Spieler2ID = item.SpielerId2;

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

                    List<Statistik6TageRennenBesteMannschaft> lstMannschaft = new();
                    lstMannschaft.Add(obj6TR_M_A);
                    dictMannschaft.Add(strSpieler1, lstMannschaft);
                }
                else
                {
                    intIndex = dictMannschaft[strSpieler1].FindIndex(f => f.Mannschaft == strMannschaftA);
                    if (intIndex == -1)
                    {
                        Statistik6TageRennenBesteMannschaft obj6TR_M_A = new();
                        obj6TR_M_A.Mannschaft = strMannschaftA;
                        obj6TR_M_A.Spieler1ID = item.SpielerId1;
                        obj6TR_M_A.Spieler2ID = item.SpielerId2;

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
                        Statistik6TageRennenBesteMannschaft obj6TR_M_B = new();
                        obj6TR_M_B.Mannschaft = strMannschaftB;
                        obj6TR_M_B.Spieler1ID = item.SpielerId2;
                        obj6TR_M_B.Spieler2ID = item.SpielerId1;

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

                        List<Statistik6TageRennenBesteMannschaft> lstMannschaft = new();
                        lstMannschaft.Add(obj6TR_M_B);
                        dictMannschaft.Add(strSpieler2, lstMannschaft);
                    }
                    else
                    {
                        intIndex = dictMannschaft[strSpieler2].FindIndex(f => f.Mannschaft == strMannschaftB);
                        if (intIndex == -1)
                        {
                            Statistik6TageRennenBesteMannschaft obj6TR_M_B = new();
                            obj6TR_M_B.Mannschaft = strMannschaftB;
                            obj6TR_M_B.Spieler1ID = item.SpielerId2;
                            obj6TR_M_B.Spieler2ID = item.SpielerId1;

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
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistik6TageRennenMannschaftMitgliedAsync", ex.ToString());
        }

        Dictionary<string, List<Statistik6TageRennenBesteMannschaft>> dictMannschaftTemp =
            new Dictionary<string, List<Statistik6TageRennenBesteMannschaft>>();
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

    public async Task<StatistikNeunerRattenKoenig> GetStatistik9erRattenAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null)
    {
        StatistikNeunerRattenKoenig objNRK = new();
        
        List<Statistik9erRatten> lst9erRatten = new();
        List<Statistik9erRatten> lst9erRattenSortiert = new();
        Dictionary<string, int> dicNeunerkönig = new Dictionary<string, int>();
        Dictionary<string, int> dicRattenkönig = new Dictionary<string, int>();

        try
        {
            var list9er = await _localDbContext.VwStatistik9ers
                .OrderBy(o => o.Spieltag)
                .ThenByDescending(t => t.Neuner)
                .Select(s => s)
                .ToListAsync();

            var listRatten = await _localDbContext.VwStatistikRattens
                .OrderBy(o => o.Spieltag)
                .ThenByDescending(t => t.Ratten)
                .Select(s => s)
                .ToListAsync();

            switch (iZeitbereich)
            {
                case 0: //laufende Meisterschaft
                    list9er = list9er.Where(w => w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    listRatten = listRatten.Where(w =>
                        w.MeisterschaftsId == _commonService.AktiveMeisterschaft.ID).ToList();
                    break;
                case 1: //letzte Meisterschaft
                    Int32 intID = await GetLetzteMeisterschaftsIDAsync();
                    list9er = list9er.Where(w => w.MeisterschaftsId == intID).ToList();
                    listRatten = listRatten.Where(w => w.MeisterschaftsId == intID).ToList();
                    break;
                case 2: //Zeitbereich
                    list9er = list9er.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
                    listRatten = listRatten.Where(w => w.Spieltag >= dtVon && w.Spieltag <= dtBis).ToList();
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
                    Statistik9erRatten objNR = new ();
                    objNR.MeisterschaftsID = item.MeisterschaftsId;
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
                        Statistik9erRatten objNR = new ();
                        objNR.MeisterschaftsID = item.MeisterschaftsId;
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

            lst9erRattenSortiert = lst9erRatten.OrderBy(o => o.Spieltag).ToList();
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("DBService", "GetStatistik9erRattenAsync", ex.ToString());
        }

        objNRK.lstStatistik9erRatten = lst9erRattenSortiert;
        objNRK.dictNeunerkönig = dicNeunerkönig;
        objNRK.dictRattenkönig = dicRattenkönig;
        return objNRK;
    }

    public async Task<List<StatistikSpielerSpieler>> GetStatistikSpielerSpielerAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null)
    {
        List<StatistikSpielerSpieler> lstStat = new();
        Dictionary<string, StatistikGUV> dicMeister = new Dictionary<string, StatistikGUV>();
        Dictionary<string, StatistikGUV> dicBlitz = new Dictionary<string, StatistikGUV>();

        var mitgliederList = await _localDbContext.TblMitglieders
            .Select(s => new { s.Id, s.Nachname, s.Vorname })
            .ToListAsync();

        foreach (var mitglied in mitgliederList)
        {
            StatistikSpielerSpieler objStat = new();
            objStat.Spielername = mitglied.Nachname + ", " + mitglied.Vorname;

            var meister = await _localDbContext.VwErgebnisMeisterschafts
                .Where(w => w.SpielerId1 == mitglied.Id || w.SpielerId2 == mitglied.Id)
                .ToListAsync();

            var blitz = await _localDbContext.VwErgebnisKurztuniers
                .Where(w => w.SpielerId1 == mitglied.Id || w.SpielerId2 == mitglied.Id)
                .ToListAsync();

            var kombi = await _localDbContext.VwErgebnisKombimeisterschafts
                .Where(w => w.SpielerId1 == mitglied.Id || w.SpielerId2 == mitglied.Id)
                .ToListAsync();

            foreach (var itemMeister in meister)
            {
                if (mitglied.Id != itemMeister.SpielerId1)
                {
                    string strKeySpieler1 = itemMeister.Spieler1Nachname + ", " + itemMeister.Spieler1Vorname;

                    if (objStat.dictMeisterschaft.ContainsKey(strKeySpieler1))
                    {
                        if (itemMeister.HolzSpieler1 > itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler1].Gewonnen++;

                        if (itemMeister.HolzSpieler1 == itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler1].Unentschieden++;

                        if (itemMeister.HolzSpieler1 < itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler1].Verloren++;
                    }
                    else
                    {
                        StatistikGUV objGUV = new();
                        objStat.dictMeisterschaft.Add(strKeySpieler1, objGUV);

                        if (itemMeister.HolzSpieler1 > itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler1].Gewonnen++;

                        if (itemMeister.HolzSpieler1 == itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler1].Unentschieden++;

                        if (itemMeister.HolzSpieler1 < itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler1].Verloren++;
                    }
                }

                if (mitglied.Id != itemMeister.SpielerId2)
                {
                    string strKeySpieler2 = itemMeister.Spieler2Nachname + ", " + itemMeister.Spieler2Vorname;

                    if (objStat.dictMeisterschaft.ContainsKey(strKeySpieler2))
                    {
                        if (itemMeister.HolzSpieler1 < itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler2].Gewonnen++;

                        if (itemMeister.HolzSpieler1 == itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler2].Unentschieden++;

                        if (itemMeister.HolzSpieler1 > itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler2].Verloren++;
                    }
                    else
                    {
                        StatistikGUV objGUV = new();
                        objStat.dictMeisterschaft.Add(strKeySpieler2, objGUV);

                        if (itemMeister.HolzSpieler1 < itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler2].Gewonnen++;

                        if (itemMeister.HolzSpieler1 == itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler2].Unentschieden++;

                        if (itemMeister.HolzSpieler1 > itemMeister.HolzSpieler2)
                            objStat.dictMeisterschaft[strKeySpieler2].Verloren++;
                    }
                }
            }

            foreach (var itemBlitz in blitz)
            {
                if (mitglied.Id != itemBlitz.SpielerId1)
                {
                    string strKeySpieler1 = itemBlitz.Spieler1Nachname + ", " + itemBlitz.Spieler1Vorname;

                    if (objStat.dictBlitztunier.ContainsKey(strKeySpieler1))
                    {
                        if (itemBlitz.PunkteSpieler1 == 2)
                            objStat.dictBlitztunier[strKeySpieler1].Gewonnen++;

                        if (itemBlitz.PunkteSpieler1 == 1)
                            objStat.dictBlitztunier[strKeySpieler1].Unentschieden++;

                        if (itemBlitz.PunkteSpieler1 == 0)
                            objStat.dictBlitztunier[strKeySpieler1].Verloren++;
                    }
                    else
                    {
                        StatistikGUV objGUV = new();
                        objStat.dictBlitztunier.Add(strKeySpieler1, objGUV);

                        if (itemBlitz.PunkteSpieler1 == 2)
                            objStat.dictBlitztunier[strKeySpieler1].Gewonnen++;

                        if (itemBlitz.PunkteSpieler1 == 1)
                            objStat.dictBlitztunier[strKeySpieler1].Unentschieden++;

                        if (itemBlitz.PunkteSpieler1 == 0)
                            objStat.dictBlitztunier[strKeySpieler1].Verloren++;
                    }
                }

                if (mitglied.Id != itemBlitz.SpielerId2)
                {
                    string strKeySpieler2 = itemBlitz.Spieler2Nachname + ", " + itemBlitz.Spieler2Vorname;

                    if (objStat.dictBlitztunier.ContainsKey(strKeySpieler2))
                    {
                        if (itemBlitz.PunkteSpieler2 == 2)
                            objStat.dictBlitztunier[strKeySpieler2].Gewonnen++;

                        if (itemBlitz.PunkteSpieler2 == 1)
                            objStat.dictBlitztunier[strKeySpieler2].Unentschieden++;

                        if (itemBlitz.PunkteSpieler2 == 0)
                            objStat.dictBlitztunier[strKeySpieler2].Verloren++;
                    }
                    else
                    {
                        StatistikGUV objGUV = new();
                        objStat.dictBlitztunier.Add(strKeySpieler2, objGUV);

                        if (itemBlitz.PunkteSpieler2 == 2)
                            objStat.dictBlitztunier[strKeySpieler2].Gewonnen++;

                        if (itemBlitz.PunkteSpieler2 == 1)
                            objStat.dictBlitztunier[strKeySpieler2].Unentschieden++;

                        if (itemBlitz.PunkteSpieler2 == 0)
                            objStat.dictBlitztunier[strKeySpieler2].Verloren++;
                    }
                }
            }

            foreach (var itemKombi in kombi)
            {
                if (mitglied.Id != itemKombi.SpielerId1)
                {
                    string strKeySpieler1 = itemKombi.Spieler1Nachname + ", " + itemKombi.Spieler1Vorname;

                    if (objStat.dictKombimeisterschaft.ContainsKey(strKeySpieler1))
                    {
                        //3 bis 8
                        if(itemKombi.Spieler1Punkte3bis8 > itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict3bis8[strKeySpieler1].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 == itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict3bis8[strKeySpieler1].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 < itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict3bis8[strKeySpieler1].Verloren++;
                        
                        //5 Kugeln
                        if(itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict5Kugeln[strKeySpieler1].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict5Kugeln[strKeySpieler1].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict5Kugeln[strKeySpieler1].Verloren++;
                        
                        //Gesamt
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dictGesamt[strKeySpieler1].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dictGesamt[strKeySpieler1].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dictGesamt[strKeySpieler1].Verloren++;
                    }
                    else
                    {
                        StatistikSpielerKombimeisterschaft objStatKombi = new();
                        objStat.dictKombimeisterschaft.Add(strKeySpieler1, objStatKombi);
                        
                        StatistikGUV objGUV = new();
                        objStat.dictKombimeisterschaft[strKeySpieler1].dict3bis8.Add(strKeySpieler1, objGUV);
                        objStat.dictKombimeisterschaft[strKeySpieler1].dict5Kugeln.Add(strKeySpieler1, objGUV);
                        objStat.dictKombimeisterschaft[strKeySpieler1].dictGesamt.Add(strKeySpieler1, objGUV);
                        
                        //3 bis 8
                        if (itemKombi.Spieler1Punkte3bis8 > itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict3bis8[strKeySpieler1].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 == itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict3bis8[strKeySpieler1].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 < itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict3bis8[strKeySpieler1].Verloren++;
                        
                        //5 Kugeln
                        if(itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict5Kugeln[strKeySpieler1].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict5Kugeln[strKeySpieler1].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dict5Kugeln[strKeySpieler1].Verloren++;
                        
                        //Gesamt
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dictGesamt[strKeySpieler1].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dictGesamt[strKeySpieler1].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler1].dictGesamt[strKeySpieler1].Verloren++;
                    }
                }
                
                if (mitglied.Id != itemKombi.SpielerId2)
                {
                    string strKeySpieler2 = itemKombi.Spieler2Nachname + ", " + itemKombi.Spieler2Vorname;
                    
                    if (objStat.dictKombimeisterschaft.ContainsKey(strKeySpieler2))
                    {
                        //3 bis 8
                        if(itemKombi.Spieler1Punkte3bis8 > itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict3bis8[strKeySpieler2].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 == itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict3bis8[strKeySpieler2].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 < itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict3bis8[strKeySpieler2].Verloren++;
                        
                        //5 Kugeln
                        if(itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict5Kugeln[strKeySpieler2].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict5Kugeln[strKeySpieler2].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict5Kugeln[strKeySpieler2].Verloren++;
                        
                        //Gesamt
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dictGesamt[strKeySpieler2].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dictGesamt[strKeySpieler2].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dictGesamt[strKeySpieler2].Verloren++;
                    }
                    else
                    {
                        StatistikSpielerKombimeisterschaft objStatKombi = new();
                        objStat.dictKombimeisterschaft.Add(strKeySpieler2, objStatKombi);
                        
                        StatistikGUV objGUV = new();
                        objStat.dictKombimeisterschaft[strKeySpieler2].dict3bis8.Add(strKeySpieler2, objGUV);
                        objStat.dictKombimeisterschaft[strKeySpieler2].dict5Kugeln.Add(strKeySpieler2, objGUV);
                        objStat.dictKombimeisterschaft[strKeySpieler2].dictGesamt.Add(strKeySpieler2, objGUV);
                        
                        //3 bis 8
                        if(itemKombi.Spieler1Punkte3bis8 > itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict3bis8[strKeySpieler2].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 == itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict3bis8[strKeySpieler2].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 < itemKombi.Spieler2Punkte3bis8)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict3bis8[strKeySpieler2].Verloren++;
                        
                        //5 Kugeln
                        if(itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict5Kugeln[strKeySpieler2].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict5Kugeln[strKeySpieler2].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dict5Kugeln[strKeySpieler2].Verloren++;
                        
                        //Gesamt
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln > itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dictGesamt[strKeySpieler2].Gewonnen++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln == itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dictGesamt[strKeySpieler2].Unentschieden++;
                        
                        if(itemKombi.Spieler1Punkte3bis8 + itemKombi.Spieler1Punkte5Kugeln < itemKombi.Spieler2Punkte3bis8 + itemKombi.Spieler2Punkte5Kugeln)
                            objStat.dictKombimeisterschaft[strKeySpieler2].dictGesamt[strKeySpieler2].Verloren++;
                    }
                }
            }
            
            lstStat.Add(objStat);
        }

        return lstStat;
    }
    
    #endregion
}