
document.addEventListener("DOMContentLoaded", function () {
    document.querySelector("#AddSubTask").addEventListener("click", saveSubTask);
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

function saveSubTask() {

    // Extract task ID from URL query parameter
    const URLtaskId = new URLSearchParams(window.location.search);
    const taskId = URLtaskId.get('id');

    const updatedTitle = document.querySelector("#subTaskTitle").value;
    const updatedDescription = document.querySelector("#subTaskDescription").value;
    const updatedDueDate = document.querySelector("#subTaskDueDate").value;

    //Gets the JWT token
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    //Once token is obtained, POST request is made to create task
    fetch(`https://localhost:8000/api/Tasks/subtasks?taskId=${taskId}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },

        body: JSON.stringify({
            title: updatedTitle,
            description: updatedDescription,
            dueDate: updatedDueDate
        })
    })
        .then(response => response.json)
        .then(data => {
            window.location.href = "Tasks/Tasks";
        })
        .catch(error => console.error("Error creating sub task", error))
}