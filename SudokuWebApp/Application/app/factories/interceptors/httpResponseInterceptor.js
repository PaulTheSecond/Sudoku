(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .factory('httpResponseInterceptor', httpResponseInterceptor);

    httpResponseInterceptor.$inject = ['$q', '$location'];

    /**
     * Перехватчик Http-ответов
     * see in http://www.codeproject.com/Articles/806029/Getting-started-with-AngularJS-and-ASP-NET-MVC-Par
     * @param {} $q 
     * @param {} $location 
     * @returns {} 
     */
    function httpResponseInterceptor($q, $location) {
        return {
            response: function (response) {
                return response || $q.when(response);
            },
            responseError: function (rejection) {
                switch (rejection.status) {
                    case 404:
                        $location.path('/404');
                        break;
                    case 500:
                        $location.path('/500');
                }
                return $q.reject(rejection);
            }
        };
    }
})();