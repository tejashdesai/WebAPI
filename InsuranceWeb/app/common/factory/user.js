(function () {
    'use strict';
    angular.module('insuranceApp.common.factory').factory('UserFactory', userFactory);

    userFactory.$inject = ['$localStorage', '$state','$http'];

    function userFactory($localStorage, $state,$http) {
        return {
            getCurrentUser: getCurrentUser,
            setCurrentUser: setCurrentUser,
            clearCurrentUser: clearCurrentUser
        };

        function getCurrentUser() {
            var user = $localStorage.userData || {};
            if (angular.isNull(user)) {
                $state.go('login.login');
            } else {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' +user.access_token; // jshint ignore:line
                return user;
            }
        }

        function setCurrentUser(data) {
            $localStorage.userData = data;
        }

        function clearCurrentUser() {
            delete $localStorage.userData;
        }
    }

})();