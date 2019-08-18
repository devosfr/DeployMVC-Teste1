<%@ Page Title="" Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true"
	CodeFile="ControlePreco.aspx.cs" Inherits="Controle_Cadastro_produtos" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="Server">
	<script type="text/javascript">
		$(document).ready(
				function () {
					$(".decimal").maskMoney({ showSymbol: false, decimal: ",", thousands: "" });
					var secao = $('#hfSecao').val();
					if (secao != "") {
						if (secao == "C") {
							$('#divDados').show();
							$('#tituloCadastro').css('color', '#FFF');
							$('#divLista').hide();

						}
						else {
							$('#divDados').hide();
							$('#divLista').show();
							$('#tituloBusca').css('color', '#FFF');
						}
					}
					else if (window.location.href.indexOf("Codigo") != -1) {
						$('#divDados').show();

						$('#divLista').hide();
						$('#tituloBusca').css('display', 'none');

					}
					else {
						$('#divDados').hide();
						$('#divLista').show();
						$('#tituloBusca').css('color', '#FFF');
					}
					$(".decimal").maskMoney({ showSymbol: false, decimal: ",", thousands: "" });

					$('#tituloCadastro').click(function () {
						$('#divLista').stop().slideToggle(function () {
							if ($('#divLista').is(':visible')) {
								$('#tituloBusca').css('color', '#FFF');
								$('#hfSecao').val("");
							}

							else
								$('#tituloBusca').css('color', 'rgb(122, 122, 122)');


						});
						$('#divDados').stop().slideToggle(function () {
							if ($('#divDados').is(':visible')) {
								$('#tituloCadastro').css('color', '#FFF');
								$('#hfSecao').val("C");
							}

							else
								$('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
						});
					});
					$('#tituloBusca').click(function () {
						$('#divLista').stop().slideToggle(function () {
							if ($('#divLista').is(':visible')) {
								$('#tituloBusca').css('color', '#FFF');
								$('#hfSecao').val("");
							}
							else {
								$('#tituloBusca').css('color', 'rgb(122, 122, 122)');
							}
						});
						$('#divDados').stop().slideToggle(function () {
							if ($('#divDados').is(':visible')) {
								$('#hfSecao').val("C");
								$('#tituloCadastro').css('color', '#FFF');
							}
							else {
								$('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
								$('#hfSecao').val("");
							}
						});
					});

				});


	</script>
	<asp:HiddenField ID="hfSecao" ClientIDMode="Static" runat="server" />
	<asp:Literal runat="server" ID="litErro"></asp:Literal>
	<h1 class="TituloPagina">Controle de Preços
	</h1>
	<h1 id="tituloCadastro" class="TituloSecao">Cadastro
	</h1>
	<div id="divDados" runat="server" clientidmode="Static">

		<div>

			<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
				CausesValidation="False" CssClass="EstiloBotao" />

		</div>
	</div>
	<h1 id="tituloBusca" class="TituloSecao">Busca
	</h1>
	<div id="divLista" runat="server" clientidmode="Static">
		<div>
			<ul class="listaCampoBusca">
				<li>
                    <span>ID:</span>
					<asp:TextBox runat="server" CssClass="Numero" ID="txtBuscaID"></asp:TextBox>
				</li>
                <li>
					<span>Referencia</span>
					<asp:TextBox runat="server" ID="txtReferencia"></asp:TextBox>
				</li>
			</ul>
			<asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CausesValidation="False"
				OnClick="btnPesquisar_Click" CssClass="EstiloBotao" />
		</div>





		<div class="idFiltro clearfix">
			<h2 class="TituloResultadoPesquisa">Resultado da pesquisa</h2>
			<div class="idResultado">
				<asp:GridView ID="gvPrecos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
					PageSize="30" DataKeyNames="Id" CellPadding="4" EmptyDataText="Não existe dados."
					ForeColor="Black" GridLines="Vertical" AllowSorting="True"
					OnRowEditing="gvDados_RowEditing" OnSorting="gvPrecos_Sorting" OnRowCancelingEdit="gvDados_RowCancelingEdit"
					OnRowUpdating="gvDados_RowUpdating" OnRowDataBound="gvDados_RowDataBound" OnPageIndexChanging="gvDados_PageIndexChanging"
					BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
					<FooterStyle BackColor="#CCCC99" />
					<RowStyle BackColor="#F7F7DE" />
					<Columns>
						<asp:BoundField DataField="Id" HeaderText="Codigo" InsertVisible="False" ReadOnly="True"
							SortExpression="Id" />
						<asp:TemplateField HeaderText="Nome" >
							<ItemTemplate>
								<%# ((Produto)Container.DataItem).Nome %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Ref." SortExpression="Referencia">
							<ItemTemplate>
								<%# ((Produto)Container.DataItem).Referencia %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Preço" SortExpression="Preco.Valor">
							<ItemTemplate>
								<%# ((Produto)Container.DataItem).Preco.Valor.ToString("C") %>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="txtValor" CssClass="decimal" runat="server" Text="<%# ((Produto)Container.DataItem).Preco.Valor.ToString() %>"></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Importar?" SortExpression="ImportarPreco">
							<ItemTemplate>
								<%# ((Produto)Container.DataItem).ImportarPreco ? "Sim" : "Não" %>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:CheckBox ID="chkImportar" runat="server" Text="Importar" Checked="<%# ((Produto)Container.DataItem).ImportarPreco %>"></asp:CheckBox>
							</EditItemTemplate>
						</asp:TemplateField>
						<%--                                <asp:BoundField DataField="DescricaoSegPai" HeaderText="Tela" InsertVisible="False"
									ReadOnly="True" SortExpression="DescricaoSegPai" />
								<asp:BoundField DataField="DescricaoSegFilho" HeaderText="Segmento" InsertVisible="False"
									ReadOnly="True" SortExpression="DescricaoSegFilho" />--%>
						<asp:CommandField ButtonType="Image" CancelImageUrl="~/Controle/comum/img/cancelar.gif" EditImageUrl="~/Controle/comum/img/icoEditar.jpg"
							ShowEditButton="True" UpdateImageUrl="~/Controle/comum/img/icoEditar.jpg" HeaderText="Editar Preço"
							HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" />
					</Columns>
					<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
					<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
					<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
					<AlternatingRowStyle BackColor="White" />
				</asp:GridView>
			</div>
		</div>
	</div>

</asp:Content>
