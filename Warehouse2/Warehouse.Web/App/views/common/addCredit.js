app.controller('creditController', ['$scope', 'dataService', '$stateParams', '$rootScope',function ($scope, db, $stateParams,$rootScope) {

  $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Add Credits";
    $scope.credits = [
        { color: 'bg-yellow-saffron', size: '', t: 20 },
        { color: 'bg-yellow-saffron', size: '', t: 30 },
        { color: 'bg-purple-studio', size: '', t: 40 },
        { color: 'bg-purple-studio', size: '', t: 50 },
        { color: 'bg-green-meadow', size: '', t: 60 },
        { color: 'bg-green-meadow', size: '', t: 70 },
        { color: 'bg-green-meadow', size: '', t: 80 },
        { color: 'bg-green-meadow', size: '', t: 90 },
        { color: 'bg-blue-steel', size: 'double', t: 100 },
        { color: 'bg-blue-steel', size: 'double', t: 200 },
        { color: 'bg-red-intense', size: 'double', t: 500 }
    ];
    $scope.navigate = function (page) {
        db.billing.balance(page).success(function (result) {
            $scope.balance = result;
        });
        db.billing.transactions(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }

    $scope.addCredits = function (credits) {
        if (credits <= 0) return;
        db.billing.addCredits(credits).success(function (result) {
            if (result.isError) {
                $scope.error = result.error;
                return;
            }
            window.location = result.url;
        });
    }
    $scope.navigate($stateParams.page);
}]);

