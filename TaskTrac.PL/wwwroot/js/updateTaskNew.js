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

document.getElementById("updateTaskForm").addEventListener("submit", function (event) {
    event.preventDefault();

    // Extract task ID from URL query parameter
    const URLtaskId = new URLSearchParams(window.location.search);
    const taskIdToUpdate = URLtaskId.get('id');

    // Get input values
    const title = document.getElementById("title").value;
    const description = document.getElementById("description").value;

    // Parse the date and adjust for timezone offset
    const dueDate = new Date(document.getElementById("dueDate").value);
    const timezoneOffset = dueDate.getTimezoneOffset() * 60000; // Convert minutes to milliseconds
    const localDueDate = new Date(dueDate.getTime() - timezoneOffset);

    // Format the dueDate into "yyyy-mm-dd"
    const formattedDueDate = localDueDate.toISOString().split('T')[0];

    document.getElementById("dueDate").value = formattedDueDate;

    // Get JWT token and returns a promise
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    // Prepare headers with content type and authorization if token available
    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    fetch(`https://localhost:8000/api/Tasks/${taskIdToUpdate}`, {
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
                throw new Error("Failed to Update Task");
            }

            window.location.href = "Tasks/Tasks";
        })
        .catch(error => console.error("Error updating Task", error));
});

    window.onload = function () {
    const URLtaskId = new URLSearchParams(window.location.search);
    const taskId = URLtaskId.get('id');

    fetch(`https://localhost:8000/api/Tasks/${taskId}`)
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
        .catch (error => console.error("Error fetching task details", error));
};