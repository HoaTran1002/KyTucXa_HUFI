var tinh = document.getElementById("tinh");
var vtinh = vquan = vxa = null;
if (tinh != null) {
    var quan = document.getElementById("quan");
    var xa = document.getElementById("xa");
    vtinh = tinh.value;
    vquan = quan.value;
    vxa = xa.value;
}
const host = "https://provinces.open-api.vn/api/";
var callAPI = (api) => {
    return axios.get(api)
        .then((response) => {
            renderDataShow(response.data, "province", vtinh);
            callApiDistrict(host + "p/" + $("#province").val() + "?depth=2");
        });
}
var callApiDistrict = (api) => {
    return axios.get(api)
        .then((response) => {
            renderDataShow(response.data.districts, "district", vquan);
            callApiWard(host + "d/" + $("#district").val() + "?depth=2");
        });
}
var callApiWard = (api) => {
    return axios.get(api)
        .then((response) => {
            renderDataShow(response.data.wards, "ward", vxa);
            vtinh = vquan = vxa = null;
        });
}

var renderDataShow = (array, select, value = undefined) => {
    let row = '<option value=""> Chọn </option>';
    if (array != null)
        array.forEach(element => {
            if (element.name == value)
                row += `<option selected value="${element.code}">${element.name}</option>`
            else
                row += `<option value="${element.code}">${element.name}</option>`
        });
    document.querySelector("#" + select).innerHTML = row
}
callAPI('https://provinces.open-api.vn/api/?depth=1');
$("#province").change(() => {
    callApiDistrict(host + "p/" + $("#province").val() + "?depth=2");
    result.value = "";
});
$("#district").change(() => {
    callApiWard(host + "d/" + $("#district").val() + "?depth=2");
    result.value = "";
});
$("#ward").change(() => {
    printResult();
})
var printResult = () => {
    if ($("#ward").val() != "") {
        let s = $("#ward option:selected").text() + ", "
            + $("#district option:selected").text() + ", "
            + $("#province option:selected").text();
        result.value = s;
    }
}
