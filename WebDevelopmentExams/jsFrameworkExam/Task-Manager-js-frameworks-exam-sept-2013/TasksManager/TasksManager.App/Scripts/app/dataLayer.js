/// <reference path="../libs/class.js" />
/// <reference path="../libs/q.js" />
/// <reference path="../libs/jquery-2.0.3.js" />
/// <reference path="../libs/httpRequester.js" />
/// <reference path="../libs/cryptoJS.js" />
/// <reference path="../libs/httpRequester.js" />

window.persisters = (function () {

    var username = localStorage.getItem('username');
    var accessToken = localStorage.getItem('accessToken');
    //var isAdmin = localStorage.getItem('isAdmin');

    function saveUserData(userData) {
        localStorage.setItem('username', userData.username);
        localStorage.setItem('accessToken', userData.accessToken);
        localStorage.setItem('id', userData.id);
        username = userData.username;
        accessToken = userData.accessToken;
    }

    function clearUserData() {
        localStorage.removeItem('username');
        localStorage.removeItem('accessToken');
        localStorage.removeItem('id');
        username = null;
        accessToken = null;
    }

    function getHeaders() {
        var headers = {
            "X-accessToken": accessToken
        };
        return headers;
    }

    var MainPersiter = Class.create({
        init: function (root) {
            this.rootUrl = root;
            this.users = new UserPersiter(this.rootUrl);
            this.appointments = new AppointmentsPersister(this.rootUrl);
            this.todos = new TodosPersister(this.rootUrl);
        },

        isUserLoggedIn: function () {
            return (accessToken != null);
        },

        getUsername: function () {
            return username;
        }
    });

    var UserPersiter = Class.create({
        init: function (root) {
            this.rootUrl = root;
        },

        register: function (user) {
            var url = this.rootUrl + "users/register/";

            user.authCode = CryptoJS.SHA1(user.authKey).toString();

            return httpRequester.postJson(url, user).then(function (result) {
                saveUserData(result);
            });
        },

        login: function (user) {
            var url = this.rootUrl + "auth/token";

            user.authCode = CryptoJS.SHA1(user.authKey).toString();
            return httpRequester.postJson(url, user).then(function (result) {
                saveUserData(result);
            });
        },

        logout: function (user) {
            var url = this.rootUrl + "users/logout";
            return httpRequester.putJson(url, null, getHeaders()).then(function() {
                return clearUserData();
            });
        },
    });

    var AppointmentsPersister = Class.create({
        init: function (root) {
            this.rootUrl = root + "appointments/";
        },

        create: function (appointment) {
            var url = this.rootUrl;
            return httpRequester.postJson(url, appointment, getHeaders());
        },
        
        getAll: function () {
            var url = this.rootUrl + "/all";
            return httpRequester.getJson(url, getHeaders());
        },

        getComming: function () {
            var url = this.rootUrl + "/comming";
            return httpRequester.getJson(url, getHeaders());
        },
        
        getByDate: function (date) { //dd-mm-yyyy
            var url = this.rootUrl + "?date=" + date;
            return httpRequester.getJson(url, getHeaders());
        },
        
        getToday: function () {
            var url = this.rootUrl + "/today";
            return httpRequester.getJson(url, getHeaders());
        },
        
        getCurrent: function () {
            var url = this.rootUrl + "/current";
            return httpRequester.getJson(url, getHeaders());
        },

    });
    
    var TodosPersister = Class.create({
        init: function (root) {
            this.rootUrl = root;
        },

        create: function (todoList) {
            var url = this.rootUrl + "lists/";
            return httpRequester.postJson(url, todoList, getHeaders());
        },

        getAll: function () {
            var url = this.rootUrl + "lists/";
            return httpRequester.getJson(url, getHeaders());
        },

        createTodosByListId: function (listId, todoList) {
            var url = this.rootUrl + "lists/" + listId + "/todos";
            return httpRequester.postJson(url, todoList, getHeaders());
        },
        
        getTodosByListId: function (listId) {
            var url = this.rootUrl + "lists/" + listId + "/todos";
            return httpRequester.getJson(url, getHeaders());
        },
        
        changeTodoStatus: function (todoId) {
            var url = this.rootUrl + "todos/" + todoId;
            return httpRequester.putJson(url, null, getHeaders());
        },

    });

    return {
        get: function (rootUrl) {
            return new MainPersiter(rootUrl);
        }
    };
})();