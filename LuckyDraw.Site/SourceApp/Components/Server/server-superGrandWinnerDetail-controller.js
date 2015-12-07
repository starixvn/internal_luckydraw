'use strict';
/*global serverModule*/

serverModule.controller('SuperGrandWinnerDetailController',
['$scope', '$routeParams', 'ServerModel', 'CommonModel', 'baseUrl',
function ($scope, $routeParams, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.prizeType = parseInt($routeParams.prizeType);

    vm.startPrize = 9;
    vm.focusWinner = 4;
    vm.prizedIndex = 3;
    vm.prizedWinners = [];
    vm.savedWinners = [];

    vm.isDelete = false;
    vm.isFinished = false;

    vm.winner1 = null;
    vm.winner2 = null;
    vm.winner3 = null;

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

                                        vm.fillWinner(winner, index + 1, prizeIndex);
                                        vm.savedWinners.push(winner);

                                        if (prizeIndex !== -1) {
                                            vm.prizedIndex -= 1;
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

    vm.setFocusWinner = function (focusWinner) {
        switch (focusWinner) {
            case 1:
                vm.winner3.IsFocus = true;
                vm.winner1.IsFocus = false;
                vm.winner2.IsFocus = false;
                $scope.$apply();
                break;
            case 2:
                vm.winner2.IsFocus = true;
                vm.winner1.IsFocus = false;
                vm.winner3.IsFocus = false;
                $scope.$apply();
                break;
            case 3:
                vm.winner1.IsFocus = true;
                vm.winner2.IsFocus = false;
                vm.winner3.IsFocus = false;
                $scope.$apply();
                break;
        }
    };

    vm.sortPrize = function () {
        ServerModel.getSuperGrandWinnerDetail(vm.prizeType, vm.startPrize).then(function () {
            vm.winnerDetail = ServerModel.winnerDetail;

            if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                angular.forEach(vm.winnerDetail.Winners, function (winner, index) {
                    var prizeIndex = winner.Prize.PrizeId < 0 ? -1 : winner.Prize.PrizeIndex;

                    vm.fillWinner(winner, index + 1, prizeIndex);
                });
            }
        });
    };

    vm.fillWinner = function (winner, position, prizeIndex) {
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

    vm.choosePrize = function (prize) {
        var prizeId = vm.winnerDetail.Prizes[(prize - 1) % 3].PrizeId,
            index = vm.winnerDetail.Winners.map(function (e) { return e.Prize.PrizeId; }).indexOf(prizeId);

        if (vm.savedWinners.length >= 3
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

                                winners.push(vm.winner3)
                                ServerModel.updateWinner(vm, winners);
                                vm.prizedIndex -= 1;
                            }
                            break;
                        case 2:
                            if (vm.winner2) {
                                vm.winner2.PrizeNumber = baseUrl + ServerModel.prizeNumber;
                                vm.winner2.Prize = vm.winnerDetail.Prizes[(prize - 1) % 3];

                                winners.push(vm.winner2)
                                ServerModel.updateWinner(vm, winners);
                                vm.prizedIndex -= 1;
                            }
                            break;
                        case 3:
                            if (vm.winner1) {
                                vm.winner1.PrizeNumber = baseUrl + ServerModel.prizeNumber;
                                vm.winner1.Prize = vm.winnerDetail.Prizes[(prize - 1) % 3];

                                winners.push(vm.winner1)
                                ServerModel.updateWinner(vm, winners);
                                vm.prizedIndex -= 1;
                            }
                            break;
                    }
                }
            });
        }
    };

    vm.removeSpecialWinner = function () {
        if (vm.focusWinner > 0 && vm.focusWinner < 4
            && vm.prizedWinners.length > 0 && vm.savedWinners.length > 0) {
            var staff;

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

            ServerModel.openPopupConfirmRemoveSpecialWinner(vm, staff, vm.focusWinner);
        }
    };

    vm.removeAllWinner = function () {
        if (vm.savedWinners.length > 0) {
            ServerModel.openPopupConfirmRemoveAllWinner(vm);
        }
    };

    vm.resetPrizeWinners = function () {
        if (vm.savedWinners.length > 0) {
            ServerModel.openPopupConfirmResetPrize(vm);
        }
    };

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

serverModule.directive('superGrandWinnerDetailKeyboardEvent',
['$window', 'baseUrl', function ($window, baseUrl) {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();
                var path = '';

                switch (event.which) {
                    case 13: // enter
                    case 27: // asc
                        path = baseUrl + 'Server/PrizeList/' + scope.winnerDetailCtrl.prizeType + '?isOpeningPrize';
                        break;
                    case 38: // up arrow
                        if (scope.winnerDetailCtrl.focusWinner < 3 && scope.winnerDetailCtrl.savedWinners.length >= 3) {
                            scope.winnerDetailCtrl.focusWinner += 1;

                            scope.winnerDetailCtrl.setFocusWinner(scope.winnerDetailCtrl.focusWinner);
                        }
                        break;
                    case 40: // down arrow
                        if (scope.winnerDetailCtrl.focusWinner > 1 && scope.winnerDetailCtrl.savedWinners.length >= 3) {
                            scope.winnerDetailCtrl.focusWinner -= 1;
                            scope.winnerDetailCtrl.setFocusWinner(scope.winnerDetailCtrl.focusWinner);
                        }
                        break;
                    case 46: // delete
                        scope.winnerDetailCtrl.removeSpecialWinner();
                        break;
                    case 49: // 1
                        scope.winnerDetailCtrl.choosePrize(1);
                        break;
                    case 50: // 2
                        scope.winnerDetailCtrl.choosePrize(2);
                        break;
                    case 51: // 3
                        scope.winnerDetailCtrl.choosePrize(3);
                        break;
                    case 52: // 4
                        scope.winnerDetailCtrl.choosePrize(4);
                        break;
                    case 53: // 5
                        scope.winnerDetailCtrl.choosePrize(5);
                        break;
                    case 54: // 6
                        scope.winnerDetailCtrl.choosePrize(6);
                        break;
                    case 55: // 7
                        scope.winnerDetailCtrl.choosePrize(7);
                        break;
                    case 56: // 8
                        scope.winnerDetailCtrl.choosePrize(8);
                        break;
                    case 57: // 9
                        scope.winnerDetailCtrl.choosePrize(9);
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

                if (path.length > 0) {
                    $window.location.href = path;
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