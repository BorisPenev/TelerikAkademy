/// <reference path="../libs/underscore.js" />
/// <reference path="../libs/angular.js" />

angular.module("forum", [])
	.config(["$routeProvider", function ($routeProvider) {
		$routeProvider
			.when("/posts", { templateUrl: "/UserControls/all-posts.html", controller: PostsController })
    	    .when("/posts/create", { templateUrl: "/UserControls/create-posts.html", controller: PostsController })
			.when("/category/:id/posts", { templateUrl: "/UserControls/posts-in-category.html", controller: CategoryController })
			.otherwise({ redirectTo: "/" });
	}]);
