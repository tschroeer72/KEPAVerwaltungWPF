namespace KEPAVerwaltungWPF.DTOs.Statistik;

public class Statistik9erRatten
{
    public int MeisterschaftsID { get; set; }
    public string Bezeichnung { get; set; }
    public DateTime Beginn { get; set; }
    public DateTime? Ende { get; set; }
    public DateTime Spieltag { get; set; }
    public int Neuner { get; set; } = 0;
    public string Neunerkönig { get; set; } = string.Empty;
    public int Ratten { get; set; } = 0;
    public string Rattenorden { get; set; } = string.Empty;
}