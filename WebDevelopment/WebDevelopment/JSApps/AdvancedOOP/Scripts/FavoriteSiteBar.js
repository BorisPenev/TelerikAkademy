var Url = Class.create({
    init: function (title, url) {
        this.title = title;
        this.url = url;
    }
});
var Folder = Class.create({
    init: function (title, setOfUrls) {
        this.title = title;
        this.setOfUrls = setOfUrls;
    }
});
var FavoriteSiteBar = Class.create({
    init: function(setOfUrls, setOfFolders) {
        this.setOfUrls = setOfUrls;
        this.setOfFolders = setOfFolders;
        this.container = document.createElement("div");
        this.count = 0;
    },
    display: function() {
        this.container.id = "favoriteSiteBarContainer";
        document.body.appendChild(this.container);
        if (this.count == 0) {
            var urlsHeader = document.createElement("h3");
            urlsHeader.innerHTML = "Favorite Sites:";
            this.container.appendChild(urlsHeader);
            this._displaySetOfUrls();
            var foldersHeader = document.createElement("h3");
            foldersHeader.innerHTML = "Favorite Groups Of Sites:";
            this.container.appendChild(foldersHeader);
            this._displaySetOfFolders();
            this.count += 1;
        }
    },
    _displaySetOfUrls: function() {
        var setOfUrls = this.setOfUrls;
        for (var i = 0; i < setOfUrls.length; i++) {
            var url = document.createElement("a");
            url.innerHTML = setOfUrls[i].title;
            url.href = setOfUrls[i].url;
            url.target = "_blank";
            this.container.appendChild(url);
        }
    },
    _displaySetOfFolders: function() {
        var folders = this.setOfFolders;
        for (var i = 0; i < folders.length; i++) {
            var paragraph = document.createElement("p");
            paragraph.id = folders[i].title;
            paragraph.innerHTML = folders[i].title;
            paragraph.className = "FavoriteSiteBarParagraph";
            paragraph.onclick = function(ev) {
                if (ev.target.childElementCount == 0) {
                    var index;
                    for (var l = 0; l < folders.length; l++) {
                        if (ev.target.id == folders[l].title) {
                            index = l;
                            break;
                        }
                    }
                    for (var j = 0; j < folders[index].setOfUrls.length; j++) {
                        var url = document.createElement("a");
                        url.href = folders[index].setOfUrls[j].url;
                        url.innerHTML = folders[index].setOfUrls[j].title;
                        url.target = "_blank";
                        ev.target.appendChild(document.createElement("br"));
                        ev.target.appendChild(url);
                    }
                } else {
                    ev.target.innerHTML = ev.target.id;
                }
            };
            this.container.appendChild(paragraph);
        }
    }
});