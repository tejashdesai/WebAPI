(function () {
    'use strict';

    angular.module("insuranceApp").run(run);

    run.$inject = ['$rootScope', '$state', '$stateParams', '$http'];

    function run($rootScope, $state, $stateParams, $http) {

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
            $http.pendingRequests.forEach(function (request) {
                if (request.cancel) {
                    request.cancel.resolve();
                }
            });

            //$rootScope.$state = $state
        });

        angular.isEmpty = function (item) {
            return item === undefined || item === null || item === '' || item === 0 || item.length === 0;
        }

        angular.isNull = function (item) {
            return item === undefined || item === null;
        }

    }

})();