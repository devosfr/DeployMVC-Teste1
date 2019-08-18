controlesModule.controller("saboresController", function ($filter, $scope, $rootScope, $http, TamanhoService) {

    if (idProduto !== "0"){
        TamanhoService.getSabores(idProduto).then(function (tamanhosNG) {
            var teste = $.parseJSON(tamanhosNG.d);
            $scope.sabores = teste;
        }, function () {
            alert('Problemas ao carregar sabores');
        }
        );
    };

    if (idProduto == "0")
        $scope.visivel = false;
    else
        $scope.visivel = true;

    TamanhoService.getSaboresProduto(idProduto).then(function (saboresNG) {
        var teste2 = $.parseJSON(saboresNG.d);
        if (teste2 == null)
            $scope.saboresSelecionadas = [];
        else
            $scope.saboresSelecionadas = teste2;
    }, function () {
        alert('Problemas ao carregar sabores');
    }
    );

    $scope.remover = function (id) {
        var selecionado = $scope.saboresSelecionadas.filter(function (item) {
            return item.id === id;
        });
        $scope.sabores.push(selecionado[0]);
        $scope.saboresSelecionadas = $scope.saboresSelecionadas.filter(function (item) {
            return item.id !== id;
        });
        $scope.filtroSabores = "";
    };

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/Controle/UserControl/ControleProduto/overlay.html';
    };
    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };


    $scope.adicionarSabor = function (novo) {

        $scope.mostraOverlay();


        TamanhoService.adicionarSabor(novo).then(function (retorno) {
            var status = $.parseJSON(retorno.d);
            if (status === "OK") {
                TamanhoService.getSabores(idProduto).then(function (tamanhosNG) {
                    var teste = $.parseJSON(tamanhosNG.d);
                    $scope.sabores = teste;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar sabores');
                    $scope.escondeOverlay();
                }
);

                TamanhoService.getSaboresProduto(idProduto).then(function (tamanhosNG) {
                    var teste2 = $.parseJSON(tamanhosNG.d);
                    if (teste2 == null)
                        $scope.saboresSelecionadas = [];
                    else
                        $scope.saboresSelecionadas = teste2;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar sabores');
                    $scope.escondeOverlay();
                }
                );

                $scope.novoSabor = "";
                $scope.filtroSabores = "";
            } else {
                alert('Problemas ao adicionar sabores');
                $scope.escondeOverlay();
            }
        }, function () {
            alert('Problemas ao carregar sabores');
            $scope.escondeOverlay();
        }
);
    };

    $scope.cancelar = function () {

        $scope.mostraOverlay();




        {
            TamanhoService.getSabores(idProduto).then(function (tamanhosNG) {
                var teste = $.parseJSON(tamanhosNG.d);
                $scope.sabores = teste;
                $scope.escondeOverlay();
            }, function () {
                alert('Problemas ao carregar sabores');
                $scope.escondeOverlay();
            }
);

            TamanhoService.getSaboresProduto(idProduto).then(function (tamanhosNG) {
                var teste2 = $.parseJSON(tamanhosNG.d);
                if (teste2 == null)
                    $scope.saboresSelecionadas = [];
                else
                    $scope.saboresSelecionadas = teste2;
                $scope.escondeOverlay();
            }, function () {
                alert('Problemas ao carregar sabores');
                $scope.escondeOverlay();
            }
            );

            $scope.novoSabor = "";
            $scope.filtroSabores = "";
            $scope.filtroSaboresSelecionadas = "";
        }


    };


    $scope.atualizar = function () {

        $scope.mostraOverlay();

        var saboresSelecionadas = $scope.saboresSelecionadas;

        TamanhoService.atualizarSabor(saboresSelecionadas, idProduto).then(function (retorno) {
            var status = $.parseJSON(retorno.d);
            if (status === "OK") {
                TamanhoService.getSabores(idProduto).then(function (tamanhosNG) {
                    var teste = $.parseJSON(tamanhosNG.d);
                    $scope.sabores = teste;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar sabores');
                    $scope.escondeOverlay();
                }
);

                TamanhoService.getSaboresProduto(idProduto).then(function (tamanhosNG) {
                    var teste2 = $.parseJSON(tamanhosNG.d);
                    if (teste2 == null)
                        $scope.saboresSelecionadas = [];
                    else
                        $scope.saboresSelecionadas = teste2;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar sabores');
                    $scope.escondeOverlay();
                }
                );

                $scope.novoSabor = "";
            } else {
                alert('Problemas ao adicionar sabores');
                $scope.escondeOverlay();
            }
        }, function () {
            alert('Problemas ao carregar sabores');
            $scope.escondeOverlay();
        }
);
        $scope.filtroSabores = "";
        $scope.filtroSaboresSelecionadas = "";
    };

    $scope.adicionar = function (id) {
        var selecionado = $scope.sabores.filter(function (item) {
            return item.id === id;
        });
        $scope.saboresSelecionadas.push(selecionado[0]);
        $scope.sabores = $scope.sabores.filter(function (item) {
            return item.id !== id;
        });

    };



});