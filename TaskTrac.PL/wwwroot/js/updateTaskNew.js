

import { showDeleteConfirmation } from './deleteConfirmation.js';

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

function handleDeleteConfirmation(subtaskId) {
    // Call showDeleteConfirmation function from deleteConfirmation.js
    showDeleteConfirmation(subtaskId, 'SubTasks');
}

//function getSubTaskById(subtaskId) {
//    //Gets the JWT token
//    const token = getToken();
//    const headers = { "Content-Type": "application/json" };

//    if (token) {
//        headers["Authorization"] = `Bearer ${token}`;
//    }

//    //Once token is obtained, GET request is made to create task
//    fetch(`https://localhost:8000/api/SubTasks?id=${subtaskId}`, {
//        method: "GET",
//        headers: headers
//    })
//        .then(response => {
//            if (!response.ok) {
//                throw new Error('Failed to fetch subtask details');
//            }
//            return response.json();
//        })
//        .then(subtask => {
//            // Format the date as yyyy/MM/dd
//            const dueDate = new Date(subtask.dueDate);
//            const timezoneOffset = dueDate.getTimezoneOffset() * 60000; // Convert minutes to milliseconds
//            const localDueDate = new Date(dueDate.getTime() - timezoneOffset);

//            // Format the dueDate into "yyyy-mm-dd"
//            const formattedDueDate = localDueDate.toISOString().split('T')[0];

//            // Populate modal with subtask data
//            document.querySelector("#subTaskModal #subTaskTitle").value = subtask.title;
//            document.querySelector("#subTaskModal #subTaskDescription").value = subtask.description;
//            document.querySelector("#subTaskModal #subTaskDueDate").value = formattedDueDate; // Use formattedDueDate here
//            document.querySelector("#subTaskModal #subTaskId").value = subtask.id;


//            document.querySelector("#subTaskModal #AddSubTask").addEventListener("click", function () {
//                updateSubtask(subtaskId)
//            });

//            const modal = new bootstrap.Modal(document.getElementById('subTaskModal'));
//            modal.show();
//        })
//        .catch(error => console.error("Error creating sub task", error))
//}

//function createTask() {


//    // Extract task ID from URL query parameter
//    const URLtaskId = new URLSearchParams(window.location.search);
//    const taskId = URLtaskId.get('id');

//    const updatedTitle = document.querySelector("#subTaskTitle").value;
//    const updatedDescription = document.querySelector("#subTaskDescription").value;
//    const updatedDueDate = document.querySelector("#subTaskDueDate").value;

//    //Gets the JWT token
//    const token = getToken();
//    const headers = { "Content-Type": "application/json" };

//    if (token) {
//        headers["Authorization"] = `Bearer ${token}`;
//    }

//    //Once token is obtained, POST request is made to create task
//    fetch(`https://localhost:8000/api/Tasks/subtasks?taskId=${taskId}`, {
//        method: "POST",
//        headers: {
//            "Content-Type": "application/json",
//            "Authorization": `Bearer ${token}`
//        },

//        body: JSON.stringify({
//            title: updatedTitle,
//            description: updatedDescription,
//            dueDate: updatedDueDate
//        })
//    })
//        .then(response => response.json)
//        .then(data => {
//            window.location.href = "Tasks/Tasks";
//        })
//        .catch(error => console.error("Error creating sub task", error))
//}

//function updateSubtask(subtaskId) {

//    // Extract task ID from URL query parameter
//    const URLtaskId = new URLSearchParams(window.location.search);
//    const taskId = URLtaskId.get('id');

//    const title = document.querySelector("#subTaskModal #subTaskTitle").value;
//    const description = document.querySelector("#subTaskModal #subTaskDescription").value;
//    const dueDate = document.querySelector("#subTaskModal #subTaskDueDate").value;

//    //Gets the JWT token
//    const token = getToken();
//    const headers = { "Content-Type": "application/json" };

//    if (token) {
//        headers["Authorization"] = `Bearer ${token}`;
//    }

//    fetch(`https://localhost:8000/api/SubTasks/${subtaskId}`, {
//        method: "PUT",
//        headers: headers,

//        body: JSON.stringify({
//            title: title,
//            description: description,
//            dueDate: dueDate
//        })
//    })
//        .then(response => {
//            if (!response.ok) {
//                throw new Error('Failed to update subtask');
//            }
//            const modal = document.getElementById('subTaskModal');
//            const bootstrapModal = bootstrap.Modal.getInstance(modal);
//            bootstrapModal.hide();

//            window.location.reload()/*href = `/UpdateTask?id=${taskId}`*/;
//            return response.json();
//        })
//        .catch(error => console.error("Error updating Task", error));
//}

//document.getElementById("updateTaskForm").addEventListener("submit", function (event) {
//    event.preventDefault();

//    // Extract task ID from URL query parameter
//    const URLtaskId = new URLSearchParams(window.location.search);
//    const taskIdToUpdate = URLtaskId.get('id');

//    // Get input values
//    const title = document.getElementById("title").value;
//    const description = document.getElementById("description").value;

