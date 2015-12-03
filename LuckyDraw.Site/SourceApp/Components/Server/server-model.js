'use strict';
/*global serverModule*/

serverModule.factory('ServerModel',
['$modal', 'serverFactory', 'notificationFactory', 'errorFactory',
function ($modal, serverFactory, notificationFactory, errorFactory) {
    var serverModel = {};

    serverModel.prizeList = [];
    serverModel.winnerDetail = null;
    serverModel.prizeNumber = null;

    serverModel.renderListPrize = function (prizeType) {
        return serverFactory.renderListPrize(prizeType)
        .success(function (prizeList) {
            serverModel.prizeList = prizeList;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.getWinnerDetail = function (prizeType, currentPrize) {
        return serverFactory.getWinnerDetail(prizeType, currentPrize)
        .success(function (winnerDetail) {
            serverModel.winnerDetail = winnerDetail;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.getSuperGrandWinnerDetail = function (prizeType, currentPrize) {
        return serverFactory.getSuperGrandWinnerDetail(prizeType, currentPrize)
        .success(function (winnerDetail) {
            serverModel.winnerDetail = winnerDetail;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.getPrizeNumber = function (prizeIndex) {
        return serverFactory.getPrizeNumber(prizeIndex)
        .success(function (prizeNumber) {
            serverModel.prizeNumber = prizeNumber;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.updateCurrentPrize = function (vm, criteria) {
        return serverFactory.updateCurrentPrize(criteria)
        .success(function (response) {
            if (response.IsSuccessful) {
                vm.renderListPrize();
            } else {
                notificationFactory.error("", response.Message);
            }
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
            serverFactory.deleteWinner(staff.StaffId)
            .success(function (response) {
                if (response.IsSuccessful) {
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
                } else {
                    notificationFactory.error("", response.Message);
                }
            }).error(function (response, status) {
                errorFactory.errorHandle(status);
            });
        })['finally'](function () {
            modalInstance = '';
        });
    };

    serverModel.openPopupConfirmRemoveAllWinner = function (vm) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/Server/Templates/modalConfirmRemoveWinner.html',
            controller: "RemoveWinnerController as removeWinnerCtrl",
            backdrop: 'static',
            resolve: {
                isRemoveAll: function () {
                    return true;
                }
            },
            windowClass: "modal-confirm-center"
        });

        modalInstance.result.then(function () {
            var staffIds = vm.saveWinner.map(function (e) { return e.StaffId; }).join(',');
            serverFactory.deleteWinner(staffIds)
            .success(function (response) {
                if (response.IsSuccessful) {
                    vm.saveWinner = [];
                } else {
                    notificationFactory.error("", response.Message);
                }
            }).error(function (response, status) {
                errorFactory.errorHandle(status);
            });
        })['finally'](function () {
            modalInstance = '';
        });
    };

    return serverModel;
} ]);