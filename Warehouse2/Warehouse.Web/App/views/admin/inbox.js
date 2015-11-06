app.controller('adminInboxController', ['$scope', 'dataService', '$stateParams','$rootScope','$state', function ($scope, db, $stateParams,$rootScope,$state) {
$scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Inbox";
    $scope.subtitle = $state.current.data.pageTitle;

    $scope.navigate = function (page) {
        db.admin.feedbacks(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.from = (($scope.currentPage-1) * db.pageSize)+1;
            $scope.to = $scope.from + result.items.length - 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.count = result.count;
            $scope.records = result.items;
        });
    }

    $scope.toggleMessage = function (row) {
        if (!row.isRead) {
            read(row, true);
        }
        row.show = !row.show;
    }

    $scope.markRead = function (row) {
        read(row, true);
    }

    $scope.markDelete = function (row) {
        return db.admin.deleteFeedback(row.id).success(function (result) {
            $scope.navigate();
        });
    }

    $scope.markUnRead = function (row) {
        read(row, false);
    }

    $scope.navigate($stateParams.page);


    function read(row, what) {
        return db.admin.feedback(row.id, what).success(function (result) {
            row.isRead = what;
        });
    }

}]);

