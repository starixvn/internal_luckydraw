'use strict';
/*global dashboardModule*/

dashboardModule.factory('DashboardModel',
['dashboardFactory', 'errorFactory',
function (dashboardFactory, errorFactory) {
    var dashboardModel = {};

    dashboardModel.staff = {};

    dashboardModel.getStaffByCode = function (code) {
        return dashboardFactory.getStaffByCode(code)
        .success(function (staff) {
            dashboardModel.staff = staff;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    return dashboardModel;
} ]);