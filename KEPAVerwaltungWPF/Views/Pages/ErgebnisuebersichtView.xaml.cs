using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class ErgebnisuebersichtView : UserControl
{
    public ErgebnisuebersichtViewModel ErgebnisuebersichtViewModel { get; }

    public ErgebnisuebersichtView(ErgebnisuebersichtViewModel ergebnisuebersichtViewModel)
    {
        ErgebnisuebersichtViewModel = ergebnisuebersichtViewModel;
        InitializeComponent();
        DataContext = ErgebnisuebersichtViewModel;
        ErgebnisuebersichtViewModel.InitBaseViewModelDelegateAndEvents();
    }

    // private void BtnSelectAllDays_OnClick(object sender, RoutedEventArgs e)
    // {
    //     if (DataContext is ErgebnisuebersichtViewModel vm)
    //     {
    //         vm.SelectedSpieltage = new ObservableCollection<Spieltage>(vm.Spieltageliste);
    //         
    //         var listBox = this.FindName("lbSpieltage") as ListBox;
    //         if (listBox != null)
    //         {
    //             listBox.SelectedItems.Clear();
    //             foreach (var item in vm.SelectedSpieltage)
    //             {
    //                 listBox.SelectedItems.Add(item);
    //             }
    //         }
    //     }
    // }
}