/// <reference path="../libs/jquery-2.0.3.js" />
/// <reference path="../libs/require.js" />
/// <reference path="../libs/rsvp.min.js" />

define(["httpRequester"], function (httpRequester) {
	function getStudents() {		
		return httpRequester.getJSON("/api/students");
	}

    return {
        students: getStudents
    };
});