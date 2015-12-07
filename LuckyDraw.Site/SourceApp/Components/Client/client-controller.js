'use strict';
/*global clientModule*/

clientModule.controller('ClientController',
['$scope', '$location', '$routeParams', 'CommonModel', 'baseUrl',
function ($scope, $location, $routeParams, CommonModel, baseUrl) {
    var vm = this;

    vm.grade = parseInt($routeParams.grade);
    vm.message = null;
    vm.staffCode = '';

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

        CommonModel.getStaffByCode('11402249').then(function () {
            if (CommonModel.staff) {
                vm.staff = CommonModel.staff;
            }
        });
    };

    vm.initialize();

    vm.getClient = function () {
        var client = 'default-client';

        if (vm.grade) {
            switch (vm.grade) {
                case 10:
                    client = 'pink-client';
                    break;
                case 11:
                    client = 'green-client';
                    break;
                case 12:
                    client = 'blue-client';
                    break;
            }
        }

        return client;
    };

    vm.checkStaffCodeByGrade = function (staffCode) {
        var isValid = false;

        if (!vm.grade) {
            isValid = true;
        } else if (parseInt(staffCode.substring(0, 2)) === vm.grade) {
            isValid = true;
        }

        return isValid;
    };

    var hubProxy = $.connection.hubs;

    vm.getStaffByCode = function (staffCode) {
        if (vm.checkStaffCodeByGrade(staffCode)) {
            CommonModel.getStaffByCode(staffCode).then(function () {
                vm.staffCode = '';

                if (CommonModel.staff) {
                    vm.staff = CommonModel.staff;

                    hubProxy.server.sendStaffCode(staffCode);
                }
            });
        } else {
            vm.staffCode = '';
        }
    };

    hubProxy.client.getResponse = function (grade, message) {
        if (grade === vm.grade) {
            vm.message = message;
            $scope.$apply();
        }
    };

    $.connection.hub.start();
} ]);