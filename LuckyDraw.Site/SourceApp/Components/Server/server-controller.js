'use strict';
/*global serverModule*/

serverModule.controller('ServerController',
['$window', 'CommonModel', 'baseUrl', function ($window, CommonModel, baseUrl) {
    var vm = this;

    vm.redirectToPrizeList = function (prizeType) {
        var path = baseUrl + 'Server/PrizeList/' + prizeType;

        $window.location.href = path;
    };

    vm.initialize = function () {
        CommonModel.getPrizeType().then(function () {
            vm.prizeTypes = CommonModel.prizeTypes;
        });
    };

    vm.initialize();

    vm.redirectToDashboard = function () {
        $window.location.href = '/';
    };

    var hubProxy = $.connection.hubs;

    hubProxy.client.getSignalNumber = function (number) {
        if (number === 1) {
            vm.redirectToPrizeList(2);
        }
        else if (number === 2) {
            vm.redirectToPrizeList(3);
        }
        else if (number === 3) {
            vm.redirectToPrizeList(4);
        }
        else if (number === 4) {
            vm.redirectToPrizeList(5);
        }
    };

    $.connection.hub.start();
} ]);

serverModule.directive('serverKeyboardEvent',
[function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();

                switch (event.which) {
                    case 27: // asc
                        scope.serverCtrl.redirectToDashboard();
                        break;
                    case 49: // 1
                        scope.serverCtrl.redirectToPrizeList(2);
                        break;
                    case 50: // 2
                        scope.serverCtrl.redirectToPrizeList(3);
                        break;
                    case 51: // 3
                        scope.serverCtrl.redirectToPrizeList(4);
                        break;
                    case 52: // 4
                        scope.serverCtrl.redirectToPrizeList(5);
                        break;
                }
            });
        }
    };
} ]);