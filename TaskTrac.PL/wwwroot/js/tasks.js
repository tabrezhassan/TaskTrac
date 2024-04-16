//GET TASKS

import { showDeleteConfirmation } from './deleteConfirmation.js';

document.addEventListener("DOMContentLoaded", function () {
    fetchTasks();

    document.getElementById('searchField').addEventListener('input', function () {
        var filter = this.value.toLowerCase(); // Convert search query to lowercase
        var rows = document.getElementById("table").getElementsByTagName("tbody")[0].getElementsByTagName("tr");

        for (var i = 0; i < rows.length; i++) {
            var title = rows[i].cells[1].textContent.toLowerCase(); // Convert cell content to lowercase
            var description = rows[i].cells[2].textContent.toLowerCase();
            var dueDate = rows[i].cells[3].textContent.toLowerCase();

            if (title.indexOf(filter) > -1 || description.indexOf(filter) > -1 || dueDate.indexOf(filter) > -1) {
                rows[i].style.display = "";
            } else {
                rows[i].style.display = "none";
            }
        }
    });
});

function getToken() {
    const token = localStorage.getItem("token");
    const expiration = localStorage.getItem("expiration");

    if (!token) {
        return null;
    }

    const currentTime = Math.floor(Date.now() / 1000);
    if (expiration < currentTime) {
        //token has expired
        localStorage.removeItem("token");
        return null;
    }

    return token;
}

// Function to format the date (yyyy/MM/dd)
function formatDate(dateString) {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}/${month}/${day}`;
}

// Function to handle delete button click
function handleDeleteConfirmation(taskId) {
    // Call showDeleteConfirmation function from deleteConfirmation.js
    showDeleteConfirmation(taskId,'Tasks');
}

function fetchTasks() {

    const token = getToken();
    const headers = {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
    };

    fetch('https://localhost:8000/api/Tasks', {
        method: "GET",
        headers: headers
    })
        .then(response => {
            console.log(response);
            if (!response.ok) {
                console.log(response);
                window.location.href = "/Login/Login";
            }
            return response.json()
        })
        .then(data => {
            // Check if data is structured as expected
            if (data && data.$values && Array.isArray(data.$values)) {
                const tasks = data.$values; // Extract tasks array from $values
                const taskList = document.getElementById("taskList");
                taskList.innerHTML = "";
                tasks.forEach(task => {
                    // Format the date as yyyy/MM/dd
                    const formattedDueDate = formatDate(task.dueDate);
                    const row = document.createElement("tr");
                    row.innerHTML = `
                    <td hidden>${task.id}</td>
                    <td>${task.title}</td>
                    <td>${task.description}</td>
                    <td>${formattedDueDate}</td>
                    <td>
                        <a class="btn btn-outline-secondary btn-sm" href="UpdateTask?id=${task.id}"> Edit</a>
                        <a class="btn btn-outline-danger btn-sm delete-task" data-task-id="${task.id}"> Delete</a>
                    </td
                `;
                    taskList.appendChild(row);
                });

                //Adds event listner for the Delete button
                document.addEventListener("click", function (event) {
                    const deleteButton = event.target.closest('.delete-task');
                    if (deleteButton) {
                        event.preventDefault();
                        const taskId = deleteButton.dataset.taskId;
                        handleDeleteConfirmation(taskId,'Tasks');
                    }
                })

            } else {
                throw new Error("Invalid data structure received from API");
            }
        })
        .catch(error => {
            console.error('Error fetching Tasks', error);

        });

    //window.onload = function () {
    //    fetchTasks();

    //    document.getElementById('searchField').addEventListener('input', function () {
    //        var filter = this.value.toUpperCase();
    //        var rows = document.getElementById("table").getElementsByTagName("tbody")[0].getElementsByTagName("tr");

    //        for (var i = 0; i < rows.length; i++) {
    //            var title = rows[i].cells[1].textContent.toUpperCase();
    //            var description = rows[i].cells[2].textContent.toUpperCase();
    //            var dueDate = rows[i].cells[3].textContent.toUpperCase();

    //            if (title.indexOf(filter) > -1 || description.indexOf(filter) > -1 || dueDate.indexOf(filter) > -1) {
    //                rows[i].style.display = "";
    //            } else {
    //                rows[i].style.display = "none";
    //            }
    //        }
    //    });
    //}
}

