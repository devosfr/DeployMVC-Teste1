var interfaceModule = angular.module("listaDesejoApp", ['ngResource', 'ngAnimate']);

interfaceModule.directive('ngReallyClick', [function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                var message = attrs.ngReallyMessage;
                if (message && confirm(message)) {
                    scope.$apply(attrs.ngReallyClick);
                }
            });
        }
    }
}]);

interfaceModule.controller("ListaDesejoController", ['$scope', '$filter', '$timeout', 'listaDesejoService', function ($scope, $filter, $timeout, listaDesejoService) {
    $scope.listaDesejo = [];
    $scope.carregado = null;
    $scope.cupom = null;
    $scope.statusCupom = null;
    $scope.mensagem = {};

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/css/overlay.html';
    };
    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };

    $scope.carregarLista = function () {
        $scope.mostraOverlay();
        listaDesejoService.query(
        function (carrinho2) {
            $scope.listaDesejo = carrinho2;
            console.log("Itens carregados.");
            $scope.escondeOverlay();
        },
        function (erro) {
            $scope.mensagem =
            {
                texto: "Não foi possível obter a lista de contatos"
            };
            console.log(erro);
        }
        );
    };

    $scope.init = function () {
        $scope.statusCupom = null;
        $scope.mensagem = {};
    
        $scope.carregarLista();
    };

    $scope.init();

    var filterTextTimeout;

    $scope.programaAtualizacao = function () {
        $scope.carregado = null;
        if (filterTextTimeout) $timeout.cancel(filterTextTimeout);

        filterTextTimeout = $timeout(function () {
            $scope.atualizaCarrinho();
        }, 1250); // delay 750 ms
    };

    
       
    $scope.excluir = function (index) {
        $scope.listaDesejo.splice(index, 1);
        if (filterTextTimeout) $timeout.cancel(filterTextTimeout);
        $scope.atualizaCarrinho();
    };

    $scope.atualizaCarrinho = function () {
        $scope.mostraOverlay();
        listaDesejoService.update({}, $scope.listaDesejo, function () {
            $scope.carregarLista();
        }, function (erro) {
            $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
            $scope.escondeOverlay();
        });
    };
}]);

interfaceModule.factory("listaDesejoService", ['$resource', function ($resource) {
    return $resource('/api/listaDesejo/', {},
        {
            'query': { method: 'GET', isArray: true },
            "update": { method: "POST", isArray: true }
        });
}]);

