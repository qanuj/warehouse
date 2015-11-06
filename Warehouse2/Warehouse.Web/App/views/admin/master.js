app.controller('adminMasterController', ['$scope', 'dataService', '$routeParams', '$rootScope', '$state', function ($scope, db, params, $rootScope,$state) {
      $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = $state.current.data.pageTitle;
    var master = db.system[$state.current.data.module];
    
    var pageSize = 1000;
    $scope.navigate = function (page, q) {
        page = page || $scope.currentPage || 1;
        master.paged(page, pageSize, q).success(function (result) {
            $scope.currentPage = page;
            pageSize = !q && page == 1 && result.items.length < pageSize ? result.items.length : pageSize;
            $scope.pages = Math.ceil(result.count / pageSize);
            $scope.records = result.items;
        });
    }

    function onSaved() {
        $scope.navigate();
    }


    $scope.save = function (record) {
        $('input[type=text]').each(function () {
            $(this).val('');
        });
        master.add(record).success(onSaved);
    }

    $scope.update = function (record) {
        master.update(record).success(onSaved);
    }

    $scope.delete = function (record) {
        master.remove(record).success(onSaved);
    }

    $scope.toggle = function (record) {
        record.editMode = !record.editMode;
    }

    $scope.navigate(params.page);


}]);

