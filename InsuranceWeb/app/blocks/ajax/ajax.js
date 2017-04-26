(function () {
    'use strict';
    angular.module('blocks.ajax', ['cfp.loadingBar']);
    angular.module('blocks.ajax').factory('AjaxFactory', AjaxFactory);

    AjaxFactory.$inject = ['$q', '$http', '$httpParamSerializer', 'cfpLoadingBar'];

    function AjaxFactory($q, $http, $httpParamSerializer, cfpLoadingBar) {
        return {
            post: post,
            get: get,
            put: put,
            ajaxDelete: ajaxDelete,
            formEncoded: formEncoded,
            fileUpload: fileUpload
        }

        function checkPendingRequest() {
            if ($http.pendingRequests.length == 0) {
                cfpLoadingBar.complete();
            } else {
                var count = 0;
                for (var i = 0; i < $http.pendingRequests.length; i++) {
                    if (!$http.pendingRequests[i].cache) {
                        count++;
                    }
                }
                if (count === 0) {
                    cfpLoadingBar.complete();
                }
            }
        }

        function post(url, data, successFunction, errorFunction) {
            cfpLoadingBar.start();
            $http.post(url, data)
                .then(function (data) {
                    checkPendingRequest();
                    if (!data) {
                        errorFunction(data);
                    }
                    successFunction(data);
                }, function (error) {
                    checkPendingRequest();
                    errorFunction(error);
                });
        }

        function get(url, data, successFunction, errorFunction) {
            cfpLoadingBar.start();
            $http.get(url, data)
                .then(function (data) {
                    checkPendingRequest();
                    if (!data) {
                        errorFunction(data);
                    }
                    successFunction(data);
                }, function (error) {
                    checkPendingRequest();
                    errorFunction(error);
                });
        }

        function put(url, data, successFunction, errorFunction) {
            cfpLoadingBar.start();
            $http.put(url, data)
                .then(function (data) {
                    checkPendingRequest();
                    if (!data) {
                        errorFunction(data);
                    }
                    successFunction(data);
                }, function (error) {
                    checkPendingRequest();
                    errorFunction(error);
                });
        }

        function ajaxDelete(url, data, successFunction, errorFunction) {
            cfpLoadingBar.start();
            $http.delete(url, data)
                .then(function (data) {
                    checkPendingRequest();
                    if (!data) {
                        errorFunction(data);
                    }
                    successFunction(data);
                }, function (error) {
                    checkPendingRequest();
                    errorFunction(error);
                });
        }

        function fileUpload(url, data, successFunction, errorFunction) {
            cfpLoadingBar.start();
            var req = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': undefined
                },
                data: data
            };
            $http(req)
                .then(function (data) {
                    checkPendingRequest();
                    if (!data) {
                        errorFunction(data);
                    }
                    successFunction(data);
                }, function (error) {
                    checkPendingRequest();
                    errorFunction(error);
                });
        }


        function formEncoded(url, data, successFunction, errorFunction) {
            cfpLoadingBar.start();
            var req = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: $httpParamSerializer(data)
            };
            $http(req)
                .then(function (data) {
                    checkPendingRequest();
                    if (!data) {
                        errorFunction(data);
                    }
                    successFunction(data);
                }, function (error) {
                    checkPendingRequest();
                    errorFunction(error);
                });
        }
    }

})();