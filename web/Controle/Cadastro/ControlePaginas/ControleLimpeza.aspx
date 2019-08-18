<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="ControleLimpeza.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

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

    <h1 id="tituloCadastro" style="color: #FFF;" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <div>

            <ul>
                <li>
                    <asp:Button runat="server" ID="btnLimparProdutos" Text="Limpar Produtos" OnClientClick="return confirm('Deseja mesmo apagar todo o conteúdo  segmento filho, incluindo todas as categorias, e demais itens relacionados?')" CssClass="EstiloBotao" OnClick="btnLimparProdutos_Click" />
                </li>
            </ul>
        </div>
    </div>
</asp:Content>

