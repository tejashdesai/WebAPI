(function () {
    'use strict';

    angular.module("insuranceApp.main").controller('SettingsController',settingsController);

    settingsController.$inject = ['SettingService','message'];

    function settingsController(SettingService,message) {
        var vm= this;

        function initDTO(){
            vm.settingData = {};
        }

        function getSettingData(){
            SettingService.getSettings(function(data){
                vm.settingData = data || {};
            },function(error){
                
            });
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
            getSettingData();
        }


        init();
    }

})();