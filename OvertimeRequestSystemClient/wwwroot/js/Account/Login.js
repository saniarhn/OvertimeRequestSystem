function Login() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya

    obj.Email = $("#loginemail").val();
    obj.Password = $("#loginpassword").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    console.log(obj);
    $.ajax({
        type: "POST",
        url: "/Accounts/Auth",
        dataType: 'json',
        data: obj
    }).done((result) => {
        console.log(result);
        window.location.href = result;
    }).fail((error) => {
        console.log(result);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Gagal login'
        })
    })
}

/*$body = $("body");
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});*/
