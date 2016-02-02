var module = angular.module('StatApp', ['ngRoute']);

//module.directive('hcPieChart', function () {
//  return {
//    restrict: 'E',
//    template: '<div></div>',
//    scope: {
//      title: '@',
//      data: '='
//    },
//    link: function (scope, element) {

//      Highcharts.chart(element[0], {
//        chart: {
//          type: 'pie'
//        },
//        plotOptions: {
//          pie: {
//            allowPointSelect: true,
//            cursor: 'pointer',
//            dataLabels: {
//              enabled: true,
//              format: '<b>{point.name}</b>: {point.percentage:.1f} %'
//            }
//          }
//        },
//        series: [{
//          data: scope.data
//        }]
//      });
//    }
//  }
//});

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
//          controller: "clientController"
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
          controller: "clientController"
        })
        .when('/editClient', {
          templateUrl: 'home/editClient',
          controller: 'clientController'
        })
        .when('/showChart', {
          templateUrl: 'home/showChart',
          controller: 'chartController'
        })
        .otherwise({ redirectTo: "/" });
    }
  ]);