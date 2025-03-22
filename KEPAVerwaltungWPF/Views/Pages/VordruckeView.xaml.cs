using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;
using Microsoft.Win32;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class VordruckeView : UserControl
{
    public VordruckeViewModel VordruckeViewModel { get; }

    public VordruckeView(VordruckeViewModel vordruckeViewModel)
    {
        VordruckeViewModel = vordruckeViewModel;
        InitializeComponent();
        DataContext = VordruckeViewModel;
        VordruckeViewModel.InitBaseViewModelDelegateAndEvents();
        
        VordruckeViewModel.DelShowFileDialog += () => ShowFileDialog();
    }
    
    private string ShowFileDialog()
    {
        string files = string.Empty;
        
        try
        {
            SaveFileDialog dlgSaveFile = new()
            {
                Filter = "PDF-Datei|*.pdf",
                RestoreDirectory = true
            };

            var result = dlgSaveFile.ShowDialog();
            if (result == true && result.HasValue)
            {
                files = dlgSaveFile.FileName;
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowInformationWindow(ex.Message);
        }
        
        return files;
    }
}