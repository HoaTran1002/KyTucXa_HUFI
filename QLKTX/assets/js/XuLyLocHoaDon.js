const host = "https://localhost:44354/api/Values/";
var callApi = (api, select) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data, select);
        });
}

callApi("https://localhost:44354/api/Values/getKhus", 'khu');
var renderData = (array, select) => {
    let row = '<option disable value=""> Chọn </option>';
    if (array != null)
        array.forEach(element => {
            row += `<option value="${element.Ma}">${element.Ten}</option>`
        });
    document.querySelector("#" + select).innerHTML = row
}

$("#khu").change(() => {
    callApi(host + "getTangs?maKhu=" + $("#khu").val(), 'tang');
    phong.innerHTML = ' <option disable value=""> Chọn </option>';
});
$("#tang").change(() => {
    callApi(host + "getPhongs?maTang=" + $("#tang").val(), 'phong');
});
$("#phong").change(() => {

    //if ($("#phong").val() != "") {
    //    disabledDV(false)
    //    printResult();
    //    idValue.innerText = $("#phong option:selected").text()
    //}
    //else {
    //    dvDon.innerHTML = "";
    //    disabledDV(true)
    //}
})



//show tất cả
showAllHD.addEventListener("click", () => {
    if (showAllHD.checked)
        showloc.hidden = true;
    else
        showloc.hidden = false;
})

//sử lý chọn lọc tháng năm
const thangHienTai = document.getElementById('thangHienTai');
thangHienTai.addEventListener("click",() => {
  
    if (!thangHienTai.checked) {        
        nam.disabled = thang.disabled = false;
    }
    else
        nam.disabled = thang.disabled = true;    
})
var th = (new Date()).getMonth()+1;
const nam = document.getElementById('nam')
nam.addEventListener("change", () => {
    XuatThang()
})
var XuatThang = () => {
    let sl = 12;
    if (nam.selectedIndex == 0)
        sl = th;
    var s = '<option selected value="0">Chọn</option>';
    for (var i = 1; i <= sl; i++)
        s += '<option value="' + i + '">Tháng ' + i + '</option>';
    thang.innerHTML = s;
}
XuatThang()

//xử lý chọn lọc trạng thái
const locs = document.querySelectorAll('.loctt');
locs.forEach(loc => loc.addEventListener("click", () => {
    if (!loc.checked)
        for (var i = 0; i < locs.length; i++)
            if (locs[i].checked)
                return;
    loc.checked = true;
}))




const subValue = document.getElementById('subvalue');
const idValue = document.getElementById('idValue');
const link = "https://localhost:44354/Admin/XemChiTietHoaDon?maHoaDon=";
btnLoc = document.getElementById('btn_loc')
btnLoc.addEventListener("click", () => {
    LocHoaDon(showAllHD.checked)
})
var LocHoaDon = (value) => {
    axios.get("https://localhost:44354/api/HoaDonApi/" + "getHoaDons?tatCa=" + value + "&khu=" + $("#khu").val() +
        "&tang=" + $("#tang").val() + "&phong=" +
        $("#phong").val() + "&thangHienTai=" +
        thangHienTai.checked + "&nam=" +
        $("#nam").val() + "&thang=" + $("#thang").val() + "&daThanhToan=" +
        daThanhToan.checked + "&chuaThanhToan=" + chuaThanhToan.checked).then((dt) => {
            var s = '';
            var dtt = dt.data            
            dtt.forEach(item => {
                let nut = "";
                if (!item.DaThanhToan)
                    nut = '<button class="btn_thanhtoan open_btn btn_xacnhan" value="' + item.MaHoaDon + '">Xác Nhận</button>';
                s +=` <tr>
                        <td>${item.MaHoaDon}</td>
                        <td>${item.MaPhong}</td>
                        <td>${item.NgayTao}</td >
                        <td>${item.ThanhTien.toLocaleString('de-DE')}đ</td>
                        <td>${item.NguoiTao}</td>
                        <td>${item.DaThanhToan == true ? "Đã " : "Chưa "}thanh toán</td>
                        <td><a href="${link + item.MaHoaDon}">Chi tiết</a>  ${nut}</td>
                      </tr>`
                console.log(item.MaHoaDon)
            })
            if (s == '') {
                showHoaDon.innerHTML = "không tìm thấy dữ liệu";
                return;
            }
            showHoaDon.innerHTML = s;
            let openModalBtns = document.querySelectorAll('.open_btn')
            openModalBtns.forEach(openModalBtn => openModalBtn.addEventListener('click', toggleModal))
            let btns = document.querySelectorAll('.btn_xacnhan');
            btns.forEach(btn => btn.addEventListener("click", () => {
                subValue.value = btn.value;
                idValue.innerHTML = btn.value;
            }))
        })

}
LocHoaDon(true)

const modal = document.querySelector('.modall')
const iconCloseModal = document.querySelector('.modal__headerr i')
const buttonCloseModal = document.querySelector('.close')
function toggleModal() {
    modal.classList.toggle('an')
}
iconCloseModal.addEventListener('click', toggleModal)
buttonCloseModal.addEventListener('click', toggleModal)

