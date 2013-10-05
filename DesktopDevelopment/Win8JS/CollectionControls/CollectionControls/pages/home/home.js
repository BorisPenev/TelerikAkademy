(function () {
    "use strict";

    WinJS.UI.Pages.define("/pages/home/home.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {

            var dataList = new WinJS.Binding.List();
            dataList.push(Data.getComputer("Dell Studio 1535", 2000, "http://www.laptoptestovi.com/images/stories/testovi/Dell/Studio_1535/Dell_studio_1535_laptop.jpg", "Core i5", 2.0, "DELL"));
            dataList.push(Data.getComputer("HP 650", 1500, "http://www.mega.pk/items_images/6900hp_650_c1n10ea.jpg", "Intel 1000M", 1.8, "HP"));
            dataList.push(Data.getComputer("Dell Studio 1535", 2000, "http://www.laptoptestovi.com/images/stories/testovi/Dell/Studio_1535/Dell_studio_1535_laptop.jpg", "Core i5", 2.0, "DELL"));

            // Sorts the groups
            function compareGroups(leftKey, rightKey) {
                return leftKey.charCodeAt(0) - rightKey.charCodeAt(0);
            }

            // Returns the group key that an item belongs to
            function getGroupKey(dataItem) {
                return dataItem.manufacturer.toUpperCase();
            }

            // Returns the title for a group
            function getGroupData(dataItem) {
                return {
                    manufacturer: dataItem.manufacturer.toUpperCase()
                };
            }

            // Create the groups for the ListView from the item data and the grouping functions
            var groupedItemsList = dataList.createGrouped(getGroupKey, getGroupData, compareGroups);


            var grdListView = document.getElementById("grdListView").winControl;
            grdListView.itemDataSource = groupedItemsList.dataSource;
            grdListView.groupDataSource = groupedItemsList.groups.dataSource;
            
            var btnSave = document.getElementById("btnSave");
            btnSave.onclick = function (e) {
                var txtName = document.getElementById("txtName").value;
                var txtPrice = document.getElementById("txtPrice").value;
                var txtImage = document.getElementById("txtImage").value;
                var txtModelName = document.getElementById("txtModelName").value;
                var txtFrequencyGHz = document.getElementById("txtFrequencyGHz").value;
                var txtManufacturer = document.getElementById("txtManufacturer").value;
                dataList.push(Data.getComputer(txtName, txtPrice, txtImage, txtModelName, txtFrequencyGHz, txtManufacturer));
            };
        }
    });
})();
