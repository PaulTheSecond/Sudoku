(function () {
    'use strict';

    angular
        .module('sudokuApp')
        .directive('numbersOnly', accountNumberDirective);

    //patternValidityMessageDirective.$inject = ['$window'];

    function accountNumberDirective() {
        // Usage:
        //     <div numbers-only></div>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            element.on('keydown', function (e) {
                var serviceCodes = [36, 35, 46, 8, 9, 13, 37, 39];
                var numPads = [97, 98, 99, 100, 101, 102, 103, 104, 105];

                if (e.ctrlKey || e.metaKey || _.indexOf(serviceCodes, e.keyCode) !== -1) {
                    return;
                }
                if (_.indexOf(numPads, e.keyCode) !== -1) {
                    if (e.target.value.length >= 12) {
                        e.preventDefault();
                    }
                    return;
                }
                var char = String.fromCharCode(e.keyCode || e.charCode);
                if (!char.match(/^\d{0,1}$/) || e.target.value.length >= 1) {
                    e.preventDefault();
                }
            });
        }
    }

})();