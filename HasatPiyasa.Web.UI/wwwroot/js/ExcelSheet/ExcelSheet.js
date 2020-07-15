$(document).ready(() => {
    CalculateColumn("tuik", "tuikTotal","topla");
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

    if (_amount > 0 && value >0) {

        if (rowname == "Tmo") {
            percent = (_amount * _percentValue) / 100;
        }
        else {
            percent = (_amount * value) / 100;
        }
        
        $("#HasatEdilen_" + rowId).val(percent)
        CalculateColumn("tmo", "tmoTotal","topla");
        CalculateColumn("percent", "percentTotal","ortalama");
        CalculateColumn("hasatedilen", "hasatedilenTotal","topla");
    }
    
}
function CalculateColumn(name, totalid, calculateType) {

    if (name == "naturel") {
        totalid = "piyasaton" + totalid.attributes["id"].value.split("_")[1]
    }

        


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
        var _count=0
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
    totalid = "piyasaton" + rowId
      
        var tuiks = $(`input[name=naturel${rowId}]`)
        var total = 0;
        $.each(tuiks, (i, v) => {
            if (v.value != "0") {
                total += Number(v.value)
            }
        })

        $("#" + totalid).val(total)
     
     
}