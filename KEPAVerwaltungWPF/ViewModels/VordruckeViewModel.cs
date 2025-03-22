using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Helper;
using KEPAVerwaltungWPF.Services;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp;
using PdfSharp.Quality;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PageOrientation = PdfSharp.PageOrientation;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class VordruckeViewModel : BaseViewModel
{
    public delegate string OnShowFileDialogHandlerType();

    public OnShowFileDialogHandlerType? DelShowFileDialog { get; set; }

    private readonly DBService _dbService;
    private readonly PrintService _printService;

    public VordruckeViewModel(DBService dbService, PrintService printService)
    {
        _dbService = dbService;
        _printService = printService;
        Titel = "Listen und Vordrucke";
    }


    // private void DruckVorlage6TageRennen(bool bPDF = false)
    // {
    //     // Dokument und Seite erstellen
    //     PdfDocument document = new PdfDocument();
    //     PdfPage page = document.AddPage();
    //     page.Size = PageSize.A4;
    //     page.Orientation = PdfSharp.PageOrientation.Landscape;
    //     XGraphics gfx = XGraphics.FromPdfPage(page);
    //
    //     // Schriftarten
    //     XFont fontTitle = new XFont("Arial", 14, XFontStyleEx.Regular);
    //     XFont fontHeader = new XFont("Arial", 12, XFontStyleEx.Regular);
    //     XFont fontSmall = new XFont("Arial", 7, XFontStyleEx.Regular);
    //
    //     double oneCM = 28.35;
    //
    //     double leftMargin = oneCM * 2; // 2 cm
    //     double topMargin = oneCM * 2; // 2 cm
    //     double rightMargin = page.Width - leftMargin;
    //     double bottomMargin = page.Height - topMargin;
    //
    //     double currentTop = topMargin;
    //
    //     // Titel
    //     gfx.DrawString("6 Tage Rennen", fontTitle, XBrushes.Black,
    //         new XRect(leftMargin, currentTop, rightMargin - leftMargin, 20), XStringFormats.TopCenter);
    //     currentTop += oneCM; // 1 cm nach unten
    //
    //     // Erste Kopfzeile
    //     double colWidth = oneCM * 4.5;
    //     double colPos = leftMargin;
    //     gfx.DrawString("Mannschaft", fontHeader, XBrushes.Black, new XRect(colPos, currentTop, colWidth, 10),
    //         XStringFormats.TopLeft);
    //     colPos += colWidth;
    //
    //     for (int i = 1; i <= 6; i++)
    //     {
    //         gfx.DrawString($"{i}. Nacht", fontHeader, XBrushes.Black, new XRect(colPos, currentTop, oneCM * 3, 10),
    //             XStringFormats.TopCenter);
    //         gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 0.5);
    //         colPos += oneCM * 3;
    //     }
    //
    //     gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 0.5);
    //
    //     gfx.DrawString("Platz", fontHeader, XBrushes.Black, new XRect(colPos, currentTop, oneCM * 3, 10),
    //         XStringFormats.TopCenter);
    //     //gfx.DrawLine(XPens.Black, leftMargin, currentTop + oneCM * 0.5, rightMargin - oneCM, currentTop + oneCM * 0.5);
    //     gfx.DrawLine(XPens.Black, leftMargin, currentTop + oneCM * 0.5, colPos, currentTop + oneCM * 0.5);
    //     currentTop += oneCM / 2;
    //
    //     // Zweite Kopfzeile
    //     colPos = leftMargin;
    //     gfx.DrawString("Nr.", fontHeader, XBrushes.Black, new XRect(colPos, currentTop, oneCM, 10),
    //         XStringFormats.TopLeft);
    //     colPos += oneCM;
    //     gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 0.5);
    //
    //     gfx.DrawString("Namen", fontHeader, XBrushes.Black, new XRect(colPos, currentTop, oneCM * 3.5, 10),
    //         XStringFormats.TopCenter);
    //     colPos += oneCM * 3.5;
    //     gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 0.5);
    //
    //     for (int i = 1; i <= 6; i++)
    //     {
    //         gfx.DrawString("Holz", fontHeader, XBrushes.Black, new XRect(colPos, currentTop, oneCM * 2, 10),
    //             XStringFormats.TopCenter);
    //         colPos += oneCM * 2;
    //         gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 0.5);
    //         gfx.DrawString("Pkt", fontSmall, XBrushes.Black, new XRect(colPos, currentTop, oneCM * 0.5, 10),
    //             XStringFormats.TopCenter);
    //         colPos += oneCM * 0.5;
    //         gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 0.5);
    //         gfx.DrawString("RD", fontSmall, XBrushes.Black, new XRect(colPos, currentTop, oneCM * 0.5, 20),
    //             XStringFormats.TopCenter);
    //         colPos += oneCM * 0.5;
    //         gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 0.5);
    //     }
    //
    //     gfx.DrawLine(XPens.Black, leftMargin, currentTop + oneCM * 0.5, rightMargin - oneCM, currentTop + oneCM * 0.5);
    //     currentTop += oneCM * 0.5;
    //
    //     // Zeilen für 10 Mannschaften
    //     for (int i = 1; i <= 10; i++)
    //     {
    //         //Mannschaftsnummer
    //         colPos = leftMargin;
    //         gfx.DrawString(i.ToString(), fontHeader, XBrushes.Black, new XRect(colPos, currentTop, oneCM, oneCM * 1.5),
    //             XStringFormats.Center);
    //         colPos += oneCM;
    //         gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 1.5);
    //
    //         //Spielernamen
    //         XPen penDash = new XPen(XColors.Black, 1) { DashStyle = XDashStyle.Dash };
    //         gfx.DrawLine(penDash, colPos, currentTop + oneCM * 0.75, colPos + oneCM * 3.5,
    //             currentTop + oneCM * 0.75); //Trennline Namen
    //         colPos += oneCM * 3.5;
    //         gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 1.5);
    //
    //         //Nächte
    //         for (int j = 1; j <= 6; j++)
    //         {
    //             gfx.DrawLine(XPens.Black, colPos, currentTop + oneCM * 0.75, colPos + oneCM * 2,
    //                 currentTop + oneCM * 0.75); //Trennline Namen
    //             colPos += oneCM * 2.0 / 3.0;
    //             gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 1.5); //Trennlinie Holz
    //             colPos += oneCM * 2.0 / 3.0;
    //             gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 1.5); //Trennlinie Holz
    //             colPos += oneCM * 2.0 / 3.0;
    //             gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 1.5); //Trennlinie Holz
    //
    //             colPos += oneCM * 0.5;
    //             gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 1.5); //Trennlinie Punkte
    //             colPos += oneCM * 0.5;
    //             gfx.DrawLine(XPens.Black, colPos, currentTop, colPos, currentTop + oneCM * 1.5); //Trennlinie Runde
    //         }
    //
    //         gfx.DrawLine(XPens.Black, leftMargin, currentTop + oneCM * 1.5, rightMargin - oneCM,
    //             currentTop + oneCM * 1.5);
    //         currentTop += oneCM * 1.5;
    //     }
    //
    //     if (bPDF)
    //     {
    //         var filename = DelShowFileDialog?.Invoke();
    //         document.Save(filename);
    //     }
    //     else
    //     {
    //         var filename = PdfFileUtility.GetTempPdfFullFileName("Vorlage_6TageRennen");
    //         document.Save(filename);
    //         PdfFileUtility.ShowDocument(filename);
    //     }
    // }


    [ObservableProperty] private bool auswahlAktiveMitglieder = true;
    [ObservableProperty] private bool auswahlAlleMitglieder = false;

    [ObservableProperty] private bool auswahl6TageRennen = false;
    [ObservableProperty] private bool auswahlKombimeisterschaft = false;
    [ObservableProperty] private bool auswahlMeisterschaft = false;
    [ObservableProperty] private bool auswahlBlitztunier = false;
    [ObservableProperty] private bool auswahlWeihnachtsbaum = false;

    [ObservableProperty] private bool auswahlSpielverluste = false;
    [ObservableProperty] private bool auswahlAbrechnung = false;

    [RelayCommand]
    public async Task Vorschau()
    {
        _printService.DruckVorlage6TageRennen();

        _printService.DruckVorlageKombimeisterschaft();

        _printService.DruckvorlageAbrechnung();

        _printService.DruckVorlageSpielverluste();

        await _printService.DruckMitgliederlisteAsync();
        await _printService.DruckMitgliederlisteAsync(false);
    }

    [RelayCommand]
    public void PDFExport()
    {
        //DruckVorlage6TageRennen(true);
    }
}