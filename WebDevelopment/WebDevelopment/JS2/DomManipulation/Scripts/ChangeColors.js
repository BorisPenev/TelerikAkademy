(function () {

    function addEventListener(selector, eventName, listener) {
        document.getElementById(selector).addEventListener(eventName, listener, false);
    }

    var textEl = document.getElementById("txtArea");

    addEventListener('cFontColor', 'change', function (event) {
        textEl.style.color = event.target.value;
    });

    addEventListener('cBackgroundColor', 'change', function (event) {
        textEl.style.backgroundColor = event.target.value;
    });
})();