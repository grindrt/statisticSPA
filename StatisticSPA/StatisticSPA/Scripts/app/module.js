var module = angular.module('StatApp', ['ngRoute']);


module.factory("shareDataService", function() {
  var savedObject = null;

  function set(newValue) {
    savedObject = newValue;
  }

  function get() {
    return savedObject;
  }

  return {
    set: set,
    get: get
}
});

module
  .config([
    '$routeProvider', function config($routeProvider) {
      $routeProvider
        .when("/showClients", {
          templateUrl: "home/showClients",
          controller: "clientController"
        })
        .when('/editClient', {
          templateUrl: 'home/editClient',
          controller: 'clientController'
        })
        .when('/showGroups', {
          templateUrl: 'home/showGroups',
          controller:'groupController'
        })
        .when('/showReports', {
          templateUrl: 'home/showReports',
          controller: 'chartController'
        })
        .otherwise({ redirectTo: "/" });
    }
  ]);