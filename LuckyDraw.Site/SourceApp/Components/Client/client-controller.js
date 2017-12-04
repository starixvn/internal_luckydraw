'use strict';
/*global clientModule*/

clientModule.controller('ClientController',
['$scope', '$window', '$location', '$routeParams', 'CommonModel', 'baseUrl',
function ($scope, $window, $location, $routeParams, CommonModel, baseUrl) {
    var vm = this;

    vm.grade = parseInt($routeParams.grade);
    vm.message = null;
    vm.staff = null;
    vm.staffCode = '';
    vm.baseUrl = baseUrl;

    vm.initialize = function () {
        CommonModel.getGrade().then(function () {
            vm.grades = CommonModel.grades;

            if ($routeParams.grade && vm.grade) {
                var index = vm.grades.map(function (e) { return e.GradeId; }).indexOf(vm.grade);

                if (index === -1) {
                    $location.path(baseUrl + 'Home/AccessDenied');
                }
            }
        });
    };

    vm.initialize();

    vm.getCssClass = function () {
        var result = '';

        if (vm.grade) {
            switch (vm.grade) {
                case 10:
                    result = 'yellow-staff';
                    break;
                case 11:
                    result = 'green-staff';
                    break;
                case 12:
                    result = 'blue-staff';
                    break;
            }
        }

        return result;
    };

    vm.checkStaffCodeByGrade = function (staffCode) {
        var isValid = false;

        if (!vm.grade) {
            isValid = true;
        } else {
            if (parseInt(staffCode.substring(0, 2)) === vm.grade) {
                isValid = true;
            } else {
                isValid = false;
                vm.message = 'Wrong group.';
                $scope.$apply();
            }
        }

        return isValid;
    };

    var hubProxy = $.connection.hubs;

    vm.getStaffByCode = function (staffCode) {
        if (vm.checkStaffCodeByGrade(staffCode)) {
            CommonModel.getStaffByCode(staffCode).then(function () {
                vm.staffCode = '';

                if (CommonModel.staff) {
                    vm.message = null;

                    vm.staff = CommonModel.staff;
                    hubProxy.server.sendStaffCode(staffCode);
                }
            });
        } else {
            vm.staffCode = '';
        }
    };

    hubProxy.client.getMessageResponse = function (grade, message) {
        if (vm.grade && grade === vm.grade) {
            vm.message = message;
            $scope.$apply();
        } else if (!vm.grade) {
            vm.message = message;
            $scope.$apply();
        }
    };

    hubProxy.client.getSignalAtClientFromServer = function () {
        hubProxy.server.sendSignalFromClientToServer(vm.grade);
    };

    $.connection.hub.start();
} ]);