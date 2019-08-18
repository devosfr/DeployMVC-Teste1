<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Linq;
using NHibernate.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public class Upload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    //public static int idCor = 0;
    public UploadTela objUpload { get; set; }

    private Repository<ImagemProduto> repoFotoProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Produto> repoProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        try
        {
            int idProduto = Convert.ToInt32(context.Request.Form["idProduto"]);
            //objUpload = Configuracoes.getTela(context.Request.Form["nomeTela"]).uplFoto;

            objUpload = new UploadTela();
            objUpload.QtdeFotos = Convert.ToInt32(context.Request.Form["QtdFotos"]);
            objUpload.TamFotoGrW = Convert.ToInt32(context.Request.Form["TamWidthG"]);
            objUpload.TamFotoGrH = Convert.ToInt32(context.Request.Form["TamHeightG"]);
            objUpload.TamFotoPqW = Convert.ToInt32(context.Request.Form["TamWidthP"]);
            objUpload.TamFotoPqH = Convert.ToInt32(context.Request.Form["TamHeightP"]);
            
            objUpload.Qualidade = Convert.ToInt32(context.Request.Form["Qualidade"]);
            objUpload.Cor = (context.Request.Form["Cor"]);
            objUpload.Configuracao = Convert.ToInt32(context.Request.Form["Configuracao"]);

            IList<ImagemProduto> colecaofotos = repoFotoProduto.All().Where(x => x.produto.Id == idProduto).ToList();
            HttpPostedFile postedFile = context.Request.Files["file"];
            
            if (colecaofotos.Count < objUpload.QtdeFotos)
            {

                string savepath = "";
                string tempPath = "";
                tempPath = HttpContext.Current.Server.MapPath("~/userfiles/");
                savepath = tempPath;
                string filename = postedFile.FileName;
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

                postedFile.SaveAs(savepath + @"\" + filename);
                
                if (colecaofotos.Count > 0)
                    upload(idProduto, savepath + @"\" + filename, filename, colecaofotos.Last().Id + 1);
                else
                    upload(idProduto, savepath + @"\" + filename, filename);
                
                context.Response.Write(js.Serialize(tempPath + "/" + filename));
                context.Response.StatusCode = 200;
            }
            else
            {
                throw new Exception("Arquivo " + @postedFile.FileName + ": Número máximo de imagens atingido(" + objUpload.QtdeFotos + ").");
            }

        }

        catch (Exception ex)
        {
            context.Response.StatusCode = 200;
            context.Response.Write(js.Serialize("Erro: " + ex.Message));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public void upload(int idCodigo, string tmpFoto, string nome, int contagem = 1)
    {


        if (idCodigo != 0)
        {
            int idProduto = idCodigo;

            Produto produto = repoProduto.FindBy(idCodigo);

            FileInfo arquivo = new FileInfo(tmpFoto);

            ImagemProduto foto = new ImagemProduto();

            foto.produto = produto;
            foto.Ordem = 0;
            repoFotoProduto.Add(foto);
            foto.Nome = idProduto + "-" + produto.Chave + "-" + foto.Id + arquivo.Extension;
            repoFotoProduto.Update(foto);
            
            //Processo de upload
            uplImage.Resize(tmpFoto, uplImage.diretorioHQ + "\\" + foto.Nome, objUpload.TamFotoGrW, objUpload.TamFotoGrH, objUpload.Qualidade, objUpload.Cor, objUpload.Configuracao);
            uplImage.Resize(tmpFoto, uplImage.diretorioLQ + "\\" + foto.Nome, objUpload.TamFotoPqW, objUpload.TamFotoPqH, objUpload.Qualidade, objUpload.Cor, objUpload.Configuracao);

            if (File.Exists(tmpFoto))
                File.Delete(tmpFoto);

            


        }
    }
}
