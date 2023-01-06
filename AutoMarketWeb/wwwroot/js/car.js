var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Car/GetAll"
        },
        "columns": [
            { "data": "name", "width": "14%" },
            { "data": "mileage", "width": "14%" },
            { "data": "yearOfProduction", "width": "14%" },
            { "data": "fuelType.name", "width": "14%" },
            { "data": "brand.name", "width": "14%" },
            { "data": "price", "width": "14%" },
            { "data": "discountPrice", "width": "14%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Car/Upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>&nbsp;Edit
                            </a>
                            <a onClick=Delete('/Admin/Car/Delete/${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash3"></i>&nbsp;Delete
                            </a>
                        </div>
                           `
                },
                "width": "14%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}