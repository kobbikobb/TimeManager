using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.Logging;
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

namespace TimeManager.Container
{
    public class ApplicationContainer : IDisposable
    {
        private readonly IWindsorContainer container;

        public ApplicationContainer()
        {
            container = new WindsorContainer();

            container.AddFacility<TypedFactoryFacility>();
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithConfig("log4net.xml"));
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(Component.For<LogInterceptor>());
            
            container.Install(new RepositoryInstaller());
            container.Install(new PresentationInstaller());
            container.Install(new ApplicationInstaller());
        }
        
        public TaskAutomation ResolveTaskAutomation()
        {
            return container.Resolve<TaskAutomation>();
        }

        public Tray ResolveTray()
        {
            return container.Resolve<Tray>();
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
