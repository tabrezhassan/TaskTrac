
import { displayToast } from './deleteConfirmation.js';

let taskId;

document.getElementById("createTaskForm").addEventListener("submit", createTask);

document.getElementById("AddSubTask").addEventListener("click", function () {
    const subtaskId = document.querySelector("#subTaskModal #subTaskId").value;

    if (subtaskId === '0') {
        createSubTask();
    } else {
        updateSubtask(subtaskId);
    }
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

function handleDeleteConfirmation(subtaskId) {
    // Call showDeleteConfirmation function from deleteConfirmation.js
    showDeleteConfirmation(subtaskId, 'SubTasks');
}

function createTask(event) {
    event.preventDefault();

    const title = document.getElementById("title").value
    const description = document.getElementById("description").value
    const dueDate = document.getElementById("dueDate").value

    //Gets the JWT token
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    //Once token is obtained, POST request is made to create task
    fetch('https://localhost:8000/api/Tasks', {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify({
            title: title,
            description: description,
            dueDate: dueDate
        })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to create task');
            }
            return response.json();
        })
        .then(data => {
            taskId = data.id; // Access the 'id' field from the response data

            // Keep the task details visible on the page
            document.getElementById('title').textContent = `Title: ${data.title}`;
            document.getElementById('description').textContent = `Description: ${data.description}`;
            document.getElementById('dueDate').textContent = `Due Date: ${data.dueDate}`;
            
            displayToast('Create Task', 'Task created successfully!, you can add Sub Tasks.', 'success');
        })
        .catch(error => {
            console.error("Error create task:", error);
            displayToast('Create Task', 'Failed to create task. Please try again.', 'error');
        });
}

function createSubTask() {

    const Title = document.querySelector("#subTaskTitle").value;
    const Description = document.querySelector("#subTaskDescription").value;
    const DueDate = document.querySelector("#subTaskDueDate").value;

    const token = getToken();
    const headers = {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
    };

    fetch(`https://localhost:8000/api/Tasks/subtasks?taskId=${taskId}`, {
        method: "POST",
        headers: headers,
        body: JSON.stringify({
            title: Title,
            description: Description,
            dueDate: DueDate
        })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to create subtask');
            }
            return response.json();
        })
        .then(data => {
            displayToast('Create Subtask', 'Subtask created successfully!', 'success');
            // Clear form fields if needed
            document.getElementById("subTaskTitle").value = "";
            document.getElementById("subTaskDescription").value = "";
            document.getElementById("subTaskDueDate").value = "";
            // Close modal if needed
            const modal = document.getElementById('subTaskModal');
            const bootstrapModal = bootstrap.Modal.getInstance(modal);
            bootstrapModal.hide();

            // Refresh subtask list if needed
            refreshSubTasks();
        })
        .catch(error => {
            console.error("Error creating subtask:", error);
            displayToast('Create Subtask', 'Failed to create subtask. Please try again.', 'error');
        });
}

