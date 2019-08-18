using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Repository.Conventions
{
    public class DefaultTableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table(string.Format("{0}{1}", "gl_", instance.EntityType.Name.ToLower()));
        }
    }
}