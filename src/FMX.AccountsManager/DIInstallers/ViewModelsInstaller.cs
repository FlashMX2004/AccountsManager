using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FMX.AccountsManager.DIInstallers
{
    public class ViewModelsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainViewModel>().LifestyleSingleton());
            container.Register(Component.For<AddAccountRecordViewModel>().LifestyleTransient());
        }
    }
}
