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

    serverModel.insertWinner = function (winners) {
        return serverFactory.insertWinner(winners)
        .success(function (response) {
            if (response.IsSuccessful) {
            } else {
                notificationFactory.error("", response.Message);
            }
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.updateWinner = function (vm, winners) {
        if (vm.prizeSetting.CurrentPrize > 0 && !vm.isDelete) {
            vm.prizeSetting.CurrentPrize -= 1;

            var criteria = {
                Winners: winners,
                PrizeTypeId: vm.prizeType,
                CurrentPrize: vm.prizeSetting.CurrentPrize
            };

            return serverFactory.updateWinner(criteria)
            .success(function (response) {
                if (response.IsSuccessful) {
                    vm.prizedWinners.push(winners[0]);
                } else {
                    notificationFactory.error("", response.Message);
                }
            }).error(function (response, status) {
                errorFactory.errorHandle(status);
            });
        }
    };

    serverModel.openPopupConfirmRemoveWinner = function (vm, staff, index) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/Server/Templates/modalConfirmRemoveWinner.html',
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
            windowClass: "modal-confirm-center"
        });

        modalInstance.result.then(function () {
            var staffIds = vm.savedWinners.map(function (e) { return e.StaffId; }).join(',');
            serverFactory.deleteWinner(staffIds)
            .success(function (response) {
                if (response.IsSuccessful) {
                    vm.savedWinners = [];
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

    serverModel.openPopupConfirmResetPrize = function (vm) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/Server/Templates/modalConfirmRemoveWinner.html',
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
            windowClass: "modal-confirm-center"
        });

        modalInstance.result.then(function () {
            var winners = [],
                currentPrize = (parseInt((vm.startPrize - 1) / 3) + 1) * 3;

            if (vm.winner1) {
                vm.winner1.Prize.PrizeId = -1;
                winners.push(vm.winner1);
            }

            if (vm.winner2) {
                vm.winner2.Prize.PrizeId = -1;
                winners.push(vm.winner2);
            }

            if (vm.winner3) {
                vm.winner3.Prize.PrizeId = -1;
                winners.push(vm.winner3);
            }

            var criteria = {
                Winners: winners,
                PrizeTypeId: vm.prizeType,
                CurrentPrize: currentPrize
            };

            serverFactory.updateWinner(criteria)
            .success(function (response) {
                if (response.IsSuccessful) {
                    vm.winner1.PrizeNumber = null;
                    vm.winner2.PrizeNumber = null;
                    vm.winner3.PrizeNumber = null;
                    vm.prizedWinners = [];
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