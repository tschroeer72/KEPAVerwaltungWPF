namespace KEPAVerwaltungWPF.DTOs.Statistik;

public class StatistikSpielerKombimeisterschaft
{
    public Dictionary<string, StatistikGUV> dict3bis8 { get; set; } = new();
    public Dictionary<string, StatistikGUV> dict5Kugeln { get; set; } = new();
    public Dictionary<string, StatistikGUV> dictGesamt { get; set; } = new();
}