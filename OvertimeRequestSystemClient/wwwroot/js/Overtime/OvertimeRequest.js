$("#btn-add-list").on('click', myFunction );
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

}


function OvertimeRequest() {
    //Get a formdata instance
/*    var fd = new FormData(detail)

    // Get all values
    var starthour = fd.getAll('starthour')
    var endhour = fd.getAll('endhour')
    var taskname = fd.getAll('taskname')
    var locationname = fd.getAll('locationname')
    // Transform
    var unavailable = starthour.map((starthour, i) => ({
        starthour, endhour: endhour[i],
        taskname: taskname[i],
        locationname: locationname[i]}))
         console.log({ unavailable })*/


      /*  var data = [];*/

  /*  $('#detail input').each(function () {
      
          *//*  data.push({
                starthour: $(this).val()
               *//* endhour: $(this).next().val()*//*
*//*                taskname: $(this).val(),
                locationname: $(this).val(),*//*
            });*//*
        $(this).val()
     
       });
       
        console.log(data);*/

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

     var obj = new Object();
    obj.StartDate = $("#startdate").val();
    obj.EndDate = $("#enddate").val();
    obj.NIP = $("#nip").val();
    obj.ListDetail = dataArray;
    
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