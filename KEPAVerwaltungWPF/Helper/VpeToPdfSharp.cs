using System;
using System.Diagnostics;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing.Layout;
using PdfSharp.Quality;

namespace KEPAVerwaltungWPF.Helper;

//public enum PageOrientation { Portrait, Landscape }
public enum TextAlignment
{
    Left,
    Center,
    Right
}

public enum PenStyle
{
    Solid,
    Dash,
    DashDot,
    DashDotDot
}

public class VpeToPdfSharp
{
    public PdfDocument Document;
    public PdfPage Page;
    public XGraphics Gfx;
    public double cm = 28.35; // 1 cm in Punkten

    public string FileName;
    
    // Seitenränder (in Punkten, aber wir speichern sie in cm umgerechnet)
    public double nLeftMargin, nTopMargin, nRightMargin, nBottomMargin;

    // Platzhalter für die zuletzt gezeichneten Objektkoordinaten (in cm)
    public double nLeft, nTop, nRight, nBottom;

    // nFree: in VPE berechnet er anhand des Inhalts – hier als Platzhalter (0 = nicht gesetzt)
    public double nFree
    {
        get { return 0; }
    }

    // Aktueller Pen-Stil
    public PenStyle PenStyle;

    //Aktuelle LinienDicke
    public double PenSize = 1;
    
    // Aktuelle Schrift und Formatierung
    public XFont CurrentFont;
    public XStringFormat StringFormat;

    public PageOrientation PageOrientation
    {
        get
        {
            return Page.Orientation == PageOrientation.Portrait
                ? PageOrientation.Portrait
                : PageOrientation.Landscape;
        }
        set
        {
            Page.Orientation = value == PageOrientation.Portrait
                ? PageOrientation.Portrait
                : PageOrientation.Landscape;
        }
    }

    // Hilfsvariablen für Store/Restore
    private double backup_nLeft, backup_nTop, backup_nRight, backup_nBottom;

    public VpeToPdfSharp()
    {
        StringFormat = new XStringFormat();
    }

    public void OpenDoc()
    {
        Document = new PdfDocument();
        Page = Document.AddPage();
        Page.Orientation = PageOrientation;
            
        Gfx = XGraphics.FromPdfPage(Page);
    }

    public void SetMargins(double left, double top, double right, double bottom)
    {
        nLeftMargin = left;
        nTopMargin = top;
        nRightMargin = (Page.Width / cm) - right;
        nBottomMargin = (Page.Height / cm) - bottom;
    }
    
    public void OpenProgressBar()
    {
        // Keine Umsetzung, nur Platzhalter
    }

    public void CloseProgressBar()
    {
        // Keine Umsetzung, nur Platzhalter
    }

    public void SelectFont(string fontName, double size)
    {
        // Hier wird die Schriftgröße (in Punkt) direkt übernommen.
        CurrentFont = new XFont(fontName, size, XFontStyleEx.Regular);
    }

    public void SetFontAttr(TextAlignment alignment, bool bold, bool italic, bool underline, bool strikeout)
    {
        XFontStyleEx style = XFontStyleEx.Regular;
        if (bold)
            style |= XFontStyleEx.Bold;
        if (italic)
            style |= XFontStyleEx.Italic;
        if (underline)
            style |= XFontStyleEx.Underline;
        if (strikeout)
            style |= XFontStyleEx.Strikeout;
        // Erzeuge erneut die Schrift mit dem neuen Stil
        CurrentFont = new XFont(CurrentFont.FontFamily.Name, CurrentFont.Size, style);

        // Setze die Textausrichtung
        switch (alignment)
        {
            case TextAlignment.Left:
                StringFormat.Alignment = XStringAlignment.Near;
                break;
            case TextAlignment.Center:
                StringFormat.Alignment = XStringAlignment.Center;
                break;
            case TextAlignment.Right:
                StringFormat.Alignment = XStringAlignment.Far;
                break;
        }
    }

    /// <summary>
    /// Erzeugt einen Seitenumbruch
    /// </summary>
    public void PageBreak()
    {
        Page = Document.AddPage();
        Gfx = XGraphics.FromPdfPage(Page);
    }
    
    /// <summary>
    /// Zeichnet einen Text in einem Rechteck. Negative Werte für w bzw. h bedeuten, dass der Absolutwert (in cm) als Breite/Höhe verwendet wird.
    /// </summary>
    public void Write(double x, double y, double w, double h, string text)
    {
        double X = x * cm;
        double Y = y * cm;
        double width = (w < 0) ? Math.Abs(w) * cm : w * cm;
        double height = (h < 0) ? Math.Abs(h) * cm : h * cm;

        XRect rect = new XRect(X, Y, width, height);
        XTextFormatter tf = new XTextFormatter(Gfx);
        //Gfx.DrawString(text, CurrentFont, XBrushes.Black, rect, StringFormat);
        tf.DrawString(text, CurrentFont, XBrushes.Black, rect, XStringFormats.TopLeft);
        
        // Aktualisiere Platzhalter (in cm)
        nLeft = x;
        nTop = y;
        nRight = (w < 0) ? x + Math.Abs(w) : x + w;
        nBottom = (h < 0) ? y + Math.Abs(h) : y + h;
    }

