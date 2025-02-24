using System.Windows;
using System.Windows.Input;
using KEPAVerwaltungWPF.Enums;

namespace KEPAVerwaltungWPF.Views.Windows;

public partial class InfoWindow : Window
{
    private DialogType DialogType { get; set; } = new();
    public string InputText { get; set; } = "";
    
    public InfoWindow(string iMessage, DialogType iDialogType)
    {
        InitializeComponent();
        Owner = ViewManager.MainView;
        txtMessage.Text = iMessage;
        DialogType = iDialogType;

        BorderInputText.Visibility = Visibility.Collapsed;
        Grid_JA_NEIN.Visibility = Visibility.Collapsed;
        Grid_Btn_OK_BtnAbbrechen.Visibility = Visibility.Collapsed;

        switch (DialogType)
        {
            case DialogType.Information:
                lblTitle.Content = "Information";
                Grid_Btn_OK_BtnAbbrechen.Visibility = Visibility.Visible;
                BtnAbbrechen.Visibility = Visibility.Collapsed;
                break;
            case DialogType.Confirmation:
                lblTitle.Content = "Bestätigen";
                Grid_JA_NEIN.Visibility = Visibility.Visible;
                break;
            case DialogType.Input:
                lblTitle.Content = "Angabe";
                BorderInputText.Visibility = Visibility.Visible;
                Grid_Btn_OK_BtnAbbrechen.Visibility = Visibility.Visible;
                break;
            default:
                break;
        }
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
    
    private void BtnNein_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void BtnJa_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void BtnAbbrechen_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void BtnOK_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        if (DialogType == DialogType.Input)
        {
            InputText = txtInputText.Text;
        }

    }
}