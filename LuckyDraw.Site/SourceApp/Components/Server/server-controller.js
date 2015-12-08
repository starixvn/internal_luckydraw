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
} ]);

serverModule.directive('serverKeyboardEvent',
[function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();

                switch (event.which) {
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