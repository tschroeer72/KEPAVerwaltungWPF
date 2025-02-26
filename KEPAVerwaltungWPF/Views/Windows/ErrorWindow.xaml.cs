using System.Text;
using System.Windows;
using System.Windows.Input;
using KEPAVerwaltungWPF.Helper;

namespace KEPAVerwaltungWPF.Views.Windows;

public partial class ErrorWindow : Window
{
    private string _strModul = string.Empty;
    private string _strMethod = string.Empty;
    private string _strErrorMessage = string.Empty;
    
    public ErrorWindow(string sModul, string sMethod, string sErrorMessage)
    {
        InitializeComponent();
        
        _strModul = sModul;
        _strMethod = sMethod;
        _strErrorMessage = sErrorMessage;
        
        lblTitle.Content = "Fehler in " + _strModul;
        LblMethode.Content = _strMethod;
        TxtMessage.Text = _strErrorMessage;
    }
    
    private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        catch (Exception)
        {
        }
    }
    
    private void BtnSchliessen_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void BtnOK_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void BtnEMail_OnClick(object sender, RoutedEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        MailService objMailService = new MailService();

        objMailService.SMTPServer = "w01bdc60.kasserver.com";
        objMailService.SMTPUser = "m06aaec5";
        objMailService.SMTPPassword = "BodnXC5Pt3pMwQE4jPCU";
        objMailService.SMTPPort = 587;
        objMailService.TLS = true;
        string strBetreff = "KEPAVerwaltung Exception ";

        objMailService.Subject = strBetreff;

        sb.AppendLine("Fehler im");
        sb.Append("Modul: " + _strModul);
        sb.AppendLine();
        sb.AppendLine();
        sb.Append("Methode: " + _strMethod);
        sb.AppendLine();
        sb.AppendLine();
        sb.AppendLine("Exception:");
        sb.AppendLine(_strErrorMessage);
        objMailService.Body = sb.ToString();
        objMailService.From = "issue@kegelgruppe-kepa.de";

        List<string> lstTo = new List<string>();
        lstTo.Add("t.schroeer@web.de");
        objMailService.To = lstTo;

        objMailService.Send();

        MessageBox.Show("Fehlermeldung wurde versendet", "Mailversand", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}