(function () {

    "use strict";

    angular.module("app", ["ngRoute"])
            .config(function ($routeProvider) {

                $routeProvider.when("/", {
                    controller: "browserController",
                    controllerAs: "vm",
                    templateUrl: "/views/drivers.html"
                });

                $routeProvider.otherwise({ redirectTo: "/" });

            });
})();