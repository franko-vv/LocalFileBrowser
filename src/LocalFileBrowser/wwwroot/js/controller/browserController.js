(function () {

    "use strict";

    angular.module("app").controller("browserController", browserController);

    function browserController($http, browserApiFactory) {

        var vm = this;
        vm.drivers = [];                    // Drivers list
        vm.filesAndFolders = [];            // List of files and folders for current folder
        vm.countFiles = {};                 // Object contains info about files count less/between/more

        vm.currentDrive = {};               // Current Drive
        vm.currentPath = "";                // Current Path
        vm.parentPath = "";                 // Parent path  
        vm.showParent = false;              // Show navigate backward
        vm.showErr = false;                 // Show message if can't read some files

        vm.isBusy = true;                   // Loading information icon
        vm.errorMessage = "";               // Error message text

        browserApiFactory.getDrives()
            .then(function (response) { vm.drivers = response.data; },
                  function (error) { vm.errorMessage = "Failed to load data: " + error; })
            .finally(function () { vm.isBusy = false; });

        vm.changeDrive = function ($index) {            
            vm.isBusy = true;
            vm.showParent = false;

            // Set new Drive
            vm.currentDrive = vm.drivers[$index];
            vm.currentPath = vm.currentDrive.name;
            
            vm.clearInformationField();
            getData(vm.currentDrive.name);
        };

        vm.changeFolder = function ($index) {            
            vm.isBusy = true;
            vm.showParent = true;

            var route = vm.filesAndFolders[$index].fullName;
            vm.currentPath = route;
            vm.parentPath = vm.filesAndFolders[$index].parent;
            console.log(route);

            vm.clearInformationField();
            getData(route);
        };

        vm.toParentFolder = function () {
            vm.isBusy = true;
            vm.currentPath = vm.parentPath;

            vm.clearInformationField();
            getData(vm.currentPath);

            var index = vm.parentPath.lastIndexOf('\\');
            if (index == 2 & vm.currentPath.length > 3) {
                vm.parentPath = vm.currentPath.substr(0, index + 1);
            }
            else if (index == 2 & vm.currentPath.length == 3) {
                vm.showParent = false;
            } else {
                vm.parentPath = vm.currentPath.substr(0, index);
            };
        };

        var getData = function (route) {
            browserApiFactory.getFilesAndFolders(route)
                .then(function (response) {
                    vm.filesAndFolders = response.data.files;
                    vm.countFiles = response.data.countFiles;
                    if (vm.countFiles.errors != 0)
                        vm.showErr = true;
                },
                    function (error) { vm.errorMessage = "Failed to load data: " + error; })
                .finally(function () { vm.isBusy = false; });
        };

        vm.clearInformationField = function () {
            vm.countFiles = {};
            vm.filesAndFolders = {};
            vm.showErr = false;
        };
    };
})();