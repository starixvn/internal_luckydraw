'use strict';
/*global serverModule*/

serverModule.controller('AdhocPrizeController',
['$window', 'CommonModel', 'baseUrl',
function ($window, CommonModel, baseUrl) {
    var vm = this;

    var hubProxy = $.connection.hubs;

    $.connection.hub.start();
} ]);