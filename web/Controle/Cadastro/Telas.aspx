<%@ Page Title="" Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master"
    AutoEventWireup="true" CodeFile="Telas.aspx.cs" Inherits="Controle_Cadastro_Cursos" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Controle/Cadastro/uplDado.ascx" TagPrefix="uc1" TagName="uplDado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        $(document).ready(
				function () {
				    $('.CEP').inputmask('99999-999');
				    $('.CPF').inputmask('999.999.999-99');
				    $(".CNPJ").inputmask('99.999.999/9999-99');
				    $('.Data').inputmask('99/99/9999');
				    $(".Telefone").inputmask('(99)9999-9999[9]');
				    $(".CNPJCPF").inputmask('99999999999[999]');
				    if (window.location.href.indexOf("Codigo") != -1) {
				        $('#divDados').show();

				        $('#divLista').hide();
				        $('#tituloBusca').css('display', 'none');

				    }
				    else if ($('#divLista').length == 0) {
				        $('#divDados').show();
				        $('#tituloCadastro').css('color', '#FFF');
				    }
				    else {
				        $('#divDados').hide();
				        $('#divDados>h1.tituloProduto').each(function () {
				            $(this).hide();
				        });
				        $('#divLista').show();
				        $('#tituloBusca').css('color', '#FFF');
				    }
				    $(".decimal").maskMoney({ showSymbol: false, decimal: ",", thousands: "" });
				    $('#h1Informacoes').click(function () {
				        $('#divInformacoes').stop().slideDown();
				    });
				    $('#h1Descricao').click(function () {

				        $('#divDescricao').stop().slideDown();
				    });
				    $('#h1Detalhes').click(function () {

				        $('#divDetalhes').stop().slideDown();
				    });
				    $('#h1Cores').click(function () {
				        $('#divCores').stop().slideDown();
				    });
				    $('#h1Busca').click(function () {

				        $('#divBusca').stop().slideDown();
				    });
				    $('#tituloCadastro').click(function () {
				        $('#divLista').stop().slideToggle(function () {
				            if ($('#divLista').is(':visible'))
				                $('#tituloBusca').css('color', '#FFF');

				            else
				                $('#tituloBusca').css('color', 'rgb(122, 122, 122)');
				        });
				        $('#divDados').stop().slideToggle(function () {
				            if ($('#divDados').is(':visible'))
				                $('#tituloCadastro').css('color', '#FFF');

				            else
				                $('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
				        });
				    });
				    $('#tituloBusca').click(function () {
				        $('#divLista').stop().slideToggle(function () {
				            if ($('#divLista').is(':visible'))
				                $('#tituloBusca').css('color', '#FFF');

				            else
				                $('#tituloBusca').css('color', 'rgb(122, 122, 122)');
				        });
				        $('#divDados').stop().slideToggle(function () {
				            if ($('#divDados').is(':visible'))
				                $('#tituloCadastro').css('color', '#FFF');

				            else
				                $('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
				        });
				    });
				});
        function escondeDivs() {
            $('#divDados>div').each(function () {
                $(this).stop().slideUp();
            });
        }
        function mostraDivs() {
            $('#divDados>div').each(function () {
                $(this).stop().slideDown();
            });
        }
    </script>
    <asp:Literal runat="server" ID="litScript"></asp:Literal>
    <asp:Literal runat="server" ID="litMensagem"></asp:Literal>
    <div runat="server" id="divLojas" visible="false">
        <asp:Label ID="lblLoja" runat="server" Text="Filial"></asp:Label>
        <asp:DropDownList ID="DropLoja" Width="179px" runat="server"
            AutoPostBack="true">
        </asp:DropDownList>
    </div>
    <h1 class="TituloPagina">
        <asp:Label ID="lblPagina" runat="server" />
    </h1>
    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <asp:Panel runat="server" ID="divDados" ClientIDMode="Static">

        <ul>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <ul>
                        <li id="liSegPai" runat="server">
                            <asp:Label ID="lblSegmentoPai" runat="server" Text="Label"></asp:Label>
                            <asp:DropDownList ID="DropSegmentoPai" Width="179px" runat="server" OnTextChanged="DropSegmentoPai_TextChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li id="liSegFilho" runat="server">

                            <asp:Label ID="lblSegmentoFilho" runat="server" />
                            <asp:DropDownList ID="DropSegmento" AutoPostBack="true" OnTextChanged="DropSegmento_TextChanged" runat="server" Width="150px">
                            </asp:DropDownList>
                        </li>
                        <li id="liCategoria" runat="server">

                            <asp:Label ID="lblCategoria" runat="server" />
                            <asp:DropDownList ID="DropCategoria" runat="server" Width="150px">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="updateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF; font-size: 36px; left: 40%; top: 40%;">Carregando ...
                        </span>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <li id="liNome" runat="server">
                <asp:Label ID="lblNome" runat="server" />
                <asp:TextBox ID="txtNome" runat="server" Width="300px" AutoCompleteType="Disabled" />
            </li>
            <li id="liReferencia" runat="server">
                <asp:Label ID="lblReferencia" runat="server" />
                <asp:TextBox ID="txtReferencia" runat="server" Width="300px" AutoCompleteType="Disabled" />
            </li>
            <li id="liKeywords" runat="server">

                <asp:Label ID="lblKeywords" runat="server" />
                <asp:TextBox ID="txtKeywords" runat="server"
                    Width="150px" AutoCompleteType="Disabled" />
            </li>
            <li id="liOrdem" runat="server">

                <asp:Label ID="lblOrdem" runat="server" />
                <asp:TextBox ID="txtOrdem" runat="server"
                    Width="300px" AutoCompleteType="Disabled" />
            </li>
            <li id="liPreco" runat="server">

                <asp:Label ID="lblvalor" runat="server" />
                <asp:TextBox ID="txtPreco" runat="server"
                    Width="150px" AutoCompleteType="Disabled" />
            </li>
            <li id="liData" runat="server">

                <asp:Label ID="lblData" runat="server" />
                <asp:TextBox ID="txtData" CssClass="Data" runat="server"
                    Width="150px" AutoCompleteType="Disabled" />
            </li>
            <li id="liDestaque" runat="server">

                <asp:Label ID="lblDestaque" runat="server" />
                <asp:DropDownList ID="DropDestaque" runat="server" Width="150px">
                    <asp:ListItem Selected="True" Value="T">Não</asp:ListItem>
                    <asp:ListItem Value="D">Sim</asp:ListItem>
                </asp:DropDownList>
            </li>


            <li id="liVisivel" runat="server">
                <asp:Label ID="lblvisivel" runat="server" />
                <asp:DropDownList ID="DropVisivel" runat="server" Width="100px">
                    <asp:ListItem Selected="True" Value="true">Sim</asp:ListItem>
                    <asp:ListItem Value="false">Não</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li id="liMeta" runat="server">
                <asp:Label ID="lblMeta" runat="server" />
                <CKEditor:CKEditorControl ID="txtMeta" EnterMode="BR" BasePath="~/ckeditor/" Width="100%" Height="250px" runat="server"></CKEditor:CKEditorControl>
            </li>
            <li id="liResumo" runat="server">

                <asp:Label ID="lblResumo" runat="server" />
                <CKEditor:CKEditorControl ID="txtResumo" EnterMode="BR" BasePath="~/ckeditor/" Width="100%" Height="250px" runat="server"></CKEditor:CKEditorControl>

            </li>
            <li id="liDescricao" runat="server">

                <asp:Label ID="lblDescricao" runat="server" />
                <CKEditor:CKEditorControl ID="txtDescricao" EnterMode="BR" BasePath="~/ckeditor/" Width="100%" Height="350px" runat="server"></CKEditor:CKEditorControl>

            </li>
            <li id="liUpload" runat="server">
                <div>
                    <uc1:uplDado runat="server" ID="uplLogoProd1" />
                </div>
            </li>
            <li style="height: 25px;"></li>
        </ul>
        <div>
            <asp:Button ID="btnAlterar" CausesValidation="true" runat="server" Text="Alterar" CssClass="EstiloBotao"
                OnClick="btnAlterar_Click" />
            <asp:Button ID="btnSalvar" CausesValidation="true" runat="server" Text="Inserir" CssClass="EstiloBotao"
                OnClick="btnSalvar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="EstiloBotao"
                CausesValidation="False" />
        </div>
    </asp:Panel>
    <h1 id="tituloBusca" class="TituloSecao" runat="server" clientidmode="Static">Busca
    </h1>
    <div id="divLista" clientidmode="Static" runat="server">
        <asp:Panel runat="server" DefaultButton="btnPesquisar">
            <ul class="listaCampoBusca">
                <li runat="server" id="liBuscaNome">
                    <asp:Label ID="lblBuscaNome" runat="server"></asp:Label>
                    <asp:TextBox runat="server" ID="txtBuscaNome"></asp:TextBox>
                </li>
                <li runat="server" id="liBuscaID">
                    <asp:Label ID="lblBuscaID" Text="ID" runat="server"></asp:Label>
                    <asp:TextBox runat="server"
                        ID="txtBuscaID"></asp:TextBox>
                </li>
                <li runat="server" id="liBuscaReferencia">
                    <asp:Label ID="lblBuscaReferencia" runat="server"></asp:Label>
                    <asp:TextBox runat="server"
                        ID="txtBuscaReferencia"></asp:TextBox>
                </li>
                <li runat="server" id="liBuscaVisivel">
                    <asp:Label ID="lblBuscaVisivel" runat="server"></asp:Label>
                    <asp:DropDownList runat="server"
                        ID="DropBuscaVisivel">
                        <asp:ListItem Selected="True" Value="">Selecione</asp:ListItem>
                        <asp:ListItem Value="true">Sim</asp:ListItem>
                        <asp:ListItem Value="false">Não</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li runat="server" id="liBuscaDestaque">
                    <asp:Label ID="lblBuscaDestaque" runat="server"></asp:Label>
                    <asp:DropDownList runat="server"
                        ID="DropBuscaDestaque">
                        <asp:ListItem Selected="True" Value="">Selecione</asp:ListItem>
                        <asp:ListItem Value="true">Sim</asp:ListItem>
                        <asp:ListItem Value="false">Não</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <ul>
                            <li id="liBuscaSegmentoPai" runat="server">
                                <asp:Label ID="lblBuscaSegmentoPai" runat="server"></asp:Label>
                                <asp:DropDownList ID="DropBuscaSegmentPai" Width="179px" runat="server" OnTextChanged="DropSegmentoPai_TextChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </li>
                            <li runat="server" id="liBuscaSegmentoFilho">
                                <asp:Label ID="lblBuscaSegmentoFilho" runat="server"></asp:Label>
                                <asp:DropDownList ID="DropBuscaSegmentoFilho" runat="server" Width="150px">
                                </asp:DropDownList>
                            </li>
                            <li runat="server" id="liBuscaCategoria">
                                <asp:Label ID="lblBuscaCategoria" runat="server"></asp:Label>
                                <asp:DropDownList ID="DropBuscaCategoria" runat="server" Width="150px">
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
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click"
                CausesValidation="False" CssClass="EstiloBotao" />
        </asp:Panel>
        <div class="idFiltro clearfix">
            <h6 class="tituloCampo">Resultado da pesquisa</h6>
            <div class="idResultado">
                <asp:GridView ID="gvDados" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="100" DataKeyNames="id" CellPadding="4" EmptyDataText="Não existe dados."
                    GridLines="Vertical" OnRowDeleting="gvDados_RowDeleting" AllowSorting="True"
                    OnSorting="gvDados_Sorting" OnRowEditing="gvDados_RowEditing" OnRowDataBound="gvDados_RowDataBound"
                    OnPageIndexChanging="gvDados_PageIndexChanging" CssClass="tableResultados">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Codigo" InsertVisible="False" ReadOnly="True"
                            SortExpression="id" />
                        <asp:BoundField DataField="nome" HeaderText="" InsertVisible="False" ReadOnly="True"
                            SortExpression="nome" />
                        <asp:BoundField DataField="referencia" HeaderText="" InsertVisible="False" ReadOnly="True"
                            SortExpression="referencia" />
                        <asp:TemplateField HeaderText="Categoria" SortExpression="categoria.nome" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Modelos.DadoVO)Container.DataItem).categoria!=null ? ((Modelos.DadoVO)Container.DataItem).categoria.nome : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seg. Filho" SortExpression="segFilho.nome" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Modelos.DadoVO)Container.DataItem).segFilho!=null ? ((Modelos.DadoVO)Container.DataItem).segFilho.nome : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seg. Pai" SortExpression="segPai.nome" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Modelos.DadoVO)Container.DataItem).segPai!=null ? ((Modelos.DadoVO)Container.DataItem).segPai.nome : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Destaque" SortExpression="destaque" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Modelos.DadoVO)Container.DataItem).destaque.Equals("D") ? "Sim" : "Não" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visível" SortExpression="visivel" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Modelos.DadoVO)Container.DataItem).visivel ? "Sim" : "Não" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ordem" HeaderText="Ordem" InsertVisible="False"
                            ReadOnly="True" SortExpression="ordem" Visible="false" />
                        <asp:TemplateField HeaderText="Data" SortExpression="data" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Modelos.DadoVO)Container.DataItem).data.ToShortDateString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg"
                            EditText="Editar" HeaderText="" ShowEditButton="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo apagar este item?')" CommandName="Delete"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
