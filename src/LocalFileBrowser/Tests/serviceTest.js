describe('Api Factory Test', function () {
    var browserApiFactory,
        httpBackend;

    beforeEach(function () {
        // load the module.
        module('app');

        // get your service, also get $httpBackend
        // $httpBackend will be a mock, thanks to angular-mocks.js
        inject(function ($httpBackend, _browserApiFactory_) {
            browserApiFactory = _browserApiFactory_;
            httpBackend = $httpBackend;
        });
    });

    // make sure no expectations were missed in your tests.
    afterEach(function () {
        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();
    });

    it('Check get drives method', function () {
        // set up some data for the http call to return and test later.
        var returnDrives = 
                        [{ "name": "C:\\", "isReady": true, "totalSize": 195 },
                        { "name": "E:\\", "isReady": true, "totalSize": 50 },
                        { "name": "G:\\", "isReady": true, "totalSize": 100 },
                        { "name": "D:\\", "isReady": false, "totalSize": 0 }];

        // expectGET to make sure this is called once
        httpBackend.expectGET('/api/browser').respond(returnDrives);

        // make the call.
        var returnedPromise = browserApiFactory.getDrives();

        // set up a handler for the response, that will put the result
        // into a variable in this scope for you to test.
        var result;
        returnedPromise.then(function (response) {
            result = response.data;
        });

        // flush the backend to "execute" the request to do the expectedGET assertion.
        httpBackend.flush();

        // check the result. 
        expect(result).toEqual(returnDrives);
    });

    it('Check get files and folders', function () {
        // set up some data for the http call to return and test later.
        var returnData = { "folders": "C:\\Rec", "totalSize": 195 };

        // expectGET to make sure this is called once
        httpBackend.expectGET('/api/browser/drive?path=C:\File').respond(returnData);

        // make the call.
        var params = "C:\File";
        var returnedPromise = browserApiFactory.getFilesAndFolders(params);

        // set up a handler for the response, that will put the result
        // into a variable in this scope for you to test.
        var result;
        returnedPromise.then(function (response) {
            result = response.data;
        });

        // flush the backend to "execute" the request to do the expectedGET assertion.
        httpBackend.flush();

        // check the result. 
        expect(result).toEqual(returnData);
    });
});