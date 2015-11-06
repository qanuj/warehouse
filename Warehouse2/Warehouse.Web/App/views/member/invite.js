app.controller('companyInviteController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, $stateParams, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Invite Bench Team";
    $scope.save = "Sent Invitations";
    $scope.records = [];

    function addRecords(x) {
        for (var i = 0; i < x; i++) {
            $scope.records.push({ name: '', email: '' });
        }
    }

    $scope.add = addRecords;

    $scope.sendInvites = function (rows) {
        var invs = [];
        for (var x in rows) {
            if (!!rows[x].name && !!rows[x].email) invs.push(rows[x]);
        }
        $scope.status = "sending";
        db.company.sendInvites(invs).success(function () {
            $scope.sent = "sent";
        });
    }

    addRecords(5);

}]);