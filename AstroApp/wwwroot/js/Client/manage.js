$(document).ready(function () {
    var clientUrl = $('#clientUrl').val();

    $('#clientTable').DataTable({
        ajax: {
            url: clientUrl,
            type: 'GET',
            dataSrc: 'data'
        },
        columns: [
            { data: 'id' },
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'email' },
            { data: 'phone' },
            { data: 'gender' },
            { data: 'zodiacSign' },
            {
                data: 'birthDate',
                render: function (data) {
                    return new Date(data).toLocaleDateString();
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return `
                        <button class="btn btn-sm btn-primary edit-btn" data-id="${row.id}">Edit</button>
                        <button class="btn btn-sm btn-danger delete-btn" data-id="${row.id}">Delete</button>
                    `;
                }
            }
        ]
    });

    // Optional: Search box
    $('.search-input').on('keyup', function () {
        $('#clientTable').DataTable().search(this.value).draw();
    });
});
