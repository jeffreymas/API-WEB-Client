var table = null;
var arrDepart = [];

$(document).ready(function () {
    table = $("#division").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/division/LoadDiv",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columnDefs": [{
            sortable: false,
            "class": "index",
            targets: 0
        }],
        order: [[1, 'asc']],
        fixedColumns: true,
        "columns": [
            { "data": null },
            { "data": "DivisionName" },
            { "data": "DepartmentName" },
            {
                "sortable": false,
                "render": function (data, type, row) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.DivisionId + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-outline-danger btn-circle" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.DivisionId + ')" ><i class="fa fa-lg fa-times"></i></button>'
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

function ClearScreen() {
    $('#DivisionId').val('');
    $('#DivisionName').val('');
    $('#update').hide();
    $('#add').show();
}

function LoadDepart(element) {
    //debugger;
    if (arrDepart.length === 0) {
        $.ajax({
            type: "Get",
            url: "/departments/LoadDepart",
            success: function (data) {
                arrDepart = data;
                renderDepart(element);
            }
        });
    }
    else {
        renderDepart(element);
    }
}

function renderDepart(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Department').hide());
    $.each(arrDepart, function (i, val) {
        $option.append($('<option/>').val(val.Id).text(val.Name))
    });
}

LoadDepart($('#DepartOption'))

function GetById(id) {
    //debugger;
    $.ajax({
        url: "/division/GetById/",
        data: { id: id }
    }).then((result) => {
        debugger;
        $('#DivisionId').val(result.DivisionId);
        $('#DivisionName').val(result.DivisionName);
        $('#DepartOption').val(result.DepartmentId)
        $('#add').hide();
        $('#update').show();
        $('#myModal').modal('show');
    })
}

function Save() {
    //debugger;
    var Div = new Object();
    Div.Id = 0;
    Div.Name = $('#DivisionName').val();
    Div.department_id = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/division/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Div
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
    })
}

function Update() {
    //debugger;
    var Div = new Object();
    Div.Id = $('#DivisionId').val();
    Div.Name = $('#DivisionName').val();
    Div.department_id = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/division/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Div
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
    }).then((resultSwal) => {
        if (resultSwal.value) {
            //debugger;
            $.ajax({
                url: "/division/Delete/",
                data: { id: id }
            }).then((result) => {
                //debugger;
                if (result.StatusCode == 200) {
                    //debugger;
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