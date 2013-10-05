(function () {
    "use strict";

    WinJS.UI.Pages.define("/pages/home/home.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            var httpRequest = new HttpRequester();
            

            var btnSend = document.getElementById("btnSend");
            btnSend.addEventListener("click",function(e) {
                var txtMessage = document.getElementById("txtMessage").value;
                var txtUsername = document.getElementById("txtUsername").value;
                var data = {
                    Content: txtMessage,
                    Author: txtUsername
                };
                httpRequest.postJSON("http://posted.apphb.com/api/posts", data).then(function(response) {
                    console.log(response);
                }, function(error) {
                    console.log(error);
                });
            });

            setInterval(function() {
                httpRequest.getJSON("http://posted.apphb.com/api/posts").then(function (response) {
                    var result = JSON.parse(response);
                    var listItems = new WinJS.Binding.List(result.reverse());
                    var pnlPosts = document.getElementById("pnlPosts").winControl;
                    pnlPosts.itemDataSource = listItems.dataSource;
                }, function (error) {
                    console.log(error);
                });
            }, 5000);
        }
    });
})();
