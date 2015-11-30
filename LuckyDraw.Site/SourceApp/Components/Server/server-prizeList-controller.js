'use strict';
/*global serverModule*/

serverModule.controller('PrizeListController',
['$scope', '$window', '$document', '$location', '$routeParams', 'CommonModel', 'PrizeModel', 'baseUrl',
function ($scope, $window, $document, $location, $routeParams, CommonModel, PrizeModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.screenWidth = 0;
    vm.screenHeight = 0;
    vm.numRow = 3;

    var getWindowSize = function () {
        //IE
        if (!$window.innerWidth) {
            if (!($document.documentElement.clientWidth == 0)) {
                //strict mode
                vm.screenWidth = $document.documentElement.clientWidth;
                vm.screenHeight = $document.documentElement.clientHeight;
            } else {
                //quirks mode
                vm.screenWidth = $document.body.clientWidth;
                vm.screenHeight = $document.body.clientHeight;
            }
        } else {
            //w3c
            vm.screenWidth = $window.innerWidth;
            vm.screenHeight = $window.innerHeight;
        }
    };

    vm.prizeType = parseInt($routeParams.prizeType);

    vm.getClassByPrizeType = function (prizeType) {
        var className = '';

        switch (prizeType) {
            case 2:
                className = 'super-grand-prize';
                break;
            case 3:
                className = 'grand-prize';
                break;
            case 4:
                className = 'major-prize';
                break;
            case 5:
                className = 'cash-prize';
                break;
        }

        return className;
    };

    vm.renderListPrize = function () {
        PrizeModel.renderListPrize(vm.prizeType).then(function () {
            vm.listPrize = PrizeModel.listPrize;
        });
    };

    vm.initialize = function () {
        getWindowSize();

        CommonModel.getPrizeType().then(function () {
            vm.prizeTypes = CommonModel.prizeTypes;

            if ($routeParams.prizeType && vm.prizeType) {
                var index = vm.prizeTypes.map(function (e) { return e.PrizeTypeId; }).indexOf(vm.prizeType);

                if (index === -1) {
                    $location.path(baseUrl + 'Home/AccessDenied');
                } else {
                    CommonModel.getPrizeSettingByPrizeType(vm.prizeType).then(function () {
                        vm.prizeSetting = CommonModel.prizeSetting;
                        vm.total = vm.prizeSetting.Quantity / vm.prizeSetting.Multiply;
                        vm.currentPrize = vm.prizeSetting.CurrentPrize;
                    });
                }
            } else {
                $location.path(baseUrl + 'Home/AccessDenied');
            }
        });
    };

    vm.initialize();
} ]);

serverModule.directive('prizeListKeyboardEvent',
['$window', 'baseUrl', function ($window, baseUrl) {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown keypress', function (event) {
                var path = '';

                switch (event.keyCode) {
                    case 13: //enter
                        scope.prizeListCtrl.renderListPrize();
                        break;
                    case 27: // asc
                        path = baseUrl + 'Server/Index';
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });
        }
    };
} ]);