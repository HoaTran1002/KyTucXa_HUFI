const dropArea = document.querySelector('.drag-area');
const dropText = document.querySelector('.keotha');
const button = document.querySelector('.chonfile');
const input = document.querySelector('.ip');
const luu = document.querySelector('.luu');
const huyluu = document.querySelector('.huyluu');
const reset = document.querySelector('.reset');
const anh = document.querySelector('.anh_load');
const thaotac = document.querySelector('.thaotac_load');

button.addEventListener('click', function () {
    input.click();
})
input.addEventListener('change', function () {
    const fl = this.files[0];
    showFile(fl)
})
dropArea.addEventListener('dragover', function (envent) {
    envent.preventDefault();
    dropText.textContent = "Thả để tải ảnh lên"
})
dropArea.addEventListener('dragleave', function (envent) {
    envent.preventDefault();
    dropText.textContent = "Kéo và thả để tải ảnh lên"
})
dropArea.addEventListener('drop', function (envent) {
    envent.preventDefault();
    const file = envent.dataTransfer.files[0];
    showFile(file);
})
function showFile(file) {
    const fileType = file.type;
    const validExtensions = ['image/jpeg', 'image/jpg', 'image/png'];
    if (validExtensions.includes(fileType)) {
        const fileReader = new FileReader();
        fileReader.onload = function () {
            const fileUrl = fileReader.result;
            console.log(fileUrl);
            anh.style.display = 'block';
            anh.src = fileUrl;
            thaotac.style.display = 'none';
            luu.innerHTML = '<button  onclick="LuuAnh()" class="btn btn-outline-info" data-bs-dismiss="modal">Lưu</button>';
            huyluu.style.display = 'block';
        }
        fileReader.readAsDataURL(file);
    } else {
        alert("Đây không phải là 1 file ảnh");
        dropText.textContent = "Kéo và thả để tải ảnh lên";
    }
}
function LuuAnh() {
    document.getElementById("avt").src = anh.src;
    document.getElementById("AnhMoi").setAttribute("value", anh.src);
    resett();
}
function resett() {
    thaotac.style.display = 'block';
    dropText.textContent = "Kéo và thả để tải ảnh lên"
    anh.style.display = 'none';
    huyluu.style.display = 'none';
    luu.innerHTML = '';
}
function XoaAnh() {
    document.getElementById("avt").src = "#";
    document.getElementById("AnhMoi").setAttribute("value", "#");
}
function ShowThongBao(nd) {
    alert(nd);
}