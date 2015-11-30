'use strict';
/*global prizeModule*/

prizeModule.factory('prizeFactory', ['$http', 'baseUrl', function ($http, baseUrl) {
    return {
        renderListPrize: function (prizeType) {
            return $http.get(baseUrl + 'api/Prize/RenderListPrize', { params: { prizeType: prizeType } });
        }
    };
} ]);