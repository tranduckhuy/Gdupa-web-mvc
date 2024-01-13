$(function () {
    $('#sherah-map').vectorMap({
        map: 'world_mill_en',
        backgroundColor: 'transparent',
        panControl: false,
        zoomControl: false,
        regionStyle: {
            initial: {
                fill: '#C5C5C5'
            },
            hover: {
                fill: '#09AD95'
            }
        },
        showTooltip: true
    });
});
