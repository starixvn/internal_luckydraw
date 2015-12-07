'use strict';
/*global serverModule*/

serverModule.factory('serverFactory', ['$http', 'baseUrl', function ($http, baseUrl) {
    return {
        renderListPrize: function (prizeType) {
            return $http.get(baseUrl + 'api/Server/RenderListPrize', { params: { prizeType: prizeType} });
        },
        getWinnerDetail: function (prizeType, currentPrize) {
            return $http.get(baseUrl + 'api/Server/GetWinnerDetail', {
                params: {
                    prizeType: prizeType,
                    currentPrize: currentPrize
                }
            });
        },
        getSuperGrandWinnerDetail: function (prizeType, currentPrize) {
            return $http.get(baseUrl + 'api/Server/GetSuperGrandWinnerDetail', {
                params: {
                    prizeType: prizeType,
                    currentPrize: currentPrize
                }
            });
        },
        getPrizeNumber: function (prizeIndex) {
            return $http.get(baseUrl + 'api/Server/GetPrizeNumber', { params: { prizeIndex: prizeIndex} });
        },
        updateCurrentPrize: function (criteria) {
            return $http.put(baseUrl + 'api/Server/UpdateCurrentPrize', criteria);
        },
        insertWinner: function (winners) {
            return $http.post(baseUrl + 'api/Server/InsertWinner', winners);
        },
        updateWinner: function (criteria) {
            return $http.put(baseUrl + 'api/Server/UpdateWinner', criteria);
        },
        deleteWinner: function (staffIds, prizeTypeId, currentPrize) {
            return $http['delete'](baseUrl + 'api/Server/DeleteWinner', {
                params: {
                    staffIds: staffIds,
                    prizeTypeId: prizeTypeId,
                    currentPrize: currentPrize
                }
            });
        }
    };
} ]);