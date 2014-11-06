using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using TimeManager.Presentation.ViewModels;

namespace TimeManager.Container
{
    public class PresentationInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<StartTaskViewModel>()
              .Interceptors<LogInterceptor>()
              .LifestyleTransient());
            container.Register(Component.For<WorkbookViewModel>()
                .Interceptors<LogInterceptor>()
                .LifestyleTransient());
            container.Register(Component.For<IStartTaskViewModelFactory>().AsFactory());
            container.Register(Component.For<IWorkbookViewModelFactory>().AsFactory());
        }
    }
}
