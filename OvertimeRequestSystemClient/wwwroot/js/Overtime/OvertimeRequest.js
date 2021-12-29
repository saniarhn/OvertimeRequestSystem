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
    $("#detail input").val('');
    $("#detail textarea").val('');
}


function OvertimeRequest() {

/*    ambil array untuk list*/

    const dataKedua = []
    var data = {}
    for (index = 0; index < dataPertama.length; index++)
    {

        data["starthour"]=dataPertama[index].starthour 
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