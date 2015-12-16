'use strict';
/*global clientModule*/

clientModule.factory('AdvancedModel',
['$modal', 'commonFactory', 'notificationFactory', 'errorFactory',
function ($modal, commonFactory, notificationFactory, errorFactory) {
    var advancedModel = {};

    advancedModel.prizeList = [];

    serverModel.openPopupSecure = function (vm) {
        var modalInstance = $modal.open({
            templateUrl: 'SourceApp/Components/Client/Templates/popupSecure.html',
            controller: "SecureController as secureCtrl",
            backdrop: 'static',
            windowClass: "modal-confirm-center"
        });

        modalInstance.result.then(function (passcode) {
            serverFactory.getStaff(passcode)
            .success(function (response) {
                if (response.IsSuccessful) {
                    
                } else {
                    notificationFactory.error("", response.Message);
                }
            }).error(function (response, status) {
                errorFactory.errorHandle(status);
            });
        })['finally'](function () {
            modalInstance = '';
        });
    };

    return serverModel;
} ]);