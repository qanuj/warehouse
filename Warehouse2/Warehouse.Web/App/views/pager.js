app.directive('ccPager', pager);
pager.$inject = [];

function pager() {
    var directive = {
        link: link,
        restrict: 'E',
        replace: true,
        templateUrl: "app/views/pager.html",
        scope: {
            maxSize: "=",
            totalPages: "=",
            currentPage: "=",
            pageAction: "&"
        }
    };

    return directive;

    function link(scope) {
        scope.pages = [];

        scope.$watch('totalPages + currentPage', function () {
            createPageArray(scope.pages, scope.totalPages,scope.currentPage, scope.maxSize||5);
        });

        scope.gotoPage = function (p) {
            scope.pageAction({ pageNumber: p });
        };
    }

    function createPageArray(pages, totalPages,currentPage, maxPages) {
        var i;
        pages.length = 0;
        var start = currentPage - ((maxPages - 1) / 2);
        if (start <= 1) start = 1;
        for (i = start ; i <= start + maxPages && i <= totalPages; i++) {
            pages.push(i);
        }
    }
}