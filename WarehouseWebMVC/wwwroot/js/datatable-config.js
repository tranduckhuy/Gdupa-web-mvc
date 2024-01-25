$('#sherah-table__main-product').DataTable({
	searching: false,
	paging: false,
	lengthChange: false,
	info: false,
	scrollCollapse: true,
});

$('#sherah-table__vendor').DataTable({
    searching: false,
    info: false,
    lengthChange: false,
    scrollCollapse: true,
    paging: false,
    language: {
        searchPlaceholder: 'Search...',
        search: '<span class="sherah-data-table-label">Search</span>'
    }
}); 

$('#sherah-table__orderv1').DataTable({
    searching: true,
    paging: false,
    lengthChange: false,
    info: false,
    scrollCollapse: true,
    language: {
        searchPlaceholder: 'Search...',
        search: '<span class="sherah-data-table-label">Search</span>'
    }
});