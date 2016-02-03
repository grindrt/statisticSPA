module
  .controller('groupController', function($scope, $http) {
    $http.get('/api/groups').success(function(data) {
      $scope.Groups = data;
    });
  })