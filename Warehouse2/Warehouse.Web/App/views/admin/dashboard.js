app.controller('adminDashboardController', ['$scope','dataService','$rootScope', function ($scope,db,$rootScope) {

    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    //do nothing ?
}]);