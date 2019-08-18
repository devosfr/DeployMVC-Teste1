//var interfaceModule = angular.module("enderecoApp", ['ngResource', 'ngAnimate','ngMask']);

//interfaceModule.directive('ngReallyClick', [function () {
//    return {
//        restrict: 'A',
//        link: function (scope, element, attrs) {
//            element.bind('click', function () {
//                var message = attrs.ngReallyMessage;
//                if (message && confirm(message)) {
//                    scope.$apply(attrs.ngReallyClick);
//                }
//            });
//        }
//    }
//}]);

//interfaceModule.controller("EnderecoController", ['$scope', '$filter', '$timeout', 'EnderecoService', 'EstadoService', 'CidadeService', 'FreteService', 'CarrinhoService', function ($scope, $filter, $timeout, EnderecoService, EstadoService, CidadeService, FreteService, CarrinhoService) {
//    $scope.cidades = [];
//    $scope.estados = [];
//    $scope.enderecos = [];
//    $scope.endereco = {};
//    $scope.fretes = [];
//    $scope.mascaraCEP = "99999-999";
//    $scope.mensagem = {};

//    $scope.mostraOverlay = function () {
//        $scope.overlayEspera = '/css/overlay.html';
//    };
//    $scope.escondeOverlay = function () {
//        $scope.overlayEspera = null;
//    };

//    $scope.carregarLista = function () {
//        $scope.mostraOverlay();
//        EnderecoService.query(
//        function (enderecos) {
//            $scope.enderecos = enderecos;
//            console.log("Enderecos carregados.");
//            $scope.escondeOverlay();
//        },
//        function (erro) {
//            $scope.mensagem =
//            {
//                texto: "Não foi possível obter a lista de contatos"
//            };
//            console.log(erro);
//        }
//        );
//    };

//    $scope.carregarItens = function () {
//        CarrinhoService.query(
//        function (carrinho2) {
//            $scope.carrinho = carrinho2;
//            console.log("Itens carregados.");
//            $scope.escondeOverlay();
//        },
//        function (erro) {
//            $scope.mensagem =
//            {
//                texto: "Não foi possível obter a lista de contatos"
//            };
//            console.log(erro);
//        }
//        );
//    };

//    $scope.carregarEstados = function () {
//        EstadoService.query(
//        function (estados) {
//            $scope.estados = estados;
//            console.log("Estados carregados.");
//        },
//        function (erro) {
//            $scope.mensagem =
//            {
//                texto: "Não foi possível obter a lista de contatos"
//            };
//            console.log(erro);
//        }
//        );
//    };

//    $scope.carregarCidades = function (estado) {
//        if (!estado)
//            $scope.cidades = [];
//        else {
//            CidadeService
//                .query({ id: estado.Id },
//                    function (cidades) {
//                        $scope.cidades = cidades;
//                        console.log("Cidades carregados.");
//                    },
//                    function (erro) {
//                        $scope.mensagem =
//                        {
//                            texto: "Não foi possível obter a lista de contatos"
//                        };
//                        console.log(erro);
//                    }
//                );
//        }
//    };

//    $scope.subTotal = function () {
//        if (!$scope.carrinho) {

//        }
//        else {
//            var total = 0;
//            angular.forEach($scope.carrinho, function (item) {
//                total += item.Quantidade * item.PrecoProduto;
//            }, total);

//            return total;
//        }
//    };

//    $scope.totalDesconto = function () {
//        var totalDesconto = 0;
//        angular.forEach($scope.carrinho, function (item) {
//            totalDesconto += item.Quantidade * item.Desconto;
//        }, totalDesconto);

//        return totalDesconto;
//    };

//    $scope.init = function () {
//        $scope.carregarLista();
//        $scope.carregarEstados();
//        $scope.carregarItens();
//        $scope.endereco = new EnderecoService();
//    };

//    $scope.init();

//    $scope.adicionaEndereco = function () {
//        var id = $scope.endereco.Id;
//        $scope.endereco.$update()
//            .then(
//          function () {
//              if (id) {
//                  $scope.endereco = new EnderecoService();
//                  $scope.mensagem = { sucesso: 'Endereço editado com sucesso.' };
//              }
//              else {
//                  $scope.endereco = new EnderecoService();
//                  $scope.mensagem = { sucesso: 'Endereço adicionado com sucesso.' };
//              }
//              $scope.carregarLista();
//          }).catch(function (erro) {
//              $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
//              $scope.escondeOverlay();
//          });
//    };

//    $scope.atualizaEndereco = function () {
//        $scope.endereco.$update()
//            .then(
//          function () {
//              $scope.carregarLista();
//              $scope.endereco = new EnderecoService();
//          }).catch(function (erro) {
//              $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
//              $scope.escondeOverlay();
//          });
//    };

