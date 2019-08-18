<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
CodeFile="Produtos.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<%@ Import Namespace="Modelos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<asp:UpdatePanel runat="server" ID="upt" UpdateMode="Always">
<ContentTemplate>



<!--===CONTEÚDO===-->
<div class="fundo-telas" style="background-image: url('<%# MetodosFE.BaseURL %>/assets/img/fundo-sobre.jpg');" data-speed="1.5">
<div class="container padding-telas">
<div class="row">
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 fundo-conteudo padding-produtos no-padding">
<!--TITULO-->
<div class="titulo-blog col-lg-12 col-md-12 col-sm-12 col-xs-12">
    <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
    </div>
    <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 titulo text-center">
        <h1>Nossos Produtos
        </h1>
        <!--MENU PRODUTOS-->
        <div class="menu-produtos">
            <ul>
                <li class="">
                    <a href="#" runat="server" id="linkSeg1" title="">
                        <asp:Literal Text="" ID="segmentoproduto1" runat="server" />
                    </a>
                </li>
                <li runat="server" visible="false" id="separador2">|
                </li>
                <li class="" runat="server" visible="false" id="lista2">
                    <a href="#" runat="server" id="linkSeg2" title="">
                        <asp:Literal Text="" ID="segmentoproduto2" runat="server" />
                    </a>
                </li>
                <li runat="server" visible="false" id="separador3">|
                </li>
                <li class="" runat="server" visible="false" id="lista3">
                    <a href="#" runat="server" id="linkSeg3" title="">
                        <asp:Literal Text="" ID="segmentoproduto3" runat="server" />
                    </a>
                </li>
                <li runat="server" visible="false" id="separador4">|
                </li>
                <li class="" runat="server" visible="false" id="lista4">
                    <a href="#" runat="server" id="linkSeg4" title="">
                        <asp:Literal Text="" ID="segmentoproduto4" runat="server" />
                    </a>
                </li>
            </ul>
        </div>
        <!--fim MENU PRODUTOS-->
    </div>

    <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
    </div>
