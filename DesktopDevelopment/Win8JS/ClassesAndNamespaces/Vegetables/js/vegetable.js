/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />

WinJS.Namespace.define("Plant", {
    Vegetable: WinJS.Class.define(function(color, isEaten) {
        this.color = color;
        this.isEaten = isEaten;
    },
        {
            descriptionString: {
                get: function() {
                    return "color: " + this.color + ", isEaten: " + this.isEaten;
                }
            }
        })
});
    