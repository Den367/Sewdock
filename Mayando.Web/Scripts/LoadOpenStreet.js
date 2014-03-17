var centerCircle;
var map;
var vlayer;
var vector;
function GetMap() {
   
    map = new OpenLayers.Map("map");//инициализация карты
   
    var mapnik = new OpenLayers.Layer.OSM();//создание слоя карты

    map.addLayer(mapnik);//добавление слоя
    vector = new OpenLayers.Layer.Vector('vector');
    map.addLayers([mapnik, vector]);
    map.setCenter(new OpenLayers.LonLat(31.83, 51.0532) //(широта, долгота)
          .transform(
            new OpenLayers.Projection("EPSG:4326"), // переобразование в WGS 1984
            new OpenLayers.Projection("EPSG:900913") // переобразование проекции
          ), 10 // масштаб    
        );

    vlayer = new OpenLayers.Layer.Vector("Editable");
    map.addLayer(vlayer);
    var zb = new OpenLayers.Control.ZoomBox({
        title: "Zoom box: zoom clicking and dragging",
        text: "Zoom"
    });
    var nav = new OpenLayers.Control.NavigationHistory({
        previousOptions: {
            title: "Go to previous map position",
            text: "Prev"
        },
        nextOptions: {
            title: "Go to next map position",
            text: "Next"
        },
        displayClass: "navHistory btn tabs-stacked"
    });
    
    var panel = new OpenLayers.Control.Panel({
        defaultControl: nav,
        createControlMarkup: function (control) {
            var button = document.createElement('button'),
            iconSpan = document.createElement('span'),
            textSpan = document.createElement('span');
            iconSpan.innerHTML = '&nbsp;';
            button.appendChild(iconSpan);
            if (control.text) textSpan.innerHTML = control.text;            
            button.appendChild(textSpan);
            button.Width = 2;
            
            return button;
        }
    });
    panel.addControls([
    nav,
    //new OpenLayers.Control.DrawFeature(vlayer, OpenLayers.Handler.Path,
    //{ title: 'Draw a feature', text: 'Draw' }),
    new OpenLayers.Control.ZoomToMaxExtent({
        displayClass: "btn tabs-stacked",
        title: "Zoom to the max extent",
        text: "World"
    })
    ]);
    var button = new OpenLayers.Control.Button({
        displayClass: "btn tabs-stacked",
        text: 'Locate',
        trigger: LocateMe        
    });  
   // var button1 = new OpenLayers.Control.Button({ displayClass: 'olControlButton1', trigger: Locate, title: 'Button is to be clicked' });
    // parent control must be added to the map
    map.addControl(nav);
    panel.addControls([nav.next, nav.previous, button]);
    //panel.addControls([button1]);
    

    map.addControl(panel);

    map.events.register('moveend', map, function () {
        var mapcenter = map.getCenter();
        vector.removeAllFeatures();
        var point = new OpenLayers.Geometry.Point(mapcenter.lon, mapcenter.lat);
        vector.addFeatures([new OpenLayers.Feature.Vector(point, {}, {
            strokeColor: '#11999999',
            strokeWidth: 3,
            fillOpacity: 0,
            pointRadius: 25
        }),
                new OpenLayers.Feature.Vector(
                   new OpenLayers.Geometry.Point(mapcenter.lon, mapcenter.lat),
                    {},
                    {
                        graphicName: 'cross',
                        strokeColor: '#777',
                        strokeWidth: 2,
                        fillOpacity: 0,
                        pointRadius: 10
                    }
                )
        ]);
        map.addLayer(vector);
        var latlon = mapcenter.transform(new OpenLayers.Projection("EPSG:900913"), new OpenLayers.Projection("EPSG:4326"));
        OpenLayers.Util.getElement("coord").innerHTML = 'GEO:' + latlon.lon.toFixed(5) + ', ' + latlon.lat.toFixed(5);
        $('input#Longitude').val(latlon.lon.toFixed(5));
        $('input[name="Latitude"]').val(latlon.lat.toFixed(5));
        
    });
    
    vector.removeAllFeatures();
   
   
}

function LocateMe() {

    var style = {
        fillColor: '#000',
        fillOpacity: 0.1,
        strokeWidth: 0
    };

    var pulsate = function (feature) {
        var point = feature.geometry.getCentroid(),
            bounds = feature.geometry.getBounds(),
            radius = Math.abs((bounds.right - bounds.left) / 2),
            count = 0,
            grow = 'up';

        var resize = function () {
            if (count > 16) {
                clearInterval(window.resizeInterval);
            }
            var interval = radius * 0.03;
            var ratio = interval / radius;
            switch (count) {
                case 4:
                case 12:
                    grow = 'down'; break;
                case 8:
                    grow = 'up'; break;
            }
            if (grow !== 'up') {
                ratio = -Math.abs(ratio);
            }
            feature.geometry.resize(1 + ratio, point);
            vector.drawFeature(feature);
            count++;
        };
        window.resizeInterval = window.setInterval(resize, 50, point, radius);
    };

    var geolocate = new OpenLayers.Control.Geolocate({
        bind: false,
        geolocationOptions: {
            enableHighAccuracy: false,
            maximumAge: 0,
            timeout: 7000
        }
    });
    map.addControl(geolocate);
    var firstGeolocation = true;
    geolocate.events.register("locationupdated", geolocate, function (e) {
        vector.removeAllFeatures();
        var circle = new OpenLayers.Feature.Vector(
            OpenLayers.Geometry.Polygon.createRegularPolygon(
                new OpenLayers.Geometry.Point(e.point.x, e.point.y),
                e.position.coords.accuracy / 2,
                40,
                0
            ),
            {},
            style
        );
        centerCircle = vector.addFeatures([
            new OpenLayers.Feature.Vector(
                e.point,
                {},
                {
                    graphicName: 'cross',
                    strokeColor: '#f00',
                    strokeWidth: 2,
                    fillOpacity: 0,
                    pointRadius: 10
                }
            ),
            circle
        ]);

        if (firstGeolocation) {
            map.zoomToExtent(vector.getDataExtent());
            pulsate(circle);
            firstGeolocation = false;
            this.bind = true;
        }
    });
    geolocate.events.register("locationfailed", this, function () {
        OpenLayers.Console.log('Location detection failed');
    });
    vector.removeAllFeatures();
    geolocate.deactivate();
    geolocate.watch = false;
    firstGeolocation = true;
    geolocate.activate();
};

function AddingMarkersAbility()
{
    var layerMarkers = new OpenLayers.Layer.Markers("Markers");//создаем новый слой маркеров
    map.addLayer(layerMarkers);//добавляем этот слой к карте
    map.events.register('click', map, function (e) {
        var size = new OpenLayers.Size(21, 25);//размер картинки для маркера
        var offset = new OpenLayers.Pixel(-(size.w / 2), -size.h); //смещение картинки для маркера
        var icon = new OpenLayers.Icon('/Images/smilies.png', size, offset);//картинка для маркера
        layerMarkers.addMarker(//добавляем маркер к слою маркеров
            new OpenLayers.Marker(map.getLonLatFromViewPortPx(e.xy), //координаты вставки маркера
            icon));//иконка маркера
    }); //добавление событие клика по карте
}
