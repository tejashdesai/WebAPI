(function () {
    'use strict';
    angular.module('insuranceApp.common.factory').factory('CommonFactory', commonFactory);

    commonFactory.$inject = [];

    function commonFactory() {
        return {
            validFile: validFile
        };

        function validFile(file, validExtension, maxSize) {
            var fileData = {}
            var fileExtension ='.' +  file.name.split('.')[file.name.split('.').length - 1].toLowerCase();
            if (validExtension.indexOf(fileExtension) === -1) {
                fileData.invalid = true;
                fileData.message = 'Invalid file type';
                return fileData;
            }
            var fileSize = (file.size / 1048576).toFixed(2);
            if (fileSize > 5) {
                fileData.invalid = true;
                fileData.message = 'File should be less than 5 MB';
                return fileData.invalid;
            }
            fileData.invalid = false;
            return fileData;
        }

    }

})();