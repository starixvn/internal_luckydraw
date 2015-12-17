'use strict';
/*global commonModule*/

commonModule.factory('commonFactory', ['$http', 'baseUrl', function ($http, baseUrl) {
    return {
        getStaff: function (query) {
            return $http.get(baseUrl + 'api/Common/GetStaff', { params: { query: query} });
        },
        getStaffByCode: function (staffCode) {
            return $http.get(baseUrl + 'api/Common/GetStaffByCode', { params: { staffCode: staffCode} });
        },
        getPrizeSettingByPrizeType: function (prizeTypeId) {
            return $http.get(baseUrl + 'api/Common/GetPrizeSettingByPrizeType', { params: { prizeTypeId: prizeTypeId} });
        },
        getPrizeByPrizeType: function (prizeTypeId) {
            return $http.get(baseUrl + 'api/Common/GetPrizeByPrizeType', { params: { prizeTypeId: prizeTypeId} });
        },
        getGrade: function () {
            return $http.get(baseUrl + 'api/Common/GetGrade');
        },
        getPrizeType: function () {
            return $http.get(baseUrl + 'api/Common/GetPrizeType');
        },
        getPassword: function () {
            return $http.get(baseUrl + 'api/Common/GetPassword');
        }
    };
} ]);