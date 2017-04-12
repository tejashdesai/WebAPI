(function () {
    'use strict';

    angular.module("insuranceApp.main").controller('HomeController',homeController);

    homeController.$inject = ['$state', 'PolicyService', 'MONTHS'];

    function homeController($state,PolicyService,MONTHS) {
        var vm= this;

        vm.months = MONTHS;

        function getDashboardData(){
            PolicyService.getDashboardData(function(data){
                vm.dashboardData = data || [];
            },function(error){
                console.log(error);
            })
        }

        function getSummaryData(){
            PolicyService.getSummaryData(function(data){
                vm.summaryData = data || [];
            },function(error){
                console.log(error);
            })
        }

        function init(){
            getDashboardData();
            getSummaryData();
        }

        vm.searchPolicy = function(item){
            $state.go('main.policy.current',{ 'month' : item.month });
        };

        init();
    }

})();