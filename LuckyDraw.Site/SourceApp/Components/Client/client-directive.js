'use strict';
/*global clientModule*/

clientModule.directive('clientKeyboardEvent', function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                event.preventDefault();

                switch (event.which) {
                    case 27: // asc
                        scope.clientCtrl.redirectToDashboard();
                        break;
                    case 48: // 0
                    case 49: // 1
                    case 50: // 2
                    case 51: // 3
                    case 52: // 4
                    case 53: // 5
                    case 54: // 6
                    case 55: // 7
                    case 56: // 8
                    case 57: // 9
                        var charCode = '';
                        charCode = String.fromCharCode(event.which);

                        if (scope.clientCtrl.staffCode.length < 8 && charCode.length === 1) {
                            scope.clientCtrl.staffCode += charCode;

                            if (scope.clientCtrl.staffCode.length === 8) {
                                scope.clientCtrl.getStaffByCode(scope.clientCtrl.staffCode);
                            }
                        }

                        break;
                }
            });
        }
    };
});