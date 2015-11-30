'use strict';
/*global prizeModule*/

prizeModule.factory('PrizeModel',
['prizeFactory', 'errorFactory',
function (prizeFactory, errorFactory) {
    var prizeModel = {};

    prizeModel.listPrize = [];

    prizeModel.renderListPrize = function (prizeType) {
        return prizeFactory.renderListPrize(prizeType)
        .success(function (listPrize) {
            prizeModel.listPrize = listPrize;
        }).error(function (response, status) {
            errorFactory.errorHandle(status);
        });
    };

    return prizeModel;
} ]);