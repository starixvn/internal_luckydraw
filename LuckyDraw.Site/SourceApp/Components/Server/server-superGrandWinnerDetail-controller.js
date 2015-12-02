'use strict';
/*global serverModule*/

serverModule.controller('SuperGrandWinnerDetailController',
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