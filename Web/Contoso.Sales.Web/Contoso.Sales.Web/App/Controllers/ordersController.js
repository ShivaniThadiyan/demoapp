'use strict';
app.controller('ordersController', ['$scope', 'ordersService', function ($scope, ordersService) {

    $scope.orders = [];

    ordersService.getorders().then(function (results) {
        $scope.orders = results.data;
        $scope.$apply();

    }, function (error) {
        //alert(error.data.message);
    });

    $scope.updateQuota = function (email, quota) {
        var data = ordersService.updateQuota(email, quota);
    };



}]);