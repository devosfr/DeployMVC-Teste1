

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using ImageResizer;
using Modelos;

/// <summary>
/// Summary description for MyPage
/// </summary>
public partial class uplImage : System.Web.UI.UserControl
{

    public int TamFotoGrW
    {
        get
        {
            if (ViewState["TamFotoGrW"] == null) ViewState["TamFotoGrW"] = 0;
            return (Int32)ViewState["TamFotoGrW"];
        }
        set { ViewState["TamFotoGrW"] = value; }
    }
    public int TamFotoPqW
    {
        get
        {
            if (ViewState["TamFotoPqW"] == null) ViewState["TamFotoPqW"] = 0;
            return (Int32)ViewState["TamFotoPqW"];
        }
        set { ViewState["TamFotoPqW"] = value; }
    }
    public int TamFotoGrH
    {
        get
        {
            if (ViewState["TamFotoGrH"] == null) ViewState["TamFotoGrH"] = 0;
            return (Int32)ViewState["TamFotoGrH"];
        }
        set { ViewState["TamFotoGrH"] = value; }
    }
    public int TamFotoPqH
    {
        get
        {
            if (ViewState["TamFotoPqH"] == null) ViewState["TamFotoPqH"] = 0;
            return (Int32)ViewState["TamFotoPqH"];
        }
        set { ViewState["TamFotoPqH"] = value; }
    }

    public int Configuracao
    {
        get
        {
            if (ViewState["ConfiguracaoUpl"] == null) ViewState["ConfiguracaoUpl"] = 1;
            return (Int32)ViewState["ConfiguracaoUpl"];
        }
        set { ViewState["ConfiguracaoUpl"] = value; }
    }
    public string Cor
    {
        get
        {
            if (ViewState["CorUpl"] == null) ViewState["CorUpl"] = "";
            return (string)ViewState["CorUpl"];
        }
        set { ViewState["CorUpl"] = value; }
    }
    public int Qualidade
    {
        get
        {
            if (ViewState["UplQualidade"] == null) ViewState["UplQualidade"] = 80;
            return (Int32)ViewState["UplQualidade"];
        }
        set { ViewState["UplQualidade"] = value; }
    }


    public int QtdeFotos
    {
        get
        {
            if (ViewState["QtdeFotos"] == null) ViewState["QtdeFotos"] = 0;
            return (Int32)ViewState["QtdeFotos"];
        }
        set { ViewState["QtdeFotos"] = value; }
    }


