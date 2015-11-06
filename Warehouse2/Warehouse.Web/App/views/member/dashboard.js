app.controller('companyDashboardController', ['$rootScope', '$scope', 'dataService', function ($rootScope,$scope, db) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    db.company.dashboard().success(function (result) {
        result.aggregate.duration.min = moment(result.aggregate.duration.min).toDate();
        result.aggregate.duration.max = moment(result.aggregate.duration.max).toDate();
        $scope.record = result;
        
        db.company.search({ location: result.aggregate.location, Skills: result.aggregate.skill }, 1, 5, 'Days').success(function (result) {
            $scope.matching = result.items;
        });
    });

    db.company.get().success(function (result) {
        $scope.profile = result;
    });

    $scope.search = function (q) {
        window.location = '#/search?q=' + (q.keywords || '') + '&location=' + (q.location || '') + '&skills=' + (q.skills || '');
        return false;
    }
}]);