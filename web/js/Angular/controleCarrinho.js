var interfaceModule = angular.module("carrinhoApp", ['ngResource', 'ngAnimate']);

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

interfaceModule.controller("CarrinhoController", ['$scope', '$filter', '$timeout', 'CarrinhoService', 'CupomService', function ($scope, $filter, $timeout, CarrinhoService, CupomService) {
    $scope.carrinho = [];
    $scope.carregado = null;
    $scope.cupom = null;
    $scope.descontoCupom = null;
    $scope.descontoPercentual = null;
    $scope.statusCupom = null;
    $scope.mensagem = {};
    $scope.validadeCupom = null;

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/css/overlay.html';
    };

    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };

    $scope.carregarLista = function () {
        $scope.mostraOverlay();
        CarrinhoService.query(
        function (carrinho2) {
            $scope.carrinho = carrinho2;

            for (var i = 0; i < $scope.carrinho.length; i++) {
                $scope.carrinho[i].ImagemProduto = $scope.carrinho[i].ImagemProduto.replace("/imagenslq/", "/imagenshq/");

            }


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
        CupomService
            .query(
                function (cupom) {
                    if (cupom.Codigo) {                      

                        if (cupom.desconto) {
                            $scope.descontoCupom = cupom.desconto;
                        }
                        if (cupom.descontoPercentual) {
                            $scope.descontoPercentual = cupom.descontoPercentual;
                        }
                        if (cupom.Validade) {
                            $scope.validadeCupom = cupom.Validade;
                                                        
                        }
                     
                        $scope.cupom = cupom;
                        $scope.programaAtualizacao();
                    }

                    else
                        $scope.cupom = new CupomService();
                        console.log("Cupom carregado.");
                        $scope.totalDesconto();
                        $scope.carregarLista();
                        $scope.programaAtualizacao();
                        console.log(cupom);
                },
                function (erro) {
                    $scope.mensagem =
                    {
                        erro: "Não foi possível carregar o cupom."
                    };
                    console.log(erro);
                }
            );

        $scope.carregarLista();
    };

    $scope.init();

    $scope.carregarCupom = function () {
 
        $scope.mensagem = {};
        CupomService
            .query(
                function (cupom) {
                    if (cupom.Codigo) {

                        if (cupom.desconto) {
                            $scope.descontoCupom = cupom.desconto;
                        }
                        if (cupom.descontoPercentual) {
                            $scope.descontoPercentual = cupom.descontoPercentual;
                        }
                        if (cupom.Validade) {
                            $scope.validadeCupom = cupom.Validade;

                        }

                        $scope.cupom = cupom;

                    }

                    else
                        $scope.cupom = new CupomService();
                    console.log("Cupom carregado.");
                    $scope.totalDesconto();
                    $scope.carregarLista();
                    $scope.programaAtualizacao();
                    console.log(cupom);
                },
                function (erro) {
                    $scope.mensagem =
                    {
                        erro: "Não foi possível carregar o cupom."
                    };
                    console.log(erro);
                }
            );

        $scope.carregarLista();
    };

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

    $scope.valorSubTotal = function () {
        var total = 0;
        angular.forEach($scope.carrinho, function (item) {
            total += item.Quantidade * item.PrecoProduto;
        }, total);

        return total;
    };

    $scope.totalDesconto = function () {
        descontoTotal = 0;

        if ($scope.descontoCupom > 0) {
            descontoTotal = $scope.descontoCupom;
        }
        if ($scope.descontoPercentual > 0) {
            descontoTotal = $scope.valorSubTotal() * ($scope.descontoPercentual / 100);
        }

        return descontoTotal;
    };

    $scope.defineCupom = function () {
        $scope.mostraOverlay();
        $scope.statusCupom = null;
        $scope.mensagem = {};
        $scope.cupom.$update(
                function () {
                 
                    $scope.mensagem =
                    {
                        sucesso: "Cupom definido com sucesso."
                    };
                   
                
                    console.log("Cupom definido.");
              
                  
                },
                function (erro) {
                    $scope.mensagem =
                    {
                        erro: "Não foi possível carregar o cupom:" + erro.data
                    };

                    console.log(erro);

                    $scope.escondeOverlay();

                }
            );
        $scope.carregarLista();
        $scope.programaAtualizacao();
        $scope.carregarCupom();
        $scope.carregarCupom();
        $scope.carregarCupom();
    }

    $scope.atualizaCarrinho = function () {
        $scope.mostraOverlay();
        CarrinhoService.update({}, $scope.carrinho, function () {
            $scope.carregarLista();
        }, function (erro) {
            $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
            $scope.escondeOverlay();
        });
    };


}]);

interfaceModule.factory("CarrinhoService", ['$resource', function ($resource) {
    return $resource('/api/carrinho/', {},
        {
            'query': { method: 'GET', isArray: true },
            "update": { method: "POST", isArray: true }
        });
}]);

interfaceModule.factory("CupomService", ['$resource', function ($resource) {
    return $resource('/api/cupom/', {},
        {
            'query': { method: 'GET' },
            "update": { method: "POST", }
        });
}]);
