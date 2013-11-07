/// <reference path="../libs/_references.js" />


window.viewsFactory = (function () {
	var rootUrl = "Scripts/partials/";

	var templates = {};

	function getTemplate(name) {
		var promise = new RSVP.Promise(function (resolve, reject) {
			if (templates[name]) {
			    resolve(templates[name]);
			}
			else {
				$.ajax({
					url: rootUrl + name + ".html",
					type: "GET",
					success: function (templateHtml) {
						templates[name] = templateHtml;
						resolve(templateHtml);
					},
					error: function (err) {
					    reject(err);
					}
				});
			}
		});
		return promise;
	}

	function getLoginView() {
		return getTemplate("login-form");
	}

	function getAppointmentsView() {
	    return getTemplate("appointments");
	}

	function getCreateAppointmentsView() {
	    return getTemplate("create-appointment");
	}
    
	function getCommingAppointmentsView() {
	    return getTemplate("appointments");
	}

	function getAppointmentsByDateView() {
	    return getTemplate("appointments-by-date");
	}

	function getTodosView() {
	    return getTemplate("todo-lists");
	}
    
	function getTodosByListIdView() {
	    return getTemplate("todo-lists-by-listid");
    }
    
	return {
		getLoginView: getLoginView,
		getAppointmentsView: getAppointmentsView,
		getCreateAppointmentsView: getCreateAppointmentsView,
		getCommingAppointmentsView: getCommingAppointmentsView,
		getAppointmentsByDateView: getAppointmentsByDateView,
		getTodosView: getTodosView,
		getTodosByListIdView: getTodosByListIdView,
	};
}());