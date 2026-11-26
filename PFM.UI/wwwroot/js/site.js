// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {

    const themeToggle = document.getElementById("themeToggle");
    const themeIcon = document.getElementById("themeIcon");
    const themeText = document.getElementById("themeText");

    const html = document.documentElement;

    // Read saved theme from Local Storage
    let currentTheme = localStorage.getItem("pfm-theme") || "light";

    // Apply theme
    function applyTheme(theme) {

        html.setAttribute("data-theme", theme);

        if (theme === "dark") {

            themeIcon.className = "bi bi-sun-fill";
            themeText.textContent = "Light Mode";

        } else {

            themeIcon.className = "bi bi-moon-stars-fill";
            themeText.textContent = "Dark Mode";

        }

        // Save selected theme
        localStorage.setItem("pfm-theme", theme);
    }

    // Apply saved theme on page load
    applyTheme(currentTheme);

    // Theme button click
    if (themeToggle) {

        themeToggle.addEventListener("click", function () {

            currentTheme =
                currentTheme === "light" ? "dark" : "light";

            applyTheme(currentTheme);

        });

    }

});
