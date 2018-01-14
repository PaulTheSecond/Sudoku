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
            $scope.onNewGameClick = _onNewGameClick;
            $scope.onSubmitField = _onSubmitField;
        }

        function initField() {
            $http.get('/api/field').then(
                function (resp) {
                    debugger;
                    $scope.data = resp.data.gameField;
                    $scope.solvedData = resp.data.solvedField;
                    $scope.defaultIndex = resp.data.defaultSchemaIndex;
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

        function _onNewGameClick() {
            $scope.data = undefined;
            $scope.solvedData = undefined;
            debugger;
            $http.get('/api/field?defaultIndex=' + $scope.defaultIndex).then(
                function (resp) {
                    $scope.data = resp.data.gameField;
                    $scope.solvedData = resp.data.solvedField;
                    $scope.defaultIndex = resp.data.defaultSchemaIndex;
                },
                function (err) { })
        }

        function _onSubmitField(data) {
            debugger;
            $http.post('/api/field/check', $scope.solvedData).then(
                function (resp) {
                    var res = confirm("Congratulation! Would you want to play yet?");
                    if (res)
                        _onNewGameClick();
                },
                function (err) {
                    alert("Oops! Something going wrong!");
                })
        }
    }
})();
