module.controller('clientController', function($scope, $http, $location, shareDataService) {
  $http.get('/api/clients').success(function(data) {
    $scope.Clients = data;
  });

  $scope.addClient = function() {
    $location.path("/addClient");
  }

  $scope.deleteClient = function(id) {
    $http.delete('/api/clients/' + id).success(function() {
      $http.get('/api/clients').success(function (data) {
        $scope.Clients = data;
      });
    });
  }

  $scope.editClient = function(id) {
    shareDataService.set(id);
    $location.path("/editClient");
  }
});

module.controller('addClientController', function($scope, $http, $location) {
  $scope.showId = false;
  $scope.client = { firstName: null, lastName: null, email: null, birthDate: null, city: null, gender: null, groups: null }

  $http.get('/api/groups').success(function(data) {
    $scope.Groups = data;
    $scope.selectedGroups = { ids: {} };
  });

  $scope.save = function() {
    var groupIds = $scope.selectedGroups.ids;
    var client = $scope.client;
    client.groups = [];

    for (var prop in groupIds) {
      if (groupIds.hasOwnProperty(prop)) {
        client.groups.push({ id: prop });
      }
    }

    $http.put('api/clients/', client).then(function() {
      $location.path("/showClients");
    }, function(errorResult) {
      $scope.error = errorResult.statusText;
    });
  }
});

module.controller('editClientController', function ($scope, $http, $location, shareDataService) {
  $scope.showId = true;

  $http.get('/api/clients/' + shareDataService.get()).success(function(client) {
    $scope.client = client;
    $http.get('/api/groups').success(function(groups) {
      $scope.Groups = groups;
      $scope.selectedGroups = { ids: {} };

      for (var i = 0; i < $scope.client.groups.length; i++) {
        var groupId = $scope.client.groups[i].id;
        $scope.selectedGroups.ids[groupId] = true;
      }

      shareDataService.set(null);
    });
  });

  $scope.save = function() {
    var groupIds = $scope.selectedGroups.ids;
    var client = $scope.client;
    client.groups = [];

    for (var prop in groupIds) {
      if (groupIds.hasOwnProperty(prop)) {
        client.groups.push({ id: prop });
      }
    }

    $http.post('api/clients/', client).then(function() {
      $location.path("/showClients");
    }, function(errorResult) {
      $scope.error = errorResult.statusText;
    });

  }
});