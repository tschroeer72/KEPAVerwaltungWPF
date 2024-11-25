using System.Windows;
using KEPAVerwaltungWPF.ViewModels;
using KEPAVerwaltungWPF.Views;
using KEPAVerwaltungWPF.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using KEPAVerwaltungWPF.Views.Windows;

namespace KEPAVerwaltungWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _ServiceProvider;

    public App()
    {
        ServiceCollection ServiceCollection = new();
        ConfigureServices(ServiceCollection);
        _ServiceProvider = ServiceCollection.BuildServiceProvider();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var mainView = _ServiceProvider.GetService<MainWindow>();
        if(mainView == null)
        {
            MessageBox.Show("Problem im ServiceProvider");
            Current.Shutdown();
        }

        ViewManager.InitViewManager(mainView!, _ServiceProvider);
        mainView!.Show();

        base.OnStartup(e);
    }
    
    private void ConfigureServices(ServiceCollection services)
    {
        //AutoMapper && Validator  
        //Type VMAssembyRefTyp = typeof(MappingConfig);
        //services.AddAutoMapper(VMAssembyRefTyp);

        //--------------------

        //Views und ViewModels
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowModel>();

        services.AddSingleton<HomeView>();
        services.AddSingleton<HomeViewModel>();

        services.AddSingleton<EinstellungenView>();
        services.AddSingleton<EinstellungenViewModel>();
    }
}