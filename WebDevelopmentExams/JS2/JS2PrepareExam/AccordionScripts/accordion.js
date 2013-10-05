var controls = (function() {

    var db = [];

    var Accordion = function (selector) {
        this.accordionContainer = document.querySelector(selector);
        this.accordionContainer.addEventListener('click', function (ev) {
            if (ev.target.children.length !== 1) {
                
                if (!ev) {
                    ev = window.event;
                }

                ev.stopPropagation();
                ev.preventDefault();

                if (ev.target instanceof HTMLSpanElement) {
                    var subList = ev.target.nextElementSibling;
                   
                    var parentElementId = ev.target.parentElement.id;
                    var elId = parentElementId.substr(4, parentElementId.length);
                    
                    var parent = db.filter(function (item) { return item.Id === parseInt(elId); });
                    var allParents = db.reduce(function (parents, item) {
                        if (item.SubId === parent[0].SubId) {
                            return parents.concat(item);
                        } else {
                            return parents;
                        }
                    }, []);

                    for (var i = 0; i < allParents.length; i++) {
                        if (allParents[i].Id != parseInt(elId)) {
                            var element = document.getElementById("item" + allParents[i].Id);
                            if (element.children.length > 1) {
                                element.children[1].style.display = "none";
                            }
                        }
                    }

                    if (subList !== null && subList !== undefined) {
                        if (subList.style.display === "") {
                            subList.style.display = "none";
                        } else {
                            subList.style.display = "";
                        }
                        
                    }
                }
            }
        }, false);
    };

    Accordion.prototype = {
        add: function (title) {
            var item = new Item(title, db.length + 1, 0);
            db.push(item);
            return item;
        },
        render: function () {
            
            while (this.accordionContainer.firstChild) {
                this.accordionContainer.removeChild(this.accordionContainer.firstChild);
            }
            
            //db.sort(function (a, b) {
            //    return a.SubId - b.SubId;
            //});
            
            for (var i = 0; i < db.length; i++) {
                var li = document.createElement('li');
                li.id = "item" + (i + 1);
                li.innerHTML = "<span>" + db[i].Title + " subId " + db[i].SubId + "</span>";
                if (db[i].SubId === 0) {
                    this.accordionContainer.appendChild(li);
                } else {
                    var ul;
                    var parentId = "item" + db[i].SubId;
                    var parent = this.accordionContainer.querySelector('#' + parentId);
                    if (parent.children.length === 1) {
                        ul = document.createElement('ul');
                        parent.appendChild(ul);
                    } else {
                        ul = parent.children[1];
                    }
                    ul.style.display = "none";
                    ul.appendChild(li);
                    
                }
            }
        },
        serialize: function () {
            return db;
        }
    };

    var Item = function(title, id, subId) {
        this.Id = id;
        this.SubId = subId;
        this.Title = title;
    };

    Item.prototype = {
        add: function (title) {
            var item = new Item(title, db.length + 1, this.Id);
            db.push(item);
            return item;
        },
    };

    return {
        getAccordion: function(selector) {
            return new Accordion(selector);
        },
        buildAccordion: function (selector, deserializedData) {
            db = deserializedData;
            return new Accordion(selector);
        }
    };
})();