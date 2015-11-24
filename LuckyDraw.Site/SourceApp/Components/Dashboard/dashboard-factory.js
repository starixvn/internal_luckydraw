'use strict';
/*global dashboardModule*/

dashboardModule.factory('dashboardFactory', ['$http', 'baseUrl', function ($http, baseUrl) {
    return {
        getStaffByCode: function (code) {
            return $http.get(baseUrl + 'api/Dashboard/GetStaffByCode', { params: { code: code} });
        }
    };
} ]);