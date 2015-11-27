'use strict';
/*global commonModule*/

commonModule.factory('CommonModel',
['commonFactory', 'errorFactory',
function (commonFactory, errorFactory) {
    var commonModel = {};

    commonModel.grade = {
        pink: 10,
        green: 11,
        blue: 12
    };
    commonModel.priceTypes = {
        superGrand: 1,
        grand: 2,
        major: 3,
        cash: 4
    };

    commonModel.staff = {};

    commonModel.getStaffByCode = function (staffcode) {
        return commonFactory.getStaffByCode(staffcode)
        .success(function (staff) {
            commonModel.staff = staff;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    return commonModel;
} ]);