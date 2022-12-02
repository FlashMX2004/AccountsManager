using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FMX.PasswordManager.DIInstallers
{
    public class WindowsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainWindow>().Instance(new MainWindow { DataContext = container.Resolve<MainViewModel>() }));
        }
    }
}
