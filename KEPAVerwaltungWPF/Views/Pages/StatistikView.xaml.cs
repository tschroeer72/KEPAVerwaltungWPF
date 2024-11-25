using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class StatistikView : UserControl
{
    public StatistikViewModel StatistikViewModel { get; }

    public StatistikView(StatistikViewModel statistikViewModel)
    {
        StatistikViewModel = statistikViewModel;
        InitializeComponent();
        DataContext = StatistikViewModel;
        StatistikViewModel.InitBaseViewModelDelegateAndEvents();
    }
}