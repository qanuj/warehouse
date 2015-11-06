app.directive('loading', [
    '$http', function ($http) {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs) {
                scope.isLoading = function () {
                    return $http.pendingRequests.length > 0;
                };

                scope.$watch(scope.isLoading, function (v) {
                    if (v) {
                        elm.show();
                    } else {
                        elm.hide();
                    }
                });
            }
        };
    }
]);
app.directive('ngSpinnerBar', [
    '$rootScope',
    function ($rootScope) {
        return {
            link: function (scope, element, attrs) {
                // by defult hide the spinner bar
                element.addClass('hide'); // hide spinner bar by default

                // display the spinner bar whenever the route changes(the content part started loading)
                $rootScope.$on('$stateChangeStart', function () {
                    element.removeClass('hide'); // show spinner bar
                });

                // hide the spinner bar on rounte change success(after the content loaded)
                $rootScope.$on('$stateChangeSuccess', function () {
                    element.addClass('hide'); // hide spinner bar
                    $('body').removeClass('page-on-load'); // remove page loading indicator
                    Layout.setSidebarMenuActiveLink('match'); // activate selected link in the sidebar menu

                    // auto scorll to page top
                    setTimeout(function () {
                        Metronic.scrollTop(); // scroll to the top on content load
                    }, $rootScope.settings.layout.pageAutoScrollOnLoad);
                });

                // handle errors
                $rootScope.$on('$stateNotFound', function () {
                    element.addClass('hide'); // hide spinner bar
                });

                // handle errors
                $rootScope.$on('$stateChangeError', function () {
                    element.addClass('hide'); // hide spinner bar
                });
            }
        };
    }
]);
app.directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                elem.on('click', function (e) {
                    e.preventDefault(); // prevent link click for above criteria
                });
            }
        }
    };
});
app.directive('dropdownMenuHover', function () {
    return {
        link: function (scope, elem) {
            elem.dropdownHover();
        }
    };
});

app.directive('toggle', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            // prevent directive from attaching itself to everything that defines a toggle attribute
            if (!element.hasClass('selectpicker')) {
                return;
            }

            var target = element.parent();
            var toggleFn = function () {
                target.toggleClass('open');
            };
            var hideFn = function () {
                target.removeClass('open');
            };

            element.on('click', toggleFn);
            element.next().on('click', hideFn);

            scope.$on('$destroy', function () {
                element.off('click', toggleFn);
                element.next().off('click', hideFn);
            });
        }
    };
});
app.directive('selectpicker', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        require: '?ngModel',
        priority: 10,
        compile: function (tElement, tAttrs, transclude) {
            tElement.selectpicker($parse(tAttrs.selectpicker)());
            tElement.selectpicker('refresh');
            return function (scope, element, attrs, ngModel) {
                if (!ngModel) return;

                scope.$watch(attrs.ngModel, function (newVal, oldVal) {
                    scope.$evalAsync(function () {
                        if (!attrs.ngOptions || /track by/.test(attrs.ngOptions)) element.val(newVal);
                        element.selectpicker('refresh');
                    });
                });

                ngModel.$render = function () {
                    scope.$evalAsync(function () {
                        element.selectpicker('refresh');
                    });
                }
            };
        }

    };
}]);