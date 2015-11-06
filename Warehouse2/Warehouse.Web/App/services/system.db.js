app.factory('systemService', [
    '$http', '$q', 'db.util', function ($http, $q, util) {
        var v = 'api/v1/';

        var calculatePaging = util.calculatePaging;
        var orderBy = util.orderBy;

        var sys = {};

        function buildMaster(master){
            return {
                paged: function(page, pageSize,q,c) {
                    var uri = v + 'system/' + master + '/paged?$orderby=Title&$inlinecount=allpages' + calculatePaging(page, pageSize);
                    if (q) uri += '&$filter=substringof(\'' + q + '\',Title)';
                    return $http.get(uri);
                },
                add: function(record) {
                    return $http.post(v + 'system/' + master + '/create', record);
                },
                update: function(formData) {
                    return $http.put(v + 'system/' + master + '/update', formData);
                },
                remove: function(record) {
                    return $http.delete(v + 'system/' + master + '/' + record.id);
                }
            };
        }

        sys.skill = buildMaster('skill');
        sys.country = buildMaster('country');
        sys.location = buildMaster('location');
        sys.functional = buildMaster('functional');
        sys.industry = buildMaster('industry');
        sys.qualification = buildMaster('qualification');

        sys.getSkills = function (q) {
            var uri = v + 'system/skill/all?$orderby=Title';
            if (q) uri += '&$filter=substringof(\'' + q + '\',Title)';
            return $http.get(uri);
        }

        sys.getLocations = function (q) {
            var uri = v + 'system/location/all?$orderby=Title';
            if (q) uri += '&$filter=substringof(\'' + q + '\',Title)';
            return $http.get(uri);
        }

        sys.pagedSkill = function (page, pageSize) {
            console.log('Page - ', page, ' Pagesize', pageSize);
            return $http.get(v + 'system/skill/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        sys.addSkill = function (record) {
            return $http.post(v + 'system/skill/create', record);
        }

        sys.editSkill = function (formData) {
            return $http.put(v + 'system/skill/update', formData);
        }

        sys.deleteSkill = function (record) {
            return $http.delete(v + 'system/skill/' + record.id);
        }

        sys.pagedIndustries = function (page, pageSize) {
            console.log('Page - ', page, ' Pagesize', pageSize);
            return $http.get(v + 'system/industry/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        sys.getIndustries = function () {
            return $http.get(v + 'system/industry/all?$orderby=Title');
        }

        sys.addIndustry = function (record) {
            return $http.post(v + 'system/industry/create', record);
        }
        sys.updateIndustry = function (record) {
            return $http.put(v + 'system/industry/update', record);
        }

        sys.deleteIndustry = function (record) {
            return $http.delete(v + 'system/industry/' + record.id);
        }

        sys.pagedFunctional = function (page, pageSize) {
            console.log('Page - ', page, ' Pagesize', pageSize);
            return $http.get(v + 'system/functional/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        sys.getFunctionals = function () {
            return $http.get(v + 'system/functional/all?$orderby=Title');
        }

        sys.getQualifications = function () {
            return $http.get(v + 'system/qualification/all?$orderby=Title');
        }

        sys.getContractType = function () {
            return $http.get(v + 'system/contractype/all?$orderby=Title');
        }

        sys.getConsultantType = function () {
            return $http.get(v + 'system/consultantype/all?$orderby=Title');
        }

        sys.getCountries = function () {
            return $http.get(v + 'system/country/all?$orderby=Title');
        }

        sys.addFunctional = function (record) {
            return $http.post(v + 'system/functional/create', record);
        }
        sys.updateFunctional = function (record) {
            return $http.put(v + 'system/functional/update', record);
        }
        sys.deleteFunctional = function (record) {
            return $http.delete(v + 'system/functional/' + record.id);
        }


        sys.pagedLocation = function (page, pageSize) {
            return $http.get(v + 'system/location/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        sys.pagedCountry = function (page, pageSize) {
            return $http.get(v + 'system/country/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        sys.searchLocations = function (address) {
            var params = { address: address, sensor: false };
            return $http.get(
                '//maps.googleapis.com/maps/api/geocode/json',
                { params: params }
            );
        }

        sys.genders = ['Male', 'Female', 'Other'];
        sys.orgType = ['Corporate', 'Consultancy'];

        sys.enums = function (name) {
            var deferred = $q.defer();
            var that = this;
            if (!that.enumsRows) {
                $http.get(v + 'system/enums').success(function (enums) {
                    that.enumsRows = enums;
                    deferred.resolve(that.enumsRows[name]);
                });
            } else {
                deferred.resolve(that.enumsRows[name]);
            }
            return deferred.promise;
        }

        return sys;

    }
]);