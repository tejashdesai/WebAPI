(function () {
    'use strict';

    angular.module("insuranceApp").config(routeConfig);

    routeConfig.$inject = ['$urlRouterProvider'];

    function routeConfig($urlRouterProvider) {
        $urlRouterProvider.otherwise('/login');
    }

})();