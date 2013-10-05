//function getItemByIndex(index) {
//    rootItem = panelBar.children("li").eq(index);
//    return rootItem;
//}

function getItemById(id) {
    rootItem = panelBar.find('[id=' + id + ']');
    return rootItem;
}

function findFieldById(fields, id) {
    rootItem = fields.find('[id=' + id + ']');
    return rootItem;
}

//function createKendoWindow(title, content) {
//    var pnlLogin = $('<div id="pnlLogin">').dialog();

//    var kwindow = $("#kwindow").kendoWindow({
//        modal: true,
//        title: title,
//        content: '../UserControls/' + content + '.html',
//        refresh: function () { this.center(); }
//    });
//    return kwindow.data('kendoWindow');
//}

function createJoinGameWindow(title, gameId) {
    var pnlJoinGame = $('<div id="pnlJoinGame" title="Join in game : ' + title.escape() + '" tag=' + gameId + '>').dialog({
        show: {
            effect: "blind",
            duration: 500
        },
    });
    pnlJoinGame.load('../UserControls/JoinGame.html');
    return pnlJoinGame;
}

var brick = "<div class='brick small'><div class='delete'>&times;</div></div>";
function createActiveGameWindow(game, isGameWindowCreated) {

    var inTurn;
    if (game.inTurn === "red") {
        inTurn = game.red.nickname;
    } else {
        inTurn = game.blue.nickname;
    }
    var str = '<table width="500px"><tr><th colspan="9">Game Title: ' + game.title + ' | In Turn: ' + inTurn.escape() + '</th></tr>';

    for (var row = 0; row < 9; row++) {
        str += '<tr>';
        for (var col = 0; col < 9; col++) {
            str += '<td id="' + row + '-' + col + '" height="42" width="42" ondrop="drop(event)" ondragover="allowDrop(event)"></td>';
        }
        str += '</tr>';
    }
    str += '</table>';

    str += '<button id="btnDefend" ondrop="dropDefend(event)" ondragover="allowDrop(event)">Place unite here to set in defend mode</button>';

    var fields = $(str);

    redUnits = game.red.units;
    blueUnits = game.blue.units;


    for (var i = 0; i < game.red.units.length; i++) {
        var field = findFieldById(fields, game.red.units[i].position.x + '-' + game.red.units[i].position.y);
        field.append(createUnit(game.red.units[i], game.red.nickname));
    }

    for (var i = 0; i < game.blue.units.length; i++) {
        var field = findFieldById(fields, game.blue.units[i].position.x + '-' + game.blue.units[i].position.y);
        field.append(createUnit(game.blue.units[i], game.blue.nickname.escape()));
    }

    if (!isGameWindowCreated) {
        notify.playGame(fields);
    } else {
        var playField = $(document).find('#pnlPlayGame');
        playField[0].innerHTML = '<table width="500px">' + fields[0].innerHTML + '</table>';
    }

    //var pnlActiveGame = $('<div id="pnlActiveGame" title="Game in progress : ' + game.title + '" tag=' + game.gameId + '>').dialog({
    //    minWidth: 500,
    //    show: {
    //        effect: "blind",
    //        duration: 500,
    //    },
    //});
    //pnlActiveGame.append(fields);
    //return pnlActiveGame;
}

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("Text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    unitId = ev.dataTransfer.getData("Text");
    if (!isNaN(unitId)) {
        var position = ev.currentTarget.id.split('-');
        var checkOwner = $(ev.currentTarget).find('img').attr('tag');
        if (checkOwner === undefined) {
            controller.provider.battle.move(selectedGameId, unitId, position[0], position[1]).then(function (result) {
                ev.target.appendChild(document.getElementById(unitId));
            }, function (error) {
                if (error.responseJSON.errCode === "INV_UNIT_POS") {

                }
            });
        } else if (checkOwner.escape() !== controller.provider.nickname()) {
            controller.provider.battle.attack(selectedGameId, unitId, position[0], position[1]).then(function(result) {

            }, function(error) {
                
            });
        }
    }
}

