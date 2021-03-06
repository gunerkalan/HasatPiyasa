﻿var Dates = [];
var Cities = [];
var AllDate = false;
var AllCities = false;

//LoadTable();
//LoadTableCity();

setTimeout(() => {
    LoadProcess()
}, 1000)

$(document).ready(() => {
    LoadProcess()

})

$("body").on("load", () => {
    LoadProcess()
})

function LoadProcess() {
    CalculateColumn("tuik", "tuikTotal", "topla");
    CalculateColumn("tmo", "tmoTotal", "topla");
    //CalculateColumn("percent", "percentTotal", "ortalama");
    CalculateColumn("hasatedilen", "hasatedilenTotal", "topla");
    CalculateColumn("bekleyenton", "bekleyentonTotal", "topla");
    CalculateColumn("piyasaton", "piyasatonTotal", "topla");
    CalculateColumnByCumulative("percent", "tmo", "percentTotal", "ağırlıklı ondalıklı");
    CalculateColumnByCumulative('toptan0', 'piyasaton', 'toptan0', 'ağırlıklı');
    CalculateColumnByCumulative('toptan1', 'piyasaton', 'toptan1', 'ağırlıklı');
    CalculateColumnByCumulative('toptan2', 'piyasaton', 'toptan2', 'ağırlıklı');
    CalculateColumnByCumulative('toptan3', 'piyasaton', 'toptan3', 'ağırlıklı');
    CalculateColumnByCumulative('toptan4', 'piyasaton', 'toptan4', 'ağırlıklı');
    CalculateColumn('naturel0', 'toplanaturel_0', 'topla');
    CalculateColumn('naturel1', 'toplanaturel_1', 'topla');
    CalculateColumn('naturel2', 'toplanaturel_2', 'topla');
    CalculateColumn('naturel3', 'toplanaturel_3', 'topla');
    CalculateColumn('naturel4', 'toplanaturel_4', 'topla');
    CalculateColumn('dfiyat', 'dfiyatTotal', 'ortalama');
    CalculateColumn('yfiyat', 'yfiyatTotal', 'ortalama');
    CalculateColumn('ofiyat', 'ofiyatTotal', 'ortalama');
    CalculateColumn('dfiyat2', 'dfiyat2Total', 'ortalama ondalıklı');
    CalculateColumn('yfiyat2', 'yfiyat2Total', 'ortalama ondalıklı');
    CalculateColumn('ofiyat2', 'ofiyat2Total', 'ortalama ondalıklı');
    $("input[type=number]").on("focus", function () {
        $(this).on("keydown", function (event) {
            if (event.keyCode === 38 || event.keyCode === 40) {
                event.preventDefault();
            }
        });
    });

    $("table input").trigger('keyup')
    $("table input").trigger('change')
    $("input").focus((e) => {

        if (e.target.value == "0")
            e.target.value = 0;

    })

    $("table input").each(function () {
        var element = $(this);
        if (element[0].value.toString()== "NaN") {
            element[0].value = 0;
        }
    });

    //console.clear()
}
function LoadMarketProcess() {
    
   

    //$("table input").trigger('keyup')
    //$("table input").trigger('change')
    //$("input").focus((e) => {

    //    if (e.target.value == "0")
    //        e.target.value = null;

    //})
    setTimeout(() => {
    $("table td").each(function () {
        var element = $(this);
        if (element[0].textContent == "NaN") {
            element[0].textContent = "";
        }
        if (element[0].textContent.indexOf(',') !== -1) {
            element[0].textContent = parseInt(element[0].textContent);
        }

    })
    }, 1000)


    //console.clear()
}
function CalculateToptan() {

    CalculateColumnByCumulative("percent", "tmo", "percentTotal", "ağırlıklı ondalıklı");
    CalculateColumnByCumulative('toptan0', 'piyasaton', 'toptan0', 'ağırlıklı');
    CalculateColumnByCumulative('toptan1', 'piyasaton', 'toptan1', 'ağırlıklı');
    CalculateColumnByCumulative('toptan2', 'piyasaton', 'toptan2', 'ağırlıklı');
    CalculateColumnByCumulative('toptan3', 'piyasaton', 'toptan3', 'ağırlıklı');
    CalculateColumnByCumulative('toptan4', 'piyasaton', 'toptan4', 'ağırlıklı');
}

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
            if (v.value != "0" & v.value != "") {
                total += parseInt(v.value.replace(/\./g, ''))
            }
        })
      
        $("#" + totalid).val(total.toString().replace(/\./g, ','))
    }

    if (calculateType == "ortalama") {
        var tuiks = $(`input[name=${name}]`)
        var total = 0;
        var _count = 0
        $.each(tuiks, (i, v) => {
            if (v.value != "0" & v.value != "") {
                total += parseInt(v.value.replace(/\./g, ''))
                _count++
            }
        })

        $("#" + totalid).val(parseInt(total / _count).toString().replace(/\./g, ','))

    }
    if (calculateType == "ortalama ondalıklı") {
        var tuiks = $(`input[name=${name}]`)
        var total = 0;
        var _count = 0
        $.each(tuiks, (i, v) => {
            if (v.value != "0" & v.value != "") {
                total += parseFloat(v.value.replace(/\./g, ''))
                _count++
            }
        })
        if ((total / _count).toString().indexOf('.') !== -1) {
            $("#" + totalid).val((total / _count).toFixed(2).replace(/\./g, ',').toString())
        }
        else {
            $("#" + totalid).val(parseInt(total / _count).toString().replace(/\./g, ','))
        }

    }


}
function CalculateColumnByCumulative(name, relatedcolumnname, totalid, calculateType) {

    if (calculateType == "ağırlıklı") {
        var tuiks = $(`input[name=${name}]`);
        var miktars = $(`input[name=${relatedcolumnname}]`);
        var miktartotal = 0;
        var total = 0;
        $.each(miktars, (i, v) => {
            if (v.value != "0" & v.value != "") {
                miktartotal += parseInt(v.value.replace(/\./g, ''))
            }
        })
        $.each(miktars, (a, b) => {
            $.each(tuiks, (i, v) => {
                if ((v.value != "0" & v.value != "") & (b.value != "0" & b.value != "") & a == i) {
                    total += parseInt(v.value.replace(/\./g, '')) * parseInt(b.value.replace(/\./g, ''))
                }
            })
        })
       
            $("#" + totalid).val((total / miktartotal).toString().replace(/\./g, ','))        }
        

    
    if (calculateType == "ağırlıklı ondalıklı") {
        var tuiks = $(`input[name=${name}]`);
        var miktars = $(`input[name=${relatedcolumnname}]`);
        var miktartotal = 0;
        var total = 0;
        $.each(miktars, (i, v) => {
            if (v.value != "0" & v.value != "") {
                miktartotal += parseFloat(v.value.replace(/\./g, ''))
            }
        })
        $.each(miktars, (a, b) => {
            $.each(tuiks, (i, v) => {
                if ((v.value != "0" & v.value != "") & (b.value != "0" & b.value != "") & a == i) {
                    total += parseFloat(v.value.replace(/\./g, '')) * parseFloat(b.value.replace(/\./g, ''))
                }
            })
        })
        if ((total / miktartotal).toString().indexOf('.') !== -1) {
            $("#" + totalid).val((total / miktartotal).toFixed(2).toString().replace(/\./g, ','))
        }
        else {
            $("#" + totalid).val(parseInt(total / miktartotal).toString())
        }


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

    CalculateColumn('piyasaton', 'piyasatonTotal', 'topla')

}
function Avarage(e) {

    var rowId = e.attributes["id"].value.split("_")[2]
    var AvgItemsId = e.attributes["id"].value.split("_")[1]

    if (AvgItemsId == "1") {
        var number1 = Number($("#d_1_" + rowId).val())
        var number2 = Number($("#y_1_" + rowId).val())
        var numberAvarage = (number1 + number2) / 2
        $("#o_1_" + rowId).val(numberAvarage)
        CalculateColumn("dfiyat", "dfiyatTotal", "ortalama")
        CalculateColumn("yfiyat", "yfiyatTotal", "ortalama")
        CalculateColumn("ofiyat", "ofiyatTotal", "ortalama")
    }
    else {
        var number1 = Number($("#d_2_" + rowId).val())
        var number2 = Number($("#y_2_" + rowId).val())
        var numberAvarage = (number1 + number2) / 2
        $("#o_2_" + rowId).val(numberAvarage)
        CalculateColumn("dfiyat2", "dfiyat2Total", "ortalama")
        CalculateColumn("yfiyat2", "yfiyat2Total", "ortalama")
        CalculateColumn("ofiyat2", "ofiyat2Total", "ortalama")
    }


}
function CityChange() {
    var value = $("#cityId :selected").val()
    window.location.href = `/DataInput/DataInputRice?cityId=${value}`;
    getLoadPanelInstance().show();
    LoadProcess();

}

function getLoadPanelInstance() {
    return $("#loadPanel").dxLoadPanel("instance");
}

function Save() {
    var AddInput = [];
    var dataInput = [];
    var error = 0;
    var inputs = Number($("input").length) / 22

    for (var i = 1; i < inputs; i++) {
        var item = {

            CityId: "",
            EmteaTypeId: "",
            EmteaGroupId: "",
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
            Perakende6: "",
            id: "",
        }


        var keys = Object.keys(item);
        var input = $(`.datainput${i} input`)
        item.CityId = $("#cityId :selected").val()
        item.EmteaTypeId = $(`.datainput${i}`).attr("emteatype")
        item.EmteaGroupId = $(`.datainput${i}`).attr("emteagroup")
        item.id = $(`.datainput${i}`).attr("dataInputId")
        $.each(input, (n, v) => {

            item[keys[n + 3]] = v.value

        })

        if (i != inputs - 1)
            AddInput.push(item)
    }

    $.each(AddInput, (i, v) => {

        if (v.GuessValue > 0) {
            if (v.HasatOran == null ||
                v.HasatMiktar == null ||
                v.UreticiKalanMiktar == null ||
                v.Natural1 == null ||
                v.Natural2 == null ||
                v.Natural3 == null ||
                v.Natural4 == null ||
                v.Natural5 == null ||
                v.NaturalToplam == null ||
                v.ToptanPiyasa1 == null ||
                v.ToptanPiyasa2 == null ||
                v.ToptanPiyasa3 == null ||
                v.ToptanPiyasa4 == null ||
                v.ToptanPiyasa5 == null ||
                v.Perakende1 == null ||
                v.Perakende2 == null ||
                v.Perakende3 == null ||
                v.Perakende4 == null ||
                v.Perakende5 == null ||
                v.Perakende6 == null
                   )
    {
        ++error
    }
}

if (error == 0) {
    if (v.HasatMiktar != "" && v.HasatMiktar != "0") {
        dataInput.push(v)
    }

    if (v.HasatMiktar == "0" && v.Perakende1 > 0 && v.Perakende2 > 0 && v.Perakende3 > 0 && v.Perakende4 > 0 && v.Perakende5 > 0 && v.Perakende6 > 0) {
        dataInput.push(v)
    }
}


    })

if (error == 0) {
    $.post("/DataInput/DataInputRice", { dataInputs: dataInput }, (res) => {

        var model = JSON.parse(JSON.stringify(res))
        if (model.success) {
            SweetAlertMesaj("Hasat Piyasa  Kaydet", model.messages, "success", "Kapat", "btn-success")
            setTimeout(() => {

                window.location.href = "/"


            }, 2000)
        }
        else {
            SweetAlertMesaj("Hasat Piyasa  Kaydet", model.messages, "error", "Kapat", "btn-danger")
        }

    })
}
else {
    SweetAlertMesaj("Hasat Piyasa  Kaydet", "Tüik verisi gelen alanları lütfen doldurunuz !", "error", "Kapat", "btn-danger")
    dataInput = [];
    AddInput = [];

}
    


}

function GetTodayDataInput() {

    $.get("/DataInput/GetTodayDataInput", (res) => {

        var model = JSON.parse(res)

    })
}




function LoadTable(pathh) {

    Dates = $('#dates').select2('val')
    Cities = $('#cities').select2('val')
    $('.rapor').css("border", "none")
    AllCities = document.getElementById("allcities").checked
    AllDate = document.getElementById("alldate").checked
    getLoadPanelInstance().show()
    $.post("/report/" + pathh, { dates: Dates, cities: Cities, allDate: AllDate, allcities: AllCities }, (res) => {
        $(".rapor").html(res)
        LoadProcess()
        LoadProcess2();
        getLoadPanelInstance().hide()
    })
}

function LoadMarketTable(pathh) {

    Dates = $('#dates').select2('val')
    Emteatypes = $('#emteatypes').select2('val')
    $('.rapor').css("border", "none")
    getLoadPanelInstance().show()
    $.post("/report/" + pathh, { dates: Dates, emteatypes: Emteatypes }, (res) => {
        $(".rapor").html(res)
        getLoadPanelInstance().hide()
    })
}


function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

function exportTableToExcel(tableID, filename = '') {

    $('#table').tableExport({ type: 'xls', fileName: filename, bootstrap: true });
    $.fn.tableExport.defaultButton = "button-default";



}
function exportFirstTableToExcel(tableID, filename = '') {

    $("table:first-child").tableExport({ type: 'xls', fileName: filename, bootstrap: true });
    $.fn.tableExport.defaultButton = "button-default";



}
