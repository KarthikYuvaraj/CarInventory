(function () {
    'use strict';

    angular
        .module('carInventory')
        .controller('CarFormModalController', CarFormModalController);

    CarFormModalController.$inject = ['$scope', '$modalInstance'];

    function CarFormModalController($scope, $modalInstance) {

        $scope.ok = function () {
            $modalInstance.close($scope.car);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
})();