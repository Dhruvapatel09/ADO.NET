
$(document).ready(function () {
    $('#createEmployeeForm').submit(function (e) {
        e.preventDefault() // Prevent default form submission.
        
        if (ValidationEmployeeForm()) {
            let employeeName = $('#EmployeeName').val().trim();
            let employeeDesignation = $('#EmployeeDesignation').val().trim();

            var requestData = {
                employeeName: employeeName,
                employeeDesignation: employeeDesignation
            };
            $('#loader').show();
            $.ajax({
                url: "http://localhost:5139/api/Employee/AddEmployee",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(requestData),
                headers: {
                    
                },
                success: function (response) {
                    ShowMessage(response.message);
                    window.location.href = '/EmployeeAJAX/Index';

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
    } else {
        // Check if the date is within a valid range for SQL Server
        let minDate = new Date('1753-01-01');
        let maxDate = new Date('9999-12-31');
        let enteredDate = new Date(employeeDOB);

        if (enteredDate < minDate || enteredDate > maxDate) {
            $('#myValidationSummary').append('<p>Date of Birth must be between 1/1/1753 and 12/31/9999.</p>');
            isValid = false;
        }
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
