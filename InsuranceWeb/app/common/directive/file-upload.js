(function () {
    'use strict';

    angular.module('insuranceApp.common.directive')
        .directive('fileUpload', fileUpload);

    fileUpload.$inject = ['$timeout', 'CommonFactory'];

    function fileUpload($timeout,CommonFactory) {
        return {
            restrict: 'A',
            scope: {
                selectedFile: '=',
                errorMessage: '=',
                isMultiple: '=',
                maxSize: '='
            },
            link: link
        };
    }

    function link(scope, element, attr) {
        // wrap tag

        var CommonFactory = element.injector().get('CommonFactory');
        var $timeout = element.injector().get('$timeout');
        var validExtension = attr.accept.split(',');
        element.bind('change', function (event) {
            var files = event.target.files;
            $timeout(function () {
                for (var i = 0, length = files.length; i < length; i++) {
                    scope.errorMessage = CommonFactory.validFile(files[i], validExtension, scope.maxSize);
                    if (!scope.errorMessage.invalid) {
                        if (scope.isMultiple) {
                            scope.selectedFile.push(files[i]);
                            element.val('');
                        } else {
                            scope.selectedFile = [];
                            scope.selectedFile.push(files[i]);
                        }
                    } else {
                        scope.selectedFile = [];
                        element.val('');
                    }
                }
            });
        });
    }
})();