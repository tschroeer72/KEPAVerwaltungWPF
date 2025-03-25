using System.Drawing;
using System.IO;
using System.Reflection;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.DTOs.Ergebnisse;
using KEPAVerwaltungWPF.DTOs.Statistik;
using KEPAVerwaltungWPF.Helper;
using KEPAVerwaltungWPF.Views;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Quality;

namespace KEPAVerwaltungWPF.Services;

public class PrintService(DBService _dbService, CommonService _commonService)
{
    #region Vorlagen

    public void DruckVorlage6TageRennen(string PDFFilename = "")
    {
        //Anfang
        VpeToPdfSharp vpe = new();
        vpe.FileName = "Vorlage_6TageRennen";
        vpe.OpenDoc();
        vpe.OpenProgressBar();
        vpe.PageOrientation = PageOrientation.Landscape;
        vpe.SetMargins(2, 2, 2, 2);

        //Überschrift
        vpe.SelectFont("Arial", 14);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write((vpe.nLeftMargin + vpe.nRightMargin) / 2 - 2, vpe.nTopMargin, -4, -1, "6 Tage Rennen");

        //Kopfzeile 1
        vpe.SelectFont("Arial", 12);
        vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
        vpe.Write(vpe.nLeftMargin, vpe.nBottom, -4.5, -0.5, "Mannschaft");
        vpe.PenStyle = PenStyle.Solid;
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nRight, vpe.nTop, -3, -0.5, "1. Nacht");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -3, -0.5, "2. Nacht");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -3, -0.5, "3. Nacht");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -3, -0.5, "4. Nacht");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -3, -0.5, "5. Nacht");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -3, -0.5, "6. Nacht");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -3, -0.5, "Platz");
        vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nLeftMargin + 22.5, vpe.nBottom);

        //Kopfzeile 2
        vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
        vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1, -0.5, "Nr.");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nRight, vpe.nTop, -3.5, -0.5, "Namen");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 12);
        vpe.Write(vpe.nRight, vpe.nTop, -2, -0.5, "Holz");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 7);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "Pkt");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "RD");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 12);
        vpe.Write(vpe.nRight, vpe.nTop, -2, -0.5, "Holz");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 7);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "Pkt");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "RD");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 12);
        vpe.Write(vpe.nRight, vpe.nTop, -2, -0.5, "Holz");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 7);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "Pkt");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "RD");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 12);
        vpe.Write(vpe.nRight, vpe.nTop, -2, -0.5, "Holz");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 7);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "Pkt");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "RD");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 12);
        vpe.Write(vpe.nRight, vpe.nTop, -2, -0.5, "Holz");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 7);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "Pkt");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "RD");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 12);
        vpe.Write(vpe.nRight, vpe.nTop, -2, -0.5, "Holz");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.SelectFont("Arial", 7);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "Pkt");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Write(vpe.nRight, vpe.nTop, -0.5, -0.5, "RD");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottom);
        vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRightMargin, vpe.nBottom);

        for (Int32 i = 1; i <= 10; i++)
        {
            //1. Mannschaft
            vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
            vpe.SelectFont("Arial", 14);
            vpe.Write(vpe.nLeftMargin, vpe.nTop + 0.5, -1, -1, i.ToString());
            vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottom);

            vpe.PenStyle = PenStyle.Dash;
            vpe.Line(vpe.nRight, vpe.nTop + 0.75, vpe.nRight + 3.5, vpe.nTop + 0.75); //Trennlinie Namen
            vpe.PenStyle = PenStyle.Solid;
            vpe.Line(vpe.nRight, vpe.nTop - 0.75, vpe.nRight, vpe.nTop + 0.75); //Senktrecht
            //1. Nacht
            vpe.Line(vpe.nRight, vpe.nTop + 0.75, vpe.nRight + 2, vpe.nTop + 0.75); //Trennlinie Holz
            vpe.Line(vpe.nLeft + 0.66, vpe.nTop - 0.75, vpe.nLeft + 0.66, vpe.nTop + 0.75); //Senktrecht
            vpe.Line(vpe.nRight + 0.66, vpe.nTop, vpe.nRight + 0.66, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.68, vpe.nTop, vpe.nRight + 0.68, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            //2. Nacht
            vpe.Line(vpe.nRight, vpe.nTop + 0.75, vpe.nRight + 2, vpe.nTop + 0.75); //Trennlinie Holz
            vpe.Line(vpe.nLeft + 0.66, vpe.nTop - 0.75, vpe.nLeft + 0.66, vpe.nTop + 0.75); //Senktrecht
            vpe.Line(vpe.nRight + 0.66, vpe.nTop, vpe.nRight + 0.66, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.68, vpe.nTop, vpe.nRight + 0.68, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            //3. Nacht
            vpe.Line(vpe.nRight, vpe.nTop + 0.75, vpe.nRight + 2, vpe.nTop + 0.75); //Trennlinie Holz
            vpe.Line(vpe.nLeft + 0.66, vpe.nTop - 0.75, vpe.nLeft + 0.66, vpe.nTop + 0.75); //Senktrecht
            vpe.Line(vpe.nRight + 0.66, vpe.nTop, vpe.nRight + 0.66, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.68, vpe.nTop, vpe.nRight + 0.68, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            //4. Nacht
            vpe.Line(vpe.nRight, vpe.nTop + 0.75, vpe.nRight + 2, vpe.nTop + 0.75); //Trennlinie Holz
            vpe.Line(vpe.nLeft + 0.66, vpe.nTop - 0.75, vpe.nLeft + 0.66, vpe.nTop + 0.75); //Senktrecht
            vpe.Line(vpe.nRight + 0.66, vpe.nTop, vpe.nRight + 0.66, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.68, vpe.nTop, vpe.nRight + 0.68, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            //5. Nacht
            vpe.Line(vpe.nRight, vpe.nTop + 0.75, vpe.nRight + 2, vpe.nTop + 0.75); //Trennlinie Holz
            vpe.Line(vpe.nLeft + 0.66, vpe.nTop - 0.75, vpe.nLeft + 0.66, vpe.nTop + 0.75); //Senktrecht
            vpe.Line(vpe.nRight + 0.66, vpe.nTop, vpe.nRight + 0.66, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.68, vpe.nTop, vpe.nRight + 0.68, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            //6. Nacht
            vpe.Line(vpe.nRight, vpe.nTop + 0.75, vpe.nRight + 2, vpe.nTop + 0.75); //Trennlinie Holz
            vpe.Line(vpe.nLeft + 0.66, vpe.nTop - 0.75, vpe.nLeft + 0.66, vpe.nTop + 0.75); //Senktrecht
            vpe.Line(vpe.nRight + 0.66, vpe.nTop, vpe.nRight + 0.66, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.68, vpe.nTop, vpe.nRight + 0.68, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht
            vpe.Line(vpe.nRight + 0.5, vpe.nTop, vpe.nRight + 0.5, vpe.nTop + 1.5); //Senktrecht

            vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRightMargin, vpe.nBottom); //Zeilenabschluss
        }

        //Ende
        vpe.CloseProgressBar();

        if (PDFFilename == string.Empty)
            vpe.Preview();
        else
            vpe.PDFExport(PDFFilename);
    }

    public void DruckVorlageKombimeisterschaft(string PDFFilename = "")
    {
        int x, y;

        // Anfang
        VpeToPdfSharp vpe = new VpeToPdfSharp();
        vpe.FileName = "Vorlage_Kombimeisterschaft";
        vpe.OpenDoc();
        vpe.OpenProgressBar();
        vpe.PenStyle = PenStyle.Solid;
        vpe.PageOrientation = PageOrientation.Portrait;
        vpe.SetMargins(1, 1, 1, 1);

        // Überschrift
        // Da die vpe-Methoden mit cm arbeiten, müssen die Seitenränder ebenfalls in cm übergeben werden.
        vpe.StringFormat.Alignment = XStringAlignment.Center;
        vpe.SelectFont("Arial", 18);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -2, "Kombimeisterschaft");

        // Spielplan
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                vpe.SelectFont("Arial", 12);
                vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
                x = 2 + (10 * i);
                y = 4 + (6 * j);
                vpe.Write(x, y + 0.3, -3, -1, "Name");
                vpe.PenStyle = PenStyle.Solid;
                vpe.Box(x + 3, y, -4, -1);
                vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
                vpe.Write(x + 3, vpe.nBottom + 0.2, -2, -0.6, "3 bis 8");
                vpe.Write(vpe.nRight, vpe.nTop, -2, -0.6, "5 Kugeln");
                vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
                vpe.Write(x, vpe.nBottom, -3, -0.5, "Gesamtpunkte");
                vpe.Box(x + 3, vpe.nTop, -2, -0.5);
                vpe.Box(vpe.nRight, vpe.nTop, -2, -0.5);
                vpe.Write(x, vpe.nBottom + 0.2, -3, -1, "Abhaken");
                vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -2, -0.5, "3");
                for (int k = 4; k <= 8; k++)
                {
                    vpe.Write(vpe.nLeft, vpe.nBottom, -2, -0.5, k.ToString());
                }
            }

            if (j < 3)
            {
                vpe.PenStyle = PenStyle.DashDot;
                vpe.Line(vpe.nLeftMargin, vpe.nBottom + 0.2, vpe.nRightMargin, vpe.nBottom + 0.2);
            }
        }

        // Ende
        vpe.CloseProgressBar();

        if (PDFFilename == string.Empty)
            vpe.Preview();
        else
            vpe.PDFExport(PDFFilename);
    }

    public void DruckVorlageMeisterschaft(int AnzahlTeilnehmer, string PDFFilename = "")
    {
        string strMeisterschaft = "Meisterschaft";
        string strMeisterschaftstyp = "Meisterschaft";
        Double x;
        Double y;
        Double xZeile;
        Double yZeile;
        Double xBak;
        Double yBak;
        Double xErg;
        Double yErg;
        string strText = string.Empty;
        Int32 intSummeHinrunde = 0;
        Int32 intSummeRückrunde = 0;
        Int32 intSummeHinRückrunde = 0;
        Int32 intIndex = -1;
        Int32 intIndexSpieler1 = -1;
        Int32 intIndexSpieler2 = -1;
        Int32 intPlatzierungTemp = 0;
        Int32 intPunkteTemp = 0;

        //Anfang
        VpeToPdfSharp vpe = new();
        vpe.FileName = "Vorlage_Meisterschaft";
        vpe.OpenDoc();
        vpe.OpenProgressBar();
        vpe.PageOrientation = PageOrientation.Landscape;
        vpe.SetMargins(2, 2, 2, 2);
        vpe.PenSize = 0.01;

        //Überschrift
        vpe.SelectFont("Arial", 18);
        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
        vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + AnzahlTeilnehmer * 2.4 + 3 * 0.8, -1,
            strMeisterschaft);
        vpe.SelectFont("Arial", 14);
        vpe.Write(vpe.nLeftMargin, vpe.nBottom, vpe.nLeftMargin + 1.7 + AnzahlTeilnehmer * 2.4 + 3 * 0.8, -1,
            strMeisterschaftstyp);

        //Kopfzeile und Namensspalte
        vpe.SelectFont("Arial", 18);
        x = vpe.nLeftMargin;
        y = vpe.nBottom;
        //strText = "2023";
        strText = DateTime.Now.Year.ToString();
        vpe.WriteBox(x, y, -1.7, -1, strText);
        x = vpe.nRight;
        y = vpe.nTop;
        xZeile = vpe.nLeft;
        yZeile = vpe.nBottom;
        xErg = vpe.nRight;
        yErg = vpe.nBottom;
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++)
        {
            vpe.SelectFont("Arial", 9);
            strText = " ";
            vpe.WriteBox(x, y, -2.4, -0.5, strText);
            xBak = vpe.nRight;
            yBak = vpe.nTop;
            vpe.SelectFont("Arial", 10);
            vpe.WriteBox(vpe.nLeft, vpe.nTop + 0.5, -2.4, -0.5, "Punkte");
            x = xBak;
            y = yBak;
        }

        vpe.SelectFont("Arial", 7);
        strText = "Tot. Hi. Rü.";
        vpe.WriteBox(x, y, -0.8, -1, strText);
        strText = "Total";
        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
        strText = "Platz";
        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);

        x = xZeile;
        y = yZeile;
        vpe.SelectFont("Arial", 9);
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++)
        {
            strText = " ";
            vpe.WriteBox(x, y, -1.7, -1, strText);
            x = vpe.nLeft;
            y = vpe.nBottom;
        }

        //Ergebnisse
        x = xErg;
        y = yErg;
        vpe.SelectFont("Arial", 10);
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++) //Zeilen durchgehen
        {
            intSummeHinrunde = 0;
            intSummeRückrunde = 0;
            intSummeHinRückrunde = 0;

            for (Int32 j = 0; j < AnzahlTeilnehmer; j++) //Spalten durchgehen
            {
                xBak = 0;
                yBak = 0;
                if (i == j)
                {
                    vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                    //Hinrunde
                    //Punkte
                    vpe.SelectFont("Arial", 10);
                    strText = "XXX";
                    vpe.WriteBox(x, y, -2.4, -0.5, strText);

                    xBak = vpe.nRight;
                    yBak = vpe.nTop;

                    //Rückrunde
                    //Punkte
                    strText = "XXX";
                    vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                    vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
                }
                else
                {
                    // //Hinrunde
                    // vpe.SelectFont("Arial", 10);
                    // strText = " ";
                    // intIndexSpieler1 = lstListe.FindIndex(f =>
                    //     f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 0);
                    // intIndexSpieler2 = lstListe.FindIndex(f =>
                    //     f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 0);
                    //
                    // //Punkte
                    // if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                    // {
                    //     if (intIndexSpieler1 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                    //         }
                    //     }
                    //
                    //     if (intIndexSpieler2 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                    //         }
                    //     }
                    // }

                    strText = " ";
                    vpe.WriteBox(x, y, -2.4, -0.5, strText);


                    xBak = vpe.nRight;
                    yBak = vpe.nTop;
                    vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                    // //Rückrunde
                    // vpe.SelectFont("Arial", 10);
                    // strText = " ";
                    // intIndexSpieler1 = lstListe.FindIndex(f =>
                    //     f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 1);
                    // intIndexSpieler2 = lstListe.FindIndex(f =>
                    //     f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 1);
                    //
                    // //Punkte
                    // if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                    // {
                    //     if (intIndexSpieler1 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                    //         }
                    //     }
                    //
                    //     if (intIndexSpieler2 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                    //         }
                    //     }
                    // }

                    strText = " ";
                    vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                }

                x = xBak;
                y = yBak;
            }

            yBak = vpe.nBottom;

            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.SelectFont("Arial", 8);
            //strText = "a" + i.ToString();
            strText = " "; // intSummeHinrunde.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -0.5, strText);
            //strText = "b" + i.ToString();
            strText = " "; //intSummeRückrunde.ToString();
            vpe.WriteBox(vpe.nLeft, vpe.nBottom, -0.8, -0.5, strText);
            //strText = "c" + i.ToString();
            strText = " "; //intSummeHinRückrunde.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -1, strText);
            //strText = "d" + i.ToString();
            // switch (lstTeilnehmer[i].Platzierung)
            // {
            //     case 1:
            //         vpe.SelectFont("Arial", 18);
            //         break;
            //     case 2:
            //         vpe.SelectFont("Arial", 15);
            //         break;
            //     case 3:
            //         vpe.SelectFont("Arial", 12);
            //         break;
            //     default:
            //         vpe.SelectFont("Arial", 8);
            //         break;
            // }

            strText = " "; //lstTeilnehmer[i].Platzierung.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
            vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

            x = xErg;
            y = yBak;
        }

        vpe.SelectFont("Arial", 7);
        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
        strText = "Tot. Hi. Rü.";
        vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -1.7, -0.5, strText);
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++)
        {
            vpe.SelectFont("Arial", 10);
            //Zeilensumme Punkte
            //strText = arrPunkte3bis8[i].ToString();
            strText = " "; //lstTeilnehmer[i].Punkte.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop, -2.4, -0.5, strText);
            ////Zeilensumme 5 Kugeln
            ////strText = arrPunkte5Kugeln[i].ToString();
            //strText = lstTeilnehmer[i].Punkte5Kugeln.ToString();
            //vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
            //vpe.SelectFont("Arial", 8);
            //vpe.Write(vpe.nRight, vpe.nTop, -0.8, -0.5, "");
        }

        //Ende
        vpe.CloseProgressBar();

        if (PDFFilename == string.Empty)
            vpe.Preview();
        else
            vpe.PDFExport(PDFFilename);
    }

    public void DruckVorlageBlitztunier(int AnzahlTeilnehmer, string PDFFilename = "")
    {
        string strMeisterschaft = "Blitztunier";
        string strMeisterschaftstyp = "Kurztunier";
        Double x;
        Double y;
        Double xZeile;
        Double yZeile;
        Double xBak;
        Double yBak;
        Double xErg;
        Double yErg;
        string strText = string.Empty;
        Int32 intSummeHinrunde = 0;
        Int32 intSummeRückrunde = 0;
        Int32 intSummeHinRückrunde = 0;
        Int32 intIndex = -1;
        Int32 intIndexSpieler1 = -1;
        Int32 intIndexSpieler2 = -1;
        Int32 intPlatzierungTemp = 0;
        Int32 intPunkteTemp = 0;

        //Anfang
        VpeToPdfSharp vpe = new();
        vpe.FileName = "Vorlage_Kurztunier";
        vpe.OpenDoc();
        vpe.OpenProgressBar();
        vpe.PageOrientation = PageOrientation.Landscape;
        vpe.SetMargins(2, 2, 2, 2);
        vpe.PenSize = 0.01;

        //Überschrift
        vpe.SelectFont("Arial", 18);
        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
        //vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1, "KOMBI-Meisterschaft");
        vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + AnzahlTeilnehmer * 2.4 + 3 * 0.8, -1,
            strMeisterschaft);
        vpe.SelectFont("Arial", 14);
        vpe.Write(vpe.nLeftMargin, vpe.nBottom, vpe.nLeftMargin + 1.7 + AnzahlTeilnehmer * 2.4 + 3 * 0.8, -1,
            strMeisterschaftstyp);

        //Kopfzeile und Namensspalte
        vpe.SelectFont("Arial", 18);
        x = vpe.nLeftMargin;
        y = vpe.nBottom;
        //strText = "2023";
        strText = DateTime.Now.Year.ToString();
        vpe.WriteBox(x, y, -1.7, -1, strText);
        x = vpe.nRight;
        y = vpe.nTop;
        xZeile = vpe.nLeft;
        yZeile = vpe.nBottom;
        xErg = vpe.nRight;
        yErg = vpe.nBottom;
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++)
        {
            vpe.SelectFont("Arial", 9);
            strText = " ";
            vpe.WriteBox(x, y, -2.4, -0.5, strText);
            xBak = vpe.nRight;
            yBak = vpe.nTop;
            vpe.SelectFont("Arial", 10);
            vpe.WriteBox(vpe.nLeft, vpe.nTop + 0.5, -2.4, -0.5, "Punkte");
            x = xBak;
            y = yBak;
        }

        vpe.SelectFont("Arial", 7);
        strText = "Tot. Hi. Rü.";
        vpe.WriteBox(x, y, -0.8, -1, strText);
        strText = "Total";
        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
        strText = "Platz";
        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);

        x = xZeile;
        y = yZeile;
        vpe.SelectFont("Arial", 9);
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++)
        {
            strText = " ";
            vpe.WriteBox(x, y, -1.7, -1, strText);
            x = vpe.nLeft;
            y = vpe.nBottom;
        }

        //Ergebnisse
        x = xErg;
        y = yErg;
        vpe.SelectFont("Arial", 10);
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++) //Zeilen durchgehen
        {
            intSummeHinrunde = 0;
            intSummeRückrunde = 0;
            intSummeHinRückrunde = 0;

            for (Int32 j = 0; j < AnzahlTeilnehmer; j++) //Spalten durchgehen
            {
                xBak = 0;
                yBak = 0;
                if (i == j)
                {
                    vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                    //Hinrunde
                    //Punkte
                    vpe.SelectFont("Arial", 10);
                    strText = "XXX";
                    vpe.WriteBox(x, y, -2.4, -0.5, strText);

                    xBak = vpe.nRight;
                    yBak = vpe.nTop;

                    //Rückrunde
                    //Punkte
                    strText = "XXX";
                    vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                    vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
                }
                else
                {
                    // //Hinrunde
                    // vpe.SelectFont("Arial", 10);
                    // strText = " ";
                    // intIndexSpieler1 = lstListe.FindIndex(f =>
                    //     f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 0);
                    // intIndexSpieler2 = lstListe.FindIndex(f =>
                    //     f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 0);
                    //
                    // //Punkte
                    // if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                    // {
                    //     if (intIndexSpieler1 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                    //         }
                    //     }
                    //
                    //     if (intIndexSpieler2 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                    //         }
                    //     }
                    // }

                    strText = " ";
                    vpe.WriteBox(x, y, -2.4, -0.5, strText);

                    xBak = vpe.nRight;
                    yBak = vpe.nTop;
                    vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                    // //Rückrunde
                    // vpe.SelectFont("Arial", 10);
                    // strText = " ";
                    // intIndexSpieler1 = lstListe.FindIndex(f =>
                    //     f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 1);
                    // intIndexSpieler2 = lstListe.FindIndex(f =>
                    //     f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                    //     f.HinRueckrunde == 1);
                    //
                    // //Punkte
                    // if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                    // {
                    //     if (intIndexSpieler1 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                    //         }
                    //     }
                    //
                    //     if (intIndexSpieler2 != -1)
                    //     {
                    //         if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                    //         }
                    //         else
                    //         {
                    //             strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                    //         }
                    //     }
                    // }

                    strText = " ";
                    vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                }

                x = xBak;
                y = yBak;
            }

            yBak = vpe.nBottom;

            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.SelectFont("Arial", 8);
            //strText = "a" + i.ToString();
            strText = " "; // intSummeHinrunde.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -0.5, strText);
            //strText = "b" + i.ToString();
            strText = " "; // intSummeRückrunde.ToString();
            vpe.WriteBox(vpe.nLeft, vpe.nBottom, -0.8, -0.5, strText);
            //strText = "c" + i.ToString();
            strText = " "; //  intSummeHinRückrunde.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -1, strText);
            //strText = "d" + i.ToString();
            // switch (lstTeilnehmer[i].Platzierung)
            // {
            //     case 1:
            //         vpe.SelectFont("Arial", 18);
            //         break;
            //     case 2:
            //         vpe.SelectFont("Arial", 15);
            //         break;
            //     case 3:
            //         vpe.SelectFont("Arial", 12);
            //         break;
            //     default:
            //         vpe.SelectFont("Arial", 8);
            //         break;
            // }

            strText = " "; // lstTeilnehmer[i].Platzierung.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
            vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

            x = xErg;
            y = yBak;
        }

        vpe.SelectFont("Arial", 7);
        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
        strText = "Tot. Hi. Rü.";
        vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -1.7, -0.5, strText);
        for (Int32 i = 0; i < AnzahlTeilnehmer; i++)
        {
            vpe.SelectFont("Arial", 10);
            //Zeilensumme Punkte
            //strText = arrPunkte3bis8[i].ToString();
            strText = " "; // lstTeilnehmer[i].Punkte.ToString();
            vpe.WriteBox(vpe.nRight, vpe.nTop, -2.4, -0.5, strText);
            ////Zeilensumme 5 Kugeln
            ////strText = arrPunkte5Kugeln[i].ToString();
            //strText = lstTeilnehmer[i].Punkte5Kugeln.ToString();
            //vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
            //vpe.SelectFont("Arial", 8);
            //vpe.Write(vpe.nRight, vpe.nTop, -0.8, -0.5, "");
        }

        //Ende
        vpe.CloseProgressBar();

        if (PDFFilename == string.Empty)
            vpe.Preview();
        else
            vpe.PDFExport(PDFFilename);
    }

    public void DruckVorlageWeihnachtsbaum(string PDFFilename = "")
    {
        double cm = 28.35; // 1 cm in Punkten

        try
        {
            //********************
            //* Grundeinstellung *
            //********************
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Size = PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //*****************
            //* Bild zeichnen *
            //*****************

            // Bild aus den Ressourcen laden
            string resourceName = "KEPAVerwaltungWPF.Images.Weihnachtsbaumkegeln.jpg";

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                //Console.WriteLine("Ressource nicht gefunden!");
                return;
            }

            XImage img = XImage.FromStream(stream);
            gfx.DrawImage(img, 0.2 * cm, 0.2 * cm);

            //******************
            //* Texte zeichnen *
            //******************
            XFont font = new XFont("Arial", 20);
            XRect rect = new XRect(9 * cm, 2 * cm, 4 * cm, 1.5 * cm);
            gfx.DrawString("Name:", font, XBrushes.Black, rect, XStringFormats.Center);

            XPen pen = new XPen(XColors.Black, 1);
            pen.DashStyle = XDashStyle.Solid;

            //Tabellenkopf
            XRect rectLinks = new XRect(17 * cm, 4 * cm, 1.3 * cm, 1.1 * cm);
            XRect rectRechts = new XRect(rectLinks.X + rectLinks.Width, rectLinks.Y, 1.3 * cm, 1.1 * cm);

            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 10);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString("Wert je Wurf", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 10);
            tf.DrawString("Wert je Reihe", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //Zeile 1
            rectLinks.Y = rectLinks.Y + rectLinks.Height;
            rectLinks.Height = 2.5 * cm;
            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 20);
            tf.DrawString("10", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            rectRechts.Y = rectRechts.Y + rectRechts.Height;
            rectRechts.Height = 2.5 * cm;
            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 14);
            tf.DrawString(" ", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //Zeile 2
            rectLinks.Y = rectLinks.Y + rectLinks.Height;
            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 20);
            tf.DrawString("7", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            rectRechts.Y = rectRechts.Y + rectRechts.Height;
            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 14);
            tf.DrawString(" ", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //Zeile 3
            rectLinks.Y = rectLinks.Y + rectLinks.Height;
            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 20);
            tf.DrawString("4", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            rectRechts.Y = rectRechts.Y + rectRechts.Height;
            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 14);
            tf.DrawString(" ", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //Zeile 4
            rectLinks.Y = rectLinks.Y + rectLinks.Height;
            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 20);
            tf.DrawString("3", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            rectRechts.Y = rectRechts.Y + rectRechts.Height;
            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 14);
            tf.DrawString(" ", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //Zeile 5
            rectLinks.Y = rectLinks.Y + rectLinks.Height;
            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 20);
            tf.DrawString("2", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            rectRechts.Y = rectRechts.Y + rectRechts.Height;
            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 14);
            tf.DrawString(" ", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //Zeile 6
            rectLinks.Y = rectLinks.Y + rectLinks.Height;
            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 20);
            tf.DrawString("5", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            rectRechts.Y = rectRechts.Y + rectRechts.Height;
            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 14);
            tf.DrawString(" ", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //Zeile 7
            rectLinks.Y = rectLinks.Y + rectLinks.Height;
            gfx.DrawRectangle(pen, rectLinks.X, rectLinks.Y, rectLinks.Width, rectLinks.Height);
            font = new XFont("Arial", 20);
            tf.DrawString("8", font, XBrushes.Black, rectLinks, XStringFormats.TopLeft);

            rectRechts.Y = rectRechts.Y + rectRechts.Height;
            gfx.DrawRectangle(pen, rectRechts.X, rectRechts.Y, rectRechts.Width, rectRechts.Height);
            font = new XFont("Arial", 14);
            tf.DrawString(" ", font, XBrushes.Black, rectRechts, XStringFormats.TopLeft);

            //*******************
            //* Linien zeichnen *
            //*******************
            pen.DashStyle = XDashStyle.Dash;

            gfx.DrawLine(pen, 9.5 * cm, 5 * cm, 16.8 * cm, 5.5 * cm);
            gfx.DrawLine(pen, 10.8 * cm, 8.5 * cm, 16.8 * cm, 8 * cm);
            gfx.DrawLine(pen, 12.5 * cm, 10.6 * cm, 16.8 * cm, 10.5 * cm);
            gfx.DrawLine(pen, 14.2 * cm, 13.4 * cm, 16.8 * cm, 12.9 * cm);
            gfx.DrawLine(pen, 15.8 * cm, 15.4 * cm, 16.8 * cm, 15.4 * cm);
            gfx.DrawLine(pen, 15.6 * cm, 19 * cm, 16.8 * cm, 18 * cm);
            gfx.DrawLine(pen, 14.5 * cm, 21.4 * cm, 16.8 * cm, 20.5 * cm);

            //**********
            //* Gesamt *
            //**********
            font = new XFont("Arial", 20);
            rect = new XRect(16 * cm, 24 * cm, 4 * cm, 1.5 * cm);
            gfx.DrawString("Gesamt:", font, XBrushes.Black, rect, XStringFormats.Center);

            pen.DashStyle = XDashStyle.Solid;
            rect = new XRect(16.5 * cm, 25.5 * cm, 3 * cm, 3 * cm);
            gfx.DrawEllipse(pen, rect);

            //****************
            //* PDF erzeugen *
            //****************
            if (PDFFilename == string.Empty)
            {
                var filename = PdfFileUtility.GetTempPdfFullFileName("Vorlage_Weihnachtsbaum");
                document.Save(filename);
                PdfFileUtility.ShowDocument(filename);
            }
            else
                document.Save(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckVorlageWeihnachtsbaum", ex.Message);
        }
    }

    public void DruckvorlageAbrechnung(string PDFFilename = "")
    {
        Int32 x;

        //Anfang
        VpeToPdfSharp vpe = new();
        vpe.FileName = "Vorlage_Abrechnung";
        vpe.OpenDoc();
        vpe.OpenProgressBar();
        vpe.PageOrientation = PageOrientation.Landscape;
        vpe.SetMargins(1, 1, 2, 2);

        //Überschrift
        vpe.SelectFont("Arial", 18);
        vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
        vpe.Write(12, vpe.nTopMargin - 0.5, -3.5, -0.6, "Kegeln am ");
        vpe.PenStyle = PenStyle.DashDot;
        vpe.Line(vpe.nRight, vpe.nBottom, vpe.nRight + 3, vpe.nBottom);
        vpe.SelectFont("Arial", 12);
        vpe.Write(12, vpe.nBottom + 0.6, -3.5, -0.4, "Spielverluste vom ");
        vpe.Line(vpe.nRight, vpe.nBottom, vpe.nRight + 3, vpe.nBottom);

        //Zahlbeträge Überschift
        vpe.Write(vpe.nLeftMargin, vpe.nBottom + 0.6, -3, -1.2, "Name");
        vpe.PenStyle = PenStyle.Solid;
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.SelectFont("Arial", 9);
        vpe.Write(vpe.nRight, vpe.nTop, -1.8, -0.5, "Restbetrag");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.8, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "Beitrag");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "6 Tg. Ren.");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "Neun");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "Ratten");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "Sarg");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "zu Zahlen");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "gezahlt");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.6, -0.5, "noch offen");
        vpe.Write(vpe.nLeft, vpe.nBottom, -1.6, -0.5, "EUR");
        vpe.StorePos();
        vpe.Line(vpe.nRight, vpe.nTop - 0.5, vpe.nRight, vpe.nBottomMargin);
        vpe.RestorePos();
        vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRight, vpe.nBottom);

        //Zeilen
        vpe.SelectFont("Arial", 12);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        for (Int32 i = 0; i < 17; i++)
        {
            vpe.Write(vpe.nLeftMargin + 3 + 1.8, vpe.nBottom + 0.2, -1.6, -0.65, "10,-");
            vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6, vpe.nBottom);
        }

        //Abrechnung
        vpe.SelectFont("Arial", 18);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nLeftMargin + 3 + 1.8 + 10 * 1.6, vpe.nTopMargin + 5, -6, -1,
            "Abrechnung");

        vpe.SelectFont("Arial", 10);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Line(vpe.nRightMargin - 1.5, vpe.nBottom + 1, vpe.nRightMargin - 1.5, vpe.nBottomMargin);
        vpe.Write(vpe.nRightMargin - 1.5, vpe.nTop, -1.5, -0.6, "EURO");
        vpe.Line(vpe.nRightMargin - 1.5, vpe.nBottom, vpe.nRightMargin, vpe.nBottom);
        vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
        vpe.Write(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.3, -5, -1, "Anfangsbestand");
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom, vpe.nRightMargin, vpe.nBottom);
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.8, vpe.nRightMargin, vpe.nBottom + 0.8);
        vpe.Write(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.3, -5, -1, "plus Einnahmen");
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom, vpe.nRightMargin, vpe.nBottom);
        vpe.Write(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.3, -5, -1, "Zwischensumme");
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom, vpe.nRightMargin, vpe.nBottom);
        vpe.Write(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.3, -5, -1, "minus Ausgaben");
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom, vpe.nRightMargin, vpe.nBottom);

        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.8, vpe.nRightMargin, vpe.nBottom + 0.8);
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.8, vpe.nRightMargin, vpe.nBottom + 0.8);
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.8, vpe.nRightMargin, vpe.nBottom + 0.8);
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.8, vpe.nRightMargin, vpe.nBottom + 0.8);

        vpe.Write(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom + 0.3, -5, -1, "Bestand am");
        vpe.Line(vpe.nLeftMargin + 3 + 1.8 + 9 * 1.6 + 0.2, vpe.nBottom, vpe.nRightMargin, vpe.nBottom);

        //Ende
        vpe.CloseProgressBar();

        if (PDFFilename == string.Empty)
            vpe.Preview();
        else
            vpe.PDFExport(PDFFilename);
    }

    public void DruckVorlageSpielverluste(string PDFFilename = "")
    {
        //Anfang
        VpeToPdfSharp vpe = new();
        vpe.FileName = "Vorlage_Spielverluste";
        vpe.OpenDoc();
        vpe.OpenProgressBar();
        vpe.PageOrientation = PageOrientation.Landscape;
        vpe.SetMargins(2, 2, 2, 2);

        //Trennlinie
        vpe.PenStyle = PenStyle.DashDotDot;
        vpe.Line((vpe.nLeftMargin + vpe.nRightMargin) / 2, vpe.nTopMargin, (vpe.nLeftMargin + vpe.nRightMargin) / 2,
            vpe.nBottomMargin);
        vpe.PenStyle = PenStyle.Solid;

        //Linke Hälfte
        //vpe.TextAlignment = TextAlignment.Center;
        vpe.SelectFont("Arial", 14);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, -11.5, -0.5, "Spielverluste");

        vpe.SelectFont("Arial", 10);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nLeftMargin, vpe.nBottom, -11.5, -0.5, "vom");
        vpe.PenStyle = PenStyle.Dash;
        vpe.Box(vpe.nLeftMargin + (vpe.nLeftMargin + 11.5) / 2 - 3, vpe.nBottom, -4, -1);
        vpe.PenStyle = PenStyle.Solid;

        vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
        vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -4, -0.8, "Name");
        vpe.StorePos();
        vpe.Line(vpe.nLeftMargin, vpe.nBottom + 0.5, vpe.nLeftMargin + 11.5, vpe.nBottom + 0.5);
        vpe.RestorePos();
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);

        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "6 Tg. R.");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "Neun");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "Ratten");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "Sarg");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);

        vpe.Line(vpe.nLeftMargin, vpe.nTop + 2, vpe.nLeftMargin + 11.5, vpe.nTop + 2);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);
        vpe.Line(vpe.nLeftMargin, vpe.nTop + 1, vpe.nLeftMargin + 11.5, vpe.nTop + 1);

        //Rechte Hälfte
        //vpe.TextAlignment = TextAlignment.Center;
        vpe.SelectFont("Arial", 14);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nLeftMargin + (vpe.nLeftMargin + vpe.nRightMargin) / 2, vpe.nTopMargin, -11.5, -0.5,
            "Spielverluste");

        vpe.SelectFont("Arial", 10);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nLeftMargin + (vpe.nLeftMargin + vpe.nRightMargin) / 2, vpe.nBottom, -11.5, -0.5, "vom");
        vpe.PenStyle = PenStyle.Dash;
        vpe.Box((vpe.nLeftMargin + vpe.nRightMargin) / 2 + vpe.nLeftMargin + (vpe.nLeftMargin + 11.5) / 2 - 3,
            vpe.nBottom, -4, -1);
        vpe.PenStyle = PenStyle.Solid;

        vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
        vpe.Write(vpe.nLeftMargin + (vpe.nLeftMargin + vpe.nRightMargin) / 2, vpe.nBottom + 1, -4, -0.8, "Name");
        vpe.StorePos();
        vpe.Line(vpe.nLeftMargin + (vpe.nLeftMargin + vpe.nRightMargin) / 2, vpe.nBottom + 0.5,
            vpe.nLeftMargin + (vpe.nLeftMargin + vpe.nRightMargin) / 2 + 11.5,
            vpe.nBottom + 0.5);
        vpe.RestorePos();
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);

        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "6 Tg. R.");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "Neun");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "Ratten");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);
        vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.8, "Sarg");
        vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nBottomMargin);

        double dblLeft = vpe.nLeftMargin + (vpe.nLeftMargin + vpe.nRightMargin) / 2;
        double dblRight = vpe.nLeftMargin + (vpe.nLeftMargin + vpe.nRightMargin) / 2 + 11.5;
        vpe.Line(dblLeft, vpe.nTop + 2, dblRight, vpe.nTop + 2);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);
        vpe.Line(dblLeft, vpe.nTop + 1, dblRight, vpe.nTop + 1);

        //Ende
        vpe.CloseProgressBar();

        if (PDFFilename == string.Empty)
            vpe.Preview();
        else
            vpe.PDFExport(PDFFilename);
    }

    #endregion

    #region Statistik

    public async Task DruckSpielerErgebnisseStatistikAsync(MitgliedDetails mitglied, bool DruckErgebnisse,
        bool DruckStatistik, string PDFFilename = "")
    {
        try
        {
            // Dokument erstellen
            Document document = new Document();
            Section section = document.AddSection();
            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.LeftMargin = Unit.FromCentimeter(0.5);
            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(2.5);

            // Titel hinzufügen
            Paragraph title = section.AddParagraph("Spielergebnisse von", "Heading1");
            title.Format.Alignment = ParagraphAlignment.Center;
            section.AddParagraph(
                mitglied.Nachname + ", " + mitglied.Vorname,
                "Heading1");

            if (DruckErgebnisse)
            {
                // Tabelle für Ergebnisse
                Table table = section.AddTable();
                table.Borders.Width = 0.5;
                table.AddColumn(Unit.FromCentimeter(2)); // Spieltag
                table.AddColumn(Unit.FromCentimeter(3)); // Meisterschaft
                table.AddColumn(Unit.FromCentimeter(3)); // Gegenspieler
                table.AddColumn(Unit.FromCentimeter(1)); // Erg.
                table.AddColumn(Unit.FromCentimeter(1)); // Holz
                table.AddColumn(Unit.FromCentimeter(1.5)); // 6 Tage Runden
                table.AddColumn(Unit.FromCentimeter(1.5)); // 6 Tage Punkte
                table.AddColumn(Unit.FromCentimeter(1.5)); // 6 Tage Platz
                table.AddColumn(Unit.FromCentimeter(1)); // Sarg
                table.AddColumn(Unit.FromCentimeter(1)); // Pokal
                table.AddColumn(Unit.FromCentimeter(1)); // 9er
                table.AddColumn(Unit.FromCentimeter(1.5)); // Ratten

                Row headerRow = table.AddRow();
                headerRow.Shading.Color = Colors.LightGray;
                headerRow.Cells[0].AddParagraph("Spieltag");
                headerRow.Cells[1].AddParagraph("Meisterschaft");
                headerRow.Cells[2].AddParagraph("Gegenspieler");
                headerRow.Cells[3].AddParagraph("Erg.");
                headerRow.Cells[4].AddParagraph("Holz");
                headerRow.Cells[5].AddParagraph("6 Tage Runden");
                headerRow.Cells[6].AddParagraph("6 Tage Punkte");
                headerRow.Cells[7].AddParagraph("6 Tage Platz");
                headerRow.Cells[8].AddParagraph("Sarg");
                headerRow.Cells[9].AddParagraph("Pokal");
                headerRow.Cells[10].AddParagraph("9er");
                headerRow.Cells[11].AddParagraph("Ratten");

                // Daten hinzufügen
                var StatistikSpielerErgebnisse = await _dbService.GetStatistikSpielerErgebnisseAsync(mitglied.ID);
                for (int i = 1; i < StatistikSpielerErgebnisse.Count; i++)
                {
                    Row row = table.AddRow();
                    row.Cells[0].AddParagraph(Convert
                        .ToDateTime(StatistikSpielerErgebnisse[i].Spieltag).ToShortDateString());
                    row.Cells[1]
                        .AddParagraph(StatistikSpielerErgebnisse[i].Meisterschaft.ToString());
                    row.Cells[2]
                        .AddParagraph(StatistikSpielerErgebnisse[i].Gegenspieler.ToString());
                    row.Cells[3].AddParagraph(StatistikSpielerErgebnisse[i].Ergebnis.ToString());
                    row.Cells[4].AddParagraph(StatistikSpielerErgebnisse[i].Holz.ToString());
                    row.Cells[5].AddParagraph(StatistikSpielerErgebnisse[i].SechsTageRennen_Runden
                        .ToString());
                    row.Cells[6].AddParagraph(StatistikSpielerErgebnisse[i].SechsTageRennen_Punkte
                        .ToString());
                    row.Cells[7].AddParagraph(StatistikSpielerErgebnisse[i].SechsTageRennen_Platz
                        .ToString());
                    row.Cells[8].AddParagraph(StatistikSpielerErgebnisse[i].Sarg.ToString());
                    row.Cells[9].AddParagraph(StatistikSpielerErgebnisse[i].Pokal.ToString());
                    row.Cells[10].AddParagraph(StatistikSpielerErgebnisse[i].Neuner.ToString());
                    row.Cells[11].AddParagraph(StatistikSpielerErgebnisse[i].Ratten.ToString());
                }
            }

            if (DruckStatistik)
            {
                section.AddPageBreak();
                section.AddParagraph("Statistik von", "Heading1");
                section.AddParagraph(
                    mitglied.Nachname + ", " + mitglied.Vorname,
                    "Heading1");

                Table statTable = section.AddTable();
                statTable.Borders.Width = 0.5;
                statTable.AddColumn(Unit.FromCentimeter(4));
                statTable.AddColumn(Unit.FromCentimeter(3));

                string[] statLabels =
                {
                    "Holz Sum.", "Holz Max.", "Holz Min.", "Holz AVG.", "Ratten Sum.", "Ratten Max.", "9er Sum.",
                    "9er Max."
                };
                string[] statKeys =
                {
                    "HolzSumme", "HolzMax", "HolzMin", "HolzAVG", "RattenSumme", "RattenMax", "NeunerSumme", "NeunerMax"
                };


                var StatistikSpieler = await _dbService.GetStatistikSpielerAsync(mitglied.ID);
                for (int i = 0; i < statLabels.Length; i++)
                {
                    Row row = statTable.AddRow();
                    row.Cells[0].AddParagraph(statLabels[i]);
                    row.Cells[1].AddParagraph(StatistikSpieler[0].GetType()
                        .GetProperty(statKeys[i])?.GetValue(StatistikSpieler[0], null)
                        ?.ToString() ?? "");
                }
            }

            // using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            // using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("https://kegelgruppe-kepa.de", QRCodeGenerator.ECCLevel.Q))
            // using (Base64QRCode base64QRCode = new Base64QRCode(qrCodeData))
            // {
            //     //Section secQR = document.AddSection();
            //     //secQR.AddImage("base64:" + base64QRCode.GetGraphic(1));
            //     section.AddImage("base64:" + base64QRCode.GetGraphic(1));
            //     // secQR.AddImage("base64:" + base64QRCode.GetGraphic(2));
            //     // secQR.AddImage("base64:" + base64QRCode.GetGraphic(5));
            // }

            // Dokument rendern und anzeigen
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();

            if (PDFFilename == string.Empty)
            {
                var filename = PdfFileUtility.GetTempPdfFullFileName("Statistik_" +
                                                                     mitglied.Nachname + "_" +
                                                                     mitglied.Vorname);
                pdfRenderer.PdfDocument.Save(filename);
                // ...and start a viewer.
                PdfFileUtility.ShowDocument(filename);
            }
            else
            {
                pdfRenderer.PdfDocument.Save(PDFFilename);
            }
        }
        catch (Exception ex)
        {
            // FrmError objForm = new FrmError("FrmMitglieder", "DruckSpielerErgebnisseStatistik", ex.ToString());
            // objForm.ShowDialog();
            ViewManager.ShowErrorWindow("PrintService", "DruckSpielerErgebnisseStatistik", ex.ToString());
        }
    }

    public async Task DruckStatistikNeunerAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        string strText = string.Empty;
        DateTime? datVon = dtVon;
        DateTime? datBis = dtBis;
       
        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_Neuner";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Landscape;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "Neuner");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            List<Statistik9er> lst9er = await _dbService.GetStatistik9erAsync(iZeitbereich, datVon, datBis);
            Int32 intCount = lst9er.Count;

            //Kopfzeile
            vpe.SelectFont("Arial", 14);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -1.5, -1, "Pos.");
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -6, -1, "Spieler");
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nRight, vpe.nTop, -1.5, -1, "Ges.");
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -1.5, -1.5, "Anz. TN.");
            double dblY = vpe.nBottom;
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            for (Int32 i = 10; i > 0; i--)
            {
                vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, i.ToString());
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            }

            vpe.Line(vpe.nLeftMargin, dblY, vpe.nRight, dblY);

            //Daten
            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);

            for (Int32 i = 0; i < lst9er.Count; i++)
            {
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1.5, -0.7, (i + 1).ToString());
                vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -6, -0.7, lst9er[i].Spieler);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.7, lst9er[i].Gesamt.ToString());
                vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.7, lst9er[i].AnzTeilnahmen.ToString());
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                for (Int32 j = 10; j > 0; j--)
                {
                    strText = string.Empty;
                    switch (j)
                    {
                        case 10:
                            if (lst9er[i].Zehn > 0) strText = lst9er[i].Zehn.ToString();
                            break;
                        case 9:
                            if (lst9er[i].Neun > 0) strText = lst9er[i].Neun.ToString();
                            break;
                        case 8:
                            if (lst9er[i].Acht > 0) strText = lst9er[i].Acht.ToString();
                            break;
                        case 7:
                            if (lst9er[i].Sieben > 0) strText = lst9er[i].Sieben.ToString();
                            break;
                        case 6:
                            if (lst9er[i].Sechs > 0) strText = lst9er[i].Sechs.ToString();
                            break;
                        case 5:
                            if (lst9er[i].Fünf > 0) strText = lst9er[i].Fünf.ToString();
                            break;
                        case 4:
                            if (lst9er[i].Vier > 0) strText = lst9er[i].Vier.ToString();
                            break;
                        case 3:
                            if (lst9er[i].Drei > 0) strText = lst9er[i].Drei.ToString();
                            break;
                        case 2:
                            if (lst9er[i].Zwei > 0) strText = lst9er[i].Zwei.ToString();
                            break;
                        case 1:
                            if (lst9er[i].Eins > 0) strText = lst9er[i].Eins.ToString();
                            break;
                    }

                    vpe.Write(vpe.nRight, vpe.nTop, -1, -0.7, strText);
                }

                vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRight, vpe.nBottom);
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistikNeuner", ex.ToString());
        }
    }

    public async Task DruckStatistikRattenAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        string strText = string.Empty;
        DateTime? datVon = dtVon;
        DateTime? datBis = dtBis;
        Int32 intZeitbereich = 0;

        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_Ratten";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Landscape;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "Ratten");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            List<StatistikRatten> lstRatten = await _dbService.GetStatistikRattenAsync(intZeitbereich, datVon, datBis);
            Int32 intCount = lstRatten.Count;

            //Kopfzeile
            vpe.SelectFont("Arial", 14);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -1.5, -1, "Pos.");
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -6, -1, "Spieler");
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nRight, vpe.nTop, -1.5, -1, "Ges.");
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -1.5, -1.5, "Anz. TN.");
            double dblY = vpe.nBottom;
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            for (Int32 i = 10; i > 0; i--)
            {
                vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, i.ToString());
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            }

            vpe.Line(vpe.nLeftMargin, dblY, vpe.nRight, dblY);

            //Daten
            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);

            for (Int32 i = 0; i < lstRatten.Count; i++)
            {
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1.5, -0.7, (i + 1).ToString());
                vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -6, -0.7, lstRatten[i].Spieler);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.7, lstRatten[i].Gesamt.ToString());
                vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.7, lstRatten[i].AnzTeilnahmen.ToString());
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                for (Int32 j = 10; j > 0; j--)
                {
                    strText = string.Empty;
                    switch (j)
                    {
                        case 10:
                            if (lstRatten[i].Zehn > 0) strText = lstRatten[i].Zehn.ToString();
                            break;
                        case 9:
                            if (lstRatten[i].Neun > 0) strText = lstRatten[i].Neun.ToString();
                            break;
                        case 8:
                            if (lstRatten[i].Acht > 0) strText = lstRatten[i].Acht.ToString();
                            break;
                        case 7:
                            if (lstRatten[i].Sieben > 0) strText = lstRatten[i].Sieben.ToString();
                            break;
                        case 6:
                            if (lstRatten[i].Sechs > 0) strText = lstRatten[i].Sechs.ToString();
                            break;
                        case 5:
                            if (lstRatten[i].Fünf > 0) strText = lstRatten[i].Fünf.ToString();
                            break;
                        case 4:
                            if (lstRatten[i].Vier > 0) strText = lstRatten[i].Vier.ToString();
                            break;
                        case 3:
                            if (lstRatten[i].Drei > 0) strText = lstRatten[i].Drei.ToString();
                            break;
                        case 2:
                            if (lstRatten[i].Zwei > 0) strText = lstRatten[i].Zwei.ToString();
                            break;
                        case 1:
                            if (lstRatten[i].Eins > 0) strText = lstRatten[i].Eins.ToString();
                            break;
                    }

                    vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, strText);
                }

                vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRight, vpe.nBottom);
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistikRattenAsync", ex.ToString());
        }
    }

    public async Task DruckStatistikPokalAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        string strText = string.Empty;
        DateTime? datVon = dtVon;
        DateTime? datBis = dtBis;
        Int32 intZeitbereich = 0;

        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_Pokal";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Portrait;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "Pokal");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            List<StatistikPokal> lstPokal = await _dbService.GetStatistikPokalAsync(intZeitbereich, datVon, datBis);
            Int32 intCount = lstPokal.Count;

            //Kopfzeile
            vpe.SelectFont("Arial", 14);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -1.5, -1, "Pos.");
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -6, -1, "Spieler");
            double dblY = vpe.nBottom;
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.SelectFont("Arial", 13);
            for (Int32 i = 1; i <= 2; i++)
            {
                vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, "Plz. " + i.ToString());
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            }

            vpe.Line(vpe.nLeftMargin, dblY, vpe.nRight, dblY);

            //Daten
            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);

            for (Int32 i = 0; i < lstPokal.Count; i++)
            {
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1.5, -0.7, (i + 1).ToString());
                vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -6, -0.7, lstPokal[i].Spieler);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                for (Int32 j = 1; j <= 2; j++)
                {
                    strText = string.Empty;
                    switch (j)
                    {
                        case 1:
                            if (lstPokal[i].Eins > 0) strText = lstPokal[i].Eins.ToString();
                            break;
                        case 2:
                            if (lstPokal[i].Zwei > 0) strText = lstPokal[i].Zwei.ToString();
                            break;
                    }

                    vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, strText);
                }

                vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRight, vpe.nBottom);
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistikPokalAsync", ex.ToString());
        }
    }

    public async Task DruckStatistikSargAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        string strText = string.Empty;
        DateTime? datVon = dtVon;
        DateTime? datBis = dtBis;
        Int32 intZeitbereich = 0;

        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_Sarg";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Landscape;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "Sarg");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            List<StatistikSarg> lstSarg = await _dbService.GetStatistikSargAsync(intZeitbereich, datVon, datBis);
            Int32 intCount = lstSarg.Count;

            //Kopfzeile
            vpe.SelectFont("Arial", 14);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -1.5, -1, "Pos.");
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -6, -1, "Spieler");
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nRight, vpe.nTop, -1.5, -1.5, "Anz. TN.");
            double dblY = vpe.nBottom;
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.SelectFont("Arial", 13);
            for (Int32 i = 1; i <= 10; i++)
            {
                vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, "Plz. " + i.ToString());
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            }

            vpe.Line(vpe.nLeftMargin, dblY, vpe.nRight, dblY);

            //Daten
            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);

            for (Int32 i = 0; i < lstSarg.Count; i++)
            {
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1.5, -0.7, (i + 1).ToString());
                vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -6, -0.7, lstSarg[i].Spieler);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.7, lstSarg[i].AnzTeilnahmen.ToString());
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                for (Int32 j = 1; j <= 10; j++)
                {
                    strText = string.Empty;
                    switch (j)
                    {
                        case 1:
                            if (lstSarg[i].Eins > 0) strText = lstSarg[i].Eins.ToString();
                            break;
                        case 2:
                            if (lstSarg[i].Zwei > 0) strText = lstSarg[i].Zwei.ToString();
                            break;
                        case 3:
                            if (lstSarg[i].Drei > 0) strText = lstSarg[i].Drei.ToString();
                            break;
                        case 4:
                            if (lstSarg[i].Vier > 0) strText = lstSarg[i].Vier.ToString();
                            break;
                        case 5:
                            if (lstSarg[i].Fünf > 0) strText = lstSarg[i].Fünf.ToString();
                            break;
                        case 6:
                            if (lstSarg[i].Sechs > 0) strText = lstSarg[i].Sechs.ToString();
                            break;
                        case 7:
                            if (lstSarg[i].Sieben > 0) strText = lstSarg[i].Sieben.ToString();
                            break;
                        case 8:
                            if (lstSarg[i].Acht > 0) strText = lstSarg[i].Acht.ToString();
                            break;
                        case 9:
                            if (lstSarg[i].Neun > 0) strText = lstSarg[i].Neun.ToString();
                            break;
                        case 10:
                            if (lstSarg[i].Zehn > 0) strText = lstSarg[i].Zehn.ToString();
                            break;
                    }

                    vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, strText);
                }

                vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRight, vpe.nBottom);
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistikSargAsync", ex.ToString());
        }
    }

    public void DruckStatistikSpielerSpieler(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_Spieler_Spieler";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Portrait;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "Spieler / Spieler");


            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistikSpielerSpieler", ex.ToString());
        }
    }

    public async Task DruckStatistik6TageRennenPlatzAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        string strText = string.Empty;
        DateTime? datVon = dtVon;
        DateTime? datBis = dtBis;
        Int32 intZeitbereich = 0;

        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_6TageRennen_Platzierung";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Portrait;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "6 Tage Rennen Platzierung");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            List<Statistik6TageRennenPlatzierung> lst6TR = await _dbService.GetStatistik6TageRennenPlatzAsync(
                intZeitbereich,
                datVon, datBis);
            Int32 intCount = lst6TR.Count;

            //Kopfzeile
            vpe.SelectFont("Arial", 14);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -1.5, -1, "Pos.");
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -6, -1, "Spieler");
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nRight, vpe.nTop, -1.5, -1.5, "Anz. TN.");
            double dblY = vpe.nBottom;
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.SelectFont("Arial", 13);
            for (Int32 i = 1; i <= 6; i++)
            {
                vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, "Plz. " + i.ToString());
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            }

            vpe.Line(vpe.nLeftMargin, dblY, vpe.nRight, dblY);

            //Daten
            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);

            for (Int32 i = 0; i < lst6TR.Count; i++)
            {
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1.5, -0.7, (i + 1).ToString());
                vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -6, -0.7, lst6TR[i].Spieler);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -1.5, -0.7, lst6TR[i].AnzTeilnahmen.ToString());
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                for (Int32 j = 1; j <= 6; j++)
                {
                    strText = string.Empty;
                    switch (j)
                    {
                        case 1:
                            if (lst6TR[i].Eins > 0) strText = lst6TR[i].Eins.ToString();
                            break;
                        case 2:
                            if (lst6TR[i].Zwei > 0) strText = lst6TR[i].Zwei.ToString();
                            break;
                        case 3:
                            if (lst6TR[i].Drei > 0) strText = lst6TR[i].Drei.ToString();
                            break;
                        case 4:
                            if (lst6TR[i].Vier > 0) strText = lst6TR[i].Vier.ToString();
                            break;
                        case 5:
                            if (lst6TR[i].Fünf > 0) strText = lst6TR[i].Fünf.ToString();
                            break;
                        case 6:
                            if (lst6TR[i].Sechs > 0) strText = lst6TR[i].Sechs.ToString();
                            break;
                    }

                    vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, strText);
                }

                vpe.Line(vpe.nLeftMargin, vpe.nBottom, vpe.nRight, vpe.nBottom);
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistik6TageRennenPlatzAsync", ex.ToString());
        }
    }

    public async Task DruckStatistik6TageRennenBesteAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        string strText = string.Empty;
        DateTime? datVon = dtVon;
        DateTime? datBis = dtBis;
        Int32 intZeitbereich = 0;

        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_6TageRennen_Beste";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Portrait;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "6 Tage Rennen Beste Mannschaft");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            List<Statistik6TageRennenBesteMannschaft> lst6TR =
                await _dbService.GetStatistik6TageRennenBesteMannschaftAsync(intZeitbereich, datVon, datBis);
            Int32 intCount = lst6TR.Count;

            //Kopfzeile
            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -1, -1, "Pos.");
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.Write(vpe.nRight, vpe.nTop, -7, -1, "Mannschaft");
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nRight, vpe.nTop, -1, -1, "Anz.");
            double dblY = vpe.nBottom;
            vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.SelectFont("Arial", 11);
            for (Int32 i = 1; i <= 6; i++)
            {
                vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, "Plz.  " + i.ToString());
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1.5 + intCount * 0.7);
            }

            vpe.Line(vpe.nLeftMargin, dblY, vpe.nRight, dblY);

            //Daten
            vpe.SelectFont("Arial", 10);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
            for (Int32 i = 0; i < lst6TR.Count; i++)
            {
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1, -0.7, (i + 1).ToString());
                vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -7, -0.7, lst6TR[i].Mannschaft);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -1, -0.7, lst6TR[i].Anzahl.ToString());
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                for (Int32 j = 1; j <= 6; j++)
                {
                    strText = string.Empty;
                    switch (j)
                    {
                        case 1:
                            if (lst6TR[i].Eins > 0) strText = lst6TR[i].Eins.ToString();
                            break;
                        case 2:
                            if (lst6TR[i].Zwei > 0) strText = lst6TR[i].Zwei.ToString();
                            break;
                        case 3:
                            if (lst6TR[i].Drei > 0) strText = lst6TR[i].Drei.ToString();
                            break;
                        case 4:
                            if (lst6TR[i].Vier > 0) strText = lst6TR[i].Vier.ToString();
                            break;
                        case 5:
                            if (lst6TR[i].Fünf > 0) strText = lst6TR[i].Fünf.ToString();
                            break;
                        case 6:
                            if (lst6TR[i].Sechs > 0) strText = lst6TR[i].Sechs.ToString();
                            break;
                    }

                    vpe.Write(vpe.nRight, vpe.nTop, -1, vpe.nBottom, strText);
                }
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistik6TageRennenBesteAsync", ex.ToString());
        }
    }

    public async Task DruckStatistik6TageRennenMannschaftAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        string strText = string.Empty;
        DateTime? datVon = dtVon;
        DateTime? datBis = dtVon;
        Int32 intZeitbereich = 0;

        try
        {
            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "Druck_Statistik_6TageRennen_Mannschaft";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Portrait;
            vpe.SetMargins(2, 2, 2, 2);

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1,
                "6 Tage Rennen Mannschaft / Mitglied");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            Dictionary<string, List<Statistik6TageRennenBesteMannschaft>> dict6TR =
                await _dbService.GetStatistik6TageRennenMannschaftMitgliedAsync(intZeitbereich, datVon, datBis);
            //Int32 intCount = dict6TR.Count;

            foreach (var item in dict6TR)
            {
                Int32 intCount = item.Value.Count;

                //Passt Tabelle noch auf Seite?
                if (vpe.nBottomMargin - vpe.nBottom < 1 + intCount * 0.5)
                {
                    vpe.PageBreak();
                }

                //Kopfzeile
                vpe.SelectFont("Arial", 12);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -1, -1, "Pos.");
                vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1 + intCount * 0.5);
                vpe.Write(vpe.nRight + 0.2, vpe.nTop, -7, -1, "Mannschaft");
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1 + intCount * 0.5);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.Write(vpe.nRight, vpe.nTop, -1, -1, "Anz.");
                double dblY = vpe.nBottom;
                vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1 + intCount * 0.5);
                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.SelectFont("Arial", 11);
                for (Int32 i = 1; i <= 6; i++)
                {
                    vpe.Write(vpe.nRight, vpe.nTop, -1, -1, "Plz.  " + i.ToString());
                    vpe.Line(vpe.nRight, vpe.nTop, vpe.nRight, vpe.nTop + 1 + intCount * 0.5);
                }

                vpe.Line(vpe.nLeftMargin, dblY, vpe.nRight, dblY);

                //Daten
                vpe.SelectFont("Arial", 10);
                vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
                for (Int32 i = 0; i < intCount; i++)
                {
                    vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                    vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1, -0.5, (i + 1).ToString());
                    vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
                    vpe.Write(vpe.nRight + 0.2, vpe.nTop, -7, -0.5, dict6TR[item.Key][i].Mannschaft);
                    vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                    vpe.Write(vpe.nRight, vpe.nTop, -1, -0.5, dict6TR[item.Key][i].Anzahl.ToString());
                    vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                    for (Int32 j = 1; j <= 6; j++)
                    {
                        strText = string.Empty;
                        switch (j)
                        {
                            case 1:
                                if (dict6TR[item.Key][i].Eins > 0) strText = dict6TR[item.Key][i].Eins.ToString();
                                break;
                            case 2:
                                if (dict6TR[item.Key][i].Zwei > 0) strText = dict6TR[item.Key][i].Zwei.ToString();
                                break;
                            case 3:
                                if (dict6TR[item.Key][i].Drei > 0) strText = dict6TR[item.Key][i].Drei.ToString();
                                break;
                            case 4:
                                if (dict6TR[item.Key][i].Vier > 0) strText = dict6TR[item.Key][i].Vier.ToString();
                                break;
                            case 5:
                                if (dict6TR[item.Key][i].Fünf > 0) strText = dict6TR[item.Key][i].Fünf.ToString();
                                break;
                            case 6:
                                if (dict6TR[item.Key][i].Sechs > 0) strText = dict6TR[item.Key][i].Sechs.ToString();
                                break;
                        }

                        vpe.Write(vpe.nRight, vpe.nTop, -1, -0.5, strText);
                    }
                }
            }

            //Ende
            vpe.CloseProgressBar();
            vpe.Preview();
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistik6TageRennenMannschaftAsync", ex.ToString());
        }
    }

    public async Task DruckStatistikNeunerRattenAsync(Int32 iZeitbereich, DateTime? dtVon = null,
        DateTime? dtBis = null, string PDFFilename = "")
    {
        try
        {
            string strText = string.Empty;
            DateTime? datVon = dtVon;
            DateTime? datBis = dtBis;
            Int32 intZeitbereich = 0;

            StatistikNeunerRattenKoenig obj9erRatten = await _dbService.GetStatistik9erRattenAsync(intZeitbereich, datVon,
                datBis);

            //Anfang
            VpeToPdfSharp vpe = new ();
            vpe.FileName = "Druck_Statistik_NeunerRatten";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Portrait;
            vpe.SetMargins(2,2,2,2);
            //vpe.AutoBreakMode = AutoBreakMode.Off;

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nRightMargin, -1, "Neunerkönig / Rattenorden");

            //Zeitraum
            switch (iZeitbereich)
            {
                case 1: // Laufende Meisterschaft
                    var intMeisterschaftsID = _commonService.AktiveMeisterschaft.ID;
                    if (intMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichAktuell = await _dbService.GetZeitbereichAsync(intMeisterschaftsID);
                    if (objBereichAktuell == null) strText = "n/a";

                    datVon = objBereichAktuell!.Von;

                    if (objBereichAktuell.Bis.HasValue)
                    {
                        datBis = objBereichAktuell!.Bis;
                    }
                    else if (datVon < DateTime.Now)
                    {
                        datBis = DateTime.Now;
                    }
                    else
                    {
                        datBis = datVon;
                    }

                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 2: // Letzte Meisterschaft
                    var intLetzteMeisterschaftsID = await _dbService.GetLetzteMeisterschaftsIDAsync();
                    if (intLetzteMeisterschaftsID == 0) strText = "n/a";

                    Zeitbereich? objBereichLast = await _dbService.GetZeitbereichAsync(intLetzteMeisterschaftsID);
                    if (objBereichLast == null) strText = "n/a";

                    datVon = objBereichLast.Von;
                    datBis = objBereichLast.Bis;
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();

                    break;
                case 3: // Zeitbereich
                    strText = datVon.Value.ToShortDateString() + " - " + datBis.Value.ToShortDateString();
                    break;
                case 4: // Gesamt
                    strText = "Gesamt";
                    break;
            }

            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, vpe.nRightMargin, -1, strText);

            //Kopfzeile
            vpe.SelectFont("Arial", 14);
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 1, -3, -0.7, "Spieltag");
            vpe.Write(vpe.nRight + 1, vpe.nTop, -6, -0.7, "Neunerkönig");
            vpe.Write(vpe.nRight + 1, vpe.nTop, -6, -0.7, "Rattenorden");

            vpe.Write(vpe.nLeftMargin, vpe.nBottom, -3, -0.3, "");

            //Daten
            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);

            double dblTop = vpe.nBottom;
            double dblLeft = vpe.nLeftMargin;
            foreach (var item in obj9erRatten.lstStatistik9erRatten)
            {
                //vpe.Write(vpe.nLeftMargin, vpe.nBottom, -3, vpe.nFree, item.Spieltag.ToShortDateString());
                vpe.Write(dblLeft, dblTop, -3, -0.7, item.Spieltag.ToShortDateString());

                strText = string.IsNullOrEmpty(item.Neunerkönig) ? "---" : item.Neunerkönig;
                vpe.Write(vpe.nRight + 1, vpe.nTop, -6, -0.7, strText);

                dblTop = vpe.nBottom + 0.3;
                strText = string.IsNullOrEmpty(item.Rattenorden) ? "---" : item.Rattenorden;
                vpe.Write(vpe.nRight + 1, vpe.nTop, -6, -0.7, strText);

                if (dblTop < vpe.nBottom) dblTop = vpe.nBottom + 0.3;
            }

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom + 2, -6, -0.7, "Neunerkönig");
            vpe.StorePos();

            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
            if (obj9erRatten.dictNeunerkönig.Count == 0)
            {
                vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1, -0.7, "-");
                vpe.Write(vpe.nRight, vpe.nTop, -5, -0.7, "---");
            }
            else
            {
                foreach (KeyValuePair<string, int> item in obj9erRatten.dictNeunerkönig.OrderByDescending(o => o.Value)
                             .ThenBy(t => t.Key))
                {
                    vpe.Write(vpe.nLeftMargin, vpe.nBottom, -1, -0.7, item.Value.ToString());
                    vpe.Write(vpe.nRight, vpe.nTop, -5, -0.7, item.Key);
                }
            }

            vpe.RestorePos();
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Left, true, false, false, false);
            vpe.Write(vpe.nRight, vpe.nTop, -6, -0.7, "Rattenorden");
            dblLeft = vpe.nLeft;

            vpe.SelectFont("Arial", 12);
            vpe.SetFontAttr(TextAlignment.Left, false, false, false, false);
            if (obj9erRatten.dictRattenkönig.Count == 0)
            {
                vpe.Write(dblLeft, vpe.nBottom, -1, -0.7, "-");
                vpe.Write(vpe.nRight, vpe.nTop, -5, -0.7, "---");
            }
            else
            {
                foreach (KeyValuePair<string, int> item in obj9erRatten.dictRattenkönig.OrderByDescending(o => o.Value)
                             .ThenBy(t => t.Key))
                {
                    vpe.Write(dblLeft, vpe.nBottom, -1, -0.7, item.Value.ToString());
                    vpe.Write(vpe.nRight, vpe.nTop, -5, -0.7, item.Key);
                }
            }

            //Ende
            vpe.CloseProgressBar();
            vpe.Preview();
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckStatistikNeunerRattenAsync", ex.ToString());
        }
    }

    #endregion

    #region Ergebnisse

    public async Task DruckErgebnisKombimeisterschaftAsync(int MeisterschaftsID, string PDFFilename = "")
    {
        try
        {
            List<ErgebnisKombimeisterschaftKreuztabelle> lstListe =
                await _dbService.GetErgebnisKombimeisterschaftAsync(MeisterschaftsID);
            List<ErgebnisKombimeisterschaftPlatzierung> lstTeilnehmer =
                new List<ErgebnisKombimeisterschaftPlatzierung>();
            string strMeisterschaft = await _dbService.GetMeisterschaftsbezeichnungAsync(MeisterschaftsID);
            string strMeisterschaftstyp =
                await _dbService.GetMeisterschaftstypFromMeisterschaftByIDAsync(MeisterschaftsID);
            //Int32[] arrPunkte3bis8 = new Int32[1];
            //Int32[] arrPunkte5Kugeln = new Int32[1];
            Double x;
            Double y;
            Double xZeile;
            Double yZeile;
            Double xBak;
            Double yBak;
            Double xErg;
            Double yErg;
            string strText = string.Empty;
            Int32 intSummeHinrunde = 0;
            Int32 intSummeRückrunde = 0;
            Int32 intSummeHinRückrunde = 0;
            Int32 intIndex = -1;
            Int32 intIndexSpieler1 = -1;
            Int32 intIndexSpieler2 = -1;
            Int32 intPlatzierungTemp = 0;
            Int32 intPunkteTemp = 0;

            //-----------------
            //Daten vorbereiten
            //-----------------

            //Erst die Gesamtpunkte addieren
            foreach (var item in lstListe)
            {
                //Erst Spieler 1
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.Spieler1ID);
                if (intIndex == -1) // Nicht gefunden, also anlegen
                {
                    ErgebnisKombimeisterschaftPlatzierung objTeilnehmer = new();
                    objTeilnehmer.SpielerID = item.Spieler1ID;
                    objTeilnehmer.SpielerName = item.Spieler1Name;
                    objTeilnehmer.Punkte3bis8 += item.Spieler1Punkte3bis8;
                    objTeilnehmer.Punkte5Kugeln += item.Spieler1Punkte5Kugeln;
                    objTeilnehmer.Gesamtpunkte = item.Spieler1Punkte3bis8 + item.Spieler1Punkte5Kugeln;
                    objTeilnehmer.Platzierung = 0;
                    lstTeilnehmer.Add(objTeilnehmer);
                }
                else //sonst Gesamtpunkte addieren
                {
                    lstTeilnehmer[intIndex].Punkte3bis8 += item.Spieler1Punkte3bis8;
                    lstTeilnehmer[intIndex].Punkte5Kugeln += item.Spieler1Punkte5Kugeln;
                    lstTeilnehmer[intIndex].Gesamtpunkte += item.Spieler1Punkte3bis8 + item.Spieler1Punkte5Kugeln;
                }

                //Dann Spieler 2
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.Spieler2ID);
                if (intIndex == -1) // Nicht gefunden, also anlegen
                {
                    ErgebnisKombimeisterschaftPlatzierung objTeilnehmer = new();
                    objTeilnehmer.SpielerID = item.Spieler2ID;
                    objTeilnehmer.SpielerName = item.Spieler2Name;
                    objTeilnehmer.Punkte3bis8 += item.Spieler2Punkte3bis8;
                    objTeilnehmer.Punkte5Kugeln += item.Spieler2Punkte5Kugeln;
                    objTeilnehmer.Gesamtpunkte = item.Spieler2Punkte3bis8 + item.Spieler2Punkte5Kugeln;
                    objTeilnehmer.Platzierung = 0;
                    lstTeilnehmer.Add(objTeilnehmer);
                }
                else //sonst Gesamtpunkte addieren
                {
                    lstTeilnehmer[intIndex].Punkte3bis8 += item.Spieler2Punkte3bis8;
                    lstTeilnehmer[intIndex].Punkte5Kugeln += item.Spieler2Punkte5Kugeln;
                    lstTeilnehmer[intIndex].Gesamtpunkte += item.Spieler2Punkte3bis8 + item.Spieler2Punkte5Kugeln;
                }
            }

            //Jetzt die Platzierung ermitteln
            var pl = (from lst in lstTeilnehmer
                orderby lst.Gesamtpunkte descending
                select lst).ToList();

            foreach (var item in pl)
            {
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.SpielerID);

                //lstTeilnehmer[intIndex].Platzierung = intPlatzierungTemp;
                if (intPunkteTemp != lstTeilnehmer[intIndex].Gesamtpunkte)
                {
                    lstTeilnehmer[intIndex].Platzierung = ++intPlatzierungTemp;
                    intPunkteTemp = lstTeilnehmer[intIndex].Gesamtpunkte;
                }
                else
                {
                    lstTeilnehmer[intIndex].Platzierung = intPlatzierungTemp;
                }
            }

            //-----------------
            //Druckaufbereitung
            //-----------------

            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "ErgebnisKombimeisterschaft";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Landscape;
            vpe.SetMargins(2, 2, 2, 2);
            vpe.PenSize = 0.01;

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            //vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1, "KOMBI-Meisterschaft");
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1,
                strMeisterschaft);
            vpe.SelectFont("Arial", 14);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1,
                strMeisterschaftstyp);

            //Kopfzeile und Namensspalte
            vpe.SelectFont("Arial", 18);
            x = vpe.nLeftMargin;
            y = vpe.nBottom;
            //strText = "2023";
            strText = await _dbService.GetMeisterschaftsjahrAsync(MeisterschaftsID);
            vpe.WriteBox(x, y, -1.7, -1, strText);
            x = vpe.nRight;
            y = vpe.nTop;
            xZeile = vpe.nLeft;
            yZeile = vpe.nBottom;
            xErg = vpe.nRight;
            yErg = vpe.nBottom;
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                vpe.SelectFont("Arial", 9);
                strText = lstTeilnehmer[i].SpielerName;
                vpe.WriteBox(x, y, -2.4, -0.5, strText);
                xBak = vpe.nRight;
                yBak = vpe.nTop;
                vpe.SelectFont("Arial", 10);
                vpe.WriteBox(vpe.nLeft, vpe.nTop + 0.5, -0.8, -0.5, "3-8");
                vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, "5 K");
                vpe.SelectFont("Arial", 8);
                vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, "Zus.");
                x = xBak;
                y = yBak;
            }

            vpe.SelectFont("Arial", 7);
            strText = "Tot. Hi. Rü.";
            vpe.WriteBox(x, y, -0.8, -1, strText);
            strText = "Total";
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
            strText = "Platz";
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);

            x = xZeile;
            y = yZeile;
            vpe.SelectFont("Arial", 9);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                strText = lstTeilnehmer[i].SpielerName;
                vpe.WriteBox(x, y, -1.7, -1, strText);
                x = vpe.nLeft;
                y = vpe.nBottom;
            }

            //Ergebnisse
            x = xErg;
            y = yErg;
            vpe.SelectFont("Arial", 10);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++) //Zeilen durchgehen
            {
                intSummeHinrunde = 0;
                intSummeRückrunde = 0;
                intSummeHinRückrunde = 0;

                for (Int32 j = 0; j < lstTeilnehmer.Count; j++) //Spalten durchgehen
                {
                    xBak = 0;
                    yBak = 0;
                    if (lstTeilnehmer[i].SpielerID == lstTeilnehmer[j].SpielerID)
                    {
                        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                        //Hinrunde
                        //3 bis 8
                        vpe.SelectFont("Arial", 10);
                        strText = "X";
                        vpe.WriteBox(x, y, -0.8, -0.5, strText);
                        //5 Kugeln
                        strText = "X";
                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                        //Summe
                        strText = "X";
                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                        xBak = vpe.nRight;
                        yBak = vpe.nTop;
                        //Rückrunde
                        //3 bis 8
                        strText = "X";
                        vpe.WriteBox(x, y + 0.5, -0.8, -0.5, strText);
                        //5 Kugeln
                        strText = "X";
                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                        //Summe
                        strText = "X";
                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
                    }
                    else
                    {
                        //Hinrunde
                        vpe.SelectFont("Arial", 10);
                        strText = " ";
                        intIndexSpieler1 = lstListe.FindIndex(f =>
                            f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 0);
                        intIndexSpieler2 = lstListe.FindIndex(f =>
                            f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 0);

                        //3 bis 8
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte3bis8.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte3bis8.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte3bis8.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte3bis8.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(x, y, -0.8, -0.5, strText);
                        strText = " ";

                        //5 Kugeln
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte5Kugeln.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                        strText = " ";

                        //Summe
                        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    intSummeHinrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                        lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                            lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                               lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln).ToString();
                                }
                                else
                                {
                                    intSummeHinrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                        lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                            lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler1].Spieler2Punkte3bis8 +
                                               lstListe[intIndexSpieler1].Spieler2Punkte5Kugeln).ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    intSummeHinrunde += lstListe[intIndexSpieler2].Spieler2Punkte3bis8 +
                                                        lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler2].Spieler2Punkte3bis8 +
                                                            lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler2].Spieler2Punkte3bis8 +
                                               lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln).ToString();
                                }
                                else
                                {
                                    intSummeHinrunde += lstListe[intIndexSpieler2].Spieler1Punkte3bis8 +
                                                        lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler2].Spieler1Punkte3bis8 +
                                                            lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler2].Spieler1Punkte3bis8 +
                                               lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln).ToString();
                                }
                            }
                        }

                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                        xBak = vpe.nRight;
                        yBak = vpe.nTop;
                        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                        //Rückrunde
                        vpe.SelectFont("Arial", 10);
                        strText = " ";
                        intIndexSpieler1 = lstListe.FindIndex(f =>
                            f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 1);
                        intIndexSpieler2 = lstListe.FindIndex(f =>
                            f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 1);

                        //3 bis 8
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte3bis8.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte3bis8.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte3bis8.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte3bis8.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(x, y + 0.5, -0.8, -0.5, strText);
                        strText = " ";

                        //5 Kugeln
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte5Kugeln.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                        strText = " ";

                        //Summe
                        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    intSummeRückrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                         lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                            lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                               lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln).ToString();
                                }
                                else
                                {
                                    intSummeRückrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                         lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler1].Spieler1Punkte3bis8 +
                                                            lstListe[intIndexSpieler1].Spieler1Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler1].Spieler2Punkte3bis8 +
                                               lstListe[intIndexSpieler1].Spieler2Punkte5Kugeln).ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    intSummeRückrunde += lstListe[intIndexSpieler2].Spieler2Punkte3bis8 +
                                                         lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler2].Spieler2Punkte3bis8 +
                                                            lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler2].Spieler2Punkte3bis8 +
                                               lstListe[intIndexSpieler2].Spieler2Punkte5Kugeln).ToString();
                                }
                                else
                                {
                                    intSummeRückrunde += lstListe[intIndexSpieler2].Spieler1Punkte3bis8 +
                                                         lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln;
                                    intSummeHinRückrunde += lstListe[intIndexSpieler2].Spieler1Punkte3bis8 +
                                                            lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln;

                                    strText = (lstListe[intIndexSpieler2].Spieler1Punkte3bis8 +
                                               lstListe[intIndexSpieler2].Spieler1Punkte5Kugeln).ToString();
                                }
                            }
                        }

                        vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                    }

                    x = xBak;
                    y = yBak;
                }

                yBak = vpe.nBottom;

                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.SelectFont("Arial", 8);
                //strText = "a" + i.ToString();
                strText = intSummeHinrunde.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -0.5, strText);
                //strText = "b" + i.ToString();
                strText = intSummeRückrunde.ToString();
                vpe.WriteBox(vpe.nLeft, vpe.nBottom, -0.8, -0.5, strText);
                //strText = "c" + i.ToString();
                strText = intSummeHinRückrunde.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -1, strText);
                //strText = "d" + i.ToString();
                switch (lstTeilnehmer[i].Platzierung)
                {
                    case 1:
                        vpe.SelectFont("Arial", 18);
                        break;
                    case 2:
                        vpe.SelectFont("Arial", 15);
                        break;
                    case 3:
                        vpe.SelectFont("Arial", 12);
                        break;
                    default:
                        vpe.SelectFont("Arial", 8);
                        break;
                }

                strText = lstTeilnehmer[i].Platzierung.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
                vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                x = xErg;
                y = yBak;
            }

            vpe.SelectFont("Arial", 7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            strText = "Tot. Hi. Rü.";
            vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -1.7, -0.5, strText);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                vpe.SelectFont("Arial", 10);
                //Zeilensumme 3 bis 8
                //strText = arrPunkte3bis8[i].ToString();
                strText = lstTeilnehmer[i].Punkte3bis8.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                //Zeilensumme 5 Kugeln
                //strText = arrPunkte5Kugeln[i].ToString();
                strText = lstTeilnehmer[i].Punkte5Kugeln.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                vpe.SelectFont("Arial", 8);
                vpe.Write(vpe.nRight, vpe.nTop, -0.8, -0.5, "");
            }

            // //Trennlinien
            // vpe.PenSize = 0.06;
            //
            // x = vpe.nLeftMargin;
            // y = vpe.nTopMargin + 1 + 1;
            // Double w = 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8;
            // Double h = y;
            // //Waagerecht
            // for (Int32 i = 0; i < lstTeilnehmer.Count + 2; i++)
            // {
            //     vpe.Line(x, y + i * 1, -w, y + i * 1);
            // }
            //
            // y = vpe.nTopMargin + 1 + 1 + 0.5;
            // w = lstTeilnehmer.Count * 2.4;
            // vpe.Line(x + 1.7, y, -w, y);
            //
            // //Senkrecht
            // x = vpe.nLeftMargin;
            // y = vpe.nTopMargin + 1 + 1;
            // w = x;
            // h = lstTeilnehmer.Count + 1 * 1 + 0.5;
            // vpe.Line(x, y, w, -h);
            // vpe.Line(x + 1.7, y, w + 1.7, -h);
            //
            // x = vpe.nLeftMargin + 1.7;
            // h = 7 * 1 - 0.5;
            // for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            // {
            //     vpe.Line(x + (i + 1) * 2.4 - 0.8, y + 0.5, x + (i + 1) * 2.4 - 0.8, -h);
            // }
            //
            // x = vpe.nLeftMargin + 1.7;
            // h = 7 * 1;
            // for (Int32 i = 0; i < lstTeilnehmer.Count + 1; i++)
            // {
            //     vpe.Line(x + i * 2.4, y, x + i * 2.4, -h);
            // }
            //
            // x = vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 0.8;
            // vpe.Line(x, y, x, -h);
            // x = vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 2 * 0.8;
            // vpe.Line(x, y, x, -h);
            // x = vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8;
            // vpe.Line(x, y, x, -h);

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckKombimeisterschaft", ex.Message);
        }
    }

    public async Task DruckErgebnisMeisterschaftAsync(int MeisterschaftsID, string PDFFilename = "")
    {
        try
        {
            List<ErgebnisMeisterschaftKreuztabelle> lstListe =
                await _dbService.GetErgebnisMeisterschaftAsync(MeisterschaftsID);
            List<ErgebnisMeisterschaftPlatzierung> lstTeilnehmer = new();
            string strMeisterschaft = await _dbService.GetMeisterschaftsbezeichnungAsync(MeisterschaftsID);
            string strMeisterschaftstyp =
                await _dbService.GetMeisterschaftstypFromMeisterschaftByIDAsync(MeisterschaftsID);
            Double x;
            Double y;
            Double xZeile;
            Double yZeile;
            Double xBak;
            Double yBak;
            Double xErg;
            Double yErg;
            string strText = string.Empty;
            Int32 intSummeHinrunde = 0;
            Int32 intSummeRückrunde = 0;
            Int32 intSummeHinRückrunde = 0;
            Int32 intIndex = -1;
            Int32 intIndexSpieler1 = -1;
            Int32 intIndexSpieler2 = -1;
            Int32 intPlatzierungTemp = 0;
            Int32 intPunkteTemp = 0;

            //-----------------
            //Daten vorbereiten
            //-----------------

            //Erst die Gesamtpunkte addieren
            foreach (var item in lstListe)
            {
                //Erst Spieler 1
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.Spieler1ID);
                if (intIndex == -1) // Nicht gefunden, also anlegen
                {
                    ErgebnisMeisterschaftPlatzierung objTeilnehmer = new();
                    objTeilnehmer.SpielerID = item.Spieler1ID;
                    objTeilnehmer.SpielerName = item.Spieler1Name;
                    objTeilnehmer.Punkte += item.Spieler1Punkte;
                    objTeilnehmer.Gesamtpunkte = item.Spieler1Punkte;
                    objTeilnehmer.Platzierung = 0;
                    lstTeilnehmer.Add(objTeilnehmer);
                }
                else //sonst Gesamtpunkte addieren
                {
                    lstTeilnehmer[intIndex].Punkte += item.Spieler1Punkte;
                    lstTeilnehmer[intIndex].Gesamtpunkte += item.Spieler1Punkte;
                }

                //Dann Spieler 2
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.Spieler2ID);
                if (intIndex == -1) // Nicht gefunden, also anlegen
                {
                    ErgebnisMeisterschaftPlatzierung objTeilnehmer = new();
                    objTeilnehmer.SpielerID = item.Spieler2ID;
                    objTeilnehmer.SpielerName = item.Spieler2Name;
                    objTeilnehmer.Punkte += item.Spieler1Punkte;
                    objTeilnehmer.Gesamtpunkte = item.Spieler1Punkte;
                    objTeilnehmer.Platzierung = 0;
                    lstTeilnehmer.Add(objTeilnehmer);
                }
                else //sonst Gesamtpunkte addieren
                {
                    lstTeilnehmer[intIndex].Punkte += item.Spieler1Punkte;
                    lstTeilnehmer[intIndex].Gesamtpunkte += item.Spieler1Punkte;
                }
            }

            //Jetzt die Platzierung ermitteln
            var pl = (from lst in lstTeilnehmer
                orderby lst.Gesamtpunkte descending
                select lst).ToList();

            foreach (var item in pl)
            {
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.SpielerID);

                //lstTeilnehmer[intIndex].Platzierung = intPlatzierungTemp;
                if (intPunkteTemp != lstTeilnehmer[intIndex].Gesamtpunkte)
                {
                    lstTeilnehmer[intIndex].Platzierung = ++intPlatzierungTemp;
                    intPunkteTemp = lstTeilnehmer[intIndex].Gesamtpunkte;
                }
                else
                {
                    lstTeilnehmer[intIndex].Platzierung = intPlatzierungTemp;
                }
            }

            //-----------------
            //Druckaufbereitung
            //-----------------

            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "ErgebnisMeisterschaft";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Landscape;
            vpe.SetMargins(2, 2, 2, 2);
            vpe.PenSize = 0.01;

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            //vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1, "KOMBI-Meisterschaft");
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1,
                strMeisterschaft);
            vpe.SelectFont("Arial", 14);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1,
                strMeisterschaftstyp);

            //Kopfzeile und Namensspalte
            vpe.SelectFont("Arial", 18);
            x = vpe.nLeftMargin;
            y = vpe.nBottom;
            //strText = "2023";
            strText = await _dbService.GetMeisterschaftsjahrAsync(MeisterschaftsID);
            vpe.WriteBox(x, y, -1.7, -1, strText);
            x = vpe.nRight;
            y = vpe.nTop;
            xZeile = vpe.nLeft;
            yZeile = vpe.nBottom;
            xErg = vpe.nRight;
            yErg = vpe.nBottom;
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                vpe.SelectFont("Arial", 9);
                strText = lstTeilnehmer[i].SpielerName;
                vpe.WriteBox(x, y, -2.4, -0.5, strText);
                xBak = vpe.nRight;
                yBak = vpe.nTop;
                vpe.SelectFont("Arial", 10);
                vpe.WriteBox(vpe.nLeft, vpe.nTop + 0.5, -2.4, -0.5, "Punkte");
                x = xBak;
                y = yBak;
            }

            vpe.SelectFont("Arial", 7);
            strText = "Tot. Hi. Rü.";
            vpe.WriteBox(x, y, -0.8, -1, strText);
            strText = "Total";
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
            strText = "Platz";
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);

            x = xZeile;
            y = yZeile;
            vpe.SelectFont("Arial", 9);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                strText = lstTeilnehmer[i].SpielerName;
                vpe.WriteBox(x, y, -1.7, -1, strText);
                x = vpe.nLeft;
                y = vpe.nBottom;
            }

            //Ergebnisse
            x = xErg;
            y = yErg;
            vpe.SelectFont("Arial", 10);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++) //Zeilen durchgehen
            {
                intSummeHinrunde = 0;
                intSummeRückrunde = 0;
                intSummeHinRückrunde = 0;

                for (Int32 j = 0; j < lstTeilnehmer.Count; j++) //Spalten durchgehen
                {
                    xBak = 0;
                    yBak = 0;
                    if (lstTeilnehmer[i].SpielerID == lstTeilnehmer[j].SpielerID)
                    {
                        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                        //Hinrunde
                        //Punkte
                        vpe.SelectFont("Arial", 10);
                        strText = "XXX";
                        vpe.WriteBox(x, y, -2.4, -0.5, strText);

                        xBak = vpe.nRight;
                        yBak = vpe.nTop;

                        //Rückrunde
                        //Punkte
                        strText = "XXX";
                        vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
                    }
                    else
                    {
                        //Hinrunde
                        vpe.SelectFont("Arial", 10);
                        strText = " ";
                        intIndexSpieler1 = lstListe.FindIndex(f =>
                            f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 0);
                        intIndexSpieler2 = lstListe.FindIndex(f =>
                            f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 0);

                        //Punkte
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(x, y, -2.4, -0.5, strText);
                        strText = " ";

                        xBak = vpe.nRight;
                        yBak = vpe.nTop;
                        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                        //Rückrunde
                        vpe.SelectFont("Arial", 10);
                        strText = " ";
                        intIndexSpieler1 = lstListe.FindIndex(f =>
                            f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 1);
                        intIndexSpieler2 = lstListe.FindIndex(f =>
                            f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 1);

                        //Punkte
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                    }

                    x = xBak;
                    y = yBak;
                }

                yBak = vpe.nBottom;

                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.SelectFont("Arial", 8);
                //strText = "a" + i.ToString();
                strText = intSummeHinrunde.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -0.5, strText);
                //strText = "b" + i.ToString();
                strText = intSummeRückrunde.ToString();
                vpe.WriteBox(vpe.nLeft, vpe.nBottom, -0.8, -0.5, strText);
                //strText = "c" + i.ToString();
                strText = intSummeHinRückrunde.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -1, strText);
                //strText = "d" + i.ToString();
                switch (lstTeilnehmer[i].Platzierung)
                {
                    case 1:
                        vpe.SelectFont("Arial", 18);
                        break;
                    case 2:
                        vpe.SelectFont("Arial", 15);
                        break;
                    case 3:
                        vpe.SelectFont("Arial", 12);
                        break;
                    default:
                        vpe.SelectFont("Arial", 8);
                        break;
                }

                strText = lstTeilnehmer[i].Platzierung.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
                vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                x = xErg;
                y = yBak;
            }

            vpe.SelectFont("Arial", 7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            strText = "Tot. Hi. Rü.";
            vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -1.7, -0.5, strText);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                vpe.SelectFont("Arial", 10);
                //Zeilensumme Punkte
                //strText = arrPunkte3bis8[i].ToString();
                strText = lstTeilnehmer[i].Punkte.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop, -2.4, -0.5, strText);
                ////Zeilensumme 5 Kugeln
                ////strText = arrPunkte5Kugeln[i].ToString();
                //strText = lstTeilnehmer[i].Punkte5Kugeln.ToString();
                //vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                //vpe.SelectFont("Arial", 8);
                //vpe.Write(vpe.nRight, vpe.nTop, -0.8, -0.5, "");
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckErgebnisMeisterschaft", ex.Message);
        }
    }

    public async Task DruckErgebnisKurztunierAsync(int MeisterschaftsID, string PDFFilename = "")
    {
        try
        {
            List<ErgebnisKurztunierKreuztabelle> lstListe =
                await _dbService.GetErgebnisKurztunierAsync(MeisterschaftsID);
            List<ErgebnisKurztunierPlatzierung> lstTeilnehmer = new();
            string strMeisterschaft = await _dbService.GetMeisterschaftsbezeichnungAsync(MeisterschaftsID);
            string strMeisterschaftstyp =
                await _dbService.GetMeisterschaftstypFromMeisterschaftByIDAsync(MeisterschaftsID);
            Double x;
            Double y;
            Double xZeile;
            Double yZeile;
            Double xBak;
            Double yBak;
            Double xErg;
            Double yErg;
            string strText = string.Empty;
            Int32 intSummeHinrunde = 0;
            Int32 intSummeRückrunde = 0;
            Int32 intSummeHinRückrunde = 0;
            Int32 intIndex = -1;
            Int32 intIndexSpieler1 = -1;
            Int32 intIndexSpieler2 = -1;
            Int32 intPlatzierungTemp = 0;
            Int32 intPunkteTemp = 0;

            //-----------------
            //Daten vorbereiten
            //-----------------

            //Erst die Gesamtpunkte addieren
            foreach (var item in lstListe)
            {
                //Erst Spieler 1
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.Spieler1ID);
                if (intIndex == -1) // Nicht gefunden, also anlegen
                {
                    ErgebnisKurztunierPlatzierung objTeilnehmer = new();
                    objTeilnehmer.SpielerID = item.Spieler1ID;
                    objTeilnehmer.SpielerName = item.Spieler1Name;
                    objTeilnehmer.Punkte += item.Spieler1Punkte;
                    objTeilnehmer.Gesamtpunkte = item.Spieler1Punkte;
                    objTeilnehmer.Platzierung = 0;
                    lstTeilnehmer.Add(objTeilnehmer);
                }
                else //sonst Gesamtpunkte addieren
                {
                    lstTeilnehmer[intIndex].Punkte += item.Spieler1Punkte;
                    lstTeilnehmer[intIndex].Gesamtpunkte += item.Spieler1Punkte;
                }

                //Dann Spieler 2
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.Spieler2ID);
                if (intIndex == -1) // Nicht gefunden, also anlegen
                {
                    ErgebnisKurztunierPlatzierung objTeilnehmer = new();
                    objTeilnehmer.SpielerID = item.Spieler2ID;
                    objTeilnehmer.SpielerName = item.Spieler2Name;
                    objTeilnehmer.Punkte += item.Spieler1Punkte;
                    objTeilnehmer.Gesamtpunkte = item.Spieler1Punkte;
                    objTeilnehmer.Platzierung = 0;
                    lstTeilnehmer.Add(objTeilnehmer);
                }
                else //sonst Gesamtpunkte addieren
                {
                    lstTeilnehmer[intIndex].Punkte += item.Spieler1Punkte;
                    lstTeilnehmer[intIndex].Gesamtpunkte += item.Spieler1Punkte;
                }
            }

            //Jetzt die Platzierung ermitteln
            var pl = (from lst in lstTeilnehmer
                orderby lst.Gesamtpunkte descending
                select lst).ToList();

            foreach (var item in pl)
            {
                intIndex = lstTeilnehmer.FindIndex(f => f.SpielerID == item.SpielerID);

                //lstTeilnehmer[intIndex].Platzierung = intPlatzierungTemp;
                if (intPunkteTemp != lstTeilnehmer[intIndex].Gesamtpunkte)
                {
                    lstTeilnehmer[intIndex].Platzierung = ++intPlatzierungTemp;
                    intPunkteTemp = lstTeilnehmer[intIndex].Gesamtpunkte;
                }
                else
                {
                    lstTeilnehmer[intIndex].Platzierung = intPlatzierungTemp;
                }
            }

            //-----------------
            //Druckaufbereitung
            //-----------------

            //Anfang
            VpeToPdfSharp vpe = new();
            vpe.FileName = "ErgebnisKurztunier";
            vpe.OpenDoc();
            vpe.OpenProgressBar();
            vpe.PageOrientation = PageOrientation.Landscape;
            vpe.SetMargins(2, 2, 2, 2);
            vpe.PenSize = 0.01;

            //Überschrift
            vpe.SelectFont("Arial", 18);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            //vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1, "KOMBI-Meisterschaft");
            vpe.Write(vpe.nLeftMargin, vpe.nTopMargin, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1,
                strMeisterschaft);
            vpe.SelectFont("Arial", 14);
            vpe.Write(vpe.nLeftMargin, vpe.nBottom, vpe.nLeftMargin + 1.7 + lstTeilnehmer.Count * 2.4 + 3 * 0.8, -1,
                strMeisterschaftstyp);

            //Kopfzeile und Namensspalte
            vpe.SelectFont("Arial", 18);
            x = vpe.nLeftMargin;
            y = vpe.nBottom;
            //strText = "2023";
            strText = await _dbService.GetMeisterschaftsjahrAsync(MeisterschaftsID);
            vpe.WriteBox(x, y, -1.7, -1, strText);
            x = vpe.nRight;
            y = vpe.nTop;
            xZeile = vpe.nLeft;
            yZeile = vpe.nBottom;
            xErg = vpe.nRight;
            yErg = vpe.nBottom;
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                vpe.SelectFont("Arial", 9);
                strText = lstTeilnehmer[i].SpielerName;
                vpe.WriteBox(x, y, -2.4, -0.5, strText);
                xBak = vpe.nRight;
                yBak = vpe.nTop;
                vpe.SelectFont("Arial", 10);
                vpe.WriteBox(vpe.nLeft, vpe.nTop + 0.5, -2.4, -0.5, "Punkte");
                x = xBak;
                y = yBak;
            }

            vpe.SelectFont("Arial", 7);
            strText = "Tot. Hi. Rü.";
            vpe.WriteBox(x, y, -0.8, -1, strText);
            strText = "Total";
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
            strText = "Platz";
            vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);

            x = xZeile;
            y = yZeile;
            vpe.SelectFont("Arial", 9);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                strText = lstTeilnehmer[i].SpielerName;
                vpe.WriteBox(x, y, -1.7, -1, strText);
                x = vpe.nLeft;
                y = vpe.nBottom;
            }

            //Ergebnisse
            x = xErg;
            y = yErg;
            vpe.SelectFont("Arial", 10);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++) //Zeilen durchgehen
            {
                intSummeHinrunde = 0;
                intSummeRückrunde = 0;
                intSummeHinRückrunde = 0;

                for (Int32 j = 0; j < lstTeilnehmer.Count; j++) //Spalten durchgehen
                {
                    xBak = 0;
                    yBak = 0;
                    if (lstTeilnehmer[i].SpielerID == lstTeilnehmer[j].SpielerID)
                    {
                        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                        //Hinrunde
                        //Punkte
                        vpe.SelectFont("Arial", 10);
                        strText = "XXX";
                        vpe.WriteBox(x, y, -2.4, -0.5, strText);

                        xBak = vpe.nRight;
                        yBak = vpe.nTop;

                        //Rückrunde
                        //Punkte
                        strText = "XXX";
                        vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
                    }
                    else
                    {
                        //Hinrunde
                        vpe.SelectFont("Arial", 10);
                        strText = " ";
                        intIndexSpieler1 = lstListe.FindIndex(f =>
                            f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 0);
                        intIndexSpieler2 = lstListe.FindIndex(f =>
                            f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 0);

                        //Punkte
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(x, y, -2.4, -0.5, strText);
                        strText = " ";

                        xBak = vpe.nRight;
                        yBak = vpe.nTop;
                        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                        //Rückrunde
                        vpe.SelectFont("Arial", 10);
                        strText = " ";
                        intIndexSpieler1 = lstListe.FindIndex(f =>
                            f.Spieler1ID == lstTeilnehmer[i].SpielerID && f.Spieler2ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 1);
                        intIndexSpieler2 = lstListe.FindIndex(f =>
                            f.Spieler2ID == lstTeilnehmer[i].SpielerID && f.Spieler1ID == lstTeilnehmer[j].SpielerID &&
                            f.HinRueckrunde == 1);

                        //Punkte
                        if (intIndexSpieler1 != -1 || intIndexSpieler2 != -1)
                        {
                            if (intIndexSpieler1 != -1)
                            {
                                if (lstListe[intIndexSpieler1].Spieler1ID == lstTeilnehmer[i].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler1Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler1].Spieler2Punkte.ToString();
                                }
                            }

                            if (intIndexSpieler2 != -1)
                            {
                                if (lstListe[intIndexSpieler2].Spieler1ID == lstTeilnehmer[j].SpielerID)
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler2Punkte.ToString();
                                }
                                else
                                {
                                    strText = lstListe[intIndexSpieler2].Spieler1Punkte.ToString();
                                }
                            }
                        }

                        vpe.WriteBox(x, y + 0.5, -2.4, -0.5, strText);
                    }

                    x = xBak;
                    y = yBak;
                }

                yBak = vpe.nBottom;

                vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
                vpe.SelectFont("Arial", 8);
                //strText = "a" + i.ToString();
                strText = intSummeHinrunde.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -0.5, strText);
                //strText = "b" + i.ToString();
                strText = intSummeRückrunde.ToString();
                vpe.WriteBox(vpe.nLeft, vpe.nBottom, -0.8, -0.5, strText);
                //strText = "c" + i.ToString();
                strText = intSummeHinRückrunde.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop - 0.5, -0.8, -1, strText);
                //strText = "d" + i.ToString();
                switch (lstTeilnehmer[i].Platzierung)
                {
                    case 1:
                        vpe.SelectFont("Arial", 18);
                        break;
                    case 2:
                        vpe.SelectFont("Arial", 15);
                        break;
                    case 3:
                        vpe.SelectFont("Arial", 12);
                        break;
                    default:
                        vpe.SelectFont("Arial", 8);
                        break;
                }

                strText = lstTeilnehmer[i].Platzierung.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -1, strText);
                vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);

                x = xErg;
                y = yBak;
            }

            vpe.SelectFont("Arial", 7);
            vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            strText = "Tot. Hi. Rü.";
            vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -1.7, -0.5, strText);
            for (Int32 i = 0; i < lstTeilnehmer.Count; i++)
            {
                vpe.SelectFont("Arial", 10);
                //Zeilensumme Punkte
                //strText = arrPunkte3bis8[i].ToString();
                strText = lstTeilnehmer[i].Punkte.ToString();
                vpe.WriteBox(vpe.nRight, vpe.nTop, -2.4, -0.5, strText);
                ////Zeilensumme 5 Kugeln
                ////strText = arrPunkte5Kugeln[i].ToString();
                //strText = lstTeilnehmer[i].Punkte5Kugeln.ToString();
                //vpe.WriteBox(vpe.nRight, vpe.nTop, -0.8, -0.5, strText);
                //vpe.SelectFont("Arial", 8);
                //vpe.Write(vpe.nRight, vpe.nTop, -0.8, -0.5, "");
            }

            //Ende
            vpe.CloseProgressBar();

            if (PDFFilename == string.Empty)
                vpe.Preview();
            else
                vpe.PDFExport(PDFFilename);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("PrintService", "DruckErgebnisKurztunier", ex.Message);
        }
    }

    #endregion

    #region Listen

    public async Task DruckMitgliederlisteAsync(bool bAktiv = true, string PDFFilename = "")
    {
        double dblHeightHeader = 0.8;
        List<MitgliedDetails> lstListe = await _dbService.GetMitlgiederlisteDruckAsync(bAktiv);
        string strHeader2;

        VpeToPdfSharp vpe = new();
        vpe.FileName = "Mitgliederliste" + (bAktiv ? "_Aktiv" : "");
        vpe.OpenDoc();
        vpe.OpenProgressBar();
        vpe.PageOrientation = PageOrientation.Portrait;
        vpe.SetMargins(2, 2, 2, 2);

        vpe.SelectFont("Arial", 14);
        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);

        // if (dblHeightHeader == 0)
        // {
        //     // determine height of headline only once for better performance:
        //     vpe.RenderPrintBox(0, 0, "x");
        //     dblHeightHeader = -vpe.nRenderHeight;
        // }

        vpe.WriteBox(vpe.nLeftMargin, vpe.nTopMargin, -17, dblHeightHeader, "KEPA 1958");
        if (bAktiv)
        {
            strHeader2 = "Mitgliederliste (aktive Kegler)";
        }
        else
        {
            strHeader2 = "Mitgliederliste";
        }

        vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -13, dblHeightHeader, strHeader2);

        vpe.SelectFont("Arial", 10);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        vpe.WriteBox(vpe.nRight, vpe.nTop, -2, dblHeightHeader, "Stand:");
        vpe.WriteBox(vpe.nRight, vpe.nTop, -2, dblHeightHeader, DateTime.Now.ToShortDateString());

        vpe.SelectFont("Arial", 8);
        vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
        vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -2.5, -0.6, "Name");
        vpe.WriteBox(vpe.nRight, vpe.nTop, -2.5, -0.6, "Vorname");
        vpe.WriteBox(vpe.nRight, vpe.nTop, -2, -0.6, "Geb.");
        vpe.WriteBox(vpe.nRight, vpe.nTop, -3, -0.6, "Anschrift");
        vpe.WriteBox(vpe.nRight, vpe.nTop, -3, -0.6, "E-Mail");
        vpe.WriteBox(vpe.nRight, vpe.nTop, -2, -0.6, "Tel-/Handy");
        vpe.WriteBox(vpe.nRight, vpe.nTop, -2, -0.6, "Ein-/Austritt");

        vpe.SelectFont("Arial", 6);
        vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
        foreach (var item in lstListe)
        {
            // if (vpe.nBottomMargin - vpe.nBottom < 2)
            // {
            //     vpe.PageBreak();
            //
            //     vpe.SelectFont("Arial", 8);
            //     vpe.SetFontAttr(TextAlignment.Center, true, false, false, false);
            //     vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -2.5, -0.6, "Name");
            //     vpe.WriteBox(vpe.nRight, vpe.nTop, -2.5, vpe.nBottom, "Vorname");
            //     vpe.WriteBox(vpe.nRight, vpe.nTop, -2, vpe.nBottom, "Geb.");
            //     vpe.WriteBox(vpe.nRight, vpe.nTop, -3, vpe.nBottom, "Anschrift");
            //     vpe.WriteBox(vpe.nRight, vpe.nTop, -3, vpe.nBottom, "E-Mail");
            //     vpe.WriteBox(vpe.nRight, vpe.nTop, -2, vpe.nBottom, "Tel-/Handy");
            //     vpe.WriteBox(vpe.nRight, vpe.nTop, -2, vpe.nBottom, "Ein-/Austritt");
            //
            //     vpe.SelectFont("Arial", 6);
            //     vpe.SetFontAttr(TextAlignment.Center, false, false, false, false);
            // }

            vpe.WriteBox(vpe.nLeftMargin, vpe.nBottom, -2.5, -0.6, item.Nachname);
            vpe.WriteBox(vpe.nRight, vpe.nTop, -2.5, -0.6, item.Vorname);
            vpe.WriteBox(vpe.nRight, vpe.nTop, -2, -0.6, item.Geburtsdatum.ToShortDateString());
            vpe.WriteBox(vpe.nRight, vpe.nTop, -3, -0.3, item.Straße);
            string strPLZOrt = item.PLZ + " " + item.Ort;
            vpe.WriteBox(vpe.nLeft, vpe.nBottom, -3, -0.3, strPLZOrt);
            vpe.WriteBox(vpe.nRight, vpe.nBottom - 0.6, -3, -0.6, item.EMail ?? string.Empty);
            vpe.WriteBox(vpe.nRight, vpe.nTop, -2, -0.3, item.TelefonPrivat ?? string.Empty);
            vpe.WriteBox(vpe.nLeft, vpe.nBottom, -2, -0.3, item.TelefonMobil ?? string.Empty);

            vpe.WriteBox(vpe.nRight, vpe.nBottom - 0.6, -2, -0.3, item.MitgliedSeit.Value.ToShortDateString());
            if (item.AusgeschiedenAm.HasValue)
                vpe.WriteBox(vpe.nLeft, vpe.nBottom, -2, -0.3, item.AusgeschiedenAm.Value.ToShortDateString());
            else
                vpe.WriteBox(vpe.nLeft, vpe.nBottom, -2, -0.3, string.Empty);
        }

        //Ende
        vpe.CloseProgressBar();

        if (PDFFilename == string.Empty)
            vpe.Preview();
        else
            vpe.PDFExport(PDFFilename);
    }

    #endregion
}