(function () {
    'use strict';

    angular.module("insuranceApp").controller('AppController', appController);

    appController.$inject = ['$scope'];

    function appController($scope) {
        var vm = this;


        var mobileView = 992;

        vm.getWidth = function () {
            return window.innerWidth;
        };

        $scope.$watch(vm.getWidth, function (newValue, oldValue) {
            if (newValue >= mobileView) {
                vm.toggle = true;
            } else {
                vm.toggle = false;
            }

        });

        vm.toggleSidebar = function () {
            vm.toggle = !vm.toggle;
        };

        window.onresize = function () {
            $scope.$apply();
        };

    }

})();