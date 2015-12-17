'use strict';
/*global clientModule*/

clientModule.factory('AdvancedModel',
['$modal', 'commonFactory', 'notificationFactory', 'errorFactory',
function ($modal, commonFactory, notificationFactory, errorFactory) {
    var advancedModel = {};

    advancedModel.password = null;
    advancedModel.staffs = [];

    advancedModel.openPopupSecure = function (vm) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/Client/Templates/popupSecure.html',
            controller: "SecureController as secureCtrl",
            backdrop: 'static',
            size: 'sm',
            windowClass: "modal-secure"
        });

        modalInstance.result.then(function (password) {
            commonFactory.getPassword()
            .success(function (defaultPassword) {
                if (password === defaultPassword) {
                    vm.unlockAdvancedZone = true;
                    modalInstance = '';
                }
            }).error(function (response, status) {
                errorFactory.errorHandle(status);
            });
        });
    };

    advancedModel.getPassword = function () {
        return commonFactory.getPassword()
        .success(function (password) {
            advancedModel.password = password;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    return advancedModel;
} ]);