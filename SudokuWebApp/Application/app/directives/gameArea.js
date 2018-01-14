(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .directive('gameArea', GameField);

    GameField.$inject = ['$window'];

    function GameField($window) {
        // Usage:
        //     <game-area></game-area>
        // Creates:
        // 
        var directive = {
            replace: true,
            restrict: 'E',
            templateUrl: './views/directives/area.directive.html',
            controller: function ($scope, $element) {
                
            },
            scope: {
                areaData: '='
            }
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();