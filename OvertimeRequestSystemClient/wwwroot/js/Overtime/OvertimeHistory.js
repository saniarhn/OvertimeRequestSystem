﻿$(document).ready(function () {
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });
    //tableOvertime2
    table = $("#tableOvertime2").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/overtimes/GetOvertimeHistory",
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
            /*{
                "data": "nip",
                "className": "dt-center",
                "targets": "_all"
            },*/
        /*    {
                "data": "overtimeId",
                "visible": false
            },*/
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
                "data": "StatusByManager",
                "defaultContent": "Diajukan",
                "className": "dt-center",
                "targets": "_all",
                "visible": false,
                "searchable":true
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
        scrollCollapse: true,
        paging: false

    });
    //tableOvertime3
    table = $("#tableOvertime3").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'All']],
        responsive: true,
        "ajax": {
            "url": "/overtimes/GetOvertimeHistory",
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
      /*      {
                "data": "nip",
                "className": "dt-center",
                "targets": "_all"
            },
            {
                "data": "overtimeId",
                "visible": false
            },*/
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["Date"]);
                },
                "className": "dt-center",
                "targets": "_all"

            },
            {
                "data": "StatusByManager",
                "defaultContent": "Diajukan",
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
                             <button type="submit" class="btn btn-info" data-toggle="modal" data-target="#DetailEmployee"
                                    data-placement="top" onclick="getData('${row["OvertimeId"]}')" onclick title="Details">
                                <i class="fas fa-info"></i>
                                </button>`;
                }
            }
        ],
        scrollCollapse: true,
        paging: false

    });

    /*$('#tableOvertime1').DataTable().column(4).search("Diterima").draw();*/
/*    $('#tableOvertime2').DataTable().column(4).search("Diajukan").draw();*/
/*    $('#tableOvertime3').DataTable().column(4).search("Diterima").draw();*/
    /* $('#tableOvertime3').DataTable({ "iDisplayLength": 100, "search": { regex: true } }).column(3).search("Diterima|Ditolak", true, false).draw();*/
    $('#tableOvertime2').DataTable().column(3).search("Diajukan").draw();
    $('#tableOvertime3').DataTable().column(2).search('Diterima|Ditolak', true, false).draw();
});


function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

$.ajax({
    "url": "/overtimes/GetOvertimeHistory",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

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
                            <div class="row">
                                  <div class="col-md-4">NIP</div>
                                  <div class="col-md-4">${result[i].NIP}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4">Name</div>
                                  <div class="col-md-4">${result[i].Name}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4">Date</div>
                                  <div class="col-md-4">${dateConversion(result[i].Date)}</div>
                            </div>
                           <div class="row">
                                  <div class="col-md-4">Total Overtime Request</div>
                                  <div class="col-md-4">${result[i].SumOvertimeHour}</div>
                            </div>
                            <div class="row mb-3 mt-2">
                                  <div class="col-md-12 text-center">Detail</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-2">Start Hour</div>
                                  <div class="col-md-2">${result[i].StartHour}</div>
                                  <div class="col-md-2">End Hour</div>
                                  <div class="col-md-2">${result[i].EndHour}</div>
                                  <div class="col-md-2">Location</div>
                                  <div class="col-md-2">${result[i].LocationName}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-2">Tugas</div>
                                  <div class="col-md-10">${result[i].TaskName}</div>
                            </div>
                            `;
                }
                else {
                    text2 = `
                          <hr/>
                          <div class="row mt-2">
                                  <div class="col-md-2">Start Hour</div>
                                  <div class="col-md-2">${result[i].StartHour}</div>
                                  <div class="col-md-2">End Hour</div>
                                  <div class="col-md-2">${result[i].EndHour}</div>
                                  <div class="col-md-2">Location</div>
                                  <div class="col-md-2">${result[i].LocationName}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-2">Tugas</div>
                                  <div class="col-md-10">${result[i].TaskName}</div>
                            </div>
                            `;
                }

            }
            else {
                text2 = `
                          <div class="row">
                                  <div class="col-md-4">NIP</div>
                                  <div class="col-md-4">${result[i].NIP}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4">Name</div>
                                  <div class="col-md-4">${result[i].Name}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-4">Date</div>
                                  <div class="col-md-4">${dateConversion(result[i].Date)}</div>
                            </div>
                           <div class="row">
                                  <div class="col-md-4">Total Overtime Request</div>
                                  <div class="col-md-4">${result[i].SumOvertimeHour}</div>
                            </div>
                            <div class="row mb-3 mt-2">
                                  <div class="col-md-12 text-center">Detail</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-2">Start Hour</div>
                                  <div class="col-md-2">${result[i].StartHour}</div>
                                  <div class="col-md-2">End Hour</div>
                                  <div class="col-md-2">${result[i].EndHour}</div>
                                  <div class="col-md-2">Location</div>
                                  <div class="col-md-2">${result[i].LocationName}</div>
                            </div>
                            <div class="row">
                                  <div class="col-md-2">Tugas</div>
                                  <div class="col-md-10">${result[i].TaskName}</div>
                            </div>
                                  `;
            }


            text += text2
            console.log(text)
            $('#table').html(text);

        }


    }).fail((error) => {
        console.log(error);
    });
}