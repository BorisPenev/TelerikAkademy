function getItemByIndex(target) {
    var itemIndexes = target;
    rootItem = panelBar.element.children("li").eq(itemIndexes);
    //return itemIndexes.length > 1 ?
    //    rootItem.find(".k-group > .k-item").eq(itemIndexes[1]) :
    //    rootItem;
};

function getItemById(target) {
    var itemIndexes = target;
    rootItem = panelBar.element.children("li").eq(itemIndexes);
    //return itemIndexes.length > 1 ?
    //    rootItem.find(".k-group > .k-item").eq(itemIndexes[1]) :
    //    rootItem;
};

function createKendoWindow(title, content) {
    var kwindow = $("#kwindow").kendoWindow({
        modal: true,
        title: title,
        content: '../UserControls/' + content + '.html',
        refresh: function () { this.center(); }
    });
    return kwindow.data('kendoWindow');
}