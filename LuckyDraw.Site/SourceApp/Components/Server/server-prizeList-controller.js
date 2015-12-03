'use strict';
/*global serverModule*/

serverModule.controller('PrizeListController',
['$scope', '$window', '$document', '$location', '$routeParams', 'CommonModel', 'ServerModel', 'baseUrl',
function ($scope, $window, $document, $location, $routeParams, CommonModel, ServerModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.isOpeningPrize = $routeParams.isOpeningPrize ? true : false;
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
        ServerModel.renderListPrize(vm.prizeType).then(function () {
            vm.prizeList = ServerModel.prizeList;
        });
    };

    vm.updateCurrentPrize = function (currentPrize) {
        var criteria = {
            PrizeTypeId: vm.prizeType,
            CurrentPrize: currentPrize
        };

        ServerModel.updateCurrentPrize(vm, criteria);
    };

    vm.initialize = function () {
        if (vm.isOpeningPrize) {
            vm.renderListPrize();
        };

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
['$window', 'baseUrl', function ($window, baseUrl) {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                var path = '';

                switch (event.which) {
                    case 27: // asc
                        path = baseUrl + 'Server/Index';
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });

            jQuery(document).on('keypress', function (event) {
                var path = '';

                switch (event.which) {
                    case 13: // enter
                        if (scope.prizeListCtrl.isOpeningPrize && scope.prizeListCtrl.currentPrize) {
                            switch (scope.prizeListCtrl.prizeType) {
                                case 2:
                                    path = baseUrl + 'Server/SuperGrandWinnerDetail/' + scope.prizeListCtrl.prizeType + '?currentPrize=' + scope.prizeListCtrl.currentPrize;
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                    path = baseUrl + 'Server/WinnerDetail/' + scope.prizeListCtrl.prizeType + '?currentPrize=' + scope.prizeListCtrl.currentPrize;
                                    break;
                            }
                        } else {
                            scope.prizeListCtrl.renderListPrize();
                            scope.prizeListCtrl.isOpeningPrize = true;
                        }
                        break;
                    case 25: // ctrl+y to next
                        if (scope.prizeListCtrl.isOpeningPrize) {
                            if (scope.prizeListCtrl.prizeSetting.Multiply === 3) {
                                if (scope.prizeListCtrl.prizeSetting.CurrentPrize > 0) {
                                    scope.prizeListCtrl.prizeSetting.CurrentPrize -= 1;
                                    scope.prizeListCtrl.currentPrize -= 1;
                                    scope.prizeListCtrl.updateCurrentPrize(scope.prizeListCtrl.currentPrize);
                                }
                            } else {
                                if (scope.prizeListCtrl.prizeSetting.CurrentPrize > 0) {
                                    scope.prizeListCtrl.prizeSetting.CurrentPrize -= 3;
                                    scope.prizeListCtrl.currentPrize -= 3;
                                    scope.prizeListCtrl.updateCurrentPrize(scope.prizeListCtrl.currentPrize);
                                }
                            }
                        }
                        break;
                    case 26: // ctrl+z to back
                        if (scope.prizeListCtrl.isOpeningPrize) {
                            if (scope.prizeListCtrl.prizeSetting.Multiply === 3) {
                                if (scope.prizeListCtrl.prizeSetting.Quantity > (scope.prizeListCtrl.prizeSetting.CurrentPrize * 3)) {
                                    scope.prizeListCtrl.prizeSetting.CurrentPrize += 1;
                                    scope.prizeListCtrl.currentPrize += 1;
                                    scope.prizeListCtrl.updateCurrentPrize(scope.prizeListCtrl.currentPrize);
                                }
                            } else {
                                if (scope.prizeListCtrl.prizeSetting.Quantity > scope.prizeListCtrl.prizeSetting.CurrentPrize) {
                                    scope.prizeListCtrl.prizeSetting.CurrentPrize += 3;
                                    scope.prizeListCtrl.currentPrize += 3;
                                    scope.prizeListCtrl.updateCurrentPrize(scope.prizeListCtrl.currentPrize);
                                }
                            }
                        }
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });
        }
    };
} ]);