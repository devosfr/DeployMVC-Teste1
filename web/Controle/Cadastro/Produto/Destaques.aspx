<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="Destaques.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Destaques" %>

<%@ Register Src="~/Controle/Cadastro/uplDestaque.ascx" TagPrefix="uc1" TagName="uplDestaque" %>


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
    <script type="text/javascript">
        var TREEVIEW_ID = "<%=tvProdutos.ClientID%>"; //the ID of the TreeView control
        //the constants used by GetNodeIndex()
        var LINK = 0;
        var CHECKBOX = 1;

        //this function is executed whenever user clicks on the node text
        function ToggleCheckBox(senderId) {
            var nodeIndex = GetNodeIndex(senderId, LINK);
            var checkBoxId = TREEVIEW_ID + "n" + nodeIndex + "CheckBox";
            var checkBox = document.getElementById(checkBoxId);
            checkBox.checked = !checkBox.checked;

            ToggleChildCheckBoxes(checkBox);
            //ToggleParentCheckBox(checkBox);
        }

        //checkbox click event handler
        function checkBox_Click(eventElement) {
            ToggleChildCheckBoxes(eventElement.target);
            //ToggleParentCheckBox(eventElement.target);
        }

        //returns the index of the clicked link or the checkbox
        function GetNodeIndex(elementId, elementType) {
            var nodeIndex;
            if (elementType == LINK) {
                nodeIndex = elementId.substring((TREEVIEW_ID + "t").length);
            }
            else if (elementType == CHECKBOX) {
                nodeIndex = elementId.substring((TREEVIEW_ID + "n").length, elementId.indexOf("CheckBox"));
            }
            return nodeIndex;
        }

        //checks or unchecks the nested checkboxes
        function ToggleChildCheckBoxes(checkBox) {
            var postfix = "n";
            var childContainerId = TREEVIEW_ID + postfix + GetNodeIndex(checkBox.id, CHECKBOX) + "Nodes";
            var childContainer = document.getElementById(childContainerId);
            if (childContainer) {
                var childCheckBoxes = childContainer.getElementsByTagName("input");
                for (var i = 0; i < childCheckBoxes.length; i++) {
                    childCheckBoxes[i].checked = checkBox.checked;
                }
            }
        }

        //unchecks the parent checkboxes if the current one is unchecked
        function ToggleParentCheckBox(checkBox) {
            if (checkBox.checked == false) {
                var parentContainer = GetParentNodeById(checkBox, TREEVIEW_ID);
                if (parentContainer) {
                    var parentCheckBoxId = parentContainer.id.substring(0, parentContainer.id.search("Nodes")) + "CheckBox";
                    if ($get(parentCheckBoxId) && $get(parentCheckBoxId).type == "checkbox") {
                        $get(parentCheckBoxId).checked = false;
                        ToggleParentCheckBox($get(parentCheckBoxId));
                    }
                }
            }
        }

        //returns the ID of the parent container if the current checkbox is unchecked
        function GetParentNodeById(element, id) {
            var parent = element.parentNode;
            if (parent == null) {
                return false;
            }
            if (parent.id.search(id) == -1) {
                return GetParentNodeById(parent, id);
            }
            else {
                return parent;
            }
        }
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
                <asp:TextBox ID="txtID" runat="server" Width="63px" Enabled="False"></asp:TextBox>
            </li>
            <li>
                <span class="nomeCampo">Nome:</span>
                <asp:TextBox ID="txtNome" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>

            </li>
            <li>
                <span class="nomeCampo">Prioridade:</span>
                <asp:TextBox ID="txtPrioridade" runat="server" CssClass="numero" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>

            </li>
            <li>
                <span class="nomeCampo">Visível:</span>
                <asp:CheckBox runat="server" ID="cbVisivel" />
            </li>
            <li>
                <asp:TreeView CollapseImageUrl="~/Controle/comum/img/ic_menos.gif" NoExpandImageUrl="~/Controle/comum/img/tvSubItemAtivo.gif" ExpandImageUrl="~/Controle/comum/img/ic_mais.gif" runat="server" ShowCheckBoxes="All" ID="tvProdutos"></asp:TreeView>
            </li>
            <li>
                <uc1:uplDestaque runat="server" ID="uplDestaque" />
            </li>
        </ul>
        <div>
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" CssClass="EstiloBotao" />
            <asp:Button ID="btnSalvar" runat="server" Text="Inserir" CssClass="EstiloBotao" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="EstiloBotao" OnClick="btnCancelar_Click" />
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
                    <asp:TextBox ID="txtBuscaNome" runat="server" Width="220px" AutoCompleteType="Disabled"></asp:TextBox>
                </li>
            </ul>

            <asp:Button ID="btnPesquisar" CssClass="EstiloBotao" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click"
                CausesValidation="False" />
        </asp:Panel>
        <div class="idFiltro clearfix">
            <h2>Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="gvObjeto" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" DataKeyNames="Id" CellPadding="4"
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
                                <%# ((Modelos.Destaque)Container.DataItem).Visivel ? "Sim" : "Não"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image"
                            EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" ShowEditButton="True"
                            UpdateImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" HeaderStyle-BorderWidth="0px"
                            ItemStyle-BorderWidth="0px" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo apagar este destaque?')" CommandName="Delete"></asp:ImageButton>
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
    <script type="text/javascript">
        var links = document.getElementsByTagName("a");
        for (var i = 0; i < links.length; i++) {
            if (links[i].className == TREEVIEW_ID + "_0") {
                links[i].href = "javascript:ToggleCheckBox(\"" + links[i].id + "\");";
            }
        }

        var checkBoxes = document.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox") {
                $addHandler(checkBoxes[i], "click", checkBox_Click);
            }
        }
    </script>
</asp:Content>

