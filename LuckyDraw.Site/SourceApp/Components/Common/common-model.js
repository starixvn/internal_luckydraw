'use strict';
/*global commonModule*/

commonModule.factory('CommonModel',
['commonFactory', 'errorFactory',
function (commonFactory, errorFactory) {
    var commonModel = {};

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