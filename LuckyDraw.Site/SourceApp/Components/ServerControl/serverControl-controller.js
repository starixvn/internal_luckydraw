'use strict';
/*global serverControlModule*/

serverControlModule.controller('ServerControlController',
['ServerControlModel',
function (ServerControlModel) {
    var vm = this;

    var hubProxy = $.connection.hubs;

    vm.sendSignalConfirm = function (isOk, key) {
        hubProxy.server.sendSignalConfirm(isOk, key);
    };

    hubProxy.client.getStaffInfo = function (staff) {
        if (staff) {
            ServerControlModel.openPopupConfirmRemoveWinner(vm, staff);
        }
    };

    vm.enter = function () {
        hubProxy.server.sendSignalEnter();
    }

    vm.esc = function () {
        hubProxy.server.sendSignalEsc();
    }

    vm.number = function (number) {
        hubProxy.server.sendSignalNumber(number);
    }

    vm.ctrlZ = function () {
        hubProxy.server.sendSignalCtrlZ();
    }

    vm.ctrlY = function () {
        hubProxy.server.sendSignalCtrlY();
    }

    vm.ctrlD = function () {
        ServerControlModel.openPopupConfirmRemoveAllWinner(vm);
    }

    vm.ctrlE = function () {
        ServerControlModel.openPopupConfirmResetPrize(vm);
    }

    vm.moveUp = function () {
        hubProxy.server.sendSignalMoveUp();
    }

    vm.moveDown = function () {
        hubProxy.server.sendSignalMoveDown();
    }

    vm.deleteWinner = function () {
        hubProxy.server.sendSignalDelete();
    }

    vm.sort = function () {
        hubProxy.server.sendSignalCharacterS();
    }

    $.connection.hub.start();
} ]);

serverControlModule.controller('RemoveWinnerController',
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