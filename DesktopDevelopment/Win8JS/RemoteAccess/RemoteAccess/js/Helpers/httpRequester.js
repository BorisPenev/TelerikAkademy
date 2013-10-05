/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />

var HttpRequester = WinJS.Class.define(function() {

}, {
    postJSON: function (url, data) {
        return HttpRequester.makeHttpRequest(url, "POST", data); 
    },
    getJSON: function (url) {
        return HttpRequester.makeHttpRequest(url, "GET");
    }
}, {
    makeHttpRequest: function(url, type, data) {
        var stringifiedData = "";
        if (data) {
            stringifiedData = JSON.stringify(data);
        }

        return WinJS.xhr({
            url: url,
            type: type,
            timeout: 5000,
            headers: {
                "content-type": "application/json"
            },
            data: stringifiedData
        }).then(
            function(response) {
                return response.responseText;
            },
            function (error) {
                return error;
            }
        );
    }
});