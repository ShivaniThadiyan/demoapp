'use strict';
app.factory('ordersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var ordersServiceFactory = {};

    var _getorders = function () {

        return $http.get(serviceBase + 'api/orders').then(function (results) {

            return results;
        });
    };

    ordersServiceFactory.getorders = _getorders;

    var _updateQuota = function (email, quota) {

        return $http.post(serviceBase + '/api/orders', { Email: email, Quota: quota }).then(function (results) {
            alert(email + ' is sent a notification that the target has been updated to ' + quota);
            return results;
        });

        //var resp = $http({
        //    url: "/api/orders/UpdateData",
        //    method: "POST",
        //    data: parameters
        //});
        //return resp;
    };
    ordersServiceFactory.updateQuota = _updateQuota;

    return ordersServiceFactory;
}]);



