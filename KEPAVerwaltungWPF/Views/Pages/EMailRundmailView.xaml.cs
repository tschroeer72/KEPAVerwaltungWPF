using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;
using Microsoft.Win32;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class EMailRundmailView : UserControl
{
    public EMailRundmailViewModel EMailRundmailViewModel { get; }

    public EMailRundmailView(EMailRundmailViewModel eMailRundmailViewModel)
    {
        EMailRundmailViewModel = eMailRundmailViewModel;
        InitializeComponent();
        DataContext = EMailRundmailViewModel;
        EMailRundmailViewModel.InitBaseViewModelDelegateAndEvents();
        
        EMailRundmailViewModel.DelShowFileDialog += () => ShowFileDialog();
    }
    
    private string[] ShowFileDialog()
    {
        string[] files = new string[0];
        
        try
        {
            OpenFileDialog dlgOpenFile = new()
            {
                Filter = "Alle Dateien|*.*",
                Multiselect = true,
                RestoreDirectory = true
            };

            var result = dlgOpenFile.ShowDialog();
            if (result == true && result.HasValue)
            {
                files = dlgOpenFile.FileNames;
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowInformationWindow(ex.Message);
        }
        
        return files;
    }
}