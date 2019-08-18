var interfaceModule = angular.module("interfaceModule", ['ngSanitize', 'ngAnimate', 'keepr']);

interfaceModule.controller("ContatoController", ['$scope','ContatoService', function ( $scope, ContatoService) {

    $scope.mensagem = {};
    $scope.contatoEmail = {};

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/Controle/UserControl/ControleProduto/overlay.html';
    };
    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };

    $scope.enviarContato = function () {

        $scope.mostraOverlay();
        var contatoEmail = $scope.contatoEmail;
        ContatoService.enviarContato(contatoEmail).then(function (resultado) {
            var mensagem = (resultado.d);

            $scope.mensagem.texto = mensagem;

            $scope.contatoEmail.email = "";

            $scope.escondeOverlay();

        }, function () {
            alert('Problemas ao cadastrar e-mail');
        }
        );
    };

    $scope.limpaDuvida = function () {
        $scope.contatoEmail.email = "";
    }

}]);

interfaceModule.factory("ContatoService", ['$http', '$q', function ($http, $q) {


    return {
        enviarContato: function (contatoEmail) {
            // Get the deferred object
            var deferred = $q.defer();
            // Initiates the AJAX call    lista: lista, 
            $http.post('/Webservices/Newsletter.asmx/enviaEmailNews', { txtEmailNew: contatoEmail.email }).success(deferred.resolve).error(deferred.reject);
            // Returns the promise - Contains result once request completes
            return deferred.promise;
        }
    }

}]);

