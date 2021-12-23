function createMap() {
    $("#map").kendoMap({
        zoom: 2,
        layers: [{
            type: "shape",
            dataSource: {
                type: "geojson",
                transport: {
                    read: "../Content/countries-sales.geo.json"
                }
            },
            style: {
                fill: {
                    color: "#1996E4"
                },
                stroke: {
                    color: "#FFFFFF"
                }
            }
        }],
        shapeCreated: onShapeCreated,
        shapeFeatureCreated: onShapeFeatureCreated,
        shapeMouseEnter: onShapeMouseEnter,
        shapeMouseLeave: onShapeMouseLeave,
        shapeClick: onShapeClick,
    });

    $("#map").unbind("mousewheel");
    $("#map").unbind("DOMMouseScroll");

    initUSA();
}

function onShapeClick(e) {
    e.shape.options.set("fill.color", "#0c669f");
    e.shape.options.set("stroke.color", "black");
    e.shape.dataItem.properties.selected = true;

    $('#countryName').text(e.shape.dataItem.properties.name);

    $("#myBtn").trigger('click');

    DotNet.invokeMethodAsync('SalesDashboardBlazor', 'GetCountryCustomers', e.shape.dataItem.properties.name)
        .then(message => {
            $('#countryCustomers').text(message);
        });
}

function onShapeCreated(e) {
    if (e.shape.dataItem.id == "USA") {
        //e.shape.dataItem.properties.selected = true;
        //onShapeClick(е);
    }
}

function onShapeFeatureCreated(e) {
    e.group.options.tooltip = {
        content: e.properties.name,
        position: "cursor",
        offset: 10,
        width: 80
    };
}

function onShapeMouseEnter(e) {
    e.shape.options.set("fill.color", "#0c669f");
}

function onShapeMouseLeave(e) {
    e.shape.options.set("fill.color", "#1996E4");

    if (!e.shape.dataItem.properties.selected) {
        e.shape.options.set("stroke.color", "white");
    }
}

//define js function that could be called from razor, result is returned to razor
function getSelectedCountry() {
    return $('#countryName').text();
}

function initUSA() {
    $('#countryName').text('USA');

    DotNet.invokeMethodAsync('SalesDashboardBlazor', 'GetCountryCustomers', 'USA')
        .then(message => {
            $('#countryCustomers').text(message);
        });

    $("#myBtn").trigger('click');
}