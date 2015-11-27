'use strict';
/*global serverModule*/

serverModule.controller('GrandController',
['CommonModel', function (CommonModel) {
    var vm = this;

    var hubProxy = $.connection.hubs;

    $.connection.hub.start().done(function () {
        console.log('Now connected, connection ID =' + $.connection.hub.id);
    });
} ]);

serverModule.directive('serverGrandKeyboardEvent',
['$window', 'baseUrl', function ($window, baseUrl) {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown keypress', function (event) {
                var path = '';

                switch (event.keyCode) {
                    case 13: //enter
                        path = baseUrl + 'Server/WinnerDetails/Index';
                        break;
                    case 27: // asc
                        path = baseUrl + 'Server/Index';
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });
        }
    };
} ]);