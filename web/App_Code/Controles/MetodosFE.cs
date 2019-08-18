using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using Modelos;
using NHibernate.Linq;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for MetodosFE
/// </summary>
/// 



public class MetodosFE
{


    public static string CONTROLE_TEXTO_GERENCIADOR
    {
        get
        {
            return BaseURL + "/Gerenciador/PaginasCadastro/Site/Telas/Default.aspx";
        }
    }

    public static string CONTROLE_TEXTO_CADASTRO
    {
        get
        {
            return BaseURL + "/Gerenciador/PaginasCadastro/Site/Telas/Cadastro.aspx";
        }
    }

    public static string LOCAL_PHOTO_LOW
    {
        get
        {
            return BaseURL + "/fotos_produtos/";
        }
    }
    public static string LOCAL_PHOTO_HD
    {
        get
        {
            return BaseURL + "/photos/";
        }
    }

    private static Repository<DadoVO> repoDado
    {
        get
        {
            return new Repository<DadoVO>(NHibernateHelper.CurrentSession);
        }
    }

    public static string ConverttoISOtoUTF8(string texto)
    {

        return Encoding.UTF8.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(texto)); ;
    }

    public static System.Drawing.Color corCabecalhoTabelas = System.Drawing.ColorTranslator.FromHtml("#A8CE47");

    public static string RemoverAcentos(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";
        else
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(input);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }

    public static DadoVO getTela(string nome)
    {
        if (String.IsNullOrEmpty(nome))
            return null;


        IList<DadoVO> lista = documentos.Where(x => x.tela != null && x.tela.nome == nome && x.visivel).ToList();

        if (lista.Count > 0)
            return lista[0];
        return null;

    }

    public static IList<DadoVO> documentos
    {
        get
        {
            if (HttpContext.Current.Items["current.documentos"] == null)
                HttpContext.Current.Items["current.documentos"] = repoDado.All().Fetch(x => x.tela).FetchMany(x => x.listaFotos).Where(x => x.visivel).ToList();
            return (IList<DadoVO>)HttpContext.Current.Items["current.documentos"];
        }
        set { HttpContext.Current.Items["current.documentos"] = value; }
    }

    //public static DadoVO getTela(int idPai)
    //{
    //    if (idPai <= 0)
    //        return null;


    //    IList<DadoVO> lista = documentos.Where(x => x.segPai.id == idPai).ToList();

    //    if (lista.Count > 0)
    //        return lista[0];
    //    return null;

    //}


    public static void limparCampos(Control cont)
    {
        //Vai editar todas as TextBox
        Control[] allControls = FlattenHierachy(cont);
        foreach (Control control in allControls)
        {
            TextBox textBox = control as TextBox;
            if (textBox != null)
            {

                //O que se deseja editar nas TextBox
                textBox.Text = "";
            }
        }
    }

    public static Control[] FlattenHierachy(Control root)
    {
        List<Control> list = new List<Control>();
        list.Add(root);
        if (root.HasControls())
        {
            foreach (Control control in root.Controls)
            {
                list.AddRange(FlattenHierachy(control));
            }
        }
        return list.ToArray();
    }




    public static int GetPagerIndex()
    {
        int iIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["pagina"]);
        return iIndex < 1 ? 1 : iIndex;
    }
    public static string buildSitePagination(int iCount, int iItensPorPagina)
    {

        bool showFirstLast = false;
        bool showPreviewsNext = true;
        string FIRST = "Primeira";
        //string PREVIOUS = "<img src=\"" + BaseURL + "/images/pagSetaEsq.png\" border=\"0\" />";
        //string NEXT = "<img src=\"" + BaseURL + "/images/pagSetaDir.png\" border=\"0\" />";
        string PREVIOUS = "<img src=\"" + MetodosFE.BaseURL + "/images/paginacao.jpg\" alt=\"paginação\" />";
        string NEXT = "<img src=\"" + MetodosFE.BaseURL + "/images/paginacao2.jpg\" alt=\"paginação\" />";
        string LAST = "<<";
        //string style = "color: #000000;font-family: Bradley Hand ITC,Arial;font-size: 18px;font-weight: bold;";
        int iIndex = GetPagerIndex(), iPages = 0;
        string _return = string.Empty,
            _url = HttpContext.Current.Request.Url.AbsoluteUri,
            _linkWithClass = "<a href=\"[url]\" class=\"pagLink\">[text]</a>",
            _link = "<a href=\"[url]\" class=\"pagLink\">[text]</a>",
            _linkAnterior = "<a href=\"[url]\" class=\"ceta_paginacao\">[text]</a>",
            _linkProximo = "<a href=\"[url]\" class=\"ceta_paginacao2\">[text]</a>";

        if (HttpContext.Current.Request.QueryString["pagina"] != null)
            _url = _url.Replace("pagina=" + HttpContext.Current.Request.QueryString["pagina"], "pagina=[index]");
        else
        {
            if (_url.IndexOf("?") != -1)
                _url += "&pagina=[index]";
            else
                _url += "?pagina=[index]";
        }

        iPages = Convert.ToInt32(Math.Ceiling((double)iCount / iItensPorPagina));

        #region Montar ul li
        _return += "<ul class=\"pagination\">";

        //#region show first
        //if (iPages > 1 && showFirstLast)
        //{
        //    if (iIndex != 1)
        //        _return += "<li class=\"semLink\">" + _linkAnterior.Replace("[url]", _url.Replace("[index]", "1")).Replace("[text]", FIRST) + "</li>";
        //    else
        //        _return += "<li class=\"semLink\">" + FIRST + "</li>";
        //}
        //#endregion

        //#region show previews
        //if (iPages > 1 && showPreviewsNext)
        //{
        //    if (iIndex != 1)
        //        _return += "<li class=\"semLink\">" + _linkAnterior.Replace("[url]", _url.Replace("[index]", (iIndex - 1).ToString())).Replace("[text]", PREVIOUS) + "</li>";
        //    else
        //        _return += "<li class=\"semLink\">" + _linkAnterior.Replace("[url]", "").Replace("[text]", PREVIOUS) + "</li>";
        //}
        //#endregion

        //for (int i = (iIndex - 5) > 0 ? (iIndex - 5) : 1; i <= iPages && i < iIndex + 5; i++)
        for (int i = 1; i <= iPages; i++)
        {

            if (iIndex != i)
                _return += "<li class=\"[separator]\">" + _linkWithClass.Replace("[url]", _url.Replace("[index]", i.ToString())).Replace("[text]", i.ToString()) + "</li>";
            else
                _return += "<li class=\"active\"><a class=\"pagAtual\">" + i + "</a></li>";


            _return = _return.Replace("[separator]", "");


            //Barrinhas pra separar

        }

        //#region show next
        //if (iPages > 1 && showPreviewsNext)
        //{
        //    if (iIndex != iPages)
        //        _return += "<li class=\"semLink\">" + _linkProximo.Replace("[url]", _url.Replace("[index]", (iIndex + 1).ToString())).Replace("[text]", NEXT) + "</li>";
        //    else
        //        _return += "<li class=\"semLink\">" + NEXT + "</li>";
        //}
        //#endregion

        //#region show last
        //if (iPages > 1 && showFirstLast)
        //{
        //    if (iIndex != iPages)
        //        _return += "<li class=\"semLink\">" + _linkProximo.Replace("[url]", _url.Replace("[index]", iPages.ToString())).Replace("[text]", LAST) + "</li>";
        //    else
        //        _return += "<li class=\"semLink\">" + _linkProximo.Replace("[url]", "").Replace("[text]", LAST) + "</li>";
        //}
        //#endregion


        _return += "</ul>";
        #endregion

        if (iCount <= iItensPorPagina)
            return null;
        else
        {
            return _return;
        }
    }

    public static void mostraMensagem(string mensagem, string tipo = null)
    {
        if (String.IsNullOrEmpty(tipo))
            tipo = "";
        if (tipo.ToLower() == "sucesso")
        {
            HttpContext.Current.Session["MensagemString"] = "<div class=\"divSucesso\">" + mensagem + "<a href=\"javascript:void(0)\" onClick=\"javascript:$(this).parent().slideUp();\" > <img src=\"" + VirtualPathUtility.ToAbsolute("~/Controle/comum/img/close-button.png") + "\"></a></div>";
        }
        else
        {
            HttpContext.Current.Session["MensagemString"] = "<div class=\"divErro\">" + mensagem + "<a href=\"javascript:void(0)\" onClick=\"javascript:$(this).parent().slideUp();\" > <img src=\"" + VirtualPathUtility.ToAbsolute("~/Controle/comum/img/close-button.png") + "\"></a></div>";
        }
    }

    public static string confereMensagem()
    {
        if (HttpContext.Current.Session["MensagemPostBack"] == null)
        {
            if (HttpContext.Current.Session["MensagemString"] != null)
            {
                string retorno = HttpContext.Current.Session["MensagemString"].ToString();
                HttpContext.Current.Session["MensagemString"] = null;
                return retorno;
            }
        }
        else HttpContext.Current.Session["MensagemPostBack"] = null;
        return null;
    }

    public static string RemoveSpecialCharacters(string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static void recuperaMensagem()
    {
        HttpContext.Current.Session["MensagemPostBack"] = "Teste";
    }

    public static string TipoLoja
    {
        set
        {
            HttpContext.Current.Items["current.tipoloja"] = value;
            HttpCookie myCookie = new HttpCookie("TipoLoja");
            myCookie.Value = value;
            myCookie.Expires = DateTime.Now.AddDays(1d);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        get
        {
            if (HttpContext.Current.Items["current.tipoloja"] != null)
                return HttpContext.Current.Items["current.tipoloja"].ToString();

            if (HttpContext.Current.Request.Cookies["TipoLoja"] == null)
                return "Varejo";
            else
                return HttpContext.Current.Request.Cookies["TipoLoja"].Value;
        }
    }

    public static void limparFormulario(Control control)
    {
        {
            var textbox = control as TextBox;
            if (textbox != null)
                textbox.Text = string.Empty;

            var dropDownList = control as DropDownList;
            if (dropDownList != null)
                dropDownList.SelectedIndex = 0;


            foreach (Control childControl in control.Controls)
            {
                limparFormulario(childControl);
            }
        }

    }
    /// <summary>
    /// Returns the name of the virtual folder where our project lives
    /// </summary>
    /// 
    public static string BaseURL
    {
        get
        {
            try
            {
                string retorno = VirtualPathUtility.ToAbsolute("~/").Replace("ecommerce/","");

                return retorno.Remove(retorno.Length - 1);
            }
            catch
            {
                // This is for design time
                return null;
            }
        }
    }

    public static string BaseURL2
    {
        get
        {
            try
            {
                return string.Format("http://{0}{1}",
                                     HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                                     (VirtualFolder.Equals("/")) ? string.Empty : VirtualFolder);
            }
            catch
            {
                // This is for design time
                return null;
            }
        }
    }

    /// <summary>
    /// Returns the name of the virtual folder where our project lives
    /// </summary>
    /// 
    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }

    public static string adicionaParametro(String valor)
    {
        //var Request = HttpContext.Current.Request;
        string endereco = HttpContext.Current.Request.Url.AbsolutePath.Replace(HttpContext.Current.Request.Url.Authority,"");

        var parametros = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
        parametros.Remove("pagina");

        List<string> partes = endereco.Split('/').ToList();

        partes.Add(valor);

        partes = partes.OrderBy(x => x.Contains("segmento")).ThenBy(x => x.Contains("subsegmento")).ThenBy(x => x.Contains("categoria")).ToList();

        //endereco = "http://" + HttpContext.Current.Request.Url.Authority;

        endereco = "";

        foreach (var item in partes)
            endereco += "/" +  item;

        endereco = endereco.Replace("//", "/");

        endereco = MetodosFE.BaseURL2 + endereco;

        endereco = endereco + (!String.IsNullOrEmpty(parametros.ToString()) ? "?" + parametros.ToString() : "");
        return endereco;
    }

    public static bool existeValor(String valor)
    {
        string endereco = HttpContext.Current.Request.Url.AbsolutePath.Replace(HttpContext.Current.Request.Url.Host, "");

        //if (valores != null)
        {
            if (endereco.Contains(valor))
                return true;
            else return false;
        }
    }


    public static string removeParametro(String valor)
    {//var Request = HttpContext.Current.Request;
        string endereco = HttpContext.Current.Request.Url.AbsolutePath;

        var parametros = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
        parametros.Remove("pagina");

        endereco = endereco.Replace(valor, "").Replace("//", "/");

        //endereco = "http://" + HttpContext.Current.Request.Url.Authority + endereco;
        
        //endereco = endereco.Replace("//", "/");

        endereco = MetodosFE.BaseURL2 + endereco;

        endereco = endereco + (!String.IsNullOrEmpty(parametros.ToString()) ? "?" + parametros.ToString() : "");

        return (endereco);
    }

    public static string montaUrl(string valor)
    {

            if (existeValor(valor))
                return removeParametro(valor).ToLower();
            else return adicionaParametro(valor).ToLower();

    }

    public static string[] GetParametros(string tipoParametro) 
    {
        string endereco = HttpContext.Current.Request.Url.AbsolutePath;
        string[] parametros = endereco.Split('/');

        parametros = parametros.Where(x => Regex.IsMatch(x, "^"+tipoParametro+"*")).ToArray();

        return parametros;
    }

    public static int verificaOrdem(string ordem)
    {
        int ordenacao = 0;

        bool resultadoConversao = Int32.TryParse(ordem, out ordenacao);

        if (resultadoConversao)
        {
            if (ordenacao != null && ordenacao > 0)
            {
                ordenacao = ordenacao;
            }
            else
            {
                ordenacao = 100;
            }
        }
        else
        {
            ordenacao = 100;
        }

        return ordenacao;
    }

    public static string adicionaQueryString(String parametro, String valor)
    {
        var nameValues = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
        nameValues.Remove(parametro);
        nameValues.Add(parametro, valor);
        var endereco = HttpContext.Current.Request.Url.AbsolutePath;
        
        string updatedQueryString = !String.IsNullOrEmpty(nameValues.ToString()) ? "?" + nameValues.ToString() : "";
        endereco = endereco + updatedQueryString;

        return  endereco;
    }

}

