'use strict';
/*global clientModule*/

clientModule.directive('clientKeyboardEvent', function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown keypress', function (event) {
                var charCode = '';

                if (event.keyCode >= 48 && event.keyCode <= 57) {
                    charCode = String.fromCharCode(event.keyCode);
                }

                if (scope.clientCtrl.staffCode.length < 8 && charCode.length === 1) {
                    scope.clientCtrl.staffCode += charCode;

                    if (scope.clientCtrl.staffCode.length === 8) {
                        scope.clientCtrl.getStaffByCode(scope.clientCtrl.staffCode);
                    }
                }

                event.preventDefault();
            });
        }
    };
});