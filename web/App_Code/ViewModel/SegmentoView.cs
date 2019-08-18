using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SegmentoView
/// </summary>
public class SegmentoView
{
    public string Segmento { get; set; }
    public IList<SubSegmentoView> Categorias { get; set; }
    public SegmentoView()
    {
        Categorias = new List<SubSegmentoView>();
    }
}

public class SubSegmentoView
{
    public string Nome { get; set; }
    public bool Marcado { get; set; }
    public string Link { get; set; }
}

