using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

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
    }
}