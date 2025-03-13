using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.DTOs.Ausgabe;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Views;
using Microsoft.EntityFrameworkCore;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class ErgebniseingabeViewModel : BaseViewModel
{
    private readonly DBService _dbService;
    private readonly CommonService _commonService;

    public ErgebniseingabeViewModel(DBService dbService, CommonService commonService)
    {
        _dbService = dbService;
        _commonService = commonService;
        Titel = "Ergebniseingabe";
    }

    private void FillComboboxen()
    {
        Spiele.Add("9er/Ratten");
        Spiele.Add("6-Tage-Rennen");
        Spiele.Add("Pokal");
        Spiele.Add("Meisterschaft");
        Spiele.Add("Blitztunier");
        Spiele.Add("Kombimeisterschaft");
        Spiele.Add("Sargkegeln");

        SpielNummer.Add("1");
        SpielNummer.Add("2");
        SpielNummer.Add("3");
        SpielNummer.Add("4");
    }

    private async void LoadAndSetDataAsync()
    {
        try
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                FillComboboxen();
                AktiveMitglieder = new ObservableCollection<AktiveSpieler>(await _dbService.GetAktiveMitgliederAsync());
                SelectedDate = DateTime.Now.Date;
                //await RefreshOutputAsync();
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("ErgebniseingabeViewModel", "LoadAndSetData", ex.ToString());
        }
        finally
        {
            IsPageBusy = false;
        }
    }

    private bool IsPlayerInList(int iSpielerID, string sSpiel, object oList)
    {
        bool bolReturn = false;

        switch (sSpiel)
        {
            case "9er/Ratten":
                var lstNR = oList as List<NeunerRatten>;
                var nr = lstNR.Where(w => w.SpielerID == iSpielerID).ToList();
                bolReturn = nr.Count > 0;
                break;
            case "6-Tage-Rennen":
                var lst6TR = oList as List<SechsTageRennen>;
                var str = lst6TR.Where(w => w.Spieler1ID == iSpielerID || w.Spieler2ID == iSpielerID).ToList();
                bolReturn = str.Count > 0;
                break;
            case "Pokal":
                var lstP = oList as List<Pokal>;
                var p = lstP.Where(w => w.SpielerID == iSpielerID).ToList();
                bolReturn = p.Count > 0;
                break;
            case "Sargkegeln":
                var lstSK = oList as List<Sargkegeln>;
                var sk = lstSK.Where(w => w.SpielerID == iSpielerID).ToList();
                bolReturn = sk.Count > 0;
                break;
            case "Meisterschaft":
                var lstM = oList as List<Meisterschaft>;
                var m = lstM.Where(w => w.Spieler1ID == iSpielerID || w.Spieler2ID == iSpielerID).ToList();
                bolReturn = m.Count > 0;
                break;
            case "Blitztunier":
                var lstBT = oList as List<Blitztunier>;
                var bt = lstBT.Where(w => w.Spieler1ID == iSpielerID || w.Spieler2ID == iSpielerID).ToList();
                bolReturn = bt.Count > 0;
                break;
            case "Kombimeisterschaft":
                var lstKM = oList as List<Kombimeisterschaft>;
                var km = lstKM.Where(w => w.Spieler1ID == iSpielerID || w.Spieler2ID == iSpielerID).ToList();
                bolReturn = km.Count > 0;
                break;
        }

        return bolReturn;
    }

    private void AddSpieler6TR(AktiveSpieler oSpieler)
    {
        if (Spiel6TageRennen.Count == 0)
        {
            SechsTageRennen obj6TageRennen = new();
            obj6TageRennen.Spieler1ID = oSpieler.ID;
            obj6TageRennen.Spieler1Name = oSpieler.Anzeigename;
            obj6TageRennen.Spielnr = Convert.ToInt32(SelectedSpielNummer);
            Spiel6TageRennen.Add(obj6TageRennen);
            DataChanged = true;
        }
        else
        {
            int intIndex = Spiel6TageRennen.Count - 1;
            if (Spiel6TageRennen[intIndex].Spieler2ID <= 0)
            {
                if (!IsPlayerInList(oSpieler.ID, "6-Tage-Rennen", Spiel6TageRennen.ToList()))
                {
                    Spiel6TageRennen[intIndex].Spieler2ID = oSpieler.ID;
                    Spiel6TageRennen[intIndex].Spieler2Name = oSpieler.Anzeigename;
                    DataChanged = true;
                }
            }
            else
            {
                if (!IsPlayerInList(oSpieler.ID, "6-Tage-Rennen", Spiel6TageRennen.ToList()))
                {
                    SechsTageRennen obj6TageRennen = new();
                    obj6TageRennen.Spieler1ID = oSpieler.ID;
                    obj6TageRennen.Spieler1Name = oSpieler.Anzeigename;
                    obj6TageRennen.Spielnr = Convert.ToInt32(SelectedSpielNummer);
                    Spiel6TageRennen.Add(obj6TageRennen);
                    DataChanged = true;
                }
            }
        }
    }

    private void AddSpielerMeisterschaft(AktiveSpieler oSpieler)
    {
        if (SpielMeisterschaft.Count == 0)
        {
            Meisterschaft objMeister = new();
            objMeister.Spieler1ID = oSpieler.ID;
            objMeister.Spieler1Name = oSpieler.Anzeigename;
            objMeister.Spieler2ID = -1;
            objMeister.Spieler2Name = "";
            objMeister.HinRueckrunde = SelectedHinRueckrundeID;
            SpielMeisterschaft.Add(objMeister);
            DataChanged = true;
        }
        else
        {
            int intIndex = SpielMeisterschaft.Count - 1;
            if (SpielMeisterschaft[intIndex].Spieler2ID <= 0)
            {
                if (!IsPlayerInList(oSpieler.ID, "Meisterschaft", SpielMeisterschaft.ToList()))
                {
                    SpielMeisterschaft[intIndex].Spieler2ID = oSpieler.ID;
                    SpielMeisterschaft[intIndex].Spieler2Name = oSpieler.Anzeigename;
                    DataChanged = true;
                }
            }
            else
            {
                if (!IsPlayerInList(oSpieler.ID, "Meisterschaft", SpielMeisterschaft.ToList()))
                {
                    Meisterschaft objMeister = new();
                    objMeister.Spieler1ID = oSpieler.ID;
                    objMeister.Spieler1Name = oSpieler.Anzeigename;
                    objMeister.Spieler2ID = -1;
                    objMeister.Spieler2Name = "";
                    objMeister.HinRueckrunde = SelectedHinRueckrundeID;
                    SpielMeisterschaft.Add(objMeister);
                    DataChanged = true;
                }
            }
        }
    }

    private void AddSpielerKombimeisterschaft(AktiveSpieler oSpieler)
    {
        if (SpielKombimeisterschaft.Count == 0)
        {
            DTOs.Kombimeisterschaft objKombi = new();
            objKombi.Spieler1ID = oSpieler.ID;
            objKombi.Spieler1Name = oSpieler.Anzeigename;
            objKombi.Spieler2ID = -1;
            objKombi.Spieler2Name = "";
            objKombi.HinRueckrunde = SelectedHinRueckrundeID;
            SpielKombimeisterschaft.Add(objKombi);
            DataChanged = true;
        }
        else
        {
            int intIndex = SpielKombimeisterschaft.Count - 1;
            if (SpielKombimeisterschaft[intIndex].Spieler2ID <= 0)
            {
                if (!IsPlayerInList(oSpieler.ID, "Kombimeisterschaft", SpielKombimeisterschaft.ToList()))
                {
                    SpielKombimeisterschaft[intIndex].Spieler2ID = oSpieler.ID;
                    SpielKombimeisterschaft[intIndex].Spieler2Name = oSpieler.Anzeigename;
                    DataChanged = true;
                }
            }
            else
            {
                if (!IsPlayerInList(oSpieler.ID, "Kombimeisterschaft", SpielKombimeisterschaft.ToList()))
                {
                    DTOs.Kombimeisterschaft objKombi = new();
                    objKombi.Spieler1ID = oSpieler.ID;
                    objKombi.Spieler1Name = oSpieler.Anzeigename;
                    objKombi.Spieler2ID = -1;
                    objKombi.Spieler2Name = "";
                    objKombi.HinRueckrunde = SelectedHinRueckrundeID;
                    SpielKombimeisterschaft.Add(objKombi);
                    DataChanged = true;
                }
            }
        }
    }

    private void AddSpielerBlitztunier(AktiveSpieler oSpieler)
    {
        if (SpielBlitztunier.Count == 0)
        {
            DTOs.Blitztunier objBlitz = new();
            objBlitz.Spieler1ID = oSpieler.ID;
            objBlitz.Spieler1Name = oSpieler.Anzeigename;
            objBlitz.Spieler2ID = -1;
            objBlitz.Spieler2Name = "";
            objBlitz.HinRueckrunde = SelectedHinRueckrundeID;
            SpielBlitztunier.Add(objBlitz);
            DataChanged = true;
        }
        else
        {
            int intIndex = SpielBlitztunier.Count - 1;
            if (SpielBlitztunier[intIndex].Spieler2ID <= 0)
            {
                if (!IsPlayerInList(oSpieler.ID, "Blitztunier", SpielBlitztunier.ToList()))
                {
                    SpielBlitztunier[intIndex].Spieler2ID = oSpieler.ID;
                    SpielBlitztunier[intIndex].Spieler2Name = oSpieler.Anzeigename;
                    DataChanged = true;
                }
            }
            else
            {
                if (!IsPlayerInList(oSpieler.ID, "Blitztunier", SpielBlitztunier.ToList()))
                {
                    DTOs.Blitztunier objBlitz = new();
                    objBlitz.Spieler1ID = oSpieler.ID;
                    objBlitz.Spieler1Name = oSpieler.Anzeigename;
                    objBlitz.Spieler2ID = -1;
                    objBlitz.Spieler2Name = "";
                    objBlitz.HinRueckrunde = SelectedHinRueckrundeID;
                    SpielBlitztunier.Add(objBlitz);
                    DataChanged = true;
                }
            }
        }
    }

    public void HandleDrop(object droppedData, string targetGridName)
    {
        switch (targetGridName)
        {
            case "dgAktiveMitglieder":
                switch (SelectedSpiel)
                {
                    case "9er/Ratten":
                        if (droppedData is NeunerRatten dataNR)
                        {
                            if (SpielNeunerRatten.Contains(dataNR))
                            {
                                if (dataNR.ID > 0) LstIDsToDelete.Add(dataNR.ID);
                                SpielNeunerRatten.Remove(dataNR);
                                DataChanged = true;
                            }

                            if (SpielNeunerRatten.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CalEnabled = true;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "6-Tage-Rennen":
                        if (droppedData is SechsTageRennen data6TR)
                        {
                            if (Spiel6TageRennen.Contains(data6TR))
                            {
                                if (data6TR.ID > 0) LstIDsToDelete.Add(data6TR.ID);
                                Spiel6TageRennen.Remove(data6TR);
                                DataChanged = true;
                            }

                            if (Spiel6TageRennen.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CalEnabled = true;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Pokal":
                        if (droppedData is Pokal dataP)
                        {
                            if (SpielPokal.Contains(dataP))
                            {
                                if (dataP.ID > 0) LstIDsToDelete.Add(dataP.ID);
                                SpielPokal.Remove(dataP);
                                DataChanged = true;
                            }

                            if (SpielPokal.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CalEnabled = true;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Sargkegeln":
                        if (droppedData is Sargkegeln dataS)
                        {
                            if (SpielSargkegeln.Contains(dataS))
                            {
                                if (dataS.ID > 0) LstIDsToDelete.Add(dataS.ID);
                                SpielSargkegeln.Remove(dataS);
                                DataChanged = true;
                            }

                            if (SpielSargkegeln.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CalEnabled = true;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Meisterschaft":
                        if (droppedData is Meisterschaft dataM)
                        {
                            if (SpielMeisterschaft.Contains(dataM))
                            {
                                if (dataM.ID > 0) LstIDsToDelete.Add(dataM.ID);
                                SpielMeisterschaft.Remove(dataM);
                                DataChanged = true;
                            }

                            if (SpielMeisterschaft.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CalEnabled = true;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Blitztunier":
                        if (droppedData is Blitztunier dataB)
                        {
                            if (SpielBlitztunier.Contains(dataB))
                            {
                                if (dataB.ID > 0) LstIDsToDelete.Add(dataB.ID);
                                SpielBlitztunier.Remove(dataB);
                                DataChanged = true;
                            }

                            if (SpielBlitztunier.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CalEnabled = true;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Kombimeisterschaft":
                        if (droppedData is Kombimeisterschaft dataKM)
                        {
                            if (SpielKombimeisterschaft.Contains(dataKM))
                            {
                                if (dataKM.ID > 0) LstIDsToDelete.Add(dataKM.ID);
                                SpielKombimeisterschaft.Remove(dataKM);
                                DataChanged = true;
                            }

                            if (SpielKombimeisterschaft.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CalEnabled = true;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                }

                break;
            case "dgEingabe":
                switch (SelectedSpiel)
                {
                    case "9er/Ratten":
                        if (droppedData is AktiveSpieler dataNR)
                        {
                            if (AktiveMitglieder.Contains(dataNR))
                            {
                                if (!IsPlayerInList(dataNR.ID, "9er/Ratten", SpielNeunerRatten.ToList()))
                                {
                                    NeunerRatten obj9erRatten = new();
                                    obj9erRatten.SpielerID = dataNR.ID;
                                    obj9erRatten.Spielername = dataNR.Anzeigename;
                                    SpielNeunerRatten.Add(obj9erRatten);
                                    DataChanged = true;
                                }

                                CboSpielauswahlEnabled = false;
                                CalEnabled = false;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "6-Tage-Rennen":
                        if (droppedData is AktiveSpieler data6TR)
                        {
                            if (AktiveMitglieder.Contains(data6TR))
                            {
                                AddSpieler6TR(data6TR);

                                CboSpielauswahlEnabled = false;
                                CalEnabled = false;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Pokal":
                        if (droppedData is AktiveSpieler dataP)
                        {
                            if (AktiveMitglieder.Contains(dataP))
                            {
                                if (!IsPlayerInList(dataP.ID, "Pokal", SpielPokal.ToList()))
                                {
                                    Pokal objPokal = new();
                                    objPokal.SpielerID = dataP.ID;
                                    objPokal.Spielername = dataP.Anzeigename;
                                    SpielPokal.Add(objPokal);
                                    DataChanged = true;
                                }

                                CboSpielauswahlEnabled = false;
                                CalEnabled = false;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Sargkegeln":
                        if (droppedData is AktiveSpieler dataS)
                        {
                            if (AktiveMitglieder.Contains(dataS))
                            {
                                if (!IsPlayerInList(dataS.ID, "Sargkegeln", SpielSargkegeln.ToList()))
                                {
                                    Sargkegeln objSarg = new();
                                    objSarg.SpielerID = dataS.ID;
                                    objSarg.Spielername = dataS.Anzeigename;
                                    SpielSargkegeln.Add(objSarg);
                                    DataChanged = true;
                                }

                                CboSpielauswahlEnabled = false;
                                CalEnabled = false;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Meisterschaft":
                        if (droppedData is AktiveSpieler dataM)
                        {
                            if (AktiveMitglieder.Contains(dataM))
                            {
                                AddSpielerMeisterschaft(dataM);

                                CboSpielauswahlEnabled = false;
                                CalEnabled = false;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Blitztunier":
                        if (droppedData is AktiveSpieler dataB)
                        {
                            if (AktiveMitglieder.Contains(dataB))
                            {
                                AddSpielerBlitztunier(dataB);

                                CboSpielauswahlEnabled = false;
                                CalEnabled = false;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                    case "Kombimeisterschaft":
                        if (droppedData is AktiveSpieler dataKM)
                        {
                            if (AktiveMitglieder.Contains(dataKM))
                            {
                                AddSpielerKombimeisterschaft(dataKM);

                                CboSpielauswahlEnabled = false;
                                CalEnabled = false;
                                CheckMandatoryFields();
                            }
                        }

                        break;
                }

                break;
        }
    }

    public void CheckMandatoryFields()
    {
        if (SelectedDate != null && SelectedSpiel != "")
        {
            switch (SelectedSpiel)
            {
                case "9er/Ratten":
                    if (SpielNeunerRatten.Count > 0)
                        BtnSpeichernEnabled = true;
                    else
                        BtnSpeichernEnabled = false;

                    break;
                case "6-Tage-Rennen":
                    if (Spiel6TageRennen.Count > 0)
                        BtnSpeichernEnabled = true;
                    else
                        BtnSpeichernEnabled = false;

                    break;
                case "Pokal":
                    if (SpielPokal.Count > 0)
                        BtnSpeichernEnabled = true;
                    else
                        BtnSpeichernEnabled = false;

                    break;
                case "Sargkegeln":
                    if (SpielSargkegeln.Count > 0)
                        BtnSpeichernEnabled = true;
                    else
                        BtnSpeichernEnabled = false;

                    break;
                case "Meisterschaft":
                    if (SpielMeisterschaft.Count > 0)
                        BtnSpeichernEnabled = true;
                    else
                        BtnSpeichernEnabled = false;

                    break;
                case "Blitztunier":
                    if (SpielBlitztunier.Count > 0)
                        BtnSpeichernEnabled = true;
                    else
                        BtnSpeichernEnabled = false;

                    break;
                case "Kombimeisterschaft":
                    if (SpielKombimeisterschaft.Count > 0)
                        BtnSpeichernEnabled = true;
                    else
                        BtnSpeichernEnabled = false;

                    break;
            }
        }
        else
            BtnSpeichernEnabled = false;
    }

    [ObservableProperty] private bool dataChanged = false;
    [ObservableProperty] List<int> lstIDsToDelete = new();

    [ObservableProperty] private List<string> spiele = new();
    [ObservableProperty] private List<string> spielNummer = new();
    [ObservableProperty] private string selectedSpiel = string.Empty;
    [ObservableProperty] private string selectedSpielNummer = "-1";
    [ObservableProperty] private DateTime? selectedDate = null;
    [ObservableProperty] private string selectedHinRueckrunde = "Hinrunde";
    public int SelectedHinRueckrundeID = 0;

    [ObservableProperty] private ObservableCollection<string> cboHinRueckrundeItems =
        new ObservableCollection<string>()
        {
            "Hinrunde",
            "Rückrunde"
        };

    [ObservableProperty] private ObservableCollection<KeyValuePair<int, string>> gridCboHinRueckrundeItems =
        new ObservableCollection<KeyValuePair<int, string>>()
        {
            new KeyValuePair<int, string>(0, "Hinrunde"),
            new KeyValuePair<int, string>(1, "Rueckrunde"),
        };

    [ObservableProperty] private bool btnSpeichernEnabled = false;
    [ObservableProperty] private bool cboSpielauswahlEnabled = true;
    [ObservableProperty] private bool calEnabled = true;

    [ObservableProperty] private ObservableCollection<AktiveSpieler> aktiveMitglieder = new();
    [ObservableProperty] private ObservableCollection<NeunerRatten> spielNeunerRatten = new();
    [ObservableProperty] private ObservableCollection<SechsTageRennen> spiel6TageRennen = new();
    [ObservableProperty] private ObservableCollection<Pokal> spielPokal = new();
    [ObservableProperty] private ObservableCollection<Sargkegeln> spielSargkegeln = new();
    [ObservableProperty] private ObservableCollection<Meisterschaft> spielMeisterschaft = new();
    [ObservableProperty] private ObservableCollection<Blitztunier> spielBlitztunier = new();
    [ObservableProperty] private ObservableCollection<Kombimeisterschaft> spielKombimeisterschaft = new();
    [ObservableProperty] private ObservableCollection<AusgabeNeunerRatten> eingabeKontrolle9erRatten = new();
    [ObservableProperty] private ObservableCollection<AusgabeSechsTageRennen> eingabeKontrolle6TageRennen = new();
    [ObservableProperty] private ObservableCollection<AusgabePokal> eingabeKontrollePokal = new();
    [ObservableProperty] private ObservableCollection<AusgabeSargkegeln> eingabeKontrolleSargkegeln = new();
    [ObservableProperty] private ObservableCollection<AusgabeMeisterschaft> eingabeKontrolleMeisterschaft = new();
    [ObservableProperty] private ObservableCollection<AusgabeBlitztunier> eingabeKontrolleBlitztunier = new();
    [ObservableProperty] private ObservableCollection<AusgabeKombimeisterschaft> eingabeKontrolleKombimeisterschaft = new();

    [RelayCommand]
    public void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            LoadAndSetDataAsync();
            IsViewModelLoaded = true;
        }
    }

    [RelayCommand]
    public void CboHinRueckrundeSelectionChanged()
    {
        if (SelectedHinRueckrunde == "Hinrunde")
            SelectedHinRueckrundeID = 0;
        else
            SelectedHinRueckrundeID = 1;
    }

    [RelayCommand]
    public async Task CalSelectedDatesChangedAsync()
    {
        SelectedDate = SelectedDate.Value.Date;
        await RefreshOutputAsync();
    }

    public async Task RefreshOutputAsync()
    {
        if (SelectedDate != null)
        {
            int intSpieltagID = await _dbService.GetSpieltagIDAsync(SelectedDate.Value);

            EingabeKontrolle9erRatten =
                new ObservableCollection<AusgabeNeunerRatten>(await _dbService.GetAusgabe9erRattenAsync(intSpieltagID));
            SpielNeunerRatten =
                new ObservableCollection<NeunerRatten>(await _dbService.GetEingabe9erRattenAsync(intSpieltagID));

            EingabeKontrolle6TageRennen =
                new ObservableCollection<AusgabeSechsTageRennen>(
                    await _dbService.GetAusgabe6TageRennenAsync(intSpieltagID));
            Spiel6TageRennen =
                new ObservableCollection<SechsTageRennen>(await _dbService.GetEingabe6TageRennenAsync(intSpieltagID));
            
            EingabeKontrollePokal =
                new ObservableCollection<AusgabePokal>(
                    await _dbService.GetAusgabePokalAsync(intSpieltagID));
            SpielPokal =
                new ObservableCollection<Pokal>(await _dbService.GetEingabePokalAsync(intSpieltagID));
            
            EingabeKontrolleSargkegeln =
                new ObservableCollection<AusgabeSargkegeln>(
                    await _dbService.GetAusgabeSargkegelnAsync(intSpieltagID));
            SpielSargkegeln =
                new ObservableCollection<Sargkegeln>(await _dbService.GetEingabeSargkegelnAsync(intSpieltagID));
            
            EingabeKontrolleMeisterschaft =
                new ObservableCollection<AusgabeMeisterschaft>(
                    await _dbService.GetAusgabeMeisterschaftAsync(intSpieltagID));
            SpielMeisterschaft =
                new ObservableCollection<Meisterschaft>(await _dbService.GetEingabeMeisterschaftAsync(intSpieltagID));
            
            EingabeKontrolleBlitztunier =
                new ObservableCollection<AusgabeBlitztunier>(
                    await _dbService.GetAusgabeBlitztunierAsync(intSpieltagID));
            SpielBlitztunier =
                new ObservableCollection<Blitztunier>(await _dbService.GetEingabeBlitztunierAsync(intSpieltagID));
            
            EingabeKontrolleKombimeisterschaft =
                new ObservableCollection<AusgabeKombimeisterschaft>(
                    await _dbService.GetAusgabeKombimeisterschaftAsync(intSpieltagID));
            SpielKombimeisterschaft =
                new ObservableCollection<Kombimeisterschaft>(await _dbService.GetEingabeKombimeisterschaftAsync(intSpieltagID));
        }
    }

    [RelayCommand]
    public async Task ErgebnisseSpeichernAsync()
    {
        int intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;

        if (DataChanged)
        {
            if (ViewManager.ShowConfirmationWindow("Wollen Sie die geänderten Daten speichern?"))
            {
                if (IsPageNotBusy)
                {
                    IsPageBusy = true;

                    switch (SelectedSpiel)
                    {
                        case "9er/Ratten":
                            await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate!.Value, SelectedSpiel,
                                SpielNeunerRatten.ToList(), LstIDsToDelete);
                            break;
                        case "6-Tage-Rennen":
                            await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate!.Value, SelectedSpiel,
                                Spiel6TageRennen.ToList(), LstIDsToDelete);
                            break;
                        case "Pokal":
                            await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate!.Value, SelectedSpiel,
                                SpielPokal.ToList(), LstIDsToDelete);
                            break;
                        case "Sargkegeln":
                            await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate!.Value, SelectedSpiel,
                                SpielSargkegeln.ToList(), LstIDsToDelete);
                            break;
                        case "Meisterschaft":
                            await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate!.Value, SelectedSpiel,
                                SpielMeisterschaft.ToList(), LstIDsToDelete);
                            break;
                        case "Blitztunier":
                            await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate!.Value, SelectedSpiel,
                                SpielBlitztunier.ToList(), LstIDsToDelete);
                            break;
                        case "Kombimeisterschaft":
                            await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate!.Value, SelectedSpiel,
                                SpielKombimeisterschaft.ToList(), LstIDsToDelete);
                            break;
                    }

                    await RefreshOutputAsync();
                    DataChanged = false;
                    IsPageBusy = false;
                }
            }
        }
    }
}