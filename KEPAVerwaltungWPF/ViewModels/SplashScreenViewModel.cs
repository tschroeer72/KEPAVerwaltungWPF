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
                case 1:
                    InitSteps = i + 1;
                    List<Mitgliederliste> lstListe = await _dbService.GetMitgliederlisteAsync();
                    
                    await Task.Delay(3000);
                    //((PictureBox)this.Controls.Find("picStep1", false)[0]).Image = Properties.Resources.checkbox_24;
                    Step1OK = true;
                    break;
                case 2:
                    InitSteps = i + 1;
                    dtDBLocal = await _dbService.GetDBVersionLocalAsync();
                    DBVersionLocal = dtDBLocal.ToString("yyyyMMddHHmm");
                    
                    await Task.Delay(3000);
                    //((PictureBox)this.Controls.Find("picStep2", false)[0]).Image = Properties.Resources.checkbox_24;
                    Step2OK = true;
                    break;
                case 3:
                    InitSteps = i + 1;
                    dtDBBackup = await _dbService.GetDBVersionWebAsync();
                    DBVersionWeb = dtDBBackup.ToString("yyyyMMddHHmm");

                    if (dtDBBackup > dtDBLocal)
                    {
                        //((PictureBox)this.Controls.Find("picStep4", false)[0]).Visible = true;
                        //((System.Windows.Forms.Label)this.Controls.Find("lblStep4", false)[0]).Visible = true;
                        Step4 = "Hole die aktuellen Daten ...";
                    }
                    else
                    {
                        Step4 = "Daten sind aktuell";
                    }

                    await Task.Delay(3000);
                    //((PictureBox)this.Controls.Find("picStep3", false)[0]).Image = Properties.Resources.checkbox_24;
                    Step3OK = true;
                    break;
                case 4:
                    InitSteps = i + 1;
                    if (Step4 != "Daten sind aktuell")
                    {
                        await _dbService.UpdateLocalDBAsync();
                    }

                    await Task.Delay(3000);
                    //((PictureBox)this.Controls.Find("picStep4", false)[0]).Image = Properties.Resources.checkbox_24;
                    Step4OK = true;
                    break;
                case 5:
                    InitSteps = i + 1;
                    //WENN Settings in der DB exitieren DANN laden SONST die Standardwerte speichern
                    // if (objDBStep5.ExistsSettings())
                    // {
                    //     objDBStep5.LoadSettings();
                    //
                    //     ClsDB objDB = new ClsDB();
                    //     Properties.Settings.Default.AktiveMeisterschaft = objDB.GetAktiveMeisterschaftsID();
                    //     objDB = null;
                    //     objDBStep5.SaveSettings();
                    // }
                    // else
                    // {
                    //     ClsDB objDB = new ClsDB();
                    //     Properties.Settings.Default.AktiveMeisterschaft = objDB.GetAktiveMeisterschaftsID();
                    //     objDB = null;
                    //     objDBStep5.SaveSettings();
                    // }

                    //((PictureBox)this.Controls.Find("picStep4", false)[0]).Image = Properties.Resources.checkbox_24;
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