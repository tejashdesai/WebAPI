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
            toastr.success(message, title);
        };

        function error(message, title, option) {
            toastr.error(message, title);
        };

        function info(message, title, option) {
            toastr.info(message, title);
        };

        function warn(message, title, option) {
            toastr.warning(message, title);
        };
    }

})();