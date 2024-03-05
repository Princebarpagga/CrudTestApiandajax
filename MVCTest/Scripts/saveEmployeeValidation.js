// saveEmployeeValidation.js
//document.getElementById("EmployeeName").addEventListener("input", validateEmployeeName);

//// Function to validate input and allow only name characters
//function validateEmployeeName() {
//    var employeeNameInput = document.getElementById("EmployeeName").value;
//    var regex = /^[A-Za-z]+$/; 

//    if (!regex.test(employeeNameInput)) { 
//        alert("Please enter only name characters (letters).");
//        document.getElementById("EmployeeName").value = "";
//    }
//}
//function validateEmployeeName() {
//    var employeeName = $('#EmployeeName').val();
//    var isValidName = /^[A-Z][a-zA-Z]*$/.test(employeeName); // Check if starts with capital letter and contains only letters
//    if (!isValidName) {
//        $('#EmployeeNameValidation').text('Employee Name must start with a capital letter and contain only letters.');
//    } else {
//        $('#EmployeeNameValidation').text(''); // Clear previous validation message if valid
//    }
//}
function saveEmployee() {
    var form = $('#createEmployeeForm');
    debugger
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
    }

    // Validate Email
    var email = $('#Email').val();
    if (email.trim() === '') {
        $('#EmailValidation').text('Email is required.');
        return;
    } else if (!validateEmail(email)) {
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
            // Check if the response contains validation errors
            if ($(response).find('.field-validation-error').length > 0) {
                $('#addEmployeePartial').html(response);
            } else {
                toastr.success('Employee added successfully.');
                $('#addEmployeeModal').modal('hide');
            }
        }
    });
}

// Validate Email Format
function validateEmail(email) {
    var re = /^[a-z][a-z0-9._-]*@[a-z0-9.-]+\.[a-z]{2,}$/i;
    //
    return re.test(email);
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function closePopup() {
    $('#addEmployeeModal').modal('hide');
}
