$(document).ready(function () {
    fetchClients();

    function fetchClients() {
        $.ajax({
            url: $("#clientUrl").val(),
            type: "GET",
            success: function (data) {
                const $container = $("#clientCards");
                $container.empty();

                if (!data || data.length === 0) {
                    $container.append("<p class='text-muted'>No clients found.</p>");
                    return;
                }

                data.forEach(client => {
                    const birthDate = client.dateOfBirth ? client.dateOfBirth.substring(0, 10) : "N/A";
                    const createdAt = client.createdAt ? client.createdAt.substring(0, 10) : "N/A";
                    const fullName = `${client.firstName || ""} ${client.lastName || ""}`;
                    const zodiacSign = client.zodiacSign || "—"; // fallback if not returned by API

                    const card = `
                        <div class="col-md-4">
                        <div class="client-card">
                            <div class="client-icon"><i class="bi bi-person-circle"></i></div>
                            <div class="client-info">
                                <h5>${client.firstName} <span class="zodiac">${client.zodiacSign}</span></h5>
                                <p>${client.email}</p>
                                <p>${client.phone}</p>
                            </div>
                        </div>
                    </div>`;
                    $container.append(card);
                });
            },
            error: function (err) {
                console.error("Error fetching clients:", err);
                $("#clientCards").html("<p class='text-danger'>Failed to load clients.</p>");
            }
        });

        $.ajax({
            url: $("#clientUrl").val(),
            type: "GET",
            success: function (data) {
                const $container = $("#UpclientCards");
                $container.empty();

                if (!data || data.length === 0) {
                    $container.append("<p class='text-muted'>No clients found.</p>");
                    return;
                }

                data.forEach(client => {
                    const birthDate = client.dateOfBirth ? client.dateOfBirth.substring(0, 10) : "N/A";
                    const createdAt = client.createdAt ? client.createdAt.substring(0, 10) : "N/A";
                    const fullName = `${client.firstName || ""} ${client.lastName || ""}`;
                    const zodiacSign = client.zodiacSign || "—"; // fallback if not returned by API

                    const card = `
                        <div class="col-md-4">
                        <div class="client-card">
                            <div class="client-icon"><i class="bi bi-person-circle"></i></div>
                            <div class="client-info">
                                <h5>${client.firstName} <span class="zodiac">${client.zodiacSign}</span></h5>
                                <p>${client.email}</p>
                                <p>${client.phone}</p>
                            </div>
                        </div>
                    </div>`;
                    $container.append(card);
                });
            },
            error: function (err) {
                console.error("Error fetching clients:", err);
                $("#clientCards").html("<p class='text-danger'>Failed to load clients.</p>");
            }
        });
    }

    // Search filter
    $("#searchBox").on("keyup", function () {
        const term = $(this).val().toLowerCase();
        $(".client-card").each(function () {
            const text = $(this).text().toLowerCase();
            $(this).toggle(text.includes(term));
        });
    });
});
