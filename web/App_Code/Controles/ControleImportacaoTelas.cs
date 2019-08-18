using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for ControleImportacaoTelas
/// </summary>
public class ControleImportacaoTelas
{

    private static Repository<Tela> RepositorioTela
    {
        get
        {
            return new Repository<Tela>(NHibernateHelper.CurrentSession);
        }
    }
    private static Repository<UploadTela> RepositorioUpload
    {
        get
        {
            return new Repository<UploadTela>(NHibernateHelper.CurrentSession);
        }
    }
    private static Repository<GrupoDePaginasVO> RepositorioGrupoDePaginas
    {
        get
        {
            return new Repository<GrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }
    private static Repository<PaginaDeControleVO> RepositorioPaginaDeControle
    {
        get
        {
            return new Repository<PaginaDeControleVO>(NHibernateHelper.CurrentSession);
        }
    }


    public static void loadConfiguracoesToDictionary(Stream arquivo)
    {
        try
        {
             XmlDocument ArquivoDeConfiguracoes = new XmlDocument();
            ArquivoDeConfiguracoes.Load(arquivo);

                XmlNodeList grupos = ArquivoDeConfiguracoes.SelectSingleNode("settings").FirstChild.ChildNodes;

                IList<Tela> telas = RepositorioTela.All().ToList();

                //IList<Tela> telasExcluir = telas;

                foreach (XmlNode grupo in grupos)
                {
                    InsereGrupo(grupo);
                }
            
        }
        catch (Exception ex)
        {
            throw new Exception("Problemas ao carregar configurações: " + ex.Message + " - " + ex.InnerException);
        }
    }


    public static void InsereGrupo(XmlNode grupoXML)
    {
        
        XmlNodeList telasXML = grupoXML.ChildNodes;

        IList<Tela> telas = new List<Tela>();

        string nomeGrupo = grupoXML.Attributes.GetNamedItem("nome").InnerXml;

        GrupoDePaginasVO grupo = RepositorioGrupoDePaginas.FindBy(x=>x.nome.ToLower().Equals(nomeGrupo.ToLower()));

        if(grupo==null)
        {
            grupo = new GrupoDePaginasVO();
            grupo.nome = nomeGrupo;
            RepositorioGrupoDePaginas.Add(grupo);
        }

                foreach (XmlNode telaXML in telasXML)
                {
                    telas.Add(GetTelaFromXML(telaXML));
                }

                foreach (var tela in telas)
                {


                    PaginaDeControleVO pagina = RepositorioPaginaDeControle.FindBy(x => x.nome.ToLower().Equals(tela.nome.ToLower()));
                    if (pagina == null) { 
                        pagina = new PaginaDeControleVO();
                        pagina.nome = tela.nome;
                        RepositorioPaginaDeControle.Add(pagina);
                    }
                    pagina.grupoDePaginas = grupo;
                    pagina.pagina = MetodosFE.BaseURL + "/Controle/Cadastro/" + grupo.Id + "/" + pagina.Id;
                    RepositorioPaginaDeControle.Update(pagina);
                    tela.pagina = pagina;
                    if (tela.Id == 0)
                        RepositorioTela.Add(tela);
                    else
                        RepositorioTela.Update(tela);
                }
    }

     public static Tela GetTelaFromXML(XmlNode telaXML)
    {
        string nomeTela = telaXML.Attributes.GetNamedItem("nome").InnerXml;

         Tela tela = RepositorioTela.FindBy(x=>x.nome.ToLower().Equals(nomeTela));

         if(tela==null)
         {
            tela = new Tela();
            tela.nome = nomeTela;
         }
         else
         {
            tela.campos.Clear();
         }

         bool multiTela = Convert.ToBoolean(telaXML.Attributes.GetNamedItem("multidocumento").InnerXml);
         tela.multiplo = multiTela;
         //string nomeGrupo = telaXML.Attributes.GetNamedItem("grupo").InnerXml;
         //GrupoDePaginasVO grupo = RepositorioGrupoDePaginas.FindBy(x=>x.nome.ToLower().Equals(nomeGrupo.ToLower()));
         //if(grupo!=null)
         //{
         //   grupo = new GrupoDePaginasVO();
         //    grupo.nome  = nomeGrupo;
         //    RepositorioGrupoDePaginas.Add(grupo);
         //}

        //enderecoWeb = telas.Attributes.GetNamedItem("enderecoWeb") == null ? "" : telas.Attributes.GetNamedItem("enderecoWeb").InnerXml;


        foreach (XmlNode NodoCampo in telaXML.ChildNodes)
        {
            if (NodoCampo.Name == "campo")
            {
                CampoTela campo = GetCampoTela(NodoCampo);
                tela.campos.Add(campo);
            }
            if (NodoCampo.Name == "uplFoto")
            {
                UploadTela upload = GetUploadTela(NodoCampo);
                RepositorioUpload.Add(upload);
                tela.upload = upload;
            }
        }

        //if(tela.Id==0)
        //    RepositorioTela.Add(tela);
        //else
        //    RepositorioTela.Update(tela);

         return tela;
    }

    public static CampoTela GetCampoTela(XmlNode campoXML)
    {
        CampoTela campo = new CampoTela();

        campo.nome = campoXML.Attributes.GetNamedItem("nome").InnerXml;
        campo.destino = campoXML.Attributes.GetNamedItem("destino").InnerXml;
        if(campoXML.Attributes.GetNamedItem("classe")!=null)
            campo.classe = campoXML.Attributes.GetNamedItem("classe").InnerXml;

        return campo;
    }

    public static UploadTela GetUploadTela(XmlNode uplXML)
    {
        UploadTela upload = new UploadTela();
                if (uplXML != null)
            if (uplXML.Name == "uplFoto")
            {
                foreach (XmlAttribute atributo in uplXML.Attributes)
                {
                    if (atributo.Name == "QtdeFotos")
                        upload.QtdeFotos = Convert.ToInt32(atributo.Value);
                    if (atributo.Name == "TamFotoGrW")
                        upload.TamFotoGrW = Convert.ToInt32(atributo.Value);
                    if (atributo.Name == "TamFotoPqW")
                        upload.TamFotoPqW = Convert.ToInt32(atributo.Value);
                    if (atributo.Name == "TamFotoGrH")
                        upload.TamFotoGrH = Convert.ToInt32(atributo.Value);
                    if (atributo.Name == "TamFotoPqH")
                        upload.TamFotoPqH = Convert.ToInt32(atributo.Value);
                    if (atributo.Name == "Configuracao")
                        upload.Configuracao = Convert.ToInt32(atributo.Value);
                    if (atributo.Name == "Qualidade")
                        upload.Qualidade = Convert.ToInt32(atributo.Value);
                    if (atributo.Name == "Cor")
                        upload.Cor = atributo.Value;
                }
            }
        return upload;
    }
    
       
}