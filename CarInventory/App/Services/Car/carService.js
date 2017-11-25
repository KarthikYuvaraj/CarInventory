(function () {
    'use strict';

    angular
        .module('carInventory')
        .factory('CarService', CarService);

    CarService.$inject = ['$resource', '$q', '$http'];

    function CarService($resource, $q, $http) {
        var resource = $resource('/api/Cars/:action/:param', { action: '@action', param: '@param'}, {
            'update': { method: 'PUT' }
        });

        var _getCars = function () {
            var deferred = $q.defer();
            resource.query({ action: "get", param: ""},
				function (result) {
				    if (result == null) {
				        result = [];
				    };
				    deferred.resolve(result);
				},
				function (response) {
				    deferred.reject(response);
				});
            return deferred.promise;

        };

        var _getCarById = function (carId) {
            var deferred = $q.defer();
            resource.query({ action: 'ById', param: carId},
				function (result) {
				    if (result == null) {
				        result = [];
				    };

				    deferred.resolve(result);
				},
				function (response) {
				    deferred.reject(response);
				});
            return deferred.promise;
        };

        var _addCar = function (carDto) {
            var deferred = $q.defer();

            $http.post('/api/Cars', carDto)
                .then(function (result) {
                        deferred.resolve(result);
                    },
                        function (response) {
                            deferred.reject(response);
                        });

            return deferred.promise;
        };

        var _updateCar = function (carDto) {
            var deferred = $q.defer();

            $http.put('/api/Cars/' + carDto.id, carDto)
                .then(function (result) {
                    deferred.resolve(result);
                },
                        function (response) {
                            deferred.reject(response);
                        });

            return deferred.promise;
        };

        var _deleteCar = function (carId) {
            var deferred = $q.defer();

            resource.delete({ action: "", param: carId},
                    function (result) {
                        if (result == null) {
                            result = [];

                        };
                        deferred.resolve(result);
                    },
                    function (response) {
                        deferred.reject(response);
                    });
            return deferred.promise;
        };

        return {
            getCars: _getCars,
            getCarById: _getCarById,
            addCar: _addCar,
            updateCar: _updateCar,
            deleteCar: _deleteCar
        };

    }

})();