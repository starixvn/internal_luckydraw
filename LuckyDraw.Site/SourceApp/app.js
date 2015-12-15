'use strict';
/*exported appModule*/

var appModule = angular.module('appModule', ['commonModule', 'prizeModule', 'dashboardModule', 'serverModule', 'clientModule', 'prizeModule',
'pascalprecht.translate', 'ui.bootstrap', 'ngRoute'])
.config(['$translateProvider', '$locationProvider', '$routeProvider', '$provide',
function ($translateProvider, $locationProvider, $routeProvider, $provide) {
    $translateProvider.useLoader('resourceFactory', {});
    $routeProvider.when('/Home/AccessDenied', { templateUrl: 'SourceApp/Shared/Templates/accessDenied.html' });
    $translateProvider.preferredLanguage('en');
    $locationProvider.html5Mode(true);
    var baseUrl = $('base').first().attr('href');
    $provide.value('baseUrl', baseUrl);
} ]);