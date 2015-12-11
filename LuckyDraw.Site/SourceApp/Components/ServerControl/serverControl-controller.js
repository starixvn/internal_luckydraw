'use strict';
/*global controlServerModule*/

controlServerModule.controller('ServerControlController',
[
function () {
    var vm = this;

    vm.enter = function () {
        hubProxy.server.sendSignalEnter();
    }

    vm.esc = function () {
        hubProxy.server.sendSignalEsc();
    }

    vm.number = function (number) {
        hubProxy.server.sendSignalNumber(number);
    }

    vm.ctrlZ = function () {
        hubProxy.server.sendSignalCtrlZ();
    }

    vm.ctrlY = function () {
        hubProxy.server.sendSignalCtrlY();
    }

    vm.ctrlD = function () {
        hubProxy.server.sendSignalCtrlD();
    }

    vm.ctrlE = function () {
        hubProxy.server.sendSignalCtrlE();
    }

    vm.moveUp = function () {
        hubProxy.server.sendSignalMoveUp();
    }

    vm.moveDown = function () {
        hubProxy.server.sendSignalMoveDown();
    }

    vm.deleteWinner = function () {
        hubProxy.server.sendSignalDelete();
    }

    vm.sort = function () {
        hubProxy.server.sendSignalCharacterS();
    }

    var hubProxy = $.connection.hubs;

    $.connection.hub.start();
} ]);