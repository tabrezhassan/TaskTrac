
document.getElementById("createTaskForm").addEventListener("submit", createTask);

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
        .then(response => response.json)
        .then(data => {
            window.location.href = "/Tasks/Tasks";
        })
        .catch(error => console.error("Error create task:", error));
        //.catch (error => console.error("Error obtaining token:", error));
}




