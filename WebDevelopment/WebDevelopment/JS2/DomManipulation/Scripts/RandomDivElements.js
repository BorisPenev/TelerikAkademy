$(document).ready(function() {

    $('#btnCreateDivElement').click(function () {
        var numberOfElements = parseInt($('#txtNumberOfElements').val());
        for (var i = 0; i < numberOfElements; i++) {
            $('#lblCreateDivElement').append(createDivElement());
        }
    });

    var position = ["static", "absolute", "fixed", "relative"];
    var borderStyle = ['solid', 'dashed', 'dotted'];
    
    function createDivElement() {
        var doc = document.createElement('div');

        doc.style.width = randomNumber(20, 100) + 'px';
        doc.style.height = randomNumber(20, 100) + 'px';
        doc.style.backgroundColor = randomColor();
        doc.style.color = randomColor();
        doc.style.position = position[parseInt(randomNumber(0, 4))];
        doc.style.borderRadius = randomColor(0, 50) + '%';
        doc.style.borderColor = randomColor();
        doc.style.borderWidth = randomNumber(1, 20) + 'px';
        doc.style.borderStyle = borderStyle[parseInt(randomNumber(0, 2))];

        var strong = document.createElement('strong');

        strong.textContent = 'div';
        doc.appendChild(strong);

        return doc;
    }

    function randomColor() {
        return 'rgba(' + randomNumber(0, 255) + ',' + randomNumber(0, 255) + ',' + randomNumber(0, 255) + ',' + randomNumber(1, 10) / 10 + ')';
    }

    function randomNumber(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }
});