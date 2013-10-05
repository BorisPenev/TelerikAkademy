/// <reference path="dataProvider.js" />
/// <reference path="ui.js" />
/// <reference path="class.js" />
/// <reference path="kendo.custom.min.js" />
/// <reference path="helpers.js" />

var BullsAndCows = BullsAndCows || {};

BullsAndCows.controller = (function () {
    
    
    var Controller = Class.create({
        init: function(dataProvider) {
            this.Provider = dataProvider;

            if (this.Provider.isLoggedIn()) {
                this.loadGame("#panelbar");
            } else {
                this.loadLogin("#kwindow");
            }
        },
        loadLogin: function (selector) {
            this.attachLoginEventHandlers(selector);

            var popupWindow = createKendoWindow("Login / Register", "Login").open();
            //popupWindow.data("kendopopupWindow");
        },
        loadGame: function () {
            this.attachGameEventHandlers("#kwindow");
            $("#organizer").show();
            
            panelBar.select(getItemByIndex(0));
            
            this.Provider.games.open().then(function (result) {
                var data = JSON.parse(result);
                for (var i = 0; i < data.length; i++) {
                    panelBar.append('<li id="' + data[i].id + '" class="k-link">' + data[i].title + '</li>', panelBar.select(getItemByIndex(1)));
                    //panelBar.append({
                    //    text: result[i].title
                    //}, panelBar.select(getItemByIndex(1)));
                }
                panelBar.expand(panelBar.select(getItemByIndex(0)));
            });

            this.Provider.games.active().then(function (result) {
                var data = JSON.parse(result);
                for (var i = 0; i < data.length; i++) {
                    panelBar.append('<li id="' + data[i].id + '" class="k-link">' + data[i].title + '</li>', panelBar.select(getItemByIndex(1)));
                }
            });
        },
        attachLoginEventHandlers: function (selector) {
            var self = this;
            var kwindow = $(selector);
            kwindow.on('click', '#btnLogin', function () {
                var username = $('#txtLoginUsername').val();
                var password = $('#txtLoginPassword').val();
                self.Provider.users.login(username, password).then(function () {
                    kwindow.data("kendoWindow").close();
                    self.loadGame("#panelbar");
                });
            });

            kwindow.on('click', '#btnRegister', function () {
                var username = $('#txtRegisterUsername').val();
                var nickname = $('#txtRegisterNickname').val();
                var password = $('#txtRegisterPassword').val();
                self.Provider.users.register(username, nickname, password).then(function () {
                    kwindow.data("kendoWindow").close();
                    self.loadGame("#panelbar");
                });
            });
        },
        attachGameEventHandlers: function (selector) {
            var self = this;
            var kwindow = $(selector);
            kwindow.on('click', '#btnJoinGame', function () {
                var password = $('#txtJoinGamePassword').val();
                var number = $('#txtJoinGameNumber').val();
                self.Provider.games.join(selectedGameId, password, number).then(function (result) {
                    var data = JSON.parse(result);
                    kwindow.data("kendoWindow").close();
                    var item = panelBar.getItemById(selectedGameId);
                }, function(error) {
                    alert(error.responseText);
                });
            });
            
            kwindow.on('click', '#btnCreateGame', function () {
                var title = $('#txtCreateGameName').val();
                var password = $('#txtCreateGamePassword').val();
                var number = $('#txtCreateGameNumber').val();
                self.Provider.games.create(title, password, number).then(function (result) {
                    var data = JSON.parse(result);
                    kwindow.data("kendoWindow").close();
                }, function (error) {
                    alert(error.responseText);
                });
            });

            $('#organizer').on('click', '#btnCreateNewGame', function () {
                createKendoWindow("Create New Game", "CreateGame").center().open();
            });
        }
    });

    
    
    return {
        get : function(dataProvider) { return new Controller(dataProvider); }
    };
}());