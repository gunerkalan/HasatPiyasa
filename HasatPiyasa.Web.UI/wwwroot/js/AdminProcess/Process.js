$(function () {
    $("#GridContainer").dxDataGrid({
        export: {
            enabled: true
        }
    });

})




function SaveEmtea() {


    swal({
        title: "Emtea Kaydet",
        text: "Emtea Kaydedilsin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emtea = {
            EmteaCode: $("#emteacode").val(),
            EmteaName: $("#emteaname").val(),

        }

        if (CheckValidateForm()) {
            $.post("/Admin/CreateEmtea", { emtea: emtea }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    SweetAlertMesaj("Emtia Kaydet", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#emtea-adding-modal").modal("hide")

                    $('#emteacode').val('')
                    $('#emteaname').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Kaydet", model.messages, "error", "Kapat", "btn-danger")

                }

            })
        }
        else {
            swal("Hata : Lütfen gerekli alanları doldurunuz !");
            this.showLoaderOnConfirm = false
            return false

        }


    });


}
function SaveEmteaGroup() {


    swal({
        title: "Emtea Grup Kaydet",
        text: "Emtea Grup Kaydedilsin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emteaGroups = {
            EmteaId: $("#drpemtias :selected").val(),
            GroupName: $("#emteagroupname").val(),

        }

        if (CheckValidateFormEg()) {
            $.post("/Admin/CreateEmteaGroup", { emteaGroups: emteaGroups }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    SweetAlertMesaj("Emtia Grup Kaydet", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#emteagroup-adding-modal").modal("hide")

                    $('#drpemtias').val('')
                    $('#emteagroupname').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Grup Kaydet", model.messages, "error", "Kapat", "btn-danger")

                }

            })
        }
        else {
            swal("Hata : Lütfen gerekli alanları doldurunuz !");
            this.showLoaderOnConfirm = false
            return false

        }


    });


}
function SaveTuikSube() {


    swal({
        title: "Tuik Şube Verisi Kaydet",
        text: "Tuik Şube Verisi Kaydedilsin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var subetuik = {
            SubeId: $("#drpsubes :selected").val(),
            EmteaTypeId: $("#drpemtiatypes :selected").val(),
            EmteaGroupId: $("#drpemtiagroups :selected").val(),
            TuikValue: $("#tuikvalue").val(),
            GuessValue: $("#guessvalue").val(),
            EmteaId: $("#drpemtias :selected").val(),
        }

            if (CheckValidateFormSubeTuik()) {
                $.post("/Admin/CreateTuikSubeData", { subetuik: subetuik }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    SweetAlertMesaj("Şube Tuik Verisi Kaydet", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#tuiksube-adding-modal").modal("hide")

                    $('#drpemtias').val('')
                    $('#guessvalue').val('')
                    $('#tuikvalue').val('')
                    $('#drpemtiagroups').val('')
                    $('#drpemtiatypes').val('')
                    $('#drpsubes').val('')

                }
                else {

                    SweetAlertMesaj("Şube Tüik Veri Kaydet", model.messages, "error", "Kapat", "btn-danger")

                }

            })
        }
        else {
            swal("Hata : Lütfen gerekli alanları doldurunuz !");
            this.showLoaderOnConfirm = false
            return false

        }


    });


}
function CheckValidateForm() {

    var EmteaCode = $("#emteacode").val()
    var EmteaName = $("#emteaname").val()

    if (EmteaCode != "" && EmteaName != "") {
        return true
    }
    else {
        ChangeColor(EmteaCode, "emteacode")
        ChangeColor(EmteaName, "emteaname")
        return false

    }
}
function CheckValidateFormEg() {

    var EmteaId = $("#drpemtias :selected").val()
    var GroupName = $("#emteagroupname").val()

    if (EmteaId != "" && GroupName != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias")
        ChangeColor(GroupName, "emteagroupname")
        return false

    }
}
function CheckValidateFormSubeTuik() {

    var EmteaId = $("#drpemtias :selected").val()
    var EmteaGroupId = $("#drpemtiagroups :selected").val()
    var EmteaTypeId = $("#drpemtiatypes :selected").val()
    var SubeId = $("#drpsubes :selected").val()
    var TuikValue = $("#tuikvalue").val()
    var GuessValue = $("#guessvalue").val()

    if (EmteaId != "-1" && EmteaGroupId != "-1" && EmteaTypeId != "-1" && SubeId != "-1" && TuikValue != "" && GuessValue !="") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias")
        ChangeColor(EmteaGroupId, "drpemtiagroups")
        ChangeColor(EmteaTypeId, "drpemtiatypes")
        ChangeColor(SubeId, "drpsubes")
        ChangeColor(TuikValue, "tuikvalue")
        ChangeColor(GuessValue, "guessvalue")
        return false

    }
}
function ChangeColor(value, v) {

    if (value == "0" || value == "" || value =="-1") {
        $("#" + v).css("border", "1px solid red")
    }
    else {
        $("#" + v).css("border", "0px")
    }

}

function SweetAlertMesaj(title, text, type, buttontext, btncss) {
    swal({
        title: title,
        text: text,
        type: type,
        confirmButtonClass: btncss,
        confirmButtonText: buttontext,
        closeOnConfirm: false
    })
}

var emteagroupid = 0;
var emteaid = 0;
var selectedemteagroupid = 0;
var count = 0;

function EmteaChange() {
    $("#drpemtiagroups").empty()
    var id = $("#drpemtias :selected").val()

    $.post("/Admin/ChooseEmteaGrup", { emteaid: id }, (res) => {
        var model = JSON.parse(JSON.stringify(res))
        $.each(model, (i, item) => {
            if (count == 1)
                selectedemteagroupid = item.id
            $("#drpemtiagroups").append(`<option value="${item.id}">${item.EmteaGrupName}</option>`)
        });

        EmteaGroupChange()

    })

}
function EmteaGroupChange() {
    var id = $("#drpemtiagroups :selected").val()
    $.post("/Admin/ChooseEmteaType", { emteagroupid: id }, (res) => {
        $("#drpemtiatypes").empty();
        model = JSON.parse(JSON.stringify(res))

        $.each(model, (i, item) => {
            $("#drpemtiatypes").append(`<option value="${item.id}">${item.EmteaTpeName}</option>`)

        });

    })

}

//$("body").on("hide.bs.modal", "#emtea-adding-modal", () => {
//    $("#GridContainer").dxDataGrid("instance").refresh();
//})

