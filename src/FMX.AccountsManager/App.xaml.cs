using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Reflection;
using System.Windows;

namespace FMX.PasswordManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Path to data in regitry
    /// </summary>
    public const string REGISTRY_PATH = @"SOFTWARE\FMX.AccountManager";

    /// <summary>
    /// Castle.Windsor DI container
    /// </summary>
    private readonly IWindsorContainer windsor;

    /// <summary>
    /// Default Constructor
    /// </summary>
    public App() => windsor = new WindsorContainer();
    
    /// <summary>
    /// Custom startup
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e) =>
        windsor.Install(FromAssembly.Instance(Assembly.GetExecutingAssembly()))
               .Resolve<MainWindow>()
               .Show();
}