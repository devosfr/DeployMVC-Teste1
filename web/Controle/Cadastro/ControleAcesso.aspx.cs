using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Usuario : System.Web.UI.Page
{
    private Repository<UsuarioVO> repoUsuario
    {
        get
        {
            return new Repository<UsuarioVO>(NHibernateHelper.CurrentSession);
        }
    }
    //private Repository<LojaVO> repoLoja
    //{
    //    get
    //    {
    //        return new Repository<LojaVO>(NHibernateHelper.CurrentSession);
    //    }
    //}

    private Repository<GrupoDePaginasVO> repoGrupoPaginas
    {
        get
        {
            return new Repository<GrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<PermissaoGrupoDePaginasVO> repoPermissaoGrupoPaginas
    {
        get
        {
            return new Repository<PermissaoGrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }

    //private Repository<PermissaoLojaVO> repoPermissaoLoja
    //{
    //    get
    //    {
    //        return new Repository<PermissaoLojaVO>(NHibernateHelper.CurrentSession);
    //    }
    //}

    private Repository<PermissaoVO> repoPermissao
    {
        get
        {
            return new Repository<PermissaoVO>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<PaginaDeControleVO> repoPaginaDeControle
    {
        get
        {
            return new Repository<PaginaDeControleVO>(NHibernateHelper.CurrentSession);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TelasBD.criaPaginasAdministracao();
            carregaUsuarios();
            if(ddlUsuario.Items.Count > 0)
                montaArvore();
        }
        else
        {
        }

    }
    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    protected void carregaUsuarios()
    {
        IList<UsuarioVO> usuarios = repoUsuario.FilterBy(x => x.tipo == "AD").ToList();
        ddlUsuario.Items.Clear();

        for (int i = 0; i < usuarios.Count; i++)
        {
            ddlUsuario.Items.Add(new ListItem(usuarios[i].login, usuarios[i].Id.ToString()));
        }
    }

    public void montaArvore()
    {
       // IList<LojaVO> lojas = repoLoja.All().ToList();
        List<GrupoDePaginasVO> grupos = repoGrupoPaginas.FilterBy(x => x.nome != "Controle de Páginas Fixas").ToList();
        UsuarioVO user = repoUsuario.FindBy(Convert.ToInt32(ddlUsuario.SelectedValue));


        tvPermissoes.Nodes.Clear();

        //foreach (LojaVO loja in lojas)
        //{
        //    TreeNode nodoPai = new TreeNode();
        //    nodoPai.Text = loja.nome;
        //    nodoPai.Value = loja.id.ToString();
        //    nodoPai.ToolTip = "Loja";
        //    List<PermissaoLojaVO> permissoesLojas = repoPermissaoLoja.FilterBy(x=>x.loja!=null && x.loja.id == loja.id && x.usuario!=null && x.usuario.id == user.id).ToList();
        //    if (permissoesLojas.Count > 0)
        //        nodoPai.Checked = true;

            foreach (GrupoDePaginasVO grupo in grupos)
            {
                TreeNode nodo = new TreeNode();
                nodo.Text = grupo.nome;
                nodo.Value = grupo.Id.ToString();
                nodo.ToolTip = "Grupo de Paginas";

                PermissaoGrupoDePaginasVO permissoaoGrupo = repoPermissaoGrupoPaginas.FindBy(x => x.grupoDePaginas != null && x.grupoDePaginas.Id == grupo.Id && x.usuario != null && x.usuario.Id == user.Id);
                if (permissoaoGrupo != null)
                    nodo.Checked = true;

                List<PaginaDeControleVO> paginas = repoPaginaDeControle.FilterBy(x => x.grupoDePaginas != null && x.grupoDePaginas.Id == grupo.Id).ToList();

                foreach (PaginaDeControleVO pagina in paginas)
                {
                    TreeNode nodoFilho = new TreeNode();
                    nodoFilho.Text = pagina.nome;
                    nodoFilho.Value = pagina.Id.ToString();

                    

                    //TreeNode visualizar = new TreeNode();
                    //visualizar.Text = "Visualizar";
                    //visualizar.Value = "Visualizar";

                    //TreeNode modificar = new TreeNode();
                    //modificar.Text = "Modificar";
                    //modificar.Value = "Modificar";

                    //TreeNode excluir = new TreeNode();
                    //excluir.Text = "Excluir";
                    //excluir.Value = "Excluir";

                    List<PermissaoVO> permissao = repoPermissao.FilterBy(x => x.paginaDeControle != null && x.paginaDeControle.Id == pagina.Id && x.usuario != null && x.usuario.Id == user.Id ).ToList();
                    if (permissao.Count > 0)
                    {
                        nodoFilho.Checked = true;
                        //PermissaoVO perm = permissao[0];
                        //if (perm.ver == "V")
                        //    visualizar.Checked = true;
                        //if (perm.editar == "V")
                        //    modificar.Checked = true;
                        //if (perm.excluir == "V")
                        //    excluir.Checked = true;
                    }
                    //nodoFilho.ChildNodes.Add(visualizar);
                    //nodoFilho.ChildNodes.Add(modificar);
                    //nodoFilho.ChildNodes.Add(excluir);

                    nodoFilho.CollapseAll();
                    nodoFilho.ToolTip = "Pagina";
                    nodo.ChildNodes.Add(nodoFilho);

                }
                nodo.CollapseAll();
                tvPermissoes.Nodes.Add(nodo);

           // }
            //tvPermissoes.Nodes.Add(nodoPai);
        }
    }

    public void atualizaPermissoes()
    {
        UsuarioVO logado = repoUsuario.FindBy(Convert.ToInt32(ddlUsuario.SelectedValue));
        repoPermissaoGrupoPaginas.Delete(repoPermissaoGrupoPaginas.FilterBy(x => x.usuario != null && x.usuario.Id == logado.Id));
        repoPermissao.Delete(repoPermissao.FilterBy(x => x.usuario != null && x.usuario.Id == logado.Id));
        //repoPermissao.Delete(repoPermissao.FilterBy(x => x.usuario != null && x.usuario == logado));
        //repoPermissaoLoja.Delete(repoPermissaoLoja.FilterBy(x => x.usuario != null && x.usuario.id == logado.id));
        foreach (TreeNode node in tvPermissoes.CheckedNodes)
        {
            atualizaPermissaoNodo(node);
        }

    }




    public void atualizaPermissaoNodo(TreeNode nodo)
    {
        UsuarioVO logado = repoUsuario.FindBy(Convert.ToInt32(ddlUsuario.SelectedValue));
        switch (nodo.ToolTip)
        {
            //case "Loja":


            //    int idLoja = Convert.ToInt32(nodo.Value);
                
            //    LojaVO loja = repoLoja.FindBy(idLoja);

            //    PermissaoLojaVO permissaoLoja = new PermissaoLojaVO();
            //    permissaoLoja.loja = loja;
            //    permissaoLoja.usuario = logado;

            //    repoPermissaoLoja.Add(permissaoLoja);



            //    break;
            case "Grupo de Paginas":


                int idGrupoDePaginas = Convert.ToInt32(nodo.Value);

                PermissaoGrupoDePaginasVO permissaoGrupo = new PermissaoGrupoDePaginasVO();

                permissaoGrupo.grupoDePaginas = repoGrupoPaginas.FindBy(idGrupoDePaginas);
                //permissaoGrupo.loja = repoLoja.FindBy(Convert.ToInt32(nodo.Parent.Value));
                permissaoGrupo.usuario = logado;
                repoPermissaoGrupoPaginas.Add(permissaoGrupo);



                break;
            case "Pagina":


                int idPagina = Convert.ToInt32(nodo.Value);
                PermissaoVO permissao = new PermissaoVO();

                PaginaDeControleVO pagina = repoPaginaDeControle.FindBy(idPagina);
                

                permissao.paginaDeControle = pagina;
                permissao.usuario = logado;
                //permissao.loja = repoLoja.FindBy(Convert.ToInt32(nodo.Parent.Parent.Value));

                //foreach (TreeNode node in nodo.ChildNodes)
                //{
                //    if (node.Text == "Visualizar")
                //    {
                //        if (node.Checked)
                //        {
                //            permissao.ver = "V";
                //        }
                //        else
                //            permissao.ver = "F";
                //    }
                //    if (node.Text == "Modificar")
                //    {
                //        if (node.Checked)
                //        {
                //            permissao.editar = "V";
                //        }
                //        else
                //            permissao.editar = "F";
                //    }
                //    if (node.Text == "Excluir")
                //    {
                //        if (node.Checked)
                //        {
                //            permissao.excluir = "V";
                //        }
                //        else
                //            permissao.excluir = "F";
                //    }


                //}
                repoPermissao.Add(permissao);
                //PermissoesBO.Insert(permissao);

                break;
        }
    }


    protected void btnAtualizar_Click(object sender, EventArgs e)
    {
        try
        {
            atualizaPermissoes();
            MetodosFE.mostraMensagem("Permissões atualizadas com sucesso.", "sucesso");
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }
    protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            montaArvore();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
        }
    }
}
