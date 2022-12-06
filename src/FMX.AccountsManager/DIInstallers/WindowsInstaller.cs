using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FMX.AccountsManager.DIInstallers
{
    /// <summary>
    /// Windows installation to DI container
    /// </summary>
    public class WindowsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainWindow>()
                                        .UsingFactoryMethod(() => new MainWindow 
                                        { 
                                            ViewModel = container.Resolve<MainViewModel>() 
                                        })
                                        .LifestyleSingleton());

            container.Register(Component.For<AddAccountRecordDialog>()
                                        .UsingFactoryMethod(() => new AddAccountRecordDialog 
                                        { 
                                            ViewModel = container.Resolve<AddAccountRecordViewModel>() 
                                        })
                                        .LifestyleTransient());

            //
            // Dialog factories registring.
            //
            DialogService.OnDialogFactoriesRegistring += service =>
            {
                service.RegisterDialogFactory(container.Resolve<AddAccountRecordDialog>);
            };
        }
    }
}
