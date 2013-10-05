var movingShapes = (function () {
    var radius = 200;
    var maxWidth = 300;
    var maxHeight = 300;
    
    return {
        add: function (element) {
            var div = createDivElement();
            switch (element) {
                case 'rect':
                    setInterval(function () {
                        var angleInRadians = (div.getAttribute("angleAttr")) * (Math.PI / 180);
                        var left = radius * Math.cos(angleInRadians) + maxWidth;
                        var top = radius * Math.sin(angleInRadians) + maxHeight;
                        div.style.left = left + 'px';
                        div.style.top = top + 'px';
                        div.attributes.angleAttr.nodeValue++;
                    }, 100);
                    break;
                default:
                    setInterval(function () {
                        var top = parseInt(div.style.top.replace('px'));
                        var left = parseInt(div.style.left.replace('px'));
                        if (top == 0 && left < maxWidth) {
                            left++;
                        }
                        else if (top < maxHeight && left == maxWidth) {
                            top++;
                        }
                        else if (left > 0 && top == maxHeight) {
                            left--;
                        }
                        else if (top > 0 && left == 0) {
                            top--;
                        }
                        div.style.left = left + 'px';
                        div.style.top = top + 'px';
                    }, 5);
                    break;
            }
        }
    };
    
    function createDivElement() {
        var doc = document.createElement('div');

        doc.style.width = 30 + 'px';
        doc.style.height = 30 + 'px';
        doc.style.backgroundColor = randomColor();
        doc.style.color = randomColor();
        doc.style.borderRadius = 30 + 'px';
        doc.style.borderColor = randomColor();
        doc.style.borderWidth = 1 + 'px';
        doc.style.borderStyle = 'solid';
        doc.style.position = 'absolute';
        
        doc.style.left = 0 + 'px';
        doc.style.top = 0 + 'px';
        
        doc.setAttribute("class", "circle");
        doc.setAttribute("angleAttr", "0");
        document.body.appendChild(doc);

        return doc;
    }
    
    function randomColor() {
        return 'rgba(' + randomNumber(0, 255) + ',' + randomNumber(0, 255) + ',' + randomNumber(0, 255) + ',' + randomNumber(1, 10) / 10 + ')';
    }

    function randomNumber(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }
    
})();