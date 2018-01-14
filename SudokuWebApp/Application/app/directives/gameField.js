(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .directive('gameField', GameField);

    GameField.$inject = ['$window'];

    function GameField($window) {
        // Usage:
        //     <game-field></game-field>
        // Creates:
        // 
        var directive = {
            replace: true,
            restrict: 'E',
            templateUrl: './views/directives/field.directive.html',
            controller: function ($scope, $element) {
                
            },
            scope: {
                fieldData: '='
            }
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();