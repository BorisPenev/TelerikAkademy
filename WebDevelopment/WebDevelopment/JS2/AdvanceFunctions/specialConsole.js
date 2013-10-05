var specialConsole = (function () {
    return {
        writeLine: function (message, params) {
            if (params == undefined) {
                console.log(message);
            } else {
                var str = message.format(params.split(','));
                console.log(str);
            }
        },
        writeError: function (message, params) {
            specialConsole.writeLine(message, params);
        },
        writeWarning: function (message, params) {
            specialConsole.writeLine(message, params);
        }
    };
})();

String.prototype.format = function (args) {
    var str = this;
    return str.replace(String.prototype.format.regex, function (item) {
        var intVal = parseInt(item.substring(1, item.length - 1));
        var replace;
        if (intVal >= 0) {
            replace = args[intVal];
        } else if (intVal === -1) {
            replace = "{";
        } else if (intVal === -2) {
            replace = "}";
        } else {
            replace = "";
        }
        return replace;
    });
};
String.prototype.format.regex = new RegExp("{-?[0-9]+}", "g");