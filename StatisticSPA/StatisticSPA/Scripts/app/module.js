var module = angular.module('StatApp', ['ngRoute']);

//app.factory("shareDataService", function() {
//  var entityId = {};

//  function set(newValue) {
//    entityId = newValue;
//  }

//  function get() {
//    return entityId;
//  }

//  return {
//    set: set,
//    get: get
//}
//});

//module
//  .config([
//    '$routeProvider', function config($routeProvider) {
//      $routeProvider
//        .when("/showClients", {
//          templateUrl: "home/showClients",
//          controller: "ClientCtrl"
//        })
//        .when('/showOrders', {
//          templateUrl: 'home/showOrders',
//          controller: 'ShowOrdersController'
//        })
//        .otherwise({ redirectTo: "/" });
//    }
//  ]);
module
  .config([
    '$routeProvider', function config($routeProvider) {
      $routeProvider
        .when("/showClients", {
          templateUrl: "home/showClients",
          controller: "ClientCtrl"
        })
        .when('/editClient', {
          templateUrl: 'home/editClient',
          controller: 'ClientCtrl'
        })
        .otherwise({ redirectTo: "/" });
    }
  ]);