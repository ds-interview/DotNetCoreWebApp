

$(document).ready(function () {


    //$("#_txtticket").val(localStorage.getItem("ticket"));

    ////set user profile pic
    //var profilepic = localStorage.getItem("profilepic");
    //if (profilepic) {
    //    $('#_imguser').prop('src', profilepic);
    //}


    //var ticket = localStorage.getItem("ticket");
    //if (ticket == null || ticket == "") {
    //    window.location.href = "/emplogin.html";
    //    return;
    //}

    ////verify ticket and redirect to home if logged in
    //$.ajax({
    //    type: "GET",
    //    url: "/api/login/check?ticket=" + localStorage.getItem("ticket"),
    //    contentType: "application/json; utf-8",
    //    dataType: "json",
    //    error: function (e) {
    //        console.log(e);
    //        if (e.status == 400) {
    //            localStorage.clear("ticket");
    //            window.location.href = "/login.html";
    //        }
    //        if (e.status == 401) {
    //            window.location.href = "/401.html";
    //            _showinfo("e", _showerror(e));
    //        }
    //    }
    //});

    $('.tip').tooltip();

    $('.datepicker').datepicker({
        autoclose: true,
        format: 'yyyy-mm-dd'
    });

    $.fn.select2.defaults.set("theme", "bootstrap4");
    
});

//bootstrap model
//===================================================================

//t = title
//m = message
//i = icon (q/i/w)
//bt = button text
//bf = callback function
function _showconfirm(t, m, i, bt, bf) {
    $('#_lblconfirmtitle').text(t);
    $('#_lblconfirmprompt').html(m);
    //if (i == 'q') {
    //    $('#_divconfirmicon').attr('class', 'fa fa-question-circle text-warning');
    //}
    //else if (i == 'i') {
    //    $('#_divconfirmimg').attr('class', 'fa fa-info-circle text-primary');
    //}
    //else if (i == 'w') {
    //    $('#_divconfirmimg').attr('class', 'fa fa-exclamation-triangle text-danger');
    //}
    $('#_btnconfirm').text(bt);
    $('#_btnconfirm').attr('disabled', false);
    $('#_txtconfirmaction').val(bf);
    $('#_lblconfirm').text('');
    $('#_divconfirm').modal('show');
}

function _doconfirm() {
    var fn = $('#_txtconfirmaction').val();
    if (fn) { setTimeout(fn, 100); }
}

function _showinfo(type, message) {
    if (type == "s") {
        toastr.success(message);
    } else if (type == "e") {
        toastr.error(message);
    } else if (type == "w") {
        toastr.warning(message);
    } else {
        toastr.info(message);
    }
}

function _logout() {
    $.ajax({
        type: "GET",
        contentType: "application/json; utf-8",
        dataType: "json",
        url: "/api/logout?ticket=" + localStorage.getItem("ticket"),
        success: function () {
            localStorage.clear();
            document.cookie = "logincookie=;path=/;expires=Thu, 01 Jan 1970 00:00:00 GMT";
            location.href = "/login.html";
        },
        error: function (e) {
            console.log(e);
            document.cookie = "logincookie=;path=/;expires=Thu, 01 Jan 1970 00:00:00 GMT";
            location.href = "/login.html";
        }
    });
}

function _geturlkey(key) {

    var url = window.location.href;

    if (url.lastIndexOf("?") < 0) {
        return;
    }

    var querystring = decodeURIComponent(url.substring(url.lastIndexOf('?') + 1));

    var kvplist = querystring.split("&");

    for (var i = 0; i < kvplist.length; i++) {
        var kvp = kvplist[i].split("=");
        if (kvp.length >= 2 && kvp[0] == key) { return kvp[1]; }
    }

    //reach here if key not found;
    return null;

}

function formatDate(date) {
    var monthNames = [
        "Jan", "Feb", "Mar",
        "Apr", "May", "Jun", "Jul",
        "Aug", "Sep", "Oct",
        "Nov", "Dec"
    ];

    var thedate = new Date(date);

    var day = thedate.getDate();
    var monthIndex = thedate.getMonth();
    var year = thedate.getFullYear();

    return monthNames[monthIndex] + ' ' + day + ', ' + year;
}

function _waithtml() {
    return "<i class='fa fa-spin fa-refresh mr-2'></i>Please wait...";
}

function _showerror(e) {
    //not found 404
    if (e.status == 404) { return "The requested resource was not found. Please contact support if the problem persists."; }
    //bad request has a text
    if (e.status == 400 && e.responseJSON && e.responseJSON.Message) { return e.responseJSON.Message; }
    //server error 500
    if (e.status == 500) { return "An unexpected error occurred. Please try again or contact support if the problem persists."; }
}
