using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using KEPAVerwaltungWPF.Models.Local;
using KEPAVerwaltungWPF.Models.Web;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Validations;
using KEPAVerwaltungWPF.ViewModels;
using KEPAVerwaltungWPF.Views;
using KEPAVerwaltungWPF.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using KEPAVerwaltungWPF.Views.Windows;
using Microsoft.EntityFrameworkCore;
using SplashScreen = KEPAVerwaltungWPF.Views.Windows.SplashScreen;

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
    
    protected override async void OnStartup(StartupEventArgs e)
    {
        var mainView = _ServiceProvider.GetService<MainWindow>();
        if(mainView == null)
        {
            MessageBox.Show("Problem im ServiceProvider");
            Current.Shutdown();
        }

        ViewManager.InitViewManager(mainView!, _ServiceProvider);
        
        // SplashScreen aus DI abrufen
        var splashScreen = _ServiceProvider.GetRequiredService<SplashScreen>();
        var splashViewModel = _ServiceProvider.GetRequiredService<SplashScreenViewModel>();
        splashScreen.Show();

        // Initialisierung
        await splashViewModel.InitializeAsync();
        
        splashScreen.Close();
        mainView!.Show();

        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.Name)));
        
        base.OnStartup(e);
    }
    
    private void ConfigureServices(ServiceCollection services)
    {
        //AutoMapper  
        Type VMAssembyRefTyp = typeof(AutoMapperConfig);
        services.AddAutoMapper(VMAssembyRefTyp);

        //Validations
        services.AddSingleton<MitgliedAnlegenValidation>();
        services.AddSingleton<MitgliedAktualisierenValidation>();
        services.AddSingleton<MeisterschaftValidation>();
        services.AddSingleton<MeisterschaftAktualisierenValidation>();
        
        //--------------------

        //Views und ViewModels
        services.AddSingleton<SplashScreen>();
        services.AddSingleton<SplashScreenViewModel>();
        
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

        //Services
        services.AddSingleton<DBService>();
        
        // DbContexts
        var localDbConnectionString = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;
        services.AddDbContext<LocalDbContext>(options => options.UseSqlServer(localDbConnectionString));

        var webDbConnectionString = ConfigurationManager.ConnectionStrings["WebDB"].ConnectionString;
        services.AddDbContext<WebDbContext>(options => options.UseMySql(webDbConnectionString, new MySqlServerVersion(new Version(8, 0, 21))));
    }
}

/*

//LocalDB 
Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=KEPAVerwaltung.mdf;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\Local

Scaffold-DbContext "Server=(localdb)\mssqllocaldb;attachdbfilename=D:\UDEMY\KEPAVerwaltungWPF\KEPAVerwaltungWPF\KEPAVerwaltung.mdf;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\Local -Context LocalDbContext -f

//MariaDB / All-Inkl
Scaffold-DbContext "server=w01bdc60.kasserver.com;database=d03c455b;uid=d03c455b;pwd=KKpJnQJsm2t6VNXo;sslmode=Required" Pomelo.EntityFrameworkCore.MySql -OutputDir Models\Web -Context WebDbContext -f
 
 */