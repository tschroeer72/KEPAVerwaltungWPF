namespace KEPAVerwaltungWPF.DTOs.Ergebnisse;

public class ErgebnisMeisterschaftKreuztabelle
{
    public int MeisterschaftsID { get; set; }
    public int Spieler1ID { get; set; }
    public string Spieler1Name { get; set; }
    public int Spieler2ID { get; set; }
    public string Spieler2Name { get; set; }
    public int Spieler1Punkte { get; set; }
    public int Spieler2Punkte { get; set; }
    public int HinRueckrunde { get; set; }
}