$(document).ready(function () {

    function GetAllJobs() {
        $.ajax({
            type: 'GET',
            traditional: true,
            contentType: "application/json",
            beforeSend: function () {
                $("#overlay").fadeIn(300);
            },
            url: apiBaseUrl + '/JobApi/GetAllJobs',
            headers: {
                Authorization: 'Bearer ' + sessionStorage.getItem("userToken"),
            },
            success: function (data, status, xhr) {
                if (status == 'success') {
                    dynamicTable(data);
                }
            },
            error: function (data, status, xhr) {
                console.log('data: ', data);
            }
        }).done(function () {
            setTimeout(function () {
                $("#overlay").fadeOut(300);
            }, 200);
        });
    }
    GetAllJobs();

    function dynamicTable(result) {
        $('#tblmain').val('');
        var dynamictable = '';
        dynamictable += '<table class="table table-striped table-bordered table-hover"  id="tblmain">'
            + '<thead>'
            + '<tr>'
            + '<td>SN.</td>'
            + '<td>Job Code</td>'
            + '<td>Title</td>'
            + '<td>Minimum Qualification</td>'
            + '<td>Sort Description</td>'
            + '<td>Last Date</td>'
            + '<td>Created By</td>'
            + '<td>Edit</td>'
            + '<td>Delete</td>'
            + '</tr>'
            + '</thead><tbody>'
        if (result != null) {
            $.each(result, function (key, value) {
                dynamictable += '<tr>'
                dynamictable += '<td>' + (key + 1) + '</td>'
                dynamictable += '<td>' + result[key].jobCode + '</td>'
                dynamictable += '<td>' + result[key].title + '</td>'
                dynamictable += '<td>' + result[key].minimumQualification + '</td>'
                dynamictable += '<td>' + result[key].sortDescription + '</td>'
                dynamictable += '<td>' + result[key].strLastDate + '</td>'
                dynamictable += '<td>' + result[key].createdByName + '</td>'
                dynamictable += '<td><a class="edit" style="cursor:pointer" onclick="EditJob(' + result[key].jobDetailId + ')"><i class="fad fa fa-pencil" aria-hidden="true"></i></a></td>'
                dynamictable += '<td><a class="edit" style="cursor:pointer" onclick="DeleteJob(' + result[key].jobDetailId + ')"><i class="fad fa fa-trash"></i></a></td>'
                dynamictable += '</tr>'
            });
        }

        dynamictable += '</tbody>'
        dynamictable += '</table>';
        $('#divTable').html(dynamictable);
        $('#tblmain').dataTable({
            "columnDefs": [
                { "className": "dt-center", "targets": "_all" }

            ], searching: true, paging: true, "ordering": true,
            dom: '<Blf<t>ip>',
            buttons: [
                'copy', 'excel', 'pdf'
            ]

        });
    }

    window.EditJob = function (id) {
        window.location = '/Job/Index?id=' + id;
    }

    window.DeleteJob = function (id) {
      
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                var objJob = {
                    JobDetailId: id,
                    ModifiedBy: parseInt(sessionStorage.getItem("LoggedInUserId")),
                    SpType: 3
                }
                $.ajax({
                    url: apiBaseUrl + '/JobApi/DeleteJob',
                    dataType: "json",
                    type: 'POST',
                    traditional: true,
                    contentType: 'application/json',
                    data: JSON.stringify(objJob),
                    cache: false,
                    success: function (data, status, xhr) {
                        if (status == 'success' && data > 0) {
                            Swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'success'
                            )
                        } else {
                            Swal.fire({
                                icon: "error",
                                title: "Oops...",
                                text: "Unable to Delete"
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
                
            }
        })
    }
});