function dropDefend(ev) {
    ev.preventDefault();
    unitId = ev.dataTransfer.getData("Text");
    if (!isNaN(unitId)) {
        controller.provider.battle.defend(selectedGameId, unitId).then(function(result) {

        }, function(error) {
            
        });
        //var position = ev.currentTarget.id.split('-');
        //var checkOwner = $(ev.currentTarget).find('img').attr('tag');
        //if (checkOwner === undefined) {
        //    controller.provider.battle.move(selectedGameId, unitId, position[0], position[1]).then(function (result) {
        //        ev.target.appendChild(document.getElementById(unitId));
        //    }, function (error) {
        //        if (error.responseJSON.errCode === "INV_UNIT_POS") {

        //        }
        //    });
        //} else if (checkOwner.escape() !== controller.provider.nickname()) {
        //    controller.provider.battle.attack(selectedGameId, unitId, position[0], position[1]).then(function (result) {

        //    }, function (error) {

        //    });
        //}
    }
}

var createUnit = function (unit, nickname) {
    if (nickname.escape() === controller.provider.nickname()) {
        return $('<img id="' + unit.id + '" src="/Images/' + unit.type + '.gif" class="unit' + unit.owner.escape() + '" tag="' + nickname + '" height="42" width="42" draggable="true" ondragstart="drag(event)">');
    } else {
        return $('<img id="' + unit.id + '" src="/Images/' + unit.type + '.gif" class="unit' + unit.owner.escape() + '" tag="' + nickname + '" height="42" width="42">');
    }
};

function add(gameId) {
    $('.gridly').append("<div class='brick small' tag='" + gameId + "'><div id='brickContainer'></div><div class='delete'>&times;</div></div>");

    $('#brickContainer').load('../UserControls/ActiveGame.html');

    return $('.gridly').gridly();
}


function createNewGameWindow() {
    var pnlNewGame = $('<div id="pnlNewGame" title="Create a new game">').dialog({
        show: {
            effect: "blind",
            duration: 500
        },
    });
    pnlNewGame.load('../UserControls/CreateGame.html');
    return pnlNewGame;
}

function findGameById(source, id) {
    return source.filter(function (obj) {
        // coerce both obj.id and id to numbers 
        // for val & type comparison
        return +obj.id === +id;
    })[0];
}

function findByNickname(source, nickname) {
    return source.filter(function (obj) {
        // coerce both obj.id and id to numbers 
        // for val & type comparison
        return +obj.nickname === +nickname;
    })[0];
}

// SEND
function pubnubPublish(message) {
    pubnub.publish({
        channel: "saykor_bulls_cows",
        message: message
    });
}

function appendGame(selectedItem, data, id) {
    if (selectedItem.find('ul').length == 0) {
        var ul = $('<ul id="' + id + '"></ul>');
        selectedItem.append(ul);
    }
    var css;
    if (data.status === "full") {
        if (data.creator.escape() === controller.provider.nickname()) {
            css = "full-active";
        } else {
            css = "full-waiting";
        }
    }
    selectedItem.find('ul').append('<li id="' + data.id + '" class="' + css + '" tag="' + data.creator.escape() + '">' + data.title.escape() + '</li>');
}

function appendScores(selectedItem, data, id) {
    if (selectedItem.find('ul').length == 0) {
        var ul = $('<ul id="' + id + '"></ul>');
        selectedItem.append(ul);
    }
    selectedItem.find('ul').append('<li id="' + data.id + '" class="k-link">' + data.nickname.escape() + ' | ' + data.score + '</li>');
}

String.prototype.escape = function () {
    var tagsToReplace = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;'
    };
    return this.replace(/[&<>]/g, function (tag) {
        return tagsToReplace[tag] || tag;
    });
};