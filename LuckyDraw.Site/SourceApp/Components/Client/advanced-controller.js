'use strict';
/*global clientModule*/

clientModule.controller('AdvancedController',
['$window', '$scope', 'AdvancedModel', 'CommonModel', 'baseUrl',
function ($window, $scope, AdvancedModel, CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.message = null;
    vm.unlockAdvancedZone = false;
    vm.staff = null;
    vm.query = '';

    AdvancedModel.openPopupSecure(vm);

    vm.getCssClass = function (staffCode) {
        var result = 'default-staff';

        if (staffCode) {
            switch (staffCode.substring(0, 2)) {
                case '10':
                    result = 'yellow-staff';
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

    vm.getStaff = function () {
        return CommonModel.getStaff(vm.query);
    };

    var hubProxy = $.connection.hubs;

    vm.sendStaffCode = function () {
        if (vm.staff) {
            vm.message = null;
            hubProxy.server.sendStaffCode(vm.staff.StaffCode);
        }
    };

    hubProxy.client.getMessageResponse = function (grade, message) {
        if (vm.staff && grade === parseInt(vm.staff.StaffCode.substring(0, 2)) && message) {
            vm.message = message;
            $scope.$apply();
        }
    };

    $.connection.hub.start();
} ]);

clientModule.controller('SecureController',
['$modalInstance', 'AdvancedModel',
function ($modalInstance, AdvancedModel) {
    var vm = this;

    vm.password = '';
    vm.error = null;

    AdvancedModel.getPassword().then(function () {
        vm.defaultPassword = AdvancedModel.password;
    });

    vm.ok = function () {
        if (vm.password === vm.defaultPassword) {
            $modalInstance.close(vm.password);
        } else {
            vm.error = 'Wrong password. Please try again!';
        }
    };

    vm.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
} ]);