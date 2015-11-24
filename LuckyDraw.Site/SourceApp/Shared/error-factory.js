/*global appModule*/
'use strict';

appModule.factory("errorFactory",
['$location', '$window', 'notificationFactory', 'baseUrl',
function ($location, $window, notificationFactory, baseUrl) {
    return {
        errorHandle: function (status) {
            notificationFactory.error("ErrorMessage");
        }
    };
} ]);