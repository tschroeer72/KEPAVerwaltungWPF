namespace KEPAVerwaltungWPF.DTOs.Ergebnisse;

public class ErgebnisKombimeisterschaftKreuztabelle
{
    public int MeisterschaftsID { get; set; }
    public int Spieler1ID { get; set; }
    public string Spieler1Name { get; set; }
    public int Spieler2ID { get; set; }
    public string Spieler2Name { get; set; }
    public int Spieler1Punkte3bis8 { get; set; }
    public int Spieler1Punkte5Kugeln { get; set; }
    public int Spieler2Punkte3bis8 { get; set; }
    public int Spieler2Punkte5Kugeln { get; set; }
    public int HinRueckrunde { get; set; }
}