    /// <summary>
    /// Zeichnet einen Text in einem Rechteck. Negative Werte für w bzw. h bedeuten, dass der Absolutwert (in cm) als Breite/Höhe verwendet wird.
    /// </summary>
    public void WriteBox(double x, double y, double w, double h, string text)
    {
        // Zeichne zuerst das Rechteck
        StorePos();
        PenStyle psbackup = PenStyle;
        PenStyle = PenStyle.Solid;
        Box(x, y, w, h);
        PenStyle = psbackup;
        RestorePos();
        
        // Dann den Text
        double X = x * cm;
        double Y = y * cm;
        double width = (w < 0) ? Math.Abs(w) * cm : w * cm;
        double height = (h < 0) ? Math.Abs(h) * cm : h * cm;

        XTextFormatter tf = new XTextFormatter(Gfx);
        XRect rect = new XRect(X, Y, width, height);
        //Gfx.DrawString(text, CurrentFont, XBrushes.Black, rect, StringFormat);
        tf.DrawString(text, CurrentFont, XBrushes.Black, rect, XStringFormats.TopLeft);
        
        // Aktualisiere Platzhalter (in cm)
        nLeft = x;
        nTop = y;
        nRight = (w < 0) ? x + Math.Abs(w) : x + w;
        nBottom = (h < 0) ? y + Math.Abs(h) : y + h;
    }
    
    /// <summary>
    /// Zeichnet ein Rechteck (Box). Negative Werte für w bzw. h werden als Breite/Höhe in cm interpretiert.
    /// </summary>
    public void Box(double x, double y, double w, double h)
    {
        double X = x * cm;
        double Y = y * cm;
        double width = (w < 0) ? Math.Abs(w) * cm : w * cm;
        double height = (h < 0) ? Math.Abs(h) * cm : h * cm;

        XPen pen = new XPen(XColors.Black, PenSize);
        switch (PenStyle)
        {
            case PenStyle.DashDot:
                pen.DashStyle = XDashStyle.DashDot;
                break;
            case PenStyle.DashDotDot:
                pen.DashStyle = XDashStyle.DashDotDot;
                break;
            case PenStyle.Dash:
                pen.DashStyle = XDashStyle.Dash;
                break;
            case PenStyle.Solid:
                pen.DashStyle = XDashStyle.Solid;
                break;
        }
        Gfx.DrawRectangle(pen, X, Y, width, height);

        // Aktualisiere Platzhalter (in cm)
        nLeft = x;
        nTop = y;
        nRight = (w < 0) ? x + Math.Abs(w) : x + w;
        nBottom = (h < 0) ? y + Math.Abs(h) : y + h;
    }

    public void Line(double x1, double y1, double x2, double y2)
    {
        double X1 = x1 * cm;
        double Y1 = y1 * cm;
        double X2 = x2 * cm;
        double Y2 = y2 * cm;

        XPen pen = new XPen(XColors.Black, PenSize);
        switch (PenStyle)
        {
            case PenStyle.DashDot:
                pen.DashStyle = XDashStyle.DashDot;
                break;
            case PenStyle.DashDotDot:
                pen.DashStyle = XDashStyle.DashDotDot;
                break;
            case PenStyle.Dash:
                pen.DashStyle = XDashStyle.Dash;
                break;
            case PenStyle.Solid:
                pen.DashStyle = XDashStyle.Solid;
                break;
        }

        Gfx.DrawLine(pen, X1, Y1, X2, Y2);

        // Aktualisiere Platzhalter (in cm)
        nLeft = x1;
        nTop = y1;
        nRight = x2;
        nBottom = y2;
    }

    public void StorePos()
    {
        backup_nLeft = nLeft;
        backup_nTop = nTop;
        backup_nRight = nRight;
        backup_nBottom = nBottom;
    }

    public void RestorePos()
    {
        nLeft = backup_nLeft;
        nTop = backup_nTop;
        nRight = backup_nRight;
        nBottom = backup_nBottom;
    }

    public void Preview()
    {
        // // Speichern des Dokuments in eine Datei und diese anschließend öffnen
        // string filename = "Output.pdf";
        // Document.Save(filename);
        // Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
        
        var filename = PdfFileUtility.GetTempPdfFullFileName(FileName);
        Document.Save(filename);
        PdfFileUtility.ShowDocument(filename);
    }

    public void PDFExport(string PDFFilename)
    {
        Document.Save(PDFFilename);
    }
}