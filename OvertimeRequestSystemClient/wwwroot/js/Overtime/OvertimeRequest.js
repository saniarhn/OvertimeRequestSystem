$("#btn-add-list").on('click', myFunction);
var index = 0;
var dataPertama = [];

function myFunction() {


 

    var data1 = {};
    
    $("#form-header").find('input').each(function (i) {
        if ((i + 1) % 2 === 0) {
            data1[this.id] = this.value;
            data1["starthour"] = null;
            data1["endhour"] = null;
            data1["taskname"] = null;
            data1["locationname"] = null;
            dataPertama.push(data1);
            data1 = {};
        }
        else {
            data1[this.id] = this.value;
        }
    });

    $("#detail").find('input').each(function (i) {
        switch (i) {
            case 0:
                dataPertama[index].starthour = this.value;
                break;
            case 1:
                dataPertama[index].endhour = this.value;
                break;
            case 2:
                dataPertama[index].locationname = this.value;
                break;
            default:
        }
    });

    $("#detail").find('textarea').each(function (i) {
        dataPertama[index].taskname = this.value;
    });

    index += 1;

    console.log(dataPertama)

    
    for (let i = 0; i < dataPertama.length; i++) {
        document.getElementById("tb_content").innerHTML = null;
        var a = 0;
        while (a < dataPertama.length) {
            var tableContent = '<tr>';
            tableContent += "<td>" + (a + 1) + "</td>" +
                "<td>" + dataPertama[a].date + "</td>" +
                "<td>" + dataPertama[a].starthour + "</td>" +
                "<td>" + dataPertama[a].endhour + "</td>" +
                "<td>" + dataPertama[a].taskname + "</td>" +
                "<td>" + dataPertama[a].locationname + "</td>";
            tableContent += '</tr>';
            document.getElementById("tb_content").innerHTML += tableContent;
            a++;
        }

    }
    document.getElementById('date').setAttribute('readonly', true);

    $("#detail input").val('');
    $("#detail textarea").val('');
}


function OvertimeRequest() {

    /* ambil array untuk list*/

    const dataKedua = [];
    var data = {};
    for (index = 0; index < dataPertama.length; index++)
    {

        data["starthour"] = dataPertama[index].starthour 
        data["endhour"] = dataPertama[index].endhour
        data["locationname"] = dataPertama[index].locationname
        data["taskname"] = dataPertama[index].taskname
        dataKedua.push(data);
        data = {}

    }

    var obj = new Object();
    obj.Date = $("#date").val();
    obj.NIP = $("#nip").val();
    obj.ListDetail = dataKedua;
    console.log('data yang akan dikirim');
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    console.log(obj);

/*    tanggal hariini format yyyy-mm-dd*/
    var todayDate = new Date().toISOString().slice(0, 10);
/*    tanggal hariini -7*/
    var date2 = minDays(new Date(), 7);
/*    tanggal hariini -7 ubah format jd yyyy-mm-dd*/
    var todayDate2 = date2.toISOString().slice(0, 10);

    console.log(todayDate2 <= obj.Date && obj.Date <= todayDate);
    if (todayDate2 <= obj.Date && obj.Date <= todayDate)
    {
        var date3 = new Date(obj.Date);

        var listdays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var date4 = listdays[date3.getDay()];
        console.log(date4);
        if (date4 != 'Sunday' || date4 != 'Saturday') {

            for (a = 0; a < dataPertama.length; a++) {
                let valuestart = moment.duration(dataPertama[a].starthour, "HH:mm");
                console.log(valuestart.hours() >= 8 && valuestart.hours() <= 17);
                if (valuestart.hours() >= 8 && valuestart.hours() <= 17) {

                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Your Time Request has a problem'
                    })
                }

                else {
                    $.ajax({

                        type: "POST",
                        url: "/Overtimes/PostOvertimeRepository",
                        dataType: 'json',

                        data: obj
                    }).done((result) => {
                        console.log(result);
                        if (result == 200) {

                            Swal.fire({
                                icon: 'success',
                                title: 'Berhasil Request',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }
                        else if (result == 400) {
                            console.log(result)
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'Gagal Request'
                            })
                        }

                    }).fail((error) => {
                        console.log(error);
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal Request'
                        })
                    })
                }

            }
        }
        else {
            $.ajax({

                type: "POST",
                url: "/Overtimes/PostOvertimeRepository",
                dataType: 'json',

                data: obj
            }).done((result) => {
                console.log(result);
                if (result == 200) {

                    Swal.fire({
                        icon: 'success',
                        title: 'Berhasil Request',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
                else if (result == 400) {
                    console.log(result)
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Gagal Request'
                    })
                }

            }).fail((error) => {
                console.log(error);
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Gagal Request'
                })
            })

        }
      
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Your Date Request has a Problem'
        })
    }
 /*   $.ajax({
         
    type: "POST",
        url: "/Overtimes/PostOvertimeRepository",
            dataType: 'json',
            
    data: obj
}).done((result) => {
    console.log(result);
    if (result == 200) {
        
        Swal.fire({
            icon: 'success',
            title: 'Berhasil Request',
            showConfirmButton: false,
            timer: 1500
        })
    }
    else if (result == 400) {
        console.log(result)
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Gagal Request'
        })
    }

}).fail((error) => {
    console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Gagal Request'
        })
    })*/
}

$("#delete").on('click', myDelete);
function myDelete() {
    document.getElementById('delete').type = 'reset';
    $("#tb_content tr").remove();
    document.getElementById('date').removeAttribute('readonly', false);
}

/*function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}*/

function minDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() - days);
    return result;
}