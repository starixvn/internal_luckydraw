'use strict';
/*exported commonModule, prizeModule, serverModule, clientModule*/

var commonModule = angular.module("commonModule", ['ngRoute']);

var prizeModule = angular.module("prizeModule", ['ngRoute']);

var serverModule = angular.module("serverModule", ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when(baseUrl + 'Server/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/index.html' });
    $routeProvider.when(baseUrl + 'Server/PrizeList/:prizeType',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/prizeList.html' });
    $routeProvider.when(baseUrl + 'Server/WinnerDetail/:prizeType',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/winnerDetail.html' });
    $routeProvider.when(baseUrl + 'Server/SuperGrandWinnerDetail/:prizeType',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/superGrandWinnerDetail.html' });
} ]);

var clientModule = angular.module("clientModule", ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $("base").first().attr("href");
    $routeProvider.when(baseUrl + 'Client/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Client/Templates/index.html' });
    $routeProvider.when(baseUrl + 'Client/Index/:grade',
    { templateUrl: baseUrl + 'SourceApp/Components/Client/Templates/index.html' });
} ]);