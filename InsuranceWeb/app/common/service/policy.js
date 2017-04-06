(function () {
    'use strict';
    angular.module('insuranceApp.common.service').service('PolicyService', policyService);

    policyService.$inject = ['AjaxFactory', 'APIPATH'];

    function policyService(AjaxFactory, APIPATH) {

        this.getDashboardData = getDashboardData;

        function getDashboardData(data, successFunction, errorFunction) {
            AjaxFactory.formEncoded(APIPATH + 'token', data, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }
    }

})();