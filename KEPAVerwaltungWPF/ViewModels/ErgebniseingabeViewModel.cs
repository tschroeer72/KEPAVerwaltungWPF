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
                        break;
                    case "Sargkegeln":
                        break;
                    case "Meisterschaft":
                        if (droppedData is SpielMeisterschaft dataM)
                            if (SpielMeisterschaft.Contains(dataM))
                                SpielMeisterschaft.Remove(dataM);
                        
                        break;
                    case "Blitztunier":
                        break;
                    case "Kombimeisterschaft":
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
                        break;
                    case "Sargkegeln":
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
                        break;
                    case "Kombimeisterschaft":
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
    [ObservableProperty] private ObservableCollection<SpielSargKegeln> spielSargkegeln = new();
    [ObservableProperty] private ObservableCollection<SpielMeisterschaft> spielMeisterschaft = new();
    [ObservableProperty] private ObservableCollection<SpielBlitztunier> spielBlitzrunier = new();
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