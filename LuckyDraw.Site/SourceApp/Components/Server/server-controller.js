'use strict';
/*global serverModule*/

serverModule.controller('ServerController',
['CommonModel', function (CommonModel) {
    var vm = this;

    vm.blueStaff = null;
    vm.greenStaff = null;
    vm.pinkStaff = null;

    var hubProxy = $.connection.hubs;

    $.connection.hub.start().done(function () {
        console.log('Now connected, connection ID =' + $.connection.hub.id);
    });
} ]);

serverModule.directive('serverKeyboardEvent',
['$window', 'baseUrl', function ($window, baseUrl) {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown keypress', function (event) {
                var path = '';

                switch (event.keyCode) {
                    case 49: // 1
                    case 97: // numpad 1
                        path = baseUrl + 'Server/SuperGrand/Index';
                        break;
                    case 50: // 2
                    case 98: // numpad 2
                        path = baseUrl + 'Server/Grand/Index';
                        break;
                    case 51: // 3
                    case 99: // numpad 3
                        path = baseUrl + 'Server/Major/Index';
                        break;
                    case 52: // 4
                    case 100: // numpad 4
                        path = baseUrl + 'Server/Cash/Index';
                        break;
                }

                if (path.length > 0) {
                    $window.location.href = path;
                }
            });
        }
    };
} ]);