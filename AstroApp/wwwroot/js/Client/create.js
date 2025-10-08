$(document).ready(function () {
    $('#rightSection').hide();
    let appointmentCount = 1;
    // Load all dropdowns
    getState();
    getZodiacSign();
    getStar();
    getGender();

    // Initialize Select2 for all dropdowns
    $('#inputState').select2({
        placeholder: "Select State",
        allowClear: true,
        width: '100%'
    });
    $('#inputZodiac').select2({
        placeholder: "Select Zodiac Sign",
        allowClear: true,
        width: '100%'
    });
    $('#inputStar').select2({
        placeholder: "Select Star",
        allowClear: true,
        width: '100%'
    });
    $('#inputGender').select2({
        placeholder: "Select Gender",
        allowClear: true,
        width: '100%'
    });

    // ---------------- Functions ----------------


    $('#chkBookAppointment').change(function () {
        if ($(this).is(':checked')) {
            $('#rightSection').slideDown(300); // Show with smooth animation

        } else {
            $('#rightSection').slideUp(300);   // Hide with smooth animation
            $('#rightButtonContainer #buttonContainer').appendTo('#clientForm');
        }

    });


    function getState() {
        $.ajax({
            url: $('#stateUrl').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var stateSelect = $('#inputState');
                stateSelect.empty().append('<option></option>'); // Default blank

                if (response && response.data && Array.isArray(response.data)) {
                    $.each(response.data, function (index, state) {
                        if (typeof state === 'string') {
                            stateSelect.append($('<option>', { value: state, text: state }));
                        } else if (state.satesId && state.satesName) {
                            stateSelect.append($('<option>', { value: state.satesId, text: state.satesName }));
                        }
                    });
                    var selected = $("#selectedState").val();
                    if (selected) stateSelect.val(selected).trigger('change');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching states:', error);
            }
        });
    }

    function getZodiacSign() {
        $.ajax({
            url: $('#zodiacUrl').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var zodiacSelect = $('#inputZodiac');
                zodiacSelect.empty().append('<option></option>'); // Default blank

                if (response && response.data && Array.isArray(response.data)) {
                    $.each(response.data, function (index, zodiac) {
                        if (zodiac.id && zodiac.engName && zodiac.tamilName) {
                            zodiacSelect.append($('<option>', {
                                value: zodiac.id,
                                text: zodiac.engName + " (" + zodiac.tamilName + ")"
                            }));
                        }
                    });
                    var selected = $("#selectedZodiac").val();
                    if (selected) zodiacSelect.val(selected).trigger('change');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching zodiac signs:', error);
            }
        });
    }

    function getStar() {
        $.ajax({
            url: $('#starUrl').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var starSelect = $('#inputStar');
                starSelect.empty().append('<option></option>'); // Default blank

                if (response && response.data && Array.isArray(response.data)) {
                    $.each(response.data, function (index, star) {
                        if (star.id && star.engName && star.tamilName) {
                            starSelect.append($('<option>', {
                                value: star.id,
                                text: star.engName + " (" + star.tamilName + ")"
                            }));
                        }
                    });
                    var selected = $("#selectedStar").val();
                    if (selected) starSelect.val(selected).trigger('change');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching stars:', error);
            }
        });
    }

    function getGender() {
        $.ajax({
            url: $('#genderUrl').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var genderSelect = $('#inputGender');
                genderSelect.empty().append('<option></option>'); // Default blank

                if (response && response.data && Array.isArray(response.data)) {
                    $.each(response.data, function (index, gender) {
                        if (gender.genderId && gender.genderName) {
                            genderSelect.append($('<option>', {
                                value: gender.genderId,
                                text: gender.genderName
                            }));
                        }
                    });
                    var selected = $("#selectedGender").val();
                    if (selected) genderSelect.val(selected).trigger('change');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching genders:', error);
            }
        });
    }

    $(document).ready(function () {
        // Bind separate submit handlers for create and update
        $('#btnCreate').on('click', function (e) {
            e.preventDefault();
            createClient();
        });

        $('#btnUpdate').on('click', function (e) {
            e.preventDefault();
            updateClient();
        });
    });

    // Create client function (your existing function)
    function createClient() {
        var client = getClientData();

        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/api/ClientsApi/Create',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'RequestVerificationToken': token
            },
            data: JSON.stringify(client),
            success: function (response) {
                console.log('Success:', response);
                if (response.success) {
                    alert('User created successfully!');
                    window.location.href = '/Client/Manage';
                } else {
                    alert('Failed to create user: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error:', status, error);
                console.error('Response text:', xhr.responseText);
                alert('An error occurred while creating the user.');
            }
        });
    }

    // Update client function
    function updateClient() {
        var client = getClientData();
        var clientId = $('#clientId').val();

        if (!clientId) {
            alert('Client ID is missing.');
            return;
        }

        client.clientId = clientId;

        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/api/ClientsApi/Update/' + clientId, // send ID in the URL
            type: 'PUT', // PUT for updates
            contentType: 'application/json',
            headers: {
                'RequestVerificationToken': token
            },
            data: JSON.stringify(client),
            success: function (response) {
                console.log('Success:', response);
                if (response.success) {
                    alert('User updated successfully!');
                    window.location.href = '/Client/Manage';
                } else {
                    alert('Failed to update user: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error:', status, error);
                console.error('Response text:', xhr.responseText);
                alert('An error occurred while updating the user.');
            }
        });
    }

    function getClientData() {
        let appointments = [];
        if ($('#chkBookAppointment').is(':checked')) {
            $('#appointmentsContainer .appointment-form').each(function () {
                let date = $(this).find('.appointment-date').val();
                let session = $(this).find('input[type="radio"]:checked').val();
                let slot = $(this).find('.slot-btn.active-slot').text();

                if (date && session && slot) {
                    appointments.push({
                        appointmentDate: date,
                        sessionMode: session,
                        timeSlot: slot
                    });
                }
            });
        }
        return {
            firstName: $('#inputFirstName').val(),
            lastName: $('#inputLastName').val(),
            email: $('#inputEmail').val(),
            phone: $('#inputPhone').val(),
            gender: $('#inputGender').val(),
            dateOfBirth: $('#inputDOB').val(),
            birthTime: $('#inputDOT').val(),
            addressLine1: $('#inputAddress').val(),
            addressLine2: $('#inputAddress2').val(),
            city: $('#inputCity').val(),
            state: $('#inputState').val(),
            zipCode: $('#inputZip').val(),
            zodiacSignId: $('#inputZodiac').val(),
            starId: $('#inputStar').val(),
            note: $('#inputNote').val(),
            appointmentChk: $('#chkBookAppointment').is(':checked'),
            appointments: appointments

        };
    }

    // Delete client by ID
    function deleteClient(clientId) {
        if (!confirm('Are you sure you want to delete this client?')) {
            return;
        }

        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/api/ClientsApi/Delete/' + clientId,
            type: 'DELETE',
            headers: {
                'RequestVerificationToken': token
            },
            success: function (response) {
                console.log('Success:', response);
                if (response.success) {
                    alert('User deleted successfully!');
                    window.location.href = '/Client/Manage';
                } else {
                    alert('Failed to delete user: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error:', status, error);
                console.error('Response text:', xhr.responseText);
                alert('An error occurred while deleting the user.');
            }
        });
    }
});
