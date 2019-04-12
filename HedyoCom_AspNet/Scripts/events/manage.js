(function () {
    $(document).ready(function () {
        setTimeout(function () {
            $('#eventtimepicker').datetimepicker({
                format: 'DD/MM/YYYY HH:mm',
                locale: 'tr'
            });
        }, 10);
    });


    function initMap() {
        var geocoder = new google.maps.Geocoder();
        var map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 6
        });
        var eventInput = document.getElementById('Place');
        var autocomplete = new google.maps.places.Autocomplete(eventInput);

        autocomplete.addListener('place_changed', function onPlaceChangedHandler() {
            console.log('asdsadsa');
            var place = autocomplete.getPlace();

            eventInput.value = place.formatted_address;
            userPositionMarker.setPosition(place.geometry.location);
            map.setCenter(place.geometry.location);
        })

        var userPositionMarker = new google.maps.Marker({
            map: map,
            position: {
                lat: 39.9333635,
                lng: 32.85974190000002,
            },
            draggable: true,
        });

        // add event listener for marker drag
        userPositionMarker.addListener('dragend', function (coords) {
            setAddress(coords.latLng, geocoder);
        });

        // prevent submit form on enter when user click enter for select result from autocomplete
        google.maps.event.addDomListener(eventInput, 'keydown', function (event) {
            if (event.keyCode === 13) {
                event.preventDefault();
            }
        }); 

        function geocodeAddress(geocoder, resultsMap) {
            var address = document.getElementById('Place').value;
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status === 'OK') {
                    resultsMap.setCenter(results[0].geometry.location);
                    userPositionMarker.setPosition(results[0].geometry.location);
                } else {
                    alert('Geocode was not successful for the following reason: ' + status);
                }
            });
        }
        function setAddress(pos, geocoder) {
            geocoder.geocode({ 'location': pos }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        eventInput.value = results[0].formatted_address;
                    } else {
                        window.alert('No results found');
                    }
                } else {
                    window.alert('Geocoder failed due to: ' + status);
                }
            });
        }

        geocodeAddress(geocoder, map);
    }

    initMap();
})();