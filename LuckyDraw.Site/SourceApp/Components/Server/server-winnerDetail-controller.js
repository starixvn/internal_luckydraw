'use strict';
/*global serverModule*/

serverModule.controller('WinnerDetailController',
['$routeParams', 'ServerModel', 'CommonModel', 'baseUrl',
function ($routeParams, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.prizeType = parseInt($routeParams.prizeType);
    vm.currentPrize = parseInt($routeParams.currentPrize);

    vm.greenStaff = null;
    vm.pinkStaff = null;
    vm.blueStaff = null;

    vm.removeWinner = function (index) {
        switch (index) {
            case 1:
                if (vm.pinkStaff) {
                    ServerModel.openPopupConfirmRemoveWinner(vm, vm.pinkStaff, index);
                }
                break;
            case 2:
                if (vm.greenStaff) {
                    ServerModel.openPopupConfirmRemoveWinner(vm, vm.greenStaff, index);
                }
                break;
            case 3:
                if (vm.blueStaff) {
                    ServerModel.openPopupConfirmRemoveWinner(vm, vm.blueStaff, index);
                }
                break;
        }
    };

    vm.initialize = function () {
        CommonModel.getPrizeType().then(function () {
            vm.prizeTypes = CommonModel.prizeTypes;

            if ($routeParams.prizeType && vm.prizeType) {
                var index = vm.prizeTypes.map(function (e) { return e.PrizeTypeId; }).indexOf(vm.prizeType);

                if (index === -1) {
                    $location.path(baseUrl + 'Home/AccessDenied');
                } else {
                    ServerModel.getWinnerDetail(vm.prizeType, vm.currentPrize).then(function () {
                        vm.winnerDetail = ServerModel.winnerDetail;

                        if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                            angular.forEach(vm.winnerDetail.Winners, function (winner) {
                                switch (winner.StaffCode.substring(0, 2)) {
                                    case '10':
                                        if (!vm.pinkStaff) {
                                            vm.pinkStaff = winner;
                                        }
                                        break;
                                    case '11':
                                        if (!vm.greenStaff) {
                                            vm.greenStaff = winner;
                                        }
                                        break;
                                    case '12':
                                        if (!vm.blueStaff) {
                                            vm.blueStaff = winner;
                                        }
                                        break;
                                }
                            });
                        }
                    });
                }
            } else {
                $location.path(baseUrl + 'Home/AccessDenied');
            }
        });
    };

    vm.initialize();

    var hubProxy = $.connection.hubs;

    hubProxy.client.getStaffCode = function (staffCode) {
        CommonModel.getStaffByCode(staffCode).then(function () {
            var staff = CommonModel.staff;

            if (staff && staff.StaffCode) {
                switch (staff.StaffCode.substring(0, 2)) {
                    case '10':
                        if (!vm.pinkStaff) {
                            vm.pinkStaff = staff;
                        }
                        break;
                    case '11':
                        if (!vm.greenStaff) {
                            vm.greenStaff = staff;
                        }
                        break;
                    case '12':
                        if (!vm.blueStaff) {
                            vm.blueStaff = staff;
                        }
                        break;
                }
            }
        });
    };

    $.connection.hub.start().done(function () {
        console.log('Now connected, connection ID =' + $.connection.hub.id);
    });
} ]);

serverModule.controller('RemoveWinnerController',
['$modalInstance', 'staff', 'isRemoveAll',
function ($modalInstance, staff, isRemoveAll) {
    var vm = this;

    vm.staff = staff;
    vm.isRemoveAll = isRemoveAll;

    vm.ok = function () {
        $modalInstance.close();
    };

    vm.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
} ]);

serverModule.directive('winnerDetailKeyboardEvent',
['$window', 'baseUrl', function ($window, baseUrl) {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                var path = '';

                switch (event.which) {
                    case 27: // asc
                        path = baseUrl + 'Server/PrizeList/' + scope.winnerDetailCtrl.prizeType + '?isOpeningPrize';
                        break;
                    case 49: // 1
                        scope.winnerDetailCtrl.removeWinner(1);
                        break;
                    case 50: // 2
                        scope.winnerDetailCtrl.removeWinner(2);
                        break;
                    case 51: // 3
                        scope.winnerDetailCtrl.removeWinner(3);
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });
        }
    };
} ]);