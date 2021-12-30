$(document).ready(function () {
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });
    
    //tableOvertime2
    table = $("#tableOvertime2").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/overtimes/GetResponseForManager",
            "dataSrc": "",
            "order": [[1, 'asc']]
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                },
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "nip",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "name",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "position",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "overtimeId",
                "visible": false
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["date"]);
                },
                "className": "dt-center",
                "targets": "_all"

            },
            {
                "data": "sumOvertimeHour",
                "className": "dt-center",
                "targets": "_all"
            },

       /*     {
                "data": "statusByManager",
                "defaultContent": "Diajukan",
                "className": "dt-center",
                "targets": "_all"
            },*/
            {
                "className": "dt-center",
                "targets": "_all",
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="submit" class="btn btn-success"
                                    data-placement="top" title="Accept" onclick="UpdateYes('${row["nip"]}','${row["overtimeId"]}')">
                                <i class="fas fa-check"></i>
                            </button>
                            <button type="submit" class="btn btn-danger" data-toggle="modal" data-target="#form-edit" onclick="getDataUpdate('${row["nip"]}','${row["overtimeId"]}')"
                                    data-placement="top" title="Decline">
                                <i class="fas fa-times"></i>
                            </button>
                            <button type="submit" class="btn btn-primary" data-toggle="modal" data-target="#form-details" 
                                    data-placement="top" title="Details">
                                <i class="fas fa-info"></i>
                            </button>`;

                }
            }
        ],
        scrollCollapse: true,
        paging: false

    });
    //tableOvertime3
    table = $("#tableOvertime3").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/overtimes/GetResponseForManager",
            "dataSrc": "",
            "order": [[1, 'asc']]
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                },
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "nip",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "name",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "position",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "overtimeId",
                "visible": false
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["date"]);
                },
                "className": "dt-center",
                "targets": "_all"

            },
            {
                "data": "sumOvertimeHour",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "statusByManager",
                "defaultContent": "Diajukan",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "className": "dt-center",
                "targets": "_all",
                "data": null,
                "render": function (data, type, row) {
                    return `
                           
                            <button type="submit" class="btn btn-primary" data-toggle="modal" data-target="#form-details" 
                                    data-placement="top" title="Details">
                                <i class="fas fa-info"></i>
                            </button>`;

                }
            }
        ],
        scrollCollapse: true,
        paging: false

    });

    /*$('#tableOvertime1').DataTable().column(4).search("Diterima").draw();*/
   /* $('#tableOvertime2').DataTable().column(4).search("Diajukan").draw();
      $('#tableOvertime3').DataTable().column(4).search("Diterima|Ditolak").draw();*/
 /*   $('#tableOvertime3').DataTable({ "iDisplayLength": 100, "search": { regex: true } }).column(4).search("Diterima|Ditolak", true, false).draw();*/

});


function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

$.ajax({
    "url": "/overtimes/GetResponseForManager",
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

function getDataUpdate(nip, overtimeid) {
    $.ajax({
        url: "/overtimes/get/" + nip,
        success: function (result) {
            console.log(result)
            var data = result
            $("#updatenip").attr("value", nip)
            $("#updateovertimeid").attr("value", overtimeid)
            $("#updaterespdesc").attr("value", data.ResponseDescription)
            $("#updatemngfinid").attr("value", data.ManagerOrFinanceId)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function UpdateNo() {
    var obj = new Object();
    obj.StatusByManager = "Ditolak";
    obj.NIP = $("#updatenip").val();
    obj.ResponseDescription = $("#updaterespdesc").val();
    obj.OvertimeId = $("#updateovertimeid").val();
    obj.ManagerOrFinanceId = $("#updatemngfinid").val();
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