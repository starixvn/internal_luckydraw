'use strict';
/*global serverModule*/

serverModule.controller('AdhocPrizeController',
['$window', '$scope', 'ServerModel', 'CommonModel', 'baseUrl',
function ($window, $scope, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.prizeType = 1; // adhoc
    vm.baseUrl = baseUrl;
    vm.currentWinner = null;
    vm.currentQuantity = 0;
    vm.numRow = 2;
    vm.numColumn = 3;
    vm.winners = [];
    vm.currentIndexFocus = -1;

    var hubProxy = $.connection.hubs;

    vm.initializeListWinner = function () {
        vm.maxIndexFocus = 0;

        for (var i = 0; i < vm.numRow; i++) {
            var itemRows = [];

            for (var j = 0; j < vm.numColumn; j++) {
                var index = (i * vm.numColumn) + j;

                if (index < vm.prizeSetting.Quantity) {
                    var itemRow = {
                        IsFocus: false
                    };

                    itemRows.push(itemRow);
                    vm.maxIndexFocus += 1;
                }
            }

            vm.winners.push(itemRows);
        }
    };

    vm.fillWinner = function () {
        angular.forEach(vm.winnerDetail.Winners, function (winner, index) {
            var rowIndex = parseInt(index / vm.numColumn),
                colIndex = index % vm.numColumn;

            if (rowIndex < vm.numRow && colIndex < vm.numColumn && vm.winners[rowIndex][colIndex]) {
                winner.IsFocus = false;
                vm.winners[rowIndex][colIndex] = angular.copy(winner);
            }
        });
    };

    var resetFocus = function () {
        for (var i = 0; i < vm.numRow; i++) {
            for (var j = 0; j < vm.numColumn; j++) {
                if (vm.winners[i][j]) {
                    vm.winners[i][j].IsFocus = false;
                }
            }
        }
    };

    vm.initialize = function () {
        CommonModel.getPrizeSettingByPrizeType(vm.prizeType).then(function () {
            if (CommonModel.prizeSetting) {
                vm.prizeSetting = CommonModel.prizeSetting;

                var numRow = (vm.prizeSetting.Quantity % vm.numColumn) === 0 ? parseInt(vm.prizeSetting.Quantity / vm.numColumn) : parseInt(vm.prizeSetting.Quantity / vm.numColumn) + 1;
                if (numRow < vm.numRow) {
                    vm.numRow = numRow;
                }

                vm.initializeListWinner();
                vm.maxIndexFocus += 1;

                ServerModel.getAdhocWinnerDetail().then(function () {
                    vm.winnerDetail = ServerModel.winnerDetail;

                    if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                        vm.currentQuantity = vm.winnerDetail.Winners.length;
                        vm.fillWinner();
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
                    result = 'yellow-staff';
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

    vm.keyLeftArrowEvent = function () {
        if (vm.currentIndexFocus > 0) {
            resetFocus();
            vm.currentIndexFocus -= 1;

            var rowIndex = parseInt(vm.currentIndexFocus / vm.numColumn),
                colIndex = vm.currentIndexFocus % vm.numColumn;

            vm.winners[rowIndex][colIndex].IsFocus = true;
            $scope.$apply();
        }
    };

    vm.keyUpArrowEvent = function () {
        vm.keyLeftArrowEvent();
    };

    vm.keyRightArrowEvent = function () {
        if (vm.currentIndexFocus < vm.maxIndexFocus) {
            vm.currentIndexFocus += 1;

            var rowIndex = parseInt(vm.currentIndexFocus / vm.numColumn),
                colIndex = vm.currentIndexFocus % vm.numColumn;

            if (rowIndex < vm.numRow && vm.winners[rowIndex][colIndex]) {
                resetFocus();
                vm.winners[rowIndex][colIndex].IsFocus = true;
                $scope.$apply();
            }
        }
    };

    vm.keyDownArrowEvent = function () {
        vm.keyRightArrowEvent();
    };

    vm.removeWinner = function () {
        var rowIndex = parseInt(vm.currentIndexFocus / vm.numColumn),
            colIndex = vm.currentIndexFocus % vm.numColumn;

        if (vm.winnerDetail.Winners.length > 0 && vm.winners[rowIndex][colIndex] && vm.winners[rowIndex][colIndex].StaffId) {
            ServerModel.openPopupConfirmRemoveAdhocWinner(vm, vm.winners[rowIndex][colIndex]);
        }
    };

    hubProxy.client.getStaffCode = function (staffCode) {
        CommonModel.getStaffByCode(staffCode).then(function () {
            var staff = CommonModel.staff,
                grade, message;

            if (staff && staff.StaffCode) {
                grade = parseInt(staff.StaffCode.substring(0, 2));

                if (vm.winnerDetail.Winners.length >= vm.prizeSetting.Quantity) {
                    message = 'Full winner.';
                    ServerModel.sendMessageResponse(hubProxy, grade, message);
                } else {
                    if (staff.Prize) {
                        if (staff.Prize.PrizeId !== vm.winnerDetail.Prize.PrizeId) {
                            message = 'Staff received other prize.';
                            ServerModel.sendMessageResponse(hubProxy, grade, message);
                        } else {
                            message = 'Barcode duplicated.';
                            ServerModel.sendMessageResponse(hubProxy, grade, message);
                        }
                    } else {
                        staff.Prize = vm.winnerDetail.Prize;
                        vm.currentWinner = staff;
                        vm.winnerDetail.Winners.unshift(staff);
                        vm.currentQuantity = vm.winnerDetail.Winners.length;
                        vm.fillWinner();

                        var criteria = {
                            Winners: new Array(staff),
                            PrizeTypeId: vm.prizeType
                        };

                        ServerModel.insertWinner(criteria);
                    }
                }
            }
        });
    };

    hubProxy.client.getSignalDelete = function () {
        var rowIndex = parseInt(vm.currentIndexFocus / vm.numColumn),
            colIndex = vm.currentIndexFocus % vm.numColumn;

        if (vm.winnerDetail.Winners.length > 0 && vm.winners[rowIndex][colIndex] && vm.winners[rowIndex][colIndex].StaffId) {
            vm.staffInfoRemove = vm.winners[rowIndex][colIndex];
            hubProxy.server.sendStaffInfoToServerControl(vm.staffInfoRemove);
        } else {
            vm.staffInfoRemove = null;
        }
    };

    hubProxy.client.getSignalMoveUp = function () {
        vm.keyUpArrowEvent();
    };

    hubProxy.client.getSignalMoveDown = function () {
        vm.keyDownArrowEvent();
    };

    hubProxy.client.getSignalAtServerFromServerControl = function () {
        hubProxy.server.sendSignalFromServerToClient();
    };

    hubProxy.client.getSignalAtServerFromClient = function (number) {
        hubProxy.server.sendSignalServerToServerControl(number);
    };

    hubProxy.client.getSignalEsc = function () {
        vm.redirectToServerIndex();
    };

    hubProxy.client.getSignalConfirmFromServerControl = function (isOk, key) {
        if (isOk && key === 'remove') {
            ServerModel.removeAdhocWinner(vm, vm.staffInfoRemove);
        }
    };

    $.connection.hub.start().done(function () {
        hubProxy.server.sendCurrentPrizeTypeToServerControl(vm.prizeType);
    });
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
                    case 37: // left arrow
                        scope.adhocPrizeCtrl.keyLeftArrowEvent();
                        break;
                    case 38: // up arrow
                        scope.adhocPrizeCtrl.keyUpArrowEvent();
                        break;
                    case 39: // right arrow
                        scope.adhocPrizeCtrl.keyRightArrowEvent();
                        break;
                    case 40: // down arrow
                        scope.adhocPrizeCtrl.keyDownArrowEvent();
                        break;
                    case 46: // delete
                        scope.adhocPrizeCtrl.removeWinner();
                        break;
                }
            });
        }
    };
} ]);