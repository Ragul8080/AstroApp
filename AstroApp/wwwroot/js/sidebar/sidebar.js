document.addEventListener("DOMContentLoaded", () => {
    const toggle = document.getElementById("header-toggle");
    const nav = document.getElementById("nav-bar");
    const body = document.getElementById("body");

    if (toggle && nav && body) {
        toggle.addEventListener("click", () => {
            nav.classList.toggle("show");      // show/hide sidebar
            body.classList.toggle("body-pd");  // add/remove body padding
            toggle.classList.toggle("bx-x");   // change icon
        });
    }

    // Active link highlight
    const linkColor = document.querySelectorAll(".nav_link");
    linkColor.forEach((l) => {
        l.addEventListener("click", function () {
            linkColor.forEach((el) => el.classList.remove("active"));
            this.classList.add("active");
        });
    });
});
