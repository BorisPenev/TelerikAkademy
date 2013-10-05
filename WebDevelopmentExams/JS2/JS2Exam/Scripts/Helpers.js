function initEvents(ev) {
    if (!ev) {
        ev = window.event;
    }

    if (ev.stopPropagation) {
        ev.stopPropagation();
    } else {
        ev.cancelBubble = true;
    }

    if (ev.preventDefault) {
        ev.preventDefault();
    } else {
        ev.returnValue = false;
    }

    var target;
    if (ev.target) {
        target = ev.target;
    } else {
        target = ev.srcElement;
    }
    return target;
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