function refreshSubTasks()
{
    const token = getToken();
    const headers = {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
    };

    // Fetch subtasks for the task
    fetch(`https://localhost:8000/api/Tasks/subtasks?taskId=${taskId}`, {
        method: "GET",
        headers: headers
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Failed to fetch sub tasks for task");
            }
            return response.json();
        })
        .then(data => {
            const subtasks = data.$values; // Accessing the array of subtasks
            const subTaskList = document.getElementById("subTaskList");
            subTaskList.innerHTML = "";

            // Check if subtasks is an array
            if (Array.isArray(subtasks)) {
                // Iterate over each subtask
                subtasks.forEach(subtask => {
                    // Format the date as yyyy/MM/dd
                    const formattedDueDate = formatDate(subtask.dueDate);
                    const row = document.createElement("tr");
                    // Fill data into table cells
                    row.innerHTML = `
                    <td hidden>${subtask.id}</td>
                    <td>${subtask.title}</td>
                    <td>${subtask.description}</td>
                    <td>${formattedDueDate}</td>
                    <td>
                        <a class="btn btn-outline-secondary btn-sm edit-subtask" href="#" data-subtask-Id="${subtask.id}">Edit <i class="fas fa-edit"></i></a>
                        <a class="btn btn-outline-danger btn-sm delete-subtask" data-subtask-id="${subtask.id}">Delete <i class="fas fa-trash-alt"></i></a>
                    </td>
                `;
                    subTaskList.appendChild(row);
                });

                // Event listener for the Sub Task Edit button in the table
                document.getElementById('subTaskList').addEventListener('click', function (event) {
                    const editButton = event.target.closest('.edit-subtask');
                    if (editButton) {
                        event.preventDefault();
                        const subtaskId = editButton.dataset.subtaskId;
                        getSubTaskById(subtaskId);
                    }
                })

                //Adds event listner for the Sub Task Delete button
                document.addEventListener("click", function (event) {
                    const deleteButton = event.target.closest('.delete-subtask');
                    if (deleteButton) {
                        event.preventDefault();
                        const subtaskId = deleteButton.dataset.subtaskId;
                        handleDeleteConfirmation(subtaskId, 'SubTasks');
                    }
                })
            }
            else {
                console.log(subtasks)
            }
        })
        .catch(error => {
            console.error('Error fetching Tasks', error);
        });
}

function getSubTaskById(subtaskId)
{
        //Gets the JWT token
        const token = getToken();
        const headers = { "Content-Type": "application/json" };

        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        }

        //Once token is obtained, GET request is made to create task
        fetch(`https://localhost:8000/api/SubTasks?id=${subtaskId}`, {
            method: "GET",
            headers: headers
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to fetch subtask details');
                }
                return response.json();
            })
            .then(subtask => {
                // Format the date as yyyy/MM/dd
                const dueDate = new Date(subtask.dueDate);
                const timezoneOffset = dueDate.getTimezoneOffset() * 60000; // Convert minutes to milliseconds
                const localDueDate = new Date(dueDate.getTime() - timezoneOffset);

                // Format the dueDate into "yyyy-mm-dd"
                const formattedDueDate = localDueDate.toISOString().split('T')[0];

                // Populate modal with subtask data
                document.querySelector("#subTaskModal #subTaskTitle").value = subtask.title;
                document.querySelector("#subTaskModal #subTaskDescription").value = subtask.description;
                document.querySelector("#subTaskModal #subTaskDueDate").value = formattedDueDate; // Use formattedDueDate here
                document.querySelector("#subTaskModal #subTaskId").value = subtask.id;


                document.querySelector("#subTaskModal #AddSubTask").addEventListener("click", function () {
                    updateSubtask(subtaskId)
                });

                const modal = new bootstrap.Modal(document.getElementById('subTaskModal'));
                modal.show();
            })
            .catch(error => console.error("Error creating sub task", error))
    }

    function updateSubtask(subtaskId)
    {

        const title = document.querySelector("#subTaskModal #subTaskTitle").value;
        const description = document.querySelector("#subTaskModal #subTaskDescription").value;
        const dueDate = document.querySelector("#subTaskModal #subTaskDueDate").value;

        //Gets the JWT token
        const token = getToken();
        const headers = { "Content-Type": "application/json" };

        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        }

        fetch(`https://localhost:8000/api/SubTasks/${subtaskId}`, {
            method: "PUT",
            headers: headers,

            body: JSON.stringify({
                title: title,
                description: description,
                dueDate: dueDate
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to update subtask');
                }
                const modal = document.getElementById('subTaskModal');
                const bootstrapModal = bootstrap.Modal.getInstance(modal);
                bootstrapModal.hide();

                // Refresh subtask list if needed
                refreshSubTasks();
            })
            .catch(error => console.error("Error updating Task", error));
    }





