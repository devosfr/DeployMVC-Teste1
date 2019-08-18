<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="Paginas.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="Server">
	<script type="text/javascript">
		$(document).ready(
				function () {
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
				<asp:CheckBox ID="chkTelaVisivel" runat="server" />
			</li>
			<li>
				<span class="nomeCampo">Multi-Documento:</span>
				<asp:CheckBox ID="chkMulti" runat="server" />

			</li>
			<li>
				<span class="nomeCampo"></span>
				<div>
					<table>
						<tbody>
							<tr>
								<th>Campo</th>
								<th>Ativo?</th>
								<th>Título</th>
								<th>Classe</th>
							</tr>

							<tr>
								<td>
									<span class="nomeCampo">Nome</span>
								</td>
								<td>
									<asp:CheckBox ID="chkNome" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtNomeNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<asp:DropDownList ID="ddlNomeClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value="numero"></asp:ListItem>
										<asp:ListItem Text="Decimal" Value="decimal"></asp:ListItem>
										<asp:ListItem Text="Data" Value="Data"></asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Referência</span>
								</td>
								<td>
									<asp:CheckBox ID="chkReferencia" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtReferenciaNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<asp:DropDownList ID="ddlReferenciaClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value="numero"></asp:ListItem>
										<asp:ListItem Text="Decimal" Value="decimal"></asp:ListItem>
										<asp:ListItem Text="Data" Value="Data"></asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Valor</span>
								</td>
								<td>
									<asp:CheckBox ID="chkValor" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtValorNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<asp:DropDownList ID="ddlValorClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value="numero"></asp:ListItem>
										<asp:ListItem Text="Decimal" Value="decimal"></asp:ListItem>
										<asp:ListItem Text="Data" Value="Data"></asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Ordem</span>
								</td>
								<td>
									<asp:CheckBox ID="chkOrdem" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtOrdemNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<asp:DropDownList ID="ddlOrdemClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value="numero"></asp:ListItem>
										<asp:ListItem Text="Decimal" Value="decimal"></asp:ListItem>
										<asp:ListItem Text="Data" Value="Data"></asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
                            <tr>
								<td>
									<span class="nomeCampo">Destaque</span>
								</td>
								<td>
									<asp:CheckBox ID="chkDestaque" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtNomeDestaque" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Data</span>
								</td>
								<td>
									<asp:CheckBox ID="chkData" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtNomeData" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td></td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Keywords</span>
								</td>
								<td>
									<asp:CheckBox ID="chkKeywords" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtNomeKeywords" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td></td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Metas</span>
								</td>
								<td>
									<asp:CheckBox ID="chkMeta" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtNomeMeta" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<%--                                    <asp:DropDownList ID="ddlClasseNome" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value=""></asp:ListItem>
										<asp:ListItem Text="Decimal" Value=""></asp:ListItem>
										<asp:ListItem Text="Data" Value=""></asp:ListItem>
									</asp:DropDownList>--%>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Resumo</span>
								</td>
								<td>
									<asp:CheckBox ID="chkResumo" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtResumoNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<%--                                    <asp:DropDownList ID="ddlClasseNome" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value=""></asp:ListItem>
										<asp:ListItem Text="Decimal" Value=""></asp:ListItem>
										<asp:ListItem Text="Data" Value=""></asp:ListItem>
									</asp:DropDownList>--%>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Descrição</span>
								</td>
								<td>
									<asp:CheckBox ID="chkDescricao" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtDescricaoNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<%--                                    <asp:DropDownList ID="ddlDescricaoClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value=""></asp:ListItem>
										<asp:ListItem Text="Decimal" Value=""></asp:ListItem>
										<asp:ListItem Text="Data" Value=""></asp:ListItem>
									</asp:DropDownList>--%>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Segmento Pai</span>
								</td>
								<td>
									<asp:CheckBox ID="chkSegmentoPai" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtSegmentoPaiNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<%--                                    <asp:DropDownList ID="ddlSegmentoFilhoClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value=""></asp:ListItem>
										<asp:ListItem Text="Decimal" Value=""></asp:ListItem>
										<asp:ListItem Text="Data" Value=""></asp:ListItem>
									</asp:DropDownList>--%>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Segmento Filho</span>
								</td>
								<td>
									<asp:CheckBox ID="chkSegmentoFilho" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtSegmentoFilhoNome" AutoCompleteType="Disabled" runat="server" />
								</td>
								<td>
									<%--                                    <asp:DropDownList ID="ddlSegmentoFilhoClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value=""></asp:ListItem>
										<asp:ListItem Text="Decimal" Value=""></asp:ListItem>
										<asp:ListItem Text="Data" Value=""></asp:ListItem>
									</asp:DropDownList>--%>
								</td>
							</tr>

							<tr>
								<td>
									<span class="nomeCampo">Categoria</span>
								</td>
								<td>
									<asp:CheckBox ID="chkCategoria" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtCategoriaNome" runat="server" />
								</td>
								<td>
									<%--                                    <asp:DropDownList ID="ddlCategoriaClasse" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value=""></asp:ListItem>
										<asp:ListItem Text="Decimal" Value=""></asp:ListItem>
										<asp:ListItem Text="Data" Value=""></asp:ListItem>
									</asp:DropDownList>--%>
								</td>
							</tr>
							<tr>
								<td>
									<span class="nomeCampo">Visível</span>
								</td>
								<td>
									<asp:CheckBox ID="chkVisivel" runat="server" />
								</td>
								<td>
									<asp:TextBox ID="txtVisivelNome" runat="server" />
								</td>
								<td>
									<%--                                    <asp:DropDownList ID="ddlVisivel" runat="server">
										<asp:ListItem Text="Selecione" Value=""></asp:ListItem>
										<asp:ListItem Text="Número" Value=""></asp:ListItem>
										<asp:ListItem Text="Decimal" Value=""></asp:ListItem>
										<asp:ListItem Text="Data" Value=""></asp:ListItem>
									</asp:DropDownList>--%>
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
							<asp:TextBox ID="txtUplCor" CssClass="" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
						</li>
					</ul>
				</div>

			</li>
		</ul>
		<div>
			<asp:Button ID="btnAlterar" runat="server" Text="Alterar"
				OnClick="btnAlterar_Click" CssClass="EstiloBotao" />
			<asp:Button ID="btnSalvar" runat="server" Text="Inserir" CssClass="EstiloBotao"
				OnClick="btnSalvar_Click" />

			<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="EstiloBotao"
				OnClick="btnCancelar_Click" />

		</div>
	</div>
	<h1 id="tituloBusca" class="TituloSecao">Busca
	</h1>
	<div id="divLista" runat="server" clientidmode="Static">
		<ul class="listaCampoBusca">
			<li>
				<span class="nomeCampo">ID:</span>
				<asp:TextBox ID="txtIDBusca" runat="server" Width="63px"></asp:TextBox></li>
			<li>
				<span class="nomeCampo">Nome:</span>
				<asp:TextBox ID="txtBuscaNome" runat="server" Width="220px"
					AutoCompleteType="Disabled"></asp:TextBox>

			</li>

		</ul>
		<asp:Button ID="btnPesquisar" CssClass="EstiloBotao" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click"
			CausesValidation="False" />
		<div class="idFiltro clearfix">
			<h2>Resultado da pesquisa</h2>
			<div class="idResultado">
				<asp:GridView ID="gvObjeto" runat="server" AllowPaging="True"
					AutoGenerateColumns="False" DataKeyNames="id" CellPadding="4"
					EmptyDataText="Não existe dados." ForeColor="Black" GridLines="Both" PageSize="50"
					AllowSorting="True" OnSorting="gvObjeto_Sorting" OnRowDeleting="gvObjeto_RowDeleting" OnRowEditing="gvObjeto_RowEditing"
					OnPageIndexChanging="gvObjeto_PageIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
					<FooterStyle BackColor="#CCCC99" />
					<RowStyle BackColor="#F7F7DE" />
					<Columns>
						<asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True"
							SortExpression="id" Visible="true">
							<HeaderStyle ForeColor="White" />
						</asp:BoundField>
						<asp:BoundField DataField="nome" HeaderText="Nome" ReadOnly="True"
							SortExpression="nome" Visible="True">
							<HeaderStyle ForeColor="White" />
						</asp:BoundField>
						<asp:TemplateField HeaderText="Multi" SortExpression="multiplo">
							<ItemTemplate>
								<%# ((Modelos.Tela)Container.DataItem).multiplo ? "Sim" : "Não"%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Upload" SortExpression="">
							<ItemTemplate>
								<%# ((Modelos.Tela)Container.DataItem).upload != null ? "Sim" : "Não"%>
							</ItemTemplate>
						</asp:TemplateField>

						<asp:TemplateField HeaderText="Visível" SortExpression="pagina">
							<ItemTemplate>
								<%# ((Modelos.Tela)Container.DataItem).pagina != null ? "Sim" : "Não"%>
							</ItemTemplate>
						</asp:TemplateField>

						<asp:CommandField ButtonType="Image"
							EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" ShowEditButton="True"
							UpdateImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" HeaderStyle-BorderWidth="0px"
							ItemStyle-BorderWidth="0px" />
						<asp:TemplateField>
							<ItemTemplate>
								<asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
									OnClientClick="return confirm('Deseja mesmo apagar esta página, apagando ele e todos os itens relacionados?')" CommandName="Delete"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
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

