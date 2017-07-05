(function () {
    'use strict';

    angular.module('insuranceApp.core')
        .constant('APIPATH', 'http://insuranceapi.jivantclinic.com/')
        .constant('MONTHS', {
            1: 'January',
            2: 'Febraury',
            3: 'March',
            4: 'April',
            5: 'May',
            6: 'June',
            7: 'July',
            8: 'August',
            9: 'September',
            10: 'Octobor',
            11: 'November',
            12: 'December'
        });
})();