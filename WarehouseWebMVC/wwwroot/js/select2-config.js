function initializeSelectSearch(selector, placeholderText) {
    $(selector).select2({
        placeholder: placeholderText,
        allowClear: true,
        closeOnSelect: true,
        selectOnClose: false
    });
    $(selector).one('select2:open', function (e) {
        var searchInputs = document.querySelectorAll('.select2-search__field');
        if (searchInputs) {
            searchInputs.forEach(function (input) {
                input.setAttribute('placeholder', 'Search...');
            });
        }
    });
}

