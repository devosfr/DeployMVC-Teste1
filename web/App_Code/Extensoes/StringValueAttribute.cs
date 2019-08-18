/// <summary>
/// Summary description for StringValueAttribute
/// </summary>
public class StringValueAttribute : System.Attribute
{

    private string _value;

    public StringValueAttribute(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }

}