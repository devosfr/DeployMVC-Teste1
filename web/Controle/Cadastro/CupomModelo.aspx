<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="CupomModelo.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        $(document).ready(
				function () {

				});


    </script>
    <asp:HiddenField ID="hfSecao" ClientIDMode="Static" runat="server" />
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 class="TituloPagina" >
        <asp:Literal runat="server" ID="litTitulo"></asp:Literal>
    </h1>

    <h1 id="tituloCadastro" class="TituloSecao" style="color:white;">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">

        <ul>
<%--            <li>
                <span class="nomeCampo">ID:</span>
                <asp:TextBox ID="txtID" runat="server" Width="63px" Enabled="False"></asp:TextBox>
            </li>--%>
            
            <li>
                <asp:GridView runat="server" ID="gvDescontos" AllowPaging="false" 
                    OnRowDeleting="gvDescontos_RowDeleting" DataKeyNames="Id" AutoGenerateColumns="false" >

                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True"
                            SortExpression="Id" Visible="True">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Produto" SortExpression="Produto.Nome">
                            <ItemTemplate>
                                <%# ((DescontoCupom)Container.DataItem).Produto.Referencia + " - "+((DescontoCupom)Container.DataItem).Produto.Nome %>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            
                        <asp:TemplateField HeaderText="Tipo de Desconto" SortExpression="Tipo">
                            <ItemTemplate>
                                <%# ((DescontoCupom)Container.DataItem).Tipo == 1 ? "Fixo" : "Porcentagem"%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Desconto" SortExpression="Desconto">
                            <ItemTemplate>
                                <%# ((DescontoCupom)Container.DataItem).Tipo == 1? ((DescontoCupom)Container.DataItem).Desconto.ToString("C") : ((DescontoCupom)Container.DataItem).Desconto.ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Comissão" SortExpression="Comissao">
                            <ItemTemplate>
                                <%# ((DescontoCupom)Container.DataItem).Comissao.ToString("C") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo">
                            <ItemTemplate>
                                <%# ((DescontoCupom)Container.DataItem).Ativo ? "Sim" : "Não"%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo excluir este desconto?')" CommandName="Delete"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />

                    
                </asp:GridView>

                <ul class="lista-filtro clearfix">
                    <li>
                        <span class="nomeCampo">Produto:</span>
                        <asp:DropDownList runat="server" ID="ddlProdutos"></asp:DropDownList>
                    </li>
                    <li>
                        <span class="nomeCampo">Tipo:</span>
                        <asp:DropDownList runat="server" ID="ddlTipoDesconto">
                            <asp:ListItem Value="1">Fixo</asp:ListItem>
                            <asp:ListItem Value="2">Porcentagem</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <span class="nomeCampo">Desconto:</span>
                        <asp:TextBox runat="server" ID="txtDesconto" CssClass="decimal"></asp:TextBox>
                    </li>
                    <li>
                        <span class="nomeCampo">Comissão:</span>
                        <asp:TextBox runat="server" ID="txtComissao" CssClass="decimal"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Button runat="server" ID="btnAdicionarDesconto" Text="Adicionar" OnClick="btnAdicionarDesconto_Click" />
                    </li>
                </ul>
            </li>
        </ul>
        <div>

        </div>
    </div>
</asp:Content>

