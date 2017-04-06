(function () {
    'use strict';
    angular.module('blocks.notification', ['toastr']);
    angular.module('blocks.notification').factory('message', message);

    message.$inject = ['toastr'];

    function message(toastr) {
        return {
            success: success,
            error: error,
            warn: warn,
            info: info
        }
        var defaultOption = {
            tapToDismiss: true,
            closeButton: true,
            allowHtml: true,
            newestOnTop: true,
            preventDuplicates: true,
            preventOpenDuplicates: true
        };

        function success(title, message, option) {
            var toastrOption = angular.extend(option, defaultOption);
            toastr.success(message, title, toastrOption);
        };

        function error(message, title, option) {
            var toastrOption = angular.extend(option, defaultOption);
            toastr.error(message, title, toastrOption);
        };

        function info(message, title, option) {
            var toastrOption = angular.extend(option, defaultOption);
            toastr.info(message, title, toastrOption);
        };

        function warn(message, title, option) {
            var toastrOption = angular.extend(option, defaultOption);
            toastr.warning(message, title, toastrOption);
        };
    }

})();