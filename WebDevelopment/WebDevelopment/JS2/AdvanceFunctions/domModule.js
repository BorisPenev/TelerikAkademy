var domModule = (function () {
    var buffer = [, ];
    return {
        appendChild: function (element, selector) {
            var parent = document.querySelector(selector);
            parent.appendChild(element);
        },
        removeChild: function (element, selector) {
            var parent = element + ' > ' + selector;
            var children = Array.prototype.slice.call(document.querySelectorAll(parent));

            for (var i = 0; i < children.Length; i++) {
                children[i].parentElement.removeChild(child);
            }
        },
        addHandler: function (selector, event, action) {
            var parents = document.querySelectorAll(selector);
            for (var i = 0; i < parents.length; i++) {
                parents[i].addEventListener(event, action);
            }
        },
        appendToBuffer: function (selector, element) {
            var parent = Array.prototype.slice.call(document.querySelectorAll(selector));
            if (parent != null) {
                for (var i = 0; i < parent.length; i++) {
                    if (buffer[selector] == null) {
                        buffer[selector] = [];
                    }
                    if (buffer[selector].length < 100) {
                        //element.id = element.id + buffer[selector].length;
                        buffer[selector].push(element);
                    } else {
                        for (var j = 0; j < buffer[selector].length; j++) {
                            parent[i].appendChild(buffer[selector][j]);
                        }
                        buffer[selector] = [];
                    }
                }
            }
        }
    };
})();

if (typeof String.prototype.startsWith != 'function') {
    // see below for better implementation!
    String.prototype.startsWith = function (str) {
        return this.indexOf(str) == 0;
    };
}