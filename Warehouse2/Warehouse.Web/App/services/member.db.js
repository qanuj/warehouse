app.factory('memberService', [
    '$http', '$q', 'db.util', function ($http, $q, util) {
        var v = 'api/v1/company/';

        var calculatePaging = util.calculatePaging;
        var orderBy = util.orderBy;

        var member = {};

        member.paged = function (page, pageSize) {
            return $http.get(v + 'paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        member.all = function () {
            return $http.get(v + 'all');
        }

        member.get = function (id) {
            return $http.get(v + 'profile' + (!!id ? '/' + id : ''));
        }

        member.update = function (formData) {
            return $http.put(v + 'profile', formData);
        }
        
        member.promote = function(p) {
            return $http.post(v + 'promote/' + p);
        }

        return member;

    }]);