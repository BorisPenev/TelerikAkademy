define([""], function() {

    function storeObject(key, obj) {
        localStorage[key] = JSON.stringify(obj);
    }

    function retrieveObject(key) {
        return JSON.parse(localStorage[key]);
    }

    return {
        save: function(name, data) {
            storeObject(name, data);
        },
        load: function(name) {
            return retrieveObject(name);
        }
    };
});