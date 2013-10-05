/// <reference path="vegetable.js" />
/// <reference path="tomato.js" />
/// <reference path="mushroom-mixin.js" />

WinJS.Namespace.defineWithParent(Plant, "GMO", {
    TomatoGmo: WinJS.Class.mix(Plant.Bio.Tomato, MushroomMixin)
});