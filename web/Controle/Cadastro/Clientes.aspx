<%@ Page Title="" Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master"
    AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="Controle_Cadastro_Categorias" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Controle/UserControl/Endereco.ascx" TagName="Endereco" TagPrefix="uc1" %>
<%--<%@ Register Src="~/Controle/UserControl/ControleLoja.ascx" TagPrefix="uc1" TagName="ControleLoja" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="Server">
    <style>
		
	</style>
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
    <%--    <uc1:ControleLoja runat="server" ID="ControleLoja" />--%>
    <h1 class="TituloPagina">Cliente
    </h1>
    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <ul>
            <li>
                <%--idloja--%>
                <span class="nomeCampo">ID:</span>
                <asp:TextBox runat="server" ID="txtID" Enabled="false"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Nome:</span>
                <asp:TextBox runat="server" ID="txtNome" Style="width: 600px;"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">CPF:</span>
                <asp:TextBox runat="server" ID="txtCPF" Style="width: 180px;" ClientIDMode="Static"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">CNPJ:</span>
                <asp:TextBox runat="server" ID="txtCNPJ" Style="width: 180px;" ClientIDMode="Static"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">E-mail:</span>
                <asp:TextBox runat="server" ID="txtEmail" Style="width: 180px;" ClientIDMode="Static"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Telefone:</span>
                <asp:TextBox runat="server" ID="txtTelefone" Style="width: 180px;" ClientIDMode="Static"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Produto:</span>
                  <asp:DropDownList runat="server" CssClass="form-control" ID="ddlProduto">
                      <asp:ListItem Value="Selecione o Produto" Text="Selecione o Produto"></asp:ListItem>
                      <asp:ListItem Value="Sniff" Text="Sniff"></asp:ListItem>
                      <asp:ListItem Value="Sniff Casa" Text="Sniff Casa"></asp:ListItem>
                  </asp:DropDownList>
            </li>
            <li>
            <span class="nomeCampo">Número de Série:</span>
              <asp:TextBox runat="server" ID="txtNumeroSerie" Style="width: 180px;" ClientIDMode="Static"></asp:TextBox>
           
            </li>
            <li>
                <span class="nomeCampo">Status:</span>
                <asp:DropDownList ID="ddlStatus" runat="server" Style="width: 180px;">
                    <asp:ListItem Text="Ativo" Value="AT"></asp:ListItem>
                    <asp:ListItem Text="Inativo" Value="IN"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <%--statusfornecedor--%>
                <span class="nomeCampo"></span>
                <uc1:Endereco runat="server" ID="Endereco" />
            </li>
            <li>
                <%--obsfornecedor--%>
                <span class="nomeCampo">Observações:</span>
                <CKEditor:CKEditorControl ID="txtObservacoes" BasePath="~/ckeditor/" Width="100%" Height="250px" runat="server"></CKEditor:CKEditorControl>
            </li>
        </ul>
        <div>
            <asp:Button ID="btnSalvar" CssClass="EstiloBotao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnAlterar" CssClass="EstiloBotao" CausesValidation="true" runat="server"
                Text="Alterar" OnClick="btnAlterar_Click" />
            <asp:Button ID="btnCancelar" CssClass="EstiloBotao" runat="server" Text="Cancelar"
                OnClick="btnCancelar_Click" CausesValidation="False" />
        </div>
    </div>
    <h1 id="tituloBusca" class="TituloSecao">Busca
    </h1>
    <div id="divLista" runat="server" clientidmode="Static">
        <asp:Panel runat="server" DefaultButton="btnPesquisar">
            <ul class="listaCampoBusca">
                <li>
                    <span class="nomeCampo">ID:</span>
                    <asp:TextBox runat="server" ID="txtBuscaID"></asp:TextBox></li>
                <li>
                    <span class="nomeCampo">Nome</span>
                    <asp:TextBox runat="server" ID="txtBuscaNome"></asp:TextBox></li>
                <li>
                    <span class="nomeCampo">CPF/CNPJ:</span>
                    <asp:TextBox runat="server" ID="txtBuscaCPFCNPJ"></asp:TextBox></li>
            </ul>
            <asp:Button ID="btnPesquisar" CssClass="EstiloBotao" runat="server" Text="Pesquisar"
                OnClick="btnPesquisar_Click" CausesValidation="False" />
        </asp:Panel>
        <div class="idFiltro clearfix">
            <h2 class="TituloResultadoPesquisa">Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="gvSegmento" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="20" DataKeyNames="id" CellPadding="4" EmptyDataText="Nenhuma loja encontrada."
                    ForeColor="Black" GridLines="Vertical" OnRowDeleting="gvSegmento_RowDeleting"
                    AllowSorting="True" OnSorting="gvSegmento_Sorting" OnRowDataBound="gvSegmento_RowDataBound"
                    OnPageIndexChanging="gvSegmento_PageIndexChanging" OnRowEditing="gvDados_RowEditing"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#DFDFDF" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                            SortExpression="id" />
                        <asp:BoundField DataField="nome" HeaderText="Nome" InsertVisible="False" ReadOnly="True"
                            SortExpression="nome" />
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg"
                            EditText="Editar" HeaderText="" ShowEditButton="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo apagar este cliente?')"
                                    CommandName="Delete"></asp:ImageButton>
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
    <!--<asp:TemplateField>
							<ItemTemplate>
								<asp:HyperLink ID="hplEditar" runat="server" NavigateUrl='<%# Eval("id", "Categorias.aspx?Codigo={0}") %>'>
									<asp:Image ID="imgEditar" ToolTip="Editar" ImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg"
										runat="server" />
								</asp:HyperLink>
							</ItemTemplate>
						</asp:TemplateField>-->
</asp:Content>
