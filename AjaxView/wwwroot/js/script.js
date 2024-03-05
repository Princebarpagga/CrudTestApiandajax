//document.addEventListener("DOMContentLoaded", function () {
//    // Function to make AJAX request and display user data
//    function getUserData() {
//        // Create a new XMLHttpRequest object
//        var xhr = new XMLHttpRequest();

//        // Define the endpoint URL
//        var url = 'https://localhost:44302/api/User';

//        // Configure the request
//        xhr.open("GET", url, true);

//        // Set up the onload callback function
//        xhr.onload = function () {
//            debugger
//            if (xhr.status === 200) {
//                // Parse JSON response
//                var userData = JSON.parse(xhr.responseText);
//                console.log("userdata", userData)
//                // Call function to display data in DataTable
//                displayUserData(userData);
//            }
//        };

//        // Set up the onerror callback function
//        xhr.onerror = function () {
//            console.error("Error fetching user data.");
//        };

//        // Send the request
//        xhr.send();
//    }

//    // Function to display user data in DataTable
//    function displayUserData(userData) {
//        // Get the container element
//        var userListContainer = document.getElementById("userTableBody");

//        // Clear any existing content
//        userListContainer.innerHTML = "";

//        // Create a table element
//        var table = document.createElement("table");
//        table.setAttribute("id", "userDataTable");
//        userListContainer.appendChild(table);

//        // Initialize the DataTable
//        $(document).ready(function () {
//            $('#userDataTable').DataTable({
//                data: userData,
//                columns: [
//                    { title: "User ID", data: "userId" },
//                    { title: "Username", data: "username" },
//                    //{
//                    //    title: "Password", data: function (user) {
//                    //        return user.showUserPassword ? user.password : "********";
//                    //    }
//                    //},
//                    { title: "Mobile Number", data: "mobileNumber" }
//                ]
//            });
//        });
//    }

   
//    getUserData();
//});
document.addEventListener("DOMContentLoaded", function () {
    // Function to make AJAX request and display user data
    function getUserData() {
        // Create a new XMLHttpRequest object
        var xhr = new XMLHttpRequest();

        // Define the endpoint URL
        var url = 'https://localhost:44302/api/User';

        // Configure the request
        xhr.open("GET", url, true);

        // Set up the onload callback function
        xhr.onload = function () {
            if (xhr.status === 200) {
                // Parse JSON response
                var userData = JSON.parse(xhr.responseText);

                // Call function to display data in DataTable
                displayUserData(userData);
            }
        };

        // Set up the onerror callback function
        xhr.onerror = function () {
            console.error("Error fetching user data.");
        };

        // Send the request
        xhr.send();
    }

    // Function to display user data in DataTable
    //function displayUserData(userData) {
    //    // Initialize the DataTable
    //    $(document).ready(function () {
    //        $('#userTable').DataTable({
    //            data: userData,
    //            columns: [
    //                { title: "User ID", data: "userId" },
    //                { title: "Username", data: "username" },
    //                { title: "Mobile Number", data: "mobileNumber" }
    //            ]
    //        });
    //    });
    //}
    // Function to display user data in a regular HTML table
    function displayUserData(userData) {
        // Get the table body element
        var userListTableBody = document.getElementById("userTableBody");

        // Clear any existing content
        userListTableBody.innerHTML = "";

        // Iterate over the user data and create table rows
        userData.forEach(function (user) {
            // Create a new table row
            var row = userListTableBody.insertRow();

            // Insert cells into the row for each piece of user data
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            var cell3 = row.insertCell(2);
            //var cell4 = row.insertCell(3);
            //var cell5 = row.insertCell(4);
            // Set the text content of each cell
            cell1.textContent = user.username;
            cell2.textContent = user.mobileNumber;
            var profileImg = document.createElement('img');
            profileImg.src = 'data:image/jpeg;base64,' + user.profilePicture; 
            profileImg.alt = 'Profile Picture';
            profileImg.width = 50; 
            profileImg.height = 50; 
            cell3.appendChild(profileImg);
            //// Create an edit button
            //var editButton = document.createElement('button');
            //editButton.textContent = 'Edit';
            //editButton.onclick = function () {
            //    // Add your edit logic here
            //    console.log('Edit button clicked for user:', user.username);
            //};
            //cell4.appendChild(editButton);

            //// Create a delete button
            //var deleteButton = document.createElement('button');
            //deleteButton.textContent = 'Delete';
            //deleteButton.onclick = function () {
            //    // Add your delete logic here
            //    console.log('Delete button clicked for user:', user.username);
            //};
            //cell5.appendChild(deleteButton);
        });
    }

   
    getUserData();
});
