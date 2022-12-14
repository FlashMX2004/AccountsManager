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
            container.Register(Component.For<IClipboardService>().ImplementedBy<ClipboardService>().LifestyleSingleton());
            container.Register(Component.For<IBinarySerializator>().ImplementedBy<BinarySerializator>().LifestyleSingleton());
            container.Register(Component.For<IXMLSerializator>().ImplementedBy<XMLSerializator>().LifestyleSingleton());
            container.Register(Component.For<ISerializationService>().ImplementedBy<SerializationService>().LifestyleSingleton());
        }
    }
}
