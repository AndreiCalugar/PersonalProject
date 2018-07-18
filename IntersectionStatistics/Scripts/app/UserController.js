
angular.module('MyApp', [])
    .controller('LoginController', function ($scope, UserService) {

    getUsers();

    function getUsers() {
        UserService.getUsers()
            .success(function (users) {
                $scope.users = users;

            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;

            });
    }
});