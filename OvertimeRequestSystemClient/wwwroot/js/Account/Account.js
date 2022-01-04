// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*$(document).ready(function () {
    table = $("#tableAccount").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/employees/getall",
            "dataSrc": "",
            "order": [[1, 'asc']]
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "nip"
            },
            {
                "data": "name"
            },
            {
                "data": "email"
            },
            {
                "data": "position"
            },
            {
                "data": "basicSalary"
            }
        ]
    });

});
*/
$(document).ready(function () {
    table = $("#tableAccount").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/accounts/getall",
            "dataSrc": "",
            "order": [[1, 'asc']]
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "NIP"
            },
            {
                "data": "Password",
                "visible":false
            },
            {
                "data": "AccountStatus",
                 "defaultContent": "Active",
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="submit" class="btn btn-danger" onclick="Delete('${row["NIP"]}')"
                                    data-placement="top" title="Delete">
                                <i class="fas fa-trash"></i>
                            </button>

                            <button class="btn btn-success" data-toggle="modal" onclick="getDataUpdate('${row["NIP"]}')" data-target="#form-edit"
                                    data-placement="top" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button type="submit" class="btn btn-info" data-toggle="modal" data-target="#DetailEmployee"
                                    data-placement="top" onclick="getData('${row["NIP"]}')" onclick title="Details">
                                <i class="fas fa-info"></i>
                                </button>`;

                }
            }
        ]
    });
   
});


function Insert() {
    var obj = new Object();
    obj.nip = $("#txtNIP").val();
    obj.password = $("#txtPassword").val();
    obj.accountStatus = $("#txtAccStatus").val();
    obj.RoleId = $("#txtRole").val();
    console.log(obj);
    $.ajax({
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        type: "POST",
        url: "/accounts/PostInsertAccount",
        dataType: 'json',
        data: obj,
        success: function (result) {
            console.log(result)
            if (result == 200) {
                Swal.fire(
                    'Good job!',
                    'Data berhasil di Submit!',
                    'success'
                )
            }
            else if (result == 400) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Submit Gagal!'
                })
            }
            table.ajax.reload();
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Gagal!'
            })
        }
    })
}

function getData(NIP) {
    $.ajax({
        url: "/accounts/GetDetailAccount/" + NIP
    }).done((result) => {
        console.log(result)
        var text = ''
        var text2 = ''
        for (var i = 0; i < result.length; i++) {
            if (result.length > 1) {
                if (i == 0) {
                    text2 = `<div class="row mb-2">
                     <div class="col-md-3">NIP</div>
                     <div class="col-md-3">${result[i].NIP}</div>
                     <div class="col-md-3">Name</div>
                     <div class="col-md-3">${result[i].Name}</div>
                </div>
                <div class="row mb-2">
                     <div class="col-md-3">Email</div>
                     <div class="col-md-3">${result[i].Email}</div>
                     <div class="col-md-3">Position</div>
                     <div class="col-md-3">${result[i].Position}</div>
                </div>
                <div class="row mb-2">
                     <div class="col-md-3">Basic Salary</div>
                     <div class="col-md-3">${result[i].BasicSalary}</div>
                     <div class="col-md-3">Manager Id</div>
                     <div class="col-md-3">${result[i].ManagerId}</div>
                </div>
                <div class="row mb-2">
                     <div class="col-md-3">Account Status</div>
                     <div class="col-md-3">${result[i].AccountStatus}</div>
                     <div class="col-md-3">Role Name</div>
                     <div class="col-md-3">${result[i].RoleName}</div>
                </div>`;
                }
                else {
                    text2 = `
                <div class="row mb-2">
                     <div class="col-md-3">Role Name</div>
                     <div class="col-md-3">${result[i].RoleName}</div>
                </div>`;
                }
            }
            else {
                text2 = `<div class="row mb-2">
                     <div class="col-md-3">NIP</div>
                     <div class="col-md-3">${result[i].NIP}</div>
                     <div class="col-md-3">Name</div>
                     <div class="col-md-3">${result[i].Name}</div>
                </div>
                <div class="row mb-2">
                     <div class="col-md-3">Email</div>
                     <div class="col-md-3">${result[i].Email}</div>
                     <div class="col-md-3">Position</div>
                     <div class="col-md-3">${result[i].Position}</div>
                </div>
                <div class="row mb-2">
                     <div class="col-md-3">Basic Salary</div>
                     <div class="col-md-3">${result[i].BasicSalary}</div>
                     <div class="col-md-3">Manager Id</div>
                     <div class="col-md-3">${result[i].ManagerId}</div>
                </div>
                <div class="row mb-2">
                     <div class="col-md-3">Account Status</div>
                     <div class="col-md-3">${result[i].AccountStatus}</div>
                     <div class="col-md-3">Role Name</div>
                     <div class="col-md-3">${result[i].RoleName}</div>
                </div>`;
            }
            text += text2
            $('#table').html(text);
        }
    }).fail((error) => {
        console.log(error);
    });
}

function Delete(nip) {
    Swal.fire({
        title: 'Are you sure to Delete this Field?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/accounts/Delete/" + nip,
                type: "Delete",
                success: function (result) {
                    console.log(result)
                    Swal.fire({
                        icon: 'success',
                        title: 'Deleted!',
                        text: 'Your file has been deleted.'
                    })
                    /*table.ajax.reload()*/
                    $('#tableAccount').DataTable().ajax.reload();
                },
                error: function (error) {
                    alert("Delete Fail");
                }
            });
        }
    })
}

function getDataUpdate(NIP) {
    console.log(NIP);
    $.ajax({
        url: "/accounts/get/" + NIP,
        success: function (result) {
            console.log(result)
            var data = result
            $("#updatenip").attr("value", data.NIP)
            $("#updateAccountStatus").attr("value", data.accountStatus)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function Update() {
    var obj = new Object();
    obj.nip = $("#updatenip").val();
    obj.accountStatus = $("#updateAccountStatus").val();
    console.log(obj)
    $.ajax({
        url: "/accounts/put/",
        type: "Put",
        data: obj,
        'dataType': 'json',
        success: function (result) {
            Swal.fire({
                icon: 'success',
                title: 'Good job!',
                text: 'Your data has been saved!'
            }),
            /*table.ajax.reload()*/
                $('#tableAccount').DataTable().ajax.reload();
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
}


$.ajax({
    "url": "/accounts/getall",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})