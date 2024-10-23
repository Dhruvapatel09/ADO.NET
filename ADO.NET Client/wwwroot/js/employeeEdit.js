$(document).ready(function () {
    GetEmployeeDetails();
    $('#employeeForm').submit(function (e) {
        e.preventDefault() // Prevent default form submission.
       
        if (ValidationEmployeeForm()) {
            let employeeId = $('#EmployeeId').val().trim();
            let employeeName = $('#EmployeeName').val().trim();
            let employeeDesignation = $('#EmployeeDesignation').val().trim();
            let employeeDOB = $('#EmployeeDOB').val().trim();

            var requestData = {
                employeeId: employeeId,
                employeeName: employeeName,
                employeeDesignation: employeeDesignation,
                employeeDOB: employeeDOB
            };
            $('#loader').show();
            $.ajax({
                url: "http://localhost:5139/api/Employee/UpdateEmployee",
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(requestData),
                headers: {
                   
                },
                success: function (response) {
                    window.location.href = '/EmployeeAJAX/Index';

                    ShowMessage(response.message);
                },
                error: function (xhr, status, error) {
                    // Check if there is a responseText available
                    if (xhr.responseText) {
                        try {
                            // Parse the responseText into a JavaScript object
                            var errorResponse = JSON.parse(xhr.responseText);

                            // Check the properties of the errorResponse object
                            if (errorResponse && errorResponse.message) {
                                // Display the error message to the user
                                alert('Error: ' + errorResponse.message);
                            } else {
                                // Display a generic error message
                                alert('An error occurred. Please try again.');
                            }
                        } catch (parseError) {
                            console.error('Error parsing response:', parseError);
                            alert('An error occurred. Please try again.');
                        }
                    } else {
                        // Display a generic error message if no responseText is available
                        alert('An unexpected error occurred. Please try again.');
                    }
                },
                complete: function () {
                    $('#loader').hide();
                }
            });
        }
    });
});

function getCookie(name) {
    const cookieValue = document.cookie
        .split('; ')
        .find(cookie => cookie.startsWith(name + '='))
        ?.split('=')[1];
    return cookieValue ? decodeURIComponent(cookieValue) : null;
}

function GetEmployeeDetails() {
   
    $('#loader').show();
    var currentUrl = window.location.href;
    var id = currentUrl.substring(currentUrl.lastIndexOf('/') + 1);
    $.ajax({
        url: "http://localhost:5139/api/Employee/GetEmployeeById/" + id,
        type: 'GET',
        dataType: 'json',
        headers: {
           
        },
        success: function (response) {
            if (response.success) {
                $('#EmployeeId').val(response.data.employeeId);
                $("#EmployeeName").val(response.data.employeeName);
                $("#EmployeeDesignation").val(response.data.employeeDesignation);
                $("#EmployeeDOB").val(response.data.employeeDOB);
            }
        },
        error: function (xhr, status, error) {
            // Check if there is a responseText available
            if (xhr.responseText) {
                try {
                    // Parse the responseText into a JavaScript object
                    var errorResponse = JSON.parse(xhr.responseText);

                    // Check the properties of the errorResponse object
                    if (errorResponse && errorResponse.message) {
                        // Display the error message to the user
                        alert('Error: ' + errorResponse.message);
                    } else {
                        // Display a generic error message
                        alert('An error occurred. Please try again.');
                    }
                } catch (parseError) {
                    console.error('Error parsing response:', parseError);
                    alert('An error occurred. Please try again.');
                }
            } else {
                // Display a generic error message if no responseText is available
                alert('An unexpected error occurred. Please try again.');
            }
        },
        complete: function () {
            $('#loader').hide();
        }
    });
}

function ValidationEmployeeForm() {

    let employeeName = $('#EmployeeName').val().trim();
    let employeeDesignation = $('#EmployeeDesignation').val().trim();
    let employeeDOB = $('#EmployeeDOB').val().trim();
    $('#myValidationSummary').empty().hide();
    let isValid = true;
    if (employeeName.length === 0) {
        $('#myValidationSummary').append('<p>Employee name is required.</p>');
        isValid = false;
    }

    if (employeeDesignation.length === 0) {
        $('#myValidationSummary').append('<p>Employee Designation is required.</p>');
        isValid = false;
    }
    if (employeeDOB.length === 0) {
        $('#myValidationSummary').append('<p>Employee DOB is required.</p>');
        isValid = false;
    }

    if (!isValid) {
        $('#myValidationSummary').show();
    }

    return isValid;
}

function ShowMessage(message) {
    // Get the message container
    var messageContainer = document.getElementById('messageContainer');

    // Display success message
    messageContainer.textContent = message;
    messageContainer.style.backgroundColor = '#4CAF50';
    messageContainer.style.opacity = '1';

    // Hide the message after 3 seconds (3000 milliseconds)
    setTimeout(function () {
        messageContainer.style.opacity = '0';
    }, 3000);

    // Redirect to index page after a short delay (optional)
    setTimeout(function () {
        window.location.href = '/EmployeeAJAX/Index';
    }, 3500); // Redirect after 3.5 seconds (3500 milliseconds)
}