/// <reference path="libs/require.js" />
/// <reference path="libs/sammy-0.7.4.js" />
/// <reference path="libs/jquery-2.0.3.js" />


(function () {

    require.config({
        paths: {
            jquery: "libs/jquery-2.0.3",
            sammy: "libs/sammy-0.7.4",
            controllers: "app/controller",
            "class": "libs/class",
            notify: "app/noty/jquery.noty",
            providers: "app/dataProvider",
            cryptoJS: "libs/sha1",
            httpRequester: "libs/http-requester",
            rsvp: "libs/rsvp.min",
            mustache: "libs/mustache"
        }
    });

    require(["jquery", "sammy", "controllers"], function ($, sammy, controllers) {
        
        var controller = controllers.get();
        
		var app = sammy("#main-content",function () {
			this.get("#/", function () {
			    if (!controller.loginCheck()) {
			        window.location = "#/login";
			    } else {
			        alert("logged");
			    }
			});

			this.get("#/login", function () {
			    controller.loadLogin();
			});

		    this.get("#/game", function() {
		        controller.loadGame();
		    });
		    
		    this.get("#/game/open", function () {
		        controller.loadOpenGames();
		    });
		    
		    this.get("#/game/my", function () {
		        controller.loadMyGame();
		    });
		    
		    this.get("#/game/in-progress", function () {
		        controller.loadGameInProgress();
		    });
		    
		    this.get("#/game/:id/join", function (id) {
		        controller.joinGame(id);
		    });

			this.get("#/item/:id", function (id) {
				alert("In item with id: " + this.params["id"]);
			});
		});
		app.run("#/");		
		
	});
}());