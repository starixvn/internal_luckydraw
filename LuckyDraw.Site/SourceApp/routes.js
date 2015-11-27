'use strict';
/*exported commonModule, serverModule, clientModule*/

var commonModule = angular.module("commonModule", ['ngRoute']);

var serverModule = angular.module("serverModule", ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when(baseUrl + 'Server/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/index.html' });
    $routeProvider.when(baseUrl + 'Server/SuperGrand/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/superGrand.html' });
    $routeProvider.when(baseUrl + 'Server/Grand/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/grand.html' });
    $routeProvider.when(baseUrl + 'Server/Major/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/major.html' });
    $routeProvider.when(baseUrl + 'Server/Cash/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/cash.html' });
    $routeProvider.when(baseUrl + 'Server/WinnerDetails/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/winnerDetails.html' });
} ]);

var clientModule = angular.module("clientModule", ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when(baseUrl + 'Client/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Client/Templates/index.html' });
    $routeProvider.when(baseUrl + 'Client/Index/:grade',
    { templateUrl: baseUrl + 'SourceApp/Components/Client/Templates/index.html' });
} ]);