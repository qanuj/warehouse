app.controller('invoiceController', ['$scope', 'dataService', '$stateParams','$rootScope', function ($scope, db, $stateParams,$rootScope) {

  $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Invoice";
    $scope.noCreditMessage = "Start Adding more credits and Promote your Profile to leading companies around world.";
    $scope.noCreditMessage = "Start Adding more credits and Promote your Profile to leading companies around world.";
    $scope.firstCredit = 10;
    $scope.addMessage = "Add 10 Credits to Start Promoting Your Profile";

    $scope.navigate = function (id) {
        db.admin.transaction(id).success(function (result) {
            $scope.record = result;
        });
    }
    $scope.navigate($stateParams.id);
}]);