    public int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }

    public List<String> fotosSemCodigo
    {
        get
        {
            if (ViewState["fotosSemCodigo"] == null) ViewState["fotosSemCodigo"] = new List<String>();
            return (List<String>)ViewState["fotosSemCodigo"];
        }
        set { ViewState["fotosSemCodigo"] = value; }
    }
    //Destino das fotos de alta qualidade
    public string destinoHQ
    {
        get
        {
            if (ViewState["destinoHQ"] == null) ViewState["destinoHQ"] = "";
            return (String)ViewState["destinoHQ"];
        }
        set { ViewState["destinoHQ"] = value; }
    }
    //Destino das fotos de baixa qualidade
    public string destinoLQ
    {
        get
        {
            if (ViewState["destinoLQ"] == null) ViewState["destinoLQ"] = "";
            return (String)ViewState["destinoLQ"];
        }
        set { ViewState["destinoLQ"] = value; }
    }

    public static string localHQ
    {
        get
        {
            return BaseURLStatic + "/ImagensHQ/";
        }
    }
    //Destino das fotos de baixa qualidade
    public static string localLQ
    {
        get
        {
            return BaseURLStatic + "/ImagensLQ/";
        }

    }
    public static string imgSemImagem
    {
        get
        {
            return BaseURLStatic + "/images/Popup/SemImagem.jpg";
        }
    }
    public static string diretorioHQ
    {
        get
        {
            if ((String)HttpContext.Current.Cache["diretorioHQ"] == null)
                HttpContext.Current.Cache["diretorioHQ"] = HttpContext.Current.Server.MapPath("~/ImagensHQ");
            return (String)HttpContext.Current.Cache["diretorioHQ"];
        }
    }
    //Destino das fotos de baixa qualidade
    public static string diretorioLQ
    {
        get
        {
            if ((String)HttpContext.Current.Cache["diretorioLQ"] == null)
                HttpContext.Current.Cache["diretorioLQ"] = HttpContext.Current.Server.MapPath("~/ImagensLQ");
            return (String)HttpContext.Current.Cache["diretorioLQ"];
        }
    }

    public void reset()
    {
        QtdeFotos = 5;
        TamFotoGrW = 700;
        TamFotoPqW = 120;
        TamFotoGrH = 500;
        TamFotoPqH = 90;
        destinoHQ = Server.MapPath("~/ImagensHQ");
        destinoLQ = Server.MapPath("~/ImagensLQ");
        Qualidade = 80;
        Configuracao = 2;
    }

    public void LimparTudo()
    {

        if (fotosSemCodigo.Count > 0)
        {
            foreach (string nome in fotosSemCodigo)
                ExcluiImagemTemp(nome);
        }
        fotosSemCodigo = null;
        Codigo = 0;

    }

    public void setConfiguracoes(UploadTela uplConfig)
    {
        if (uplConfig == null)
        {
            reset();
        }
        else
        {
            QtdeFotos = uplConfig.QtdeFotos;
            TamFotoGrW = uplConfig.TamFotoGrW;
            TamFotoPqW = uplConfig.TamFotoPqW;
            TamFotoGrH = uplConfig.TamFotoGrH;
            TamFotoPqH = uplConfig.TamFotoPqH;
            Qualidade = uplConfig.Qualidade;
            Cor = uplConfig.Cor;
            Configuracao = uplConfig.Configuracao;
        }
    }

    public void ExcluiImagemTemp(string endereco)
    {

        //FotoProdutoVO fotos = new FotoProdutoVO();
        //List<FotoProdutoVO> fotos = FotosProdutosBO.FindAll(nomeFoto:endereco);
        //FotoProdutoVO foto = fotos[0];


        //Informações do arquivo

        String photos = Server.MapPath("~/Photos/" + endereco.Replace(BaseURL + "/Photos/", ""));
        if (endereco != "Vazio.gif")
        {
            File.Delete(destinoLQ + "\\" + endereco.Replace(BaseURL + "/Photos/", ""));

            //destinoLQ.Text = Server.MapPath("Photos");
            //texto = destinoLQ.Text;
            //destinoLQ.Text = "";
            //destinoLQ.Text = texto.Replace("\\controle\\cadastro", "");

            //File.Delete(destinoLQ.Text + "\\" + nomeFoto);
            //fotosSemCodigo.Remove(endereco);
        }
        //FotosProdutosBO.Delete(foto);
    }


    public static void Resize(string srcPath, string destPath, int nWidth, int nHeight, int qualidade = 80, string bgcolor = null, int mode = 0)
    {


        string configuracaoFundo = "";
        if (!string.IsNullOrEmpty(bgcolor))
            configuracaoFundo = "&bgcolor=" + bgcolor;

        ImageJob image = null;
        //Resize the image
        string configuracao = null;

        switch (mode)
        {
            case 1: configuracao = "width=" + nWidth + "&height=" + nHeight + "&scale=both&anchor=middlecenter" + configuracaoFundo;
                break;
            case 2:
                
                var info = ImageBuilder.Current.LoadImageInfo(srcPath, null);
                decimal width = Convert.ToDecimal(info["source.width"]);
                decimal height = Convert.ToDecimal(info["source.height"]);
                if (width < nWidth || height < nHeight)
                {
                    decimal diferencaWidth = nWidth / width;
                    decimal diferencaHeight = nHeight / height;

                    if (diferencaWidth > diferencaHeight)
                    {
                        image = new ImageJob(srcPath, srcPath, new Instructions("width=" + nWidth + "&scale=both"));
                        image.Build();

                        image = new ImageJob(srcPath, destPath, new Instructions("width=" + nWidth + "&height=" + nHeight + "&mode=crop&chop=auto&quality=80" + configuracaoFundo));
                        image.Build();
                    }
                    else
                    {
                        image = new ImageJob(srcPath, srcPath, new Instructions("height=" + nHeight + "&scale=both"));
                        image.Build();
                        //image.Instructions = new Instructions()
                        image = new ImageJob(srcPath, destPath, new Instructions("width=" + nWidth + "&height=" + nHeight + "&mode=crop&crop=auto&quality=80" + configuracaoFundo));
                        image.Build();
                    }

                }
                else
                {
                    decimal diferencaWidth = nWidth / width;
                    decimal diferencaHeight = nHeight / height;

                    if (diferencaWidth > diferencaHeight)
                    {
                        image = new ImageJob(srcPath, srcPath, new Instructions("width=" + nWidth + "&scale=both"));
                        image.Build();

                        image = new ImageJob(srcPath, destPath, new Instructions("width=" + nWidth + "&height=" + nHeight + "&mode=crop&crop=auto&quality=80" + configuracaoFundo));
                        image.Build();
                    }
                    else
                    {
                        image = new ImageJob(srcPath, srcPath, new Instructions("height=" + nHeight + "&scale=both"));
                        image.Build();



                        image = new ImageJob(srcPath, destPath, new Instructions("width=" + nWidth + "&height=" + nHeight + "&mode=crop&crop=auto&quality=80" + configuracaoFundo));
                        image.Build();
                    }
                }
                break;
            case 3: configuracao = "";
                break;
            default:
                configuracao = "width=" + nWidth + "&height=" + nHeight + "&mode=Crop&scale=both&anchor=middlecenter" + configuracaoFundo;
                break;


            //Upload the byte array to SQL: ms.ToArray();
        }
        if (mode != 2)
        {
            image = new ImageJob(srcPath, destPath, new Instructions(configuracao));
            image.Build();
        }

    }

    protected static string BaseURLStatic
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

    protected string BaseURL
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
    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }

}