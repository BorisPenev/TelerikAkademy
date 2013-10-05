$(document).ready(function () {

    var count = 5;
    var angle = 0;
    var radius = 200;
    var maxWidth = screen.width - 100;
    var maxHeight = screen.height - 100;
    var divs = [];
    start();

    function start() {
        generateDivs(count);
        divs = $('#wrapper div');
        $('#wrapper').timer({
            delay: 100,
            callback: function() {
                for (var i = 0; i < count; i++) {
                    moveDiv(divs[i]);
                }
            },
            repeat: true,
        });
       
    }

    function generateDivs(n) {

        for (var i = 0; i < n; i++) {
            var movableDiv = document.createElement("div");
            movableDiv.classList.add("movableDiv");
            movableDiv.style.position = "absolute";
            moveDiv(movableDiv);
            $('#wrapper').append(movableDiv);
        }
    }

    function moveDiv(movableDiv) {
        angle += 5;

        var topPos = parseInt(maxHeight / count);
        var x = topPos + Math.cos(angle) * radius;

        var left = parseInt(maxWidth / count);
        var y = left + Math.sin(angle) * radius;

        $(movableDiv).animate({
            "left": y + "px",
            "top": x + "px"
        }, "fast");
    }

});