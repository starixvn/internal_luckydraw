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
        }); openPopupConfirmRemoveWinner
    };

    serverModel.getSuperGrandWinnerDetail = function (prizeType, currentPrize) {
        return serverFactory.getSuperGrandWinnerDetail(prizeType, currentPrize)
        .success(function (winnerDetail) {
            serverModel.winnerDetail = winnerDetail;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.getAdhocWinnerDetail = function () {
        return serverFactory.getAdhocWinnerDetail()
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

    serverModel.insertWinner = function (criteria) {
        return serverFactory.insertWinner(criteria)
        .success(function (response) {
            if (response.IsSuccessful) {
            } else {
                notificationFactory.error("", response.Message);
            }
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    serverModel.superGrandUpdateWinner = function (vm, winners) {
        var criteria = {
            Winners: winners,
            PrizeTypeId: vm.prizeType,
            CurrentPrize: vm.prizeSetting.CurrentPrize
        };

        return serverFactory.updateWinner(criteria)
        .success(function (response) {
            if (response.IsSuccessful) {
            } else {
                notificationFactory.error("", response.Message);
            }
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });

        if (vm.prizeSetting.CurrentPrize > 0) {
            vm.prizeSetting.CurrentPrize -= 1;
        }
    };

    serverModel.sendMessageResponse = function (hubProxy, grade, message) {
        hubProxy.server.sendMessageResponse(grade, message);
    };

    serverModel.checkWinner = function (hubProxy, winner) {
        var isValid = true,
            grade = parseInt(winner.StaffCode.substring(0, 2)),
            message = '';

        if (winner.Prize && winner.Prize.PrizeId) {
            message = 'Staff received other prize.';
            serverModel.sendMessageResponse(hubProxy, grade, message);

            isValid = false;
        }

        return isValid;
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
            var currentPrize = vm.prizeSetting.CurrentPrize;
            if (vm.nextPrize) {
                currentPrize = vm.prizeSetting.CurrentPrize + 1;
                vm.nextPrize = false;

                if (currentPrize > (vm.prizeSetting.Quantity / vm.prizeSetting.Multiply)) {
                    currentPrize = parseInt(vm.prizeSetting.Quantity / vm.prizeSetting.Multiply);
                }
            }

            serverFactory.deleteWinner(staff.StaffId, vm.prizeType, currentPrize)
            .success(function (response) {
                if (response.IsSuccessful) {
                    vm.currentPrize = currentPrize;
                    vm.prizeSetting.CurrentPrize = currentPrize;
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

                    index = vm.winnerDetail.Winners.map(function (e) { return e.StaffId; }).indexOf(staff.StaffId);
                    if (index !== -1) {
                        vm.winnerDetail.Winners.splice(index, 1);
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

    serverModel.openPopupConfirmRemoveSpecialWinner = function (vm, staff, index) {
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
            if (((vm.prizeSetting.CurrentPrize * vm.prizeSetting.Multiply) < vm.prizeSetting.Quantity)
                && staff.Prize && staff.Prize.PrizeId > 0) {
                vm.prizeSetting.CurrentPrize += 1;
            }

            serverFactory.deleteWinner(staff.StaffId, vm.prizeType, vm.prizeSetting.CurrentPrize)
            .success(function (response) {
                if (response.IsSuccessful) {
                    switch (index) {
                        case 1:
                            vm.winner3 = null;
                            break;
                        case 2:
                            vm.winner2 = null;
                            break;
                        case 3:
                            vm.winner1 = null;
                            break;
                    }

                    index = vm.prizedWinners.map(function (e) { return e.StaffId; }).indexOf(staff.StaffId);
                    if (index !== -1) {
                        vm.prizedWinners.splice(index, 1);
                    }

                    index = vm.winnerDetail.Winners.map(function (e) { return e.StaffId; }).indexOf(staff.StaffId);
                    if (index !== -1) {
                        vm.winnerDetail.Winners.splice(index, 1);
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
            var staffIds = vm.winnerDetail.Winners.map(function (e) { return e.StaffId; }).join(','),
                currentPrize = 0;

            if (vm.startPrize > 0) {
                currentPrize = (parseInt((vm.startPrize - 1) / 3) + 1) * 3;
            }

            serverFactory.deleteWinner(staffIds, vm.prizeType, currentPrize)
            .success(function (response) {
                if (response.IsSuccessful) {
                    vm.winner1 = null;
                    vm.winner2 = null;
                    vm.winner3 = null;

                    vm.winnerDetail.Winners = [];
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
            var winners = [], currentPrize = 0;

            if (vm.startPrize > 0) {
                currentPrize = (parseInt((vm.startPrize - 1) / 3) + 1) * 3;
            }

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
                    if (vm.winner1 && vm.winner1.PrizeNumber) {
                        vm.winner1.PrizeNumber = null;
                    }

                    if (vm.winner2 && vm.winner2.PrizeNumber) {
                        vm.winner2.PrizeNumber = null;
                    }

                    if (vm.winner3 && vm.winner3.PrizeNumber) {
                        vm.winner3.PrizeNumber = null;
                    }

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