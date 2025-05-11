// 'Describe' creates a Jasmine test.
// A describe block contains assertions, using the 'it' function.
describe('clearObservable', function () {
    var directoryViewModel = new DirectoryViewModel("D:\\users\\tbenf\\downloads");    

    // 'beforeEach' performs setup before each 'it' test
    beforeEach(function () {
        directoryViewModel.newDirectoryName("New Directory");
    });
    it('Sets new folder name to empty string', function () {
        clearObservable(directoryViewModel.newDirectoryName);

        var actual = directoryViewModel.newDirectoryName();
       
        console.log("A: " + actual);
        // Jasmine uses the 'expect' function for assertions. Its format is very human-readable.
        // If an expection proves false, it will throw an exception and the assertion will be reported as failed.
        expect(actual).toBe("");
    });    
});

describe('clearObservableArray', function () {
    var directoryViewModel = new DirectoryViewModel("D:\\users\\tbenf\\downloads");   
        
    it('Clears out subdirectories', function () {
        clearObservableArray(directoryViewModel.subDirectories);

        var actual = directoryViewModel.subDirectories();

        console.log("A: " + actual);
        // Jasmine uses the 'expect' function for assertions. Its format is very human-readable.
        // If an expection proves false, it will throw an exception and the assertion will be reported as failed.
        expect(actual.length).toBe(0);
    });
});

describe('getDriveLetter', function () {    
    // 'beforeEach' performs setup before each 'it' test
    it('Gets the drive letter as first character in path', function () {
        var actual = getDriveLetter("d/users/account");

        // Jasmine uses the 'expect' function for assertions. Its format is very human-readable.
        // If an expection proves false, it will throw an exception and the assertion will be reported as failed.
        expect(actual).toBe("d");
    });
});

describe('getPathFromName', function () {
    // 'beforeEach' performs setup before each 'it' test
    it('Gets URL from drive path ', function () {
        var actual = getPathFromName("d\\users\\account");

        // Jasmine uses the 'expect' function for assertions. Its format is very human-readable.
        // If an expection proves false, it will throw an exception and the assertion will be reported as failed.
        expect(actual).toBe("d/users/account");
    });
});