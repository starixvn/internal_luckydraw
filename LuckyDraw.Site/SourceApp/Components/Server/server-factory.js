'use strict';
/*global serverModule*/

serverModule.factory('serverFactory', ['$http', 'baseUrl', function ($http, baseUrl) {
    return {
        renderListPrize: function (prizeType) {
            return $http.get(baseUrl + 'api/Server/RenderListPrize', { params: { prizeType: prizeType } });
        },
        getPrizeDetail: function (prizeType, currentPrize) {
            return $http.get(baseUrl + 'api/Server/GetPrizeDetail', {
                params: {
                    prizeType: prizeType,
                    currentPrize: currentPrize
                }
            });
        },
        getSuperGrandPrizeByPrizeType: function (prizeType, currentPrize) {
            return $http.get(baseUrl + 'api/Server/GetSuperGrandPrize', {
                params: {
                    prizeType: prizeType,
                    currentPrize: currentPrize
                }
            });
        },
        updateCurrentPrize: function (criteria) {
            return $http.put(baseUrl + 'api/Server/UpdateCurrentPrize', criteria);
        }
    };
} ]);