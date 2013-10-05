(function () {
    "use strict";

    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;

    ui.Pages.define("/pages/home/home.html", {
        
        ready: function (element, options) {
            WinJS.xhr({ url: "http://en.wikipedia.org/wiki/Darth_Vader" }).then(function (result) {
                var el = $( '<div></div>' );
                el.html(result.responseText);
                var contentList = el.find("#toc a");
                var list = new WinJS.Binding.List();
                for (var i = 0; i < contentList.length; i++) {
                    var link = $(contentList[i]);
                    var href = link.attr("href");
                    var text = link[0].innerText;
                    var c = new PageContent(href, text);
                    list.push(c);
                }
                
                var grdView = document.getElementById("grdView").winControl;
                grdView.itemDataSource = list.dataSource;

                grdView.oniteminvoked = function (e) {
                    e.detail.itemPromise.then(function (item) {
                        Windows.System.Launcher.launchUriAsync(new Windows.Foundation.Uri("http://en.wikipedia.org/wiki/Darth_Vader"+ item.data.link));
                    });
                };
            }, function(error) {
                console.log(error);
            });
        },

        // This function updates the page layout in response to viewState changes.
        updateLayout: function (element, viewState, lastViewState) {
            /// <param name="element" domElement="true" />

            
        },
    });
})();
