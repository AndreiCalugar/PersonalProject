(function () {
    'use strict';

    var datePicker = angular
        .module('app')
        .controller('StatisticsController', StatisticsController);

    StatisticsController.$inject = ['$scope', 'StatisticsService', '$rootScope', '$timeout'];
    function StatisticsController($scope, StatisticsService, $rootScope, $timeout) {



        $scope.allStatistics = [];
        $scope.getStatistics = statistics;
        $scope.returnStatistics = receiveStatstistics;
        



        function statistics() {
          
            StatisticsService.GetAll($scope.date3, $scope.date4, function (response) {
                
                $scope.allStatistics = response;
                

                for (var i = 0; i < $scope.allStatistics.length; i++ )
                {
                    console.log($scope.allStatistics[i].date);
                    $scope.labels.push($scope.allStatistics[i].date);
                    $scope.data.push($scope.allStatistics[i].decibels);
                    console.log($scope.labels[i]);
                    console.log($scope.allStatistics[i].decibels);
                   
                  
                }

             
            });
        }

        function receiveStatstistics() {
            StatisticsService.GetCollectedData(function (response) {
                if (response != false) {

                    var myresponse = new Object();
                    myresponse.date = response.feeds[0].last_value_at;
                    myresponse.decibels = response.feeds[0].last_value;
                    myresponse.gas = response.feeds[0].last_value;
                    myresponse.counter = response.feeds[0].last_value;
                    console.log(myresponse);

                   
                    StatisticsService.Create(myresponse);
                    $timeout(receiveStatstistics, 10000);
                }
                else
                           console.log("eroare");
                           // $timeout(getCurrentLocation, 1000);
              
             });


        }
    



        $scope.labels = [];
        $scope.series = ['Series A', 'Series B'];
        $scope.data = [];

        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };

        $scope.type = "line";
        $scope.options = [
            {
                size: {
                    height: 550,
                    width: 400
                }
            }
        ];
        $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }];
        $scope.options = {
            scales: {
                yAxes: [
                    {
                        id: 'y-axis-1',
                        type: 'linear',
                        display: true,
                        position: 'left'
                    }
                ]
            }
        };
       

    }

})();