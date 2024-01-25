function importProduct({
    pId = 0,
    pName = '',
    pPrice = 0,
    pQuantity = 1,
    pTotal = pPrice * pQuantity
}) {
    var main = document.getElementById('product-import-table');

    if (main) {
        var tableRow = document.createElement('tr');

        tableRow.setAttribute('id', 'table-row-' + pId);
        tableRow.classList.add('text-center');
        tableRow.style.borderColor = '#5A5A5A';
        tableRow.innerHTML = `
            <td class="product-remove" style="padding:20px 0 0 5px;">
               <i class="fa-solid fa-square-xmark" onclick="removeProduct(${pId})" style="font-size:20px;color:#FF6767;cursor:pointer;"></i>
               <p style="display:none;">${pId}</p>
            </td>
            <td class="product-name" style="min-width:350px;">
               <h6 style="padding-top:8px;margin:auto;font-weight:500;max-width:350px;">${pName}</h6>
            </td>
            <td class="price" style="max-width:150px;">
               <div class="input-group mb-3" style="max-height:30px;display:flex;justify-content:center;align-items:center;">
                   <input type="number" name="quantity" class="quantity input-number form-control" value="${pPrice.toFixed(2)}"
                         id="product-import-price-${pId}" oninput="changePrice(${pId})"
                         style="text-align:center;max-height:45px;max-width:80px;margin:0 12px;padding:0;color:#5A5A5A;" />
               </div>
            </td>
            <td class="quantity" style="max-width: 150px;">
               <div class="input-group mb-3" style="max-height:30px;display:flex;justify-content:center;align-items:center;">
                        <span class="input-group-btn mr-2">
                            <button type="button" onclick="productIncDec(${pId},'minus')" class="quantity-left-minus btn" data-type="minus" data-field=""
                                    style="width:20px;height:20px;display:flex;justify-content:center;align-items:center;">
                                <i class="fa-solid fa-minus" style="font-size:16px;color:#666;"></i>
                            </button>
                        </span>
                        <input type="number" name="quantity" id="quantity-${pId}" class="quantity input-number form-control" value="${pQuantity}"
                            style="text-align:center;max-height:45px;max-width:80px;margin:0 12px;padding:0;color:#5A5A5A;" />
                        <span class="input-group-btn ml-2">
                            <button type="button" onclick="productIncDec(${pId},'plus')" class="quantity-right-plus btn" data-type="plus" data-field=""
                                    style="width:20px;height:20px;display:flex;justify-content:center;align-items:center;">
                                <i class="fa-solid fa-plus" style="font-size:16px;color:#666;"></i>
                            </button>
                        </span>
                    </div>
               </td>
               <td class="total" id="product-import-total-${pId}" style="padding-top:20px;max-height:30px;">$${pTotal}</td>
        `;

        main.appendChild(tableRow);

        updateOverallTotal();

        var button = document.getElementById('button-plus' + pId);
        button.classList.add('button-plus--disabled');
        button.disabled = true;
    }
}

function removeProduct(pId) {
    var main = document.getElementById('product-import-table');
    var row = document.getElementById('table-row-' + pId);
    var button = document.getElementById('button-plus' + pId);

    main.removeChild(row);

    // Update overall total after removing the row
    updateOverallTotal();

    button.classList.remove('button-plus--disabled');
    button.disabled = false;
}

function productIncDec(pId, type) {
    var quantityInput = document.getElementById('quantity-' + pId);
    var quantity = parseInt(quantityInput.value);
    var priceInput = document.getElementById('product-import-price-' + pId);
    var price = parseFloat(priceInput.value);
    var total = document.getElementById('product-import-total-' + pId);

    if (type === "minus" && quantity > 1) {
        quantityInput.value = quantity - 1;
    }

    if (type === "plus") {
        quantityInput.value = quantity + 1;
    }

    // Update total for the current row
    updateRowTotal(pId, quantityInput, price);
    // Update overall total
    updateOverallTotal();
}

function changePrice(pId) {
    var quantityInput = document.getElementById('quantity-' + pId);
    var priceInput = document.getElementById('product-import-price-' + pId);

    // Update total for the current row
    updateRowTotal(pId, quantityInput, priceInput.value);
    // Update overall total
    updateOverallTotal();
}

function updateRowTotal(pId, quantityInput, price) {
    var total = document.getElementById('product-import-total-' + pId);
    var quantity = parseInt(quantityInput.value);

    if (!isNaN(quantity) && !isNaN(price)) {
        total.innerHTML = '$' + (price * quantity).toFixed(2);
    } else {
        total.innerHTML = '$0.00';
    }
}

document.getElementById('total-price-final').innerHTML = '$0.00';

function updateOverallTotal() {
    var totalElements = document.querySelectorAll('.total');
    var overallTotal = 0;

    totalElements.forEach(function (totalElement) {
        overallTotal += parseFloat(totalElement.textContent.replace('$', ''));
    });

    var totalFinal = document.getElementById('total-price-final');
    totalFinal.innerHTML = '$' + overallTotal.toFixed(2);
}