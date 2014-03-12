    (function(Embro,$,undefined) {

        $(function () {

            $('#startStitchEmulationButton').click(function (e) {
                var needle = 0;
                var coord = 0;
                var needleCount;
                var coordQty;
                var x,y;                
                var coordBlocks;
                var svg,g;
                var $polyline;                
                var currentPoly;
                var tick;
                e.preventDefault();

                var json = Embro.Json;
                //$.post(this.href, function (json) {
                //    console.log(json);               
                //    coordBlocks = json;
                //    needleCount = coordBlocks.length;
                //    console.log(needleCount);
                //    coordQty = coordBlocks[0].needle.length;
                //    console.log(coordQty);
                //}, 'json');
                
                coordBlocks = json;
                needleCount = coordBlocks.length;
                console.log(needleCount);
                coordQty = coordBlocks[0].needle.length;
                
                svg = $("div#embro-detail svg");
                $("div#embro-detail svg g polyline").remove();                               
                svg.attr("id", "embro-detail-svg");                
                g = $("div#embro-detail svg g");
                g.attr("id", "svg-g-animated");
                tick = window.setTimeout(                    startDrawing             , 10);
              
                function startDrawing()
                {                   
                    tick = window.setInterval(function () {
                        appendCoord();
                    }, 1);
                }
                
                function appendCoord() {                   
                    if ((coord == 0)) {
                        
                            $polyline = makeSvg('polyline',
                                { id: 'needle_' + needle, points: '', fill: 'none', stroke: "#" + coordBlocks[needle].color.slice(2, 8), 'stroke-width': 2 });
                            currentPoly = $polyline;
                            document.getElementById("svg-g-animated").appendChild(currentPoly);
                         
                            coordQty = coordBlocks[needle].needle.length;                            
                    }

                    if ((coordQty > 0) && (coordQty > coord)) {
                        var points = $(currentPoly).attr("points");
                        x = coordBlocks[needle].needle[coord].X;
                        y = coordBlocks[needle].needle[coord].Y;

                        if (points.length > 0) {
                            $(currentPoly).attr("points", points + "," + x + " " + y);
                        } else {
                            $(currentPoly).attr("points", x + " " + y);
                        }
                        coord += 1;
                    }                    
                    else {
                        // если последний стежок в нити
                        needle += 1;
                        // если обработали все нити    
                        if (needle == needleCount) {                            
                            clearTimeout(tick);
                            return;
                        };
                        coord = 0;
                    }
                };

                function makeSvg(tag, attrs) {
                    var el = document.createElementNS('http://www.w3.org/2000/svg', tag);
                    for (var k in attrs)
                        el.setAttribute(k, attrs[k]);
                    return el;
                }


            });

            return;
        });
    }(window.Embro = window.Embro || {},jQuery));