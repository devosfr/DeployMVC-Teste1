controlesModule.factory("TamanhoService", function ($http, $q, $rootScope) {

    var enderecoServico = "/Webservices/ControleGrupos.asmx/";
    var enderecoServico2 = "/Webservices/ControleProdutoCor.asmx/";
    var enderecoServico3 = "/Webservices/ControleProdutoSabor.asmx/";

    return {
        getGrupos: function () {
            // Get the deferred object
            var deferred = $q.defer();
            // Initiates the AJAX call
            $http.get(enderecoServico + 'getGrupos').success(deferred.resolve).error(deferred.reject);
            // Returns the promise - Contains result once request completes
            return deferred.promise;
        },
        atualizarTamanho: function (lista, idProduto) {
            // Get the deferred object
            var deferred = $q.defer();
            // Initiates the AJAX call
            $http.post(enderecoServico+'atualizar', {lista:lista,idProduto:idProduto}).success(deferred.resolve).error(deferred.reject);
            // Returns the promise - Contains result once request completes
            return deferred.promise;
        }

    }

});