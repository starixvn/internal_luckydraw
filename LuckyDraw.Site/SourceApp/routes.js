'use strict';
/*exported commonModule, prizeModule, dashboardModule, serverModule, clientModule*/

var commonModule = angular.module('commonModule', ['ngRoute']);

var prizeModule = angular.module('prizeModule', ['ngRoute']);

var dashboardModule = angular.module('dashboardModule', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $('base').first().attr('href');

    $routeProvider.when('/',
    { templateUrl: baseUrl + 'SourceApp/Components/Dashboard/Templates/index.html' });
} ]);

var serverModule = angular.module('serverModule', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $('base').first().attr('href');

    $routeProvider.when(baseUrl + 'Server/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/index.html', caseInsensitiveMatch: true });
    $routeProvider.when(baseUrl + 'Server/PrizeList/:prizeType',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/prizeList.html', caseInsensitiveMatch: true });
    $routeProvider.when(baseUrl + 'Server/WinnerDetail/:prizeType',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/winnerDetail.html', caseInsensitiveMatch: true });
    $routeProvider.when(baseUrl + 'Server/SuperGrandWinnerDetail/:prizeType',
    { templateUrl: baseUrl + 'SourceApp/Components/Server/Templates/superGrandWinnerDetail.html', caseInsensitiveMatch: true });
} ]);

var clientModule = angular.module('clientModule', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    var baseUrl = $('base').first().attr('href');

    $routeProvider.when(baseUrl + 'Client/Index',
    { templateUrl: baseUrl + 'SourceApp/Components/Client/Templates/index.html', caseInsensitiveMatch: true });
    $routeProvider.when(baseUrl + 'Client/Index/:grade',
    { templateUrl: baseUrl + 'SourceApp/Components/Client/Templates/index.html', caseInsensitiveMatch: true });
} ]);
