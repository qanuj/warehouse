app.factory('adminService', ['$http', '$q', 'db.util', function ($http, $q, util) {
    var v = 'api/v1/admin/';

    var calculatePaging = util.calculatePaging;
    var orderBy = util.orderBy;

    var admin = {};

    admin.get = function() {
        return $http.get(v + 'profile');
    }
    
    admin.transactions=function(page,pageSize) {
        return $http.get(v + 'transaction?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
    }
    
    admin.feedbacks=function(page,pageSize) {
        return $http.get(v + 'feedback?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
    }

    
    admin.banners=function(page,pageSize) {
        return $http.get(v + 'banners?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
    }
    
    admin.deleteBanner=function(id) {
        return $http.delete(v + 'banners'+id);
    }
    
    admin.createBanner=function(model) {
        return $http.post(v + 'banners',model);
    }
    
    admin.updateBanner=function(model) {
        return $http.put(v + 'banners', model);
    }
    
    admin.deleteFeedback = function (id) {
        return $http.delete(v + 'feedback/'+id);
    }

    admin.feedback = function (id,what) {
        return $http.put(v + 'feedback/'+id+'/'+what.toString().toLowerCase());
    }

    admin.transaction = function (id) {
        return $http.get(v + 'transaction/' + id);
    }

    admin.gift = function (email, credits) {
        return $http.post(v + 'gift', { email: email, credit: credits });
    }

    admin.config = function (config) {
        if (!config) return $http.get(v + 'config');
        return $http.put(v + 'config', config);
    }

    return admin;
}]);