<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ListaDesejos.aspx.cs" Inherits="_carrinho" EnableEventValidation="false" %>

<%@ Import Namespace="Modelos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <div class="breadcrumbs-v4">
            <div class="container">
                <h1>minha lista de desejos</h1>
                <ul class="breadcrumb-v4-in">
                    <li><a href="<%# MetodosFE.BaseURL %>/">Home</a></li>
                    <li class="active">Minha Lista de desejos</li>
                </ul>
            </div><!--/end container-->
        </div>
        <!--=== End Breadcrumbs v4 ===-->
        <!--=== Content Medium Part ===-->
        <div class="fundo-cinza" ng-app="listaDesejoApp" ng-cloak>
            <div class="content-md" ng-controller="ListaDesejoController">
                <div class="container">
                    <div class="shopping-cart">
                        <div>
                            <section>
                                <div class="table-responsive">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="add-ao-carrinho" ng-repeat="itemDesejo in listaDesejo">
                                                <td class="product-in-table">
                                                    <img class="img-responsive" src="{{itemDesejo.ImagemProduto}}" alt="">
                                                    <div class="product-it-in">
                                                        <h3>{{itemDesejo.NomeProduto}}</h3>
                                                        <div class="texto-produto">{{itemDesejo.DescricaoProduto}}</div>
                                                    </div>
                                                </td>
                                                <td class="shop-black"><a href="{{itemDesejo.LinkProduto}}">VER PRODUTO</a></td>
                                               
                                                <td>
                                                    <button type="button" ng-really-message="Deseja mesmo excluir este item?" ng-really-click="excluir($index)" class="close"><span>&times;</span><span class="sr-only">Close</span></button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </section>
                        </div>
                    </div>
                </div><!--/end container-->
            </div>
        </div>
</asp:Content>



