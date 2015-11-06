app.controller('companyEditProfileController', ['$scope', 'dataService','$rootScope', function ($scope, db, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    db.system.getIndustries().success(function(result) {
        $scope.industries = result;
    });
    
    db.system.getLocations().success(function (result) {
        $scope.locations = result;
    });

    db.company.get().success(function (result) {
        result.picture = { url: result.pictureUrl };
        result.loc = { formatted_address: result.location };
        $scope.record = result;
    });

    $scope.refreshAddresses = function (address) {
      return db.system.searchLocations(address).then(function (response) {
            $scope.addresses = response.data.results;
        });
    };

    $scope.save = function (record) {
        if (record.picture) {
            record.pictureUrl = record.picture.url;
        }
        if (record.loc) {
            record.location = record.loc.formatted_address;
        }

        db.company.update(record).success(function (result) {
            window.location = "#/profile";
        });
    }

    $scope.orgType = db.system.orgType;

}]);