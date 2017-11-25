(function () {
    'use strict';

    angular
        .module('carInventory')
        .controller('CarController', CarController);

    CarController.$inject = ['$scope', '$q', 'CarService', 'errorHandler', '$modal'];

    function CarController($scope, $q, CarService, errorHandler, $modal) {
        (function startup() {
            var cars = CarService.getCars();

            $q.all([
                cars
            ]).then(function (data) {
                if (data != null) {
                    $scope.cars = data[0];
                }
            }, function (reason) {
                errorHandler.logServiceError('CarController', reason);
            }, function (update) {
                errorHandler.logServiceNotify('CarController', update);
            });
        })();

        function removeCar(carId) {
            for (var i = 0; i < $scope.cars.length; i++) {
                if ($scope.cars[i].id == carId) {
                    $scope.cars.splice(i, 1);
                    break;
                }
            }
        };

        $scope.cars = [];

        $scope.Commands = {
            saveCar: function (car) {
                CarService.addCar(car).then(
                    function (result) {
                        $scope.cars.push(result.data);
                    },
                    function (response) {
                        console.log(response);
                    });
            },
            updateCar: function (car) {
                CarService.updateCar(car).then(
                    function (result) {

                    },
                    function (response) {
                        console.log(response);
                    });
            }
        };

        $scope.Queries = {
            getCars: function () {
                CarService.getCars();
            },
            getCarById: function (carId) {
                CarService.getCarById(carId);
            }
        };

        $scope.Modals = {
            open: function (car) {
                $scope.car = car;

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/App/Templates/Car/CarFormModal.html',
                    controller: 'CarFormModalController',
                    size: 'lg',
                    scope: $scope,
                    backdrop: 'static'
                });

                modalInstance.result.then(
                    function (car) {
                        if (car.id != null) {
                            $scope.Commands.updateCar(car);
                        }
                        else
                        {
                            $scope.Commands.saveCar(car);
                        }
                    },
                    function (event) {

                    });
            },
            deleteCar: function (carId) {
                if (confirm('Are you sure you want to delete this car?')) {
                    CarService.deleteCar(carId).then(
                        function (data) {
                            removeCar(carId);
                        },
                        function (response) {
                            console.log(response);
                        });
                }
                else {
                    console.log('delete cancelled');
                }
            }
        }
    };
})
();