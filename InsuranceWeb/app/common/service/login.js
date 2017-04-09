(function () {
    'use strict';
    angular.module('insuranceApp.common.service').service('LoginService',loginService);

    loginService.$inject = ['AjaxFactory','APIPATH'];

    function loginService(AjaxFactory,APIPATH){

        this.loginUser = loginUser;

        function  loginUser(data,successFunction,errorFunction){
            AjaxFactory.formEncoded(APIPATH + 'token',data,function(response){
                successFunction(response.data);
            },function(error){
                errorFunction(error.data);
            });
        }
    }

})();