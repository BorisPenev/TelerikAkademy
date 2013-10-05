// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    var localSettings = Windows.Storage.ApplicationData.current.localSettings;

    app.onactivated = function (args) {
        var messages = document.getElementById("div-messages");
        var poemEditor = document.getElementById("textarea-poem-edior");
        var saveFileButton = document.getElementById("button-save-file");
        var loadFileButton = document.getElementById("button-load-file");

        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                //load session from suspend mode
                var poem = WinJS.Application.sessionState["poem"];
                if (poem) {
                    poemEditor.innerText = poem;
                }
            }

            args.setPromise(WinJS.UI.processAll());
            
            var messages = document.getElementById("div-messages");
            var poemEditor = document.getElementById("textarea-poem-edior");
            var saveFileButton = document.getElementById("button-save-file");
            var loadFileButton = document.getElementById("button-load-file");

            saveFileButton.addEventListener("click", function () {
                var saveFilePicker = new Windows.Storage.Pickers.FileSavePicker();
                saveFilePicker.defaultFileExtension = ".txt";
                saveFilePicker.fileTypeChoices.insert("TextDocument", [".txt"]);
                saveFilePicker.suggestedFileName = "New Poem";

                saveFilePicker.pickSaveFileAsync().then(function (file) {
                    var poemText = poemEditor.innerText;
                    Windows.Storage.FileIO.writeTextAsync(file, poemText);
                });
            });

            loadFileButton.addEventListener("click", function () {
                if (poemEditor.value !== "") {
                    var messageDialog = new Windows.UI.Popups.MessageDialog(
                        "You are going to erase the current poem",
                        "Discard current poem?");
                    messageDialog.commands.append(new Windows.UI.Popups.UICommand("Confirm"));
                    messageDialog.commands.append(
                        new Windows.UI.Popups.UICommand("Cancel"));

                    // Set the command that will be invoked by default
                    messageDialog.defaultCommandIndex = 0;

                    // Set the command to be invoked when escape is pressed
                    messageDialog.cancelCommandIndex = 1;
                    messageDialog.showAsync().done(function (command) {
                        var text = command;
                        if (command.label === "Confirm") {
                            openPoem(poemEditor);
                        }
                    });
                } else {
                    openPoem(poemEditor);
                }

            });
        }
    };

    app.oncheckpoint = function (args) {
        //save session in suspend mode
        var poem = document.getElementById("textarea-poem-edior").innerText;
        WinJS.Application.sessionState["poem"] = poem;
    };

    Windows.UI.WebUI.WebUIApplication.onresuming = function(e) {
        //load session when resuming mode
        var poem = WinJS.Application.sessionState["poem"];
        if (poem) {
            var poemEditor = document.getElementById("textarea-poem-edior");
            poemEditor.innerText = poem;
        }
    };

    var openPoem = function (poemEditor) {
        var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
        openPicker.fileTypeFilter.append(".txt");
        openPicker.commitButtonText = "Pick play-list file";
        openPicker.pickSingleFileAsync().done(function (pickedFile) {
            if (pickedFile) {
                Windows.Storage.FileIO.readTextAsync(pickedFile).then(function (data) {
                    poemEditor.innerText = data;
                });
            }

        });
    };
    
    app.start();
})();
