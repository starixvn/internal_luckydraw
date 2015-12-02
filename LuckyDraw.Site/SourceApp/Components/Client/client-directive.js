'use strict';
/*global clientModule*/

clientModule.directive('clientKeyboardEvent', function () {
    return {
        link: function (scope, element, attrs) {
            jQuery(document).on('keydown', function (event) {
                var charCode = '';

                if (event.which >= 48 && event.which <= 57) {
                    charCode = String.fromCharCode(event.which);
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