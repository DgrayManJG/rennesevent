var map;
var panel;
var initialize;
var calculate;
var direction;

initialize = function(){
  //var latLng = new google.maps.LatLng(48.116931, -1.674894); // Correspond au coordonnÃƒÂ©es de Rennes
  var myOptions = {
    zoom      : 14, // Zoom par dÃƒÂ©faut
    center    : latLng, // CoordonnÃƒÂ©es de dÃƒÂ©part de la carte de type latLng 
    mapTypeId : google.maps.MapTypeId.TERRAIN, // Type de carte, diffÃƒÂ©rentes valeurs possible HYBRID, ROADMAP, SATELLITE, TERRAIN
    maxZoom   : 20
  };
  
  map = new google.maps.Map(document.getElementById('map'), myOptions);

    panel = document.getElementById('panel');

    var tabLatLngParking1 = tabParking1[0].split(",");
    tabLatLngParking1[0] = parseFloat(tabLatLngParking1[0]);
    tabLatLngParking1[1] = parseFloat(tabLatLngParking1[1]);

    var tabLatLngParking2 = tabParking2[0].split(",");
    tabLatLngParking2[0] = parseFloat(tabLatLngParking2[0]);
    tabLatLngParking2[1] = parseFloat(tabLatLngParking2[1]);

    var tabLatLngParking3 = tabParking3[0].split(",");
    tabLatLngParking3[0] = parseFloat(tabLatLngParking3[0]);
    tabLatLngParking3[1] = parseFloat(tabLatLngParking3[1]);

    var iconParking = {
        url: "../../Content/images/parking-icon.png", // url 
        scaledSize: new google.maps.Size(50, 50), // scaled size 
        origin: new google.maps.Point(0, 0), // origin 
        anchor: new google.maps.Point(0, 0) // anchor 
    };

    /*var iconEvenement = {
        url: "../../Content/images/evenements/" + imageEvenement, // url 
        scaledSize: new google.maps.Size(80, 60), // scaled size 
        origin: new google.maps.Point(0, 0), // origin 
        anchor: new google.maps.Point(0, 0) // anchor 
    };*/
    

    var marker = new google.maps.Marker({ 
        position: new google.maps.LatLng(tabLatLngParking1[0], tabLatLngParking1[1]),
        map: map,
        title: tabParking1[1],
        animation: google.maps.Animation.DROP,
        icon: iconParking
    });
    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(tabLatLngParking2[0], tabLatLngParking2[1]),
        map: map,
        title: tabParking2[1],
        animation: google.maps.Animation.DROP,
        icon: iconParking
    });
    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(tabLatLngParking3[0], tabLatLngParking3[1]),
        map: map,
        title: tabParking3[1],
        animation: google.maps.Animation.DROP,
        icon: iconParking
    });


    var marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
  
  var contentMarker = [
      '<div id="containerTabs">',
      '<div id="tabs">',
      '<ul>',
        '<li><a href="#tab-1"><span>Lorem</span></a></li>',
        '<li><a href="#tab-2"><span>Ipsum</span></a></li>',
        '<li><a href="#tab-3"><span>Dolor</span></a></li>',
      '</ul>',
      '<div id="tab-1">',
        '<h3>Lille</h3><p>Suspendisse quis magna dapibus orci porta varius sed sit amet purus. Ut eu justo dictum elit malesuada facilisis. Proin ipsum ligula, feugiat sed faucibus a, <a href="http://www.google.fr">google</a> sit amet mauris. In sit amet nisi mauris. Aliquam vestibulum quam et ligula pretium suscipit ullamcorper metus accumsan.</p>',
      '</div>',
      '<div id="tab-2">',
       '<h3>Aliquam vestibulum</h3><p>Aliquam vestibulum quam et ligula pretium suscipit ullamcorper metus accumsan.</p>',
      '</div>',
      '<div id="tab-3">',
        '<h3>Pretium suscipit</h3><ul><li>Lorem</li><li>Ipsum</li><li>Dolor</li><li>Amectus</li></ul>',
      '</div>',
      '</div>',
      '</div>'
  ].join('');

  var infoWindow = new google.maps.InfoWindow({
    content  : contentMarker,
    position : latLng
  });
  
  //Decommenter pour afficher un info bulle au clic sur le marqueur
  /*google.maps.event.addListener(marker, 'click', function() {
    infoWindow.open(map,marker);
  });*/
  
  google.maps.event.addListener(infoWindow, 'domready', function(){ // infoWindow est biensÃƒÂ»r notre info-bulle
    jQuery("#tabs").tabs();
  });
  
  
  direction = new google.maps.DirectionsRenderer({
    map   : map,
    panel : panel // Dom element pour afficher les instructions d'itinÃƒÂ©raire
  });

};

calculate = function(){
    origin      = document.getElementById('origin').value; // Le point dÃƒÂ©part
    destination = document.getElementById('destination').value; // Le point d'arrivÃƒÂ©
    if(origin && destination){
        var request = {
            origin      : origin,
            destination : destination,
            travelMode  : google.maps.DirectionsTravelMode.DRIVING // Mode de conduite
        }
        var directionsService = new google.maps.DirectionsService(); // Service de calcul d'itinÃƒÂ©raire
        directionsService.route(request, function(response, status){ // Envoie de la requÃƒÂªte pour calculer le parcours
          //console.log(response);
            if(status == google.maps.DirectionsStatus.OK){
                direction.setDirections(response); // Trace l'itinÃƒÂ©raire sur la carte et les diffÃƒÂ©rentes ÃƒÂ©tapes du parcours
            }
        });
        $('#panel').slideDown();
    }
};

initialize();