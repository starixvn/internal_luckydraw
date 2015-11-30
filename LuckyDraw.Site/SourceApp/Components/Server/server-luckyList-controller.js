'use strict';
/*global serverModule*/

serverModule.controller('LuckyListController',
['$routeParams', 'CommonModel',
function ($routeParams, CommonModel) {
    var vm = this;

    vm.priceType = parseInt($routeParams.priceType);

    vm.initialize = function () {
        CommonModel.getPrizeType().then(function () {
            vm.prizeTypes = CommonModel.prizeTypes;
        });
    };

    vm.initialize();
} ]);