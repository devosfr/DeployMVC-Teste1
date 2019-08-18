var interfaceModule = angular.module("produtoApp", ['ngResource', 'ngAnimate', 'ngMask']);

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

interfaceModule.controller("ProdutoController", ['$scope', '$filter', '$timeout', 'FreteProdutoService', function ($scope, $filter, $timeout, FreteProdutoService) {
    $scope.mensagem = {};
    $scope.cep = "";
    $scope.mascaraCEP = "99999-999";
    $scope.fretes = [];
    $scope.idTamanho = "6";
    $scope.mostraOverlay = function (){
        $scope.overlayEspera = '/css/overlay.html';
    };
    $scope.escondeOverlay = function(){
        $scope.overlayEspera = null;
    };

    $scope.pesquisarCEP = function () {
        $scope.mostraOverlay();

        //console.log(idProduto + " - " + $("#ddlTamanho").val()+" - "+$scope.cep);
        FreteProdutoService.query(
            {idProduto: idProduto, idTamanho:$scope.idTamanho,cep:$scope.cep},
        function (carrinho2){
            $scope.fretes = carrinho2;
            console.log(carrinho2);
            console.log("Itens carregados.");
            $scope.escondeOverlay();
        },
        function (erro){
            $scope.mensagem =
            {
                texto: "Não foi possível obter a lista de contatos"
            };
            console.log(erro);
        }
        );
    };
}]);

interfaceModule.factory("FreteProdutoService", ['$resource', function ($resource) {
    return $resource('/api/freteproduto/', {},
        {
            'query': { method: 'GET', isArray: true }
        });
}]);