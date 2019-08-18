using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Repository.Conventions
{
    public class DefaultReferenceConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(string.Format(instance.Class.Name.StartsWith("id") ? "{1}" : "{0}{1}", "id",
                                          instance.Class.Name));
            instance.LazyLoad();

        }
    }
}