﻿function handleDelete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this action!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Product/DeleteProduct?productId='+id;
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'Product is safe for now :D',
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
        text: "Will you deactive this account?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, just do it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/User/DeactiveUser?userId=' + id + '&inforId=' + uid;
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'User is safe for now :D',
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
        text: "Will you active this account?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, just do it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/User/ActiveUser?userId=' + id + '&inforId=' + uid;
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'User is safe for now :D',
                icon: 'error',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            });
        }
    });
}

function handleDeactiveSupplier(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Will you archive this supplier?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, just do it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Supplier/DeactiveSupplier?supplierId=' + id;
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'Supplier is safe for now :D',
                icon: 'error',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            });
        }
    });
}

function handleActiveSupplier(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Will you unarchive this supplier?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, just do it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Supplier/ActiveSupplier?supplierId=' + id;
        }
        if (!result.isConfirmed) {
            Swal.fire({
                title: 'Canceled',
                text: 'Supplier is safe for now :D',
                icon: 'error',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            });
        }
    });
}

