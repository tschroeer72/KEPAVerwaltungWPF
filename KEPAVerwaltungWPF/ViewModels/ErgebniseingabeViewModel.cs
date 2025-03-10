using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
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
                                SpielNeunerRatten.Remove(dataNR);
        
                            if (SpielNeunerRatten.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CheckMandatoryFields();
                            }
                        }
        
                        break;
                    case "6-Tage-Rennen":
                        if (droppedData is SechsTageRennen data6TR)
                        {
                            if (Spiel6TageRennen.Contains(data6TR))
                                Spiel6TageRennen.Remove(data6TR);
                            
                            if(Spiel6TageRennen.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CheckMandatoryFields();
                            }
                        }
        
                        break;
                    case "Pokal":
                        if (droppedData is Pokal dataP)
                        {
                            if (SpielPokal.Contains(dataP))
                                SpielPokal.Remove(dataP);
                            
                            if(SpielPokal.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CheckMandatoryFields();
                            }
                        }
        
                        break;
                    case "Sargkegeln":
                        if (droppedData is Sargkegeln dataS)
                        {
                            if (SpielSargkegeln.Contains(dataS))
                                SpielSargkegeln.Remove(dataS);
                            
                            if(SpielSargkegeln.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CheckMandatoryFields();
                            }
                        }
        
                        break;
                    case "Meisterschaft":
                        if (droppedData is Meisterschaft dataM)
                        {
                            if (SpielMeisterschaft.Contains(dataM))
                                SpielMeisterschaft.Remove(dataM);
                            
                            if(SpielMeisterschaft.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CheckMandatoryFields();
                            }
                        }
        
                        break;
                    case "Blitztunier":
                        if (droppedData is Blitztunier dataB)
                        {
                            if (SpielBlitztunier.Contains(dataB))
                                SpielBlitztunier.Remove(dataB);
                            
                            if(SpielBlitztunier.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
                                CheckMandatoryFields();
                            }
                        }
        
                        break;
                    case "Kombimeisterschaft":
                        if (droppedData is Kombimeisterschaft dataKM)
                        {
                            if (SpielKombimeisterschaft.Contains(dataKM))
                                SpielKombimeisterschaft.Remove(dataKM);
                            
                            if(SpielKombimeisterschaft.Count == 0)
                            {
                                CboSpielauswahlEnabled = true;
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
                                }
        
                                CboSpielauswahlEnabled = false;
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
                                }
        
                                CboSpielauswahlEnabled = false;
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
                                }
        
                                CboSpielauswahlEnabled = false;
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
    
    [ObservableProperty] private List<string> spiele = new();
    [ObservableProperty] private List<string> spielNummer = new();
    [ObservableProperty] private string selectedSpiel = string.Empty;
    [ObservableProperty] private string selectedSpielNummer = "1";
    [ObservableProperty] private DateTime? selectedDate = DateTime.Now;
    [ObservableProperty] private string selectedHinRueckrunde = "Hinrunde";
    public int SelectedHinRueckrundeID = 0;

    [ObservableProperty] private ObservableCollection<string> cboHinRueckrundeItems =
        new ObservableCollection<string>()
        {
            "Hinrunde" ,
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
    
    [ObservableProperty] private ObservableCollection<AktiveSpieler> aktiveMitglieder = new();
    [ObservableProperty] private ObservableCollection<NeunerRatten> spielNeunerRatten = new();
    [ObservableProperty] private ObservableCollection<SechsTageRennen> spiel6TageRennen = new();
    [ObservableProperty] private ObservableCollection<Pokal> spielPokal = new();
    [ObservableProperty] private ObservableCollection<Sargkegeln> spielSargkegeln = new();
    [ObservableProperty] private ObservableCollection<Meisterschaft> spielMeisterschaft = new();
    [ObservableProperty] private ObservableCollection<Blitztunier> spielBlitztunier = new();
    [ObservableProperty] private ObservableCollection<Kombimeisterschaft> spielKombimeisterschaft = new();
    
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
    public async Task ErgebnisseSpeichernAsync()
    {
        int intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;

        if (IsPageNotBusy)
        {
            IsPageBusy = true;
            
            switch (SelectedSpiel)
            {
                case "9er/Ratten":
                    await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate.Value, SelectedSpiel,
                        SpielNeunerRatten.ToList());
                    break;
                case "6-Tage-Rennen":
                    await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate.Value, SelectedSpiel,
                        Spiel6TageRennen);
                    break;
                case "Pokal":
                    await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate.Value, SelectedSpiel,
                        SpielPokal);
                    break;
                case "Sargkegeln":
                    await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate.Value, SelectedSpiel,
                        SpielSargkegeln);
                    break;
                case "Meisterschaft":
                    await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate.Value, SelectedSpiel,
                        SpielMeisterschaft);
                    break;
                case "Blitztunier":
                    await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate.Value, SelectedSpiel,
                        SpielBlitztunier);
                    break;
                case "Kombimeisterschaft":
                    await _dbService.SaveEingabeAsync(intMeisterschaftsID, SelectedDate.Value, SelectedSpiel,
                        SpielKombimeisterschaft);
                    break;
            }

            IsPageBusy = false;
        }
    }
}