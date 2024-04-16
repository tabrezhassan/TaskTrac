
document.addEventListener("DOMContentLoaded", function() {
    // Function to fetch subtasks for a task and populate the table
    function fetchAndPopulateSubtasks(taskId) {
        fetch(`https://localhost:8000/api/Tasks/subtasks?taskId=${taskId}`)
        .then(response => response.json())
        .then(data => {
            populateSubtasksTable(data);
        })
        .catch(error => console.error('Error fetching subtasks:', error));
    }

    // Function to populate the subtasks table
    function populateSubtasksTable(subtasks) {
        const tableBody = document.getElementById('subtasksTable').getElementsByTagName('tbody')[0];
        tableBody.innerHTML = ''; // Clear existing table rows

        subtasks.forEach(subtask => {
            const formattedDueDate = new Date(subtask.dueDate).toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: '2-digit' });
            const row = `
                <tr>
                    <td hidden>${subtask.id}</td>
                    <td>${subtask.title}</td>
                    <td>${subtask.description}</td>
                    <td>${formattedDueDate}</td>
                    <td>
                        <a class="btn btn-outline-secondary btn-sm edit-subtask" href="#" data-subtaskId="${subtask.id}"><i class="fas fa-edit"></i>Edit</a>
                        <a class="btn btn-outline-danger btn-sm delete-subtask" data-subtask-id="${subtask.id}"><i class="fas fa-trash-alt"></i>Delete</a>
                    </td>
                </tr>`;
            tableBody.insertAdjacentHTML('beforeend', row);
        });
    }

    // Edit button click event (delegated to the table)
    document.getElementById('subtasksTable').addEventListener('click', function(event) {
        if (event.target.classList.contains('edit-subtask')) {
            event.preventDefault();
            var subtaskId = event.target.getAttribute('data-subtaskId');
            // Handle edit action here, e.g., fetchDataForEdit(subtaskId);
        }
    });

    // Delete button click event (delegated to the table)
    document.getElementById('subtasksTable').addEventListener('click', function(event) {
        if (event.target.classList.contains('delete-subtask')) {
            var subtaskId = event.target.getAttribute('data-subtask-id');
            // Handle delete action here
        }
    });

    // Assuming you have taskId available, call this function to populate the table initially
    var taskId = "your_task_id"; // Replace "your_task_id" with the actual taskId
    fetchAndPopulateSubtasks(taskId);
});
