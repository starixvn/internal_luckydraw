'use strict';
/*global serverModule*/

serverModule.factory('ServerModel',
['$modal', 'serverFactory', 'notificationFactory', 'errorFactory',
function ($modal, serverFactory, notificationFactory, errorFactory) {
    var serverModel = {};

    serverModel.prizeList = [];

    serverModel.renderListPrize = function (prizeType) {
        return serverFactory.renderListPrize(prizeType)
        .success(function (prizeList) {
            serverModel.prizeList = prizeList;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.openPopupConfirmRemoveWinner = function (vm, staff, index) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/Server/Templates/modalConfirmRemoveWinner.html',
            controller: "RemoveWinnerController as removeWinnerCtrl",
            backdrop: 'static',
            resolve: {
                staff: function () {
                    return angular.copy(staff);
                }
            },
            windowClass: "modal-confirm-center"
        });

        modalInstance.result.then(function () {
            switch (index) {
                case 1:
                    vm.pinkStaff = null;
                    break;
                case 2:
                    vm.greenStaff = null;
                    break;
                case 3:
                    vm.blueStaff = null;
                    break;
            }
        })['finally'](function () {
            modalInstance = '';
        });
    };

    serverModel.updateCurrentPrize = function (criteria) {
        return serverFactory.updateCurrentPrize(criteria)
        .success(function (response) {
            if (response.IsSuccessful) {
                notificationFactory.success("UpdatedSuccessfully");
                modalInstance.close(kpi);
            }
            else {
                notificationFactory.error("", response.Message);
            }
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    return serverModel;
} ]);