(function () {
    //var db = {
    //    folders: [{ id: 1, subId: 0, folderName: "New Foler" },
    //        { id: 2, subId: 0, folderName: "New Folder(1)" },
    //        { id: 3, subId: 0, folderName: "New Folder(2)" }]
    //};

    TreeView = function (param) {
        //localStorage.removeItem("myFolders");
        this.treeViewContainer = document.getElementById(param.treeViewContainer);
        //this.treeViewContainer = param.treeViewContainer;
        this.btnAddFolder = param.btnAddFolder;
        this.txtNewFolderName = param.txtNewFolderName;
        this.data = [];
    };

    TreeView.prototype = {
        init: function () {
            //this.initDB();
            this.loadMyFolders();
            this.initEvents();
        },
        //initDB: function () {
        //    initAddElement(db.folders);
        //    this.loadMyFolders();
        //},
        loadLoacalStorage: function () {
            if (localStorage["myFolders"] != undefined) {
                this.data = JSON.parse(localStorage["myFolders"]);
            }
        },
        saveLocalStorage: function() {
            localStorage["myFolders"] = JSON.stringify(this.data);
        },
        loadMyFolders: function () {
            if (this.data.length === 0) {
                this.loadLoacalStorage();
            }
            initAddElement(this.data);
            //var i;
           
            //var foldersCount = this.data.length;
            //for (i = 0; i < foldersCount; i++) {
            //    this.treeViewContainer.innerHTML += "<li id='" + this.data[i].id + "'>" + this.data[i].folderName + "</li>";
            //}
        },
        initEvents: function () {
            var that = this;
            this.btnAddFolder.addEventListener('click', function (evt) {
                evt.preventDefault();
                //var folderName = that.txtNewFolderName.value;
                //that.treeViewContainer.innerHTML += "<li>" + folderName + "</li>";
                var subId = 0;
                if (treeViewContainer != null && treeViewContainer.childNodes != null) {
                    for (var j = 0; j < treeViewContainer.childNodes.length; j++) {
                        if (treeViewContainer.childNodes[j].className != "") {
                            subId = treeViewContainer.childNodes[j].id;
                        }
                    }
                }
                var newData = { id: that.data.length + 1, subId: subId, "folderName": that.txtNewFolderName.value };
                addElement(newData);
                that.data.push(newData);
                that.saveLocalStorage();
            }, false);
        }
    };

    function initAddElement(source) {
        var i;
        var foldersCount = source.length;
        for (i = 0; i < foldersCount; i++) {
            addElement(source[i]);
            //this.treeViewContainer.innerHTML += "<li id='" + db.folders[i].id + "''>" + db.folders[i].folderName + "</li>";
        }
    }

    function addElement(elementData) {
        var li = document.createElement('li');
        li.id = elementData.id;
        li.innerHTML = elementData.folderName;
        li.addEventListener('click', function () {
            if (this.className === "") {
                if (treeViewContainer != null && treeViewContainer.childNodes != null) {
                    for (var j = 0; j < treeViewContainer.childNodes.length; j++) {
                        if (treeViewContainer.childNodes[j].className != "") {
                            treeViewContainer.childNodes[j].className = null;
                        }
                    }
                }
                this.className = 'selected';
            } else {
                this.className = null;
            }

        }, false);
        if (elementData.subId > 0) {
            var subElement = document.getElementById(elementData.subId);
            if (subElement != null) {
                if (subElement.childNodes.length === 1) {
                    var ul = document.createElement('ul');
                    ul.appendChild(li);
                    subElement.appendChild(ul);
                } else {
                    subElement.appendChild(li);
                }
            }
        } else {
            this.treeViewContainer.appendChild(li);
        }
    }

})();