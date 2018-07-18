(function () {
    'use strict';

    angular
        .module('app')
        .factory('StatisticsService', StatisticsService);

    StatisticsService.$inject = ['$http'];
    function StatisticsService($http) {
        var service = {};

        service.GetAll = GetAll;
        service.GetById = GetById;
        service.GetByUsername = GetByUsername;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;
        service.GetCollectedData = GetCollectedData;
       

        return service;

        

         function GetAll(date3, date4,callback) {
            console.log(date3 + date4);
            $http.post('api/statistic/getStatisticAfterDate', { firstdate: date3, lastdate: date4 }).then(function (response) {
                console.log(response.data);
                callback(response.data);
            });
        }


         function GetCollectedData(callback) {

             var xhr = new XMLHttpRequest();
             xhr.open('GET', 'https://io.adafruit.com/api/groups/Default/receive.json?x-aio-key=0477f696cbf44827b7ce0bc9b153319a');
             xhr.onreadystatechange = function () {
                 if (this.status == 200 && this.readyState == 4) {
                     //console.log('response: ' + this.responseText);
                     callback(JSON.parse(this.responseText));
                 }
                 else
                     callback(false);
             };
             xhr.send();

         }

        











         function GetById(id) {
            return $http.get('/api/statistics/' + id).then(handleSuccess, handleError('Error getting user by id'));
        }

        function GetByUsername(username) {
            return $http.get('/api/statistics/' + username).then(handleSuccess, handleError('Error getting user by username'));
        }

        function Create(myresponse) {
            console.log(myresponse);
            return $http.post('/api/Statistic/getcollectedData', myresponse).then(handleSuccess, handleError('Error creating user'));
        }

        function Update(statistics) {
            return $http.put('/api/statistics/' + user.id, user).then(handleSuccess, handleError('Error updating user'));
        }

        function Delete(id) {
            return $http.delete('/api/statistics/' + id).then(handleSuccess, handleError('Error deleting user'));
        }

        // private functions

        function handleSuccess(res) {
            return res.data;
        }

        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();
