using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using System.Reflection;

namespace Repository.Conventions
{
    public class DefaultStringLengthConvention : IPropertyConvention
    {
            public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria) { criteria.Expect(x => x.Type == typeof(string)); }
            public void Apply(IPropertyInstance instance)
            {
                int leng = 255;

                MemberInfo[] myMemberInfos = ((PropertyInstance)(instance)).EntityType.GetMember(instance.Name);
                if (myMemberInfos.Length > 0)
                {
                    object[] myCustomAttrs = myMemberInfos[0].GetCustomAttributes(false);
                    if (myCustomAttrs.Length > 0)
                    {
                        if (myCustomAttrs[0] is StringLength)
                        {
                            leng = ((StringLength)(myCustomAttrs[0])).Length;
                        }
                    }
                }
                instance.Length(leng);
            }
        
    }
}