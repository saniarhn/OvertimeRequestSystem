// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var options = {
    chart: {
        type: 'bar'
    },
    series: [{
        name: 'sales',
        data: [30, 40, 45, 50, 49, 60, 70, 91, 125]
    }],
    xaxis: {
        categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999]
    }
}

var chart = new ApexCharts(document.querySelector("#chart-1"), options);

chart.render();

$.ajax({
    /*url: "https://localhost:44382/OvertimeRequestSystemAPI/Employees/GetCountPosition"*/
    url:"/employees/GetCountPosition"
}).done((result) => {
    console.log(result)
    var options2 = {
        series: [{
            data: result.result.Value
        }],
        chart: {
            height: 350,
            type: 'bar',
            events: {
                click: function (chart, w, e) {

                }
            }
        },
        plotOptions: {
            bar: {
                columnWidth: '45%',
                distributed: true
            }
        },
        dataLabels: {
            enabled: false
        },
        legend: {
            show: false
        },
        xaxis: {
            categories: result.result.Key,
            labels: {
                style: {
                    fontSize: '12px'
                }
            }
        }
    };
    var chart2 = new ApexCharts(document.querySelector("#chart-2"), options2);
    chart2.render();
}).fail((error) => {
    console.log(error);
});

$.ajax({
    "url": "/employees/GetCountPosition",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})


$.ajax({
    url: "https://localhost:44382/OvertimeRequestSystemAPI/Employees/GetCountRole"
}).done((result) => {
    console.log(result)
    var options3 = {
        series: [{
            data: result.result.Value
        }],
        chart: {
            height: 350,
            type: 'bar',
            events: {
                click: function (chart, w, e) {

                }
            }
        },
        plotOptions: {
            bar: {
                columnWidth: '45%',
                distributed: true
            }
        },
        dataLabels: {
            enabled: false
        },
        legend: {
            show: false
        },
        xaxis: {
            categories: result.result.Key,
            labels: {
                style: {
                    fontSize: '12px'
                }
            }
        }
    };
    var chart3 = new ApexCharts(document.querySelector("#chart-3"), options3);
    chart3.render();
}).fail((error) => {
    console.log(error);
});


