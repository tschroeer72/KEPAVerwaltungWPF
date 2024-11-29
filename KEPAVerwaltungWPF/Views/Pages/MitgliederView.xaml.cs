using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class MitgliederView : UserControl
{
    public MitgliederViewModel MitgliederViewModel { get; }

    public MitgliederView(MitgliederViewModel mitgliederViewModel)
    {
        InitializeComponent();

        MitgliederViewModel = mitgliederViewModel;
        DataContext = MitgliederViewModel;
        MitgliederViewModel.InitBaseViewModelDelegateAndEvents();
    }
    
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var m = MitgliederViewModel.CurrentMitglied;
        Console.WriteLine(m);
    }
}