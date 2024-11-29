using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class MitgliedDetails : ObservableObject
{
    [ObservableProperty] private int iD;
    [ObservableProperty] private string vorname;
    [ObservableProperty] private string nachname;
    [ObservableProperty] private string spitzname;
    [ObservableProperty] private string straße;
    [ObservableProperty] private string pLZ;
    [ObservableProperty] private string ort;
    [ObservableProperty] private DateTime geburtsdatum;
    [ObservableProperty] private DateTime? mitgliedSeit;
    [ObservableProperty] private DateTime? passivSeit;
    [ObservableProperty] private Nullable<System.DateTime> ausgeschiedenAm;
    [ObservableProperty] private bool ehemaliger;
    [ObservableProperty] private string notizen;
    [ObservableProperty] private string bemerkungen;
    [ObservableProperty] private string anrede;
    [ObservableProperty] private string eMail;
    [ObservableProperty] private string telefonPrivat;
    [ObservableProperty] private string telefonFirma;
    [ObservableProperty] private string telefonMobil;
    [ObservableProperty] private string fax;
    [ObservableProperty] private Nullable<int> spAnz;
    [ObservableProperty] private Nullable<int> spGew;
    [ObservableProperty] private Nullable<int> spUn;
    [ObservableProperty] private Nullable<int> spVerl;
     [ObservableProperty] private Nullable<int> holzGes;
     [ObservableProperty] private Nullable<int> holzMax;
     [ObservableProperty] private Nullable<int> holzMin;
     [ObservableProperty] private string punkte;
     [ObservableProperty] private Nullable<int> platz;
     [ObservableProperty] private Nullable<int> turboDBNummer;
}