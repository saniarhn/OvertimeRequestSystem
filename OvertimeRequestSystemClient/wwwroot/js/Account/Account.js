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
        url: "/employees/get/" + NIP
    }).done((result) => {
        console.log(result)
        var text = ''
        text = `<div class = "text-center">
                    <table class= "table bg-light table-hover text-info text-center">
                        <tr>
                            <td>NIP</td>
                            <td>:</td>
                            <td>${result.NIP}</td>
                        </tr>
                        <tr>
                            <td>Name</td>
                            <td>:</td>
                            <td>${result.Name}</td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>:</td>
                            <td>${result.Email}</td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>:</td>
                            <td>${result.Position}</td>
                        </tr>
                        <tr>
                            <td>Base Salary</td>
                            <td>:</td>
                            <td>Rp ${result.BasicSalary}</td>
                        </tr>
                    </table>
                    </div>`
        $('.datainfo').html(text);
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
                    table.ajax.reload()
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
                table.ajax.reload()
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