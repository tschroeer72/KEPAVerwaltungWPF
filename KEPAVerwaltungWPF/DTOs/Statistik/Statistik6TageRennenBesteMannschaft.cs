namespace KEPAVerwaltungWPF.DTOs.Statistik;

public class Statistik6TageRennenBesteMannschaft
{
    public string Mannschaft { get; set; } = string.Empty;
    public int Spieler1ID { get; set; }
    public int Spieler2ID { get; set; }
    public int Anzahl { get; set; } = 0;
    public int Sechs { get; set; } = 0;
    public int Fünf { get; set; } = 0;
    public int Vier { get; set; } = 0;
    public int Drei { get; set; } = 0;
    public int Zwei { get; set; } = 0;
    public int Eins { get; set; } = 0;
}