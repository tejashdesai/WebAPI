(function () {
    'use strict';

    angular.module("insuranceApp.main.policy").controller('CurrentPolicyController', currentPolicyController);

    currentPolicyController.$inject = ['$state', '$stateParams', 'PolicyService', 'message', 'MONTHS'];

    function currentPolicyController($state, $stateParams, PolicyService, message, MONTHS) {
        var vm = this;

        vm.months = MONTHS;

        vm.filterData = {
            month: angular.isNull($stateParams.month) ? null : +$stateParams.month,
            name: '',
            policyNumber: ''
        };

        function getCurrentPolicy() {
            PolicyService.getCurrentPolicy(function (data) {
                vm.currentPolicyData = data || [];
            }, function (error) {
                console.log(error);
            })
        }

        function init() {
            getCurrentPolicy();
        }

        vm.filterRange = function (obj) {
            return obj.name.toLowerCase().indexOf(vm.filterData.name.toLowerCase()) > -1 &&
                obj.policyNumber.toLowerCase().indexOf(vm.filterData.policyNumber.toLowerCase()) > -1 &&
                (angular.isEmpty(vm.filterData.month) || obj.month === vm.filterData.month);
        };

        vm.resetFilter = function () {
            vm.filterData = {
                month: angular.isNull($stateParams.month) ? null : +$stateParams.month,
                name: '',
                policyNumber: ''
            };
        };


        vm.notifyUser = function (item) {
            PolicyService.sendNotification(item.policyHistoryID, function (data) {
                message.success('Notification', 'SMS and Email notification sent successfully.');
                $state.go('main.policy.current');
            }, function (error) {

            })
        };

        vm.deletePolicy = function (item) {
            PolicyService.deletePolicyData(item.policyID, function (data) {
                message.success('Notification', 'Policy deleted successfully.');
                var index = vm.currentPolicyData.indexOf(item);
                vm.currentPolicyData.splice(index, 1);
            }, function (error) {

            });
        };

        init();
    }

})();