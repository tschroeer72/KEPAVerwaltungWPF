using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mime;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Helper;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Views;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class EMailRundmailViewModel : BaseViewModel
{
    public delegate string[] OnShowFileDialogHandlerType();
    public OnShowFileDialogHandlerType? DelShowFileDialog { get; set; }
    
    private readonly DBService _dbService;

    public EMailRundmailViewModel(DBService dbService)
    {
        _dbService = dbService;
        Titel = "Rundmail an die Mitglieder";
    }

    private async Task LoadAndSetData()
    {
        Emailliste = new ObservableCollection<EmailListe>(await _dbService.GetEMaillisteAsync(RundMailAlle));
    }

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(RundMailNurMitglieder))]
    private bool rundMailAlle = true;

    public bool RundMailNurMitglieder => !rundMailAlle;

    [ObservableProperty] private ObservableCollection<EmailListe> emailliste = new();
    [ObservableProperty] private ObservableCollection<EmailListe> selectedEmails = new();
    [ObservableProperty] private ObservableCollection<Anhaenge> anhaenge = new();
    [ObservableProperty] private ObservableCollection<Anhaenge> selectedAnhaenge = new();
    [ObservableProperty] private string weitererEmpfaenger = "";
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
    public async void RadioButtonClicked()
    {
        Emailliste = new ObservableCollection<EmailListe>(await _dbService.GetEMaillisteAsync(RundMailAlle));
    }

    [RelayCommand]
    public void EmfaengerLoeschen()
    {
        foreach (var email in SelectedEmails.ToList())
        {
            Emailliste.Remove(email);
        }

        SelectedEmails.Clear();
    }

    [RelayCommand]
    public void EmfaengerHinzufuegen()
    {
        EmailListe objAdd = new();
        objAdd.Name = "Sonstiger Empfänger";
        objAdd.EMail = WeitererEmpfaenger;
        Emailliste.Add(objAdd);
    }
    
    [RelayCommand]
    void ShowFileDialog()
    {
        var filenames = DelShowFileDialog?.Invoke();

        //if (filenames == null || string.IsNullOrEmpty(filenames)) return;

        // //if (Haus == null) { Haus = new(); }
        // Haus.Bild = File.ReadAllBytes(filename);
        // OnPropertyChanged(nameof(Haus));

        foreach (var file in filenames)
        {
            Anhaenge objAnhang = new ();
            objAnhang.Dokument = Path.GetFileName(file);
            objAnhang.Pfad = file;
            Anhaenge.Add(objAnhang);
        }
    }

    [RelayCommand]
    public void AnhaengeLoeschen()
    {
        foreach (var anhang in SelectedAnhaenge.ToList())
        {
            Anhaenge.Remove(anhang);
        }

        SelectedAnhaenge.Clear();
    }

    [RelayCommand]
    public void SendMail()
    {
        if (Betreff != string.Empty && Nachricht != string.Empty)
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                List<string> lstEMail = new List<string>();
                MailService objMail = new();

                objMail.SMTPServer = "w01bdc60.kasserver.com";
                objMail.SMTPUser = "m06abf11";
                objMail.SMTPPassword = "VWLzFXtgj3Lrom3fCgLT";
                objMail.SMTPPort = 587;
                objMail.TLS = true;

                objMail.Subject = Betreff.Trim();
                objMail.Body = Nachricht.Trim();
                objMail.From = "info@kegelgruppe-kepa.de";

                foreach (var item in Emailliste)
                {
                    lstEMail.Add(item.EMail);
                    //lstEMail.Add("t.schroeer@web.de");
                }

                objMail.To = lstEMail;

                foreach (var item in Anhaenge)
                {
                    System.Net.Mail.Attachment objAnhang =
                        new System.Net.Mail.Attachment(item.Pfad, MediaTypeNames.Application.Octet);

                    objMail.Attachment.Add(objAnhang);
                }

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