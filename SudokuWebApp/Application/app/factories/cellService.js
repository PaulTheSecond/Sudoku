(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .service('cellService', cellServise);

    cellServise.$inject = ['$http'];

    function cellServise($http) {
        var service = {
            detectSelectedCellIndex: detectSelectedCellIndex,
            getAreaIndex: getAreaIndex,
            getCellInAreaIndex: getCellInAreaIndex,
            unselectAll: unselectAll
        };

        return service;

        function detectSelectedCellIndex(children) {
            var result = undefined;
            var data = children;
            for (var ai = 0; ai < 3; ai++) {
                for (var aj = 0; aj < 3; aj++) {
                    for (var ci = 0; ci < 3; ci++) {
                        for (var cj = 0; cj < 3; cj++) {
                            if (data[ai][aj].children[ci][cj].isSelected) {
                                result = data[ai][aj].children[ci][cj].realIndex;
                            }
                        }
                    }
                }
            }
            return result;
        }

        function getAreaIndex(index) {
            switch (index) {
                case 0:
                case 1:
                case 2:
                    return 0;
                case 3:
                case 4:
                case 5:
                    return 1;
                case 6:
                case 7:
                case 8:
                    return 2;
                default:
                    return 0;
            }
        }

        function getCellInAreaIndex(cellIndex) {
            switch (cellIndex) {
                case 0:
                case 3:
                case 6:
                    return 0;
                case 1:
                case 4:
                case 7:
                    return 1;
                case 2:
                case 5:
                case 8:
                    return 2;
                default:
                    return 0;
            }
        }

        function unselectAll(field) {
            var si = detectSelectedCellIndex(field.children);
            while (si) {
                var ai = getAreaIndex(si.i);
                var aj = getAreaIndex(si.j);
                var ci = getCellInAreaIndex(si.i);
                var cj = getCellInAreaIndex(si.j);

                field.children[ai][aj].children[ci][cj].isSelected = false;

                si = detectSelectedCellIndex(field.children);
            }
        }
    }
})();