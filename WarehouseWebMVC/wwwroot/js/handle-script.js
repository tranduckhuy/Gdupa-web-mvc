﻿function handleDelete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this action!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it !'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Deleted!',
                text: 'Product has been deleted :3',
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    var currentPath = window.location.pathname;
                    var newPath = currentPath.replace("/Product/Product", "/Product/DeleteProduct") + '?id=' + id;
                    window.location.href = newPath;
                }
            });
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'Product is safe for now :)',
                icon: 'error',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            });
        }
    });
}
