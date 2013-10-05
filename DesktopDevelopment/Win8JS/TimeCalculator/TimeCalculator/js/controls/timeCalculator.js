/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
WinJS.Namespace.define("Saykor.Controls", {
    TimeCalculator: WinJS.Class.define(function (element, options) {
        this._element = element;
        element.winControl = this;
        this._element.winControl = this;

        this._buildVisualTree();
    },
        {
            _element: null,
            _buildVisualTree: function () {
                var self = this;
                var fieldset = document.createElement("fieldset");
                this._element.appendChild(fieldset);

                var legend = document.createElement("legend");
                legend.textContent = "Date Time Calculator";
                fieldset.appendChild(legend);
                
                var pnlDatePicker = document.createElement("div");
                fieldset.appendChild(pnlDatePicker);
                
                //start date picker
                var lblStartDatePicker = document.createElement("label");
                lblStartDatePicker.textContent = "Start Date:";
                pnlDatePicker.appendChild(lblStartDatePicker);
                
                var pnlStartDatePicker = document.createElement("div");
                var startDatePicker = new WinJS.UI.DatePicker(pnlStartDatePicker);
                pnlDatePicker.appendChild(pnlStartDatePicker);
                
                //end datepicker
                var lblEndDatePicker = document.createElement("label");
                lblEndDatePicker.textContent = "End Date:";
                pnlDatePicker.appendChild(lblEndDatePicker);

                var pnlEndDatePicker = document.createElement("div");
                var endDatePicker = new WinJS.UI.DatePicker(pnlEndDatePicker);
                pnlDatePicker.appendChild(pnlEndDatePicker);
                
                var pnlTimePicker = document.createElement("div");
                pnlTimePicker.style.display = "none";
                fieldset.appendChild(pnlTimePicker);

                //start Time picker
                var lblStartTimePicker = document.createElement("label");
                lblStartTimePicker.textContent = "Start Time:";
                pnlTimePicker.appendChild(lblStartTimePicker);

                var pnlStartTimePicker = document.createElement("div");
                var startTimePicker = new WinJS.UI.TimePicker(pnlStartTimePicker);
                pnlTimePicker.appendChild(pnlStartTimePicker);

                //end Timepicker
                var lblEndTimePicker = document.createElement("label");
                lblEndTimePicker.textContent = "End Time:";
                pnlTimePicker.appendChild(lblEndTimePicker);

                var pnlEndTimePicker = document.createElement("div");
                var endTimePicker = new WinJS.UI.TimePicker(pnlEndTimePicker);
                pnlTimePicker.appendChild(pnlEndTimePicker);
                
                var pnlButtons = document.createElement("div");
                fieldset.appendChild(pnlButtons);
                
                var pnlShowTimePanel = document.createElement("div");
                var btnShowTimePanel = new WinJS.UI.ToggleSwitch(pnlShowTimePanel);
                btnShowTimePanel.textContent = "Show/Hide Time Panel";
                btnShowTimePanel.addEventListener("change", function(e) {
                    if (btnShowTimePanel.checked) {
                        pnlTimePicker.style.display = "block";
                    } else {
                        pnlTimePicker.style.display = "none";
                    }
                });
                pnlButtons.appendChild(pnlShowTimePanel);

                var btnCalculate = document.createElement("button");
                btnCalculate.textContent = "Calculate";
                btnCalculate.addEventListener("click", function(e) {
                    menu.show(btnCalculate, "bottom");
                });
                pnlButtons.appendChild(btnCalculate);
                
                var pnlMenu = document.createElement("div");
                var menu = new WinJS.UI.Menu(pnlMenu);

                var btnCalculateDays = document.createElement("button");
                btnCalculateDays.textContent = "Calculate Days";
                btnCalculateDays.addEventListener("click", function (e) {
                    var result = Saykor.Controls.TimeCalculator.daysBetween(startDatePicker.current, endDatePicker.current);
                    pnlResultMessage.innerHTML = result;
                    menu.hide();
                });
                pnlMenu.appendChild(btnCalculateDays);
                
                var btnCalculateHours = document.createElement("button");
                btnCalculateHours.textContent = "Calculate Hours";
                btnCalculateHours.addEventListener("click", function (e) {
                    var result = Saykor.Controls.TimeCalculator.daysBetween(startDatePicker.current, endDatePicker.current) * 24;
                    pnlResultMessage.innerHTML = result;
                    menu.hide();
                });
                pnlMenu.appendChild(btnCalculateHours);
                
                var btnCalculateDaysHours = document.createElement("button");
                btnCalculateDaysHours.textContent = "Calculate Days & Hours";
                btnCalculateDaysHours.addEventListener("click", function (e) {
                    var resultDays = Saykor.Controls.TimeCalculator.daysBetween(startDatePicker.current, endDatePicker.current);
                    var resultHours = Saykor.Controls.TimeCalculator.hoursBetween(startTimePicker.current, endTimePicker.current);
                    pnlResultMessage.innerHTML = "Days: " + resultDays + " Hours: " + resultHours;
                    menu.hide();
                });
                pnlMenu.appendChild(btnCalculateDaysHours);
                
                document.body.appendChild(pnlMenu);

                var pnlResultMessage = document.createElement("div");
                pnlButtons.appendChild(pnlResultMessage);
            }
        }, {
            daysBetween: function(date1, date2) {
                //Get 1 day in milliseconds
                var oneDay = 1000 * 60 * 60 * 24;

                // Convert both dates to milliseconds
                var date1Ms = date1.getTime();
                var date2Ms = date2.getTime();

                // Calculate the difference in milliseconds
                var differenceMs = date2Ms - date1Ms;

                // Convert back to days and return
                return Math.round(differenceMs / oneDay);
            },
            hoursBetween: function(date1, date2) {
                //Get 1 hour in milliseconds
                var oneHour = 1000 * 60 * 60;

                // Convert both dates to milliseconds
                var date1Ms = date1.getTime();
                var date2Ms = date2.getTime();

                // Calculate the difference in milliseconds
                var differenceMs = date2Ms - date1Ms;

                // Convert back to days and return
                return Math.round(differenceMs / oneHour);
            }
        })
});