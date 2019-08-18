<%@ Page Title="" Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master"
    AutoEventWireup="true" CodeFile="Cadastrar.aspx.cs" Inherits="Controle_Cadastro_Categorias" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Controle/UserControl/Endereco.ascx" TagName="Endereco" TagPrefix="uc1" %>
<%--<%@ Register Src="~/Controle/UserControl/ControleLoja.ascx" TagPrefix="uc1" TagName="ControleLoja" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="Server">

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

    <h1 id="tituloCadastro" class="TituloSecao" >Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <ul>
            <li>
                <span class="nomeCampo">ID:</span>
                <asp:TextBox ID="txtID" runat="server" Width="63px" Enabled="False"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Tipo:</span>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" ID="ddlTipo" Width="63px">
                    <asp:ListItem Value="F">PF</asp:ListItem>
                    <asp:ListItem Value="J">PJ</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <span class="nomeCampo">Status:</span>
                <asp:DropDownList runat="server" ID="ddlStatus">
                    <asp:ListItem Value="AT">Ativo</asp:ListItem>
                    <asp:ListItem Value="IN">Inativo</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <span class="nomeCampo">Nome / Razão Social:</span>
                <asp:TextBox ID="txtNome" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Email:</span>
                <asp:TextBox ID="txtEmail" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Telefone:</span>
                <asp:TextBox ID="txtTelefone" CssClass="Telefone" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Celular:</span>
                <asp:TextBox ID="txtCelular" CssClass="Telefone" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li style="padding-left: 0px; padding-bottom: 5px">
                <span class="nomeCampo">CPF:</span>
                <asp:TextBox ID="txtCPF" CssClass="CPF" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <section runat="server" id="divPF" visible="false">
             
                    <li style="padding-left: 61px;">
                        <span class="nomeCampo">Gênero:</span>
                        <asp:DropDownList runat="server" ID="ddlGenero" Width="220px">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="padding-top: 5px; padding-bottom:5px">
                        <span class="nomeCampo">Data Nascimento:</span>
                        <asp:TextBox ID="txtNascimento" CssClass="Data" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
                    </li>
            </section>
            <section runat="server" id="divPJ" visible="false">
                <li style="padding-left: 69px;">
                    <span class="nomeCampo">CNPJ:</span>
                    <asp:TextBox ID="txtCNPJ" CssClass="CNPJ" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
                </li>
                <li style="padding-top: 5px; padding-bottom: 5px; padding-left: 14px;">
                    <span class="nomeCampo">Inscr. Estadual:</span>
                    <asp:TextBox ID="txtInscricao" CssClass="" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
                </li>
            </section>
            <li>
                <span class="nomeCampo">Senha:</span>
                <asp:TextBox ID="txtSenha" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
        </ul>
        <div>
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" CssClass="EstiloBotao" />
            <asp:Button ID="btnSalvar" runat="server" Text="Inserir" CssClass="EstiloBotao" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="EstiloBotao" OnClick="btnCancelar_Click" />
        </div>
    </div>
    <asp:Literal runat="server" ID="litSucesso"></asp:Literal>
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
                            EditText="Editar" HeaderText="" ShowEditButton="True"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo apagar este cliente?')"
                                    CommandName="Delete" CommandArgument="<%# ((Modelos.Cliente)Container.DataItem).CPF %>"></asp:ImageButton>
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
