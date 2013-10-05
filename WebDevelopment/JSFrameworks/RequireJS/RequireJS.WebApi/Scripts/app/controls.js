/// <reference path="jquery-2.0.3.js" />
/// <reference path="class.js" />
define(["libs/class"], function (Class) {
	var controls = controls || {};
	var TableView = Class.create({
	    init: function (itemsSource, rows, cols) {
	        if (!(itemsSource instanceof Array)) {
	            throw "The itemsSource of a TableView must be an array!";
	        }
	        this.itemsSource = itemsSource;
	        this.rows = rows;
	        this.cols = cols;
	    },
	    render: function (template) {
	        var table = document.createElement("table");
	        var count = 0;
	        for (var row = 0; row < this.rows; row++) {
	            var tr = document.createElement("tr");
	            for (var col = 0; col < this.cols; col++, count++) {
	                var td = document.createElement("td");
	                if (count < this.itemsSource.length) {
	                    td.id = count;
	                    td.innerHTML = template(this.itemsSource[count]);
	                    tr.appendChild(td);
	                }
	            }
	            table.appendChild(tr);
	        }
	        return table.outerHTML;
	    }
	});
	controls.tableView = function (itemsSource, rows, cols) {
	    return new TableView(itemsSource, rows, cols);
	};

	return controls;
});