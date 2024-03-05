function validateEmployeeForm() {
    debugger
    /*$(document).ready(function () {*/
    $('#updateEmployeeButton').click(function () {
        // Validate the form
        if (validateEmployeeForm()) {
            
            $('#editEmployeeForm').submit();
        }
    });
//});

    var isValid = true;

    // Reset previous validation messages
    $('.text-danger').text('');

    // Validate Employee Name
    var employeeName = $('#EmployeeName').val().trim();
    if (!employeeName) {
        debugger
        $('#EmployeeName').next('.text-danger').text('Employee Name is required.');
        isValid = false;
    }else if (!validateEmployeeName(employeeName)) {
        $('#EmployeeName').next('.text-danger').text('Please enter only name characters (letters).');
        isValid = false;
    }

    // Validate Salary
    var salary = $('#Salary').val().trim();
    if (!salary) {
        $('#Salary').next('.text-danger').text('Salary is required.');
        isValid = false;
    }

    // Validate Email
    var email = $('#Email').val().trim();
    if (!email) {
        $('#EmailValidation').text('Email is required.');
        isValid = false;
    } else if (!validateEmail(email)) {
        $('#EmailValidation').text('Invalid email format.');
        isValid = false;
    }

    // Validate Department
    var departmentId = $('#DepartmentId').val();
    if (!departmentId) {
        $('#DepartmentId').next('.text-danger').text('Department is required.');
        isValid = false;
    }

    // Validate Skill
    var skillId = $('#SkillId').val();
    if (!skillId) {
        $('#SkillId').next('.text-danger').text('Skill is required.');
        isValid = false;
    }
    if (isValid) {
        debugger
        toastr.success('Employee updated successfully.');
        $('#editEmployeeModal').modal('hide');
    }

    return isValid;
}

// Validate Email Format
function validateEmail(email) {
   // var re = /\S+@\S+\.\S+/;
    var re = /^[a-z][a-z0-9._-]*@[a-z0-9.-]+\.[a-z]{2,}$/i;
    return re.test(email);
}
function closePopup() {
    $('#editEmployeeModal').modal('hide');
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function showupdatesuccesstoast() {
    toastr.success('employee updated successfully.');
    $('#editEmployeeModal').modal('hide');
}
function validateEmployeeName(employeeName) {
    debugger
    var regex = /^[A-Za-z]+$/;
    return regex.test(employeeName);
}