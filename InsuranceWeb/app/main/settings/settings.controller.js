(function () {
    'use strict';

    angular.module("insuranceApp.main").controller('HomeController',homeController);

    homeController.$inject = ['SettingService'];

    function homeController(SettingService) {
        var vm= this;

        function initDTO(){
            vm.settingData = {};
        }

        function getSettingData(){
            SettingService.getDashboardData(function(data){
                vm.settingData = data || {};
            },function(error){
                
            })
        }

        function init(){
            initDTO();
            getSettingData();
        }


        vm.saveSettings = function(){
            SettingService.saveSettings(vm.settingData,function(data){
                message.success('Settings', 'Saved successfully.');
            },function(error){
                message.success('Settings', 'Error while saving settings.');
            })
        };

        vm.cancelSettings = function(){
            init();
        }


        init();
    }

})();