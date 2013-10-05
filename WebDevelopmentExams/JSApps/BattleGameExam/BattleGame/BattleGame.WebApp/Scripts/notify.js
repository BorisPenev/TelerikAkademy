/// <reference path="main.js" />
/// <reference path="dataProvider.js" />
/// <reference path="controller.js" />
/// <reference path="noty/jquery.noty.js" />
var notify = (function () {

    var create = function (title, gameId, type, state, panelItem, nofyType) {
        //$.noty.returns = 'id'; // BC for old api

        var buttons;
        var timeout = false;
        if (nofyType === 'success') {
            timeout = 2000;
        }

        if (nofyType === undefined || nofyType === 'alert') {
            nofyType = 'alert';
            buttons = [
                {
                    type: 'btn btn-primary',
                    text: 'Ok',
                    click: function($noty) {
                        var game = findGameById(games, gameId);
                        if (game === undefined) {
                            game = {
                                id: gameId,
                                title: title.substring(title.indexOf('joined your game ') + 17, title.indexOf('<br />')),
                                creatorNickname: controller.provider.nickname,
                                status: "in-progress"
                            };
                            games.push(game);
                        }
                        if (type === "game-started" || game.status == "in-progress") {
                            noty({ force: true, text: 'Have fun :)', type: 'success', layout: 'topRight', timeout: 1000 });
                            controller.startGame(game);
                        } else {
                            if (type === "game-joined" || game.status === "full") {
                                controller.provider.game.start(gameId).then(function(result) {
                                    noty({ force: true, text: 'Have fun :)', type: 'success', layout: 'topRight', timeout: 1000 });
                                    controller.startGame(game);
                                    if (panelItem !== undefined && panelItem !== null) {
                                        //var item = getItemById('21');
                                        //item.parent().remove(item);
                                        //getItemById('in-progress').append(item);
                                    } else {
                                        appendGame(getItemById('in-progress'), game);
                                    }
                                });
                            } else if (type === "guess-made") {

                            }
                        }
                        // this = button element
                        // $noty = $noty element
                        $noty.close();
                        //noty({ force: true, text: 'You clicked "Ok" button', type: 'success', layout: 'topRight' });
                    }
                },
                {
                    type: 'btn btn-danger',
                    text: 'Cancel',
                    click: function($noty) {
                        $noty.close();
                        var game = findGameById(games, gameId);
                        if (game === undefined) {
                            game = {
                                id: gameId,
                                title: title.substring(title.indexOf('joined your game ') + 17, title.indexOf('<br />')),
                                creatorNickname: controller.provider.nickname,
                                status: "in-progress"
                            };
                            games.push(game);
                        }
                        game.status = "full";
                        appendGame(getItemById('my-games'), game);
                        noty({ force: true, text: 'You clicked "Cancel" button', type: 'error', layout: 'topRight', timeout: 1000 });
                    }
                }
            ];
        }

        var n = noty({
            layout: 'topRight',
            theme: 'noty_theme_default',
            animateOpen: { height: 'toggle' },
            animateClose: { height: 'toggle' },
            easing: 'swing',
            text: title,
            type: nofyType,
            speed: 500,
            timeout: timeout,
            closeButton: false,
            closeOnSelfClick: true,
            closeOnSelfOver: false,
            force: false,
            onShow: false,
            onShown: false,
            onClose: false,
            onClosed: false,
            buttons: buttons,
            modal: false,
            template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
            cssPrefix: 'noty_',
            custom: {
                container: null
            }
        });

        //console.log('html: ' + n);
    };

    var createLogin = function() {
        var n = noty({
            layout: 'topCenter',
            theme: 'noty_theme_default',
            animateOpen: { height: 'toggle' },
            animateClose: { height: 'toggle' },
            easing: 'swing',
            text: '',
            type: 'alert',
            speed: 500,
            timeout: false,
            closeButton: false,
            closeOnSelfClick: false,
            closeOnSelfOver: false,
            closeWith: ["button"],
            force: false,
            onShow: function () {
                var pnlLogin = $('#pnlLogin');
                pnlLogin.on('click', '#btnLogin', function () {
                    var username = $('#txtLoginUsername').val();
                    var password = $('#txtLoginPassword').val();
                    controller.provider.user.login(username, password).then(function (result) {
                        controller.loadGame("#panelbar");
                        n.close();
                    });
                });
                pnlLogin.on('click', '#btnRegister', function () {
                    var username = $('#txtRegisterUsername').val();
                    var nickname = $('#txtRegisterNickname').val();
                    var password = $('#txtRegisterPassword').val();
                    controller.provider.user.register(username, nickname, password).then(function (result) {
                        controller.loadGame("#panelbar");
                        n.close();
                    });
                });
                pnlLogin.load('../UserControls/Login.html');
            },
            onClose: false,
            onClosed: false,
            modal: true,
            template: '<div class="noty_message"><span id="pnlLogin" class="noty_text"></span></div>',
            cssPrefix: 'noty_',
            custom: {
                container: null
            },
        });
    };
    
    var playGame = function (content) {
        var n = noty({
            layout: 'topCenter',
            theme: 'noty_theme_default',
            animateOpen: { height: 'toggle' },
            animateClose: { height: 'toggle' },
            easing: 'swing',
            text: content,
            type: 'alert',
            speed: 500,
            timeout: false,
            closeButton: false,
            closeOnSelfClick: false,
            closeOnSelfOver: false,
            closeWith: ["button"],
            force: false,
            buttons: buttons = [
                {
                    type: 'btn btn-danger',
                    text: 'Close Game',
                    click: function($noty) {
                        $noty.close();
                    }
                }
            ],
            //onShow: function () {
            //    var pnlLogin = $('#pnlLogin');
            //    pnlLogin.on('click', '#btnLogin', function () {
            //        var username = $('#txtLoginUsername').val();
            //        var password = $('#txtLoginPassword').val();
            //        controller.provider.user.login(username, password).then(function (result) {
            //            controller.loadGame("#panelbar");
            //            n.close();
            //        });
            //    });
            //    pnlLogin.on('click', '#btnRegister', function () {
            //        var username = $('#txtRegisterUsername').val();
            //        var nickname = $('#txtRegisterNickname').val();
            //        var password = $('#txtRegisterPassword').val();
            //        controller.provider.user.register(username, nickname, password).then(function (result) {
            //            controller.loadGame("#panelbar");
            //            n.close();
            //        });
            //    });
            //    pnlLogin.load('../UserControls/Login.html');
            //},
            onClose: false,
            onClosed: false,
            modal: true,
            template: '<div class="noty_message"><span id="pnlPlayGame" class="noty_text"></span></div>',
            cssPrefix: 'noty_',
            custom: {
                container: null
            },
        });
    };
    
    return {
        create: create,
        createLogin: createLogin,
        playGame: playGame
    };

}());