//    $scope.selecionaEndereco = function (item) {
//        $scope.enderecoSelecionado = item;
//        $scope.mostraOverlay();
//        FreteService.query({ id: item.Id },
//                    function (fretes) {
//                        $scope.fretes = fretes;
//                        console.log("Fretes carregados.");
//                        $scope.escondeOverlay();
//                    },
//                    function (erro) {
//                        $scope.mensagem =
//                        {
//                            texto: "Não foi possível obter a lista de contatos"
//                        };
//                        $scope.escondeOverlay();
//                        console.log(erro);
//                    }
//                );
//    };

//    $scope.selecionaFrete = function (item) {
//        $scope.freteSelecionado = item;
//        item.$seleciona()
//            .then(
//          function () {
//              console.log("Frete selecionado.");
//          }).catch(function (erro) {
//              $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
//              $scope.escondeOverlay();
//          });
//    };

//    $scope.editarEndereco = function (endereco) {

//        angular.copy(endereco, $scope.endereco);
//        $scope.carregarCidades(endereco.Estado);

//    };

//    $scope.excluirEndereco = function (item) {
//        EnderecoService.delete(
//            { id: item.Id },
//          function () {
//              $scope.carregarLista();
//              $scope.mensagem = { sucesso: 'Endereço excluído com sucesso.' };
//          },
//        function (erro) {
//            $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
//            $scope.escondeOverlay();
//        });
//    };


//}]);

//interfaceModule.factory("EnderecoService", ['$resource', function ($resource) {
//    return $resource('/api/endereco/', { },
//        {
//            'query': { method: 'GET', isArray: true },
//            "update": { method: "POST", isArray: true },
//            "delete": { method: "DELETE" }
//        });
//}]);
//interfaceModule.factory("CarrinhoService", ['$resource', function ($resource) {
//    return $resource('/api/carrinho/', {},
//        {
//            'query': { method: 'GET', isArray: true }
//        });
//}]);

//interfaceModule.factory("EstadoService", ['$resource', function ($resource) {
//    return $resource('/api/estado/:id', { id: "@id" },
//        {
//            'query': { method: 'GET', isArray: true }
//        });
//}]);
//interfaceModule.factory("CidadeService", ['$resource', function ($resource) {
//    return $resource('/api/cidade/:id', { id: "@id" },
//        {
//            'query': { method: 'GET', isArray: true }
//        });
//}]);
//interfaceModule.factory("FreteService", ['$resource', function ($resource) {
//    return $resource('/api/frete/:id', { id: "@id" },
//        {
//            'query': { method: 'GET', isArray: true },
//            "seleciona": { method: "POST" }
//        });
//}]);

var interfaceModule = angular.module("enderecoApp", ['ngResource', 'ngAnimate', 'ngMask']);

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

