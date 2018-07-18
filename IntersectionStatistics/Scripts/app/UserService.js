MyApp.factory('UserService', ['$http', function ($http) {

    var urlBase = 'localhost:63695/Api';
    var UserService = {};
    UserService.getUsers = function () {
        return $http.get(urlBase + '/Users');
    };

    return UserService;
}]);

