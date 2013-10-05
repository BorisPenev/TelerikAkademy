$(document).ready(function () {

    $('#btnReverseNumber').click(function () {
        var n = $('#txtReverseNumber').val();
        var s = n.split('');
        s.reverse();
        $('#pnlReverseNumber').text(s.join(''));
    });

    $('#btnCheckCorrectBrackets').click(function () {
        var str = $('#txtCheckCorrectBrackets').val().split('');
        var count = 0;
        for (var i = 0; i < str.length; i++)
        {
            switch (str[i])
            {
                case '(':
                    count++;
                    break;
                case ')':
                    count--;
                    break;
                default:
                    break;
            }
            if (count < 0)
                break;
        }
        $('#pnlCheckCorrectBrackets').text(count == 0);
    });

    $('#btnCountSubstring').click(function () {
        $('#pnlCountSubstring').text("The result is: " + SearchInText("We are living in an yellow submarine. We don't have anything else. Inside the submarine is very tight. So we are drinking all the day. We will move out of it in 5 days.", "in", true).length);
    });

    function SearchInText(source, word, isCaseSensitive) {
        var re = new RegExp(word, "g");
        if (isCaseSensitive === true)
            re = new RegExp(word, "gi");
        return source.match(re);
    }

    $('#btnToUpperRegionOfString').click(function () {
        var str = "We are <mixcase>living</mixcase> in a <upcase>yellow submarine</upcase>. We <mixcase>don't</mixcase> have <lowcase>anything</lowcase> else.";
        var reg = new RegExp("<upcase>(.*?)</upcase>");
        var mc = str.match(reg);
        for (var i = 0; i < mc.length; i++) {
            var re = new RegExp(mc[i], "g");
            str = str.replace(re, mc[i + 1].toUpperCase());
            i++;
        }

        var reg = new RegExp("<lowcase>(.*?)</lowcase>");
        var mc = str.match(reg);
        for (var i = 0; i < mc.length; i++) {
            var re = new RegExp(mc[i], "g");
            str = str.replace(re, mc[i + 1].toLowerCase());
            i++;
        }

        //sigurno ima po-dobar nachin no ne mi se zanimavashe poveche sled kato tova raboti.
        var count = str.match(new RegExp("<mixcase>(.*?)</mixcase>", "g"), "g").length;
        for (var m = 0; m < count; m++) {
            var reg = new RegExp("<mixcase>(.*?)</mixcase>");
            var mc = str.match(reg);
            for (var i = 0; i < mc.length; i++) {
                var re = new RegExp(mc[i], "g");
                var mix = mc[i + 1].split('');
                var result = "";
                for (var j = 0; j < mix.length; j++) {
                    if (j % 2 == 0)
                        result += mix[j].toUpperCase();
                    else
                        result += mix[j].toLowerCase();
                }
                str = str.replace(re, result);
                i++;
            }
        } 

        $('#pnlToUpperRegionOfString').text(str);
    });

    $('#btnReplaceWhiteSpace').click(function () {
        var str = $('#txtReplaceWhiteSpace').val();
        $('#pnlReplaceWhiteSpace').text(str.replace(new RegExp(" ", "g"), "&nbsp;"));
    });

    $('#btnExtractText').click(function () {
        var result = "";
        var str = "<html><head><title>Sample site</title></head><body><div>text<div>more text</div>and more...</div>in body</body></html>";
        var count = str.match(new RegExp(">(.*?)<", "g"), "g");
        for (var i = 0; i < count.length; i++) {
            var mc = count[i].replace(">", "").replace("<", "")
            if (mc != "")
                result += mc;
        }
        $('#pnlExtractText').text(result);
    });

    $('#btnParseUri').click(function () {
        var str = $('#txtParseUri').val();
        $('#pnlParseUri').text("protocol: " + parseUri(str).protocol + " server: " + parseUri(str).host + " resources: " + parseUri(str).path);
    });

    function parseUri(str) {
        var o = parseUri.options,
            m = o.parser[o.strictMode ? "strict" : "loose"].exec(str),
            uri = {},
            i = 14;

        while (i--) uri[o.key[i]] = m[i] || "";

        uri[o.q.name] = {};
        uri[o.key[12]].replace(o.q.parser, function ($0, $1, $2) {
            if ($1) uri[o.q.name][$1] = $2;
        });

        return uri;
    };

    parseUri.options = {
        strictMode: false,
        key: ["source", "protocol", "authority", "userInfo", "user", "password", "host", "port", "relative", "path", "directory", "file", "query", "anchor"],
        q: {
            name: "queryKey",
            parser: /(?:^|&)([^&=]*)=?([^&]*)/g
        },
        parser: {
            strict: /^(?:([^:\/?#]+):)?(?:\/\/((?:(([^:@]*)(?::([^:@]*))?)?@)?([^:\/?#]*)(?::(\d*))?))?((((?:[^?#\/]*\/)*)([^?#]*))(?:\?([^#]*))?(?:#(.*))?)/,
            loose: /^(?:(?![^:@]+:[^:@\/]*@)([^:\/?#.]+):)?(?:\/\/)?((?:(([^:@]*)(?::([^:@]*))?)?@)?([^:\/?#]*)(?::(\d*))?)(((\/(?:[^?#](?![^?#\/]*\.[^?#\/.]+(?:[?#]|$)))*\/?)?([^?#\/]*))(?:\?([^#]*))?(?:#(.*))?)/
        }
    };

    $('#btnReplaceAnhorTag').click(function () {
        var str = "<p>Please visit <a href=\"http://academy.telerik. com\">our site</a> to choose a training course. Also visit <a href=\"www.devbg.org\">our forum</a> to discuss the courses.</p>";//$('#txtParseUri').val();
        $('#pnlReplaceAnhorTag').text(str.replace(new RegExp("<a href=\"", "g"), "[URL=").replace(new RegExp("\">", "g"), "]").replace(new RegExp("</a>", "g"), "[/URL]"));
    });

    $('#btnExtractEmail').click(function () {
        var str = $('#txtExtractEmail').val();
        var emails = extractEmails(str);
        $('#pnlExtractEmail').text(emails.join('\n'));
    });

    function extractEmails ( text ){
        return text.match(/([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9._-]+)/gi);
    }

    $('#btnFindPilindromWords').click(function () {
        var result = [];
        var str = "Write a program that extracts from a given text all palindromes, e.g. \"ABBA\", \"lamal\", \"exe\"";
        var words = str.match(/(\w*)/gi);
        for (var i = 0; i < words.length; i++) {
            if (words[i].length > 1) {
                if (IsPalindrom(words[i]))
                    result.push(words[i]);
            }
        }
        $('#pnlFindPilindromWords').text(result.toString());
    });

    function IsPalindrom(str)
    {
        for (var i = 0; i < str.length / 2; i++)
            if (str[i] != str[str.length - 1 - i])
            return false;
        return true;
    }

    $('#btnStringFormat').click(function () {
        var str = "{0}, {1}, {0} text {2}";
        str = str.format(["1", "Pesho", "Gosho"]);
        $('#pnlStringFormat').text(str);
    });

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
});