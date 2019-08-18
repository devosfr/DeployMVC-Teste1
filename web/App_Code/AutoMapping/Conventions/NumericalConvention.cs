using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

/// <summary>
/// Summary description for DecimalConvention
/// </summary>
public class NumericalConvention : IPropertyConvention
{
    public void Apply(IPropertyInstance instance)
    {
        if (instance.Type == typeof(decimal)) //Set the condition based on your needs
        {
            instance.Precision(10);
            instance.Scale(2);
        }
    }
}