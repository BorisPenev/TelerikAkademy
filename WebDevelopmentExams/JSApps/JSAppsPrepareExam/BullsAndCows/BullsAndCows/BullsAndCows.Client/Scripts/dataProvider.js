/// <reference path="class.js" />
/// <reference path="http-requester.js" />

var BullsAndCows = BullsAndCows || {};

BullsAndCows.Providers = (function () {

    var saveSession = function (result) {
        //var data = JSON.parse(result);
        localStorage.setItem("sessionKey", result.sessionKey);
        sessionKey = result.sessionKey;
    };
    
    var loadSession = function () {
        sessionKey = localStorage.getItem("sessionKey");
        return sessionKey;
    };

    var clearSession = function() {
        localStorage.removeItem("sessionKey");
        sessionKey = null;
    };
    
    var sessionKey = loadSession();
    
    var BaseProvider = Class.create({
        init: function (serviceUrl) {
            this.serviceUrl = serviceUrl;
            this.users = new UserProvider(serviceUrl + "user/");
            this.games = new GameProvider(serviceUrl + "game/");
        },

        isLoggedIn: function() {
            var login = loadSession();
            if (login !== "" && login !== null && login !== undefined) {
                return true;
            }
            return false;
        }
    });

    var UserProvider = Class.create({
        init: function (serviceUrl) {
            this.serviceUrl = serviceUrl;
        },

        register: function (username, nickname, password) {
            var hash = CryptoJS.SHA1(password).toString();

            return httpRequester.postJSON(this.serviceUrl + "register", {
                username: username,
                nickname: nickname,
                authCode: hash
            }).then(function (result) {
                saveSession(result);
            }, function (error) {
                console.log(error.responseText);
            });
        },

        login: function (username, password) {
            var hash = CryptoJS.SHA1(password).toString();

            return httpRequester.postJSON(this.serviceUrl + "login", {
                username: username,
                authCode: hash
            }).then(function (result) {
                saveSession(result);
            }, function (error) {
                console.log(error.responseText);
            });
        },

        logout: function () {
            httpRequester.getJSON(this.serviceUrl + "logout/" + sessionKey).
                then(function () {
                    clearSession();
                });
        },

        scores: function () {
        }
    });

    var GameProvider = Class.create({
        init: function(serviceUrl) {
            this.serviceUrl = serviceUrl;
        },
        create: function (title, password, number) {
            var hash = CryptoJS.SHA1(password).toString();
            
            var url = this.serviceUrl + "create/" + sessionKey;
            var data = {
                title: title,
                password: hash,
                number: number
            };
            return httpRequester.postJSON(url, data);
        },

        join: function (gameId, password, number) {
            var hash = CryptoJS.SHA1(password).toString();
            
            var url = this.serviceUrl + "join/" + sessionKey;
            var data = {
                gameId: gameId,
                password: hash,
                number: number
            };
            return httpRequester.postJSON(url, data);
        },

        start: function () {

        },
        open: function() {
            return httpRequester.getJSON(this.serviceUrl + "open/" + sessionKey);
        },
        active: function() {
            return httpRequester.getJSON(this.serviceUrl + "my-active/" + sessionKey);
        },
        state: function () {

        }
    });
    
    var GuessProvider = Class.create({

        intialize: function () {

        },

        make: function () {

        }

    });

    var MessagesProvider = Class.create({

        initilize: function () {

        },

        unread: function () {

        },

        all: function () {

        }

    });
    
    return {
        getProvider: function(url) { return new BaseProvider(url); },
    };
}());

//var localProvider = BullsAndCows.Providers.getProvider("http://localhost:40643/api/");
////localProvider.users.register("Kiro2", "Kiro Motikata", "teslata");
//localProvider.users.login("Kiro2", "some text");