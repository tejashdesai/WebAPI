(function () {
    'use strict';
    angular.module('insuranceApp.common.service').service('SettingService', settingService);

    settingService.$inject = ['AjaxFactory', 'APIPATH'];

    function settingService(AjaxFactory, APIPATH) {

        this.getSettings = getSettings;
        this.saveSettings = saveSettings;

        function getSettings(successFunction, errorFunction) {
            AjaxFactory.get(APIPATH + 'getsettings', {}, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }

        function saveSettings(data, successFunction, errorFunction) {
            AjaxFactory.formEncoded(APIPATH + 'updatesettings', data, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error);
            });
        }

      
    }

})();