<%@ Page Title="" Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true"
    CodeFile="Categorias.aspx.cs" Inherits="Controle_Cadastro_Categorias" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Register Src="~/Controle/Cadastro/uplCategoria.ascx" TagPrefix="uc1" TagName="uplCategoria" %>


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
    <h1 class="TituloPagina">
        <asp:Literal runat="server" ID="litTitulo"></asp:Literal>
    </h1>
    <h2>Tela:
        <asp:DropDownList runat="server" ID="ddlTela" AutoPostBack="true" OnTextChanged="ddlTela_TextChanged"></asp:DropDownList></h2>
    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">

        <ul>
            <li>
                <span class="nomeCampo">Nome:</span>
                <asp:TextBox ID="txtNome" Width="300px" runat="server"></asp:TextBox>
            </li>
            <li runat="server" id="liCampo2">

                <span class="nomeCampo">Ordem:</span>
                <asp:TextBox ID="txtOrdem" CssClass="numero" Width="300px" runat="server"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Visível:</span>
                <asp:CheckBox runat="server" ID="chkVisivel" />
            </li>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <ul>
                        <li>
                            <span class="nomeCampo">Seg. Pai:</span>
                            <asp:DropDownList ID="ddlSegmentoPai" AutoPostBack="true" OnTextChanged="ddlSegmentoPai_TextChanged" runat="server" Width="300px">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span class="nomeCampo">Seg. Filho:</span>
                            <asp:DropDownList ID="ddlSegmentoFilho" runat="server" Width="300px">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>


            <li runat="server" id="liDescricao">
                <span class="nomeCampo">Descrição:</span>
                <CKEditor:CKEditorControl ID="txtDescricao" BasePath="~/ckeditor/" Width="100%" Height="350px" runat="server"></CKEditor:CKEditorControl>
            </li>
            <li>
                <uc1:uplCategoria runat="server" ID="uplCategoria" />
            </li>
        </ul>
        <asp:UpdateProgress ID="updateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF; font-size: 36px; left: 40%; top: 40%;">Carregando ...
                                <div class="plus">
                                    Carregando...
                                </div>
                    </span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div>

            <asp:Button ID="btnSalvar" CssClass="EstiloBotao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnAlterar" CssClass="EstiloBotao" CausesValidation="true" runat="server" Text="Alterar"
                OnClick="btnAlterar_Click" />

            <asp:Button ID="btnCancelar" CssClass="EstiloBotao" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                CausesValidation="False" />
        </div>
    </div>
    <h1 id="tituloBusca" class="TituloSecao">Busca
    </h1>
    <div id="divLista" runat="server" clientidmode="Static">
        <asp:Panel runat="server" DefaultButton="btnPesquisar">

            <ul class="listaCampoBusca">
                <li>
                    <span class="nomeCampo">Nome:</span>
                    <asp:TextBox runat="server" ID="txtBuscaNome"></asp:TextBox></li>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <ul>
                            <li>
                                <span class="nomeCampo">Seg. Pai:</span>
                                <asp:DropDownList ID="ddlBuscaSegmentoPai" AutoPostBack="true" OnTextChanged="ddlBuscaSegmentoPai_TextChanged" runat="server" Width="300px">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <span class="nomeCampo">Seg. Filho:</span>
                                <asp:DropDownList ID="ddlBuscaSegmentoFilho" runat="server" Width="300px">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="updateProgress" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                    <ProgressTemplate>
                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                            <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF; font-size: 36px; left: 40%; top: 40%;">Carregando ...
                                <div class="plus">
                                    Carregando...
                                </div>
                            </span>

                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

            </ul>
            <asp:Button ID="btnPesquisar" CssClass="EstiloBotao" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click"
                CausesValidation="False" />
        </asp:Panel>
        <div class="idFiltro clearfix">
            <h2 class="TituloResultadoPesquisa">Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="gvSegmento" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="20" DataKeyNames="id" CellPadding="4" EmptyDataText="Não existe dados."
                    ForeColor="Black" GridLines="Vertical" OnRowDeleting="gvSegmento_RowDeleting"
                    AllowSorting="True" OnSorting="gvSegmento_Sorting" OnRowDataBound="gvSegmento_RowDataBound"
                    OnPageIndexChanging="gvSegmento_PageIndexChanging" OnRowEditing="gvDados_RowEditing" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#DFDFDF" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID"
                            ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="nome" HeaderText="Nome" ReadOnly="True"
                            SortExpression="nome" />
                        <asp:TemplateField HeaderText="Segmento Filho" SortExpression="segFilho.nome">
                            <ItemTemplate>
                                <%# ((Modelos.CategoriaVO)Container.DataItem).segFilho.nome %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visível" SortExpression="visivel">
                            <ItemTemplate>
                                <%# ((Modelos.CategoriaVO)Container.DataItem).visivel?"Sim":"Não" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg"
                            EditText="Editar" HeaderText="" ShowEditButton="True" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo apagar esta categoria, incluindo todos os produtos e demais itens relacionados a ela?')"
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
