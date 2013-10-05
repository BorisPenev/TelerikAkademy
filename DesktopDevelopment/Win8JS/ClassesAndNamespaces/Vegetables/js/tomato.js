/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="vegetable.js" />

WinJS.Namespace.defineWithParent(Plant, "Bio", {
    Tomato: WinJS.Class.derive(Plant.Vegetable, function (radius, color) {
        this.radius = radius;
        Plant.Vegetable.call(this, color, true);
    })
});