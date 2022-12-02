using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Reflection;
using System.Windows;

namespace FMX.AccountsManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
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

    /// <summary>
    /// Returns service of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Type of service</typeparam>
    /// <returns>Service of type <typeparamref name="T"/></returns>
    public static T GetService<T>() => (Current as App)!.windsor.Resolve<T>();
}