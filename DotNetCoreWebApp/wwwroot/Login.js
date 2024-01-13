$(document).ready(function () {
   
    $("#btnLogin").click(function () {
        $("#usernameErr").html("");
        $("#passwordErr").html("");

        var username = $("#UserName").val().trim();
        var password = $("#txtpassword").val().trim();
        //validations-->
        if (!username || username === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Username!"
            });
        } else if (!password || password === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Password!"
            });
        } else {
            var userDto = { UserName: username, Password: password }
            $.ajax({
                url: apiBaseUrl + '/LoginApi/ValidateLogin',
                dataType: "json",
                type: 'POST',
                traditional: true,
                contentType: 'application/json',
                data: JSON.stringify(userDto),
                cache: false,
                success: function (data, status, xhr) {
                    if (status == 'success' && data !== "No Record") {
                        sessionStorage.setItem("LoggedInUserId", data.userId);
                        sessionStorage.setItem("LoggedInUserName", data.userId);
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Please check Username & Password!"
                        });
                    }
                },
                error: function (data) {
                    if (data.responseJSON.status == 'Login Failed') {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Please check Username & Password!"
                        });
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Some Error Occurred!"
                        });
                    }
                }
            });
        };
    });
});