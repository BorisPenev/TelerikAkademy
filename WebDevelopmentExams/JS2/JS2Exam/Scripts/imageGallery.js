var controls = (function() {
    var pnlPopup = document.getElementById('pnlPopup');
    var db = [];

    //Class: ImageGallery
    var ImageGallery = function (selector) {
        this.imageGalleryContainer = document.querySelector(selector);
        if (!this.imageGalleryContainer.addEventListener) {
            this.imageGalleryContainer.attachEvent("onclick", imageGalleryClickHandler);
        }
        else {
            this.imageGalleryContainer.addEventListener("click", imageGalleryClickHandler, false);
        }
        this.isAscendingOrder = true;
    };

    ImageGallery.prototype = {
        addImage: function (title, imgSrc) {
            var item = new ImageItem(db.length + 1, 0, title, imgSrc);
            db.push(item);
            return item;
        },
        addAlbum: function (title) {
            var item = new ImageItem(db.length + 1, 0, title, null);
            db.push(item);
            return item;
        },
        render: function () {
            while (this.imageGalleryContainer.firstChild) {
                this.imageGalleryContainer.removeChild(this.imageGalleryContainer.firstChild);
            }

            var newAlbum = createNewAlbum(0, this.isAscendingOrder);
            this.imageGalleryContainer.appendChild(newAlbum);
        },
        getImageGalleryData: function () {
            return db;
        },
        orderByAscending: function () {
            this.isAscendingOrder = !this.isAscendingOrder;
            this.render();
        }
    };
    
    //Class: ImageItem
    var ImageItem = function (id, subId, title, imgSrc) {
        this.Id = id;
        this.SubId = subId;
        this.Title = title;
        this.ImgSrc = imgSrc;
    };

    ImageItem.prototype = {
        addImage: function (title, imgSrc) {
            var item = new ImageItem(db.length + 1, this.Id, title, imgSrc);
            db.push(item);
            return item;
        },
        addAlbum: function (title) {
            var item = new ImageItem(db.length + 1, this.Id, title, null);
            db.push(item);
            return item;
        }
    };
    
    //Functions
    

    function createNewAlbum(currentItem, orderByAscending) {
        var currentSubId = 0;
        var newAlbumTitle;
        if (typeof currentItem === 'object') {
            currentSubId = currentItem.Id;
            newAlbumTitle = document.createElement('p');
            newAlbumTitle.innerHTML = currentItem.Title.escape();
        }
        
        var albumUl = document.createElement('ul');
        albumUl.id = "item" + currentSubId;

        var itemsByCurrentId = db.reduce(function (parents, item) {
            if (item.SubId === currentSubId) {
                return parents.concat(item);
            } else {
                return parents;
            }
        }, []);

        if (orderByAscending) {
            itemsByCurrentId.sort(function(a, b) {
                return a.Title - b.Title;
            });
        } else {
            itemsByCurrentId.reverse(function (a, b) {
                return a.Title - b.Title;
            });
        }
        
        var rootLi = document.createElement('li');
        if (newAlbumTitle !== undefined) {
            rootLi.appendChild(newAlbumTitle);
        }
        var newAlbumContainer = document.createElement('div');
        newAlbumContainer.className = "newAlbum";
        rootLi.appendChild(newAlbumContainer);
        
        var itemContainer = document.createElement('div');
        itemContainer.className = "container";
        
        newAlbumContainer.appendChild(itemContainer);
        albumUl.appendChild(rootLi);
        for (var i = 0; i < itemsByCurrentId.length; i++) {
            var div = document.createElement('div');
            div.className = "imageItem";
            div.id = "item" + itemsByCurrentId[i].Id;
            div.innerHTML = "<p>" + itemsByCurrentId[i].Title.escape() + "</p>";
            if (itemsByCurrentId[i].SubId === currentSubId) {
                if (itemsByCurrentId[i].ImgSrc !== null) {
                    div.innerHTML += "<img src='" + itemsByCurrentId[i].ImgSrc + "'>";
                    itemContainer.appendChild(div);
                } else {
                    var newAlbum = createNewAlbum(itemsByCurrentId[i], orderByAscending);
                    newAlbumContainer.appendChild(newAlbum);
                }
            }
        }
        return albumUl;
    }
    
    //enable nextElementSibling in IE8
    function nextElementSibling(el) {
        do {
            el = el.nextSibling;
        } while (el && el.nodeType !== 1);
        return el;
    }

    //Event Handlers
    function imageGalleryClickHandler(ev) {
        var target = initEvents(ev);

        //show-collapse
        if (target instanceof HTMLParagraphElement) {
            var subList = target.nextElementSibling || nextElementSibling(target);
            if (subList !== undefined && subList !== null) {
                if (subList.style.display === "") {
                    subList.style.display = "none";
                } else {
                    subList.style.display = "";
                }
            }
        }

        //popup bigger image
        if (target instanceof HTMLImageElement) {
            while (pnlPopup.firstChild) {
                pnlPopup.removeChild(pnlPopup.firstChild);
            }

            var img = document.createElement('img');
            img.src = target.src;
            img.width = target.width * 2;
            img.height = target.height * 2;
            pnlPopup.appendChild(img);

            OpenPopupWindow();
        }
    }
    
    return {
        getImageGallery: function (selector) {
            return new ImageGallery(selector);
        },
        buildImageGallery: function (selector, data) {
            db = data;
            return new ImageGallery(selector);
        }
    };
})();