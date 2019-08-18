<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="PaginaSegPai.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

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
                <span class="nomeCampo">Nome:</span>
                <asp:TextBox ID="txtNome" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Visível:</span>
                <asp:CheckBox runat="server" ID="chkTelaVisivel" />
            </li>
            <li>
                <span class="nomeCampo"></span>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <th>Campo</th>
                                <th>Ativo?</th>
                            </tr>
                            <tr>
                                <td>
                                    <span class="nomeCampo">Ordem</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkOrdem" runat="server" />
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <span class="nomeCampo">Descrição</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkDescricao" runat="server" />
                                </td>

                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div>
                    <h2>
                        <asp:CheckBox ID="chkUpload" runat="server" />
                        Upload
                    </h2>
                    <ul>
                        <li>
                            <span class="nomeCampo" style="width: 125px;">Tam. Largura (G):</span>
                            <asp:TextBox ID="txtUplLarguraG" CssClass="numero" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                        <li>
                            <span class="nomeCampo" style="width: 125px;">Tam. Altura (G):</span>
                            <asp:TextBox ID="txtUplAlturaG" CssClass="numero" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                        <li>
                            <span class="nomeCampo" style="width: 125px;">Tam. Largura (P):</span>
                            <asp:TextBox ID="txtUplLarguraP" CssClass="numero" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                        <li>
                            <span class="nomeCampo" style="width: 125px;">Tam. Altura (P):</span>
                            <asp:TextBox ID="txtUplAlturaP" CssClass="numero" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                        <li>
                            <span class="nomeCampo" style="width: 125px;">Quantidade Máx.:</span>
                            <asp:TextBox ID="txtUplQuantidade" CssClass="numero" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                                                <li>
                            <span class="nomeCampo" style="width: 125px;">Qualidade:</span>
                            <asp:TextBox ID="txtUplQualidade" CssClass="numero" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                        <li>
                            <span class="nomeCampo" style="width: 125px;">Configuração:</span>
                            
                            <asp:DropDownList ID="ddlUplConfiguracao" runat="server">
                                <asp:ListItem Text="Encaixar imagem nas dimensões (Sujeito a ficar com borda)" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Ampliar imagem até encaixar nas dimensões(Sem borda)" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Sem nenhuma modificacão" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                        </li>
                        <li>
                            <span class="nomeCampo" style="width: 125px;">Cor do Fundo(Cód. ex. : FFFFFF):</span>
                            <asp:TextBox ID="txtUplCor" CssClass="numero" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                    </ul>
                </div>

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

