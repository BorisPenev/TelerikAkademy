// From http://brandonlive.com/2013/06/06/winjs-helper-appsettingtoggle-bind-a-toggleswitch-to-an-appdata-settings-value
(function () {
    "use strict";
    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    WinJS.Namespace.define("BrandonUtils.Controls", {
        // Options:
        // settingName - Required. The name of the setting to toggle.
        // getSettingContainer - Function to return setting container to use.
        // enableRoaming - Use roaming setting container.
        // If neither of the above are provided, the default local container is used.
        AppSettingToggle: WinJS.Class.derive(WinJS.UI.ToggleSwitch, function (element, options) {
            WinJS.UI.ToggleSwitch.apply(this, [ element, options ]);
            WinJS.Utilities.addClass(element, "AppSettingToggle");
            if (options.getSettingContainer) {
                this.settingContainer = options.getSettingContainer();
            }
            else if (options.enableRoaming) {
                this.settingContainer = Windows.Storage.ApplicationData.current.roamingSettings.values;
            }
            else {
                this.settingContainer = Windows.Storage.ApplicationData.current.localSettings.values;
            }

            this.checked = this.settingContainer.hasKey(options.settingName) && this.settingContainer[options.settingName];    
            this.onchange = this._onChange.bind(this);
        },
        {
            _onChange: function () {
                this.settingContainer.insert(this.settingName, this.checked);
            },
        }),
    });
})();
