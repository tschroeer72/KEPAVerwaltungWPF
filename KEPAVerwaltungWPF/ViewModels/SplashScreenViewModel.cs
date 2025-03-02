using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class SplashScreenViewModel : BaseViewModel
{
    private readonly DBService _dbService;

    public SplashScreenViewModel(DBService dbService)
    {
        Titel = "Kegelgruppe KEPA 1958 Verwaltung";

        _dbService = dbService;
    }

    private async Task StepsAsync()
    {
        DateTime dtDBLocal = DateTime.MinValue;
        DateTime dtDBBackup = DateTime.MinValue;

        InitSteps = 1;
        for (int i = 1; i <= 6; i++)
        {
            switch (i)
            {
                case 1: //Baue verbindung zur Datenbank auf ...
                    InitSteps = i + 1;
                    List<Mitgliederliste> lstListe = await _dbService.GetMitgliederlisteAsync();
                    
                    await Task.Delay(3000);
                    Step1OK = true;
                    break;
                case 2: //Datenbankversion Lokal:
                    InitSteps = i + 1;
                    dtDBLocal = await _dbService.GetDBVersionLocalAsync();
                    DBVersionLocal = dtDBLocal.ToString("yyyyMMddHHmm");
                    
                    await Task.Delay(3000);
                    Step2OK = true;
                    break;
                case 3://Datenbankversion Sicherung:
                    InitSteps = i + 1;
                    dtDBBackup = await _dbService.GetDBVersionWebAsync();
                    DBVersionWeb = dtDBBackup.ToString("yyyyMMddHHmm");

                    if (dtDBBackup > dtDBLocal)
                    {
                        Step4 = "Hole die aktuellen Daten ...";
                    }
                    else
                    {
                        Step4 = "Daten sind aktuell";
                    }

                    await Task.Delay(3000);
                    Step3OK = true;
                    break;
                case 4://Hole die aktuellen Daten ... <=> Daten sind aktuell
                    InitSteps = i + 1;
                    if (Step4 != "Daten sind aktuell")
                    {
                        await _dbService.UpdateLocalDBAsync();
                    }

                    await Task.Delay(3000);
                    Step4OK = true;
                    break;
                case 5: //Lade Einstellungen ...
                    InitSteps = i + 1;
                    //WENN Settings in der DB exitieren DANN laden SONST die Standardwerte speichern
                    if(await _dbService.ExistsSettingsWebAsync())
                    {
                        await _dbService.LoadSettingsWebAsync();
                    }
                    
                    await Task.Delay(3000);
                    Step5OK = true;
                    break;
                case 6:
                    await Task.Delay(3000);
                    InitSteps = i + 1;
                    break;
            }
        }
    }

    public async Task InitializeAsync()
    {
        ProgramVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        Copyright = "(c) 2025 by Thorsten Schröer";

        // object[] attributes = Assembly.GetExecutingAssembly()
        //     .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        // if (attributes.Length == 0)
        //     Copyright = "(c)";
        // else
        //     Copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

        await StepsAsync();
    }

    [ObservableProperty] private string programVersion = default;
    [ObservableProperty] private string copyright = default;
    [ObservableProperty] private string dBVersionLocal = default;
    [ObservableProperty] private string dBVersionWeb = default;
    [ObservableProperty] private int initSteps = 0;
    [ObservableProperty] private bool step1OK = false;
    [ObservableProperty] private bool step2OK = false;
    [ObservableProperty] private bool step3OK = false;
    [ObservableProperty] private bool step4OK = false;
    [ObservableProperty] private bool step5OK = false;
    [ObservableProperty] private string step4 = "Daten sind aktuell";
}