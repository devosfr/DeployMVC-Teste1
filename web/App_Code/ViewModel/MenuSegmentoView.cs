using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubSegmentoView
/// </summary>
public class MenuSegmentoView
{
    public string Nome { get; set; }
    public string Link { get; set; }
    public string Classe { get; set; }
    public string Configuracoes { get; set; }
    public IList<MenuSubSegmentoView> SubSegmentos { get; set; }

    public MenuSegmentoView() 
    {
        SubSegmentos = new List<MenuSubSegmentoView>();
    }
}
