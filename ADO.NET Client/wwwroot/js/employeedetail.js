
$(document).ready(function () {
    GetEmployeeDetails();
});

function GetEmployeeDetails() {
  
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
                $("#employeeName").html(response.data.employeeName);
                $("#employeeDesignation").html(response.data.employeeDesignation);
                $("#employeeDOB").html(response.data.employeeDOB);
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
