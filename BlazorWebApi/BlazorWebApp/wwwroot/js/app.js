
function showModal (modalId) {
    $('#' + modalId).modal('show');
}
function hideModal (modalId) {
    $('#' + modalId).modal('hide');
}
function showAlert(message) {
    alert(message);
}

function setCookie(name, value) {
    var expires = "";
    var date = new Date();
    date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000)); // Set cookie to expire in 7 days
    expires = "; expires=" + date.toUTCString();
    document.cookie = name + "=" + (value || "") + expires + "; path=/; secure; samesite=None";
}