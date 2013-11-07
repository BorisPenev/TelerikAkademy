/// <reference path="libs/_references.js" />

$(function() {
    var appLayout = new kendo.Layout('<div id="main-content"></div>');
    var data = persisters.get("api/");
    vmFactory.setPersister(data);
    kendo.culture("en-GB");

    //if (localStorage.getItem("isAdmin") == 2) {
    //    $("#adinBtn").remove();
    //    $("#navbar").append('<li><a id="adinBtn" href="Scripts/admin.html">Admin</a></li>');
    //}

    var router = new kendo.Router();
    router.route("/", function() {
        if (data.isUserLoggedIn()) {
            router.navigate("/appointments");
        } else {
            router.navigate("/login");
        }
    });

    router.route("/login", function() {
        if (data.isUserLoggedIn()) {
            router.navigate("/appointments");
        } else {
            viewsFactory.getLoginView().then(function(loginViewHtml) {
                var loginVm = vmFactory.getLoginVM(function() {
                    router.navigate("/appointments");
                }, function(err) {
                    console.log(err);
                });
                var view = new kendo.View(loginViewHtml, {
                    model: loginVm
                });
                appLayout.showIn("#main-content", view);
            });
        }
    });

    router.route("/appointments", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getAppointmentsView().then(function(appointmentsView) {
                vmFactory.getAppointmentsViewModel().then(function(vm) {
                    var view = new kendo.View(appointmentsView,
                        {
                            model: vm
                        });
                    appLayout.showIn("#main-content", view);
                    kendo.bind($("#grid"), vm);

                }, function(err) {
                    console.log(err);
                });
            });
        }
    });

    router.route("/comming-appointments", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getCommingAppointmentsView().then(function(commingAppointmentsView) {
                vmFactory.getCommingAppointmentsViewModel().then(function(vm) {
                    var view = new kendo.View(commingAppointmentsView,
                        {
                            model: vm
                        });
                    appLayout.showIn("#main-content", view);
                    kendo.bind($("#grid"), vm);

                }, function(err) {
                    console.log(err);
                });
            });
        }
    });

    router.route("/appointments-by-date", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getAppointmentsByDateView().then(function(appointmentsByDateView) {
                var view = new kendo.View(appointmentsByDateView);
                appLayout.showIn("#main-content", view);
                $("#datepicker").kendoDatePicker();
                var datepicker = $("#datepicker").data("kendoDatePicker");
                datepicker.bind("change", function() {
                    var pickedDate = kendo.toString(this.value(), 'MM-dd-yyyy'); //"dd-MM-yyyy"
                    router.navigate("/appointments-by-date/" + pickedDate);
                });
            });
        }
    });

    router.route("/appointments-by-date/:d", function(d) {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getAppointmentsByDateView().then(function(appointmentsByDateView) {
                vmFactory.getAppointmentsByDateViewModel(d).then(function(vm) {
                    var view = new kendo.View(appointmentsByDateView,
                        {
                            model: vm
                        });
                    appLayout.showIn("#main-content", view);
                    kendo.bind($("#grid"), vm);
                    $("#datepicker").hide();
                }, function(err) {
                    console.log(err);
                });
            });
        }
    });

    router.route("/appointments-today", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getAppointmentsView().then(function(appointmentsView) {
                vmFactory.getAppointmentsTodayViewModel().then(function(vm) {
                    var view = new kendo.View(appointmentsView,
                        {
                            model: vm
                        });
                    appLayout.showIn("#main-content", view);
                    kendo.bind($("#grid"), vm);
                }, function(err) {
                    console.log(err);
                });
            });
        }
    });

    router.route("/appointments-current", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getAppointmentsView().then(function(appointmentsView) {
                vmFactory.getAppointmentsCurrentViewModel().then(function(vm) {
                    var view = new kendo.View(appointmentsView,
                        {
                            model: vm
                        });
                    appLayout.showIn("#main-content", view);
                    kendo.bind($("#grid"), vm);
                }, function(err) {
                    console.log(err);
                });
            });
        }
    });

    router.route("/create-appointment", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getCreateAppointmentsView().then(function(createAppointmentsView) {
                var createAppointmentsViewModel = vmFactory.getCreateAppointmentsViewModel();
                var view = new kendo.View(createAppointmentsView,
                    {
                        model: createAppointmentsViewModel
                    });
                appLayout.showIn("#main-content", view);
                $("#calendar").kendoCalendar();
                var calendar = $("#calendar").data("kendoCalendar");
                calendar.bind("change", createAppointmentsViewModel.calendarOnChange);
            });
        }
    });

    router.route("/todo-lists", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getTodosView().then(function(todoListView) {
                var todoListViewModel = vmFactory.getTodoListViewModel();
                var view = new kendo.View(todoListView,
                    {
                        model: todoListViewModel
                    });
                appLayout.showIn("#main-content", view);
                todoListViewModel.load();
                kendo.bind($('#grid'), todoListViewModel);


                var grdAllTodos = $('#grdAllTodos').data('kendoGrid');
                grdAllTodos.bind("change", function(e) {
                    var selectedItem = this.dataItem(this.select());
                    router.navigate("/todo-lists/" + selectedItem.id);
                });
            });
        }
    });

    router.route("/todo-lists/:todoId", function (todoId) {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getTodosByListIdView().then(function (todoListByListIdView) {
                var todoListByListIdViewModel = vmFactory.getTodoListByListIdViewModel();
                var view = new kendo.View(todoListByListIdView,
                    {
                        model: todoListByListIdViewModel
                    });
                appLayout.showIn("#main-content", view);
                todoListByListIdViewModel.load(todoId);

                var grdTodos = $('#grdTodos').data('kendoGrid');
                grdTodos.bind("change", function (e) {
                    var selectedItem = this.dataItem(this.select());
                    todoListByListIdViewModel.isDoneChanged(selectedItem);
                });
            });
        }
    });

    router.route("/logout", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            vmFactory.getLogout().then(function() {
                router.navigate("/login");
            });
        }
    });

    $(function() {
        appLayout.render("#app");
        router.start();
    });
});

String.prototype.escape = function () {
    var tagsToReplace = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;'
    };
    return this.replace(/[&<>]/g, function (tag) {
        return tagsToReplace[tag] || tag;
    });
};