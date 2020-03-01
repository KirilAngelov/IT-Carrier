var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/users/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "username", "width": "5%" },
            { "data": "password", "width": "5%" },
            { "data": "name", "width": "5%" },
            { "data": "middleName", "width": "5%" },
            { "data": "lastName", "width": "5%" },
            { "data": "personalId", "width": "5%" },
            { "data": "telephoneNumber", "width": "5%" },
            { "data": "email", "width": "5%" },
            { "data": "startDate", "width": "5%" },
            { "data": "active", "width": "5%" },
            { "data": "fireDate", "width": "5%" },
           
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Users/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/users/Delete?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}