$(document).ready(function () {

    $("#btnSignUp").click(function () {
        var name = $("#txtName").val().trim();
        var username = $("#txtUsername").val().trim();
        var password = $("#txtPassword").val().trim();
        var cnfirmPassword = $("#txtConfirmPassword").val().trim();
        //validations-->
        if (!name || name === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Username!"
            });
        }
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
        }
        else if (password != cnfirmPassword) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Password and Confirm Password does not Match!"
            });
        }
        else {

            var signUpDto = { Name: name, Username: username, Password: password }
            $.ajax({
                url: apiBaseUrl + '/LoginApi/SignUpUser',
                dataType: "json",
                type: 'POST',
                traditional: true,
                contentType: 'application/json',
                data: JSON.stringify(signUpDto),
                cache: false,
                success: function (data, status, xhr) {
                    if (status == 'success' && data > 0) {
                        Swal.fire({
                            position: "top-end",
                            icon: "success",
                            title: "Sign Up Success. Ypu can now Login",
                            showConfirmButton: true,
                            timer: 2000
                        });
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