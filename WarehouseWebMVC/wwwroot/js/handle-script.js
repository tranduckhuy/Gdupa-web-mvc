function handleDelete(id) {
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
            window.location.href = '/Product/DeleteProduct?productId='+id;
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

function handleDeactiveUser(id, uid) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this action!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, just do it !'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/User/DeactiveUser?userId=' + id + '&inforId=' + uid;
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'User is safe for now :)',
                icon: 'error',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            });
        }
    });
}

function handleActiveUser(id, uid) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this action!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, just do it !'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/User/ActiveUser?userId=' + id + '&inforId=' + uid;
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'User is safe for now :)',
                icon: 'error',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            });
        }
    });
}
