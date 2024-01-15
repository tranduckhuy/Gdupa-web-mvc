$('#sherah-table__vendor').DataTable({
	searching: true,
	info: true,
	lengthChange: true,
	lengthMenu: [5, 10, 15, 20],
	scrollCollapse: true,
	paging: false,
	language: {
		searchPlaceholder: 'Search...',
		search: '<span class="sherah-data-table-label" style="font-size: 16px; cursor: pointer;">Search</span>',
	}
});