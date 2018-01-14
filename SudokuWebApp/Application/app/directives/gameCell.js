(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .directive('gameCell', GameField);

    GameField.$inject = ['$window'];

    function GameField($window) {
        // Usage:
        //     <game-cell></game-cell>
        // Creates:
        // 
        var directive = {
            replace: true,
            restrict: 'E',
            template: '<input type="text" ng-model="cellData.value" class="cell" numbers-only />',
            compile: function (element, attributes) {
                return {
                    pre: function (scope, element, attributes, controller, transcludeFn) {              
                        if (!scope.cellData.value) {
                            scope.cellData.value = null;
                        }
                    }
                }
            },
            scope: {
                cellData: '='
            }
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();