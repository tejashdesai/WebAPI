(function () {
    'use strict';

    angular.module("insuranceApp").factory(httpInterceptor);

    httpInterceptor.$inject = ['$q', '$injector'];

    function httpInterceptor($q, $injector) {
        var message;
        var httpInterceptor = {
            request: request,
            requestError: requestError,
            response: response,
            responseError: responseError
        }

        return httpInterceptor;

        function request(config) {
            return config || $q.when(config);
        };

        function requestError(rejection) {
            return $q.reject(rejection);
        };

        function response(response) {
            return response || $q.when(response);;
        };

        function responseError(rejection) {
            if (rejection.status === 401) {
                var state = $injector.get('$state');
                state.go("login.login");
            } else {
                var message = $injector.get('message');
                message.error('Error', rejection.data.message);
            }
            return $q.reject(rejection);
        };
    }


    angular.module("insuranceApp").config(config);

    config.$inject = ['$httpProvider'];

    function config($httpProvider) {
        $httpProvider.interceptors.push(httpInterceptor);
    }

})();