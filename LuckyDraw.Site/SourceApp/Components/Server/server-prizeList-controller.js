'use strict';
/*global serverModule*/

serverModule.controller('PrizeListController',
['$scope', '$window', '$document', '$location', '$routeParams', 'CommonModel', 'ServerModel', 'baseUrl',
function ($scope, $window, $document, $location, $routeParams, CommonModel, ServerModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
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

    vm.getLogoByPrizeType = function (prizeType) {
        var className = '';

        switch (prizeType) {
            case 2:
                className = 'super-grand';
                break;
            case 3:
                className = 'grand';
                break;
            case 4:
                className = 'major';
                break;
            case 5:
                className = 'cash';
                break;
        }

        return className;
    };

    vm.renderListPrize = function () {
        ServerModel.renderListPrize(vm.prizeType).then(function () {
            vm.prizeList = ServerModel.prizeList;
        });
    };

    vm.redirectToWinnerDetail = function () {
        var path = '';

        switch (vm.prizeType) {
            case 2:
                path = baseUrl + 'Server/SuperGrandWinnerDetail/' + vm.prizeType;
                break;
            case 3:
            case 4:
            case 5:
                path = baseUrl + 'Server/WinnerDetail/' + vm.prizeType;
                break;
        }

        if (path.length > 0) {
            $window.location.href = path;
        }
    };

    vm.redirectToServerIndex = function () {
        var path = baseUrl + 'Server/Index';

        $window.location.href = path;
    };

    vm.nextPrize = function () {
        if (vm.prizeSetting.CurrentPrize > 0) {
            if (vm.prizeSetting.Multiply > 1) {
                vm.prizeSetting.CurrentPrize -= 1;
            } else {
                vm.prizeSetting.CurrentPrize -= 3;
            }

            if (vm.prizeSetting.CurrentPrize < 0) {
                vm.prizeSetting.CurrentPrize = 0;
            }

            vm.updateCurrentPrize(vm.prizeSetting.CurrentPrize);
        }
    };

    vm.backPrize = function () {
        if (vm.prizeSetting.Multiply > 1) {
            if (vm.prizeSetting.Quantity > (vm.prizeSetting.CurrentPrize * vm.prizeSetting.Multiply)) {
                vm.prizeSetting.CurrentPrize += 1;

                if ((vm.prizeSetting.CurrentPrize * vm.prizeSetting.Multiply) > vm.prizeSetting.Quantity) {
                    vm.prizeSetting.CurrentPrize = parseInt(vm.prizeSetting.Quantity / vm.prizeSetting.Multiply);
                }
            }

            vm.updateCurrentPrize(vm.prizeSetting.CurrentPrize);
        } else {
            if (vm.prizeSetting.Quantity > vm.prizeSetting.CurrentPrize) {
                vm.prizeSetting.CurrentPrize += 3;

                if (vm.prizeSetting.CurrentPrize > vm.prizeSetting.Quantity) {
                    vm.prizeSetting.CurrentPrize = vm.prizeSetting.Quantity;
                }
            }

            vm.updateCurrentPrize(vm.prizeSetting.CurrentPrize);
        }
    };

    vm.updateCurrentPrize = function (currentPrize) {
        var criteria = {
            PrizeTypeId: vm.prizeType,
            CurrentPrize: currentPrize
        };

        ServerModel.updateCurrentPrize(vm, criteria);
    };

    vm.initialize = function () {
        vm.renderListPrize();

        CommonModel.getPrizeType().then(function () {
            vm.prizeTypes = CommonModel.prizeTypes;

            if ($routeParams.prizeType && vm.prizeType) {
                var index = vm.prizeTypes.map(function (e) { return e.PrizeTypeId; }).indexOf(vm.prizeType);

                if (index === -1) {
                    $location.path(baseUrl + 'Home/AccessDenied');
                } else {
                    CommonModel.getPrizeSettingByPrizeType(vm.prizeType).then(function () {
                        vm.prizeSetting = CommonModel.prizeSetting;
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
[function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();

                switch (event.which) {
                    case 13: // enter
                        scope.prizeListCtrl.redirectToWinnerDetail();
                        break;
                    case 89: // ctrl+y to next
                        if (event.ctrlKey) {
                            scope.prizeListCtrl.nextPrize();
                        }
                        break;
                    case 90: // ctrl+z to back
                        if (event.ctrlKey) {
                            scope.prizeListCtrl.backPrize();
                        }
                        break;
                    case 27: // asc
                        scope.prizeListCtrl.redirectToServerIndex();
                        break;
                }
            });
        }
    };
} ]);