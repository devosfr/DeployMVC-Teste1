(function () {
    'use strict';

    angular.module('groupsApp', ['ui.tree'])
    .factory("GroupsService", function ($http, $q, $rootScope) {

        var enderecoServico = "/Webservices/ControleGrupos.asmx/";
        var enderecoServico2 = "/Webservices/ControleProdutoCor.asmx/";
        var enderecoServico3 = "/Webservices/ControleProdutoSabor.asmx/";

        return {
            getGrupos: function () {
                // Get the deferred object
                var deferred = $q.defer();
                // Initiates the AJAX call
                $http.post(enderecoServico + 'getGrupos', {}).success(deferred.resolve).error(deferred.reject);
                // Returns the promise - Contains result once request completes
                return deferred.promise;
            },
            setGrupos: function (lista) {
                // Get the deferred object
                var deferred = $q.defer();
                // Initiates the AJAX call
                $http.post(enderecoServico + 'setGrupos', { gruposJS: lista }).success(deferred.resolve).error(deferred.reject);
                // Returns the promise - Contains result once request completes
                return deferred.promise;
            }

        }

    })
    .controller('groupsCtrl', function ($scope, $log, GroupsService) {

        $scope.info = "";
        $scope.groups = [];

        $scope.buscar = function () {
            carregar();
        };

        $scope.mostraOverlay = function () {
            $scope.overlayEspera = '/css/overlay.html';
        };
        $scope.escondeOverlay = function () {
            $scope.overlayEspera = null;
        };

        function carregar()
        {
            $scope.mostraOverlay();
            GroupsService.getGrupos().then(function (lista) {
                var listaP = $.parseJSON(lista.d);
                $scope.groups = listaP;
                $scope.escondeOverlay();

            }, function () {
                $scope.escondeOverlay();
                //$scope.pedido.mensagem = ('Problemas ao carregar as formas de entrega.');
            }
);
        }

        var init = function ()
        {
            carregar();
        }

        init();

        $scope.salvar = function () {
            $scope.mostraOverlay();
            GroupsService.setGrupos($scope.groups).then(function (lista) {
                var listaP = $.parseJSON(lista.d);
                //$scope.groups = listaP;
                $scope.buscar();
                $scope.escondeOverlay();

            }, function () {
                //$scope.pedido.mensagem = ('Problemas ao carregar as formas de entrega.');
                $scope.escondeOverlay();
            }
  );
            //=  ;
        };



        $scope.addGroup = function () {
            var groupName = document.getElementById("groupName").value;
            if (groupName.length > 0) {
                var group = { nome: groupName, ordem: $scope.groups.length, type: "group", paginas: [] };
                $scope.groups.push(group);
                //Groups.$add({
                //  name: groupName,
                //  type: "group",
                //  paginas: [],
                //  ordem: $scope.groups.length
                //});
                document.getElementById("groupName").value = '';
            }
        };

        $scope.editGroup = function (group) {
            group.editing = true;
        };

        $scope.cancelEditingGroup = function (group) {
            group.editing = false;
        };

        $scope.saveGroup = function (group) {
            group.editing = false;
        };

        $scope.removeGroup = function (group) {
            if (window.confirm('Deseja mesmo remover este grupo?')) {
                //group.destroy();
                var groupSemPagina;

                var groups = $scope.groups;

                var cont = 0;
                for (var grupoObj in groups) {
                    if (groups[cont].type === "Sem Paginas") {
                        for (var i = 0; i < group.paginas.length; i++) {
                            groups[cont].paginas.push(group.paginas[i]);
                        }
                        break;
                    }
                    cont++;
                }
                groups = groups.filter(function (el) {
                    return el.nome != group.nome;
                });
                $scope.groups = groups;

            }
        };

        $scope.removeCategory = function (group, category) {
            if (window.confirm('Deseja mesmo retirar esta página?'))
            {

                group.paginas = group.paginas.filter(function (el) {
                    return el.id != category.id;
                });

                var groups = $scope.groups;

                var cont = 0;
                for (var grupoObj in groups) {
                    if (groups[cont].type === "Sem Paginas") {
                        groups[cont].paginas.push(category);
                        break;
                    }
                    cont++;
                }

                $scope.groups = groups;

            }
        };

        $scope.options = {
            accept: function (sourceNode, destNodes, destIndex) {
                var data = sourceNode.$modelValue;
                var destType = destNodes.$element.attr('type');
                return (data.type == destType); // only accept the same type
            },
            dropped: function (event) {
                console.log(event);
                var sourceNode = event.source.nodeScope;
                var destNodes = event.dest.nodesScope;
                // update changes to server
                if (destNodes.isParent(sourceNode)
                  && destNodes.$element.attr('type') == 'category') { // If it moves in the same group, then only update group
                    var group = destNodes.$nodeScope.$modelValue;
                    group.editing = false;
                } else { // save all
                    //$scope.saveGroups();
                }
            }
            //,
            //beforeDrop: function (event) {
            //    if (!window.confirm('Are you sure you want to drop it here?')) {
            //        event.source.nodeScope.$$apply = false;
            //    }
            //}
        };


    });

})();
