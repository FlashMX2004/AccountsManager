using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FMX.AccountsManager.DIInstallers
{
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
            // Dialogs Registring.
            //
            DialogService.OnDialogsRegister += service =>
            {
                service.RegisterFactory(container.Resolve<AddAccountRecordDialog>);
            };
        }
    }
}
