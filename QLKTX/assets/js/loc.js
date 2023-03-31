const locs = document.querySelectorAll('.loc');
const showAll = document.querySelector('.showAll');
let dem = 4;
locs.forEach(loc => loc.addEventListener("click", () => {
    if (!loc.checked) {
        dem--
        showAll.checked = false
    }
    else {
        dem++
        if (dem == locs.length)
            showAll.checked = true
    }
}))
showAll.addEventListener("click", () => {
    if (showAll.checked)
        locs.forEach(loc => loc.checked = true)
    else
        locs.forEach(loc => loc.checked = false)
})