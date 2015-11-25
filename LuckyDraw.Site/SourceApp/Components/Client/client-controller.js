'use strict';
/*global clientModule*/

clientModule.controller('ClientController',
['CommonModel', function (CommonModel) {
    var vm = this;

    var hubProxy = $.connection.hubs;

    vm.staffCode = '';

    CommonModel.getStaffByCode('11402249').then(function () {
        if (CommonModel.staff) {
            vm.staff = CommonModel.staff;
        }
    });

    vm.getStaffByCode = function (staffCode) {
        CommonModel.getStaffByCode(staffCode).then(function () {
            vm.staffCode = '';

            if (CommonModel.staff) {
                vm.staff = CommonModel.staff;

                hubProxy.server.sendStaffCode(staffCode);
            }
        });
    };

    $.connection.hub.start().done(function () {
        console.log('Now connected, connection ID =' + $.connection.hub.id);
    });
} ]);

clientModule.directive('keyboardEvent', function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown keypress', function (event) {
                var charCode = '';

                if (event.keyCode >= 48 && event.keyCode <= 57) {
                    charCode = String.fromCharCode(event.keyCode);
                } else if (event.keyCode >= 96 && event.keyCode <= 105) {
                    charCode = String.fromCharCode(event.keyCode - 48);
                }
                
                if (scope.clientCtrl.staffCode.length < 8 && charCode.length === 1) {
                    scope.clientCtrl.staffCode += charCode;

                    if (scope.clientCtrl.staffCode.length === 8) {
                        scope.clientCtrl.getStaffByCode(scope.clientCtrl.staffCode);
                    }
                }

                event.preventDefault();
            });
        }
    };
});