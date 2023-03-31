const lois = document.querySelectorAll('.loi');
const dvs = document.querySelectorAll('.dvPhong')
const ttdichvus = document.querySelectorAll('.ttdichvu');
const gias = document.querySelectorAll('.gia')
const chiSoCus = document.querySelectorAll('.chiSoCu');
const tenDVs = document.querySelectorAll('.tenDV');
const host = "https://localhost:44354/api/Values/";
var callApi = (api,select) => {
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
    callApi(host + "getTangs?maKhu=" + $("#khu").val(),'tang');
    phong.innerHTML = ' <option disable value=""> Chọn </option>';
    resetOutput();
});
$("#tang").change(() => {
    callApi(host + "getPhongs?maTang=" + $("#tang").val(),'phong');
    resetOutput();
    disabledDV(true)
});
$("#phong").change(() => {
    if ($("#phong").val() != "") {
        disabledDV(false)
        printResult();
        idValue.innerText = $("#phong option:selected").text()
    }
    else {
        dvDon.innerHTML = "";
        disabledDV(true)
    }
})
var resetOutput = () => {
    dvDon.innerHTML = "";
    disabledDV(true)
    resetChiSo()
}

function disabledDV(val) {
    dvs.forEach(dv => {
        dv.disabled = val
        dv.value = ""
    })
}
var resetChiSo = () => {
    chiSoCus.forEach(cs => cs.innerText = "")
}
var printResult = () => {
    axios.get(host + "getThongKeDVDonPhong?ma=" + $("#phong").val()).then((dt) => {
        var s = '';
        dt.data.forEach(item => {
            s += '<tr>';
            s += `<td>${item.TenDichVu}</td>`;
            s += `<td>${item.GiaHienTai}</td>`;
            s += `<td>${item.SoLuong}</td>`;
            s += `<td class="tongtien" >${item.GiaHienTai * item.SoLuong}</td>`;
            s += '</tr>'
        })
        dvDon.innerHTML = s;        
    });
    axios.get(host + "getDichVuPhongCoChiSo?maP=" + $("#phong").val()).then((dt) => {
        var dtt = dt.data
        dtt.forEach(item => {
            let idd = document.getElementById(item.MaDichVu)
            document.getElementById("csc" + item.MaDichVu).innerText = idd.min = idd.value = item.ChiSoHienTai;
        })
    });
    ThanhTien();
}
function CheckValue(elem) {
    var value = elem.value;
    var x = value.substr(0, value.length - 1);
    elem.value = isNaN(value) ? x : value < elem.min ? elem.min: value
}

for (var i = 0; i < dvs.length; i++) {
    let l = lois[i];
    let tb = ttdichvus[i];
    let gia = parseInt(gias[i].innerText)
    let tendv = tenDVs[i].innerText;
    dvs[i].addEventListener("input", function () {
        let moi = parseInt(this.value), cu = parseInt(this.min);
        if (this.value == "" || moi < cu) {
            l.hidden = false;
            tb.innerHTML = "";
        }
        else {
            l.hidden = true;
            let s = "";     
            s += `<td>${tendv}</td>
                  <td>${gia}</td>
                  <td> <input type="text" name="Sl${this.name}" value="${moi - cu}" readonly class="soluong btn" /></td>
                  <td class="tongtien" >${(moi - cu) * gia}</td>`
            tb.innerHTML = s;
            ThanhTien();
        }
    })
}
var ThanhTien = () => {
    let tong = 0;
    document.querySelectorAll('.tongtien').forEach(t => tong += parseInt(t.innerText))
    thanhtien.innerText = tong.toLocaleString('de-DE') + 'đ';
}
