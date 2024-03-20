/* Full Screen */
const fullscreenButton = document.getElementById("sherah-header__full");
const htmlElement = document.documentElement;

fullscreenButton.addEventListener("click", () => {
    if (document.fullscreenElement) {
        document.exitFullscreen();
    } else {
        htmlElement.requestFullscreen();
    }
});

/* Dark Mode */
const button = document.getElementById("sherah-dark-light-button");
const action = document.querySelectorAll(
    "#sherah-sidebarmenu__dark, #sherah-dark-light"
);

button.addEventListener("click", function () {
    action.forEach((el) => {
        el.classList.toggle("active");
    });
    localStorage.setItem("isDark", action[0].classList.contains("active"));
});

if (localStorage.getItem("isDark") === "true") {
    action.forEach((el) => {
        el.classList.add("active");
    });
}

/* Sherah Sidebar Menu */
const cs_button = document.querySelectorAll(".sherah__sicon");
const menuItems = document.querySelectorAll("#sherahMenu li a");
const menuItemsWithChildren = document.querySelectorAll("#sherahMenu li a[data-bs-toggle='collapse']");
const dashboardLink = document.querySelector("#dashboard-link");
const logo = document.querySelector("#logo-link");
const cs_action = document.querySelectorAll(
    "#sherahMenu, .sherah-header, .sherah-adashboard"
);

cs_button.forEach((button) => {
    button.addEventListener("click", function () {
        cs_action.forEach((el) => {
            el.classList.toggle("sherah-close");
        });
        localStorage.setItem(
            "iscicon",
            cs_action[0].classList.contains("sherah-close")
        );
    });
});

if (localStorage.getItem("iscicon") === "true") {
    cs_action.forEach((el) => {
        el.classList.add("sherah-close");
    });
}

const menuState = {};
menuItems.forEach((menuItem) => {
    menuItem.addEventListener("click", function () {
        menuItems.forEach((item) => {
            item.classList.add("collapsed");
        });

        this.classList.remove("collapsed");

        menuItems.forEach((item) => {
            menuState[item.getAttribute('href')] = item.classList.contains('collapsed');
        });
        localStorage.setItem('menuState', JSON.stringify(menuState));
    });
});
menuItemsWithChildren.forEach((menuItem) => {
    menuItem.addEventListener("click", function () {
        const isExpanded = this.getAttribute("aria-expanded") === "true";

        if (isExpanded) {
            this.classList.remove("collapsed");
            this.setAttribute("aria-expanded", "false");
        } else {
            this.classList.add("collapsed");
            this.setAttribute("aria-expanded", "true");
        }

        menuState[this.getAttribute('href')] = !isExpanded;
        localStorage.setItem('menuState', JSON.stringify(menuState));
    });
});
function restoreMenuState() {
    const menuState = JSON.parse(localStorage.getItem('menuState'));
    if (menuState) {
        Object.keys(menuState).forEach((link) => {
            const menuItem = document.querySelector(`#sherahMenu li a[href="${link}"]`);
            if (menuItem) {
                if (menuState[link]) {
                    menuItem.classList.add('collapsed');
                } else {
                    menuItem.classList.remove('collapsed');
                }
            }
        });
    }

    const currentPageUrl = window.location.pathname;
    if (currentPageUrl === "/Dashboard/Dashboard") {
        dashboardLink.classList.remove('collapsed');
        menuItems.forEach((item) => {
            if (item !== dashboardLink) {
                item.classList.add("collapsed");
            }
        });
    }

    menuItemsWithChildren.forEach((menuItem) => {
        const submenu = document.querySelector(menuItem.getAttribute('data-bs-target'));
        if (submenu && submenu.contains(document.querySelector(`a[href="${currentPageUrl}"]`))) {
            menuItem.classList.remove('collapsed');
        }
    });
}
window.addEventListener('load', restoreMenuState());