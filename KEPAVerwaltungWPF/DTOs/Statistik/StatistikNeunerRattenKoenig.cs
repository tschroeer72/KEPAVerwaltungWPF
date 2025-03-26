namespace KEPAVerwaltungWPF.DTOs.Statistik;

public class StatistikNeunerRattenKoenig
{
    public List<Statistik9erRatten> lstStatistik9erRatten { get; set; } = new();
    public Dictionary<string, int> dictNeunerkönig { get; set; } = new();
    public Dictionary<string, int> dictRattenkönig { get; set; } = new();
}