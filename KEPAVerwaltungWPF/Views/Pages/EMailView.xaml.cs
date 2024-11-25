using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class EMailView : UserControl
{
    public EMailViewModel EMailViewModel { get; }

    public EMailView(EMailViewModel eMailViewModel)
    {
        EMailViewModel = eMailViewModel;
        InitializeComponent();
        DataContext = EMailViewModel;
        EMailViewModel.InitBaseViewModelDelegateAndEvents();
    }
}