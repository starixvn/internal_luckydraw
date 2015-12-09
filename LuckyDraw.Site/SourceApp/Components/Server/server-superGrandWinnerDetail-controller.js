'use strict';
/*global serverModule*/

serverModule.controller('SuperGrandWinnerDetailController',
['$window', '$scope', '$routeParams', 'ServerModel', 'CommonModel', 'baseUrl',
function ($window, $scope, $routeParams, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.prizeType = parseInt($routeParams.prizeType);

    vm.focusWinner = 4;
    vm.prizedWinners = [];

    vm.isFinished = false;

    vm.winner1 = null;
    vm.winner2 = null;
    vm.winner3 = null;

    var fillWinnerByPrizeNumber = function (winner, position, prizeIndex) {
        ServerModel.getPrizeNumber(prizeIndex).then(function () {
            if (ServerModel.prizeNumber) {
                winner.PrizeNumber = baseUrl + ServerModel.prizeNumber;
            }

            switch (position) {
                case 1:
                    vm.winner3 = winner;
                    break;
                case 2:
                    vm.winner2 = winner;
                    break;
                case 3:
                    vm.winner1 = winner;
                    break;
            }
        });
    };

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
                            vm.startPrize = vm.prizeSetting.CurrentPrize;

                            ServerModel.getSuperGrandWinnerDetail(vm.prizeType, vm.startPrize).then(function () {
                                vm.winnerDetail = ServerModel.winnerDetail;

                                if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                                    angular.forEach(vm.winnerDetail.Winners, function (winner, index) {
                                        var prizeIndex = winner.Prize.PrizeId < 0 ? -1 : winner.Prize.PrizeIndex;

                                        if (vm.winnerDetail.IsSamePrize) {
                                            vm.fillWinner(winner);
                                        } else {
                                            fillWinnerByPrizeNumber(winner, index + 1, prizeIndex);
                                        }

                                        if (prizeIndex !== -1) {
                                            vm.prizedWinners.push(winner);
                                        }
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

    vm.getClass = function (staffCode) {
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

    vm.redirectToPrizeList = function () {
        var path = baseUrl + 'Server/PrizeList/' + vm.prizeType;

        $window.location.href = path;
    };

    var setFocusWinner = function (focusWinner) {
        switch (focusWinner) {
            case 1:
                if (vm.winner3) {
                    vm.winner3.IsFocus = true;
                }
                if (vm.winner1) {
                    vm.winner1.IsFocus = false;
                }
                if (vm.winner2) {
                    vm.winner2.IsFocus = false;
                }
                $scope.$apply();
                break;
            case 2:
                if (vm.winner2) {
                    vm.winner2.IsFocus = true;
                }
                if (vm.winner1) {
                    vm.winner1.IsFocus = false;
                }
                if (vm.winner3) {
                    vm.winner3.IsFocus = false;
                }
                $scope.$apply();
                break;
            case 3:
                if (vm.winner1) {
                    vm.winner1.IsFocus = true;
                }
                if (vm.winner2) {
                    vm.winner2.IsFocus = false;
                }
                if (vm.winner3) {
                    vm.winner3.IsFocus = false;
                }
                $scope.$apply();
                break;
        }
    };

    vm.keyUpArrowEvent = function () {
        if (vm.focusWinner < 3 && vm.winnerDetail.Winners.length > 0) {
            vm.focusWinner += 1;

            setFocusWinner(vm.focusWinner);
        }
    };

    vm.keyDownArrowEvent = function () {
        if (vm.focusWinner > 1 && vm.winnerDetail.Winners.length > 0) {
            vm.focusWinner -= 1;

            setFocusWinner(vm.focusWinner);
        }
    };

    vm.removeSpecialWinner = function () {
        if (vm.focusWinner > 0 && vm.focusWinner < 4) {
            var staff = null;

            switch (vm.focusWinner) {
                case 1:
                    staff = vm.winner3;
                    break;
                case 2:
                    staff = vm.winner2;
                    break;
                case 3:
                    staff = vm.winner1;
                    break;
            }

            if (staff) {
                ServerModel.openPopupConfirmRemoveSpecialWinner(vm, staff, vm.focusWinner);
            }
        }
    };

    vm.choosePrize = function (prize) {
        var prizeId = vm.winnerDetail.Prizes[(prize - 1) % 3].PrizeId,
            index = vm.prizedWinners.map(function (e) { return e.Prize.PrizeId; }).indexOf(prizeId);

        if (vm.winnerDetail.Winners.length >= 3
                && (prize > 0 && prize < 10)
                && index === -1
                && (parseInt((vm.startPrize - 1) / 3) === parseInt((prize - 1) / 3))) {
            ServerModel.getPrizeNumber(prize).then(function () {
                if (ServerModel.prizeNumber) {
                    var winners = [];
                    switch (vm.focusWinner) {
                        case 1:
                            if (vm.winner3) {
                                vm.winner3.PrizeNumber = baseUrl + ServerModel.prizeNumber;
                                vm.winner3.Prize = vm.winnerDetail.Prizes[(prize - 1) % 3];

                                winners.push(vm.winner3);
                                vm.prizedWinners.push(vm.winner3);
                                vm.prizeSetting.CurrentPrize = (parseInt((vm.prizeSetting.CurrentPrize - 1) / 3) + 1) * 3 - vm.prizedWinners.length;
                                if (vm.prizeSetting.CurrentPrize < 0) {
                                    vm.prizeSetting.CurrentPrize = 0;
                                }

                                ServerModel.superGrandUpdateWinner(vm, winners);
                            }
                            break;
                        case 2:
                            if (vm.winner2) {
                                vm.winner2.PrizeNumber = baseUrl + ServerModel.prizeNumber;
                                vm.winner2.Prize = vm.winnerDetail.Prizes[(prize - 1) % 3];

                                winners.push(vm.winner2);
                                vm.prizedWinners.push(vm.winner2);
                                vm.prizeSetting.CurrentPrize = (parseInt((vm.prizeSetting.CurrentPrize - 1) / 3) + 1) * 3 - vm.prizedWinners.length;
                                if (vm.prizeSetting.CurrentPrize < 0) {
                                    vm.prizeSetting.CurrentPrize = 0;
                                }

                                ServerModel.superGrandUpdateWinner(vm, winners);
                            }
                            break;
                        case 3:
                            if (vm.winner1) {
                                vm.winner1.PrizeNumber = baseUrl + ServerModel.prizeNumber;
                                vm.winner1.Prize = vm.winnerDetail.Prizes[(prize - 1) % 3];

                                winners.push(vm.winner1);
                                vm.prizedWinners.push(vm.winner1);
                                vm.prizeSetting.CurrentPrize = (parseInt((vm.prizeSetting.CurrentPrize - 1) / 3) + 1) * 3 - vm.prizedWinners.length;
                                if (vm.prizeSetting.CurrentPrize < 0) {
                                    vm.prizeSetting.CurrentPrize = 0;
                                }

                                ServerModel.superGrandUpdateWinner(vm, winners);
                            }
                            break;
                    }
                }
            });
        }
    };

    vm.removeAllWinner = function () {
        if (vm.winnerDetail.Winners.length > 0) {
            ServerModel.openPopupConfirmRemoveAllWinner(vm);
        }
    };

    vm.resetPrizeWinners = function () {
        if (vm.winnerDetail.Winners.length > 0 && !vm.winnerDetail.IsSamePrize) {
            ServerModel.openPopupConfirmResetPrize(vm);
        }
    };

    vm.sortPrize = function () {
        if (!vm.winnerDetail.IsSamePrize) {
            ServerModel.getSuperGrandWinnerDetail(vm.prizeType, vm.startPrize).then(function () {
                vm.winnerDetail = ServerModel.winnerDetail;

                if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                    angular.forEach(vm.winnerDetail.Winners, function (winner, index) {
                        var prizeIndex = winner.Prize.PrizeId < 0 ? -1 : winner.Prize.PrizeIndex;

                        fillWinnerByPrizeNumber(winner, index + 1, prizeIndex);

                        if (prizeIndex !== -1) {
                            vm.prizedWinners.push(winner);
                        }
                    });
                }
            });
        }
    };

    var hubProxy = $.connection.hubs;

    vm.sendMessageResponse = function (oldWinner, newWinner) {
        var grade = parseInt(newWinner.StaffCode.substring(0, 2)),
            message = '';

        if (vm.winnerDetail.IsSamePrize) {
            if (vm.prizedWinners.length >= 3) {
                message = 'Full, barcode: ' + newWinner.StaffCode;
            } else if (newWinner.StaffCode === oldWinner.StaffCode) {
                message = 'Barcode duplicated, barcode: ' + newWinner.StaffCode;
            } else {
                message = 'Wrong group, barcode: ' + newWinner.StaffCode;
            }
        } else {
            if (vm.winnerDetail.Winners.length >= 3) {
                message = 'Full, barcode: ' + newWinner.StaffCode;
            } else if (newWinner.StaffCode === oldWinner.StaffCode) {
                message = 'Barcode duplicated, barcode: ' + newWinner.StaffCode;
            } else {
                message = 'Wrong group, barcode: ' + newWinner.StaffCode;
            }
        }

        if (message.length > 0) {
            hubProxy.server.sendMessageResponse(grade, message);
        }
    };

    vm.fillWinner = function (winner) {
        var grade = parseInt(winner.StaffCode.substring(0, 2)),
            isValid = true;

        switch (grade) {
            case 10:
                if (!vm.winner1) {
                    vm.winner1 = winner;
                } else {
                    isValid = false;
                    vm.sendMessageResponse(vm.winner1, winner);
                }
                break;
            case 11:
                if (!vm.winner2) {
                    vm.winner2 = winner;
                } else {
                    isValid = false;
                    vm.sendMessageResponse(vm.winner2, winner);
                }
                break;
            case 12:
                if (!vm.winner3) {
                    vm.winner3 = winner;
                } else {
                    isValid = false;
                    vm.sendMessageResponse(vm.winner3, winner);
                }
                break;
        }

        return isValid;
    };

    hubProxy.client.getStaffCode = function (staffCode) {
        CommonModel.getStaffByCode(staffCode).then(function () {
            var staff = CommonModel.staff;

            if (staff && staff.StaffCode) {
                if (vm.winnerDetail.IsSamePrize && vm.fillWinner(staff)) {
                    vm.prizedWinners.push(staff);
                    vm.winnerDetail.Winners.push(staff);

                    angular.forEach(vm.winnerDetail.Prizes, function (prize, index) {
                        if (vm.prizedWinners[index]) {
                            vm.prizedWinners[index].Prize = prize;
                        }
                    });

                    vm.prizeSetting.CurrentPrize = (parseInt((vm.prizeSetting.CurrentPrize - 1) / 3) + 1) * 3 - vm.prizedWinners.length;

                    var criteria = {
                        Winners: vm.prizedWinners,
                        PrizeTypeId: vm.prizeType,
                        CurrentPrize: vm.prizeSetting.CurrentPrize
                    };

                    ServerModel.insertWinner(criteria);
                } else {
                    if (vm.fillWinner(staff)) {
                        staff.Prize = {
                            PrizeId: -1
                        };
                        vm.winnerDetail.Winners.push(staff);

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

    $.connection.hub.start();
} ]);

serverModule.directive('superGrandWinnerDetailKeyboardEvent',
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
                    case 38: // up arrow
                        scope.winnerDetailCtrl.keyUpArrowEvent();
                        break;
                    case 40: // down arrow
                        scope.winnerDetailCtrl.keyDownArrowEvent();
                        break;
                    case 46: // delete
                        scope.winnerDetailCtrl.removeSpecialWinner();
                        break;
                    case 49: // 1
                    case 50: // 2
                    case 51: // 3
                    case 52: // 4
                    case 53: // 5
                    case 54: // 6
                    case 55: // 7
                    case 56: // 8
                    case 57: // 9
                        var prizeNumber = parseInt(String.fromCharCode(event.which));
                        scope.winnerDetailCtrl.choosePrize(prizeNumber);
                        break;
                    case 68: // press ctrl+d to remove winners
                        if (event.ctrlKey) {
                            scope.winnerDetailCtrl.removeAllWinner();
                        }
                        break;
                    case 69: // press ctrl+e to reset prize
                        if (event.ctrlKey) {
                            scope.winnerDetailCtrl.resetPrizeWinners();
                        }
                        break;
                    case 83: // s
                        scope.winnerDetailCtrl.sortPrize();
                        break;
                }
            });
        }
    };
} ]);

serverModule.directive('focusEvent',
[function () {
    return {
        link: function (scope, element, attrs) {
            attrs.$observe('isFocus', function (value) {
                if (value === 'true') {
                    $(element).addClass('focus');
                } else {
                    $(element).removeClass('focus');
                }
            });
        }
    };
} ]);