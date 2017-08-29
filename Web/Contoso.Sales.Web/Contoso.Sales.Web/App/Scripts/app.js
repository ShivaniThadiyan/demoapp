'use strict';
var app =
angular.module('todoApp', ['ngRoute','AdalAngular'])
app.config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalProvider) {

    $routeProvider.when("/Home", {
        controller: "homeCtrl",
        templateUrl: "/App/Views/Home.html",
    }).when("/TodoList", {
        controller: "todoListCtrl",
        templateUrl: "/App/Views/TodoList.html",
        requireADLogin: true,
    }).when("/quota", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    }).when("/UserData", {
        controller: "userDataCtrl",
        templateUrl: "/App/Views/UserData.html",
    }).otherwise({ redirectTo: "/Home" });

    adalProvider.init(
        {
            instance: 'https://login.microsoftonline.com/',                   
            tenant: 'common',//'65863036-0a9f-4b2d-8074-8c6239640ae6',
            clientId: '713cb9fe-27ef-4395-9d18-d21c130ab6da',
            extraQueryParameter: 'nux=1&prompt=consent',
            //cacheLocation: 'localStorage', // enable this for IE, as sessionStorage does not work for localhost.
        },
        $httpProvider
        );
   
}]);
//var serviceBase = 'http://contososales.azurewebsites.net/';

var serviceBase = 'http://contososales.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});
