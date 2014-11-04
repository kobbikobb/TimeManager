using System;
using System.Windows.Forms;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using TimeManagerLib.Data;
using TimeManagerLib.Model;
using TimeManagerLib.ViewModel;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TimeManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var container = new WindsorContainer())
            {
                container.AddFacility<TypedFactoryFacility>();
                container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

                container.Register(Component.For<ITimeManagerRepository>().ImplementedBy<TimeManagerRepositoryFake>());

                container.Register(Component.For<StartTaskViewModel>().LifestyleTransient());
                container.Register(Component.For<WorkbookViewModel>().LifestyleTransient());
                container.Register(Component.For<IStartTaskViewModelFactory>().AsFactory());
                container.Register(Component.For<IWorkbookViewModelFactory>().AsFactory());

                container.Register(Component.For<TaskAutomation>());
                container.Register(Component.For<StartTaskAction, ITrayAction>().ImplementedBy<StartTaskAction>());
                container.Register(Component.For<ViewWorkbookAction, ITrayAction>().ImplementedBy<ViewWorkbookAction>());
                container.Register(Component.For<Tray>());

                var taskAutomation = container.Resolve<TaskAutomation>();
                var machineStatusTaskAutomation = new MachineStatusTaskAutomation(taskAutomation);
                var timeoutTaskAutomation = new TimeoutTaskAutomation(taskAutomation);
                machineStatusTaskAutomation.StartAutomation();
                timeoutTaskAutomation.StartAutomation();

                var tray = container.Resolve<Tray>();

                Application.Run(tray);
            }
        }
    }
}