//    // Parse the date and adjust for timezone offset
//    const dueDate = new Date(document.getElementById("dueDate").value);
//    const timezoneOffset = dueDate.getTimezoneOffset() * 60000; // Convert minutes to milliseconds
//    const localDueDate = new Date(dueDate.getTime() - timezoneOffset);

//    // Format the dueDate into "yyyy-mm-dd"
//    const formattedDueDate = localDueDate.toISOString().split('T')[0];

//    document.getElementById("dueDate").value = formattedDueDate;

//    // Get JWT token and returns a promise
//    const token = getToken();
//    const headers = { "Content-Type": "application/json" };

//    // Prepare headers with content type and authorization if token available
//    if (token) {
//        headers["Authorization"] = `Bearer ${token}`;
//    }

//    fetch(`https://localhost:8000/api/Tasks/${taskIdToUpdate}`, {
//        method: "PUT",
//        headers: headers,

//        body: JSON.stringify({
//            title: title,
//            description: description,
//            dueDate: dueDate
//        })
//    })
//        .then(response => {
//            if (!response.ok) {
//                throw new Error("Failed to Update Task");
//            }

//            window.location.href = "Tasks/Tasks";
//        })
//        .catch(error => console.error("Error updating Task", error));
//});

window.onload = function () {
    const URLtaskId = new URLSearchParams(window.location.search);
    const taskId = URLtaskId.get('id');

    document.getElementById("AddSubTask").addEventListener("click", function () {
        const subtaskId = document.querySelector("#subTaskModal #subTaskId").value;

        if (subtaskId === '0') {
            createTask();
        } else {
            updateSubtask(subtaskId);
        }
    });

    //Gets the JWT token
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    // Fetch Task details
    fetch(`https://localhost:8000/api/Tasks/${taskId}`, {
        method: "GET",
        headers: headers
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Failed to fetch task details");
            }

            return response.json();
        })
        .then(data => {
            document.getElementById("title").value = data.title;
            document.getElementById("description").value = data.description

            // Parse the date and adjust for timezone offset
            const dueDate = new Date(data.dueDate);
            const timezoneOffset = dueDate.getTimezoneOffset() * 60000; // Convert minutes to milliseconds
            const localDueDate = new Date(dueDate.getTime() - timezoneOffset);

            // Format the dueDate into "yyyy-mm-dd"
            const formattedDueDate = localDueDate.toISOString().split('T')[0];

            document.getElementById("dueDate").value = formattedDueDate;
        })
        .catch(error => console.error("Error fetching task details", error));


    // Fetch subtasks for the task
    //fetch(`https://localhost:8000/api/Tasks/subtasks?taskId=${taskId}`, {
    //    method: "GET",
    //    headers: headers
    //})
    //    .then(response => {
    //        if (!response.ok) {
    //            throw new Error("Failed to fetch sub tasks for task");
    //        }
    //        return response.json();
    //    })
    //    .then(data => {
    //        const subtasks = data.$values; // Accessing the array of subtasks
    //        const subTaskList = document.getElementById("subTaskList");
    //        subTaskList.innerHTML = "";

    //        // Check if subtasks is an array
    //        if (Array.isArray(subtasks)) {
    //            // Iterate over each subtask
    //            subtasks.forEach(subtask => {
    //                // Format the date as yyyy/MM/dd
    //                const formattedDueDate = formatDate(subtask.dueDate);
    //                const row = document.createElement("tr");
    //                // Fill data into table cells
    //                row.innerHTML = `
    //                <td hidden>${subtask.id}</td>
    //                <td>${subtask.title}</td>
    //                <td>${subtask.description}</td>
    //                <td>${formattedDueDate}</td>
    //                <td>
    //                    <a class="btn btn-outline-secondary btn-sm edit-subtask" href="#" data-subtask-Id="${subtask.id}"><i class="fas fa-edit"></i>Edit</a>
    //                    <a class="btn btn-outline-danger btn-sm delete-subtask" data-subtask-id="${subtask.id}"><i class="fas fa-trash-alt"></i>Delete</a>
    //                </td>
    //            `;
    //                subTaskList.appendChild(row);
    //            });

    //            // Event listener for the Sub Task Edit button in the table
    //            document.getElementById('subTaskList').addEventListener('click', function (event) {
    //                const editButton = event.target.closest('.edit-subtask');
    //                if (editButton) {
    //                    event.preventDefault();
    //                    const subtaskId = editButton.dataset.subtaskId;
    //                    getSubTaskById(subtaskId);
    //                }
    //            })

    //            //Adds event listner for the Sub Task Delete button
    //            document.addEventListener("click", function (event) {
    //                const deleteButton = event.target.closest('.delete-subtask');
    //                if (deleteButton) {
    //                    event.preventDefault();
    //                    const subtaskId = deleteButton.dataset.subtaskId;
    //                    handleDeleteConfirmation(subtaskId, 'SubTasks');
    //                }
    //            })
    //        }
    //        else {
    //            console.log(subtasks)
    //        }
    //    })
    //    .catch(error => {
    //        console.error('Error fetching Tasks', error);
    //    });

}