</div>
<!--fim TITULO-->
<!--PRODUTOS-->
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
    <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
    </div>

    <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">

        <asp:Repeater runat="server" ID="repProdutos">
            <ItemTemplate>

                <!--PRODUTO-->
                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-6 produto produtosLista">

                    <div onclick="getProduct(this)">
                        <input type="hidden" name="name" value="<%# (((Modelos.Produto)Container.DataItem)).Indisponivel %>" />
                        <div class="img-produto" data-toggle="modal" data-target="#p<%# (((Modelos.Produto)Container.DataItem)).Id %>" style="cursor: pointer;">
                            <img src="<%# GetImg(((Modelos.Produto)Container.DataItem)) %>" class="img-responsive imgprodutosLista"
                                alt="<%# (((Modelos.Produto)Container.DataItem)).Nome %>" title="<%# (((Modelos.Produto)Container.DataItem)).Nome %>" />
                        </div>
                        <div class="texto-produto tamanho-46">
                            <%# (((Modelos.Produto)Container.DataItem)).Nome %><%# (((Modelos.Produto)Container.DataItem)).Indisponivel == true? "<h6 style='color:#35be05; float:right; margin-right:15px'>(Indisponível)</h6>":"" %>
                        </div>
                        <div class="texto-produto tamanho-46" style="text-decoration:line-through; color:#808080; font-size:16px; font-weight:bolder">
                            De R$: <%# (((Modelos.Produto)Container.DataItem)).Preco.ValorSemPromocao %>
                        </div>
                        <div class="texto-produto tamanho-46" style="font-size:24px;font-weight:bolder; color:#206543">
                            Por R$: <%# (((Modelos.Produto)Container.DataItem)).Preco.Valor %>
                        </div

                        <input type="hidden" name="name" value="<%# (((Modelos.Produto)Container.DataItem)).Descricao %>" />
                        <div class="link-modal">
                            <a data-toggle="modal" data-target="#p<%# (((Modelos.Produto)Container.DataItem)).Id %>" class="linkLeiaMais" style="cursor: pointer;">Leia mais<img src="<%# MetodosFE.BaseURL %>/assets/img/leia-mais.png" class="img-responsive" alt="" title="" />
                            </a>

                        </div>
                        <div class="col-md-12" id="btnAdicionaraoCarrinho" style="padding-left: 0px; margin-top: 20px">
                            <asp:LinkButton Text="Adicionar ao Carrinho" OnClick="btnAdd_ServerClick"
                                CommandArgument="<%# (((Modelos.Produto)Container.DataItem)).Chave %>" runat="server"
                                ID="btnAdd" CssClass="botaoProdutos" />
                        </div>

                        
                        
                        <%-- MODAL INDISPONIVEL --%>
                        <!-- Modal -->
                        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                          <div class="modal-dialog" role="document">
                            <div class="modal-content">
                              <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Mensagem</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                              </div>
                              <div class="modal-body">
                                Produto Indisponível
                              </div>
                              <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btnfechar" data-dismiss="modal">Fechar</button>
                              </div>
                            </div>
                          </div>
                        </div>
                        <%-- MODAL INDISPONIVEL --%>
                                  
                        <!--MODAL-->
                        <div class="modal-produto" id="modal">

                            <div class="modal fade modal-ocultar" id="p<%# (((Modelos.Produto)Container.DataItem)).Id %>" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="fechar-modal hidden-lg hidden-md" data-dismiss="modal" aria-hidden="true">
                                                Fechar
                                            <img src="<%# MetodosFE.BaseURL %>/assets/img/leia-mais.png" class="img-responsive" alt="" title="" />
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">

                    <!--SLIDE-->
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 img-modal">
                        <div id="myCarousel" class="carousel slide carousel-v1 slide-obras">
                            <div class="carousel-inner">
                                <asp:Repeater runat="server" ID="repFilho" DataSource="<%# GetImgLista(((Modelos.Produto)Container.DataItem)) %>">
                                    <ItemTemplate>
                                        <div class="item active">
                                            <img src="<%# Container.DataItem %>" title="" alt="" class="img-responsive img-produto-modal">
                                        </div>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <div class="item">
                                            <img src="<%# Container.DataItem %>" title="" alt="" class="img-responsive img-produto-modal">
                                        </div>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </div>

                            <div class="carousel-arrow">
                                <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                    <i class="">
                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/seta-esq.png" class="img-responsive" alt="" title="" />
                                    </i>
                                </a>
                                <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                    <i class="">
                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/seta-dir.png" class="img-responsive" alt="" title="" />
                                    </i>
                                </a>
                            </div>
                        </div>

                                                </div>
                                                <!--fim SLIDE-->



                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <button type="button" class="fechar-modal hidden-sm hidden-xs" data-dismiss="modal" aria-hidden="true">
                                                        Fechar
                                                    <img src="<%# MetodosFE.BaseURL %>/assets/img/leia-mais.png" class="img-responsive" alt="" title="" />
                                                    </button>
                                                    <div class="titulo-modal">
                                                        <h2 id="nomeProdutoModal">
                                                            <%# (((Modelos.Produto)Container.DataItem)).Nome %>
                                                        </h2>
                                                    </div>
                                                    <div class="texto-modal" id="descricaoProdutoModal">
                                                        <%# (((Modelos.Produto)Container.DataItem)).Descricao %>
                                                    </div>
                                                    <br />
                                                     <div class="texto-modal" style="color:#808080; font-size:16px; font-weight:bolder">
                                                        Referência: <%# (((Modelos.Produto)Container.DataItem)).Referencia %>
                                                    </div> 
                                                    <div class="col-lg-2 col-md-4 col-sm-3 col-xs-12 produto-3" style="color:#808080; font-size:16px; font-weight:bolder; width: 110px">
                                                        Tamanho: <asp:DropDownList runat="server" ID="ddlTamanho" CssClass="form-control" Height="40px" Width="90px" Style="margin-left: 2px">
                                                                 </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-4 col-sm-3 col-xs-12 produto-3" 
                                                        style="color:#808080; font-size:16px; font-weight:bolder; width: 83px; margin-left: 10px">
                                                        Cor: <asp:DropDownList runat="server" ID="ddlCor" CssClass="form-control" 
                                                            Height="40px" Width="90px" Style="padding-right: 4px">
                                                             </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-4 col-sm-3 col-xs-12 produto-3 produtoMarca">
                                                        <div class="span-marca">Marca:
                                                            </div>
                                                            <br />
                                                        <asp:Literal Text="" ID="litMarca" runat="server" />
                                                    </div>

                                                    <div class="col-lg-12 col-md-4 col-sm-3 col-xs-12 produto-3 preco-sem-valor">
                                                        De R$: <%# (((Modelos.Produto)Container.DataItem)).Preco.ValorSemPromocao %>
                                                    </div>
                                                    <div class="col-lg-12 col-md-4 col-sm-3 col-xs-12 produto-3 preco-sem-valor preco-valor">
                                                        Por R$: <%# (((Modelos.Produto)Container.DataItem)).Preco.Valor %>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--fim MODAL-->
                    </div>
                </div>
                <!--fim PRODUTO-->

            </ItemTemplate>
        </asp:Repeater>



        <!--PAGINAÇÃO-->
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 paginacao margin-bottom-30">
            <div class="text-center">
                <ul class="pagination">
                    <asp:Literal runat="server" ID="litPaginacao" />
                </ul>
            </div>
        </div>
        <!--fim PAGINAÇÃO-->

        <%--   <!--PAGINAÇÃO-->
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-30">

                    <div class="paginacao">
                        <ul class="">
                            <li class="">
                                <a href="#" title="">< Back
                                </a>
                            </li>

                            <li class="active">
                                <a href="#" title=""><asp:Literal runat="server" ID="litPaginacao" />
                                </a>
                            </li>


                            <li class="">
                                <a href="#" title="">Next >
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--fim PAGINAÇÃO-->--%>
        
    </div>

    <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
    </div>
</div>
<!--fim PRODUTOS-->
</div>
</div>
</div>
</div>
<!--===fim CONTEÚDO===-->
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
