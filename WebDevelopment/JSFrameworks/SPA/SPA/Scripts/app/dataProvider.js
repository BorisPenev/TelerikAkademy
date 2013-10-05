/// <reference path="http-requester.js" />
/// <reference path="class.js" />
/// <reference path="q.js" />
/// <reference path="sha1.js" />

define(["class"], function (Class) {
	var nickname = localStorage.getItem("nickname");
	var sessionKey = localStorage.getItem("sessionKey");    
	

    var BaseProvider = Class.create({
        init: function(serviceUrl) {
            this.serviceUrl = serviceUrl;
            //this.user = new UserProvider(this.serviceUrl);
            //this.game = new GameProvider(this.serviceUrl);
            //this.message = new MessagesProvider(this.serviceUrl);
            //this.battle = new BattleProvider(this.serviceUrl);
        },
        isUserLoggedIn: function() {
            var isLoggedIn = nickname != null && sessionKey != null;
            return isLoggedIn;
        },
        clearSession: function() {
            localStorage.removeItem("nickname");
            localStorage.removeItem("sessionKey");
            nickname = "";
            sessionKey = "";
        },
        saveSession: function(userData) {
            localStorage.setItem("nickname", userData.principal_id);
            localStorage.setItem("sessionKey", userData.access_token);
            nickname = userData.nickname;
            sessionKey = userData.sessionKey;
        },
        nickname: function() {
            return nickname.escape();
        }
    });
	//var UserProvider = Class.create({
	//	init: function (serviceUrl) {
	//		this.serviceUrl = serviceUrl + "user/";
	//	},
	//	register: function (username, nickname, password) {
	//	    var url = this.serviceUrl + "register";
	//	    var userData = {
	//	        username: username,
	//	        nickname: nickname,
	//	        authCode: CryptoJS.SHA1(password).toString()
	//	    };
		    
	//	    return httpRequester.postJSON(url, userData).then(function (result) {
	//	        saveSession(result);
	//	    }, function (error) {
	//	        console.log(error.responseText);
	//	    });
	//	},
	//	login: function (username, password) {
	//		var url = this.serviceUrl + "login";
	//		var userData = {
	//			username: username,
	//			authCode: CryptoJS.SHA1(password).toString()
	//		};

	//		return httpRequester.postJSON(url, userData).then(function (result) {
	//		    saveSession(result);
	//		    return result.sessionKey;
	//		}, function (error) {
	//		    console.log(error.responseText);
	//		    return error.responseText;
	//		});
	//	},
	//	logout: function () {
	//	    var url = this.serviceUrl + "logout/" + sessionKey;
		    
	//		return httpRequester.getJSON(url).then(function() {
	//		    clearSession();
	//		});
	//	},
	//	scores: function () {
	//	    return httpRequester.getJSON(this.serviceUrl + "scores/" + sessionKey);
	//	}
	//});
	//var GameProvider = Class.create({
	//    init: function (serviceUrl) {
	//        this.serviceUrl = serviceUrl + "game/";
	//    },
	//    create: function (title, password) {
	//        var hash = CryptoJS.SHA1(password).toString();
	//        var url = this.serviceUrl + "create/" + sessionKey;
	//        var data = {
	//            title: title,
	//            password: hash
	//        };
	        
	//        return httpRequester.postJSON(url, data);
	//    },
	//    join: function (gameId, password) {
	//        var hash = CryptoJS.SHA1(password).toString();
	//        var url = this.serviceUrl + "join/" + sessionKey;
	//        var data = {
	//            id: gameId,
	//            password: hash
	//        };
	        
	//        return httpRequester.postJSON(url, data);
	//    },
	//    start: function (gameId) {
	//        var url = this.serviceUrl + "/" + gameId + "/start/" + sessionKey;
	        
	//        return httpRequester.getJSON(url);
	//    },
	//    field: function (gameId) {
	//        var url = this.serviceUrl + "/" + gameId + "/field/" + sessionKey;
	        
	//        return httpRequester.getJSON(url);
	//    },
	//    open: function () {
	//        var url = this.serviceUrl + "open/" + sessionKey;
	        
	//        return httpRequester.getJSON(url);
	//    },
	//    active: function () {
	//        var url = this.serviceUrl + "my-active/" + sessionKey;
	        
	//        return httpRequester.getJSON(url);
	//    }
	//});

	//var BattleProvider = Class.create({
	//    init: function (serviceUrl) {
	//        this.serviceUrl = serviceUrl + "battle/";
	//    },

	//    move: function (gameId, unitId, row, col) {
	//        var url = this.serviceUrl + "/" + gameId + "/move/" + sessionKey;
	//        var data = {
	//            unitId: unitId,
	//            position: { x: row, y: col }
	//        };

	//        return httpRequester.postJSON(url, data);
	//    },
	//    attack: function (gameId, unitId, row, col) {
	//        var url = this.serviceUrl + "/" + gameId + "/attack/" + sessionKey;
	//        var data = {
	//            unitId: unitId,
	//            position: { x: row, y: col }
	//        };

	//        return httpRequester.postJSON(url, data);
	//    },
	//    defend: function (gameId, unitId) {
	//        var url = this.serviceUrl + "/" + gameId + "/defend/" + sessionKey;
	//        var data = unitId;

	//        return httpRequester.postJSON(url, data);
	//    }
	//});

	//var MessagesProvider = Class.create({
	//    init: function (serviceUrl) {
	//        this.serviceUrl = serviceUrl + "messages/";
	//    },
	//    all: function () {
	//        var url = this.serviceUrl + "all/" + sessionKey;
	        
	//        return httpRequester.getJSON(url);
	//    },
	//    unread: function () {
	//        var url = this.serviceUrl + "unread/" + sessionKey;
	        
	//        return httpRequester.getJSON(url);
	//    }
	//});
	return {
		get: function (url) {
			return new BaseProvider(url);
		}
	};
});