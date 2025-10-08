$(document).ready(function () {
    var clientUrl = $('#clientUrl').val();
    var editUrl = $('#editUrl').val();
    var detailUrl = $('#detailUrl').val();
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
                                <button class="btn btn-sm btn-primary details-btn">Details</button>
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

    $('#clientTable tbody').on('click', '.details-btn', function () {
        var rowData = table.row($(this).closest('tr')).data();
        var id = rowData.clientId; // Get clientId from the hidden column
        if (id) {
            window.location.href = detailUrl + '/' + id;
        } else {
            console.error('Client ID not found for this row.');
        }
    });

});