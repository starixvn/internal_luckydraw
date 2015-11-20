'use strict';
/*exported dashboardModule*/

var dashboardModule = angular.module("dashboardModule", ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when('/',
    { templateUrl: baseUrl + 'SourceApp/Components/Dashboard/Templates/index.html' });
} ]);