module
  .controller('clientController', function ($scope, $http, $location) {
    getClients();

    function getClients() {
      //return $http.get('/api/clients');
        $http.get('/api/clients').success(function (data, status, headers, config) {

        $scope.Clients = data;

      }).error(function (data, status, heades, config) {
        $scope.title = 'Something going wrong';
      });
    }

    $scope.addClient = function(option) {
      $http.post('/api/clients', { 'id': option.id }).success(function(data, status, headers, config) {

        })
        .error(function (data, status, headers, config) {
          $scope.title = 'Something going wrong';
        });
    }

    $scope.editClient = function(id) {
      $location.path("/editClient");

      $http.get('/api/clients/' + id).success(function (data, status, headers, config) {

        $scope.client = data;

      }).error(function (data, status, heades, config) {
        $scope.title = 'Something going wrong';
      });
    }

    $scope.save= function() {
      $scope.error = "AAA";
    }
  })