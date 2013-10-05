/// <reference path="libs/require.js" />
require.config({
	paths: {
		jquery: "libs/jquery-2.0.3",
		rsvp: "libs/rsvp.min",
		httpRequester: "libs/http-requester",
		mustache: "libs/mustache",
		controls: "app/controls",
		dataPersister: "app/data-persister",
		repository: "libs/repository"
	}
});

require(["jquery", "mustache", "dataPersister", "controls", "repository"], function ($, mustache, dataPersister, controls, repository) {
    var data = [];
    var selectedItem = 0;

    var personTemplate = document.getElementById("person-template");
    if (personTemplate) {
        dataPersister.students().then(function (result) {
            data = result;
            var mPersonTemplate = mustache.compile(personTemplate.innerHTML);
            var tableView = controls.tableView(data, 3, 2);
            var tableViewHtml = tableView.render(mPersonTemplate);
            document.getElementById("content").innerHTML = tableViewHtml;
        }, function (error) {
            console.error(error);
        });
    } else {
        var marksTemplate = document.getElementById("marks-template");
        if (marksTemplate) {
            selectedItem = repository.load("selectedItem");
            var mMarksTemplate = marksTemplate.innerHTML;
            document.getElementById("marks").innerHTML = mustache.render(mMarksTemplate, selectedItem.Marks);
        }
    }

    $("body").on("click", "td", function (event) {
        repository.save("selectedItem", data[$(this).attr("id")]);
        window.location.href = "/marks.html";
    });
});