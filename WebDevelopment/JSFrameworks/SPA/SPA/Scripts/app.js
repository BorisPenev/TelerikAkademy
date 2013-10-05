/// <reference path="libs/require.js" />
/// <reference path="libs/sammy-0.7.4.js" />
/// <reference path="libs/jquery-2.0.3.js" />

(function () {

    require.config({
        paths: {
            jquery: "libs/jquery-2.0.3.min",
            sammy: "libs/sammy-0.7.4",
            controllers: "app/controller",
            providers: "app/dataProvider",
            "class": "libs/class"
        }
    });

    require(["jquery", "sammy", "controllers"], function ($, sammy, controllers) {
        
        var el = new Everlive('4h2qUbMFKWJrcIBf');
        
        //var username = "saykor";
        //var password = "test1234";
        //Everlive.$.Users.login(username, password,
        //    function (loginData) {
        //        console.log(JSON.stringify(loginData));
        //        var data = Everlive.$.data('Post');
        //        data.get()
        //            .then(function (data) {
        //                document.write(JSON.stringify(data));
        //            },
        //            function (error) {
        //                document.write(JSON.stringify(error));
        //            });
                
        //        //var tag = Everlive.$.data('Tag');
        //        //var multipleTagData = [{ 'Name': 'harper' }, { 'Name': 'lee' }];
        //        //tag.create(multipleTagData, function (tagData) {
        //        //    var tags = [];
        //        //    for (var i = 0; i < tagData.result.length; i++) {
        //        //        tags.push(tagData.result[i].Id);
        //        //    }
        //        //        var post = Everlive.$.data('Post');
        //        //        post.create({
        //        //            'Title': 'Harper Lee',
        //        //            'Content': 'test content',
        //        //            'Tags': tags
        //        //        },
        //        //            function (postData) {
        //        //                console.log(JSON.stringify(postData));
        //        //            },
        //        //            function (error) {
        //        //                console.log(JSON.stringify(error));
        //        //            }
        //        //        );
        //        //    },
        //        //    function (error) {
        //        //        console.log(JSON.stringify(error));
        //        //    }
        //        //);
                
        //    },
        //    function (error) {
        //        document.write(JSON.stringify(error));
        //    }
        //);
        
        var controller = controllers.get(el);
        
		var app = sammy("#main-content",function () {
			this.get("#/", function () {
			    this.redirect("#/home");
			});
		    
			this.get("#/home", function () {
			    if (!controller.loginCheck()) {
			        this.redirect("#/login");
			    } else {
			        this.redirect("#/posts");
			    }
			});

			this.get("#/posts", function () {
			    controller.getAllPosts();
			});
		    
			this.get("#/posts/{tags}", function () {

			});
		    
			this.get("#/post/:postId", function () {
			    controller.loadPost(this.params['postId']);
			});
		    
			this.get("#/post/{postId}/comment", function () {
			});
		    
			this.get("#/login", function () {
			    controller.loadLogin();
			});
		    
			this.get("#/logout", function () {
			    controller.logout();
			});
		});
		app.run("#/");		
		
	});
}());