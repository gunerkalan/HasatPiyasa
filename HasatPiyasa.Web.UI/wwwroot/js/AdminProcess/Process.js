﻿$(function () {
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
    var GroupName= $("#emteagroupname").val()

    if (EmteaId != "" && GroupName != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias")
        ChangeColor(GroupName, "emteagroupname")
        return false

    }
}
function ChangeColor(value, v) {

    if (value == "0" || value == "") {
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

//$("body").on("hide.bs.modal", "#emtea-adding-modal", () => {
//    $("#GridContainer").dxDataGrid("instance").refresh();
//})

