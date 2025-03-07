﻿using System.Collections.ObjectModel;
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

    public ErgebniseingabeViewModel(DBService dbService)
    {
        _dbService = dbService;
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

    private void AddSpieler6TR(AktiveSpieler oSpieler)
    {
        if (Spiel6TageRennen.Count == 0)
        {
            Spiel6TageRennen obj6TageRennen = new();
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
                Spiel6TageRennen[intIndex].Spieler2ID = oSpieler.ID;
                Spiel6TageRennen[intIndex].Spieler2Name = oSpieler.Anzeigename;
            }
        }
    }
    
    private void AddSpielerMeisterschaft(AktiveSpieler oSpieler)
    {
        if (SpielMeisterschaft.Count == 0)
        {
            SpielMeisterschaft objMeister = new();
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
                SpielMeisterschaft[intIndex].Spieler2ID = oSpieler.ID;
                SpielMeisterschaft[intIndex].Spieler2Name = oSpieler.Anzeigename;
            }
            else
            {
                SpielMeisterschaft objMeister = new();
                objMeister.Spieler1ID = oSpieler.ID;
                objMeister.Spieler1Name = oSpieler.Anzeigename;
                objMeister.Spieler2ID = -1;
                objMeister.Spieler2Name = "";
                objMeister.HinRueckrunde = SelectedHinRueckrundeID;
                SpielMeisterschaft.Add(objMeister);
            }
        }
    }
    
    private void AddSpielerKombimeisterschaft(AktiveSpieler oSpieler)
    {
        if (SpielKombimeisterschaft.Count == 0)
        {
            DTOs.SpielKombimeisterschaft objKombi = new();
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
                SpielKombimeisterschaft[intIndex].Spieler2ID = oSpieler.ID;
                SpielKombimeisterschaft[intIndex].Spieler2Name = oSpieler.Anzeigename;
            }
            else
            {
                DTOs.SpielKombimeisterschaft objKombi = new();
                objKombi.Spieler1ID = oSpieler.ID;
                objKombi.Spieler1Name = oSpieler.Anzeigename;
                objKombi.Spieler2ID = -1;
                objKombi.Spieler2Name = "";
                objKombi.HinRueckrunde = SelectedHinRueckrundeID;
                SpielKombimeisterschaft.Add(objKombi);
            }
        }
    }
    
    private void AddSpielerBlitztunier(AktiveSpieler oSpieler)
    {
        if (SpielBlitztunier.Count == 0)
        {
            DTOs.SpielBlitztunier objBlitz = new();
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
                SpielBlitztunier[intIndex].Spieler2ID = oSpieler.ID;
                SpielBlitztunier[intIndex].Spieler2Name = oSpieler.Anzeigename;
            }
            else
            {
                DTOs.SpielBlitztunier objBlitz = new();
                objBlitz.Spieler1ID = oSpieler.ID;
                objBlitz.Spieler1Name = oSpieler.Anzeigename;
                objBlitz.Spieler2ID = -1;
                objBlitz.Spieler2Name = "";
                objBlitz.HinRueckrunde = SelectedHinRueckrundeID;
                SpielBlitztunier.Add(objBlitz);
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
                            if (NeunerRatten.Contains(dataNR))
                                NeunerRatten.Remove(dataNR);

                        break;
                    case "6-Tage-Rennen":
                        if (droppedData is Spiel6TageRennen data6TR)
                            if (Spiel6TageRennen.Contains(data6TR))
                                Spiel6TageRennen.Remove(data6TR);

                        break;
                    case "Pokal":
                        if (droppedData is SpielPokal dataP)
                            if (SpielPokal.Contains(dataP))
                                SpielPokal.Remove(dataP);

                        break;
                    case "Sargkegeln":
                        if (droppedData is SpielSargkegeln dataS)
                            if (SpielSargkegeln.Contains(dataS))
                                SpielSargkegeln.Remove(dataS);
                        
                        break;
                    case "Meisterschaft":
                        if (droppedData is SpielMeisterschaft dataM)
                            if (SpielMeisterschaft.Contains(dataM))
                                SpielMeisterschaft.Remove(dataM);
                        
                        break;
                    case "Blitztunier":
                        if (droppedData is SpielBlitztunier dataB)
                            if (SpielBlitztunier.Contains(dataB))
                                SpielBlitztunier.Remove(dataB);
                        break;
                    case "Kombimeisterschaft":
                        if (droppedData is SpielKombimeisterschaft dataKM)
                            if (SpielKombimeisterschaft.Contains(dataKM))
                                SpielKombimeisterschaft.Remove(dataKM);
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
                                //AktiveMitglieder.Remove(data);
                                NeunerRatten obj9erRatten = new();
                                obj9erRatten.SpielerID = dataNR.ID;
                                obj9erRatten.Spielername = dataNR.Anzeigename;
                                NeunerRatten.Add(obj9erRatten);
                            }
                        }

                        break;
                    case "6-Tage-Rennen":
                        if (droppedData is AktiveSpieler data6TR)
                        {
                            if (AktiveMitglieder.Contains(data6TR))
                            {
                                //AktiveMitglieder.Remove(data);
                                AddSpieler6TR(data6TR);
                            }
                        }

                        break;
                    case "Pokal":
                        if (droppedData is AktiveSpieler dataP)
                        {
                            if (AktiveMitglieder.Contains(dataP))
                            {
                                //AktiveMitglieder.Remove(data);
                                SpielPokal objPokal = new();
                                objPokal.SpielerID = dataP.ID;
                                objPokal.Spielername = dataP.Anzeigename;
                                SpielPokal.Add(objPokal);
                            }
                        }
                        
                        break;
                    case "Sargkegeln":
                        if (droppedData is AktiveSpieler dataS)
                        {
                            if (AktiveMitglieder.Contains(dataS))
                            {
                                //AktiveMitglieder.Remove(data);
                                SpielSargkegeln objSarg = new();
                                objSarg.SpielerID = dataS.ID;
                                objSarg.Spielername = dataS.Anzeigename;
                                SpielSargkegeln.Add(objSarg);
                            }
                        }
                        
                        break;
                    case "Meisterschaft":
                        if (droppedData is AktiveSpieler dataM)
                        {
                            if (AktiveMitglieder.Contains(dataM))
                            {
                                //AktiveMitglieder.Remove(data);
                                AddSpielerMeisterschaft(dataM);
                            }
                        }
                        
                        break;
                    case "Blitztunier":
                        if (droppedData is AktiveSpieler dataB)
                        {
                            if (AktiveMitglieder.Contains(dataB))
                            {
                                //AktiveMitglieder.Remove(data);
                                AddSpielerBlitztunier(dataB);
                            }
                        }
                        
                        break;
                    case "Kombimeisterschaft":
                        if (droppedData is AktiveSpieler dataKM)
                        {
                            if (AktiveMitglieder.Contains(dataKM))
                            {
                                //AktiveMitglieder.Remove(data);
                                AddSpielerKombimeisterschaft(dataKM);
                            }
                        }
                        
                        break;
                }
                break;
        }
    }
    
    [ObservableProperty] private List<string> spiele = new();
    [ObservableProperty] private List<string> spielNummer = new();
    [ObservableProperty] private string selectedSpiel = string.Empty;
    [ObservableProperty] private string selectedSpielNummer = "1";
    [ObservableProperty] private string selectedHinRueckrunde = "Hinrunde";
    public int SelectedHinRueckrundeID = 0;
    [ObservableProperty] private DateTime? selectedDate = null;

    [ObservableProperty] private ObservableCollection<string> cboHinRueckrundeItems =
        new ObservableCollection<string>()
        {
            "Hinrunde" ,
            "Rückrunde" 
        };
    
    [ObservableProperty] private ObservableCollection<AktiveSpieler> aktiveMitglieder = new();
    [ObservableProperty] private ObservableCollection<NeunerRatten> neunerRatten = new();
    [ObservableProperty] private ObservableCollection<Spiel6TageRennen> spiel6TageRennen = new();
    [ObservableProperty] private ObservableCollection<SpielPokal> spielPokal = new();
    [ObservableProperty] private ObservableCollection<SpielSargkegeln> spielSargkegeln = new();
    [ObservableProperty] private ObservableCollection<SpielMeisterschaft> spielMeisterschaft = new();
    [ObservableProperty] private ObservableCollection<SpielBlitztunier> spielBlitztunier = new();
    [ObservableProperty] private ObservableCollection<SpielKombimeisterschaft> spielKombimeisterschaft = new();
    
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
    public void ErgebnisseSpeichern()
    {
        var a = SelectedDate;
        var b = SelectedSpiel;
        var c = SelectedSpielNummer;
        var d = SelectedHinRueckrunde;
        var e = SelectedHinRueckrundeID;
    }
}