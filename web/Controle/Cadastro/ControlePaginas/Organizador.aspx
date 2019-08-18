<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="Organizador.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        $(document).ready(
				function () {
				    $('#divDados').show();
				    $('#tituloCadastro').css('color', '#FFF');
				});


    </script>
    <link href="<%= MetodosFE.BaseURL %>/Controle/UserControl/ControlePaginas/bower_components/bootstrap.min.css" rel="stylesheet" />
    <link href="<%= MetodosFE.BaseURL %>/Controle/UserControl/ControlePaginas/bower_components/angular-ui-tree.min.css" rel="stylesheet" />
    <link href="<%= MetodosFE.BaseURL %>/Controle/UserControl/ControlePaginas/bower_components/demo.css" rel="stylesheet" />

    <asp:HiddenField ID="hfSecao" ClientIDMode="Static" runat="server" />
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 class="TituloPagina">
        <asp:Literal runat="server" ID="litTitulo"></asp:Literal>
    </h1>

    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <div>

            <ul>
                <li ng-app="groupsApp">

                    <div class="container" ng-controller="groupsCtrl">
                        <h1 class="page-header">Controle de Páginas</h1>

                        <div ng-include="overlayEspera" style="position: absolute; z-index: 99; top: 0; left: 0; width: 100%; height: 100%;"></div>
                        <div class="row">
                            <div class="col-lg-12" ui-tree="options">
                                <ol ui-tree-nodes ng-model="groups" type="group">
                                    <li ng-repeat="group in groups" ui-tree-node>
                                        <div class="group-title angular-ui-tree-handle" ng-show="!group.editing">
                                            <a href="" class="btn btn-danger btn-xs pull-right" style="height:18px;" ng-show="group.type == 'group'" data-nodrag="" ng-click="removeGroup(group)"><i class="glyphicon glyphicon-remove"></i></a>
                                            <a href="" class="btn btn-primary btn-xs pull-right" style="height:18px;" ng-show="group.type == 'group'" data-nodrag="" ng-click="editGroup(group)"><i class="glyphicon glyphicon-pencil"></i></a>
                                            <div>&nbsp;{{group.nome}}</div>
                                        </div>
                                        <div class="group-title angular-ui-tree-handle" nodrag ng-show="group.editing">
                                            <form class="form-inline" role="form">
                                                <div class="form-group">
                                                    <label class="sr-only" for="groupName">Nome do Grupo</label>
                                                    <input type="text" class="form-control" placeholder="Group name" ng-model="group.nome">
                                                </div>
                                                <button type="submit" onclick="return false;" class="btn btn-default" ng-click="saveGroup(group)">Save</button>
                                                <button type="submit" onclick="return false;" class="btn btn-default" ng-click="cancelEditingGroup(group)">Cancel</button>
                                            </form>
                                        </div>
                                        <ol ui-tree-nodes ng-model="group.paginas" type="category">
                                            <li ng-repeat="category in group.paginas" ui-tree-node>
                                                <div class="category-title angular-ui-tree-handle">
                                                    <a href="" class="btn btn-danger btn-xs pull-right" style="height:18px;" ng-show="group.type == 'group'" data-nodrag="" ng-click="removeCategory(group, category)"><i class="glyphicon glyphicon-remove"></i></a>
                                                    <div>
                                                        &nbsp;{{category.nome}}
                 
                                                    </div>
                                                </div>
                                            </li>
                                        </ol>
                                    </li>
                                </ol>
                                <ol class="angular-ui-tree-nodes">
                                    <li class="angular-ui-tree-node">
                                        <div class="group-title angular-ui-tree-handle">
                                            <form class="form-inline" role="form">
                                                <div class="form-group">
                                                    <label class="sr-only" for="groupName">Group name</label>
                                                    <input type="text" class="form-control" id="groupName" placeholder="Nome do Grupo">
                                                </div>
                                                <button type="submit" class="btn btn-default" ng-click="addGroup()">Adicionar Grupo</button>
                                            </form>
                                        </div>
                                    </li>
                                    <li>
                                        <button type="submit" onclick="return false;" class="btn btn-default" ng-click="buscar()">Recarregar</button>
                                        <button type="submit" onclick="return false;" class="btn btn-default" ng-click="salvar()">Salvar</button>
                                    </li>
                                </ol>
                            </div>
                        </div>

                    </div>

                    <!-- JavaScript -->
                    <!--[if IE 8]>
  <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
  <script src="http://cdnjs.cloudflare.com/ajax/libs/es5-shim/3.4.0/es5-shim.min.js"></script>
  <![endif]-->
                    <script src="<%= MetodosFE.BaseURL %>/Controle/UserControl/ControlePaginas/bower_components/angular.min.js"></script>
                    <script src="<%= MetodosFE.BaseURL %>/Controle/UserControl/ControlePaginas/bower_components/angular-ui-tree.min.js"></script>
                    <script src="<%= MetodosFE.BaseURL %>/Controle/UserControl/ControlePaginas/groups.js"></script>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>

