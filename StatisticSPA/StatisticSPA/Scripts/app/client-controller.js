module
  .controller('clientController', function ($scope, $http, $location, shareDataService) {
    if (shareDataService.get()) {
      getClientForEdit();
    } else {
       getClients();
    }

    function getClients() {
        $http.get('/api/clients').success(function (data, status, headers, config) {

        $scope.Clients = data;

      }).error(function (data, status, heades, config) {
        $scope.title = 'Something going wrong';
      });
    }

    function getClientForEdit() {
      $http.get('/api/groups').success(function (data) {
        $scope.client = shareDataService.get();
        $scope.Groups = data;
        $scope.selectedGroups = { ids: {} };

        for (var i = 0; i < $scope.client.group.length; i++) {
          var groupId = $scope.client.group[i].id;
          $scope.selectedGroups.ids[groupId] = true;
        }
        shareDataService.set(null);
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

      $http.get('/api/clients/' + id).success(function (data, status, headers, config) {

        shareDataService.set(data);
        $location.path("/editClient");

      }).error(function (data, status, heades, config) {
        $scope.title = 'Something going wrong';
      });
    }

    $scope.save= function() {
      $scope.error = "AAA";

      var groupIds = $scope.selectedGroups.ids;
      var client = $scope.client;
      client.group = [];

      for (var i = 0; i < groupIds.length; i++) {
        client.group.push({
          id: groupIds[i]
        });
      }

      $http.post('api/clients', client).then(function() {
        $location.path("/showClients");
      });

    }
  })