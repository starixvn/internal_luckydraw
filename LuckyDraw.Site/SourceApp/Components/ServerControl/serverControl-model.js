'use strict';
/*global serverControlModule*/

serverControlModule.factory('ServerControlModel',
['$modal',
function ($modal) {
    var serverControlModel = {};

    serverControlModel.openPopupConfirmRemoveWinner = function (vm, staff) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/ServerControl/Templates/modalConfirm.html',
            controller: "RemoveWinnerController as removeWinnerCtrl",
            backdrop: 'static',
            resolve: {
                staff: function () {
                    return angular.copy(staff);
                },
                isRemoveAll: function () {
                    return false;
                },
                isReset: function () {
                    return false;
                }
            },
            windowClass: "modal-confirm"
        });

        modalInstance.result.then(function () {
            vm.sendSignalConfirm(true, 'remove');
        }, function () {
            vm.sendSignalConfirm(false, 'remove');
        })['finally'](function () {
            modalInstance = '';
        });
    };

    serverControlModel.openPopupConfirmRemoveAllWinner = function (vm) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/ServerControl/Templates/modalConfirm.html',
            controller: "RemoveWinnerController as removeWinnerCtrl",
            backdrop: 'static',
            resolve: {
                staff: function () {
                    return null;
                },
                isRemoveAll: function () {
                    return true;
                },
                isReset: function () {
                    return false;
                }
            },
            windowClass: "modal-confirm"
        });

        modalInstance.result.then(function () {
            vm.sendSignalConfirm(true, 'remove-all');
        }, function () {
            vm.sendSignalConfirm(false, 'remove-all');
        })['finally'](function () {
            modalInstance = '';
        });
    };

    serverControlModel.openPopupConfirmResetPrize = function (vm) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/ServerControl/Templates/modalConfirm.html',
            controller: "RemoveWinnerController as removeWinnerCtrl",
            backdrop: 'static',
            resolve: {
                staff: function () {
                    return null;
                },
                isRemoveAll: function () {
                    return false;
                },
                isReset: function () {
                    return true;
                }
            },
            windowClass: "modal-confirm"
        });

        modalInstance.result.then(function () {
            vm.sendSignalConfirm(true, 'reset');
        }, function () {
            vm.sendSignalConfirm(false, 'reset');
        })['finally'](function () {
            modalInstance = '';
        });
    };

    return serverControlModel;
} ]);