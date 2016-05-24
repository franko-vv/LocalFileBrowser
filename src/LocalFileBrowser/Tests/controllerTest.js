describe("Controller Test", function () {
    var controller;

    beforeEach(angular.mock.module("app"));
    beforeEach(angular.mock.inject(function ($controller) {
        controller = $controller("browserController");
    }));

    it("Clear data function", function () {
        controller.showErr = true;
        controller.countFiles = 50;
        controller.filesAndFolders = { id: 50 };

        controller.clearInformationField();

        expect(controller.showErr).toEqual(false);
        expect(controller.countFiles).toEqual({});
        expect(controller.filesAndFolders).toEqual({});
    });
    
    it("Change drive function", function () {
        controller.drivers = [{ name: 'C:\\' }, { name: 'D:\\' }];

        controller.changeDrive(1);

        expect(controller.currentDrive.name).toEqual('D:\\');
        expect(controller.currentPath).toEqual('D:\\');
        expect(controller.showParent).toEqual(false);
    });

    it("Change folder function", function () {
        controller.filesAndFolders = [{ fullName: 'C:\\Rec', parent: 'C:\\' },
                                      { fullName: 'D:\\1\\Child', parent: 'D:\\1\\' }];

        controller.changeFolder(1);

        expect(controller.currentPath).toEqual('D:\\1\\Child');
        expect(controller.parentPath).toEqual('D:\\1\\');
        expect(controller.showParent).toEqual(true);
    });

    it("To parent folder function", function () {
        controller.currentPath = 'D:\\1\\Child\\Child';
        controller.parentPath = 'D:\\1\\Child';
        controller.showParent = true;

        controller.toParentFolder();

        expect(controller.currentPath).toEqual('D:\\1\\Child');
        expect(controller.parentPath).toEqual('D:\\1');
        expect(controller.showParent).toEqual(true);
    });

    it("To parent folder function when root", function () {
        controller.currentPath = 'D:\\1';
        controller.parentPath = 'D:\\';
        controller.showParent = true;

        controller.toParentFolder();

        expect(controller.currentPath).toEqual('D:\\');
        expect(controller.showParent).toEqual(false);
    });
});