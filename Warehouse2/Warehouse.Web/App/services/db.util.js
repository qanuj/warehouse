app.factory('db.util', [function() {

    var factory = {
        pageSize: 10,
        calculatePaging: calculatePaging,
        orderBy: orderBy
    };

    function calculatePaging(page, pageSize) {
        pageSize = pageSize || factory.pageSize;
        var pg = "&$top=" + pageSize;
        if (page > 1) {
            pg += "&$skip=" + ((page - 1) * pageSize);
        }
        return pg;
    }

    function orderBy(order) {
        return '&$orderby=' + order;
    }

    return factory;
}]);