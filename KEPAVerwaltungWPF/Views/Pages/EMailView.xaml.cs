using System.Windows;
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
    
    private void OpenUnderPage(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton objButton)
        {
            switch (objButton.Name)
            {
                case "BtnEntwickler":
                    ViewManager.ShowUnderPageOn<EMailEntwicklerView>(AnimatedContentControl);
                    break;
                case "BtnRundmail":
                    ViewManager.ShowUnderPageOn<EMailRundmailView>(AnimatedContentControl);
                    break;
            }
        }
    }

    private void EMailView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!EMailViewModel.IsViewModelLoaded)
        {
            ViewManager.ShowUnderPageOn<EMailRundmailView>(AnimatedContentControl);
            BtnRundmail.IsChecked = true;
            EMailViewModel.IsViewModelLoaded = true;
        }
    }
}