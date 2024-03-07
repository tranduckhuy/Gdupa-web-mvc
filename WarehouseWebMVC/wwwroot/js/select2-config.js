function initializeSelectSearch(selector, placeholderText) {
    $(selector).select2({
        placeholder: placeholderText,
        allowClear: true,
        closeOnSelect: true,
        selectOnClose: false
    });
    $(selector).one('select2:open', function (e) {
        var searchInput = document.querySelector('.select2-search__field');
        if (searchInput) {
            searchInput.setAttribute('placeholder', "Search...");
        }
    });
}

