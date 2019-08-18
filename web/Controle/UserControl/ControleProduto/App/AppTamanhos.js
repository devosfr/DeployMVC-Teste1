var controlesModule = angular.module("controlesModule", ['ngSanitize', 'ngAnimate']);

controlesModule.directive('ngReallyClick', [function () {
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

controlesModule.directive('format', ['$filter',
  function ($filter) {
      return {
          require: '?ngModel',
          link: function (scope, elem, attrs, ctrl) {
              if (!ctrl) return;


              ctrl.$formatters.unshift(function (a) {
                  elem[0].value = ctrl.$modelValue
                  elem.priceFormat({
                      prefix: '',
                      centsSeparator: ',',
                      thousandsSeparator: '.'
                  });
                  return elem[0].value;
              });


              ctrl.$parsers.unshift(function (viewValue) {
                  elem.priceFormat({
                      prefix: '',
                      centsSeparator: ',',
                      thousandsSeparator: '.'
                  });
                  return elem[0].value;
              });
          }
      };
  }
]);

controlesModule.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});



controlesModule.controller("tamanhoController", function ($filter, $scope, $rootScope, $http, TamanhoService) {

    if (idProduto !== "0") {
        TamanhoService.getTamanhos(idProduto).then(function (tamanhosNG) {
            var teste = $.parseJSON(tamanhosNG.d);
            $scope.tamanhos = teste;
        }, function () {
            alert('Problemas ao carregar tamanhos');
        }
        );
    };

    if (idProduto == "0")
        $scope.visivel = false;
    else
        $scope.visivel = true;

    if (idProduto > 0) {
        TamanhoService.getTamanhosProduto(idProduto).then(function (tamanhosNG) {
            var teste2 = $.parseJSON(tamanhosNG.d);
            if (teste2 == null)
                $scope.selecionados = [];
            else
                $scope.selecionados = teste2;
        }, function () {
            alert('Problemas ao carregar tamanhos');
        }
        );
    };

    $scope.removerTamanho = function (id) {
        var selecionado = $scope.selecionados.filter(function (item) {
            return item.id === id;
        });
        $scope.tamanhos.push(selecionado[0]);
        $scope.selecionados = $scope.selecionados.filter(function (item) {
            return item.id !== id;
        });
        $scope.filtroTamanhos = "";
    };

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/Controle/UserControl/ControleProduto/overlay.html';
    };
    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };


    $scope.adicionarTamanho = function (novo) {

        $scope.mostraOverlay();


        TamanhoService.adicionarTamanho(novo).then(function (retorno) {
            var status = $.parseJSON(retorno.d);
            if (status === "OK") {
                TamanhoService.getTamanhos(idProduto).then(function (tamanhosNG) {
                    var teste = $.parseJSON(tamanhosNG.d);
                    $scope.tamanhos = teste;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar tamanhos');
                    $scope.escondeOverlay();
                }
);

                TamanhoService.getTamanhosProduto(idProduto).then(function (tamanhosNG) {
                    var teste2 = $.parseJSON(tamanhosNG.d);
                    if (teste2 == null)
                        $scope.selecionados = [];
                    else
                        $scope.selecionados = teste2;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar tamanhos');
                    $scope.escondeOverlay();
                }
                );

                $scope.novoTamanho = "";
                $scope.filtroTamanhos = "";
            } else {
                alert('Problemas ao adicionar tamanho');
                $scope.escondeOverlay();
            }
        }, function () {
            alert('Problemas ao carregar tamanhos');
            $scope.escondeOverlay();
        }
);
    };

    $scope.cancelar = function () {

        $scope.mostraOverlay();




        {
            TamanhoService.getTamanhos(idProduto).then(function (tamanhosNG) {
                var teste = $.parseJSON(tamanhosNG.d);
                $scope.tamanhos = teste;
                $scope.escondeOverlay();
            }, function () {
                alert('Problemas ao carregar tamanhos');
                $scope.escondeOverlay();
            }
);

            TamanhoService.getTamanhosProduto(idProduto).then(function (tamanhosNG) {
                var teste2 = $.parseJSON(tamanhosNG.d);
                if (teste2 == null)
                    $scope.selecionados = [];
                else
                    $scope.selecionados = teste2;
                $scope.escondeOverlay();
            }, function () {
                alert('Problemas ao carregar tamanhos');
                $scope.escondeOverlay();
            }
            );

            $scope.novoTamanho = "";
            $scope.filtroTamanhos = "";
            $scope.filtroSelecionados = "";
        }


    };


    $scope.atualizar = function () {

        $scope.mostraOverlay();

        var selecionados = $scope.selecionados;

        TamanhoService.atualizarTamanho(selecionados, idProduto).then(function (retorno) {
            var status = $.parseJSON(retorno.d);
            if (status === "OK") {
                TamanhoService.getTamanhos(idProduto).then(function (tamanhosNG) {
                    var teste = $.parseJSON(tamanhosNG.d);
                    $scope.tamanhos = teste;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar tamanhos');
                    $scope.escondeOverlay();
                }
);

                TamanhoService.getTamanhosProduto(idProduto).then(function (tamanhosNG) {
                    var teste2 = $.parseJSON(tamanhosNG.d);
                    if (teste2 == null)
                        $scope.selecionados = [];
                    else
                        $scope.selecionados = teste2;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar tamanhos');
                    $scope.escondeOverlay();
                }
                );

                $scope.novoTamanho = "";
            } else {
                alert('Problemas ao adicionar tamanho');
                $scope.escondeOverlay();
            }
        }, function () {
            alert('Problemas ao carregar tamanhos');
            $scope.escondeOverlay();
        }
);
        $scope.filtroTamanhos = "";
        $scope.filtroSelecionados = "";
    };

    $scope.adicionar = function (id) {
        var selecionado = $scope.tamanhos.filter(function (item) {
            return item.id === id;
        });
        $scope.selecionados.push(selecionado[0]);
        $scope.tamanhos = $scope.tamanhos.filter(function (item) {
            return item.id !== id;
        });

    };



});


