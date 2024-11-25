using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class EMailEntwicklerView : UserControl
{
    public EMailEntwicklerViewModel EMailEntwicklerViewModel { get; }

    public EMailEntwicklerView(EMailEntwicklerViewModel eMailEntwicklerViewModel)
    {
        EMailEntwicklerViewModel = eMailEntwicklerViewModel;
        InitializeComponent();
        DataContext = EMailEntwicklerViewModel;
        EMailEntwicklerViewModel.InitBaseViewModelDelegateAndEvents();
    }
}