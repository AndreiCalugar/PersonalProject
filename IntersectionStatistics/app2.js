(function () {
    'use strict';

    angular
        .module('app', ['ngRoute', 'ngCookies', 'angularjs-datetime-picker', 'chart.js','ngMap'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider', '$locationProvider', 'ChartJsProvider'];
    function config($routeProvider, $locationProvider, ChartJsProvider) {
        $routeProvider
            .when('/', {
                controller: 'HomeController',
                templateUrl: 'Views/home.view.html',
                controllerAs: 'vm'
            })

            .when('/login', {
                controller: 'LoginController',
                templateUrl: 'Views/login.view.html',
                controllerAs: 'vm'
            })

            .when('/register', {
                controller: 'RegisterController',
                templateUrl: 'Views/register.view.html',
                controllerAs: 'vm'
            })
            .when('/decibels', {
                controller: 'StatisticsController',
                templateUrl: 'Views/decibels.view.html',
                controllerAs: 'vm'
           
            })
            .otherwise({ redirectTo: '/login' });

        // Configure all charts
        ChartJsProvider.setOptions({
            chartColors: ['#FF5252', '#FF8A80'],
            responsive: true
        });
        // Configure all line charts
        ChartJsProvider.setOptions('line', {
            showLines: true
        });
    }

    run.$inject = ['$rootScope', '$location', '$cookies', '$http'];
    function run($rootScope, $location, $cookies, $http) {
        // keep user logged in after page refresh
        $rootScope.globals = $cookies.getObject('globals') || {};
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata;
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in and trying to access a restricted page
            var restrictedPage = $.inArray($location.path(), ['/login', '/register']) === -1;
            var loggedIn = $rootScope.globals.currentUser;
            if (restrictedPage && !loggedIn) {
                $location.path('/login');
            }
        });
    }

})();