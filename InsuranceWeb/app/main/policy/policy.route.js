(function () {
    'use strict';

    angular.module("insuranceApp.main").config(routeConfig);

    routeConfig.$inject = ['$stateProvider'];

    function routeConfig($stateProvider) {
        $stateProvider
            .state('main.policy', {
                url : '/policy',
                abstract: true,
                template: '<ui-view></ui-view>'
            })
            .state('main.policy.expired', {
                url: '/expired',
                templateUrl: 'app/main/policy/expired/expired.html',
                controller: "ExpiredPolicyController",
                controllerAs: 'new',
                data: {
                    title :'Expired Policy'
                }
            })
            .state('main.policy.current', {
                url: '/current',
                templateUrl: 'app/main/policy/current/current.html',
                controller: "NewPolicyController",
                controllerAs: 'new',
                data: {
                    title :'Current Policy'
                }
            })
            .state('main.policy.new', {
                url: '/new',
                templateUrl: 'app/main/policy/new/new.html',
                controller: "NewPolicyController",
                controllerAs: 'new',
                data: {
                    title :'Add/Edit Policy'
                }
            });
    }

})();