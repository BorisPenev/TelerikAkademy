/// <reference path="vegetable.js" />
/// <reference path="cucumber.js" />
/// <reference path="mushroom-mixin.js" />

WinJS.Namespace.define("GMO", {
    CucumberGMO: WinJS.Class.mix(Plant.Cucumber, MushroomMixin)
});