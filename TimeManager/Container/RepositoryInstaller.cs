using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TimeManager.Core.Repositories;
using TimeManager.NHibernate;

namespace TimeManager.Container
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            const string connectionString = @"Data Source=(LocalDB)\v11.0;Integrated Security=True;" +
                                       @"AttachDbFileName=|DataDirectory|\TimeManager.mdf";

            var sessionFactory = Fluently.Configure()
                   .Database(MsSqlConfiguration.MsSql2005.ConnectionString(connectionString))
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                   .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                   .BuildSessionFactory();

            container.Register(Component.For<ISession>().UsingFactoryMethod(sessionFactory.OpenSession).LifestyleTransient());
            container.Register(Component.For<ITimeManagerRepository>()
                .ImplementedBy<TimeManagerRepository>()
                .Interceptors<LogInterceptor>()
                .LifestyleTransient());
        }
    }
}
