// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*// Write your JavaScript code.
function OvertimeRequest() {
    var dataArray = [];
    var data = {};

    $("#detail").find('input').each(function (i) {
        if ((i + 1) % 3 === 0) {
            data[this.id] = this.value;
            data["taskname"] = null;
            dataArray.push(data);
            data = {};
        } else {
            data[this.id] = this.value;
        }
    });

    $("#detail").find('textarea').each(function (i) {
        dataArray[i].taskname = this.value;
    });


    console.log(dataArray)
}
$("#btn-add-list").on('click', myFunction);
function myFunction() {
    var text = ''
    text = `
    <div class="row">
        <!--Grid column-->
        <div class="col-md-6">
            <div class="form-group mb-0">
                <label for="inputMDEx1">Jam Mulai</label>
                <input type="time" id="starthour" class="form-control">
            </div>
        </div>
        <!--Grid column-->
        <!--Grid column-->
        <div class="col-md-6">
            <div class="form-group mb-0">
                <label for="inputMDEx1">Jam Selesai</label>
                <input type="time" id="endhour" class="form-control">
            </div>
        </div>
        <!--Grid column-->

    </div>
    <div class="form-group">
        <label for="exampleFormControlTextarea1">Tugas</label>
        <textarea class="form-control" id="taskname" rows="3"></textarea>
    </div>
    <div class="form-group mb-0">
        <label for="email" class="">Location</label>
        <input type="text" id="locationname" name="locationname" class="form-control">
    </div>`
    $('#detail').append(text);

}*/

$(document).ready(function () {
    table = $("#tableEmployee").DataTable({
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
                "data": null,
                "render": function (data, type, row) {
                    return `<button class="btn btn-info" data-toggle="modal" data-target="#DetailEmployee"
                                    data-placement="top" onclick="getData('${row["nip"]}')" title="Details">
                                <i class="fas fa-info-circle"></i>
                            </button>

                            <button type="submit" class="btn btn-danger" onclick="Delete('${row["nip"]}')"
                                    data-placement="top" title="Delete">
                                <i class="fas fa-trash"></i>
                            </button>

                            <button class="btn btn-success" data-toggle="modal" onclick="getDataUpdate('${row["nip"]}')" data-target="#form-edit"
                                    data-placement="top" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>`;

                }
            }
        ]
    });
   
});

$(document).ready(function () {
    table = $("#tableAboutEmployee").DataTable({
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

function Insert() {
    var obj = new Object();
    obj.Name = $("#txtEmpName").val();
    obj.Email = $("#txtEmpEmail").val();
    obj.Position = $("#txtEmpPosition").val();
    obj.BasicSalary = $("#txtEmpBasicSalary").val();
    obj.ManagerId = $("#txtEmpManagerID").val();
    console.log(obj);

    $.ajax({
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        url: "accounts/InsertAccount",
        type: "Post",
        'data': obj,
        //'data': JSON.stringify(obj),
        'dataType': 'json',
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

function getData(nip) {
    $.ajax({
        url: "/employees/get/" + nip
    }).done((result) => {
        console.log(result)
        var text = ''
        text = `<div class = "text-center">
                    <table class= "table bg-light table-hover text-info text-center">
                        <tr>
                            <td>NIP</td>
                            <td>:</td>
                            <td>${result.nip}</td>
                        </tr>
                        <tr>
                            <td>Name</td>
                            <td>:</td>
                            <td>${result.name}</td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>:</td>
                            <td>${result.email}</td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>:</td>
                            <td>${result.position}</td>
                        </tr>
                        <tr>
                            <td>Base Salary</td>
                            <td>:</td>
                            <td>Rp ${result.basicSalary}</td>
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
                url: "/employees/Delete/" + nip,
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

function getDataUpdate(nip) {
    $.ajax({
        url: "/employees/get/" + nip,
        success: function (result) {
            console.log(result)
            var data = result
            $("#updatenip").attr("value", data.nip)
            $("#updateName").attr("value", data.name)
            $("#updateBaseSalary").attr("value", data.basicSalary)
            $("#updateemail").attr("value", data.email)
            $("#updateposition").attr("value", data.position)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function Update() {
    var obj = new Object();
    obj.nip = $("#updatenip").val();
    obj.name = $("#updateName").val();
    obj.basicSalary = $("#updateBaseSalary").val();
    obj.email = $("#updateemail").val();
    obj.position = $("#updateposition").val();
    console.log(obj)
    $.ajax({
        url: "/employees/put/",
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