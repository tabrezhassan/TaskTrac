
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


document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-task');
    const confirmDeleteToast = document.getElementById('confirmDeleteToast');
    const confirmDeleteButton = confirmDeleteToast.querySelector('.confirm-delete');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault();
            const taskId = button.dataset.taskId;
            // Show the confirm delete toast
            $('#confirmDeleteToast').toast('show');
            confirmDeleteButton.onclick = function () {
                // Delete the task
                deleteTask(taskId);
            };
        });
    });
});

function deleteTask(taskId) {
    // Get JWT token and returns a promise
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    // Prepare headers with content type and authorization if token available
    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    fetch(`https://localhost:8000/api/Tasks/${taskId}`, {
        method: 'DELETE',
        headers: headers,
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to delete task');
            }
            // Reload the page
            location.reload();
        })
        .catch(error => console.error('Error deleting task:', error));
}
