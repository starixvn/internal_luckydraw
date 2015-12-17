'use strict';
/*global commonModule*/

commonModule.factory('CommonModel',
['commonFactory', 'errorFactory',
function (commonFactory, errorFactory) {
    var commonModel = {};

    commonModel.staffs = [];
    commonModel.staff = null;
    commonModel.prizeSetting = null;
    commonModel.prizes = [];
    commonModel.grades = [];
    commonModel.prizeTypes = [];

    commonModel.getStaff = function (query) {
        return commonFactory.getStaff(query)
        .success(function (staffs) {
            commonModel.staffs = staffs;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    commonModel.getStaffByCode = function (staffCode) {
        return commonFactory.getStaffByCode(staffCode)
        .success(function (staff) {
            commonModel.staff = staff;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    commonModel.getPrizeSettingByPrizeType = function (prizeTypeId) {
        return commonFactory.getPrizeSettingByPrizeType(prizeTypeId)
        .success(function (prizeSetting) {
            commonModel.prizeSetting = prizeSetting;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    commonModel.getPrizeByPrizeType = function (prizeTypeId) {
        return commonFactory.getPrizeByPrizeType(prizeTypeId)
        .success(function (prizes) {
            commonModel.prizes = prizes;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    commonModel.getGrade = function () {
        return commonFactory.getGrade()
        .success(function (grades) {
            commonModel.grades = grades;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    commonModel.getPrizeType = function () {
        return commonFactory.getPrizeType()
        .success(function (prizeTypes) {
            commonModel.prizeTypes = prizeTypes;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    return commonModel;
} ]);