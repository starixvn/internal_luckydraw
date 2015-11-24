'use strict';
/*global dashboardModule*/

dashboardModule.controller('DashboardController',
['DashboardModel', function (DashboardModel) {
    var vm = this;

    vm.staffCode = '';

    DashboardModel.getStaffByCode('11402249').then(function () {
        vm.staff = DashboardModel.staff;
    });

    vm.getStaffByCode = function (staffCode) {
        DashboardModel.getStaffByCode(staffCode).then(function () {
            vm.staff = DashboardModel.staff;
        });
    };
} ]);

dashboardModule.directive('customEvent', function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown keypress', function (event) {
                var charCode = '';

                if (event.keyCode >= 48 && event.keyCode <= 57) {
                    charCode = String.fromCharCode(event.keyCode);
                } else if (event.keyCode >= 96 && event.keyCode <= 105) {
                    charCode = String.fromCharCode(event.keyCode - 48);
                }
                
                if (scope.dashboardCtrl.staffCode.length < 8 && charCode.length === 1) {
                    scope.dashboardCtrl.staffCode += charCode;

                    if (scope.dashboardCtrl.staffCode.length === 8) {
                        scope.dashboardCtrl.getStaffByCode(scope.dashboardCtrl.staffCode);
                    }
                }

                event.preventDefault();
            });
        }
    };
});