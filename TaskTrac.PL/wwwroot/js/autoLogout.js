
import { displayToast } from './deleteConfirmation.js';

var logoutTime = 300000;
var logoutTimer;

function startLogoutTimer() {
    logoutTimer = setTimeout(logout, logoutTime);
}

function resetLogoutTimer() {
    clearTimeout(logoutTimer);
    startLogoutTimer();
}

function logout() {
    //Displays toast notification for auto logout
    displayToast('Logout', 'TaskTrak has logged you out, due to inactivity!', 'warning');

    //redirects user to login page after logout
    setTimeout(function () {
        window.location.href = "/Login/Login";
    }, 5000)
}


//Event listeners to reset logoutTimer when there is user activity

// Start logout timer when the window loads
window.onload = startLogoutTimer;

// Reset logout timer when there is user activity
window.addEventListener("mousemove", resetLogoutTimer);
window.addEventListener("keypress", resetLogoutTimer);