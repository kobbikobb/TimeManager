using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TimeManager.NHibernate
{
    public class SessionFactoryHelper
    {
        public string ConnectionString { get; set; }

        public SessionFactoryHelper()
        {
            ConnectionString = @"Data Source=(LocalDB)\v11.0;Integrated Security=True;AttachDbFileName=|DataDirectory|\TimeManager.mdf";
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                       .Database(MsSqlConfiguration.MsSql2005.ConnectionString(ConnectionString))
                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                       .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                       .BuildSessionFactory();
        }
    }
}
