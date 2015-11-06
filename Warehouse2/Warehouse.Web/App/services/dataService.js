app.factory('dataService', ['$http', '$q', 'memberService', 'billingService', 'systemService', 'adminService','db.util',
    function ($http, $q, memberService, billingService, systemService, adminService,util) {

        var calculatePaging = util.calculatePaging;
        var vb = 'api/v1/banner/';

        var factory = {
            pageSize: 10,
            member: memberService,
            admin: adminService,
            billing: billingService,
            system: systemService,
            role: document.querySelector('html').dataset.role,
            args: findArguments
        };

        factory.me = factory[factory.role.toLowerCase()];

        function quickApi(api) {
            return {
                paged: function (page, pageSize, q, c) {
                    var uri = api + 'paged?$orderby=Title&$inlinecount=allpages' + calculatePaging(page, pageSize);
                    if (q) uri += '&$filter=substringof(\'' + q + '\',Title)';
                    return $http.get(uri);
                },
                add: function (record) {
                    return $http.post(api, record);
                },
                update: function (record) {
                    return $http.put(api, record);
                },
                remove: function (record) {
                    return $http.delete(api + record.id);
                }
            };
        }

        factory.banner = quickApi(vb);

        return factory;

        function findArguments() {
            var tmp = {};
            var args = window.location.hash.split('?');
            if (args.length > 1) {
                var rgs = args[1].split("&");
                for (var x in rgs) {
                    var k = rgs[x].split('=');
                    tmp[k[0]] = k.length > 1 && k[1] != 'undefined' ? decodeURIComponent(k[1]) : null;
                }
            }
            return tmp;
        }
    }]);