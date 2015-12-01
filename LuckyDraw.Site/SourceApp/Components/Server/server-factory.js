'use strict';
/*global serverModule*/

serverModule.factory('serverFactory', ['$http', 'baseUrl', function ($http, baseUrl) {
    return {
        renderListPrize: function (prizeType) {
            return $http.get(baseUrl + 'api/Server/RenderListPrize', { params: { prizeType: prizeType } });
        },
        updateCurrentPrize: function (criteria) {
            return $http.put(baseUrl + 'api/Server/RenderListPrize', criteria);
        }
    };
} ]);