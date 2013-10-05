/// <reference path="controller.js" />
/// <reference path="data-Provider.js" />

$(document).ready(function () {
    selectedGameId = 0;
    
    panelBar = $("#panelbar").kendoPanelBar({
        expandMode: "single",
        select: onSelect,
    }).data("kendoPanelBar");

    function onSelect(e) {
        selectedGameId = $(e.item)[0].id;
        if (!isNaN(selectedGameId)) {
            createKendoWindow("Join in Game Id: " + selectedGameId, "JoinGame").center().open();
        }
    };
    
    var serviceRoot = "http://localhost:40643/api/";

    var localProvider = BullsAndCows.Providers.getProvider(serviceRoot);

    //localProvider.users.login('saykor', '123456').then(function() {
        var accessController = BullsAndCows.controller.get(localProvider);
    //});    
});