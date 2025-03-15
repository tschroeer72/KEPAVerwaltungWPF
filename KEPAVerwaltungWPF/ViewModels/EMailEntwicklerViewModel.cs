using System.Collections.ObjectModel;
using System.Net.Mime;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Helper;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class EMailEntwicklerViewModel : BaseViewModel
{
    private readonly DBService _dbService;

    public EMailEntwicklerViewModel(DBService dbService)
    {
        _dbService = dbService;
        Titel = "Versenden einer E-Mail an den Entwickler";
    }
    
    private async Task LoadAndSetData()
    {
        EmailKopie = new ObservableCollection<EmailListe>(await _dbService.GetEMaillisteAsync(true));
    }
    
    [ObservableProperty] private ObservableCollection<EmailListe> emailKopie = new();
    [ObservableProperty] private ObservableCollection<EmailListe> selectetEmailKopie = new();
    [ObservableProperty] private bool grundFehler = false;
    [ObservableProperty] private bool grundInfo = true;
    [ObservableProperty] private bool grundWunsch = false;
    [ObservableProperty] private string betreff = "";
    [ObservableProperty] private string nachricht = "";
    
    [RelayCommand]
    public void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            LoadAndSetData();
            IsViewModelLoaded = true;
        }
    }
    [RelayCommand]
    public void SendMail()
    {
        if (Betreff != string.Empty && Nachricht != string.Empty)
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                List<string> lstCCEMail = new List<string>();
                MailService objMail = new();

                objMail.SMTPServer = "w01bdc60.kasserver.com";
                objMail.SMTPUser = "m06aaec5";
                objMail.SMTPPassword = "BodnXC5Pt3pMwQE4jPCU";
                objMail.SMTPPort= 587;
                objMail.TLS = true;

                string strBetreff = "KEPAVerwaltung ";
                if (GrundFehler) strBetreff += "(FEHLER) " + Betreff.Trim();
                if (GrundInfo) strBetreff += "(INFO) " + Betreff.Trim();
                if (GrundWunsch) strBetreff += "(WUNSCH) " + Betreff.Trim();
                
                objMail.Subject = strBetreff;
                objMail.Body = Nachricht.Trim();
                objMail.From = "issue@kegelgruppe-kepa.de";

                foreach (var item in SelectetEmailKopie)
                {
                    lstCCEMail.Add(item.EMail);
                    //lstCCEMail.Add("t.schroeer@web.de");
                }

                List<string> lstTo = new List<string>();
                lstTo.Add("t.schroeer@web.de");
                objMail.To = lstTo;
                objMail.CC = lstCCEMail;

                objMail.Send();

                DelShowMainInfoFlyout?.Invoke("Mail wurde versendet");

                IsPageBusy = false;
            }
        }
        else
        {
            DelShowMainInfoFlyout?.Invoke("Betreff und Nachricht müssen gefüllt sein!");
        }
    }
}