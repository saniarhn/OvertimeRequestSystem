$("#btn-add-list").on('click', myFunction );
function myFunction() {
    var text = ''
    text = `<div class="row mt-4">

        <!--Grid column-->
        <div class="col-md-6">
            <div class="form-group mb-0">
                <label for="inputMDEx1">Jam Mulai</label>
                <input type="time" id="inputMDEx1" class="form-control">
            </div>
        </div>
        <!--Grid column-->
        <!--Grid column-->
        <div class="col-md-6">
            <div class="form-group mb-0">
                <label for="inputMDEx1">Jam Selesai</label>
                <input type="time" id="inputMDEx1" class="form-control">
            </div>
        </div>
        <!--Grid column-->

    </div>
    <div class="form-group">
        <label for="exampleFormControlTextarea1">Tugas</label>
        <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
    </div>
    <div class="form-group mb-0">
        <label for="email" class="">Location</label>
        <input type="text" id="email" name="email" class="form-control">
    </div>`
    $('#btn-add-list').before(text);

}