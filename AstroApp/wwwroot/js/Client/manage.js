$(document).ready(function () {
    var clientUrl = $('#clientUrl').val();
    var editUrl = $('#editUrl').val();
    var deleteUrl = $('#deleteUrl').val();

    var table = $('#clientTable').DataTable({
        ajax: {
            url: clientUrl,
            type: 'GET',
            dataSrc: ''
        },
        columns: [
            { data: 'clientId', title: 'ID', visible: false },
            { data: 'firstName', title: 'First Name' },
            { data: 'lastName', title: 'Last Name' },
            { data: 'email', title: 'Email' },
            { data: 'phone', title: 'Phone' },
            {
                data: null,
                title: 'Actions',
                render: function (data, type, row) {
                    return `
                                <button class="btn btn-sm btn-primary edit-btn">Edit</button>
                                <button class="btn btn-sm btn-danger delete-btn">Delete</button>
                            `;
                }
            }
        ],
        pageLength: 25,
        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
        responsive: true
    });

    // Search input
    $('.search-input').on('keyup', function () {
        table.search(this.value).draw();
    });

    // Add New Client Button
    $('#CreateUser').on('click', function () {
        window.location.href = $("#createUrl").val();
    });

    // Edit Button 
    $('#clientTable tbody').on('click', '.edit-btn', function () {
        var rowData = table.row($(this).closest('tr')).data();
        var id = rowData.clientId; // Get clientId from the hidden column
        if (id) {
            window.location.href = editUrl + '/' + id;
        } else {
            console.error('Client ID not found for this row.');
        }
    });

    // Delete Button
    $('#clientTable tbody').on('click', '.delete-btn', function () {
        var rowData = table.row($(this).closest('tr')).data();
        var id = rowData.clientId;
        if (id) {
            window.location.href = deleteUrl + '/' + id;
        } else {
            console.error('Client ID not found for this row.');
        }
    });
});