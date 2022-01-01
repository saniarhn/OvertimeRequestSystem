﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
                "data": "NIP"
            },
            {
                "data": "Name"
            },
            {
                "data": "Position"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button class="btn btn-info" data-toggle="modal" data-target="#DetailEmployee"
                                    data-placement="top" onclick="getData('${row["NIP"]}')" title="Details">
                                <i class="fas fa-info-circle"></i>
                            </button>

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
/*
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
                "data": "NIP"
            },
            {
                "data": "Name"
            },
            {
                "data": "Email"
            },
            {
                "data": "Position"
            },
            {
                "data": "BasicSalary"
            }
        ]
    });

});
*/
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
        url: "/Employees/Post",
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
                            <td>Position</td>
                            <td>:</td>
                            <td>${result.Position}</td>
                        </tr>
                        <tr>
                            <td>Basic Salary</td>
                            <td>:</td>
                            <td>Rp ${result.BasicSalary}</td>
                        </tr>
                        <tr>
                            <td>Manager Id</td>
                            <td>:</td>
                            <td> ${result.ManagerId}</td>
                        </tr>

                    </table>
                    </div>`
        $('#DetailEmployee .modal-body').html(text);
    }).fail((error) => {
        console.log(error);
    });
}

function Delete(NIP) {
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
                url: "/employees/Delete/" + NIP,
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
    $.ajax({
        url: "/employees/get/" + NIP,
        success: function (result) {
            console.log(result)
            var data = result
            $("#updatenip").attr("value", data.NIP)
            $("#updateName").attr("value", data.Name)
            $("#updateBaseSalary").attr("value", data.BasicSalary)
            $("#updateemail").attr("value", data.Email)
            $("#updateposition").attr("value", data.Position)
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

$.ajax({
    "url": "/employees/getall",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})



/*function Validate() {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('click', function (event) {
                console.log(form.checkValidity() === false && ($('#txtEmpEmail').val().length != 0) === false)
                if (form.checkValidity() === false && ($('#txtEmpEmail').val().length != 0) === false) {
                    form.classList.add('was-validated');
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Submit Gagal'
                    })
                    event.preventDefault();
                    event.stopPropagation();
                    return false;
                } else {
                    Insert()
                }
            }, false);
        });
    }, false);
}*/