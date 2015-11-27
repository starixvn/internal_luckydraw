'use strict';
/*global clientModule*/

clientModule.controller('ClientController',
['$location', '$routeParams', 'CommonModel', 'baseUrl',
function ($location, $routeParams, CommonModel, baseUrl) {
    var vm = this;

    vm.grade = parseInt($routeParams.grade);

    if ($routeParams.grade && vm.grade) {
        switch (vm.grade) {
            case CommonModel.grade.pink:
            case CommonModel.grade.green:
            case CommonModel.grade.blue:
                break;
            default:
                $location.path(baseUrl + 'Home/AccessDenied');
        }
    }

    vm.staffCode = '';

    var hubProxy = $.connection.hubs;

    CommonModel.getStaffByCode('11402249').then(function () {
        if (CommonModel.staff) {
            vm.staff = CommonModel.staff;
        }
    });

    vm.getClient = function () {
        var client = 'default-client';

        if (vm.grade) {
            switch (vm.grade) {
                case CommonModel.grade.pink:
                    client = 'pink-client';
                    break;
                case CommonModel.grade.green:
                    client = 'green-client';
                    break;
                case CommonModel.grade.blue:
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

    $.connection.hub.start().done(function () {
        console.log('Now connected, connection ID =' + $.connection.hub.id);
    });
} ]);