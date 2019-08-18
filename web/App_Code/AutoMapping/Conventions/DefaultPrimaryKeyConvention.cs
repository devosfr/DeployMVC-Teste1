using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Repository.Conventions
{
    public class DefaultPrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column("id");
            instance.GeneratedBy.Native();
        }
    }
}