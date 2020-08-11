var GlobalEmteaId;

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
function SaveEmteaType() {


    swal({
        title: "Emtea Tipi Kaydet",
        text: "Emtea Tipi Kaydedilsin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emteaTypes = {
            EmteaGroupId: $("#drpemtiagroups :selected").val(),
            EmteaTypeName: $("#emteatypename").val(),

        }

        if (CheckValidateFormEmteaType()) {
            $.post("/Admin/CreateEmteaType", { emteaTypes: emteaTypes }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    SweetAlertMesaj("Emtia Tipi Kaydet", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#emteatype-adding-modal").modal("hide")

                    $('#drpemtias').val('')
                    $('#drpemtiagroups').val('')
                    $('#emteatypename').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Tipi Kaydet", model.messages, "error", "Kapat", "btn-danger")

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
function SaveEmteaTypeGroup() {


    swal({
        title: "Emtea Tip Gurup Kaydet",
        text: "Emtea Tip Gurup Kaydedilsin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emteaTypeGroups = {
            EmteaTypeId: $("#drpemtiatypes :selected").val(),
            EmteaTypeGroupName: $("#emteatypegroupname").val(),

        }

        if (CheckValidateFormEmteaTypeGroup()) {
            $.post("/Admin/CreateEmteaTypeGroups", { emteaTypeGroups: emteaTypeGroups }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    SweetAlertMesaj("Emtia Grup Tipi Kaydet", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#emteatypegroup-adding-modal").modal("hide")

                    $('#drpemtias').val('')
                    $('#drpemtiagroups').val('')
                    $('#drpemtiatypes').val('')
                    $('#emteatypegroupname').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Tip Grup Kaydet", model.messages, "error", "Kapat", "btn-danger")

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
function SaveTuikCity() {


    swal({
        title: "Tuik İl Verisi Kaydet",
        text: "Tuik İl Verisi Kaydedilsin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var citytuik = {
            SubeId: $("#drpsubes :selected").val(),
            CityId: $("#drpcities :selected").val(),
            EmteaTypeId: $("#drpemtiatypes :selected").val(),
            EmteaGroupId: $("#drpemtiagroups :selected").val(),
            TuikValue: $("#tuikvalue").val(),
            GuessValue: $("#guessvalue").val(),
            EmteaId: $("#drpemtias :selected").val(),
        }

        if (CheckValidateFormCityTuik()) {
            $.post("/Admin/CreateTuikCityData", { citytuik: citytuik }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    SweetAlertMesaj("İl Tuik Verisi Kaydet", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#tuikcity-adding-modal").modal("hide")

                    $('#drpemtias').val('')
                    $('#guessvalue').val('')
                    $('#tuikvalue').val('')
                    $('#drpemtiagroups').val('')
                    $('#drpemtiatypes').val('')
                    $('#drpsubes').val('')
                    $('#drpcities').val('')

                }
                else {

                    SweetAlertMesaj("İl Tüik Veri Kaydet", model.messages, "error", "Kapat", "btn-danger")

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
function CheckValidateForm2() {

    var EmteaCode = $("#emteacode2").val()
    var EmteaName = $("#emteaname2").val()

    if (EmteaCode != "" && EmteaName != "") {
        return true
    }
    else {
        ChangeColor(EmteaCode, "emteacode2")
        ChangeColor(EmteaName, "emteaname2")
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
function CheckValidateFormEmteaType() {

    var EmteaId = $("#drpemtias :selected").val()
    var EmteaGroupId = $("#drpemtiagroups :selected").val()
    var EmteaTypeName = $("#emteatypename").val()

    if (EmteaGroupId != "" && EmteaTypeName != "" && EmteaGroupId != "-1" && EmteaId != "" && EmteaId != "-1") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias")
        ChangeColor(EmteaGroupId, "drpemtiagroups")
        ChangeColor(EmteaTypeName, "emteatypename")
        return false

    }
}
function CheckValidateFormEmteaTypeGroup() {

    var EmteaId = $("#drpemtias :selected").val()
    var EmteaGroupId = $("#drpemtiagroups :selected").val()
    var EmteaTypeId = $("#drpemtiatypes :selected").val()
    var EmteaTypeGroupName = $("#emteatypegroupname").val()

    if (EmteaGroupId != "" && EmteaGroupId != "-1" && EmteaId != "" && EmteaId != "-1" && EmteaTypeId != "" && EmteaTypeId != "-1" && EmteaTypeGroupName != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias")
        ChangeColor(EmteaGroupId, "drpemtiagroups")
        ChangeColor(EmteaTypeId, "drpemtiatypes")
        ChangeColor(EmteaTypeGroupName, "emteatypegroupname")
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

    if (EmteaId != "-1" && EmteaGroupId != "-1" && EmteaTypeId != "-1" && SubeId != "-1" && TuikValue != "" && GuessValue != "") {
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
function CheckValidateFormCityTuik() {

    var EmteaId = $("#drpemtias :selected").val()
    var EmteaGroupId = $("#drpemtiagroups :selected").val()
    var EmteaTypeId = $("#drpemtiatypes :selected").val()
    var SubeId = $("#drpsubes :selected").val()
    var CityId = $("#drpcities :selected").val()
    var TuikValue = $("#tuikvalue").val()
    var GuessValue = $("#guessvalue").val()

    if (EmteaId != "-1" && EmteaGroupId != "-1" && EmteaTypeId != "-1" && SubeId != "-1" && TuikValue != "" && GuessValue != "" && CityId != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias")
        ChangeColor(EmteaGroupId, "drpemtiagroups")
        ChangeColor(EmteaTypeId, "drpemtiatypes")
        ChangeColor(SubeId, "drpsubes")
        ChangeColor(CityId, "drpcities")
        ChangeColor(TuikValue, "tuikvalue")
        ChangeColor(GuessValue, "guessvalue")
        return false

    }
}
function ChangeColor(value, v) {

    if (value == "0" || value == "" || value == "-1") {
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
function SubeChange() {
    $("#drpcities").empty()
    var id = $("#drpsubes :selected").val()

    $.post("/Admin/ChooseSubeCity", { subeid: id }, (res) => {
        var model = JSON.parse(JSON.stringify(res))
        $.each(model, (i, item) => {

            $("#drpcities").append(`<option value="${item.id}">${item.CitiName}</option>`)
        });


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
function EditEmtea(id) {
    GlobalEmteaId = id

    $.post("/Admin/GetEmtea", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#emteacode2").val(model.Veri.EmteaCode)
            $("#emteaname2").val(model.Veri.EmteaName)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.EmteaCode}  kodlu Emtea'yı Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")

        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function UpdateEmtea() {


    swal({
        title: "Emtea Güncelle",
        text: "Emtea Güncellensin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emtea = {
            Id: GlobalEmteaId,
            EmteaCode: $("#emteacode2").val(),
            EmteaName: $("#emteaname2").val(),

        }

        if (CheckValidateForm2()) {
            $.post("/Admin/UpdateEmtea", { emtea: emtea }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Emtea Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#emteacode2').val('')
                    $('#emteaname2').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Güncelle", model.messages, "error", "Kapat", "btn-danger")

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
function DeleteEmtea(id, EmteaCode) {

    swal({
        title: "Sil ?",
        text: `${EmteaCode} kodlu Emtea Silinsin mi ?`,
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Vazgeç"

    }, 
        $.post("/Admin/DeleteEmtea", { id: id, }, (res) => {

            var model = JSON.parse(res)

            if (model.BasariliMi) {
                SweetAlertMesaj("Silme", "Emtea Silinmiştir !", "success", "Kapat", "btn-success")
                $("#GridContainer").dxDataGrid("instance").refresh();
                this.showLoaderOnConfirm = false
            }
            else {
                swal("Hata !", model.ErrorMessage, "error");
                this.showLoaderOnConfirm = false
            }
        })
    );
}


//$("body").on("hide.bs.modal", "#emtea-adding-modal", () => {
//    $("#GridContainer").dxDataGrid("instance").refresh();
//})

