$(document).ready(() => {
    CalculateColumn("tuik", "tuikTotal", "topla");
    CalculateColumn("tmo", "tmoTotal", "topla");
    console.clear()
    $("input[type=number]").on("focus", function () {
        $(this).on("keydown", function (event) {
            if (event.keyCode === 38 || event.keyCode === 40) {
                event.preventDefault();
            }
        });
    });
     
})
function YuzdeHesapla(e) {
    var value = Number(e.value);
    var percent = 0;
    var rowname = e.attributes["id"].value.split("_")[0]

    if (rowname != "Tmo") {

        if (value > 100)
            e.value = 100
        if (value < 0)
            e.value = 0
    }


    var rowId = e.attributes["id"].value.split("_")[1]
    var _amount = Number($("#Tmo_" + rowId).val())
    var _percentValue = Number($("#HasatYuzde_" + rowId).val())

    if (_amount > 0 && value > 0) {

        if (rowname == "Tmo") {
            percent = (_amount * _percentValue) / 100;
        }
        else {
            percent = (_amount * value) / 100;
        }

        $("#HasatEdilen_" + rowId).val(percent)
        CalculateColumn("tmo", "tmoTotal", "topla");
        CalculateColumn("percent", "percentTotal", "ortalama");
        CalculateColumn("hasatedilen", "hasatedilenTotal", "topla");
    }


}
function CalculateColumn(name, totalid, calculateType) {

    if (calculateType == "topla") {
        var tuiks = $(`input[name=${name}]`)
        var total = 0;
        $.each(tuiks, (i, v) => {
            if (v.value != "0") {
                total += Number(v.value)
            }
        })

        $("#" + totalid).val(total)
    }

    if (calculateType == "ortalama") {
        var tuiks = $(`input[name=${name}]`)
        var total = 0;
        var _count = 0
        $.each(tuiks, (i, v) => {
            if (v.value != "0") {
                total += Number(v.value)
                _count++
            }
        })
        $("#" + totalid).val(total / _count)

    }


}
function CalculateNaturel(totalid) {
    var rowId = totalid.attributes["id"].value.split("_")[1]
    var index = totalid.dataset.bind;
    totalid = "piyasaton" + rowId
    var tuiks = $(`input[name=naturel${rowId}]`)
    var total = 0;
    $.each(tuiks, (i, v) => {
        if (v.value != "0") {
            total += Number(v.value)
        }
    })

    var columInputs = $(`input[data-bind=${index}]`)
    var columntotal = 0
    $.each(columInputs, (i, v) => {
        if (v.value != "0") {
            columntotal += Number(v.value)
        }
    })

    $("#" + totalid).val(total)
    $("#toplanaturel_" + index).val(columntotal)


}
function Avarage(e) {

    var rowId = e.attributes["id"].value.split("_")[2]
    var AvgItemsId = e.attributes["id"].value.split("_")[1]

    if (AvgItemsId == "1") {
        var number1 = Number($("#d_1_" + rowId).val())
        var number2 = Number($("#y_1_" + rowId).val())
        var numberAvarage = (number1 + number2) / 2
        $("#o_1_" + rowId).val(numberAvarage)
        CalculateColumn("dfiyat", "dfiyatTotal", "topla")
        CalculateColumn("yfiyat", "yfiyatTotal", "topla")
        CalculateColumn("ofiyat", "ofiyatTotal", "topla")
    }
    else {
        var number1 = Number($("#d_2_" + rowId).val())
        var number2 = Number($("#y_2_" + rowId).val())
        var numberAvarage = (number1 + number2) / 2
        $("#o_2_" + rowId).val(numberAvarage)
        CalculateColumn("dfiyat2", "dfiyat2Total", "topla")
        CalculateColumn("yfiyat2", "yfiyat2Total", "topla")
        CalculateColumn("ofiyat2", "ofiyat2Total", "topla")
    }


}
function CityChange() {
    var value = $("#cityId :selected").val()
    window.location.href = `/DataInput/DataInputRice?cityId=${value}`;
    getLoadPanelInstance().show();

}

function getLoadPanelInstance() {
    return $("#loadPanel").dxLoadPanel("instance");
}

function Save() {
    var AddInput = [];
    var dataInput= [];

    var inputs = Number($("input").length) / 22

    for (var i = 1; i < inputs; i++) {
        var item = {

            CityId: "",
            EmteaTypeId: "",
            EmteaGroupId:"",
            TuikValue: "",
            GuessValue: "",
            HasatOran: "",
            HasatMiktar: "",
            UreticiKalanMiktar: "",
            Natural1: "",
            Natural2: "",
            Natural3: "",
            Natural4: "",
            Natural5: "",
            NaturalToplam: "",
            ToptanPiyasa1: "",
            ToptanPiyasa2: "",
            ToptanPiyasa3: "",
            ToptanPiyasa4: "",
            ToptanPiyasa5: "",
            Perakende1: "",
            Perakende2: "",
            Perakende3: "",
            Perakende4: "",
            Perakende5: "",
            Perakende6: ""

        }
      

        var keys = Object.keys(item);
        var input = $(`.datainput${i} input`)       
        item.CityId = $("#cityId :selected").val()
        item.EmteaTypeId = $(`.datainput${i}`).attr("emteatype")
        item.EmteaGroupId = $(`.datainput${i}`).attr("emteagroup")
        $.each(input, (n, v) => {

            item[keys[n + 3]] = v.value

        })

        if (i != inputs-1)
        AddInput.push(item)
    }

    $.each(AddInput, (i, v) => {

        if (v.HasatMiktar != "" && v.HasatMiktar != "0") {
            dataInput.push(v)
        }

    })

    $.post("/DataInput/DataInputRice", { dataInputs: dataInput }, (res) => {

        var model = JSON.parse(JSON.stringify(res))
        if (model.success) {
            SweetAlertMesaj("Hasat Piyasa  Kaydet", "Kaydedildi.", "success", "Kapat", "btn-success")
        }
        else {
            SweetAlertMesaj("Hasat Piyasa  Kaydet", "Hata Oluştu. !", "error", "Kapat", "btn-danger")
        }

    })
    

}

 