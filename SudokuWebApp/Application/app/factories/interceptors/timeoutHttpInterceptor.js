(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .factory('timeoutHttpInterceptor', timeoutHttpInterceptor);
    
    /**
     * Перехватчик запросов, установщик таймаута на запрос
     * @returns {} 
     */
    function timeoutHttpInterceptor() {
        return {
        'request': function(config) {
                config.timeout = 20000;
                return config;
            }
        };
    };
})();