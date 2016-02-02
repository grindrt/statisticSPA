module
  .controller('chartController', function($scope, $http) {

  $scope.chartTypeData = {
    chartTypeSelect: null,
    chartTypes: [
      { id: 1, title: 'Histogram' },
      { id: 2, title: 'Pie' }
    ]
  }

  $scope.chartTypeSelected = function() {

    $http.get('/api/groups').success(function (data) {

      var chart;

      if ($scope.chartTypeData.chartTypeSelect == 1) {
        var columnData = { lines: [] };

        for (var i = 0; i < data.length; i++) {

          var line = {
            name: data[i].title,
            y: data[i].client.length,
            color: data[i].color
          }

          columnData.lines.push(line);
        }

        $scope.columnData = columnData;

        chart = {
          chart: {
            type: 'column'
          },
          title: {
            text: 'Quantity of clients per group'
          },
          xAxis: {
            type: 'category'
          },
          yAxis: {
            title: {
              text: 'Total clients in group'
            }

          },
          legend: {
            enabled: true
          },
          plotOptions: {
            series: {
              dataLabels: {
                enabled: true,
                format:'{point.y} client(s)'
              },
              colorByPoint: true
            }
          },
          series: [
            {
              name: "Groups",
              data: $scope.columnData.lines
            }
          ]
        };

      } else {

        var pieData = { lines: [], colors: [] };

        for (var i = 0; i < data.length; i++) {

          var line = {
            name: data[i].title,
            y: data[i].client.length,
            color: data[i].color
          }

          pieData.lines.push(line);
          pieData.colors.push(data[i].color);
        }

        $scope.pieData = pieData;

        chart = {
          chart: {
            type: 'pie'
          },
          plotOptions: {
            pie: {
              allowPointSelect: true,
              cursor: 'pointer',
              dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
              }
            }
          },
          series: [
            {
              data: $scope.pieData.lines,
              colors: $scope.pieData.colors,
              showInLegend: true
            }
          ]
        };
      }

      Highcharts.chart('chartContainer', chart);


    });
  }

})