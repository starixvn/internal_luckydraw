'use strict';
/*global serverModule*/

serverModule.controller('WinnerDetailsController',
['CommonModel', 'baseUrl',
function (CommonModel, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;
    vm.blueStaff = null;
    vm.greenStaff = null;
    vm.pinkStaff = null;

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