function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function saveEmployee() {
    debugger
    var form = $('#createEmployeeForm');

    // Clear previous validation messages
    $('.text-danger').text('');

    // Validate Employee Name
    var employeeName = $('#EmployeeName').val();
    if (employeeName.trim() === '') {
        $('#EmployeeNameValidation').text('Employee Name is required.');
        return;
    } else if (!/^[A-Z][a-zA-Z]*$/.test(employeeName)) {
        $('#EmployeeNameValidation').text('Employee Name must start with a capital letter and contain only letters.');
        return;
    }

    // Validate Salary
    var salary = $('#Salary').val();
    if (salary.trim() === '') {
        $('#SalaryValidation').text('Salary is required.');
        return;
    } else if (!/^\d/.test(salary)) {
        $('#SalaryValidation').text('Salary must start with a number.');
        return;
    }
    //else if (parseInt(salary) > 5000) {
    //    $('#SalaryValidation').text('Maximum salary value exceeded (5000).');
    //    return;
    //}
    //else if (salary < 5000) {
    //    $('#SalaryValidation').text('Maximum salary value exceeded (5000).');
    //    return;
    //}

    // Validate Email
    //var email = $('#Email').val();
    //if (email.trim() === '') {
    //    $('#EmailValidation').text('Email is required.');
    //    return;
    //    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    //    if (!emailRegex.test(email)) {
    //        $('#EmailValidation').text('Invalid email format.');
    //        return;
    //    }
    //}
    var email = $('#Email').val();
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (email.trim() === '') {
        $('#EmailValidation').text('Email is required.');
        return;
    }
    else if (!emailRegex.test(email)) {
        $('#EmailValidation').text('Invalid email format.');
        return;
    }
    
    // Validate Department
    var departmentId = $('#DepartmentId').val();
    if (departmentId.trim() === '') {
        $('#DepartmentIdValidation').text('Department is required.');
        return;
    }

    // Validate Skill
    var skillId = $('#SkillId').val();
    if (skillId.trim() === '') {
        $('#SkillIdValidation').text('Skill is required.');
        return;
    }

    // If all fields are valid, submit the form
    $.ajax({
        url: form.attr('action'),
        method: form.attr('method'),
        data: form.serialize(),
        success: function (response) {
            if ($(response).find('.field-validation-error').length > 0) {
                // Display validation errors if any
                $('#addEmployeePartial').html(response);
            } else {
                // Clear form fields
                form[0].reset();
                // Close the modal
                $('#addEmployeeModal').modal('hide');
                // Show success message using Toastr
                toastr.success('Employee added successfully!');
                location.reload();
            }
        },
        error: function (xhr, status, error) {
            // Handle error
            console.error(xhr.responseText);
            toastr.error('An error occurred while saving the employee. Please try again.');
        }
    });
}


function closePopup() {
    $('#addEmployeeModal').modal('hide');
}