$(document).ready(function () {
    getState();
    getZodiacSign();
    getStar()

    // Initialize Select2
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
    function getZodiacSign() {
        $.ajax({
            url: $('#zodiacUrl').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                console.log("Zodiac API Response:", response);

                if (response && response.data && Array.isArray(response.data)) {
                    var zodiacSelect = $('#inputZodiac');
                    zodiacSelect.empty();
                    zodiacSelect.append('<option></option>'); // Default blank

                    $.each(response.data, function (index, zodiac) {
                        // Check for correct object format
                        if (zodiac.id && zodiac.engName && zodiac.tamilName) {
                            zodiacSelect.append(
                                $('<option>', {
                                    value: zodiac.id,
                                    text: zodiac.engName + " (" + zodiac.tamilName + ")"
                                })
                            );
                        } else {
                            console.warn('Unexpected zodiac format:', zodiac);
                        }
                    });

                    // Refresh Select2
                    zodiacSelect.trigger('change');
                } else {
                    console.error('Invalid response format:', response);
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
                if (response && response.data && Array.isArray(response.data)) {
                    var starSelect = $('#inputStar');
                    starSelect.empty();
                    starSelect.append('<option></option>'); // Default blank

                    $.each(response.data, function (index, star) {
                        // Check for correct object format
                        if (star.id && star.engName && star.tamilName) {
                            starSelect.append(
                                $('<option>', {
                                    value: star.id,
                                    text: star.engName + " (" + star.tamilName + ")"
                                })
                            );
                        } else {
                            console.warn('Unexpected zodiac format:', star);
                        }
                    });
                    starSelect.trigger('change');
                } else {
                    console.error('Invalid response format:', response);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching zodiac signs:', error);
            }
        });
    }
    function getState() {
        $.ajax({
            url: $('#stateUrl').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                console.log("States API Response:", response);
                if (response && response.data && Array.isArray(response.data)) {
                    var stateSelect = $('#inputState');
                    stateSelect.empty();
                    stateSelect.append('<option></option>'); // Default blank

                    $.each(response.data, function (index, state) {
                        if (typeof state === 'string') {
                            stateSelect.append(
                                $('<option>', { value: state, text: state })
                            );
                        } else if (state.satesId && state.satesName) {
                            stateSelect.append(
                                $('<option>', { value: state.satesId, text: state.satesName })
                            );
                        } else {
                            console.warn('Unexpected state format:', state);
                        }
                    });

                    stateSelect.trigger('change');
                } else {
                    console.error('Invalid response format:', response);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching states:', error);
            }
        });
    }
    $('form').on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        var client = {
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
            note: $('#inputNote').val()
        };

        $.ajax({
            url: $('#createUrl').val(),
            type: 'POST',
            data: JSON.stringify(client),
            contentType: 'application/json',
            success: function (response) {
                if (response.success) {
                    alert('User created successfully!');
                    window.location.href = '/Client/Manage'; // redirect if needed
                } else {
                    alert('Failed to create user: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error creating user:', error);
                alert('An error occurred while creating user.');
            }
        });
    });

});
