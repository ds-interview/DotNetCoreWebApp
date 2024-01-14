$(document).ready(function () {

    $("#btnUpdate").hide();

    var jobDetailId = parseInt($("#hdnJobId").val());
    
    if (jobDetailId > 0) {
        $("#btnUpdate").show();
        $("#btnCreate").hide();
    }

    $('.datepicker').datetimepicker({
        format: 'dd/MM/YYYY',
        lang: 'ru'
    });

    $("#btnCreate").click(function () {
        var jobCode = $("#txtJobCode").val().trim();
        var title = $("#txtTitle").val().trim();
        var minQualification = $("#txtMinQual").val().trim();
        var sortDescription = $("#txtSortDesc").val().trim();
        var lastDate = $("#txtLastDate").val().trim();
        //validations-->
        if (!jobCode || jobCode === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Job Code!"
            });
        }
        if (!title || title === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Title!"
            });
        } else if (!minQualification || minQualification === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Minimum Qualification for Job!"
            });
        }
        else if (!lastDate || lastDate === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Password and Confirm Password does not Match!"
            });
        }
        else {

            var objJob = {
                JobDetailId: jobDetailId,
                JobCode: jobCode,
                Title: title,
                MinimumQualification: minQualification,
                SortDescription: sortDescription,
                strLastDate: lastDate,
                CreatedBy: parseInt(sessionStorage.getItem("LoggedInUserId")),
                SpType: 1
            }
            $.ajax({
                url: apiBaseUrl + '/JobApi/Job',
                dataType: "json",
                type: 'POST',
                traditional: true,
                contentType: 'application/json',
                data: JSON.stringify(objJob),
                cache: false,
                success: function (data, status, xhr) {
                    if (status == 'success' && data > 0) {
                        Swal.fire({
                            position: "top-end",
                            icon: "success",
                            title: "Job Created Successfully",
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

    $("#btnUpdate").click(function () {
        var jobCode = $("#txtJobCode").val().trim();
        var title = $("#txtTitle").val().trim();
        var minQualification = $("#txtMinQual").val().trim();
        var sortDescription = $("#txtSortDesc").val().trim();
        var lastDate = $("#txtLastDate").val().trim();
        //validations-->
        if (!jobCode || jobCode === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Job Code!"
            });
        }
        if (!title || title === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Title!"
            });
        } else if (!minQualification || minQualification === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please enter Minimum Qualification for Job!"
            });
        }
        else if (!lastDate || lastDate === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Password and Confirm Password does not Match!"
            });
        }
        else {

            var objJob = {
                JobDetailId: jobDetailId,
                JobCode: jobCode,
                Title: title,
                MinimumQualification: minQualification,
                SortDescription: sortDescription,
                strLastDate: lastDate,
                ModifiedBy: parseInt(sessionStorage.getItem("LoggedInUserId")),
                SpType: 2
            }
            $.ajax({
                url: apiBaseUrl + '/JobApi/Job',
                dataType: "json",
                type: 'POST',
                traditional: true,
                contentType: 'application/json',
                data: JSON.stringify(objJob),
                cache: false,
                success: function (data, status) {
                    if (status == 'success' && data > 0) {
                        Swal.fire({
                            position: "top-end",
                            icon: "success",
                            title: "Job Updated Successfully",
                            showConfirmButton: false,
                            timer: 2000
                        });
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Not able to Update."
                        });
                    }
                },
                error: function (data) {
                    console.log(data);
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Some Error Occurred!"
                    });
                }
            });
        };
    });
});