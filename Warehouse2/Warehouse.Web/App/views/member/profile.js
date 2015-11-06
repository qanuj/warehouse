app.controller('companyProfileController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, param, $rootScope) {
    
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Company Profile";

    db.company.get(param.id).success(function (result) {
        $scope.record = result;
        $scope.page = db.currentPage;
        db.job.paged(result.id, 1).success(function (result) {
            $scope.jobs = result.items;
        });
    });

}]);