
// Event listener for the "Edit" button in the table
document.getElementById('subTaskList').addEventListener('click', function (event) {
    const editButton = event.target.closest('.edit-subtask');
    if (editButton) {
        event.preventDefault();
        const subtaskId = editButton.dataset.subtaskId;
        getSubTaskById(subtaskId);
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


function getSubTaskById(subtaskId) {
    //Gets the JWT token
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    //Once token is obtained, POST request is made to create task
    fetch(`https://localhost:8000/api/SubTasks?id=${subtaskId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch subtask details');
            }
            return response.json();
        })
        .then(subtask => {
            document.querySelector("subTaskModal #subTaskTitle").value = subtask.title;
            document.querySelector("subTaskModal #subTaskDescription").value = subtask.description;
            document.querySelector("subTaskModal #subTaskDueDate").value = subtas.dueDate;

            document.querySelector("subTaskModal saveSubTask").addEventListener("click", function () {
                updateSubtask(subtaskId)
            });

            const modal = new bootstrap.Modal(document.getElementById('subTaskModal'));
            modal.show();
        })
        .catch(error => console.error("Error creating sub task", error))
}


function updateSubtask(subtaskId) {

    // Extract task ID from URL query parameter
    const URLtaskId = new URLSearchParams(window.location.search);
    const taskId = URLtaskId.get('id');

    const title = document.querySelector("subTaskModal subTaskTitle").value;
    const description = document.querySelector("subTaskModal subTaskDescription").value;
    const dueDate = document.querySelector("subTaskModal subTaskDueDate").value;

    //Gets the JWT token
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    fetch(`https://localhost:8000/api/subtasks/${subtaskId}`, {
        method: "PUT",
        headers: headers,
    })

    body: JSON.stringify({
        title: title,
        description: description,
        dueDate: dueDate
    })
    then(response => {
        if (!response.ok) {
            throw new Error('Failed to update subtask');
        }
        return response.json();
    })
        .then(subtask => {
            const modal = document.getElementById('subTaskModal');
            const bootstrapModal = bootstrap.Modal.getInstance(modal);
            bootstrapModal.hide();
        })
        .catch(error => {
            console.error('Error updating subtask:', error);
        });


}

