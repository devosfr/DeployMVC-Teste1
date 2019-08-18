using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for CupomDTO
/// </summary>
[DataContract]
public class CupomDTO
{
	public CupomDTO()
	{
		//
		// TODO: Add constructor logic here
		//
        
	}
    [DataMember]
    public string Codigo { get; set; }

    [DataMember]
    public decimal desconto { get; set; }

    [DataMember]
    public int descontoPercentual { get; set; }

    [DataMember]
    public DateTime validade { get; set; }
}