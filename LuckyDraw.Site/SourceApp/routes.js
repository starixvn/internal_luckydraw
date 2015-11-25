'use strict';
/*exported commonModule, serverModule, clientModule*/

var commonModule = angular.module("commonModule", ['ngRoute']);

var serverModule = angular.module("serverModule", ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when('/Server/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/index.html' });
} ]);

var clientModule = angular.module("clientModule", ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when('/',
    { templateUrl: baseUrl + 'SourceApp/Components/Client/Templates/index.html' });
} ]);