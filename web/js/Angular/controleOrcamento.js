var interfaceModule = angular.module("orcamentoApp", ['ngResource', 'ngAnimate']);

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

interfaceModule.controller("OrcamentoController", ['$scope', '$filter', '$timeout', 'OrcamentoService', function ($scope, $filter, $timeout, OrcamentoService) {
    $scope.carrinho = [];
    $scope.carregado = null;

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/css/overlay.html';
    };
    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };

    $scope.carregarLista = function () {
        $scope.mostraOverlay();
        OrcamentoService.query(
        function (carrinho2) {
            $scope.carrinho = carrinho2;
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

    $scope.incrementaQuantidade = function (item) {
        item.Quantidade++;
        $scope.programaAtualizacao();
    };

    $scope.decrementaQuantidade = function (item) {
        if (item.Quantidade > 0) {
            item.Quantidade--;
            $scope.programaAtualizacao();
        }

    };

    $scope.excluir = function (index) {
        $scope.carrinho.splice(index, 1);
        if (filterTextTimeout) $timeout.cancel(filterTextTimeout);
        $scope.atualizaCarrinho();
    };

    $scope.valorTotal = function () {
        var total = 0;
        angular.forEach($scope.carrinho, function (item) {
            total += item.Quantidade * item.PrecoProduto;
        }, total);

        return total;
    };



    $scope.atualizaCarrinho = function () {
        $scope.mostraOverlay();
        OrcamentoService.update({}, $scope.carrinho, function () {
            $scope.carregarLista();
        }, function (erro) {
            $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
            $scope.escondeOverlay();
        });
    };
}]);

interfaceModule.factory("OrcamentoService", ['$resource', function ($resource) {
    return $resource('/api/orcamento/', {},
        {
            'query': { method: 'GET', isArray: true },
            "update": { method: "POST", isArray: true }
        });
}]);