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
                    return `<a class="btn btn-info" href="https://localhost:44307/admins/aboutemployee" role="button"
                                    data-placement="top" title="Details">
                                <i class="fas fa-info-circle"></i>
                            </a>

                            <button type="submit" class="btn btn-danger" onclick=""
                                    data-placement="top" title="Delete">
                                <i class="fas fa-trash"></i>
                            </button>

                            <a class="btn btn-success" href="https://localhost:44307/admins/editemployee" data-target="#form-edit" role="button"
                                    data-placement="top" title="Edit">
                                <i class="fas fa-edit"></i>
                            </a>`;

                }
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