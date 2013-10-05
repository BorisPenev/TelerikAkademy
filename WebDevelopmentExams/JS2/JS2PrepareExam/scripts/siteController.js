(function() {

    var SiteController = function (param) {
        this.param = param;
        //this.treeViewContainer = document.getElementById(param.treeViewContainer);
        this.btnAddFolder = document.getElementById(param.btnAddFolder);
        this.txtNewFolderName = document.getElementById(param.txtNewFolderName);
    };

    SiteController.prototype = {
        init: function() {
            this.initTreeView();
        },
        initTreeView: function() {
            this.treeView = new TreeView({
                treeViewContainer: this.param.treeViewContainer,
                btnAddFolder: this.btnAddFolder,
                txtNewFolderName: this.txtNewFolderName
            });
            this.treeView.init();
        }
    };

    //document.body.onload = function() {
    //    var siteController = new SiteController({
    //        treeViewContainer: 'treeViewContainer'
    //    });
    //    siteController.init();
    //};

    window.addEventListener('load', function() {
        var siteController = new SiteController({
            treeViewContainer: 'treeViewContainer',
            btnAddFolder: 'btnAddFolder',
            txtNewFolderName: 'txtNewFolderName'
        });
        siteController.init();
    }, false);

})();