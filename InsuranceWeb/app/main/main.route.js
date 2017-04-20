(function () {
    'use strict';

    angular.module("insuranceApp.main").config(routeConfig);

    routeConfig.$inject = ['$stateProvider'];

    function routeConfig($stateProvider) {
        $stateProvider
            .state('main', {
                abstract: true,
                templateUrl: 'app/main/layout.html',
                controller: "MainController",
                controllerAs: 'main',
                resolve: {
                    currentUser: ['UserFactory', function (UserFactory) {
                        var user = UserFactory.getCurrentUser();
                        return user;
                    }]
                }
            })
            .state('main.home', {
                url: '/home',
                templateUrl: 'app/main/home/home.html',
                controller: "HomeController",
                controllerAs: 'home',
                data: {
                    title: 'Home'
                }
            });
    }

})();