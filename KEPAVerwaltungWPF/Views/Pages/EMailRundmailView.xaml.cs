using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

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
    }
}