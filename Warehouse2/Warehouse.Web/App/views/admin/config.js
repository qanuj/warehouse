app.controller('adminConfigController', ['$scope', 'dataService', '$stateParams','$rootScope', function ($scope, db, $stateParams,$rootScope) {
$scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Configuration";

    $scope.saveChanges = function (config) {
        db.admin.config(config).success(function (result) {
            $scope.row = result;
        });
    }

    $scope.saveChanges();

}]);

