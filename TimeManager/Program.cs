using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TimeManager.Core.Repositories;
using TimeManager.NHibernate;
using TimeManager.Presentation.ViewModels;
using Application = System.Windows.Forms.Application;

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

            //Set the default format for wpf controls to be of the machine default language
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            using (var container = new WindsorContainer())
            {
                container.AddFacility<TypedFactoryFacility>();
                container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
                const string connectionString = @"Data Source=(LocalDB)\v11.0;Integrated Security=True;" + 
                                                @"AttachDbFileName=|DataDirectory|\TimeManager.mdf";
                var sessionFactory = Fluently.Configure()
                       .Database(MsSqlConfiguration.MsSql2005.ConnectionString(connectionString))
                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                       .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                       .BuildSessionFactory();

                container.Register(Component.For<ISession>().UsingFactoryMethod(sessionFactory.OpenSession).LifestyleTransient());
                container.Register(Component.For<ITimeManagerRepository>().ImplementedBy<TimeManagerRepository>().LifestyleTransient());

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
