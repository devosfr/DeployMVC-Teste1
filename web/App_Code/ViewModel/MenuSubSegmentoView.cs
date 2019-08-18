using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SegmentoView
/// </summary>
public class MenuSubSegmentoView
{
    public string Nome { get; set; }
    public string Classe { get; set; }
    public string Configuracoes { get; set; }
    public string Link { get; set; }
    public IList<MenuCategoriaView> Categorias { get; set; }
    public MenuSubSegmentoView()
    {
        Categorias = new List<MenuCategoriaView>();
    }
}

