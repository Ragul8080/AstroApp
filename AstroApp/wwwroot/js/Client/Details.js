$(document).ready(function () {
    var editUrl = $('#editUrl').val();
    // Edit button click
    $(document).on('click', '#editbtn', function () {
        var clientId = $('#clientId').val();
        if (clientId) {
            window.location.href = editUrl + '/' + clientId;
        } else {
            console.error('Client ID not found.');
        }
    });
});