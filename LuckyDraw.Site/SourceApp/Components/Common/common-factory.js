'use strict';
/*global commonModule*/

commonModule.factory('commonFactory', ['$http', 'baseUrl', function ($http, baseUrl) {
    return {
        getStaffByCode: function (staffCode) {
            return $http.get(baseUrl + 'api/Common/GetStaffByCode', { params: { staffCode: staffCode} });
        }
    };
} ]);