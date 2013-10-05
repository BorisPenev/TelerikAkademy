/// <reference path="class.js" />
/// <reference path="dataProvider.js" />
/// <reference path="jquery-2.0.2.min.js" />
/// <reference path="helpers.js" />
/// <reference path="../src/everlive.all.js" />
/// <reference path="notify.js" />
define(["jquery", "class", "providers"], function ($, Class, providers) {
    var rootUrl = "http://localhost:22954/api/";
    var el;
    var Controller = Class.create({
        init: function (everlive) {
            this.provider = providers.get(rootUrl);
            el = everlive;
        },
        loginCheck: function() {
            return this.provider.isUserLoggedIn();
        },
        loadLogin: function () {
            var self = this;
            $("#main-content").on('click', '#btnLogin', function () {
                var username = $('#txtLoginUsername').val();
                var password = $('#txtLoginPassword').val();
                Everlive.$.Users.login(username, password,
                    function (data) {
                        self.provider.saveSession(data.result);
                        window.location = "#/posts";
                        //var data = Everlive.$.data('Post');
                        //data.create({ 'Title': 'Harper Lee' },
                        //    function (data) {
                        //        alert(JSON.stringify(data));
                        //    },
                        //    function (error) {
                        //        alert(JSON.stringify(error));
                        //    }
                        //);
                    },
                    function (error) {
                        console.log(JSON.stringify(error));
                    }
                );
                //return self.provider.user.login(username, password).then(function (result) {
                //    if (result.Message) {
                //        console.log(result.Message);
                //    } else {
                //        window.location = "#/game";
                //    }
                //});
            });
            $("#main-content").on('click', '#btnRegister', function () {
                var username = $('#txtRegisterUsername').val();
                var password = $('#txtRegisterPassword').val();
                var email = $('#txtEmailAddress').val();
                Everlive.$.Users.register(username,password,{
                        Email: email
                    },
                    function (data) {
                        console.log(JSON.stringify(data));
                    },
                    function (error) {
                        console.log(JSON.stringify(error));
                    }
                );
                //self.provider.user.register(username, nickname, password).then(function (result) {
                //    self.loadGame("#panelbar");
                //});
            });
            $("#main-content").load('/UserControls/Login.html');
        },
        getAllPosts: function() {
            var data = Everlive.$.data('Post');
            data.get()
                .then(function(response) {
                    $.get('/UserControls/Posts.html')
                        .success(function(html) {
                            $('#main-content').html(html);
                            var grdGameList = document.getElementById("grdPostList");
                            response.result.forEach(function(item) {
                                showPost(item, grdGameList);
                            });
                        });
                }, function(error) {
                    alert(JSON.stringify(error));
                });
        },
        loadPost: function(postId) {
            var data = Everlive.$.data('Post');
            data.getById(postId).then(function(postData) {
                $.get('/UserControls/Posts.html')
                    .success(function(html) {
                        $('#main-content').html(html);
                        var grdGameList = document.getElementById("grdPostList");
                        showPost(postData.result, grdGameList);
                    });

            }, function(error) {
                alert(JSON.stringify(error));
            });
        },
        loadGame: function () {
            var pnlGame = $("#main-content");
            pnlGame.load('/UserControls/Game.html');
            //this.attachGameEventHandlers();
            //$("#organizer").show();

            //this.loadOpenGames();

            //this.provider.game.active().then(function(result) {
            //    for (var i = 0; i < result.length; i++) {
            //        games.push(result[i]);
            //        if (result[i].status === "full") {
            //            appendGame(getItemById('my-games'), result[i], "lbMyGames");
            //        } else {
            //            appendGame(getItemById('in-progress'), result[i], "lbInProgress");
            //        }
            //    }
            //});

            //this.loadMessages();
        },
        loadOpenGames: function () {
            var self = this;
            var pnlGameList = $("#main-content");
            pnlGameList.load('/UserControls/GameList.html');

            this.provider.game.open().then(function (result) {
                var items = [];
                
                for (var i = 0; i < result.length; i++) {
                    items.push(result[i]);
                }

                var gameItemTemplate = document.getElementById("game-list-item-template").innerHTML;// $(pnlGameList[0]).find("#game-list-item-template")[0].innerHTML;
                var grdGameList = document.getElementById("grdGameList");
                grdGameList.innerHTML = Mustache.render(gameItemTemplate, items);
            }, function (error) {
                console.log(error);
            });
        },
        loadMyGame: function () {
            var self = this;
            var pnlGameList = $("#main-content");
            pnlGameList.load('/UserControls/GameList.html');
            
            this.provider.game.active().then(function (result) {
                var items = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].status === "full") {
                        items.push(result[i]);
                    }
                }

                var gameItemTemplate = document.getElementById("game-list-item-template").innerHTML;// $(pnlGameList[0]).find("#game-list-item-template")[0].innerHTML;
                var grdGameList = document.getElementById("grdGameList");
                grdGameList.innerHTML = Mustache.render(gameItemTemplate, items);
            }, function(error) {
                console.log(error);
            });
        },
        loadGameInProgress: function () {
            var self = this;
            var pnlGameList = $("#main-content");
            pnlGameList.load('/UserControls/GameList.html');

            this.provider.game.active().then(function (result) {
                var items = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].status !== "full") {
                        items.push(result[i]);
                    }
                }

                var gameItemTemplate = document.getElementById("game-list-item-template").innerHTML;// $(pnlGameList[0]).find("#game-list-item-template")[0].innerHTML;
                var grdGameList = document.getElementById("grdGameList");
                grdGameList.innerHTML = Mustache.render(gameItemTemplate, items);
            }, function (error) {
                console.log(error);
            });
        },
        //loadMessages: function() {
        //    var self = this;
        //    setInterval(function() {
        //        console.log('message');
        //        self.provider.message.unread().then(function(result) {
        //            for (var i = 0; i < result.length; i++) {
        //                var content = 'text: ' + result[i].text +
        //                    '<br />gameId: ' + result[i].gameId +
        //                    '<br />type: ' + result[i].type +
        //                    '<br />state: ' + result[i].state;
        //                if (result[i].type === "guess-made") {
        //                    var game = findGameById(games, result[i].gameId);
        //                    console.log(game);
        //                    if (game !== undefined && game.button !== undefined) {
        //                        game.button.removeAttr("disabled");
        //                        game.button.css('background', '#61FF73');
        //                    }
        //                    notify.create(content, result[i].gameId, result[i].type, result[i].state, null, 'information');
        //                } else if (result[i].type === "game-joined" || result[i].type === "game-started") {
        //                    var item = getItemById(result[i].gameId);
        //                    $(item).remove();
        //                    //item.parent().remove(item);
        //                    item.attr('class', '');
        //                    getItemById('lbInProgress').append(item);
        //                    notify.create(content, result[i].gameId, result[i].type, result[i].state, null, 'alert');
        //                } else if (result[i].type === "game-move") {
        //                    if (selectedGameId === result[i].gameId) {
        //                        controller.provider.game.field(result[i].gameId).then(function(fields) {
        //                            createActiveGameWindow(fields, true);
        //                            notify.create(content, fields.gameId, null, null, null, 'success');
        //                        });
        //                    }
        //                }
        //            }
        //        });
        //    }, 2000);
        //},
        //attachGameEventHandlers: function() {
        //    var self = this;
        //    var kwindow = $("body");

        //    kwindow.on('click', '#btnJoinGame', function(e) {
        //        var password = $($(e.target).parent()).find('#txtJoinGamePassword').val();
        //        self.provider.game.join(selectedGameId, password).then(function(result) {
        //            $($(e.target).parent()).dialog("close");
        //            game = findGameById(games, selectedGameId);
        //            game.status = "full";
        //            var item = getItemById(game.id);
        //            $(item).remove();
        //            //(item.parent()).remove(item);
        //            item.attr('tag', game.creator);
        //            item.attr('class', 'full-waiting');
        //            getItemById('my-games').append(item);
        //        }, function(error) {
        //            alert(error.responseText);
        //        });
        //    });

        //    kwindow.on('click', '#btnCreateGame', function(e) {
        //        var title = $($(e.target).parent()).find('#txtCreateGameName').val();
        //        var password = $($(e.target).parent()).find('#txtCreateGamePassword').val();
        //        self.provider.game.create(title.escape(), password).then(function(result) {
        //            $($(e.target).parent()).dialog("close");
        //            var data = {
        //                message: "new-game",
        //                user: self.provider.nickname()
        //            };
        //            pubnubPublish(data);
        //        }, function(error) {
        //            alert(error.responseText);
        //        });
        //    });

        //    //kwindow.on('click', '#btnMakeTurn', function (e) {
        //    //    var gameId = $($(e.target).parent()).attr('tag');
        //    //    var number = $($(e.target).parent()).find('#txtMakeTurn').val();
        //    //    controller.provider.guess.make(gameId, number).then(function (result) {
        //    //        var game = findGameById(games, gameId);
        //    //        game.button = $(e.target);
        //    //        game.button.attr("disabled", "disabled");
        //    //        game.button.css('background', '#FFBABA');
        //    //        controller.provider.game.field(game.id).then(function (result) {
        //    //            var turn;
        //    //            if (result.red === controller.provider.nickname()) {
        //    //                turn = result.redGuesses[result.redGuesses.length - 1];
        //    //            } else {
        //    //                turn = result.blueGuesses[result.blueGuesses.length - 1];
        //    //            }
        //    //            game.grdListTurns.prepend('<li>number: ' + turn.number + ' , cows: ' + turn.cows + ', bulls: ' + turn.bulls + '</li>');
        //    //        });

        //    //    }, function (error) {
        //    //        alert(error.responseText);
        //    //    });
        //    //});

        //    $('#organizer').on('click', '#btnCreateNewGame', function() {
        //        createNewGameWindow();
        //    });

        //    $('#organizer').on('click', '#btnReadAllMessages', function() {
        //        self.provider.message.all().then(function(result) {
        //            for (var i = 0; i < result.length; i++) {
        //                var content = 'text: ' + result[i].text +
        //                    '<br />gameId: ' + result[i].gameId +
        //                    '<br />type: ' + result[i].type +
        //                    '<br />state: ' + result[i].state;
        //                notify.create(content, result[i].gameId, result[i].type, result[i].state);
        //            }
        //        });
        //    });

        //    panelBar.on('click', 'ul > li[id]', function(e) {
        //        e.preventDefault();
        //        selectedGameId = $(e.target)[0].id;
        //        if (!isNaN(selectedGameId)) {
        //            var game = findGameById(games, selectedGameId);
        //            if (game.status === "open") {
        //                createJoinGameWindow(game.title.escape(), game.id);
        //            } else if (game.status === "full" && game.creator.escape() === self.provider.nickname()) {
        //                var content = 'Start the game.';
        //                notify.create(content, game.id, '', game.status, e.target);
        //            } else if (game.status === "in-progress") {
        //                self.startGame(game);
        //            }
        //        }
        //    });

        //    panelBar.on('click', 'h3', function(e) {
        //        if ($(e.target).text().trim() === "Scores") {
        //            controller.provider.user.scores().then(function(result) {
        //                for (var i = 0; i < result.length; i++) {
        //                    var score = findGameById(scores, i);
        //                    if (score === undefined) {
        //                        result[i].id = i;
        //                        scores.push(result[i]);
        //                        appendScores(getItemById('scores'), result[i], "lbScores");
        //                    }
        //                }
        //            });
        //        }
        //    });
        //},
        //startGame: function(game) {
        //    controller.provider.game.field(game.id).then(function(result) {
        //        selectedGameId = result.gameId;
        //        createActiveGameWindow(result, false);
        //    });
        //}
    });
    var showPost = function (item, container) {
        var postTemplate = Mustache.compile(document.getElementById("post-list-template").innerHTML);
        
        var query = new Everlive.Query();
        query.where().isin('Id', item.Tags);
        Everlive.$.data('Tag').get(query).then(function(tagsResult) {
            item.tags = tagsResult.result;
            container.innerHTML += postTemplate(item);
        }, function(error) {
            alert(JSON.stringify(error));
        });
    };
    
    return {
        get: function (everlive) {
            return new Controller(everlive);
        }
    };
});