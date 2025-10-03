$(document).ready(function () {
    fetchClients();

    // Fetch all clients
    function fetchClients() {
        $.ajax({
            url: "/api/ScheduleApi",
            type: "GET",
            success: function (data) {
                $("#clientCards").empty();
                if (data.length === 0) {
                    $("#clientCards").append("<p class='text-muted'>No clients found.</p>");
                }
                data.forEach(client => {
                    let card = `
                        <div class="col-md-4">
                            <div class="client-card">
                                <div class="client-icon"><i class="bi bi-person-circle"></i></div>
                                <div class="client-info">
                                    <h5>${client.firstName} <span class="zodiac">${client.zodiacSign}</span></h5>
                                    <p>Born: ${client.birthDate.substring(0, 10)}</p>
                                    <p>${client.email}</p>
                                    <p>${client.phone}</p>
                                    <small class="last-visit">Last visit: ${client.createdAt.substring(0, 10)}</small>
                                </div>
                            </div>
                        </div>`;
                    $("#clientCards").append(card);
                });
            },
            error: function (err) {
                console.error("Error fetching clients:", err);
            }
        });
    }

    // Search filter
    $("#searchBox").on("keyup", function () {
        let term = $(this).val().toLowerCase();
        $(".client-card").each(function () {
            let text = $(this).text().toLowerCase();
            $(this).toggle(text.includes(term));
        });
    });
});
