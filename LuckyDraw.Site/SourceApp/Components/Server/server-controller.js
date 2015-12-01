'use strict';
/*global serverModule*/

serverModule.controller('ServerController',
['CommonModel', function (CommonModel) {
    var vm = this;

    vm.initialize = function () {
        CommonModel.getPrizeType().then(function () {
            vm.prizeTypes = CommonModel.prizeTypes;
        });
    };

    vm.initialize();
} ]);

serverModule.directive('serverKeyboardEvent',
['$window', 'baseUrl', function ($window, baseUrl) {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keypress', function (event) {
                var path = '';

                switch (event.keyCode) {
                    case 49: // 1
                        path = baseUrl + 'Server/PrizeList/2';
                        break;
                    case 50: // 2
                        path = baseUrl + 'Server/PrizeList/3';
                        break;
                    case 51: // 3
                        path = baseUrl + 'Server/PrizeList/4';
                        break;
                    case 52: // 4
                        path = baseUrl + 'Server/PrizeList/5';
                        break;
                    case 65: // a
                        path = baseUrl + 'Server/LuckyList/Index?priceType=2';
                        break;
                    case 66: // b
                        path = baseUrl + 'Server/LuckyList/Index?priceType=3';
                        break;
                    case 67: // c
                        path = baseUrl + 'Server/LuckyList/Index?priceType=4';
                        break;
                    case 68: // d
                        path = baseUrl + 'Server/LuckyList/Index?priceType=5';
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });
        }
    };
} ]);