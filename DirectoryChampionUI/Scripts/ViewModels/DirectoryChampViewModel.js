"use strict";

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

var DirectoryViewModel = function (path) {
    // Data
    var self = this;
    self.directoryPath = ko.observable(path);
    self.subDirectories = ko.observableArray().trackHasItems();
    self.files = ko.observableArray().trackHasItems();
    self.newDirectoryName = ko.observable();
    self.destinationFolder = ko.observable();
    self.selectedDirectoryPath = ko.observable();
    self.selectedFilePath = ko.observable();
    self.searchTerms = ko.observable();
    self.fileToUpload = ko.observable();       

    // Computed Obvservables
    self.hasSubDirectoriresOrFiles = ko.computed(function () {
        return self.subDirectories.hasItems || self.files.hasItems;
    }, this);

    self.isFile = ko.computed(function () {
        return self.selectedDirectoryPath() === null ||
            self.selectedDirectoryPath() === '';
    }, this);

    self.uploadName = ko.computed(function () {
        return self.fileToUpload() ? self.fileToUpload().name : '-';
    });    

    self.notRoot = ko.computed(function () {       
        return self.directoryPath().length > 2;
    });

    // Initialize view model from path
    $.getJSON(getRootGetUrlFromPath(defaultPath),        
        function (data) {
            // Now use this data to update your view models,
            // and Knockout will update your UI automatically
            var directory =
                new Directory(data.driveLetter + ":" + data.directoryPath, true);
            
            $.each(data.subDirectories, function (key, val) {
                self.subDirectories.push(new Directory(val, false));
            });

            $.each(data.files, function (key, val) {
                self.files.push(val);
            });
        });

    // -----------------
    // --- Behaviors ---
    // -----------------    

    //  --- Creates a folder
    self.showNewFolderModal = function (data) {
        toggleModal("#newFolderModal");
    };

    self.createFolder = function (data) {
        var newDirectoryName = data.newDirectoryName();

        toggleModal("#newFolderModal");
        console.log("SELF: " + ko.toJSON(self.items));

        $.ajax({
            url: 'https://localhost:44306/api/directory/create/' + newDirectoryName,
            type: "PUT",
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON(data),
            datatype: 'json',
            processData: false,
            success: function (result) {

                if (result.returnvalue === true) {
                    var newSubDirectoryPath =
                        self.directoryPath().replace(/\//g, '\\') + '\\' + newDirectoryName;

                    var newSubDirectory = new Directory(newSubDirectoryPath, false);

                    if (self.subDirectories().indexOf(newSubDirectory) < 0) {
                        self.subDirectories.push(newSubDirectory);
                    }
                }
                else {
                    // --- TODO: Show Error
                }
            }
        });

        // Clear newDirectoryName
        clearObservable(self.newDirectoryName);
    };

    // --- Navigate to parent directory
    self.upALevel = function (data) {
        var dirPath = self.directoryPath();

        // Remove last path value in string
        var upLevelDirPath =
            dirPath.lastIndexOf("\\") === 0 ?
                "" : dirPath.slice(0, dirPath.lastIndexOf("\\"));

        // Update our observable
        self.directoryPath(upLevelDirPath);

        // Set path for getJSON
        var path = getDriveLetter(upLevelDirPath) +
            '/' + upLevelDirPath.substring(3).replace('\\', '/');

        $.getJSON('https://localhost:44306/api/directory/' + path,
            function (data) {
                var directory =
                    new Directory(data.driveLetter + ":" + data.directoryPath, true);

                clearObservableArray(self.subDirectories);
                clearObservableArray(self.files);

                $.each(data.subDirectories, function (key, val) {
                    self.subDirectories.push(new Directory(val, false));
                });

                $.each(data.files, function (key, val) {
                    self.files.push(val);
                });              
            });
    };

    //  --- Search Directory
    self.searchDirectory = function (data) {
        $.ajax({
            url: 'https://localhost:44306/api/directory/search/',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON(data),
            datatype: 'json',
            processData: false,
            success: function (result) {
                // Clear and rebuild our directories
                clearObservableArray(self.subDirectories);
                clearObservableArray(self.files);
                
                $.each(result.subDirectories, function (key, val) {
                    console.log("SEARCH SUB: " + val);
                    self.subDirectories.push(new Directory(val, false));
                });

                // Clear and rebuild our files
                $.each(result.files, function (key, val) {
                    console.log("SEARCH FILE: " + val);
                    self.files.push(val);
                });

                clearObservable(self.directoryPath);
                clearObservable(self.searchTerms);
            }
        });
    };

    // --- Move SubDirectory or File
    self.move = function (data) {
        console.log("MOVING: to " + self.destinationFolder());

        var isFile = self.selectedFilePath() ? true : false;
        var destination = self.destinationFolder();

        $.ajax({
            url: 'https://localhost:44306/api/directory/move/' + destination,
            type: "PUT",
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON(data),
            datatype: 'json',
            processData: false,
            success: function (result) {
                alert("Directory Moved");
            }
        });
    };

    self.copy = function (data) {
        console.log("COPYING: to " + self.destinationFolder());
    };

    // --- Download
    self.downloadFile = function (data) {
        var link = document.createElement('a');
        link.href = data;
        link.download = data;
        document.body.appendChild(link);
        link.click();
    };

    // --- Upload
    self.showUploadModal = function (data) {
        self.fileToUpload(data.fileToUpload)
        $("#fileUploadModal").modal('show');
    };

    self.uploadFileToServer = function (test) {       
        var data = $('#formUpload').serializeObject();        
        console.log("TEST: " + ko.toJSON(test));
        console.log("1: " + $("#formUpload").serialize());
        console.log("2: " + $("#formUpload").serializeArray());
        console.log("3: " + $("#formUpload").serializeObject());

        var file = $('form input[type=file]')[0].files[0];

        $.ajax({
            url: 'https://localhost:44306/api/directory/RawStringDataManual/',
            processData: false,
            contentType: 'application/json; charset=utf-8',
            data: data,
            type: 'POST'
        }).done(function (result) {
               alert(result);
        }).fail(function (a, b, c) {
               console.log(a, b, c);
            });       
    };

    // --- Delete
    self.deleteDirectory = function (data) {
        self.selectedDirectoryPath(data.directoryPath);
        clearObservable(self.selectedFilePath);

        toggleModal("#deleteConfirmationModal");       
    };

    self.deleteDirectoryConfirmed = function (data) {        

        toggleModal("#deleteConfirmationModal");

        $.ajax({
            url: 'https://localhost:44306/api/directory/deletedirectory/',
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON(self),
            datatype: 'json',
            processData: false,
            success: function (response) {
                
                if (response.returnvalue === true) {                    
                    self.subDirectories($.grep(data.subDirectories(),
                                            function (o, i) {
                                                return o.directoryPath === data.selectedDirectoryPath();
                                            },
                        true));

                    console.log("SUB: " + ko.toJSON(self.subDirectories()));
                }
                else {
                    // --- TODO: Show Error
                }
            }
        });
    };

    self.deleteFile = function (data) {
        clearObservable(self.selectedDirectoryPath);
        self.selectedFilePath(data);

        toggleModal("#deleteConfirmationModal");
    };  

    self.deleteFileConfirmed = function (data) {
       
        toggleModal("#deleteConfirmationModal");

        $.ajax({
            url: 'https://localhost:44306/api/directory/deletefile/',
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON(self),
            datatype: 'json',
            processData: false,
            success: function (response) {
                if (response.returnvalue === true) {
                    self.files($.grep(data.files(),
                        function (o, i) {
                            return o === data.selectedFilePath();
                        },
                        true));
                }
                else {
                    // --- TODO: Show Error
                }
            }
        });
    };    

    // Drag and drop functionality
    // Drag and drop functionality
    self.dragging = function (data, event) {
        console.log("DRAGGING: " + data);

        // Our drag only allows move operations
        //event.dataTransfer.effectAllowed = "move";

        //dataTransfer.setData("text", data);

        // The target of a drag start is a directory or file
        // The data for a direcotry or file is its path
        //event.dataTransfer.setData("text/plain", data.directoryPath());
    }

    self.allowDrop = function (data, event) {
        console.log("ALLOW DROP SUB");
        event.target.classList.add("validtarget");

        event.preventDefault();
    }

    self.dragLeft = function (data, event) {
        console.log("DRAG LEFT SUB");
        event.target.classList.remove("validtarget");
        event.preventDefault();
    }

    self.dropped = function (data, event) {
        console.log("DROP SUB: " + event.target);
        //event.target.classList.remove("validtarget");
        //event.preventDefault();

        //var path = data.originalFolder();
        //console.log("PATH: " + path);

        console.log("MOVING: " + self.originalFolder() + " to " + self.directoryPath());        
    }  
};

var Directory = function (path, expanded) {
    // Data
    var self = this;

    self.driveLetter = ko.observable(path[0]);
    self.directoryPath = ko.observable(path);
    self.directories = ko.observableArray().trackHasItems();
    self.files = ko.observableArray().trackHasItems();
    self.dateCreated = ko.observable();
    self.lastModified = ko.observable();    
    self.expanded = ko.observable(expanded);
    self.selected = ko.observable(false);
    self.originalFolder = ko.observable();

    if (self.expanded()) {
        var expandedPath =
            getRootGetUrlFromPath(self.directoryPath());
        
        $.getJSON(expandedPath,
            function (data) {               
                // Clear and rebuild our directories
                $.each(data.subDirectories, function (key, val) {                    
                    self.directories.push(new Directory(val, false));
                });

                // Clear and rebuild our files
                $.each(data.files, function (key, val) {
                    self.files.push(val);
                });
            });
    }

    // Computed Obvservables
    self.hasSubDirectoriresOrFiles = ko.computed(function () {
        return self.directories.hasItems || self.files.hasItems;
    }, this);

    // -----------------
    // --- Behaviors ---
    // -----------------        

    // Drag and drop functionality
    self.dragging = function (data, event) {
        var keys = Object.keys(data);
        console.log("KEYS: " + keys);
        console.log("DRAGGING: " + self.directoryPath());

        var element = event.target;

        // --- TODO

        keys = Object.keys(event);
        console.log("EVENT KEYS: " + keys);
        
        // Our drag only allows move operations
        //event.dataTransfer.effectAllowed = "move";

        //event.dataTransfer.setData("text", self.directoryPath());        
    }

    self.allowDrop = function (data, event) {
        console.log("ALLOW DROP");
        event.target.classList.add("validtarget");
    }

    self.dragLeft = function (data, event) {
        console.log("DRAG LEFT");
        event.target.classList.remove("validtarget");
    }

    self.dropped = function (data, event) {
        
    }       

    // Expands the directory
    self.expandOrCollapse = function (data, event) {
        if (!data.expanded()) {
            $.getJSON(getRootGetUrlFromPath(data.directoryPath()),
                function (returnData) {
                    // Now use this data to update your view models,
                    // and Knockout will update your UI automatically                
                    $.each(returnData.subDirectories, function (key, val) {
                        data.directories.push(new Directory(val, false));
                    });

                    $.each(returnData.files, function (key, val) {
                        data.files.push(val);
                    });
                });

            data.expanded(true);
        }
        else {
            clearObservableArray(data.directories);
            clearObservableArray(data.files);

            data.expanded(false);
        }

        toggleTwo(event.target, "expandable", "collapsable");
    }

    self.loadSubdirectoriesAndFiles = function () {
        var path =
            getRootGetUrlFromPath(self.directoryPath());

        $.getJSON(path,
            function (data) {
                // Now use this data to update your view models,
                // and Knockout will update your UI automatically

                // Clear and rebuild our directories
                self.directories([]);
                $.each(data.directories, function (key, val) {
                    self.directories.push(val);
                });

                // Clear and rebuild our files
                self.files([]);
                $.each(data.files, function (key, val) {
                    self.files.push(val);
                });
            });
    }

    // --- Opens a folder and displays its contents
    self.goToFolder = function (data) {
        var path = getRootGetUrlFromPath(data);

        $.getJSON(path,
            function (data) {
                // Now use this data to update your view models,
                // and Knockout will update your UI automatically

                // Clear and rebuild our directories
                self.directories([]);
                $.each(data.directories, function (key, val) {
                    self.directories.push(val);
                });

                // Clear and rebuild our files
                self.files([]);
                $.each(data.files, function (key, val) {
                    self.files.push(val);
                });
            });

        var newRootPath = data.substring(2);

        self.directoryPath(newRootPath);
    };      

    // A computed internally ask to start tracking dependencies 
    //   and receive a notification when anything observable is accessed
    //ko.dependencyDetection.begin({
    //    callback: function (subscribable, internalId) {
    //        console.log("original context: " + internalId + " was accessed");
    //    }
    //});
};

ko.observableArray.fn.trackHasItems = function () {
    // Create a sub-observable
    this.hasItems = ko.observable();

    // Update it when the observableArray is updated
    this.subscribe(function (newValue) {
        this.hasItems(newValue && newValue.length ? true : false);
    }, this);

    // Trigger change to initialize the value
    this.valueHasMutated();

    // Support chaining by returning the array
    return this;
};

ko.bindingHandlers.fileUpload = {
    init: function (element, valueAccessor) {
        $(element).change(function () {
            valueAccessor()(element.files[0]);
        });
    },
    update: function (element, valueAccessor) {
        if (ko.unwrap(valueAccessor()) === null) {
            $(element).wrap('<form>').closest('form').get(0).reset();
            $(element).unwrap();
        }
    }
};

ko.bindingHandlers.singleClick = {
    init: function (element, valueAccessor) {
        var handler = valueAccessor(),
            delay = 100,
            clickTimeout = false;

        $(element).click(function () {
            if (clickTimeout !== false) {
                clearTimeout(clickTimeout);
                clickTimeout = false;
            } else {
                clickTimeout = setTimeout(function () {
                    clickTimeout = false;
                    handler();
                }, delay);
            }
        });
    }
};

(function ($, ko) {
    var _dragged, _hasBeenDropped, _draggedIndex;

    ko.bindingHandlers.drag = {
        init: function (element, valueAccessor, allBindingsAccessor,
            viewModel, context) {
            var value = valueAccessor();
            var dragElement = $(element);

            var dragOptions = {
                containment: 'window',
                helper: function (evt, ui) {
                    var h = dragElement.clone().css({
                        width: dragElement.width(),
                        height: dragElement.height()
                    });
                    h.data('ko.draggable.data', value(context, evt));
                    return h;
                },
                revert: true,
                revertDuration: 0,
                start: function () {
                    _hasBeenDropped = false;
                    _dragged = ko.utils.unwrapObservable(valueAccessor().value);
                    element.style.opacity = '0.2';
                    if ($.isFunction(valueAccessor().value)) {
                        valueAccessor().value(undefined);
                        dragElement.draggable("option", "revertDuration", 500);
                    } else if (valueAccessor().array) {
                        _draggedIndex = valueAccessor().array.indexOf(_dragged);
                        valueAccessor().array.splice(_draggedIndex, 1);
                    }
                },
                stop: function (e, ui) {
                    element.style.opacity = '1';

                    if (!_hasBeenDropped) {                       
                        if ($.isFunction(valueAccessor().value)) {
                            valueAccessor().value(_dragged);
                        } else if (valueAccessor().array) {
                            valueAccessor().array.splice(_draggedIndex, 0, _dragged);
                        }
                    }
                },
                appendTo: 'body'
            };
            dragElement.draggable(dragOptions).disableSelection();           
        }
    };

    ko.bindingHandlers.drop = {
        init: function (element, valueAccessor, allBindingsAccessor,
            viewModel, context) {
            var value = valueAccessor();
            $(element).droppable({
                tolerance: 'pointer',
                hoverClass: 'dragHover',
                activeClass: 'dragActive',
                drop: function (evt, ui) {
                    value(ui.helper.data('ko.draggable.data'), context);
                }
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor,
            viewModel, context) {
            var value = valueAccessor();

            if (ko.unwrap(value))
                console.log("UNWRAP: " + ko.unwrap(value));
            else
                console.log("WRAP: " + value);
        }
    };
})(jQuery, ko);

// Default path added as variable as requested
var defaultPath = 'D:\\users\\tbenf\\downloads';
var directoryViewModel = new DirectoryViewModel(defaultPath);
ko.applyBindings(directoryViewModel);

$(document).ready(function () {
    // Display modal
    $('.a-modal').on('click', function (e) {
        e.preventDefault();
        $('#directoryChampionModal').modal('show').find('.modal-content').load($(this).attr('href'));
    });

    $('#newFolderModal').on('shown.bs.modal', function () {
        $(this).trigger('focus');
    });

    $('#deleteConfirmationModal').on('shown.bs.modal', function () {
        $(this).trigger('focus');
    });    
});




