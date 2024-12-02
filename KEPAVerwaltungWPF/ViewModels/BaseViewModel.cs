using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    public delegate bool DelShowInformationWindowType(string iMessage);
    public DelShowInformationWindowType? DelShowInformationWindow { get; set; }

    public delegate bool DelShowConfirmationWindowType(string iMessage);
    public DelShowConfirmationWindowType? DelShowConfirmationWindow { get; set; }

    public delegate bool DelShowErrorWindowType(string sModul, string sMethod, string sErrorMessage);
    public DelShowErrorWindowType? DelShowErrorWindow { get; set; }
    
    public delegate string DelShowInputWindowType(string iMessage);
    public DelShowInputWindowType? DelShowInputWindow { get; set; }

    public delegate void DelShowMainInfoFlyoutType(string iMessage, bool iWarnung = false);
    public DelShowMainInfoFlyoutType? DelShowMainInfoFlyout { get; set; }


    public delegate void DelGoBackOrGotoHomeType();
    public DelGoBackOrGotoHomeType? DelGoBackOrGotoHome { get; set; }

    public delegate void DelShowLoginViewType(bool bAsStartLogin);
    public DelShowLoginViewType? DelShowLoginView { get; set; }

    [ObservableProperty]
    string titel = "";

    [ObservableProperty]
    string message = string.Empty;

    [ObservableProperty]
    bool isViewModelLoaded = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPageNotBusy))]
    bool isPageBusy = false;

    public bool IsPageNotBusy => !IsPageBusy;  
}