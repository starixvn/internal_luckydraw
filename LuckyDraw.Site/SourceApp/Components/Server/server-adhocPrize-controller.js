'use strict';
/*global serverModule*/

serverModule.controller('AdhocPrizeController',
['$window', 'CommonModel', 'baseUrl',
function ($window, CommonModel, baseUrl) {
    var vm = this;

    vm.redirectToServerIndex = function () {
        var path = baseUrl + 'Server/Index';

        $window.location.href = path;
    };

    var hubProxy = $.connection.hubs;

    $.connection.hub.start();
} ]);

serverModule.directive('adhocPrizeKeyboardEvent',
[function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();

                switch (event.which) {
                    case 13: // enter
                    case 27: // asc
                        scope.adhocPrizeCtrl.redirectToServerIndex();
                        break;
                }
            });
        }
    };
} ]);