<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="Segmento.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
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
                <span class="nomeCampo">ID:</span>
                <asp:TextBox ID="txtID" runat="server" Width="63px" Enabled="False"></asp:TextBox></li>
            <li>
                <span class="nomeCampo">Nome:</span>
                <asp:TextBox ID="txtNome" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>

            </li>
            
            <li>
                <span class="nomeCampo">Ordem:</span>
                <asp:TextBox ID="txtOrdem" CssClass="numero" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>

            </li>
            <li>
                    <span class="nomeCampo">Imagem:</span>
                    <CKEditor:CKEditorControl runat="server" ID="img"></CKEditor:CKEditorControl>
                </li>
            <li style="    margin-left: 96px;">
                <asp:CheckBox runat="server" ID="chkVisivel" Text="Visível" />
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
        <asp:Panel runat="server" DefaultButton="btnPesquisar">
            <ul class="listaCampoBusca">
                <li>
                    <span class="nomeCampo">ID:</span>
                    <asp:TextBox ID="txtIDBusca" runat="server" CssClass="numero" Width="63px"></asp:TextBox></li>
                <li>
                    <span class="nomeCampo">Nome:</span>
                    <asp:TextBox ID="txtBuscaNome" runat="server" Width="220px"
                        AutoCompleteType="Disabled"></asp:TextBox>
                </li>
                

            </ul>
            <asp:Button ID="btnPesquisar" CssClass="EstiloBotao" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click"
                CausesValidation="False" />
        </asp:Panel>
        <div class="idFiltro clearfix">
            <h2>Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="gvObjeto" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" PageSize="500" DataKeyNames="Id" CellPadding="4"
                    EmptyDataText="Não existe dados." ForeColor="Black" GridLines="Both"
                    AllowSorting="True" OnSorting="gvObjeto_Sorting" OnRowDeleting="gvObjeto_RowDeleting" OnRowEditing="gvObjeto_RowEditing"
                    OnPageIndexChanging="gvObjeto_PageIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True"
                            SortExpression="Id" Visible="True">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nome" HeaderText="Nome" ReadOnly="True"
                            SortExpression="Nome" Visible="True">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Visível" SortExpression="visivel">
                            <ItemTemplate>
                                <%# ((Modelos.SegmentoProduto)Container.DataItem).Visivel?"Sim":"Não" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Ordem" HeaderText="Ordem" ReadOnly="True"
                            SortExpression="Ordem" Visible="True">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Image"
                            EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" ShowEditButton="True"
                            UpdateImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" HeaderStyle-BorderWidth="0px"
                            ItemStyle-BorderWidth="0px" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo apagar este segmento, apagando ele e todos os itens relacionados?')" CommandName="Delete"></asp:ImageButton>
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

