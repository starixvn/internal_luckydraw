'use strict';
/*global serverModule*/

serverModule.controller('WinnerDetailController',
['$window', '$routeParams', 'ServerModel', 'CommonModel', 'baseUrl',
function ($window, $routeParams, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.prizeType = parseInt($routeParams.prizeType);

    vm.greenStaff = null;
    vm.pinkStaff = null;
    vm.blueStaff = null;
    vm.signalFromServerControl = false;

    var hubProxy = $.connection.hubs;

    var sendMessageResponse = function (oldWinner, newWinner) {
        var grade = parseInt(newWinner.StaffCode.substring(0, 2)),
            message = '';

        if (vm.winnerDetail.Winners.length > 2) {
            message = 'Full winner.';
        } else if (newWinner.StaffCode === oldWinner.StaffCode) {
            message = 'Barcode duplicated.';
        } else {
            message = 'Group had winner.';
        }

        if (message.length > 0) {
            ServerModel.sendMessageResponse(hubProxy, grade, message);
        }
    };

    vm.getClassPrizeByPrizeType = function (prizeType) {
        return ServerModel.getClassPrizeByPrizeType(prizeType);
    }

    vm.getClassLogoByPrizeType = function (prizeType) {
        return ServerModel.getClassLogoByPrizeType(prizeType);
    }

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
        vm.staffInfoRemove = null;

        switch (index) {
            case 1:
                if (vm.pinkStaff) {
                    vm.staffInfoRemove = {
                        staff: vm.pinkStaff,
                        index: index
                    };
                }
                break;
            case 2:
                if (vm.greenStaff) {
                    vm.staffInfoRemove = {
                        staff: vm.greenStaff,
                        index: index
                    };
                }
                break;
            case 3:
                if (vm.blueStaff) {
                    vm.staffInfoRemove = {
                        staff: vm.blueStaff,
                        index: index
                    };
                }
                break;
        }

        if (vm.staffInfoRemove) {
            if (vm.signalFromServerControl) {
                hubProxy.server.sendStaffInfoToServerControl(vm.staffInfoRemove.staff);
            } else {
                ServerModel.openPopupConfirmRemoveWinner(vm, vm.staffInfoRemove.staff, vm.staffInfoRemove.index);
            }
        }
    };

    vm.redirectToPrizeList = function () {
        var path = baseUrl + 'Server/PrizeList/' + vm.prizeType;

        $window.location.href = path;
    };

    vm.fillWinner = function (winner) {
        var grade = parseInt(winner.StaffCode.substring(0, 2)),
            isValid = true;

        switch (grade) {
            case 10:
                if (!vm.pinkStaff) {
                    vm.pinkStaff = winner;
                } else {
                    isValid = false;
                    sendMessageResponse(vm.pinkStaff, winner);
                }
                break;
            case 11:
                if (!vm.greenStaff) {
                    vm.greenStaff = winner;
                } else {
                    isValid = false;
                    sendMessageResponse(vm.greenStaff, winner);
                }
                break;
            case 12:
                if (!vm.blueStaff) {
                    vm.blueStaff = winner;
                } else {
                    isValid = false;
                    sendMessageResponse(vm.blueStaff, winner);
                }
                break;
        }

        return isValid;
    };

    hubProxy.client.getStaffCode = function (staffCode) {
        CommonModel.getStaffByCode(staffCode).then(function () {
            var staff = CommonModel.staff;

            if (staff && staff.StaffCode) {
                if (staff.Prize && staff.Prize.PrizeId !== vm.winnerDetail.Prize.PrizeId) {
                    var grade = parseInt(staff.StaffCode.substring(0, 2)),
                        message = 'Staff received other prize.';

                    ServerModel.sendMessageResponse(hubProxy, grade, message);
                } else if (vm.fillWinner(staff)) {
                    staff.Prize = vm.winnerDetail.Prize;
                    vm.winnerDetail.Winners.push(staff);

                    if (vm.winnerDetail.Winners.length === 3) {
                        vm.prizeSetting.CurrentPrize -= 1;
                    }

                    var criteria = {
                        Winners: new Array(staff),
                        PrizeTypeId: vm.prizeType,
                        CurrentPrize: vm.prizeSetting.CurrentPrize
                    };

                    ServerModel.insertWinner(criteria);
                }
            }
        });
    };

    hubProxy.client.getSignalEnter = function () {
        vm.redirectToPrizeList();
    };

    hubProxy.client.getSignalEsc = function () {
        vm.redirectToPrizeList();
    };

    hubProxy.client.getSignalNumber = function (number) {
        if (number > 0 && number < 4) {
            vm.signalFromServerControl = true;
            vm.removeWinner(number);
        }
    };

    hubProxy.client.getSignalConfirmFromServerControl = function (isOk, key) {
        if (isOk && key === 'remove') {
            ServerModel.removeWinner(vm, vm.staffInfoRemove.staff, vm.staffInfoRemove.index);
        }
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
[function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();

                switch (event.which) {
                    case 13: // enter
                    case 27: // asc
                        scope.winnerDetailCtrl.redirectToPrizeList();
                        break;
                    case 49: // 1
                    case 50: // 2
                    case 51: // 3
                        var number = parseInt(String.fromCharCode(event.which));
                        scope.winnerDetailCtrl.signalFromServerControl = false;
                        scope.winnerDetailCtrl.removeWinner(number);

                        break;
                }
            });
        }
    };
} ]);