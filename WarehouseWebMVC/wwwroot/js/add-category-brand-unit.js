function addCategory() {
    var categoryValue = document.getElementById("category-input").value;
    if (categoryValue.trim() === "") {
        Swal.fire({
            icon: "error",
            title: "Error!",
            text: "Category field have to be filled!",
        });
    } else {
        var xhr = new XMLHttpRequest();

        var url = "/Product/AddCategory";
        var method = "POST";

        xhr.open(method, url, true);

        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var response = JSON.parse(xhr.responseText);
                    if (response.success) {
                        Swal.fire({
                            title: "Yeah!",
                            text: "Your category has been added.",
                            icon: "success"
                        });
                        document.getElementById("category-input").value = "";
                        var select = document.getElementById("category-select");
                        var option = document.createElement("option");
                        option.value = response.category.CategoryId;
                        option.text = response.category.Name;
                        select.appendChild(option);
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: "Oops! The category name already exists. Please try another category.",
                            icon: "error"
                        });
                    }
                } else {
                    Swal.fire({
                        title: "Error!",
                        text: "Failed to communicate with server. Please try again later.",
                        icon: "error"
                    });
                }
            }
        };

        var data = JSON.stringify({ categoryName: categoryValue });
        xhr.send(data);
    }
}

function addBrand() {
    var brandValue = document.getElementById("brand-input").value;
    if (brandValue.trim() === "") {
        Swal.fire({
            icon: "error",
            title: "Error!",
            text: "Brand field have to be filled!",
        });
    } else {
        var xhr = new XMLHttpRequest();

        var url = "/Product/AddBrand";
        var method = "POST";

        xhr.open(method, url, true);

        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var response = JSON.parse(xhr.responseText);
                    if (response.success) {
                        Swal.fire({
                            title: "Yeah!",
                            text: "Your brand has been added.",
                            icon: "success"
                        });
                        document.getElementById("brand-input").value = "";
                        var select = document.getElementById("brand-select");
                        var option = document.createElement("option");
                        option.value = response.brand.BrandId;
                        option.text = response.brand.Name;
                        select.appendChild(option);
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: "Oops! The brand name already exists. Please try another brand.",
                            icon: "error"
                        });
                    }
                } else {
                    Swal.fire({
                        title: "Error!",
                        text: "Failed to communicate with server! Please try again later.",
                        icon: "error"
                    });
                }
            }
        };

        var data = JSON.stringify({ brandName: brandValue });
        xhr.send(data);
    }
}

function updateUnitDropdown() {
    var storedUnits = localStorage.getItem("units");
    var units = storedUnits ? JSON.parse(storedUnits) : [];
    var select = document.getElementById("unit-select");

    units.forEach(function (unit) {
        var option = document.createElement("option");
        option.value = unit.value;
        option.text = unit.text;
        select.appendChild(option);
    });
}

function updateUnitDropdownAfterAdd() {
    var storedUnits = localStorage.getItem("units");
    var units = storedUnits ? JSON.parse(storedUnits) : [];
    var select = document.getElementById("unit-select");

    var lastUnit = units[units.length - 1];
    var option = document.createElement("option");
    option.value = lastUnit.value;
    option.text = lastUnit.text;
    select.appendChild(option);
}

function addUnit() {
    var unitValue = document.getElementById("unit-input").value;
    if (unitValue.trim() === "" || isUnitExist(unitValue) || isReservedUnit(unitValue)) {
        Swal.fire({
            icon: "error",
            title: "Error!!",
            text: "Unit name already exists or is invalid!!",
        });
    } else {
        var storedUnits = localStorage.getItem("units");
        var units = storedUnits ? JSON.parse(storedUnits) : [];
        units.push({ text: unitValue, value: unitValue });
        localStorage.setItem("units", JSON.stringify(units));

        updateUnitDropdownAfterAdd();

        Swal.fire({
            title: "Yeah!",
            text: "Your unit has been added.",
            icon: "success"
        });

        document.getElementById("unit-input").value = "";
    }
}

function isUnitExist(unitValue) {
    var storedUnits = localStorage.getItem("units");
    var units = storedUnits ? JSON.parse(storedUnits) : [];

    return units.some(function (unit) {
        return unit.value.toUpperCase() === unitValue.toUpperCase();
    });
}

function isReservedUnit(unitValue) {
    var reservedValues = ["Piece", "Pair", "piece", "pair"];
    return reservedValues.includes(unitValue);
}

