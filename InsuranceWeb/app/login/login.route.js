(function () {
    'use strict';

    angular.module("insuranceApp.login").config(routeConfig);

    routeConfig.$inject = ['$stateProvider'];

    function routeConfig($stateProvider) {
        $stateProvider
            .state('login', {
                abstract: true,
                template: '<ui-view></ui-view>',
            })
            .state('login.login', {
                url: '/login',
                templateUrl: 'app/login/login/login.html',
                controller: "LoginController",
                controllerAs: 'login'
            });
    }

})();