(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$scope', '$http', 'cellService'];

    function HomeController($scope, $http, cellService) {
        $scope.title = 'HomeController';

        activate();

        function activate() {
            initField();
            $scope.onHintClick = _onHintClick;
        }

        function initField() {
            $http.get('/api/field').then(
                function (resp) {
                    $scope.data = resp.data.gameField;
                    $scope.solvedData = resp.data.solvedField;
                },
                function (err) { })
        }

        function _onHintClick() {
            var selectedIndex = cellService.detectSelectedCellIndex($scope.data.children);
            debugger;
            var ai = cellService.getAreaIndex(selectedIndex.i);
            var aj = cellService.getAreaIndex(selectedIndex.j);
            var ci = cellService.getCellInAreaIndex(selectedIndex.i);
            var cj = cellService.getCellInAreaIndex(selectedIndex.j);

            var value = $scope.solvedData.children[ai][aj].children[ci][cj].value;

            $scope.data.children[ai][aj].children[ci][cj].value = value;
            $scope.data.children[ai][aj].children[ci][cj].isDisabled = true;
        }        
    }
})();
