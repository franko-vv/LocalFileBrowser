angular.module("app").factory('browserApiFactory', ['$http', function ($http) {

    var browserApiFactory = {};

    browserApiFactory.getDrives = function () {
        return $http.get("/api/browser");
    };

    browserApiFactory.getFilesAndFolders = function (newPath) {
        return $http.get("/api/browser/drive", { params: { path: newPath } });
    };

    return browserApiFactory;
}]);