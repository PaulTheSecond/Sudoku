(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$scope', '$http'];

    function HomeController($scope, $http) {
        $scope.title = 'HomeController';

        activate();

        function activate() {
            $http.get('/api/field').then(
                function (resp) {
                    $scope.data = resp.data.gameField;
                },
                function (err) { })
        }
    }
})();
