'use strict';
/*global serverModule*/

serverModule.controller('AdhocPrizeController',
['$window', 'ServerModel', 'CommonModel', 'baseUrl',
function ($window, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.prizeType = 1; // adhoc
    vm.baseUrl = baseUrl;
    vm.currentWinner = null;
    vm.currentQuantity = 0;
    vm.numColumn = 4;
    vm.winners = [];

    var initializeListWinner = function () {
        for (var i = 0; i < vm.numRow; i++) {
            var itemRows = [];

            for (var j = 0; j < vm.numColumn; j++) {
                var index = (i * vm.numColumn) + j;

                if (index < vm.prizeSetting.Quantity) {
                    var itemRow = {};
                    itemRows.push(itemRow);
                }
            }

            vm.winners.push(itemRows);
        }
    };

    vm.initialize = function () {
        CommonModel.getPrizeSettingByPrizeType(vm.prizeType).then(function () {
            if (CommonModel.prizeSetting) {
                vm.prizeSetting = CommonModel.prizeSetting;

                vm.numRow = (vm.prizeSetting.Quantity % vm.numColumn) === 0 ? parseInt(vm.prizeSetting.Quantity / vm.numColumn) : parseInt(vm.prizeSetting.Quantity / vm.numColumn) + 1;
                initializeListWinner();

                ServerModel.getAdhocWinnerDetail().then(function () {
                    vm.winnerDetail = ServerModel.winnerDetail;

                    if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                        vm.currentQuantity = vm.winnerDetail.Winners.length;

                        angular.forEach(vm.winnerDetail.Winners, function (winner) {

                        });
                    }
                });
            }
        });
    };

    vm.initialize();

    vm.getCssClass = function (staffCode) {
        var result = '';

        if (staffCode) {
            switch (staffCode.substring(0, 2)) {
                case '10':
                    result = 'pink-staff';
                    break;
                case '11':
                    result = 'green-staff';
                    break;
                case '12':
                    result = 'blue-staff';
                    break;
            }
        }

        return result;
    };

    vm.redirectToServerIndex = function () {
        var path = baseUrl + 'Server/Index';

        $window.location.href = path;
    };

    var hubProxy = $.connection.hubs;

    var sendMessageResponse = function (winner) {
        var isValid = true,
            grade = parseInt(winner.StaffCode.substring(0, 2)),
            message = '';

        var index = vm.winnerDetail.Winners.map(function (e) { return e.StaffCode; }).indexOf(winner.StaffCode);

        if (vm.winnerDetail.Winners.length > vm.prizeSetting.Quantity) {
            message = 'Full winner.';
        } else if (index !== -1) {
            message = 'Barcode duplicated.';
        }

        if (message.length > 0) {
            isValid = false;
            ServerModel.sendMessageResponse(hubProxy, grade, message);
        }

        return isValid;
    };

    hubProxy.client.getStaffCode = function (staffCode) {
        CommonModel.getStaffByCode(staffCode).then(function () {
            var staff = CommonModel.staff;

            if (staff && staff.StaffCode && ServerModel.checkWinner(hubProxy, staff)) {
                staff.Prize = vm.winnerDetail.Prize;
                vm.winnerDetail.Winners.push(staff);

                var criteria = {
                    Winners: new Array(staff),
                    PrizeTypeId: vm.prizeType
                };

                ServerModel.insertWinner(criteria);
            }
        });
    };

    $.connection.hub.start();
} ]);

serverModule.directive('adhocPrizeKeyboardEvent',
[function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();

                switch (event.which) {
                    case 13: // enter
                    case 27: // asc
                        scope.adhocPrizeCtrl.redirectToServerIndex();
                        break;
                }
            });
        }
    };
} ]);