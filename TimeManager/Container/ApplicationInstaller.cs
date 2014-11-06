using Castle.MicroKernel.Registration;

namespace TimeManager.Container
{
    public class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<TaskAutomation>());
            container.Register(Component.For<StartTaskAction, ITrayAction>()
                .Interceptors<LogInterceptor>()
                .ImplementedBy<StartTaskAction>());
            container.Register(Component.For<ViewWorkbookAction, ITrayAction>()
                .Interceptors<LogInterceptor>()
                .ImplementedBy<ViewWorkbookAction>());
            container.Register(Component.For<Tray>());
        }
    }
}