controlesModule.controller("coresController", function ($filter, $scope, $rootScope, $http, TamanhoService) {

    if (idProduto !== "0") {
        TamanhoService.getCores(idProduto).then(function (tamanhosNG) {
            var teste = $.parseJSON(tamanhosNG.d);
            $scope.cores = teste;
        }, function () {
            alert('Problemas ao carregar cores');
        }
        );
    };

    if (idProduto == "0")
        $scope.visivel = false;
    else
        $scope.visivel = true;

    TamanhoService.getCoresProduto(idProduto).then(function (coresNG) {
        var teste2 = $.parseJSON(coresNG.d);
        if (teste2 == null)
            $scope.coresSelecionadas = [];
        else
            $scope.coresSelecionadas = teste2;
    }, function () {
        alert('Problemas ao carregar cores');
    }
    );

    $scope.remover = function (id) {
        var selecionado = $scope.coresSelecionadas.filter(function (item) {
            return item.id === id;
        });
        $scope.cores.push(selecionado[0]);
        $scope.coresSelecionadas = $scope.coresSelecionadas.filter(function (item) {
            return item.id !== id;
        });
        $scope.filtroCores = "";
    };

    $scope.mostraOverlay = function () {
        $scope.overlayEspera = '/Controle/UserControl/ControleProduto/overlay.html';
    };
    $scope.escondeOverlay = function () {
        $scope.overlayEspera = null;
    };


    $scope.adicionarCor = function (novo) {

        $scope.mostraOverlay();


        TamanhoService.adicionarCor(novo).then(function (retorno) {
            var status = $.parseJSON(retorno.d);
            if (status === "OK") {
                TamanhoService.getCores(idProduto).then(function (tamanhosNG) {
                    var teste = $.parseJSON(tamanhosNG.d);
                    $scope.cores = teste;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar cores');
                    $scope.escondeOverlay();
                }
);

                TamanhoService.getCoresProduto(idProduto).then(function (tamanhosNG) {
                    var teste2 = $.parseJSON(tamanhosNG.d);
                    if (teste2 == null)
                        $scope.coresSelecionadas = [];
                    else
                        $scope.coresSelecionadas = teste2;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar cores');
                    $scope.escondeOverlay();
                }
                );

                $scope.novaCor = "";
                $scope.filtroCores = "";
            } else {
                alert('Problemas ao adicionar cores');
                $scope.escondeOverlay();
            }
        }, function () {
            alert('Problemas ao carregar cores');
            $scope.escondeOverlay();
        }
);
    };

    $scope.cancelar = function () {

        $scope.mostraOverlay();




        {
            TamanhoService.getCores(idProduto).then(function (tamanhosNG) {
                var teste = $.parseJSON(tamanhosNG.d);
                $scope.cores = teste;
                $scope.escondeOverlay();
            }, function () {
                alert('Problemas ao carregar cores');
                $scope.escondeOverlay();
            }
);

            TamanhoService.getCoresProduto(idProduto).then(function (tamanhosNG) {
                var teste2 = $.parseJSON(tamanhosNG.d);
                if (teste2 == null)
                    $scope.coresSelecionadas = [];
                else
                    $scope.coresSelecionadas = teste2;
                $scope.escondeOverlay();
            }, function () {
                alert('Problemas ao carregar cores');
                $scope.escondeOverlay();
            }
            );

            $scope.novaCor = "";
            $scope.filtroCores = "";
            $scope.filtroCoresSelecionadas = "";
        }


    };


    $scope.atualizar = function () {

        $scope.mostraOverlay();

        var coresSelecionadas = $scope.coresSelecionadas;

        TamanhoService.atualizarCor(coresSelecionadas, idProduto).then(function (retorno) {
            var status = $.parseJSON(retorno.d);
            if (status === "OK") {
                TamanhoService.getCores(idProduto).then(function (tamanhosNG) {
                    var teste = $.parseJSON(tamanhosNG.d);
                    $scope.cores = teste;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar cores');
                    $scope.escondeOverlay();
                }
);

                TamanhoService.getCoresProduto(idProduto).then(function (tamanhosNG) {
                    var teste2 = $.parseJSON(tamanhosNG.d);
                    if (teste2 == null)
                        $scope.coresSelecionadas = [];
                    else
                        $scope.coresSelecionadas = teste2;
                    $scope.escondeOverlay();
                }, function () {
                    alert('Problemas ao carregar cores');
                    $scope.escondeOverlay();
                }
                );

                $scope.novaCor = "";
            } else {
                alert('Problemas ao adicionar cores');
                $scope.escondeOverlay();
            }
        }, function () {
            alert('Problemas ao carregar cores');
            $scope.escondeOverlay();
        }
);
        $scope.filtroCores = "";
        $scope.filtroCoresSelecionadas = "";
    };

    $scope.adicionar = function (id) {
        var selecionado = $scope.cores.filter(function (item) {
            return item.id === id;
        });
        $scope.coresSelecionadas.push(selecionado[0]);
        $scope.cores = $scope.cores.filter(function (item) {
            return item.id !== id;
        });

    };



});



