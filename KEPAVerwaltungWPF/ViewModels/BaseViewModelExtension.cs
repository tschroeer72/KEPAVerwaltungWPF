using KEPAVerwaltungWPF.Views;

namespace KEPAVerwaltungWPF.ViewModels;

public static class BaseViewModelExtension
{
    public static void InitBaseViewModelDelegateAndEvents(this BaseViewModel baseViewModel)
    {
        ViewManager.InitBaseDelEvents(baseViewModel);
    }
}