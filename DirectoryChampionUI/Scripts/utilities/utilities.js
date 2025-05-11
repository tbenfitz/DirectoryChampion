// Gets path for json call
function getRootGetUrlFromPath(drivePath) {
    var driveLetter = drivePath[0];

    // Replace \ with /
    var urlPath = drivePath.substring(2, drivePath.length).replace(/\x5C/g, '/');

    return 'https://localhost:44306/api/directory/' + driveLetter + urlPath;
}

 // Replace \ with /
function getPathFromName(name) {   
    return name.replace(/\x5C/g, '/');
}

// Returns first letter from a directory
//   or file path
function getDriveLetter(path) {
    return path[0];
}

// Strips the path of a directory or file
function stripPath(path) {
    return path.substring(path.lastIndexOf('\\') + 1);
}

// Toggles a modal
function toggleModal(selector) {
    $(selector).modal('toggle');
}

// Toggles any two classes
function toggleTwo(element, firstClass, secondClass) {
    $(element).toggleClass(firstClass).toggleClass(secondClass);
}

// Clears a ko.observable object
function clearObservable(observable) {
    observable("");
}

// Clears a ko.observableArray object
function clearObservableArray(observableArray) {
    observableArray([]);
}

// Used to clean up data from drag operations
function removeDragData(event) {
    if (event.dataTransfer.items) {
        // Use DataTransferItemList interface to remove the drag data
        event.dataTransfer.items.clear();
    } else {
        // Use DataTransfer interface to remove the drag data
        event.dataTransfer.clearData();
    }
} 