(function ($) {
    $.fn.priceFormat = function (options) {
        var defaults = {
            prefix: 'US$ ',
            suffix: '',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: false,
            centsLimit: 2,
            clearPrefix: false,
            clearSufix: false,
            allowNegative: false,
            insertPlusSign: false
        };
        var options = $.extend(defaults, options);
        return this.each(function () {
            var obj = $(this);
            var is_number = /[0-9]/;
            var prefix = options.prefix;
            var suffix = options.suffix;
            var centsSeparator = options.centsSeparator;
            var thousandsSeparator = options.thousandsSeparator;
            var limit = options.limit;
            var centsLimit = options.centsLimit;
            var clearPrefix = options.clearPrefix;
            var clearSuffix = options.clearSuffix;
            var allowNegative = options.allowNegative;
            var insertPlusSign = options.insertPlusSign;
            if (insertPlusSign) allowNegative = true;

            function to_numbers(str) {
                var formatted = '';
                for (var i = 0; i < (str.length) ; i++) {
                    char_ = str.charAt(i);
                    if (formatted.length == 0 && char_ == 0) char_ = false;
                    if (char_ && char_.match(is_number)) {
                        if (limit) {
                            if (formatted.length < limit) formatted = formatted + char_
                        } else {
                            formatted = formatted + char_
                        }
                    }
                }
                return formatted
            }

            function fill_with_zeroes(str) {
                while (str.length < (centsLimit + 1)) str = '0' + str;
                return str
            }

            function price_format(str) {
                var formatted = fill_with_zeroes(to_numbers(str));
                var thousandsFormatted = '';
                var thousandsCount = 0;
                if (centsLimit == 0) {
                    centsSeparator = "";
                    centsVal = ""
                }
                var centsVal = formatted.substr(formatted.length - centsLimit, centsLimit);
                var integerVal = formatted.substr(0, formatted.length - centsLimit);
                formatted = (centsLimit == 0) ? integerVal : integerVal + centsSeparator + centsVal;
                if (thousandsSeparator || $.trim(thousandsSeparator) != "") {
                    for (var j = integerVal.length; j > 0; j--) {
                        char_ = integerVal.substr(j - 1, 1);
                        thousandsCount++;
                        if (thousandsCount % 3 == 0) char_ = thousandsSeparator + char_;
                        thousandsFormatted = char_ + thousandsFormatted
                    }
                    if (thousandsFormatted.substr(0, 1) == thousandsSeparator) thousandsFormatted = thousandsFormatted.substring(1, thousandsFormatted.length);
                    formatted = (centsLimit == 0) ? thousandsFormatted : thousandsFormatted + centsSeparator + centsVal
                }
                if (allowNegative && (integerVal != 0 || centsVal != 0)) {
                    if (str.indexOf('-') != -1 && str.indexOf('+') < str.indexOf('-')) {
                        formatted = '-' + formatted
                    } else {
                        if (!insertPlusSign) formatted = '' + formatted;
                        else formatted = '+' + formatted
                    }
                }
                if (prefix) formatted = prefix + formatted;
                if (suffix) formatted = formatted + suffix;
                return formatted
            }

            function key_check(e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                var typed = String.fromCharCode(code);
                var functional = false;
                var str = obj.val();
                var newValue = price_format(str + typed);
                if ((code >= 48 && code <= 57) || (code >= 96 && code <= 105)) functional = true;
                if (code == 8) functional = true;
                if (code == 9) functional = true;
                if (code == 13) functional = true;
                if (code == 46) functional = true;
                if (code == 37) functional = true;
                if (code == 39) functional = true;
                if (allowNegative && (code == 189 || code == 109)) functional = true;
                if (insertPlusSign && (code == 187 || code == 107)) functional = true;
                if (!functional) {
                    e.preventDefault();
                    e.stopPropagation();
                    if (str != newValue) obj.val(newValue)
                }
            }

            function price_it() {
                var str = obj.val();
                var price = price_format(str);
                if (str != price) obj.val(price)
            }

            function add_prefix() {
                var val = obj.val();
                obj.val(prefix + val)
            }

            function add_suffix() {
                var val = obj.val();
                obj.val(val + suffix)
            }

            function clear_prefix() {
                if ($.trim(prefix) != '' && clearPrefix) {
                    var array = obj.val().split(prefix);
                    obj.val(array[1])
                }
            }

            function clear_suffix() {
                if ($.trim(suffix) != '' && clearSuffix) {
                    var array = obj.val().split(suffix);
                    obj.val(array[0])
                }
            }
            $(this).bind('keydown.price_format', key_check);
            $(this).bind('keyup.price_format', price_it);
            $(this).bind('focusout.price_format', price_it);
            if (clearPrefix) {
                $(this).bind('focusout.price_format', function () {
                    clear_prefix()
                });
                $(this).bind('focusin.price_format', function () {
                    add_prefix()
                })
            }
            if (clearSuffix) {
                $(this).bind('focusout.price_format', function () {
                    clear_suffix()
                });
                $(this).bind('focusin.price_format', function () {
                    add_suffix()
                })
            }
            if ($(this).val().length > 0) {
                price_it();
                clear_prefix();
                clear_suffix()
            }
        })
    };
    $.fn.unpriceFormat = function () {
        return $(this).unbind(".price_format")
    };
    $.fn.unmask = function () {
        var field = $(this).val();
        var result = "";
        for (var f in field) {
            if (!isNaN(field[f]) || field[f] == "-") result += field[f]
        }
        return result
    }
})(jQuery);