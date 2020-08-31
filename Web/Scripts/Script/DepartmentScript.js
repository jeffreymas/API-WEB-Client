var table = null;

$(document).ready(function () {
    table = $("#MyTable").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/Departments/LoadDepart",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columnDefs": [{
            sortable: false,
            "class": "index",
            targets: 0
        }],
        "columns": [
            { "data": null },
            { "data": "Name" },
            {
                "sortable": false,
                "render": function (data, type, row) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.Id + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-outline-danger btn-circle" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.Id + ')" ><i class="fa fa-lg fa-times"></i></button>'
                }
            }
        ]
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});


function Save() {
    debugger;
    var Dept = new Object();
    Dept.Id = 0;
    Dept.Name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: "/Departments/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Dept
    }).then((result) => {
        if (result.StatusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data inserted Successfully',
                showConfirmButton: false,
                timer: 1500,
            })
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    });
}

function Update() {
    //debugger;
    var Dept = new Object();
    Dept.Id = $('#Id').val();
    Dept.Name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: "/Departments/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Dept
    }).then((result) => {
        //debugger;
        if (result.StatusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data Updated Successfully',
                showConfirmButton: false,
                timer: 1500,
            });
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Departments/Delete/",
                data: { Id: id }
            }).then((result) => {
                if (result.StatusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Delete Successfully',
                        showConfirmButton: false,
                        timer: 1500,
                    });
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}

function GetById(id) {
    //debugger;
    $.ajax({
        url: "/Departments/GetById/",
        data: { id: id }
    }).then((result) => {
        //debugger;
        $('#Id').val(result.Id);
        $('#Name').val(result.Name);
        $('#Insert').hide();
        $('#Update').show();
        $('#exampleModal').modal('show');
    })
}

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Insert').show();
}