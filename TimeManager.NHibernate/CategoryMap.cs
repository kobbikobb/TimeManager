using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using TimeManager.Core;

namespace TimeManager.NHibernate
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name).Not.Nullable();
            
            References(x => x.Project).Cascade.SaveUpdate();
            HasMany(x => x.Tasks).Inverse().Cascade.All();
        }
    }
}
