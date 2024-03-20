function removeSomeeTag() {
    var elementToRemove = document.querySelector('center > a[href="http://somee.com"]');
    if (elementToRemove) {
        elementToRemove.parentElement.removeChild(elementToRemove);
        console.log("Element removed successfully!");
    } else {
        console.log("Element not found.");
    }
}
