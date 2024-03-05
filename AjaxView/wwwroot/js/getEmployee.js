document.addEventListener("DOMContentLoaded", function () {
    // Function to make AJAX request and display Employee data
    function getEmployeeData() {
        // Create a new XMLHttpRequest object
        var xhr = new XMLHttpRequest();

        // Define the endpoint URL
        var url = 'https://localhost:44302/api/Employee';

        // Configure the request
        xhr.open("GET", url, true);

        // Set up the onload callback function
        xhr.onload = function () {
            if (xhr.status === 200) {
                // Parse JSON response
                var employeeData = JSON.parse(xhr.responseText);

                // Call function to display data in DataTable
                displayEmployeeData(employeeData);
            }
        };

        // Set up the onerror callback function
        xhr.onerror = function () {
            console.error("Error fetching Employee data.");
        };

        // Send the request
        xhr.send();
    }

   
    // Function to display Employee data in a regular HTML table
    function displayEmployeeData(employeeData) {
        // Get the table body element
        var employeeListTableBody = document.getElementById("empTableBody");

        // Clear any existing content
        employeeListTableBody.innerHTML = "";

        // Iterate over the Employee data and create table rows
        employeeData.forEach(function (employee) {
            // Create a new table row
            var row = employeeListTableBody.insertRow();
          
            // Insert cells into the row for each piece of Employee data
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            var cell3 = row.insertCell(2);
            var cell4 = row.insertCell(3);
            var cell5 = row.insertCell(4);
            // Set the text content of each cell
            cell1.textContent = employee.employeeName;
            cell2.textContent = employee.salary;
            cell3.textContent = employee.departmentName;
            cell4.textContent = employee.skillName;
            cell5.textContent = employee.email;
            
        });
    }


    getEmployeeData();
});
