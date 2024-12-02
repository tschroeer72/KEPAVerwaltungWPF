using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using KEPAVerwaltungWPF.Services;
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

        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.Name)));
        
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

        services.AddSingleton<VerwaltungView>();
        services.AddSingleton<VerwaltungViewModel>();
        
        services.AddSingleton<MitgliederView>();
        services.AddSingleton<MitgliederViewModel>();

        services.AddSingleton<MeisterschaftenView>();
        services.AddSingleton<MeisterschaftenViewModel>();

        services.AddSingleton<SpielverwaltungView>();
        services.AddSingleton<SpielverwaltungViewModel>();

        services.AddSingleton<ErgebniseingabeView>();
        services.AddSingleton<ErgebniseingabeViewModel>();

        services.AddSingleton<ErgebnisuebersichtView>();
        services.AddSingleton<ErgebnisuebersichtViewModel>();

        services.AddSingleton<StatistikView>();
        services.AddSingleton<StatistikViewModel>();

        services.AddSingleton<EMailView>();
        services.AddSingleton<EMailViewModel>();

        services.AddSingleton<EMailEntwicklerView>();
        services.AddSingleton<EMailEntwicklerViewModel>();

        services.AddSingleton<EMailRundmailView>();
        services.AddSingleton<EMailRundmailViewModel>();
        
        services.AddSingleton<BerichtsView>();
        services.AddSingleton<BerichtsViewModel>();
        
        services.AddSingleton<EinstellungenView>();
        services.AddSingleton<EinstellungenViewModel>();

        services.AddSingleton<DBService>();
    }
}