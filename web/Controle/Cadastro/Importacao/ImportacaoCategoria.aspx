<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="ImportacaoCategoria.aspx.cs" Inherits="Controle_Importacao_Categorias" Title="Acabamentos" %>

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
					<asp:Button runat="server" ID="btnImportar" Text="Importar" CssClass="EstiloBotao" OnClick="btnImportar_Click1" />
				</li>
				<li>
					<asp:FileUpload runat="server" ID="fulSiteMap" />
				</li>
				<li>
					<asp:Button runat="server" ID="btnCarregar" Text="Clique para carregar" CssClass="EstiloBotao" OnClick="btnCarregar_Click" />
				</li>
			</ul>
		</div>
	</div>
</asp:Content>

