'use strict';
/*exported serverControlModule*/

var serverControlModule = angular.module("serverControlModule", [
'pascalprecht.translate', 'ui.bootstrap', 'ngRoute'])
.config(['$translateProvider', '$locationProvider', '$routeProvider', '$provide',
function ($translateProvider, $locationProvider, $routeProvider, $provide) {
    $routeProvider.when('/Home/AccessDenied', { templateUrl: 'SourceApp/Shared/Templates/accessDenied.html' });
    $translateProvider.preferredLanguage('en');
    $locationProvider.html5Mode(true);
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when(baseUrl + 'ServerControl/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/ServerControl/Templates/index.html' });
    $provide.value("baseUrl", baseUrl);
} ]);
