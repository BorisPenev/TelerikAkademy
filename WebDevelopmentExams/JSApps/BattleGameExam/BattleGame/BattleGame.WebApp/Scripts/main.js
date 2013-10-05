/// <reference path="controller.js" />
/// <reference path="pqgrid.min.js" />
/// <reference path="dataProvider.js" />
/// <reference path="kendo.custom.min.js" />

$(document).ready(function() {
    selectedGameId = 0;

    games = [];
    scores = [];

    redUnits = [];
    blueUnits = [];

    panelBar = $("#panelbar").accordion({
        heightStyle: "fill",
        header: "h3",
        active: 0,
        collapsible: true
    });
    
    $("#organizer").resizable({
        minHeight: 140,
        minWidth: 200,
        resize: function () {
            panelBar.accordion("refresh");
        }
    });
    
    controller = controllers.get();
  
});