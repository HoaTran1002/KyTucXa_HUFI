const btns = document.querySelectorAll('.btn_xacnhan');
const subValue = document.getElementById('subvalue');
const idValue = document.getElementById('idValue');
btns.forEach(btn => btn.addEventListener("click", () => {
    subValue.value = btn.value;
    idValue.innerHTML = btn.value;
}))