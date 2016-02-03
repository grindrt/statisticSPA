module.controller('groupController', function($scope, $http, $location, shareDataService) {
  $http.get('/api/groups').success(function(data) {
    $scope.Groups = data;
  });

  $scope.addGroup = function() {
    $location.path("/addGroup");
  }

  $scope.editGroup = function(id) {
    shareDataService.set(id);
    $location.path("/editGroup");
  }

  $scope.deleteGroup = function(id) {
    $http.delete('/api/groups/' + id).success(function() {
      $http.get('/api/groups').success(function(data) {
        $scope.Clients = data;
      });
    });
  }
});

module.controller('addGroupController', function($scope, $http, $location) {
  $scope.showId = false;
  $scope.group = { title: null, color: null, clients: null }
  $scope.groupColor = {
    control: 'Wheel',
    theme: 'bootstrap',
    position: 'top left'
  }

  $http.get('/api/clients/').success(function(data) {
    $scope.Clients = data;
    $scope.selectedClients = { ids: {} };
  });

  $scope.save = function() {
    var clients = $scope.selectedClients.ids;
    var group = $scope.group;
    group.clients = [];

    for (var prop in clients) {
      if (clients.hasOwnProperty(prop)) {
        group.clients.push({ id: prop });
      }
    }

    $http.put('api/groups/', client).then(function() {
      $location.path("/showGroups");
    }, function(errorResult) {
      $scope.error = errorResult.statusText;
    });
  }
});

module.controller('editGroupController', function ($scope, $http, $location, shareDataService) {
  $scope.showId = true;
  $scope.groupColor = {
    control: 'Wheel',
    theme: 'bootstrap',
    position: 'top left'
  }

  $http.get('/api/groups/' + shareDataService.get()).success(function (group) {
    $scope.group = group;
    $http.get('/api/clients').success(function (clients) {
      $scope.Clients = clients;
      $scope.selectedClients = { ids: {} };

      for (var i = 0; i < $scope.group.clients.length; i++) {
        var clientId = $scope.group.clients[i].id;
        $scope.selectedClients.ids[clientId] = true;
      }

      shareDataService.set(null);
    });
  });

  $scope.save = function () {
    var clients = $scope.selectedClients.ids;
    var group = $scope.group;
    group.clients = [];

    for (var prop in clients) {
      if (clients.hasOwnProperty(prop)) {
        group.clients.push({ id: prop });
      }
    }

    $http.put('api/groups/', client).then(function () {
      $location.path("/showGroups");
    }, function (errorResult) {
      $scope.error = errorResult.statusText;
    });
  }
})