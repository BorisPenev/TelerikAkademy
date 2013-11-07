/// <reference path="../libs/_references.js" />
/// <reference path="dataLayer.js" />

window.vmFactory = (function () {
	var data = null;

	function getLoginViewModel(successCallback) {		
		var viewModel = {
		    username: "DonchoMinkov",
		    email: "saykor@gmail.com",
			password: "123456q",
			login: function () {
			    var user = {
			        username: this.get("username").escape(),
			        email: this.get("email").escape(),
			        authCode: this.get("password")
			    };

				data.users.login(user)
					.then(function () {
					    //if (localStorage.getItem("isAdmin") == 2) {
					    //    $("#adinBtn").remove();
					    //    $("#navbar").append('<li><a id="adinBtn" href="Scripts/admin.html">Admin</a></li>');
					    //}

						if (successCallback) {
							successCallback();
						}
					});
			},
			register: function () {
			    var user = {
			        username: this.get("username").escape(),
			        email: this.get("email").escape(),
			        authCode: this.get("password")
			    };

				data.users.register(user)
					.then(function () {
					    //if (localStorage.getItem("isAdmin") == 2) {
					    //    $("#adinBtn").remove();
					    //    $("#navbar").append('<li><a id="adinBtn" href="Scripts/admin.html">Admin</a></li>');
					    //}

						if (successCallback) {
							successCallback();
						}
					});
			}
		};
		return kendo.observable(viewModel);
	};

	function getLogout() {
	    return data.users.logout();
	}
    
	function getAppointmentsViewModel() {
	    return data.appointments.getAll().then(function (appointments) {
	        var viewModel = {
	            dataSource: appointments
	        };

	        return kendo.observable(viewModel);
	    });
	}

	function getCreateAppointmentsViewModel() {
	    var viewModel = {
	        id: 0,
	        owner: "",
	        subject: "enter subject",
	        description: "enter description",
	        appointmentDate: "",
	        duration: "30",
	        resultMsg: "",
	        create: function () {
	            var self = this;
	            
	            var appointment = {
	                subject: this.get("subject").escape(),
	                description: this.get("description").escape(),
	                appointmentDate: viewModel.appointmentDate,//this.get("appointmentDate"),
	                duration: this.get("duration").escape(),
	            };

	            if (isNaN(appointment.duration)) {
	                self.resultMsg = "please enter a duration in minutes";
	            } else {
	                data.appointments.create(appointment)
	                    .then(function(result) {
	                        if (result) {
	                            self.set("id", result.id);
	                            self.set("owner", result.owner);
	                            self.set("resultMsg", "Save completed");
	                        }
	                        //if (successCallback) {
	                        //    successCallback();
	                        //}
	                    });
	            }
	        },
	        calendarOnChange:function() {
	            var view = this.view();
	            var date = kendo.toString(this.value(), 'd');//"dd-MM-yyyy"
	            viewModel.appointmentDate = date;
	        }
	    };
	    return kendo.observable(viewModel);
	};

	function getCommingAppointmentsViewModel() {
	    return data.appointments.getComming().then(function (appointments) {
	        var viewModel = {
	            dataSource: appointments
	        };

	        return kendo.observable(viewModel);
	    });
	}
    
	function getAppointmentsByDateViewModel(date) {
	    return data.appointments.getByDate(date).then(function (appointments) {
	        var viewModel = {
	            dataSource: appointments
	        };

	        return kendo.observable(viewModel);
	    });
	};
    
	function getAppointmentsTodayViewModel() {
	    return data.appointments.getToday().then(function (appointments) {
	        var viewModel = {
	            dataSource: appointments
	        };

	        return kendo.observable(viewModel);
	    });
	};
    
	function getAppointmentsCurrentViewModel() {
	    return data.appointments.getCurrent().then(function (appointments) {
	        var viewModel = {
	            dataSource: appointments
	        };

	        return kendo.observable(viewModel);
	    });
	};

	function getTodoListViewModel() {
	    var viewModel = {
	        id: 0,
	        owner: "",
	        newTodoText: "todo text",
	        newList: {
	            title: "list title",
	            todos: []
	        },
	        dataSource: [],
	        resultMsg: "",
	        addTodo: function () {
	            var todo = {
	                text: this.get("newTodoText")
	            };
	            this.newList.todos.push(todo);
	        },
	        createList: function () {
	            var self = this;
	            data.todos.create(this.get("newList"))
                    .then(function (result) {
                        if (result) {
                            var list = {
                                id: result.id,
                                title: self.get("newList").title
                            };
                            self.dataSource.push(list);
                            self.set("resultMsg", "Save completed");
                        }
                    });
	        },
	        load: function () {
	            var self = this;
	            data.todos.getAll().then(function (todoLists) {
	                for (var i = 0; i < todoLists.length; i++) {
	                    self.dataSource.push(todoLists[i]);
	                }
	            });
	        },
	    };
	    
	    return kendo.observable(viewModel);
	};
    
	function getTodoListByListIdViewModel() {
	    var viewModel = {
	        id: 0,
	        owner: "",
	        title: "",
	        newTodoText: "todo text",
	        dataSource: new kendo.data.DataSource({
	            data: []
	        }),
	        resultMsg: "",
	        addTodo: function () {
	            var self = this;
	            var todo = {
	                text: this.get("newTodoText").escape()
	            };
	            data.todos.createTodosByListId(this.id, todo).then(function (result) {
                        if (result) {
                            location.reload();
                        }
                    });
	        },
	        load: function (listId) {
	            var self = this;
	            data.todos.getTodosByListId(listId).then(function (list) {
	                self.set("id", list.id);
	                self.set("title", list.title);
	                for (var i = 0; i < list.todos.length; i++) {
	                    self.dataSource.add(list.todos[i]);
	                }
	            });
	        },
	        isDoneChanged: function (todoItem) {
	            var self = this;
	            data.todos.changeTodoStatus(todoItem.id).then(function (list) {
	                if (list) {
	                    todoItem.isDone = !todoItem.isDone;
	                    self.dataSource.fetch();
	                }
	                self.dataSource.fetch();
	            });
	        },
	    };

	    return kendo.observable(viewModel);
	};
    
	return {
	    getLoginVM: getLoginViewModel,
	    getLogout: getLogout,
		getAppointmentsViewModel: getAppointmentsViewModel,
		getCreateAppointmentsViewModel: getCreateAppointmentsViewModel,
		getCommingAppointmentsViewModel: getCommingAppointmentsViewModel,
		getAppointmentsByDateViewModel: getAppointmentsByDateViewModel,
		getAppointmentsTodayViewModel: getAppointmentsTodayViewModel,
		getAppointmentsCurrentViewModel: getAppointmentsCurrentViewModel,
		getTodoListViewModel: getTodoListViewModel,
		getTodoListByListIdViewModel: getTodoListByListIdViewModel,
		setPersister: function (persister) {
			data = persister;
		}
	};
}());