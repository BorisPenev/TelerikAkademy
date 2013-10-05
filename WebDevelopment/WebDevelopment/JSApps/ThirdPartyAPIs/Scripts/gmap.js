$(document).ready(function () {
    var cities = ["London", "Rome", "Paris", "Berlin", "Athen", "Sofia", "Moscow", "Shenzhen", "Beijing", "Tokyo"];
    var index = 0;
    
    var map;
    var z = 7;

    var nav = $('#pnlNav');
    for (var i = 0; i < cities.length; i++) {
        nav.append('<li id='+ i +'>' + cities[i] + '</li>');
    }
    nav.find('li').click(function(ev) {
        GetLocation(cities[ev.target.id]);
    });

    function initialize() {
        var mapOptions = {
            zoom: z,
            //center: new google.maps.LatLng(x, y),
            mapTypeId: google.maps.MapTypeId.ROADMAP //ROADMAP
        };
        map = new google.maps.Map(document.getElementById('map-canvas'),
            mapOptions);
    }

    initialize();
    GetLocation(cities[index]);
    
    function GetLocation(city) {
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': city }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var latitude = results[0].geometry.location.lat();
                var longitude = results[0].geometry.location.lng();
                
                var pos = new google.maps.LatLng(latitude, longitude);

                var infowindow = new google.maps.InfoWindow({
                    map: map,
                    position: pos,
                    content: "Latidude: " + latitude + "<br/>Longitude: " + longitude
                });
                
                map.setCenter(pos);
                //map.panTo(pos);
                //map.setZoom(z);
            } else {
                alert("Request failed.");
            }
            
        });
    };

    $('#btnPrevious').click(function() {
        if (index <= 0) {
            index = cities.length - 1;
        } else {
            index--;
        }
        GetLocation(cities[index]);
    });
    
    $('#btnNext').click(function () {
        if (index >= cities.length - 1) {
            index = 0;
        } else {
            index++;
        }
        GetLocation(cities[index]);
    });
});