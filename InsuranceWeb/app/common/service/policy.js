(function () {
    'use strict';
    angular.module('insuranceApp.common.service').service('PolicyService', policyService);

    policyService.$inject = ['AjaxFactory', 'APIPATH'];

    function policyService(AjaxFactory, APIPATH) {

        this.getDashboardData = getDashboardData;
        this.getSummaryData = getSummaryData;
        this.getCurrentPolicy = getCurrentPolicy;
        this.savePolicy = savePolicy;
        this.getPolicyData = getPolicyData;
        this.deletePolicyData = deletePolicyData;
        this.sendNotification = sendNotification;

        function getDashboardData(successFunction, errorFunction) {
            AjaxFactory.get(APIPATH + 'dashboard', {}, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }

        function getSummaryData(successFunction, errorFunction) {
            AjaxFactory.get(APIPATH + 'summary', {}, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }

        function getCurrentPolicy(successFunction, errorFunction) {
            AjaxFactory.get(APIPATH + 'currentpolicy', {}, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }

        function savePolicy(data, successFunction, errorFunction) {
            AjaxFactory.fileUpload(APIPATH + 'savepolicy', data, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error);
            });
        }

        function getPolicyData(data, successFunction, errorFunction) {
            AjaxFactory.get(APIPATH + 'policy/' + data, {}, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }

        function deletePolicyData(data, successFunction, errorFunction) {
            AjaxFactory.get(APIPATH + 'deletepolicy/' + data, {}, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }

        function sendNotification(data, successFunction, errorFunction) {
            AjaxFactory.get(APIPATH + 'notification/' + data, {}, function (response) {
                successFunction(response.data);
            }, function (error) {
                errorFunction(error.data);
            });
        }
    }

})();