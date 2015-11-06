app.factory('billingService', ['$http', '$q', 'db.util', function ($http, $q, util) {
    var v = 'api/v1/billing/';

    var calculatePaging = util.calculatePaging;
    var orderBy = util.orderBy;

    var billing = {};

    billing.balance = function () {
        return $http.get(v + 'balance');
    }
    
    billing.transactions = function (page, pageSize) {
        return $http.get(v + 'transaction?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
    }

    billing.transaction = function (id) {
        return $http.get(v + 'transaction/' + id);
    }

    billing.addCredits = function (credits) {
        return $http.post(v + 'credits/' + credits);
    }

    return billing;
}]);