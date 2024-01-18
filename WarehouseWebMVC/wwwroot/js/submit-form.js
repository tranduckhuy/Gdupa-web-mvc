document.querySelector('#formAddProduct').addEventListener('submit', function (e) {
    e.preventDefault();

    var formData = new FormData(this);
    formData.append('proImage1', document.getElementById('proImage1').value);
    formData.append('proImage2', document.getElementById('proImage2').value);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Product/AddProduct'); 
        if (xhr.status === 200) {
            console.log(xhr.responseText);
        }
    });
    xhr.send(formData);
});