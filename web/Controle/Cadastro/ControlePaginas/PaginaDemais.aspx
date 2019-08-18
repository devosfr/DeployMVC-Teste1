<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="PaginaDemais.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        $(document).ready(
				function () {
				    $('#divDados').show();
				    $('#tituloCadastro').css('color', '#FFF');
				});


    </script>
    <asp:HiddenField ID="hfSecao" ClientIDMode="Static" runat="server" />
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 class="TituloPagina">
        <asp:Literal runat="server" ID="litTitulo"></asp:Literal>
    </h1>

    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <ul>
            <li>
                <table>
                    <tbody>
                        <tr>
                            <th>Tela</th>
                            <th>Ativo?</th>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Controle do SiteMap</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkControleSiteMap" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Usuários</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkUsuarios" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Controle de Acesso</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkControleAcesso" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Estados</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkEstados" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Cidades</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCidades" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Bairros</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkBairros" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Segmentos de Produtos</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSegmentosProdutos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">SubSegmentos de Produtos</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSubSegmentosProdutos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Categorias de Produtos</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCategoriasProdutos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Cores de Produtos</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCoresProdutos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Tamanhos de Produtos</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkTamanhosProdutos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Pedidos</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkPedidos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Clientes</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkClientes" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Cupons</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCupons" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Modelo de Cupom</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkModeloCupom" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Importação</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkImportacao" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Controle de Preço</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkPreco" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Importaçao de Categoria</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkImportacaoCategoria" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Apoio Ao Cliente</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkApoioCliente" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Opções de Fretes</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkOpcoesFrete" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Destaques</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDestaques" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="nomeCampo">Rastreamento Correios</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkRastreamentoCorreios" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </li>
        </ul>
        <div>
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar"
                OnClick="btnAlterar_Click" CssClass="EstiloBotao" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="EstiloBotao"
                OnClick="btnCancelar_Click" />

        </div>
    </div>
</asp:Content>

