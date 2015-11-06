var app = angular.module('app', [
    // Angular modules 
    'ngAnimate', // animations
    'ngRoute', // routing
    'ngSanitize', // sanitizes html bindings (ex: sidebar.js)
    "ui.router",
    "oc.lazyLoad",
    // 3rd Party Modules
    'ui-rangeSlider',
    'ngTagsInput',
    'angularMoment',
    'ngCkeditor',
    'theaquaNg',
    'daterangepicker',
    'frapontillo.bootstrap-switch',
    'nya.bootstrap.select',
    'humenize',
    'toastr',
    'rzModule',
    'ui.gravatar', //gravtaar for user
    'ui.select2', //ui-select for dropdown and multi values.
    'ui.bootstrap', // ui-bootstrap (ex: carousel, pagination, dialog)
    'blueimp.fileupload', //jQuery File Uploader Component
    'rzModule' //slider module
]);
app.constant('angularMomentConfig', { preprocess: 'utc' });
app.config([
    '$ocLazyLoadProvider', '$controllerProvider', function ($ocLazyLoadProvider, $controllerProvider) {
        $ocLazyLoadProvider.config({
            // global configs go here
        });
        $controllerProvider.allowGlobals();
    }
]);

app.factory('settings', [
    '$rootScope', function ($rootScope) {
        // supported languages
        var settings = {
            layout: {
                pageSidebarClosed: false, // sidebar menu state
                pageBodySolid: false, // solid body color state
                pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
            },
            layoutImgPath: Metronic.getAssetsPath() + 'admin/layout/img/',
            layoutCssPath: Metronic.getAssetsPath() + 'admin/layout/css/'
        };

        $rootScope.settings = settings;

        return settings;
    }
]);
app.controller('AppController', [
    '$scope', '$rootScope', function ($scope, $rootScope) {
        $scope.$on('$viewContentLoaded', function () {
            Metronic.initComponents(); // init core components
            //Layout.init(); //  Init entire layout(header, footer, sidebar, etc) on page load if the partials included in server side instead of loading with ng-include directive 
        });
    }
]);

app.controller('HeaderController', ['$scope','$rootScope','dataService', function ($scope,$rootScope,db) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initHeader(); // init header
    });
    var role = document.querySelector('html').dataset.role.toLowerCase();
    $scope.role = role;
    db.me.get().success(function (result) {
        if (!result.pictureUrl && result.hash) {
            result.pictureUrl = "//gravatar.com/avatar/" + result.hash + "?s=50";
        }
        $rootScope.profile = result;
    });
}]);

/* Setup Layout Part - Sidebar */
app.controller('SidebarController', ['$scope',function ($scope) {
    $scope.role = document.querySelector('html').dataset.role.toLowerCase();
    $scope.$on('$includeContentLoaded', function () {
        Layout.initSidebar(); // init sidebar
    });
}]);

/* Setup Layout Part - Footer */
app.controller('FooterController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initFooter(); // init footer
    });
}]);
/* Init global settings and run the app */
app.run(["$rootScope", "settings", "$state", function ($rootScope, settings, $state) {
    $rootScope.$state = $state; // state to be accessed from view
}]);