$(document).ready(function () {
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });
    //tableOvertime1
    table = $("#tableOvertime1").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/overtimes/GetResponseForFinance",
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
                "data": "NIP",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "Name",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "Position",
                "visible":false
            },
            {
                "data": "OvertimeId",
                "visible": false
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["Date"]);
                },
                "className": "dt-center",
                "targets": "_all"

            },
            {
                "data": "SumOvertimeHour",
                "className": "dt-center",
                "targets": "_all"
            },

            {
                "data": "StatusByFinance",
                "defaultContent": "Diajukan",
                "className": "dt-center",
                "targets": "_all",
                "visible": false,
                "searchable": true,
            },
            {
                "className": "dt-center",
                "targets": "_all",
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="submit" class="btn btn-success"
                                    data-placement="top" title="Accept" onclick="UpdateYes('${row["NIP"]}','${row["OvertimeId"]}')">
                                <i class="fas fa-check"></i>
                            </button>
                            <button type="submit" class="btn btn-danger" data-toggle="modal" data-target="#form-edit" onclick="getDataUpdate('${row["NIP"]}','${row["OvertimeId"]}')"
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
    //tableOvertime2
    table = $("#tableOvertime2").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/overtimes/GetResponseForFinance",
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
                "data": "NIP",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "Name",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "Position",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "OvertimeId",
                "visible": false
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["Date"]);
                },
                "className": "dt-center",
                "targets": "_all"

            },
            {
                "data": "SumOvertimeHour",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "StatusByFinance",
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
    
    $('#tableOvertime1').DataTable().column(7).search("Diajukan").draw();
    $('#tableOvertime2').DataTable().column(7).search('Diterima|Ditolak', true, false).draw();

});


function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

$.ajax({
    "url": "/overtimes/GetResponseForFinance",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

function UpdateYes(nip, overtimeid) {
            var obj = new Object();
            obj.StatusByFinance = "Diterima";
            obj.NIP = nip;
            obj.OvertimeId = overtimeid;
            console.log(obj)
            $.ajax({
                url: "/overtimes/PutOvertimeResponseFinance/",
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
    obj.StatusByFinance = "Ditolak";
    obj.NIP = $("#updatenip").val();
    obj.ResponseDescription = $("#updaterespdesc").val();
    obj.OvertimeId = $("#updateovertimeid").val();
    obj.ManagerOrFinanceId = $("#updatemngfinid").val();
    console.log(obj)
    $.ajax({
        url: "/overtimes/PutOvertimeResponseFinance/",
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