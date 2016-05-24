(function () {

    "use strict";

    angular.module("app", ["ngRoute"])
            .config(function ($routeProvider) {

                $routeProvider.when("/", {
                    controller: "browserController",
                    controllerAs: "vm",
                    templateUrl: "/views/browser.html"
                });

                $routeProvider.otherwise({ redirectTo: "/" });

            });
})();