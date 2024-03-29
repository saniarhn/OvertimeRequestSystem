﻿$(document).ready(function () {
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
                "defaultContent": "pending",
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
                            <button type="submit" class="btn btn-info" data-toggle="modal" data-target="#DetailEmployee"
                                    data-placement="top" onclick="getData('${row["OvertimeId"]}')" onclick title="Details">
                                <i class="fas fa-info"></i>
                                </button>`;

                }
            }
        ],
/*        scrollCollapse: true,
        paging: false*/
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
                "visible": false,
                "searchable":true
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
                "defaultContent": "pending",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "className": "dt-center",
                "targets": "_all",
                "data": null,
                "render": function (data, type, row) {
                    return `
                                <button type="submit" class="btn btn-info" data-toggle="modal" data-target="#DetailEmployee"
                                    data-placement="top" onclick="getData('${row["OvertimeId"]}')" onclick title="Details">
                                <i class="fas fa-info"></i>
                                </button>`;

                }
            }
        ],
/*        scrollCollapse: true,
        paging: false*/

    });
    
    $('#tableOvertime1').DataTable().column(7).search("pending").draw();
    $('#tableOvertime2').DataTable().column(7).search('accepted|denied', true, false).draw();

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
    obj.StatusByFinance = "accepted";
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
                    /*table.ajax.reload()*/
                        $('#tableOvertime1').DataTable().ajax.reload();
                    $('#tableOvertime2').DataTable().ajax.reload();
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
    obj.StatusByFinance = "denied";
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
            /*table.ajax.reload()*/
            $('#tableOvertime1').DataTable().ajax.reload();
            $('#tableOvertime2').DataTable().ajax.reload();
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

/*function getData(OvertimeId) {
    var a = OvertimeId;
    console.log(a);
    $.ajax({
        url: "/overtimes/GetDetailResponse/" + OvertimeId
    }).done((result) => {
        console.log(result)
        console.log(result[0]);
        console.log(result[1]);
        console.log(result.length);
        var text = ''
        var text2 = ''
        for (var i = 0; i < result.length; i++) {

            if (result.length > 1) {
                if (i == 0) {
                    text2 = `
                            <tr>
                                <th colspan="4">NIP</th>
                                <td>${result[i].NIP}</td>
                            </tr>
                            <tr>
                                <th colspan="4">Name</th>
                                <td>${result[i].Name}</td>
                            </tr>
                           
                            <tr>
                                <th colspan="4">Date</th>
                                <td>${dateConversion(result[i].Date)}</td>
                            </tr>
                            <tr>
                                <th colspan="4">Total Overtime Request</th>
                                <td>${result[i].SumOvertimeHour}</td>
                            </tr>
                            <tr>
                                <th colspan="8" class="text-center">Detail</th>
                            </tr>
                            <tr>
                                <th colspan="2">Start Hour</th>
                                <td colspan="2">${result[i].StartHour}</td>
                                <th colspan="2">End Hour</th>
                                <td colspan="2">${result[i].EndHour}</td>
                            </tr>
                            <tr>
                                <th  colspan="2">Tugas</th>
                                <td  colspan="2">${result[i].TaskName}</td>
                                <th  colspan="2">Location</th>
                                <td  colspan="2">${result[i].LocationName}</td>
                            </tr>
                            `;
                }
                else {
                    text2 = `
                          <tr>
                                <th colspan="2">Start Hour</th>
                                <td colspan="2">${result[i].StartHour}</td>
                                <th colspan="2">End Hour</th>
                                <td colspan="2">${result[i].EndHour}</td>
                            </tr>
                            <tr>
                                <th  colspan="2">Tugas</th>
                                <td  colspan="2">${result[i].TaskName}</td>
                                <th  colspan="2">Location</th>
                                <td  colspan="2">${result[i].LocationName}</td>
                            </tr>
                            `;
                }

            }
            else {
                text2 = `
                          <tr>
                                <th>NIP</th>
                                <td>${result[i].NIP}</td>
                            </tr>
                            <tr>
                                <th>Name</th>
                                <td>${result[i].Name}</td>
                            </tr>
                            <tr>
                                <th>Date</th>
                                <td>${dateConversion(result[i].Date)}</td>
                            </tr>
                            <tr>
                                <th>Total Overtime Request</th>
                                <td>${result[i].SumOvertimeHour}</td>
                            </tr>
                            <tr>
                                <th colspan="2" class="text-center">Detail</th>
                            </tr>
                            <tr>
                                <th>Start Hour</th>
                                <td>${result[i].StartHour}</td>
                                <th>End Hour</th>
                                <td>${result[i].EndHour}</td>
                            </tr>
                            <tr>
                                <th>Tugas</th>
                                <td>${result[i].TaskName}</td>
                                <th>Location</th>
                                <td>${result[i].LocationName}</td>
                            </tr>
                                  `;
            }


            text += text2
            console.log(text)
            $('#table').html(text);

        }


    }).fail((error) => {
        console.log(error);
    });
}*/

function getData(OvertimeId) {
    /*    var a = OvertimeId;
        console.log(a);*/
    $.ajax({
        url: "/overtimes/GetDetailResponse/" + OvertimeId
    }).done((result) => {
        /*    console.log(result)
            console.log(result[0]);
            console.log(result[1]);
            console.log(result.length);*/
        var text = ''
        var text2 = ''
        var text3 = ''
        for (var i = 0; i < result.length; i++) {

            if (result.length > 1) {
                if (i == 0) {
                    text3 = ` <div class="row">
                                  <div class="col-md-4 font-weight-bold">NIP</div>
                                  <div class="col-md-4">${result[i].NIP}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4 font-weight-bold">Name</div>
                                  <div class="col-md-4">${result[i].Name}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4 font-weight-bold">Date</div>
                                  <div class="col-md-4">${dateConversion(result[i].Date)}</div>
                            </div>
                           <div class="row">
                                  <div class="col-md-4 font-weight-bold">Total Hour Request</div>
                                  <div class="col-md-4">${result[i].SumOvertimeHour}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4 font-weight-bold">Salary From Request</div>
                                  <div class="col-md-4">${result[i].OvertimeSalary}</div>
                            </div>`;
                    text2 = `
                            <tr>
                                  <th colspan="12" class="text-center">Detail</th>
                            </tr>
                            <tr>
                                  <th>Start Hour</th>
                                  <td>${result[i].StartHour}</td>
                                  <th>End Hour</th>
                                  <td>${result[i].EndHour}</td>
                                  <th>Location</th>
                                  <td>${result[i].LocationName}</td>
                            </tr>
                            <tr>
                                  <th>Task</th>
                                  <td colspan="10">${result[i].TaskName}</td>
                            </tr>
                            `;
                }
                else {
                    text2 = `
                          <hr/>
                          <tr>
                                  <th>Start Hour</th>
                                  <td>${result[i].StartHour}</td>
                                  <th>End Hour</th>
                                  <td>${result[i].EndHour}</td>
                                  <th>Location</th>
                                  <td>${result[i].LocationName}</td>
                            </tr>
                            <tr>
                                  <th>Task</th>
                                  <td colspan="10">${result[i].TaskName}</td>
                            </tr>
                            `;
                }

            }
            else {
                text3 = ` <div class="row">
                                  <div class="col-md-4 font-weight-bold">NIP</div>
                                  <div class="col-md-4 ">${result[i].NIP}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4 font-weight-bold">Name</div>
                                  <div class="col-md-4 ">${result[i].Name}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4 font-weight-bold">Date</div>
                                  <div class="col-md-4">${dateConversion(result[i].Date)}</div>
                            </div>
                           <div class="row">
                                  <div class="col-md-4 font-weight-bold">Total Hour Request</div>
                                  <div class="col-md-4">${result[i].SumOvertimeHour}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4 font-weight-bold">Salary From Request</div>
                                  <div class="col-md-4">${result[i].OvertimeSalary}</div>
                            </div>`;
                text2 = `
                     
                            <tr>
                                  <th colspan="12" class="text-center">Detail</th>
                            </tr>
                            <tr>
                                  <th>Start Hour</th>
                                  <td>${result[i].StartHour}</td>
                                  <th>End Hour</th>
                                  <td>${result[i].EndHour}</td>
                                  <th>Location</th>
                                  <td>${result[i].LocationName}</td>
                            </tr>
                            <tr>
                                  <th>Task</th>
                                  <td colspan="10">${result[i].TaskName}</td>
                            </tr>
                                  `;
            }


            text += text2
            /*   console.log(text)*/
            $('#table1').html(text3);
            $('#table').html(text);

        }


    }).fail((error) => {
        console.log(error);
    });
}