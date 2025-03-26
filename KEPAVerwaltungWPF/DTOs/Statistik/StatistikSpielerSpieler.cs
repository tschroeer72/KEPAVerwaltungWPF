namespace KEPAVerwaltungWPF.DTOs.Statistik;

public class StatistikSpielerSpieler
{
    public string Spielername { get; set; } = string.Empty;
    public Dictionary<string, StatistikGUV> dictMeisterschaft { get; set; } = new();
    public Dictionary<string, StatistikGUV> dictBlitztunier { get; set; } = new();
    public Dictionary<string, StatistikSpielerKombimeisterschaft> dictKombimeisterschaft { get; set; } = new();
}