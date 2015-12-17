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

clientModule.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});

clientModule.directive('searchAutoComplete', ['$timeout', 'errorFactory', function ($timeout, errorFactory) {
    return {
        scope: {
            staff: "=",
            searchStaffs: "&",
            sendStaffCode: "&"
        },
        link: function (scope, iElement) {
            iElement.autocomplete({
                source: function (request, response) {
                    scope.searchStaffs().success(function (staffs) {
                        var data = [];

                        angular.forEach(staffs, function (staff) {
                            data.push(
                            {
                                value: staff.FullName + ' (' + staff.NickName + ')',
                                id: staff.StaffId,
                                staff: staff
                            });
                        });

                        response(data);
                    }).error(function (response, status) {
                        errorFactory.errorHandle(status);
                    });
                },
                select: function (event, ui) {
                    scope.staff = ui.item.staff;
                    scope.$apply();

                    scope.sendStaffCode();
                },
                appendTo: iElement.parent(),
                delay: 500,
                minLength: 2
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                var el = '<div>Nick Name: ' + item.staff.NickName + '</div>'
                        + '<div>Full Name: ' + item.staff.FullName + '</div>'
                        + '<div>Code: ' + item.staff.BarCode + '</div>'
                        + '<div>Department: ' + item.staff.Department.Name + '</div>';

                return $("<li>").append(el).appendTo(ul);
            };
        }
    };
} ]);