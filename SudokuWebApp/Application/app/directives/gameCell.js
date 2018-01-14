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
            template: '<input type="text" ng-model="cellData.value" class="cell" ng-focus="onFocus()" ng-disabled="cellData.isDisabled" numbers-only required />',
            compile: function (element, attributes) {
                return {
                    pre: function (scope, element, attributes, controller, transcludeFn) {              
                        if (!scope.cellData.value) {
                            scope.cellData.value = null;
                        } else {
                            scope.cellData.isDisabled = true;
                        }
                    }
                }
            },
            controller: function ($scope, $element, cellService) {
                $scope.onFocus = function () {
                    if ($scope.cellData.isDisabled)
                        return;

                    cellService.unselectAll($scope.field);
                    $scope.cellData.isSelected = true;
                }
            },
            scope: {
                cellData: '=',
                field: '='
            }
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();