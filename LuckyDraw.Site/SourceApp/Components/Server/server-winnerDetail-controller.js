'use strict';
/*global serverModule*/

serverModule.controller('WinnerDetailController',
['$routeParams', 'ServerModel', 'CommonModel', 'baseUrl',
function ($routeParams, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.prizeType = parseInt($routeParams.prizeType);

    vm.nextPrize = false;

    vm.greenStaff = null;
    vm.pinkStaff = null;
    vm.blueStaff = null;

    vm.initialize = function () {
        CommonModel.getPrizeType().then(function () {
            vm.prizeTypes = CommonModel.prizeTypes;

            if ($routeParams.prizeType && vm.prizeType) {
                var index = vm.prizeTypes.map(function (e) { return e.PrizeTypeId; }).indexOf(vm.prizeType);

                if (index === -1) {
                    $location.path(baseUrl + 'Home/AccessDenied');
                } else {
                    CommonModel.getPrizeSettingByPrizeType(vm.prizeType).then(function () {
                        if (CommonModel.prizeSetting) {
                            vm.prizeSetting = CommonModel.prizeSetting;
                            vm.currentPrize = vm.prizeSetting.CurrentPrize;

                            ServerModel.getWinnerDetail(vm.prizeType, vm.currentPrize).then(function () {
                                vm.winnerDetail = ServerModel.winnerDetail;

                                if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                                    angular.forEach(vm.winnerDetail.Winners, function (winner) {
                                        vm.fillWinner(winner);
                                    });
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

    var hubProxy = $.connection.hubs;

    vm.fillWinner = function (winner) {
        var grade = parseInt(winner.StaffCode.substring(0, 2)),
            message = '';

        switch (grade) {
            case 10:
                if (!vm.pinkStaff) {
                    vm.pinkStaff = winner;
                } else {
                    if (winner.StaffCode === vm.pinkStaff.StaffCode) {
                        message = 'Barcode duplicated, barcode: ' + winner.StaffCode;
                        hubProxy.server.sendResponse(grade, message);
                    } else {
                        message = 'Wrong group, barcode: ' + winner.StaffCode;
                        hubProxy.server.sendResponse(grade, message);
                    }
                }
                break;
            case 11:
                if (!vm.greenStaff) {
                    vm.greenStaff = winner;
                } else {
                    if (winner.StaffCode === vm.greenStaff.StaffCode) {
                        message = 'Barcode duplicated, barcode: ' + winner.StaffCode;
                        hubProxy.server.sendResponse(grade, message);
                    } else {
                        message = 'Wrong group, barcode: ' + winner.StaffCode;
                        hubProxy.server.sendResponse(grade, message);
                    }
                }
                break;
            case 12:
                if (!vm.blueStaff) {
                    vm.blueStaff = winner;
                } else {
                    if (winner.StaffCode === vm.blueStaff.StaffCode) {
                        message = 'Barcode duplicated, barcode: ' + winner.StaffCode;
                        hubProxy.server.sendResponse(grade, message);
                    } else {
                        message = 'Wrong group, barcode: ' + winner.StaffCode;
                        hubProxy.server.sendResponse(grade, message);
                    }
                }
                break;
        }
    };

    hubProxy.client.getStaffCode = function (staffCode) {
        CommonModel.getStaffByCode(staffCode).then(function () {
            var staff = CommonModel.staff;

            if (staff && staff.StaffCode) {
                vm.fillWinner(staff);
            }
        });
    };

    $.connection.hub.start();
} ]);

serverModule.controller('RemoveWinnerController',
['$modalInstance', 'staff', 'isRemoveAll', 'isReset',
function ($modalInstance, staff, isRemoveAll, isReset) {
    var vm = this;

    vm.staff = staff;
    vm.isRemoveAll = isRemoveAll;
    vm.isReset = isReset;

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
                event.preventDefault();
                var path = '';

                switch (event.which) {
                    case 13: // enter
                    case 27: // asc
                        path = baseUrl + 'Server/PrizeList/' + scope.winnerDetailCtrl.prizeType;
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