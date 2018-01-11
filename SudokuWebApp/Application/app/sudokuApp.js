(function () {
    'use strict';

    var app = angular.module('sudokuApp', [
        // Angular modules 
        'ngRoute',
        'ngAnimate',
        'ngResource',
        'ngMessages'
        // Custom modules 

        // 3rd Party Modules

    ]);

    configFunction.$inject = ['$routeProvider', '$httpProvider'];

    app.config(configFunction);

    function configFunction($routeProvider, $httpProvider) {
        $routeProvider
            .when('/', {
                controller: 'HomeController',
                templateUrl: '/views/home/index.html',
                requiresAuthentication: true
            })            
            .when('/404', {
                templateUrl: '/views/error/fof.html'
            })
            .when('/500', {
                templateUrl: '/views/error/InternalServerError.html'
            })
            .otherwise({
                redirectTo: '/404'
            });

        $httpProvider.interceptors.push('httpResponseInterceptor');
        $httpProvider.interceptors.push('timeoutHttpInterceptor');
    };

    app.run(['$rootScope', function ($rootScope) {
        $rootScope.mainTitle = 'Sudoku WEB-application';
    }]);

})();