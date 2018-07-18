(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['UserService', '$rootScope','NgMap'];
    function HomeController(UserService, $rootScope, NgMap) {
        var vm = this;
       
        vm.user = null;
        vm.allUsers = [];
        vm.deleteUser = deleteUser;

        




        NgMap.getMap().then(function (map) {

            console.log(map.getCenter());
            console.log('markers', map.markers);
            console.log('shapes', map.shapes);



        });






        initController();

       

        function initController() {
     
            loadCurrentUser();
            loadAllUsers();
            
        }


      

        function loadCurrentUser() {
            UserService.GetByUsername($rootScope.globals.currentUser.username)
                .then(function (user) {
                    vm.user = user;
                });
        }

        function loadAllUsers() {
            
            UserService.GetAll()
                .then(function (response) {
                    vm.allUsers = response;
                });
        }

        function deleteUser(id) {
            UserService.Delete(id)
            .then(function () {
                loadAllUsers();
            });
        }


       
       
       
       
    }


})();