using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FMX.AccountsManager.DIInstallers
{
    /// <summary>
    /// Services installation to DI container
    /// </summary>
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>());
            container.Register(Component.For<IRecordService>().ImplementedBy<RegistryRecordService>().LifestyleSingleton());
            container.Register(Component.For<IDialogService>().ImplementedBy<DialogService>().LifestyleSingleton());
        }
    }
}
