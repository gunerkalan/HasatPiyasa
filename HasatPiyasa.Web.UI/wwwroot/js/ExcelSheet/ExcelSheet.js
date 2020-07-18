$(document).ready(() => {
    CalculateColumn("tuik", "tuikTotal", "topla");
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
        var number1 = Number( $("#d_1_" + rowId).val())
        var number2 = Number($("#y_1_" + rowId).val())
        var numberAvarage = (number1 + number2) / 2
        $("#o_1_" + rowId).val(numberAvarage)
        CalculateColumn("dfiyat", "dfiyatTotal", "topla")
        CalculateColumn("yfiyat", "yfiyatTotal", "topla")
        CalculateColumn("ofiyat", "ofiyatTotal", "topla")
    }
    else {
        var number1 =Number( $("#d_2_" + rowId).val())
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
}