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
    lengthChange: true,
    scrollCollapse: true,
    paging: true,
    language: {
        paginate: {
            next: '<i class="fas fa-angle-right"></i>', // Font Awesome class for next button
            previous: '<i class="fas fa-angle-left"></i>' // Font Awesome class for previous button
        },
        lengthMenu: 'Showing _MENU_',
        searchPlaceholder: 'Search...',
        search: '<span class="sherah-data-table-label">Search</span>'
    }
});