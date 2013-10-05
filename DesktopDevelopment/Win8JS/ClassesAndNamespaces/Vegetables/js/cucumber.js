/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="vegetable.js" />

WinJS.Namespace.defineWithParent(Plant,"Bio", {
    Cucumber: WinJS.Class.derive(Plant.Vegetable, function (length, color) {
        this.length = length;
        Plant.Vegetable.call(this, color, false);
    })
});
