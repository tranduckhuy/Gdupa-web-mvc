tinymce.init({
    selector: '.tinymce',
    width: 500,
    height: 300,
    plugins: [
        'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'prewiew', 'anchor', 'pagebreak',
        'searchreplace', 'wordcount', 'visualblocks', 'code', 'fullscreen', 'insertdatetime', 'media',
        'table', 'emoticons', 'template', 'codesample'
    ],
    toolbar: 'undo redo | styles | bold italic underline | alignleft aligncenter alignright alignjustify |' +
            'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
            'forecolor backcolor emoticons',
    menu: {
        favs: {title: 'menu', items: 'code visualaid | searchreplace | emoticons'}
    },
    menubar: false,
    content_style: 'body{font-family:Helvetica,Arial,sans-serif; font-size:16px}'
});