$('#sherah-table__main-product').DataTable({
	searching: false,
	paging: false,
	lengthChange: false,
	info: false,
	scrollCollapse: true,
});

$('#sherah-table__vendor').DataTable({
    searching: true,
    info: false,
    lengthChange: false,
    scrollCollapse: true,
    paging: false,
    language: {
        searchPlaceholder: 'Search...',
        search: '<span class="sherah-data-table-label">Search</span>'
    }
});

