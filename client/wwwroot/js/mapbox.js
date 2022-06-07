window.initMapbox = (token, _dotNetHelper) => {
  mapboxgl.accessToken = token;
  window.dotNetHelper = _dotNetHelper;

  window.map = new mapboxgl.Map({
    container: "map", // container ID
    style: "mapbox://styles/mapbox/streets-v11", // style URL
    center: [4.8952902, 52.3702904], // starting position [lng, lat]
    zoom: 12, // starting zoom
  });
};

window.addListings = (listings) => {
  // Remove old layer and source
  if (window.map.getLayer("listings")) {
    window.map.removeLayer("listings");
    window.map.removeSource("listings");
  }

  // Mapbox data source
  window.map.addSource("listings", {
    type: "geojson",
    data: {
      type: "FeatureCollection",
      features: listings.map((l) => ({
        type: "Feature",
        properties: {
          id: l.id,
          name: l.name,
          color: mapRoomTypeToColor(l.roomType),
        },
        geometry: {
          type: "Point",
          coordinates: [l.longitude, l.latitude],
        },
      })),
    },
  });

  // Mapbox layer
  window.map.addLayer({
    id: "listings",
    type: "circle",
    source: "listings",
    paint: {
      "circle-radius": 1,
      "circle-opacity": 0.75,
      "circle-color": ["get", "color"],
    },
  });

  // Change listings' circle radius based on map zoom level
  window.map.on("zoom", () => {
    map.setPaintProperty("listings", "circle-radius", {
      base: 1.75,
      stops: [
        [12, 2],
        [22, 180],
      ],
    });
  });

  // When a click event occurs on a feature in the listings layer, open a popup at the
  // location of the feature, with name HTML from its properties.
  // https://docs.mapbox.com/mapbox-gl-js/example/popup-on-click/
  window.map.on("click", "listings", (e) => {
    window.dotNetHelper.invokeMethodAsync(
      "handleListingClick",
      e.features[0].properties.id.toString()
    );

    // // Copy coordinates array.
    const coordinates = e.features[0].geometry.coordinates.slice();
    const name = e.features[0].properties.name;

    // Ensure that if the map is zoomed out such that multiple
    // copies of the feature are visible, the popup appears
    // over the copy being pointed to.
    while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
      coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
    }

    new mapboxgl.Popup().setLngLat(coordinates).setHTML(name).addTo(map);
  });

  // Change the cursor to a pointer when the mouse is over the listings layer.
  window.map.on("mouseenter", "listings", () => {
    map.getCanvas().style.cursor = "pointer";
  });

  // Change it back to a pointer when it leaves.
  window.map.on("mouseleave", "listings", () => {
    map.getCanvas().style.cursor = "";
  });
};

window.addMarker = (listing) => {
  const marker = new mapboxgl.Marker()
    .setLngLat([listing.longitude, listing.latitude])
    .addTo(window.map);

  marker
    .getElement()
    .addEventListener("click", () =>
      alert(
        `Visit location #${listing.id}, also known as '${listing.name}', all the way over here!`
      )
    );
};

/** HELPER FUNCTIONS **/
const mapRoomTypeToColor = (roomType) => {
  switch (roomType) {
    case "Entire home/apt":
      return "#ffc107";
    case "Private room":
      return "#0d6efd";
    case "Shared room":
      return "#dc3545";
    default:
      return "#000000";
  }
};
