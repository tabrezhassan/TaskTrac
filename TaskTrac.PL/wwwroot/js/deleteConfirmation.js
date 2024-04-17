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

function displayToast(header,message,type) {

        // Create the toast element
        const toastElement = document.createElement('div');
        toastElement.classList.add('toast', 'toast-custom');
        toastElement.setAttribute('role', 'alert');
        toastElement.setAttribute('aria-live', 'assertive');
        toastElement.setAttribute('aria-atomic', 'true');


        // Create the toast header
        const toastHeader = document.createElement('div');
        toastHeader.classList.add('toast-header');

        const strong = document.createElement('strong');
        strong.classList.add('mr-auto');
        strong.textContent = header;

        const closeSpan = document.createElement('span');
        closeSpan.setAttribute('aria-hidden', 'true');
        closeSpan.innerHTML = '&times;';

        toastHeader.appendChild(strong);

        // Create the toast body
        const toastBody = document.createElement('div');
        toastBody.classList.add('toast-body');

        // Customize confirmation message based on item type
        let confirmationMessage = message;

        const textNode = document.createTextNode(confirmationMessage);

        // Create the toast footer
        const toastFooter = document.createElement('div');
        toastFooter.classList.add('toast-footer', 'toastFooter');

        // Create the Yes and No buttons
        const confirmButton = document.createElement('button');
        confirmButton.setAttribute('type', 'button');
        confirmButton.classList.add('btn', 'btn-outline-success', 'ml-4', 'confirm');
        confirmButton.textContent = 'Ok';

        toastBody.appendChild(textNode);

        // Append buttons to footer
        toastFooter.appendChild(confirmButton);

        // Append header and body to toast
        toastElement.appendChild(toastHeader);
        toastElement.appendChild(toastBody);
        toastElement.appendChild(toastFooter)

        // Append toast to the document
        document.body.appendChild(toastElement);

        // Show the toast
        const toast = new bootstrap.Toast(toastElement);
        toast.show();

        // Add event listener for confirm and cancel buttons
        confirmButton.addEventListener('click', function () {
            toast.hide();
        });
    }


async function showDeleteConfirmation(itemId, itemType) {

    // Check if itemType is 'task' and if it has associated subtasks
    if (itemType === 'Tasks' && await hasSubtasks(itemId)) {
        // If task has subtasks, display a message and return without showing the delete confirmation
        displayToast('Delete Task', 'Cannot delete task with subtasks.', 'warning');
        return;
    }

    // Customize confirmation message based on item type
    let confirmationMessage = '';
    if (itemType === 'Tasks') {
        confirmationMessage = 'Are you sure you want to delete this task?';
    } else if (itemType === 'SubTasks') {
        confirmationMessage = 'Are you sure you want to delete this subtask?';
    }

    // Create the toast element
    const toastElement = document.createElement('div');
    toastElement.classList.add('toast', 'toast-custom');
    toastElement.setAttribute('role', 'alert');
    toastElement.setAttribute('aria-live', 'assertive');
    toastElement.setAttribute('aria-atomic', 'true');

    // Create the toast header
    const toastHeader = document.createElement('div');
    toastHeader.classList.add('toast-header');

    const strong = document.createElement('strong');
    strong.classList.add('mr-auto');
    strong.textContent = 'Confirm Delete';

    const closeSpan = document.createElement('span');
    closeSpan.setAttribute('aria-hidden', 'true');
    closeSpan.innerHTML = '&times;';

    toastHeader.appendChild(strong);

    // Create the toast body
    const toastBody = document.createElement('div');
    toastBody.classList.add('toast-body');

    const textNode = document.createTextNode(confirmationMessage);

    // Create the toast footer
    const toastFooter = document.createElement('div');
    toastFooter.classList.add('toast-footer', 'toastFooter');

    // Create the Yes and No buttons
    const confirmButton = document.createElement('button');
    confirmButton.setAttribute('type', 'button');
    confirmButton.classList.add('btn', 'btn-outline-success', 'ml-4', 'confirm-delete');
    confirmButton.textContent = 'Yes';

    const cancelButton = document.createElement('button');
    cancelButton.setAttribute('type', 'button');
    cancelButton.classList.add('btn', 'btn-outline-warning', 'ml-4', 'cancel-delete');
    cancelButton.textContent = 'No';

    toastBody.appendChild(textNode);

    // Append buttons to footer
    toastFooter.appendChild(confirmButton);
    toastFooter.appendChild(cancelButton);

    // Append header and body to toast
    toastElement.appendChild(toastHeader);
    toastElement.appendChild(toastBody);
    toastElement.appendChild(toastFooter)

    // Append toast to the document
    document.body.appendChild(toastElement);

    // Show the toast
    const toast = new bootstrap.Toast(toastElement);
    toast.show();

    // Add event listener for confirm and cancel buttons
    confirmButton.addEventListener('click', function () {
        deleteItem(itemId, itemType);
        toast.hide();
    });

    cancelButton.addEventListener('click', function () {
        toast.hide();
    });
}

async function hasSubtasks(taskId) {
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    const response = await fetch(`https://localhost:8000/api/Tasks/subtasks?taskId=${taskId}`, {
        method: "GET",
        headers: headers,
    });

    if (!response.ok) {
        throw new Error("Failed to fetch subtasks");
    }

    const subtasks = await response.json();
    return subtasks && subtasks.$values.length > 0;
}

function deleteItem(itemId, itemType) {
    // Get JWT token and returns a promise
    const token = getToken();
    const headers = { "Content-Type": "application/json" };

    // Prepare headers with content type and authorization if token available
    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    fetch(`https://localhost:8000/api/${itemType}/${itemId}`, {
        method: "DELETE",
        headers: headers,
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`Failed to delete ${itemType}`);
            }

            location.reload();
        })
        .catch(error => {
            console.error(`Error deleting ${itemType}`, error)
        });
}



export { showDeleteConfirmation};
export { displayToast };