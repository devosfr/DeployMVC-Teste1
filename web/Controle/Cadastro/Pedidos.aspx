<%@ Page Title="" Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true"
    CodeFile="Pedidos.aspx.cs" Inherits="Controle_Cadastro_produtos" %>

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
    <asp:HiddenField ID="hfQtd" Value="0" runat="server" />
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 class="TituloPagina">Pedidos
    </h1>
    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <ul>
            <li>
                <span class="nomeCampo">ID:</span>
                <asp:TextBox ID="txtIdPedido" runat="server"></asp:TextBox>
            </li>

            <li>
                <span class="nomeCampo">Cliente:</span>
                <asp:DropDownList ID="ddlNomeCliente" runat="server" Style="width: 300px;">
                </asp:DropDownList>
            </li>
            <li>
                <span class="nomeCampo">Valor Total:</span>
                <asp:TextBox ID="txtValorTotal" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Situação</span>
                <asp:DropDownList ID="ddlSituacao" runat="server" Width="391px" AutoCompleteType="Disabled">
                </asp:DropDownList>
            </li>
            <li>
                <span class="nomeCampo">Data do Pedido</span>
                <asp:TextBox ID="txtDataPedido" runat="server" Width="391px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Tipo de Frete:</span>
                <asp:TextBox ID="txtTipoFrete" runat="server" Width="391px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Preço do Frete:</span>
                <asp:TextBox ID="txtPrecoFrete" runat="server" Width="391px" CssClass="decimal" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <%--<li>
                <span class="nomeCampo">Rastreamento:</span>
                <asp:TextBox ID="txtRastreamento" runat="server" CssClass="" Style="width: 300px;" AutoCompleteType="Disabled"></asp:TextBox>
            </li>--%>
            <%--<li>
                <span class="nomeCampo">Cupom Utilizado:</span>
                <asp:TextBox ID="txtCupom" runat="server" CssClass="" Style="width: 300px;" AutoCompleteType="Disabled"></asp:TextBox>
            </li>--%>
            <%--<li>
                <span class="nomeCampo">Tipo de Pagamento:</span>
                <asp:DropDownList ID="ddlTipoPagamento" runat="server" Width="391px" AutoCompleteType="Disabled">
                </asp:DropDownList>
            </li>--%>
            <li runat="server" id="liBanco">
                <span class="nomeCampo">Banco:</span>
                <asp:TextBox ID="txtBanco" runat="server" CssClass="" Style="width: 300px;" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li runat="server" id="liInformacoes">
                <span class="nomeCampo">Informaçoes:</span>
                <asp:TextBox ID="txtInformacoes" runat="server" TextMode="MultiLine" Rows="8" CssClass="" Style="width: 300px;" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>

                <a runat="server" id="linkComprovante" target="_blank">Ver Comprovante</a>
            </li>
            <li>
                <asp:GridView ID="gvItensPedido" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                    OnRowEditing="TaskGridView_RowEditing" OnRowCancelingEdit="TaskGridView_RowCancelingEdit"
                    OnRowUpdating="TaskGridView_RowUpdating" CellPadding="3" GridLines="Vertical"
                    Width="700px" OnRowDeleting="TaskGridView_RowDeleting" Style="font-family: 'Arial'; font-size: 12px"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="0px" HeaderStyle-BorderWidth="0px" RowStyle-BorderWidth="0px" EditRowStyle-BorderWidth="0px"
                    EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id"
                            Visible="false" ReadOnly="true" HeaderStyle-BorderWidth="0px" ItemStyle-BorderWidth="0px" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <%# ((ItemPedido)Container.DataItem).Pedido.Id %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID do Produto" Visible="true" InsertVisible="true">
                            <ItemTemplate>
                                <%# ((ItemPedido)Container.DataItem).Produto.Id %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Produto" InsertVisible="true">
                            <ItemTemplate>
                                <%# ((ItemPedido)Container.DataItem).Produto.Nome %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tamanho" InsertVisible="true">
                            <ItemTemplate>
                                <%# ((ItemPedido)Container.DataItem).Tamanho %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cor" InsertVisible="true">
                            <ItemTemplate>
                                <%# ((ItemPedido)Container.DataItem).Cor != null ? ((ItemPedido)Container.DataItem).Cor.Nome : "Nenhuma" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ref." InsertVisible="true">
                            <ItemTemplate>
                                <%# ((ItemPedido)Container.DataItem).Produto.Referencia %>
                            </ItemTemplate>
                        </asp:TemplateField>

                      <asp:TemplateField HeaderText="R$ unit" InsertVisible="true">
                            <ItemTemplate>
                                <%# getPreco(((ItemPedido)Container.DataItem)) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Quantidade" HeaderText="Qtd" SortExpression="Quantidade"
                            Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderWidth="0px"
                            ItemStyle-BorderWidth="0px">
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Total" InsertVisible="true">
                            <ItemTemplate>
                                <%# getPreco(((ItemPedido)Container.DataItem)) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/Controle/comum/img/cancelar.gif" EditImageUrl="~/Controle/comum/img/icoEditar.jpg"
                            ShowEditButton="True" UpdateImageUrl="~/Controle/comum/img/icoEditar.jpg" HeaderStyle-BorderWidth="0px"
                            ItemStyle-BorderWidth="0px" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Controle/comum/img/icoExcluir.jpg" ShowDeleteButton="True"
                            HeaderStyle-BorderWidth="0px" ItemStyle-BorderWidth="0px" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="Black" />
                    <AlternatingRowStyle BackColor="#F8F8FB" />
                </asp:GridView>
            </li>
        </ul>
        <div>
            <asp:Button ID="btnAlterar" CausesValidation="true" runat="server" Text="Alterar"
                OnClick="btnAlterar_Click" CssClass="EstiloBotao" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                CausesValidation="False" CssClass="EstiloBotao" />
            <asp:Button ID="btnExportarPedido" CausesValidation="false" runat="server" Text="Exportar Pedidos (PDF)"
                OnClick="btnExportarPedido_Click" CssClass="EstiloBotao" />
           
        </div>
    </div>
    <h1 id="tituloBusca" class="TituloSecao">Busca
    </h1>
    <div id="divLista" runat="server" clientidmode="Static">
        <asp:Panel runat="server" DefaultButton="btnPesquisar">
            <ul class="listaCampoBusca">
                <li>
                    <span class="nomeCampo">ID:</span>
                    <asp:TextBox runat="server" ID="txtBuscaID"></asp:TextBox>
                </li>
                <li>
                    <span class="nomeCampo">Cliente:</span>
                    <asp:DropDownList ID="ddlBuscaCliente" runat="server" Width="150px">
                    </asp:DropDownList>
                </li>
                <li>
                    <span class="nomeCampo">Formas de Pagamento:</span>
                    <asp:DropDownList ID="ddlBuscaFormasDePagamento" runat="server" Width="150px">
                    </asp:DropDownList>
                </li>
                <li>
                    <span class="nomeCampo">Status:</span>
                    <asp:DropDownList ID="ddlBuscaStatus" runat="server" Width="391px" AutoCompleteType="Disabled">
                    </asp:DropDownList>
                </li>
            </ul>
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CausesValidation="False"
                OnClick="btnPesquisar_Click" CssClass="EstiloBotao" />
             <asp:Button ID="btnExcel" CausesValidation="false" runat="server" Text="Exportar Pedido (Excel)"
                 OnClick="btnExcel_Click" CssClass="EstiloBotao" />
        </asp:Panel>





        <div class="idFiltro clearfix">
            <h2 class="TituloResultadoPesquisa">Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="gvDados" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="500" DataKeyNames="Id" CellPadding="4" EmptyDataText="Não existe dados."
                    ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="gvDados_Sorting"
                    OnRowEditing="gvDados_RowEditing" OnRowCancelingEdit="gvDados_RowCancelingEdit"
                    OnRowUpdating="gvDados_RowUpdating" OnRowDataBound="gvDados_RowDataBound" OnPageIndexChanging="gvDados_PageIndexChanging"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Codigo" InsertVisible="False" ReadOnly="True"
                            SortExpression="Id" />
                        <asp:TemplateField HeaderText="Cliente" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Pedido)Container.DataItem).Cliente.Nome %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Total" InsertVisible="false">
                            <ItemTemplate>
                                <%# ((Pedido)Container.DataItem).GetTotalPedido().ToString("C") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" InsertVisible="False" SortExpression="Status">
                            <ItemTemplate>
                                <%# ((Pedido)Container.DataItem).GetStatusPedido() %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStatusPedido" runat="server" Width="391px" AutoCompleteType="Disabled">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DataPedido" HeaderText="Data do Pedido" InsertVisible="False"
                            ReadOnly="True" SortExpression="data" DataFormatString="{0:dd/MM/yyyy}" />
                        <%--                                <asp:BoundField DataField="DescricaoSegPai" HeaderText="Tela" InsertVisible="False"
									ReadOnly="True" SortExpression="DescricaoSegPai" />
								<asp:BoundField DataField="DescricaoSegFilho" HeaderText="Segmento" InsertVisible="False"
									ReadOnly="True" SortExpression="DescricaoSegFilho" />--%>
                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/Controle/comum/img/cancelar.gif" EditImageUrl="~/Controle/comum/img/icoEditar.jpg"
                            ShowEditButton="True" UpdateImageUrl="~/Controle/comum/img/icoEditar.jpg" HeaderText="Editar Status"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" />
                        <asp:TemplateField HeaderText="Detalhes" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hplEditar" runat="server" NavigateUrl='<%# Eval("id", "Pedidos.aspx?Codigo={0}") %>'>
                                    <asp:Image ID="imgEditar" ToolTip="Editar" ImageUrl="~/Controle/comum/img/icoEditar.jpg"
                                        runat="server" />
                                </asp:HyperLink>
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
