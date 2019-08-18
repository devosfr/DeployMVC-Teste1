<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="Fretes.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Controle de Fretes" %>

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
    <h1 class="TituloPagina">Opçoes de Frete
    </h1>

    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <ul>
            <li>
                <span class="nomeCampo">ID:</span>
                <asp:TextBox ID="txtId" runat="server" Width="63px" AutoPostBack="True" Enabled="False"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Nome:</span>
                <asp:TextBox ID="txtNome" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Ativo:</span>
                <asp:CheckBox runat="server" ID="cbAtivo" />
            </li>
            <li>
                <span class="nomeCampo">Preço:</span>
                <asp:TextBox ID="txtPreco" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Prazo (Dias):</span>
                 <asp:TextBox ID="txtDias" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>
            </li>
           <%-- <li>
                <span class="nomeCampo">Peso Máximo:</span>
                <asp:TextBox ID="txtPesoMaximo" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Valor Mínimo de Compra:</span>
                <asp:TextBox ID="txtValorMinimo" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>
            </li>--%>
            <li>
                <span class="nomeCampo">Estado:</span>
                <asp:DropDownList runat="server" ID="ddlEstado" AutoPostBack="true" OnTextChanged="ddlEstado_TextChanged">
                </asp:DropDownList>
            </li>
            <li>
                <span class="nomeCampo">Cidade:</span>
                <asp:DropDownList runat="server" ID="ddlCidade">
                </asp:DropDownList>
            </li>
            <li>
                <span class="nomeCampo">Bairro:</span>
                <asp:TextBox ID="txtBairro" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>
            </li>
        </ul>
        <div>

            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" CssClass="EstiloBotao"
                OnClick="btnAlterar_Click" />
            <asp:Button ID="btnSalvar" runat="server" Text="Inserir" CssClass="EstiloBotao"
                OnClick="btnSalvar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="EstiloBotao"
                OnClick="btnCancelar_Click" />
        </div>

    </div>
    <h1 id="tituloBusca" class="TituloSecao">Busca
    </h1>
    <div id="divLista" runat="server" clientidmode="Static">
        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar"
            OnClick="btnPesquisar_Click" CssClass="EstiloBotao" />
        <div class="idFiltro clearfix">
            <h2>Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="false"
                    AutoGenerateColumns="False" DataKeyNames="id" CellPadding="4"
                    EmptyDataText="Não existe dados." ForeColor="Black" GridLines="Both"
                    AllowSorting="True" OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                    OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True"
                            SortExpression="id" Visible="True">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="Nome" SortExpression="nome">
                            <ItemTemplate>
                                <%# ((Modelos.OpcaoFreteLocalidade)Container.DataItem).Nome %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Preço" SortExpression="Preco">
                            <ItemTemplate>
                                <%# ((Modelos.OpcaoFreteLocalidade)Container.DataItem).Preco.ToString("C") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo">
                            <ItemTemplate>
                                <%# ((Modelos.OpcaoFreteLocalidade)Container.DataItem).Ativo %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ButtonType="Image"
                            EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" ShowEditButton="True"
                            UpdateImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" HeaderStyle-BorderWidth="0px"
                            ItemStyle-BorderWidth="0px" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                            ShowDeleteButton="True" HeaderStyle-BorderWidth="0px" ItemStyle-BorderWidth="0px" />
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