interfaceModule.controller("EnderecoController", ['$scope', '$filter', '$timeout', 'EnderecoService', 'EstadoService', 'CidadeService', 'FreteService', 'CarrinhoService', 'CupomService', function ($scope, $filter, $timeout, EnderecoService, EstadoService, CidadeService, FreteService, CarrinhoService, CupomService) {
    $scope.cidades = [];
    $scope.estados = [];
    $scope.enderecos = [];
    $scope.endereco = {};
    $scope.fretes = [];
    $scope.mascaraCEP = "99999-999";
    $scope.mensagem = {};
    $scope.descontoCupom = null;
    $scope.descontoPercentual = null;
    $scope.validadeCupom = null;

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/css/overlay.html';
    };

    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };

    $scope.carregarLista = function () {
        $scope.mostraOverlay();
        EnderecoService.query(
        function (enderecos) {
            $scope.enderecos = enderecos;
            console.log("Enderecos carregados.");
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

    $scope.carregarItens = function () {
        CarrinhoService.query(
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

    $scope.carregarEstados = function () {
        EstadoService.query(
        function (estados) {
            $scope.estados = estados;
            console.log("Estados carregados.");
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

    $scope.carregarCidades = function () {

        CidadeService.query(
        function (cidades) {
            $scope.cidades = cidades;
            console.log("Cidades carregadas.");
        },
        function (erro) {
            $scope.mensagem =
            {
                texto: "Não foi possível obter a lista de contatos"
            };
    console.log(erro);
}
);






        //if (!estado)
        //    $scope.cidades = [];
        //else {
        //    CidadeService
        //        .query({ id: estado.Id },
        //            function (cidades) {
        //                $scope.cidades = cidades;
        //                console.log("Cidades carregados.");
        //            },
        //            function (erro) {
        //                $scope.mensagem =
        //                {
        //                    texto: "Não foi possível obter a lista de contatos"
        //                };
        //                console.log(erro);
        //            }
        //        );
        //}
    };

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

    $scope.subTotal = function () {
        if (!$scope.carrinho) {

        }
        else {
            var total = 0;
            angular.forEach($scope.carrinho, function (item) {
                total += item.Quantidade * item.PrecoProduto;
            }, total);

            return total;
        }
    };

    $scope.totalDesconto = function () {
        descontoTotal = 0;

        if ($scope.descontoCupom) {
            descontoTotal = $scope.descontoCupom;
        }
        if ($scope.descontoPercentual) {
            descontoTotal = $scope.subTotal() * ($scope.descontoPercentual / 100);
        }

        return descontoTotal;
    };

    $scope.init = function () {
        $scope.carregarLista();
        $scope.carregarEstados();
        $scope.carregarCidades();
        $scope.carregarItens();
        $scope.endereco = new EnderecoService();
        $scope.carregarCupom();
    };

    $scope.init();

    $scope.adicionaEndereco = function () {
        var id = $scope.endereco.Id;
        $scope.endereco.$update()
            .then(
          function () {
              if (id) {
                  $scope.endereco = new EnderecoService();
                  $scope.mensagem = { sucesso: 'Endereço editado com sucesso.' };
              }
              else {
                  $scope.endereco = new EnderecoService();
                  $scope.mensagem = { sucesso: 'Endereço adicionado com sucesso.' };
              }
              $scope.carregarLista();
          }).catch(function (erro) {
              $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
              $scope.escondeOverlay();
          });
    };

    $scope.atualizaEndereco = function () {
        $scope.endereco.$update()
            .then(
          function () {
              $scope.carregarLista();
              $scope.endereco = new EnderecoService();
          }).catch(function (erro) {
              $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
              $scope.escondeOverlay();
          });
    };

    $scope.selecionaEndereco = function (item) {
        $scope.enderecoSelecionado = item;
        $scope.mostraOverlay();
        FreteService.query({ id: item.Id },
                    function (fretes) {
                        $scope.fretes = fretes;
                        console.log("Fretes carregados.");
                        $scope.escondeOverlay();
                    },
                    function (erro) {
                        $scope.mensagem =
                        {
                            texto: "Não foi possível obter a lista de contatos"
                        };
                        $scope.escondeOverlay();
                        console.log(erro);
                    }
                );
    };

    $scope.selecionaFrete = function (item) {
        $scope.freteSelecionado = item;
        item.$seleciona()
            .then(
          function () {
              console.log("Frete selecionado.");
          }).catch(function (erro) {
              $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
              $scope.escondeOverlay();
          });
    };

    $scope.editarEndereco = function (endereco) {

        angular.copy(endereco, $scope.endereco);
        $scope.carregarCidades(endereco.Estado);

    };

    $scope.excluirEndereco = function (item) {
        EnderecoService.delete(
            { id: item.Id },
          function () {
              $scope.carregarLista();
              $scope.mensagem = { sucesso: 'Endereço excluído com sucesso.' };
          },
        function (erro) {
            $scope.mensagem = { erro: 'Não foi possível salvar: ' + erro.data };
            $scope.escondeOverlay();
        });
    };


}]);

interfaceModule.factory("EnderecoService", ['$resource', function ($resource) {
    return $resource('/api/endereco/', {},
        {
            'query': { method: 'GET', isArray: true },
            "update": { method: "POST", isArray: true },
            "delete": { method: "DELETE" }
        });
}]);
interfaceModule.factory("CarrinhoService", ['$resource', function ($resource) {
    return $resource('/api/carrinho/', {},
        {
            'query': { method: 'GET', isArray: true }
        });
}]);
interfaceModule.factory("EstadoService", ['$resource', function ($resource) {
    return $resource('/api/estado/:id', { id: "@id" },
        {
            'query': { method: 'GET', isArray: true }
        });
}]);
interfaceModule.factory("CidadeService", ['$resource', function ($resource) {
    return $resource('/api/cidade/:id', { id: "@id" },
        {
            'query': { method: 'GET', isArray: true }
        });
}]);
interfaceModule.factory("FreteService", ['$resource', function ($resource) {
    return $resource('/api/frete/:id', { id: "@id" },
        {
            'query': { method: 'GET', isArray: true },
            "seleciona": { method: "POST" }
        });
}]);
interfaceModule.factory("CupomService", ['$resource', function ($resource) {
    return $resource('/api/cupom/', {},
        {
            'query': { method: 'GET' },
            "update": { method: "POST", }
        });
}]);