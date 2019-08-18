using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Repository.Conventions
{
    public class DefaultHasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(string.Format("{0}{1}", "id", instance.EntityType.Name));
            instance.LazyLoad();
            instance.Cascade.AllDeleteOrphan();
            //instance.Inverse();
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }


    }
    public class CustomManyToManyTableNameConvention
: ManyToManyTableNameConvention
    {
        protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
        {
            return collection.EntityType.Name.ToLower() + "_" + otherSide.EntityType.Name.ToLower();
        }

        protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
        {
            return collection.EntityType.Name.ToLower() + "_" + collection.ChildType.Name.ToLower();
        }
    }
    public class ManyToManyConvention : IHasManyToManyConvention
    {
        #region IConvention<IManyToManyCollectionInspector,IManyToManyCollectionInstance> Members

        public void Apply(FluentNHibernate.Conventions.Instances.IManyToManyCollectionInstance instance)
        {
            if (instance.OtherSide == null)
            {
                instance.Table(
                   string.Format(
                       "{0}To{1}",
                       instance.EntityType.Name + "_Id",
                       instance.ChildType.Name + "_Id"));
            }
            instance.Cascade.All();
        }

        #endregion
    }

}