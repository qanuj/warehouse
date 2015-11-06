app.controller('billingController', ['$scope', 'dataService', '$stateParams','$rootScope', function ($scope, db, $stateParams,$rootScope) {

  $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Billing Transactions";
    $scope.noCreditMessage = "Start Adding more credits and Post Active Requirement to your Facebook Page.";
    $scope.firstCredit = 10;
    $scope.addMessage = "Add 10 Credits to Start Posting Requirement";

    $scope.status = $stateParams.status;
    if ($stateParams.status == 'success') {
        $scope.message = "Yipee!, Thank you! Now you are go shopping.";
    } else if ($stateParams.status == 'success') {
        $scope.message = "Ouch! Something went wrong, our team will look into this, meanwhile you can use you current credits, or tr one more time.";
    }

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

