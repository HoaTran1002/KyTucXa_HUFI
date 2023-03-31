const khac = document.querySelector('#khac');
const lyDoKhac = document.querySelector('#lyDoKhac');
const rdo_lydos = document.querySelectorAll(".rdo_lydo");
rdo_lydos.forEach(rdo => rdo.addEventListener("click", () => {
	lyDoKhac.disabled = true;
	lyDoKhac.value = "";
}))
khac.addEventListener("click", () => {
	lyDoKhac.disabled = false;
})
lyDoKhac.addEventListener("input", () => {
	khac.value = lyDoKhac.value
});
