(function ($) {
    $.fn.treeView = function () {
        var element = this;
        element.find('li>ul').hide();
        element.find('li').click(function (ev) {
            ev.stopPropagation();
            $(this).find('>ul').toggle();
        });
    };
}(jQuery));