(function () {
    'use strict';

    angular.module("insuranceApp.login").controller('LoginController', loginController);

    loginController.$inject = ['$state', 'UserFactory', 'LoginService'];

    function loginController($state, UserFactory, LoginService) {
        var vm = this;

        vm.userData = {
            grant_type: 'password'
        };

        vm.loginUser = function () {
            vm.invalidUser = false;
            LoginService.loginUser(vm.userData, function (data) {
                UserFactory.setCurrentUser(data);
                setTimeout(function () {
                    $state.go('main.home');
                }, 500);
            }, function (error) {
                vm.invalidUser = true;
                vm.invalidMessage = 'The username or password is incorrect';
            });
        };
    }

})();