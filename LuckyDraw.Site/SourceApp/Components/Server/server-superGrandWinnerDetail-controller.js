'use strict';
/*global serverModule*/

serverModule.controller('SuperGrandWinnerDetailController',
['$routeParams', 'ServerModel', 'CommonModel', 'baseUrl',
function ($routeParams, ServerModel, CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.prizeType = parseInt($routeParams.prizeType);
    vm.currentPrize = parseInt($routeParams.currentPrize);

    vm.prizeIndex = 3;
    vm.saveWinner = [];

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
                    ServerModel.getSuperGrandWinnerDetail(vm.prizeType, vm.currentPrize).then(function () {
                        vm.winnerDetail = ServerModel.winnerDetail;

                        if (vm.winnerDetail && vm.winnerDetail.Winners.length > 0) {
                            angular.forEach(vm.winnerDetail.Winners, function (winner, index) {
                                vm.fillWinner(winner, index + 1, winner.Prize.PrizeIndex);
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

    vm.fillWinner = function (winner, position, prizeIndex) {
        ServerModel.getPrizeNumber(prizeIndex).then(function () {
            winner.PrizeNumber = baseUrl + ServerModel.prizeNumber;

            switch (position) {
                case 1:
                    vm.winner1 = winner;
                    break;
                case 2:
                    vm.winner2 = winner;
                    break;
                case 3:
                    vm.winner3 = winner;
                    break;
            }
        });
    };

    vm.choosePrize = function (prize) {
        if (saveWinner.length >= 3 && prize > 0 && prize < 10) {
        }
    };

    vm.removeAllWinner = function () {
        if (vm.saveWinner.length > 0) {
            ServerModel.openPopupConfirmRemoveAllWinner(vm);
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
                var path = '';

                switch (event.which) {
                    case 27: // asc
                        path = baseUrl + 'Server/PrizeList/' + scope.winnerDetailCtrl.prizeType + '?isOpeningPrize';
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });

            jQuery(document).on('keypress', function (event) {
                var path = '';

                switch (event.which) {
                    case 4: // press ctrl+d to remove winners
                        break;
                    case 5: // press ctrl+e to close
                        break;
                    case 13: // enter
                        path = baseUrl + 'Server/PrizeList/' + scope.winnerDetailCtrl.prizeType + '?isOpeningPrize';
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
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });
        }
    };
} ]);