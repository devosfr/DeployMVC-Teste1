<%@ Page Language="C#" MasterPageFile="~/MasterDetalheProduto.master" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="detalhe-produto.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--      <asp:UpdatePanel runat="server" ID="upt" UpdateMode="Always">
        <ContentTemplate>--%>
    <link href="../css/jquery.jqzoom.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.min.css" />
    <link rel="stylesheet" href="../css/custom.css" type="text/css" />
    <style>
        .fotorama__nav-wrap {
            display: none;
        }
    </style>


    <!-- JS Customization -->
    <script src="<%= MetodosFE.BaseURL %>/assets/plugins/jquery/jquery.min.js"></script>
    <script src="<%= MetodosFE.BaseURL %>/js/custom.js"></script>
    <script>
        function go(url) {
            var win = window.open(url, '_self');
        }
    </script>







    <!--MENU E PRODUTOS-->
    <div class="container margin-bottom-30">
        <div class="row">

            <!--PRODUTO-->
            <div class="col-lg-12 col-md-9 col-sm-12 col-xs-12 no-padding">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-30">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 titulo-tela">
                        <h1>
                            <asp:Literal Text="" runat="server" ID="segmento" />

                            <asp:Literal Text="" runat="server" ID="subseg" />
                        </h1>
                    </div>
                </div>
                <div class="col-lg-5 col-md-7 col-sm-12 col-xs-12 img-detalhe-do-produto">

                    <asp:Repeater runat="server" ID="repImagens">
                        <ItemTemplate>
                            <div class="master-slider-control" id="fotorama<%# ((ImagemProduto)Container.DataItem).Id %>" data-value="<%# ((ImagemProduto)Container.DataItem).Id %>">
                                <div class="clearfix">
                                    <div style="width: 170px; margin-right: 5px; position: relative;">
                                        <a href='<%# ((Modelos.ImagemProduto)Container.DataItem).GetEnderecoImagemHQ() %>' class="jqzoom" rel="gal1" title="<%# ((Modelos.ImagemProduto)Container.DataItem).Nome %>">
                                            <img class="ImagemCarrinho" src='<%# ((Modelos.ImagemProduto)Container.DataItem).GetEnderecoImagemHQ() %>' title="<%# ((Modelos.ImagemProduto)Container.DataItem).Nome %>" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="col-lg-6 col-md-5 col-sm-12 col-xs-12 detalhes-do-produto detalheproduto" ng-app="produtoApp" ng-controller="ProdutoController">

                    <div class="botao-voltar">
                        <a href="javascript:window.history.go(-1)">Voltar
                        </a>
                    </div>
                    <div class="text-left  nome-do-produto">
                        <h2>
                            <asp:Literal runat="server" ID="litNome"></asp:Literal>
                        </h2>
                        <br />
                        <h2 style="font-size: 18px">Código:
                            <asp:Literal Text="" ID="litCodigo" runat="server" />
                        </h2>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 texto-produto">
                        <div class="texto-produto-texto">
                            <asp:Literal runat="server" ID="litDescricao"></asp:Literal>
                        </div>
                        <div style='font-size: 14px; color: #ccc; text-decoration: line-through; text-align: left; font-weight: bold'>
                            De R$
                            <asp:Literal Text="" ID="litPrecoSemPromocao" runat="server" />
                        </div>
                        <div class="valor-do-detalhe-do-produto" id="precoProd">
                            <asp:Literal runat="server" ID="litPreco"></asp:Literal>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <div class="col-lg-2 col-md-4 col-sm-3 col-xs-12 produto-3" style="width: 120px">
                            <p class="fonte-pequena-uppercase"><b>Cor</b></p>

                            <%--<asp:TextBox runat="server" ID="litCor"  CssClass="form-control" Height="40px" Width="90px" Style="" /> --%>
                            <asp:DropDownList runat="server" ID="ddlCor" CssClass="form-control" Height="40px" Width="90px" Style="padding-right: 4px">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-4 col-sm-3 col-xs-12 produto-3" style="width: 110px">
                            <p class="fonte-pequena-uppercase"><b>Tamanho</b></p>
                            <asp:DropDownList runat="server" ID="ddlTamanho" CssClass="form-control" Height="40px" Width="90px" Style="margin-left: 2px">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-4 col-sm-3 col-xs-12 produto-3" style="width: 83px; margin-left: 10px">
                            <p class="fonte-pequena-uppercase"><b>Quantidade</b></p>
                            <asp:TextBox runat="server" CssClass="cep-frete-produto" ID="txtQuantidade" Width="50px" Style="margin-left: 2px" Text="1" type="number" min="1"></asp:TextBox>

                        </div>
                        <div class="col-lg-5 col-md-8 col-sm-9 col-xs-12 produto-3" style="padding-left: 5px" id="calculeOFrete">
                            <p class="fonte-pequena-uppercase"><b>Calcule o Frete</b></p>

                            <input class="cep-frete-produto" mask="99999-999" placeholder="CEP" ng-model="cep" name="code" type="text" />
                            <input type="button" value="OK" ng-click="pesquisarCEP()" class="calcular-cep-produto" />
                            <asp:Literal runat="server" ID="litEtiqueta"></asp:Literal>
                        </div>


                        <div ng-include="overlayEspera"></div>


                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 calcula-frete no-padding">
                        <div class="alert alert-dismissible alert-danger rounded" ng-show="mensagem.erro">
                            <p>{{mensagem.erro}}</p>
                        </div>
                        <div class="opcoes-frete col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" ng-repeat="frete in fretes">
                            <div class="col-lg-4 col-md-5 col-sm-3 col-xs-3 opcoes no-padding">
                                {{frete.Nome}}
                            </div>
                            <div class="col-lg-8 col-md-7 col-sm-9  col-xs-9 opcoes">
                                <a>{{frete.Preco | currency : 'R$ ': 2}}<br />
                                </a>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 calcular-frete2 margin-top-20 margin-bottom-25">

                        <div class="col-lg-12 col-md-12 col-sm-7 col-xs-12 produto-add col-xs-12 ">
                            <button runat="server" id="btnAdd" onserverclick="btnAdd_ServerClick" class="adicionar-carrinho">Adicionar no carrinho</button>
                        </div>
                    </div>
                </div>


                <asp:Repeater runat="server" ID="repCaracteristicas">
                    <ItemTemplate>

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 quem-viu-este">
                            <h2><%# ((Modelos.InformacaoProduto)Container.DataItem).Nome %>
                            </h2>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 quem-viu-este">

                            <div class="texto-produto-texto">
                                <%# ((Modelos.InformacaoProduto)Container.DataItem).Texto %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>


            </div>
            <!--FIM PRODUTO-->
        </div>
    </div>
    <!-- FIM MENU E PRODUTOS-->
    <!--fim CONTEÚDO-->

















































    <script type="text/javascript">

        var preco = document.getElementById('precoProd').innerText;
        novoVal = parseInt(preco);
        // var doubleVal = novoVal.toFixed(2);

        var f = novoVal.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });

        //sem R$
        var f2 = novoVal.toLocaleString('pt-br', { minimumFractionDigits: 2 });

        document.getElementById('precoProd').innerText = f;



    </script>









    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
