// Created by Brandon Paddock (http://brandonlive.com)
//http://brandonlive.com/2013/04/17/binding-helpers-for-winjs/
// Provided free for any use in any WinJS project.

// Details + Examples:

// BindingHelpers.displayNoneIfNull
// Marks the element as display:none if the value is null or undefined.
// <div class="userName" data-win-bind="style.display: user.name BindingHelpers.displayNoneIfNull">
// Name: <div class="userNameValue" data-win-bind="textContent: user.name"></div></div>

// BindingHelpers.displayNoneIfNotNull
// Inverse of displayNonIfNull

// BindingHelpers.originalIfNull
// Uses the original (markup) value if the bound value is null or undefined.
// <div class="userName" data-win-bind="textContent: user.name BindingHelpers.originalIfNull>No user name available.</div>

// BindingHelpers.fillStringTemplate
// Treat the default (markup) value as a template, where the placeholder {value} is replaced with the bound value.
// <div class="userName" data-win-bind="textContent: user.name BindingHelpers.fillStringTemplate">Name: {value}</div>

// BindingHelpers.fillLinkTemplate
// Same as fillStringTemplate, but url encodes the value.
// <a href="http://website.com/accountLookup?id={value}" data-win-bind="href: account.id BindingHelpers.fillLinkTemplate">Account</a>

// BindingHelpers.stripHtmlTags
// Strips HTML tags from the value (by inserting the value into a DIV and then extracting the innerText value).

// BindingHelpers.toStaticHtml
// Pre-processes the value using toStaticHTML.

// BindingHelpers.appendToClasses
// Allows you to bind the value as a class name to the classes property.

(function () {
    "use strict";

    // These are used by the initializers below.
    function _getValue(obj, path) {
        if (path) {
            for (var i = 0, len = path.length; i < len && (obj !== null && obj !== undefined) ; i++) {
                obj = obj[path[i]];
            }
        }
        return obj;
    }

    function _putValue(obj, path, newValue) {
        if (path) {
            var i = 0;
            for (; i < (path.length - 1) && (obj !== null && obj !== undefined) ; i++) {
                obj = obj[path[i]];
            }

            if (obj) {
                obj[path[i]] = newValue;
            }
        }
    }

    // Set the element to display:none if the bound property value is null or undefined.
    var _displayNoneIfNull = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        if (dest.displayOriginal === undefined) {
            dest.displayOriginal = dest.style.display;
        }

        var updateDisplayBind = function () {
            var value = _getValue(source, sourceProperty);
            dest.style.display = ((value === undefined) || (value === null)) ? "none" : dest.displayOriginal;
        };
        return WinJS.Binding.bind(source, { sourceProperty: updateDisplayBind });
    });

    // Set the element to display:none if the bound property value is NOT null or undefined.
    var _displayNoneIfNotNull = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        if (dest.displayOriginal === undefined) {
            dest.displayOriginal = dest.style.display;
        }

        var updateDisplayBind = function () {
            var value = _getValue(source, sourceProperty);
            dest.style.display = ((value === undefined) || (value === null)) ? dest.displayOriginal : "none";
        };
        return WinJS.Binding.bind(source, { sourceProperty: updateDisplayBind });
    });

    // If the bound value is null or undefined, use the original value specified in markup.
    var _originalIfNull = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        if (dest.valueOriginal === undefined) {
            dest.valueOriginal = _getValue(dest, destProperty);
        }

        var originalIfNullBind = function () {
            var newValue = _getValue(source, sourceProperty);
            _putValue(dest, destProperty, ((newValue === null) || (newValue === undefined)) ? dest.valueOriginal : newValue);
        };
        return WinJS.Binding.bind(source, { sourceProperty: originalIfNullBind });
    });

    // Treat the default (markup) value as a template, where the placeholder {value} is replaced with the bound value.
    var _fillStringTemplate = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        if (!dest.bindingOriginalValues) {
            dest.bindingOriginalValues = new Array();
        }

        var destPropertyName = destProperty.join(".");

        if (!dest.bindingOriginalValues[destPropertyName]) {
            dest.bindingOriginalValues[destPropertyName] = _getValue(dest, destProperty);
        }

        var bindHelper = function () {
            var value = _getValue(source, sourceProperty);
            var formatted = dest.bindingOriginalValues[destPropertyName].replace("{value}", value);
            _putValue(dest, destProperty, formatted);
        };
        return WinJS.Binding.bind(source, { sourceProperty: bindHelper });
    });

    // Same as fillStringTemplate, but url encodes the value.
    var _fillLinkTemplate = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        if (!dest.bindingOriginalValues) {
            dest.bindingOriginalValues = new Array();
        }

        var destPropertyName = destProperty.join(".");

        if (!dest.bindingOriginalValues[destPropertyName]) {
            dest.bindingOriginalValues[destPropertyName] = _getValue(dest, destProperty);
        }

        var bindHelper = function () {
            var value = _getValue(source, sourceProperty);
            var formatted = dest.bindingOriginalValues[destPropertyName].replace("{value}", encodeURIComponent(value));
            _putValue(dest, destProperty, formatted);
        };
        return WinJS.Binding.bind(source, { sourceProperty: bindHelper });
    });

    // Strips HTML tags from the value (by inserting the value into a DIV and then extracting the innerText value).
    var _stripHtmlTags = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        var bindHelper = function () {
            var value = _getValue(source, sourceProperty);
            var element = document.createElement("div");
            element.innerHTML = value;
            var textValue = element.innerText;
            _putValue(dest, destProperty, textValue);
        };
        return WinJS.Binding.bind(source, { sourceProperty: bindHelper });
    });

    // Pre-processes the value using toStaticHTML.
    var _toStaticHtml = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        var bindHelper = function () {
            var value = _getValue(source, sourceProperty);
            _putValue(dest, destProperty, toStaticHTML(value));
        };
        return WinJS.Binding.bind(source, { sourceProperty: bindHelper });
    });

    // Allows you to bind the value as a class name to the classes property.
    var _appendToClasses = WinJS.Binding.initializer(function (source, sourceProperty, dest, destProperty) {
        var appendHelper = function () {
            WinJS.Utilities.addClass(dest, _getValue(source, sourceProperty));
        };
        return WinJS.Binding.bind(source, { sourceProperty: appendHelper });
    });

    WinJS.Namespace.define("BindingHelpers", {
        displayNoneIfNull: _displayNoneIfNull,
        displayNoneIfNotNull: _displayNoneIfNotNull,
        originalIfNull: _originalIfNull,
        fillStringTemplate: _fillStringTemplate,
        fillLinkTemplate: _fillLinkTemplate,
        stripHtmlTags: _stripHtmlTags,
        toStaticHtml: _toStaticHtml,
        appendToClasses: _appendToClasses,
    });
})();
