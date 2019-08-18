<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Endereco.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4" style="background-color:#fff">
        <div class="container">
            <a class="page-name" style="color:#1b511b">meu carrinho</a>
            <h1 style="color:#1b511b">Veja seus itens no carrinho</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" style="color:#1b511b">Home</a></li>
                <li><a href="<%# MetodosFE.BaseURL %>/carrinho" style="color:#1b511b">Meu Carrinho</a></li>
                <li class="active">Endereço de Entrega</li>
            </ul>
        </div>
        <!--/end container-->
    </div>


<%--    <h1>
        <asp:Literal Text="" ID="EmailCliente" runat="server" />
    </h1>--%>

    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Content Medium Part ===-->
    <div class="fundo-cinza" ng-app="enderecoApp" style="background-color:#fff">
        <div class="content-md" ng-controller="EnderecoController">
            <div ng-include="overlayEspera" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></div>
            <div class="container">
                <div class="shopping-cart">
                    <div>
                        <div class="container margin-bottom-50">
                            <div class="col-sm-2"></div>
                            <div class="col-sm-8">
                                <div class="col-sm-4 espaca-50-left text-center">
                                    <div class="carrinho-light"></div>
                                    <br />
                                    <p class="inativo-carrinho">Carrinho de compras</p>
                                </div>
                                <div class="col-sm-4 espaca-50-left text-center">
                                    <div class="cobranca-dark"></div>
                                    <br />
                                    <p class="ativo-carrinho">Endereço de Entrega</p>
                                </div>
                                <div class="col-sm-4 espaca-50-left text-center">
                                    <div class="pagamento-light"></div>
                                    <br />
                                    <p class="inativo-carrinho">Pagamento</p>
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <section class="billing-info">
                            <div class="row">
                                <div class="col-md-6">
                                    <h2 class="title-type">Novo Endereço</h2>
                                    <div class="billing-info-inputs checkbox-list">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <input id="Text4" ng-model="endereco.Logradouro" type="text" placeholder=" Endereço" name="" class="form-control">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <input id="Text4" ng-model="endereco.Numero" type="text" placeholder="Número" name="" class="form-control">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <input id="Text4" ng-model="endereco.Complemento" type="text" placeholder="Complemento" name="" class="form-control">
                                            </div>
                                        </div>

                                        <div class="row">
                                           
                                            <div class="col-sm-6">
                                                <select class="form-control" style="line-height: 34px;" ng-model="endereco.Cidade" ng-change="carregarCidades(endereco.Cidade)" ng-options="cidade as (cidade.Nome) for cidade in (cidades | orderBy: 'Nome') track by cidade.Id">
                                                    <option value="">Cidade</option>
                                                </select>
                                            </div>

                                             <div class="col-sm-6">
                                                <select class="form-control" style="line-height: 34px;" ng-model="endereco.Estado" ng-change="carregarEstados(endereco.Estado)" ng-options="estado as (estado.Nome) for estado in (estados | orderBy: 'Nome') track by estado.Id">
                                                    <option value="">Estado</option>
                                                </select>
                                            </div>

                                        </div>

                                        <div class="row margin-top-10">
                                            <div class="col-sm-6">
                                                <input id="Text6" ng-model="endereco.Bairro" type="text" placeholder="Bairro" name="" class="form-control Cep">
                                            </div>
                                            <div class="col-sm-6">
                                                <input id="Text6" type="text" ng-model="endereco.CEP" placeholder="CEP" mask="99999-999" name="" class="form-control Cep">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 margin-top-20 cadastrar-botao">
                                            <a href="#" class="btn-u-carrinho btn-u-sea-shop pull-right margin-top-20" ng-click="adicionaEndereco()">{{endereco.Id ? 'Editar': 'CADASTRAR'}}</a>
                                        </div>


                                        <%-- <div class="col-md-12 margin-top-20 cadastrar-botao">
                                                <a href="#" class="btn-u-carrinho btn-u-sea-shop pull-right margin-top-20">CADASTRAR</a>
                                            </div>--%>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 margin-top-20">

                                            <div class="alert alert-dismissible alert-danger rounded" ng-show="mensagem.erro">
                                                <p class="palavras-brancas">{{mensagem.erro}}</p>
                                            </div>

                                            <div class="alert alert-dismissible alert-success rounded" ng-show="mensagem.sucesso">
                                                <p class="palavras-brancas">{{mensagem.sucesso}}</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <style>
                                    .selecionado {
                                        border: 2px solid #343434 !important;
                                        background-color: #DDD;
                                    }

                                    .ativo {
                                        border: 2px solid #343434 !important;
                                        background-color: #DDD;
                                    }
                                </style>
                                <div class="col-md-6 campo-de-endereco">
                                    <h2 class="title-type">Endereço cadastrado</h2>
                                    <div class="">
                                        <ul class="list-inline product-color margin-bottom-30">
                                            <li class="frete nopadding" ng-repeat="endereco in enderecos| orderBy: '-Principal'">
                                                <input type="radio" id="endereco-1" name="preto"  ng-hide="true">
                                                <label class="frete" for="endereco-1" ng-click="selecionaEndereco(endereco)"
                                                    style="height:100px;width:400px; font-size:14px; text-align:left" ng-class="{'selecionado' : enderecoSelecionado.Id == endereco.Id}">
                                                    {{endereco.Logradouro}}, {{endereco.Numero}}{{endereco.Complemento ? '/'+ endereco.Complemento : ''}}<br />
                                                    Bairro {{endereco.Bairro}}<br />
                                                    CEP {{endereco.CEP}}, {{endereco.Cidade.Nome}} - {{endereco.Estado.Sigla}}<br />
                                                    <span class="glyphicon glyphicon-remove pull-right icone-endereco-red" ng-really-message="Deseja mesmo excluir este item?" ng-really-click="excluirEndereco(endereco)"></span><span class="fa fa-edit pull-right icone-endereco" ng-click="editarEndereco(endereco)"></span>
                                                </label>
                                            </li>
                                        </ul>
                                        <span ng-show="!enderecos || enderecos.length == 0">É preciso cadastrar um endereço para efetuar a compra.</span>
                                    </div>
                                </div>
                            </div>
                        </section>

                        <div class="coupon-code">
                            <div class="row">
                                <div class="col-sm-6 sm-margin-bottom-30">
                                    <div class="col-sm-8 nopadding">
                                        <h3 ng-show="fretes.length>0" >Escolha uma das opções abaixo:</h3>
                                        <h3 ng-show="fretes.length==0 || !fretes">Sem opções de envio disponíveis</h3>
                                        <div ng-include="overlayEspera" style="position: absolute; top: 25px; left: 0; width: 100%; height: 100%;"></div>
                                        <div class="">
                                            <ul class="list-inline product-color margin-bottom-30">
                                                <li class="frete nopadding" ng-repeat="frete in fretes">
                                                    <input type="radio" ng-hide="true" id="color-{{frete.Nome}}" name="preto">
                                                    <label class="frete" for="color-{{frete.Nome}}" ng-click="selecionaFrete(frete)"
                                                         style="width: 400px; text-align:left; height:35px; paddin-left:3px; font-size: 16px;"
                                                         ng-class="{'ativo': freteSelecionado.Codigo == frete.Codigo}">
                                                        {{frete.Nome}}: {{frete.Preco | currency : 'R$ ': 2}} - {{frete.Prazo}} dias úteis<br />
                                                    </label>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 total-carrinho">
                                    <div class="col-lg-6 col-md-4"></div>
                                    <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12 conjunto-de-botoes-carrinho">
                                        <ul class="list-inline total-result">
                                            <li>
                                                <h4>Subtotal:</h4>
                                                <div class="total-result-in">
                                                    <div>{{subTotal() | currency: 'R$ ': 2}}</div>
                                                </div>
                                            </li>
                                            <li ng-show="freteSelecionado.Preco > 0">
                                                <h4>Frete:</h4>
                                                <div class="total-result-in">
                                                    <div class="text-right">{{freteSelecionado ? (freteSelecionado.Preco| currency: 'R$ ':2) : '- - -'}}</div>
                                                </div>
                                            </li>
                                            <li ng-show="totalDesconto() > 0">
                                                <h4>Desconto:</h4>
                                                <div class="total-result-in">
                                                    <div class="text-right">{{totalDesconto()| currency: 'R$ ': 2}}</div>
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
                                                    <div>{{(subTotal() + freteSelecionado.Preco) - totalDesconto()|currency: 'R$ ':2}}</div>
                                                </div>
                                                <br />
                                                <br />
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 botoes-carrinho-avancar">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <a class="btn-u-carrinho btn-u-sea-shop" title="" href="javascript:window.history.go(-1)">VOLTAR</a>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <a class="btn-u-carrinho btn-u-sea-shop" href="<%= MetodosFE.BaseURL %>/pagamento" title="" ng-click="" ng-show="freteSelecionado">AVANÇAR</a>
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


</asp:Content>
