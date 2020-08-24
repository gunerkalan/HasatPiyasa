var GlobalEmteaId;
var GlobalEmteaGroupId;
var GlobalEmteaTypeId;
var GlobalEmteaTypeGroupId;
var GlobalTuikSubeId;
var GlobalTuikUserId;
var GlobalSubeId;
var GlobalAddedTime;

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
function CheckValidateFormEg2() {

    var EmteaId = $("#drpemtias2 :selected").val()
    var GroupName = $("#emteagroupname2").val()

    if (EmteaId != "" && GroupName != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias2")
        ChangeColor(GroupName, "emteagroupname2")
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
function CheckValidateFormEmteaType2() {

    var EmteaId = $("#drpemtias2 :selected").val()
    var EmteaGroupId = $("#drpemtiagroups2 :selected").val()
    var EmteaTypeName = $("#emteatypename2").val()

    if (EmteaGroupId != "" && EmteaTypeName != "" && EmteaGroupId != "-1" && EmteaId != "" && EmteaId != "-1") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias2")
        ChangeColor(EmteaGroupId, "drpemtiagroups2")
        ChangeColor(EmteaTypeName, "emteatypename2")
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
function CheckValidateFormEmteaTypeGroup2() {

    var EmteaId = $("#drpemtias2 :selected").val()
    var EmteaGroupId = $("#drpemtiagroups2 :selected").val()
    var EmteaTypeId = $("#drpemtiatypes2 :selected").val()
    var EmteaTypeGroupName = $("#emteatypegroupname2").val()

    if (EmteaGroupId != "" && EmteaGroupId != "-1" && EmteaId != "" && EmteaId != "-1" && EmteaTypeId != "" && EmteaTypeId != "-1" && EmteaTypeGroupName != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias2")
        ChangeColor(EmteaGroupId, "drpemtiagroups2")
        ChangeColor(EmteaTypeId, "drpemtiatypes2")
        ChangeColor(EmteaTypeGroupName, "emteatypegroupname2")
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
function CheckValidateFormSubeTuik2() {

    var EmteaId = $("#drpemtias2 :selected").val()
    var EmteaGroupId = $("#drpemtiagroups2 :selected").val()
    var EmteaTypeId = $("#drpemtiatypes2 :selected").val()
    var SubeId = $("#drpsubes2 :selected").val()
    var TuikValue = $("#tuikvalue2").val()
    var GuessValue = $("#guessvalue2").val()

    if (EmteaId != "-1" && EmteaGroupId != "-1" && EmteaTypeId != "-1" && SubeId != "-1" && TuikValue != "" && GuessValue != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias2")
        ChangeColor(EmteaGroupId, "drpemtiagroups2")
        ChangeColor(EmteaTypeId, "drpemtiatypes2")
        ChangeColor(SubeId, "drpsubes2")
        ChangeColor(TuikValue, "tuikvalue2")
        ChangeColor(GuessValue, "guessvalue2")
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
function EmteaChange2(id) {
    $("#drpemtiagroups2").empty()


    $.post("/Admin/ChooseEmteaGrup", { emteaid: id }, (res) => {
        var model = JSON.parse(JSON.stringify(res))
        $.each(model, (i, item) => {
            if (count == 1)
                selectedemteagroupid = item.id
            $("#drpemtiagroups2").append(`<option value="${item.id}">${item.EmteaGrupName}</option>`)
        });

    })

   
}
function EmteaChange3() {
    $("#drpemtiagroups2").empty()
    var id = $("#drpemtias2 :selected").val()

    $.post("/Admin/ChooseEmteaGrup", { emteaid: id }, (res) => {
        var model = JSON.parse(JSON.stringify(res))
        $.each(model, (i, item) => {
            if (count == 1)
                selectedemteagroupid = item.id
            $("#drpemtiagroups2").append(`<option value="${item.id}">${item.EmteaGrupName}</option>`)
        });

        EmteaGroupChange3()

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
function EmteaGroupChange3() {
    var id = $("#drpemtiagroups2 :selected").val()
    $.post("/Admin/ChooseEmteaType", { emteagroupid: id }, (res) => {
        $("#drpemtiatypes2").empty();
        model = JSON.parse(JSON.stringify(res))

        $.each(model, (i, item) => {
            $("#drpemtiatypes2").append(`<option value="${item.id}">${item.EmteaTpeName}</option>`)

        });

    })

}
function EmteaGroupChange2(id) {

    $.post("/Admin/ChooseEmteaType", { emteagroupid: id }, (res) => {
        $("#drpemtiatypes").empty();
        model = JSON.parse(JSON.stringify(res))

        $.each(model, (i, item) => {
            $("#drpemtiatypes2").append(`<option value="${item.id}">${item.EmteaTpeName}</option>`)

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
function SoftDeleteEmtea(Id, EmteaCode, EmteaName) {
    GlobalEmteaId = Id
    swal({
        title: "Sil ?",
        text: `${EmteaCode} kodlu Emtea Silinsin mi ?`,
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",

    },
        function () {

            var emtea = {
                Id: GlobalEmteaId,
                IsActive: false,
                EmteaCode: EmteaCode,
                EmteaName: EmteaName

            }

            $.post("/Admin/DeleteEmtea", { emtea: emtea, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtea Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}
function EditEmteaGroup(id) {
    GlobalEmteaGroupId = id

    $.post("/Admin/GetEmteaGroup", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#drpemtias2").val(model.Veri.EmteaId)
            $("#emteagroupname2").val(model.Veri.GroupName)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.GroupName}  isimli Emtea Gurubu Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")

        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function UpdateEmteaGroup() {


    swal({
        title: "Emtea Gurup Güncelle",
        text: "Emtea Gurup Güncellensin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emteagroup = {
            Id: GlobalEmteaGroupId,
            EmteaId: $("#drpemtias2").val(),
            GroupName: $("#emteagroupname2").val(),

        }

        if (CheckValidateFormEg2()) {
            $.post("/Admin/UpdateEmteaGroup", { emteagroup: emteagroup }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Emtea Gurup Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#emteagroupname2').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Gurup Güncelle", model.messages, "error", "Kapat", "btn-danger")

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
function SoftDeleteEmteaGroup(Id, GroupName, EmteaId) {
    GlobalEmteaGroupId = Id,
        GlobalEmteaId = EmteaId
    swal({
        title: "Sil ?",
        text: `${GroupName} isimli Emtea Gurup Silinsin mi ?`,
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",

    },
        function () {

            var emteagroup = {
                Id: GlobalEmteaGroupId,
                IsActive: false,
                EmteaId: GlobalEmteaId,
                GroupName: GroupName

            }

            $.post("/Admin/DeleteEmteaGroup", { emteagroup: emteagroup, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtea Gurup Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}
function EditEmteaType(id, EmteaId) {
    GlobalEmteaTypeId = id
    EmteaChange2(EmteaId)
    $.post("/Admin/GetEmteaType", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#drpemtias2").val(model.Veri.EmteaId)

            $("#drpemtiagroups2").val(model.Veri.EmteaGroupId)
            $("#emteatypename2").val(model.Veri.EmteaTypeName)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.EmteaTypeName}  isimli Emtea Tipi Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")



        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function SoftDeleteEmteaType(Id, EmteaGroupId, EmteaTypeName) {
    GlobalEmteaGroupId = EmteaGroupId,
        GlobalEmteaTypeId = Id
    swal({
        title: "Sil ?",
        text: `${EmteaTypeName} isimli Emtea Tipi Silinsin mi ?`,
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",

    },
        function () {

            var emteatype = {
                Id: GlobalEmteaTypeId,
                IsActive: false,
                EmteaGroupId: GlobalEmteaGroupId,
                EmteaTypeName: EmteaTypeName

            }

            $.post("/Admin/DeleteEmteaType", { emteatype: emteatype, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtea Tipi Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}
function UpdateEmteaType() {


    swal({
        title: "Emtea Tipi Güncelle",
        text: "Emtea Tipi Güncellensin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emteatype = {
            Id: GlobalEmteaTypeId,
            EmteaTypeName: $("#emteatypename2").val(),
            EmteaGroupId: $("#drpemtiagroups2").val(),

        }

        if (CheckValidateFormEmteaType2()) {
            $.post("/Admin/UpdateEmteaType", { emteatype: emteatype }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Emtea Tipi Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#drpemtiagroups2').val('')
                    $('#emteatypename2').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Tipi Güncelle", model.messages, "error", "Kapat", "btn-danger")

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
function EditEmteaTypeGroup(id, EmteaId, EmteaGroupId) {
    GlobalEmteaTypeGroupId = id
    EmteaChange2(EmteaId)
    EmteaGroupChange2(EmteaGroupId)
    $.post("/Admin/GetEmteaGroupType", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#drpemtias2").val(model.Veri.EmteaId)
            $("#drpemtiagroups2").val(model.Veri.EmteaGroupId)
            $("#drpemtiatypes2").val(model.Veri.EmteaTypeId)


            $("#emteatypegroupname2").val(model.Veri.EmteaTypeGroupName)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.EmteaTypeGroupName}  isimli Emtea Tip Gurubu Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")



        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function UpdateEmteaTypeGroup() {


    swal({
        title: "Emtea Tip Gurubu Güncelle",
        text: "Emtea Gurubu Güncellensin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var emteatypegroup = {
            Id: GlobalEmteaTypeGroupId,
            EmteaTypeGroupName: $("#emteatypegroupname2").val(),
            EmteaTypeId: $("#drpemtiatypes2").val(),
        }

        if (CheckValidateFormEmteaTypeGroup2()) {
            $.post("/Admin/UpdateEmteaTypeGroup", { emteatypegroup: emteatypegroup }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Emtea Tip Gurubu Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#drpemtiagroups2').val('')
                    $('#drpemtiatypes2').val('')
                    $('#emteatypegroupname2').val('')

                }
                else {

                    SweetAlertMesaj("Emtia Tipi Güncelle", model.messages, "error", "Kapat", "btn-danger")

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
function SoftDeleteEmteaTypeGroup(Id, EmteaTypeId, EmteaTypeGroupName) {
    GlobalEmteaTypeId = EmteaTypeId,
        GlobalEmteaTypeGroupId = Id
    swal({
        title: "Sil ?",
        text: `${EmteaTypeGroupName} isimli Emtea Tip Gurubu Silinsin mi ?`,
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",

    },
        function () {

            var emteatypegroup = {
                Id: GlobalEmteaTypeGroupId,
                IsActive: false,
                EmteaTypeId: GlobalEmteaTypeId,
                EmteaTypeGroupName: EmteaTypeGroupName

            }

            $.post("/Admin/DeleteEmteaTypeGroup", { emteatypegroup: emteatypegroup, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtea Tip Gurubu Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}
function TuikDetail(id) {

    $.post("/Admin/GetDetail", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {
            var table = CreatDetailTable(model)
            $("#list").empty()
            $("#list").append(table)
            $("#orderid").html(`Emtea Tipi :${model.Veri.EmteaTypeName}`)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#detailModal").modal("show")
        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function CreatDetailTable(model) {
    var table = "";

        table = `<table class="table table-striped" id="detailTable">
                            <tbody>
                               <tr>
                                   
                                     <td colspan="2"><span class="detailname">Eklenme Tarihi</span> :<span class="detailvalue ">${model.Veri.AddedTime}</span> </td>
                                      <td><span class="detailname">Şube Adı</span> :</td><td><span class="detailvalue">${model.Veri.SubeName}</span> </td>                          
                                </tr>

                                <tr>
                                    <td><span class="detailname">Emtea Kodu</span> :</td><td><span class="detailvalue">${model.Veri.EmteaCode}</span> </td>
                                    <td><span class="detailname">Emtea Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaName}</span> </td>
                                                                       
                                </tr>
                                <tr>
                                    <td><span class="detailname">Emtea Gurup Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaGroupName}</span> </td>
                                    <td><span class="detailname">Emtea Tip Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaTypeName}</span> </td>
                                                                       
                                </tr>
                                <tr>
                                    <td><span class="detailname">Tüik Yılı</span> :</td><td><span class="detailvalue">${model.Veri.TuikYear}</span> </td>
                                     <td><span class="detailname">Tüik Verisi</span> :</td><td><span class="detailvalue">${model.Veri.TuikValue}</span> </td>
                                                                       
                                </tr>
                                 <tr>
                                    <td><span class="detailname">TMO Tahmin Yılı</span> :</td><td><span class="detailvalue">${model.Veri.GuessYear}</span> </td>
                                     <td><span class="detailname">TMO Tahmin Verisi</span> :</td><td><span class="detailvalue">${model.Veri.GuessValue}</span> </td>
                                                                       
                                </tr>
                                 <tr>
                                    <td><span class="detailname">Ek. Kullanıcı</span> :</td><td><span class="detailvalue">${model.Veri.AddedUser}</span> </td>
                                     <td><span class="detailname">Ek. Kullanıcı</span> :</td><td><span class="detailvalue">${model.Veri.AddSicil}</span> </td>
                                                                       
                                </tr>
                                  
                                 <tr>
                                    <td><span class="detailname">Güncellenme Tarihi</span> :</td><td><span class="detailvalue">${model.Veri.UpdatedTime == null ? '' : model.Veri.UpdatedTime}</span> </td>
                                    <td><span class="detailname">Güncel. Kullanıcı</span> :</td><td><span class="detailvalue">${model.Veri.UpdatedUser == null ? '' : model.Veri.UpdatedUser}</span> </td>
                                </tr>
                               
                              
                            </tbody>
                        </table>`
   
    

    return table;
}
function EditTuikSubeData(id, EmteaId, EmteaGroupId, SubeName, TuikYear, UserId, AddedTime) {
    GlobalTuikSubeId = id
    GlobalTuikUserId = UserId
    GlobalAddedTime = AddedTime
    EmteaChange2(EmteaId)
    EmteaGroupChange2(EmteaGroupId)
    $.post("/Admin/GetTuikSube", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#drpemtias2").val(model.Veri.EmteaId)
            $("#drpemtiagroups2").val(model.Veri.EmteaGroupId)
            $("#drpemtiatypes2").val(model.Veri.EmteaTypeId)
            $("#drpsubes2").val(model.Veri.SubeId)

            $("#tuikvalue2").val(model.Veri.TuikValue)
            $("#guessvalue2").val(model.Veri.GuessValue)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.SubeName + " " + model.Veri.TuikYear}  Tüik Verisini Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")



        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function UpdateTuikSubeData() {


    swal({
        title: "Tüik Verisi Güncelle",
        text: "Tüik Verisi Güncellensin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var tuik = {
            Id: GlobalTuikSubeId,
            AddUserId: GlobalTuikUserId,
            EmteaId: $("#drpemtias2").val(),
            EmteaGroupId: $("#drpemtiagroups2").val(),
            EmteaTypeId: $("#drpemtiatypes2").val(),
            SubeId: $("#drpsubes2").val(),
            TuikValue: $("#tuikvalue2").val(),
            GuessValue: $("#guessvalue2").val(),
            AddedTime: GlobalAddedTime
        }

            if (CheckValidateFormSubeTuik2()) {
                $.post("/Admin/UpdateTuikSube", { tuik: tuik }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Tük Verisini Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#drpemtiagroups2').val('')
                    $('#drpemtiatypes2').val('')
                    $('#drpsubes2').val('')
                    $('#tuikvalue2').val('')
                    $('#guessvalue2').val('')
                }
                else {

                    SweetAlertMesaj("Tüik Verisi Güncelle", model.messages, "error", "Kapat", "btn-danger")

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
function SoftDeleteTuikSubeData(id, EmteaId, EmteaGroupId, EmteaTypeId, SubeId, UserId) {
    GlobalEmteaTypeId = EmteaTypeId,
    GlobalEmteaGroupId = EmteaGroupId
    GlobalEmteaId = EmteaId
    GlobalTuikSubeId = id
    GlobalSubeId = SubeId
    GlobalTuikUserId = UserId
    swal({
        title: "Sil ?",
        text: `Tüik Verisi Silinsin mi ?`,
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",

    },
        function () {

            var tuik = {
                Id: GlobalTuikSubeId,
                IsActive: false,
                IsCity:false,
                EmteaTypeId: GlobalEmteaTypeId,
                EmteaId: GlobalEmteaId,
                EmteaGroupId: GlobalEmteaGroupId,
                AddUserId: SubeId,
                SubeId: SubeId
            }

            $.post("/Admin/DeleteTuikData", { tuik: tuik, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Tüik Verisi Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}




//$("body").on("hide.bs.modal", "#emtea-adding-modal", () => {
//    $("#GridContainer").dxDataGrid("instance").refresh();
//})

