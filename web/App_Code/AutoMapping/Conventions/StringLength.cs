using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StringLength
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class StringLength : System.Attribute
{
    public int Length = 0;
    public StringLength(int taggedStrLength)
    {
        Length = taggedStrLength;
    }
}