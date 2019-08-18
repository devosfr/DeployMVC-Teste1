using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for FreteDTO
/// </summary>
[DataContract]
public class FreteDTO
{
    [DataMember]
	public virtual string Codigo { get; set; }
    [DataMember]
    public virtual string Nome { get; set; }
    [DataMember]
	public virtual decimal Preco { get; set; }
    [DataMember]
    public virtual int Prazo { get; set; }
		
	public FreteDTO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}