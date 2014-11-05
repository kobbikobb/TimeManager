
using FluentNHibernate.Mapping;
using TimeManager.Core;

namespace TimeManager.NHibernate
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Description).Not.Nullable();
            Map(x => x.Started).Not.Nullable();
            Map(x => x.Completed);
            Map(x => x.WorkedHours).Not.Nullable();

            References(x => x.Category).Cascade.SaveUpdate();
        }
    }
}
