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
        
        LblModul.Content = _strModul;
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
        ClsMail objMail = new ClsMail();

        objMail.SMTPServer = "w01bdc60.kasserver.com";
        objMail.SMTPUser = "m06aaec5";
        objMail.SMTPPassword = "BodnXC5Pt3pMwQE4jPCU";
        objMail.SMTPPort = 587;
        objMail.TLS = true;
        string strBetreff = "KEPAVerwaltung Exception ";

        objMail.Subject = strBetreff;

        sb.AppendLine("Fehler im");
        sb.Append("Modul: " + _strModul);
        sb.AppendLine();
        sb.AppendLine();
        sb.Append("Methode: " + _strMethod);
        sb.AppendLine();
        sb.AppendLine();
        sb.AppendLine("Exception:");
        sb.AppendLine(_strErrorMessage);
        objMail.Body = sb.ToString();
        objMail.From = "issue@kegelgruppe-kepa.de";

        List<string> lstTo = new List<string>();
        lstTo.Add("t.schroeer@web.de");
        objMail.To = lstTo;

        objMail.Send();

        MessageBox.Show("Fehlermeldung wurde versendet", "Mailversand", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}