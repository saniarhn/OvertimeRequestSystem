$(document).ready(function () {

    table = $("#tableOvertime").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/overtimes/getall",
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
                  "data": "overtimeId"
    /*            "data": null,
                "render": function (data, type, row) {
                    if (row['overtimeId'] === null  ) {
                        return `Male`;
                    } else {
                        return `Female`;
                    }
                }*/
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["date"]);
                }

            },
       
            {
                "data": "statusByManager"
      /*          "data": null,
                "render": function (data, type, row) {
                    if (row['StatusByManager'] === null) {
                        return `Diajukan`;
                    } else {
                        return `S`;
                    }
                }*/
            },
            {
                "data": "statusByFinance"
            },
            {
                "data": "nip"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="submit" class="btn btn-success"
                                    data-placement="top" title="Accept" onclick="UpdateYes('${row["nip"]}','${row["overtimeId"]}')">
                                <i class="fas fa-check"></i>
                            </button>
                            <button type="submit" class="btn btn-danger" onclick="UpdateNo('${row["nip"]}')"
                                    data-placement="top" title="Decline">
                                <i class="fas fa-times"></i>
                            </button>`;

                }
            }
        ]
    });

});


function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

$.ajax({
    "url": "/overtimes/getall",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

function UpdateYes(nip, overtimeid) {
            var obj = new Object();
            obj.StatusByManager = "Diterima";
            obj.NIP = nip;
    obj.OvertimeId = overtimeid;
            console.log(obj)
            $.ajax({
                url: "/overtimes/PutOvertimeResponseManager/",
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

function UpdateNo(nip) {
    var obj = new Object();
    obj.statusbymanager = statusbymanager.val("Ditolak");
    console.log(obj)
    $.ajax({
        url: "/overtimes/PutOvertimeResponseManager/",
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