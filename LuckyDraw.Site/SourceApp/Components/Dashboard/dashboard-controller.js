'use strict';
/*global dashboardModule*/

dashboardModule.controller('DashboardController',
['$window', 'baseUrl',
function ($window, baseUrl) {
    var vm = this;

    vm.baseUrl = baseUrl;

    vm.redirectToServer = function () {
        $window.location.href = baseUrl + 'Server/Index';
    };

    vm.redirectToDefaultClient = function () {
        $window.location.href = baseUrl + 'Client/Index';
    };

    vm.redirectToPinkClient = function () {
        $window.location.href = baseUrl + 'Client/Index/10';
    };

    vm.redirectToGreenClient = function () {
        $window.location.href = baseUrl + 'Client/Index/11';
    };

    vm.redirectToBlueClient = function () {
        $window.location.href = baseUrl + 'Client/Index/12';
    };
} ]);

dashboardModule.directive('dashboardKeyboardEvent', function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();
            });
        }
    };
});