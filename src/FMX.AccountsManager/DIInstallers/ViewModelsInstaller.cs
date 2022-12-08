using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FMX.AccountsManager.DIInstallers
{
    /// <summary>
    /// View models installation to DI container
    /// </summary>
    public class ViewModelsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainViewModel>().LifestyleSingleton());
            container.Register(Component.For<AddAccountRecordViewModel>().LifestyleTransient());
            container.Register(Component.For<MessageBoxViewModel>().LifestyleTransient());
            container.Register(Component.For<AccountRecordViewModel>().LifestyleTransient());
        }
    }
}
