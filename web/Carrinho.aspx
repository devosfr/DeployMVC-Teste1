<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Carrinho.aspx.cs" Inherits="_carrinho" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div style="background-color: #fff">
        <!--CAMINHO-->
        <div class="container caminho-normal">
            <div class="row">
               <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h1>Carrinho</h1>
                    <ul class="lista-caminho-carrinho">
                        <li><a href="<%# MetodosFE.BaseURL %>/" title="">Home</a></li>
                        <li>/
                        </li>
                        <li class="active">Carrinho</li>
                        <li>/
                        </li>
                        <li class="active">Meu Carrinho</li>
                    </ul>
                </div>
            </div>
        </div>
        <!--fim CAMINHO-->


        <div class="container fundo-branco padding-telas tamanho-550" ng-app="carrinhoApp" ng-cloak>
            <div ng-controller="CarrinhoController">
                <div ng-include="overlayEspera" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></div>
                <div class="content-md ">
                    <div class="container" style="width: 100%; float: left; margin-bottom:60px">
                        <div class="shopping-cart">
                            <div>
                                <div class="container margin-bottom-50">
                                    <div class="col-sm-2"></div>
                                    <div class="col-sm-8">
                                        <div class="col-sm-4 espaca-50-left text-center">
                                            <div class="carrinho-dark"></div>
                                            <br />
                                            <p class="ativo-carrinho">Carrinho de compras</p>
                                        </div>
                                        <div class="col-sm-4 espaca-50-left text-center">
                                            <div class="cobranca-light"></div>
                                            <br />
                                            <p class="inativo-carrinho">Endereço de Entrega</p>
                                        </div>
                                        <div class="col-sm-4 espaca-50-left text-center">
                                            <div class="pagamento-light"></div>
                                            <br />
                                            <p class="inativo-carrinho">Pagamento</p>
                                        </div>
                                    </div>
                                    <div class="col-sm-2"></div>
                                </div>
                                <section>
                                <div class="table-responsive">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th>Produto</th>
                                                <th>Valor</th>
                                                <th>quantidade</th>
                                                <th>Total</th>
                                            </tr>
                                        </thead>
                                        <tbody ng-cloak>
                                            <tr ng-repeat="itemCarrinho in carrinho">
                                                <td class="product-in-table">
                                                    <img class="img-responsive" src='{{itemCarrinho.ImagemProduto}}' alt="{{itemCarrinho.NomeProduto}}">
                                                    <div class="product-it-in">
                                                        <h3>{{itemCarrinho.NomeProduto}}</h3>
                                                        <div>{{itemCarrinho.DescricaoProduto}}</div>
                                                    </div>
                                                </td>
                                                <td>{{itemCarrinho.PrecoProduto | currency: 'R$ ' : 2}}</td>
                                                <td>
                                                    <button type='button' class="quantity-button" name='subtract' ng-click="decrementaQuantidade(itemCarrinho)" value='-'>-</button>
                                                    <input type='text' class="quantity-field" name='qty1' ng-model="itemCarrinho.Quantidade" ng-change="programaAtualizacao()" value="5" id='qty1' />
                                                    <button type='button' class="quantity-button" name='add' ng-click="incrementaQuantidade(itemCarrinho)" value='+'>+</button>
                                                </td>
                                                <td class="shop-red">{{itemCarrinho.PrecoProduto * itemCarrinho.Quantidade| currency: 'R$ ' : 2}}</td>
                                                <td class="botao-fechar">
                                                    <button type="button" ng-really-message="Deseja mesmo excluir este item?" ng-really-click="excluir($index)" class="close">x</button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </section>
                                <div class="coupon-code">
                                    <div class="row">
                                        <div class="col-sm-6 sm-margin-bottom-30 usar" ng-if="!statusCupom">
                                            <%--<h3>Cupom de Desconto</h3>
                                            <input ng-model="cupom.Codigo" placeholder="Insira seu código" class="form-control margin-bottom-10" name="code" type="text" />
                                            <button type="button" ng-click="defineCupom(cupom.Codigo)" class="btn-u-carrinho btn-u-sea-shop">usar</button>




                                            <div class="alert alert-dismissible alert-danger rounded" ng-show="mensagem.erro">
                                                <p class="palavras-brancas">{{mensagem.erro}}</p>
                                            </div>
                                            <div class="alert alert-dismissible alert-success rounded" ng-show="mensagem.sucesso">
                                                <p class="palavras-brancas">{{mensagem.sucesso}}</p>
                                            </div>--%>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 total-carrinho">
                                            <div class="col-lg-6 col-md-4"></div>
                                            <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12 conjunto-de-botoes-carrinho">
                                                <ul class="list-inline total-result">
                                                    <li>
                                                        <h4>Subtotal:</h4>
                                                        <div class="total-result-in">
                                                            <div>{{valorSubTotal() | currency: 'R$ ' : 2}}</div>
                                                        </div>
                                                    </li>
                                                    <li ng-show="totalDesconto() > 0">
                                                        <h4>Descontos:</h4>
                                                        <div class="total-result-in">
                                                            <div class="text-right">{{totalDesconto() | currency: 'R$ ' : 2}}</div>
                                                        </div>
                                                    </li>
                                                    <li class="">
                                                        <div class="">
                                                            <hr />
                                                        </div>
                                                    </li>
                                                    <li class="total-price">
                                                        <h4>Total:</h4>
                                                        <div class="total-result-in">
                                                            <div>{{valorSubTotal() - totalDesconto() | currency: 'R$ ' : 2}}</div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                       <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 botoes-carrinho-avancar">
                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                <a class="btn-u-carrinho btn-u-sea-shop" title="" href="javascript:window.history.go(-1)">VOLTAR</a>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                <a class="btn-u-carrinho btn-u-sea-shop" href="<%= MetodosFE.BaseURL %>/endereco" title="">AVANÇAR</a>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/end container-->
                </div>
            </div>
        </div>

    </div>

</asp:Content>



