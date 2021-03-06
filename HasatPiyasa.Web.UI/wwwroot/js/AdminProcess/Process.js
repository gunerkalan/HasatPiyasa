﻿var GlobalEmteaId;
var GlobalEmteaGroupId;
var GlobalEmteaTypeId;
var GlobalEmteaTypeGroupId;
var GlobalTuikSubeId;
var GlobalTuikUserId;
var GlobalSubeId;
var GlobalAddedTime;
var GlobalTuikCityId;
var GlobalCityId;
var GlobalUserId;
var GlobalEmteaAddedTime;
var GlobalEmteaGroupAddedTime;
var GlobalEmteaTypeAddedTime;

$(function () {
    $("#GridContainer").dxDataGrid({
        export: {
            enabled: true
        }
    });

})




function SaveEmtea() {


    swal({
        title: "Emtia Kaydet",
        text: "Emtia Kaydedilsin Mi ?",
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
        title: "Emtia Grup Kaydet",
        text: "Emtia Grup Kaydedilsin Mi ?",
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
        title: "Emtia Tipi Kaydet",
        text: "Emtia Tipi Kaydedilsin Mi ?",
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
            EmteaTypeCode: $("#emteatypecode").val(),
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
                    $('#emteatypecode').val('')

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
        title: "Emtia Tip Gurup Kaydet",
        text: "Emtia Tip Gurup Kaydedilsin Mi ?",
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
function SaveUser() {


    swal({
        title: "Kullanıcı Kaydet",
        text: "Kullanıcı Kaydedilsin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var user = {
            Name: $("#username").val(),
            Surname: $("#surname").val(),
            SicilNumber: $("#sicilnumber").val(),
            DomainUserName: $("#domainusername").val(),
            SubeId: $("#drpsubes :selected").val(),
            Title: $("#title").val(),
            Email: $("#email").val(),
            UserRoleId: $("#drproles :selected").val(),
        }

        if (CheckValidateFormUser()) {
            $.post("/Admin/CreateUser", { user: user }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    SweetAlertMesaj("Kullanıcı Kaydet", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#user-adding-modal").modal("hide")

                    $('#username').val('')
                    $('#surname').val('')
                    $('#sicilnumber').val('')
                    $('#domainusername').val('')
                    $('#drpsubes').val('')
                    $('#title').val('')
                    $('#email').val('')
                    $('#drproles').val('')
                }
                else {

                    SweetAlertMesaj("Kullanıcı Kaydet", model.messages, "error", "Kapat", "btn-danger")

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
function CheckValidateFormUser() {

    var Name = $("#username").val()
    var Surname = $("#surname").val()
    var SicilNumber = $("#sicilnumber").val()
    var DomainUserName = $("#domainusername").val()
    var SubeId = $("#drpsubes :selected").val()
    var UserRoleId = $("#drproles :selected").val()
    var Title = $("#title").val()
    var Email = $("#email").val()

    if (SubeId != "" && SubeId != "-1" && UserRoleId != "" && UserRoleId != "-1" && Name != "" && Surname != "" && SicilNumber != "" && DomainUserName != "" && Title != "" && Email != "") {
        return true
    }
    else {
        ChangeColor(Name, "username")
        ChangeColor(Surname, "surname")
        ChangeColor(SicilNumber, "sicilnumber")
        ChangeColor(DomainUserName, "domainusername")
        ChangeColor(SubeId, "drpsubes")
        ChangeColor(UserRoleId, "drproles")
        ChangeColor(Title, "title")
        ChangeColor(Email, "email")
        return false

    }
}
function CheckValidateFormUser2() {

    var Name = $("#username2").val()
    var Surname = $("#surname2").val()
    var SicilNumber = $("#sicilnumber2").val()
    var DomainUserName = $("#domainusername2").val()
    var SubeId = $("#drpsubes2 :selected").val()
    var UserRoleId = $("#drproles2 :selected").val()
    var Title = $("#title2").val()
    var Email = $("#email2").val()

    if (SubeId != "" && SubeId != "-1" && UserRoleId != "" && UserRoleId != "-1" && Name != "" && Surname != "" && SicilNumber != "" && DomainUserName != "" && Title != "" && Email != "") {
        return true
    }
    else {
        ChangeColor(Name, "username2")
        ChangeColor(Surname, "surname2")
        ChangeColor(SicilNumber, "sicilnumber2")
        ChangeColor(DomainUserName, "domainusername2")
        ChangeColor(SubeId, "drpsubes2")
        ChangeColor(UserRoleId, "drproles2")
        ChangeColor(Title, "title2")
        ChangeColor(Email, "email2")
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
    var EmteaTypeCode = $("#emteatypecode").val()

    if (EmteaGroupId != "" && EmteaTypeName != "" && EmteaTypeCode != "" && EmteaGroupId != "-1" && EmteaId != "" && EmteaId != "-1") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias")
        ChangeColor(EmteaGroupId, "drpemtiagroups")
        ChangeColor(EmteaTypeName, "emteatypename")
        ChangeColor(EmteaTypeCode, "emteatypecode")
        return false

    }
}
function CheckValidateFormEmteaType2() {

    var EmteaId = $("#drpemtias2 :selected").val()
    var EmteaGroupId = $("#drpemtiagroups2 :selected").val()
    var EmteaTypeName = $("#emteatypename2").val()
    var EmteaTypeCode = $("#emteatypecode2").val()

    if (EmteaGroupId != "" && EmteaTypeName != "" && EmteaTypeCode != "" && EmteaGroupId != "-1" && EmteaId != "" && EmteaId != "-1") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias2")
        ChangeColor(EmteaGroupId, "drpemtiagroups2")
        ChangeColor(EmteaTypeName, "emteatypename2")
        ChangeColor(EmteaTypeCode, "emteatypecode2")
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
function CheckValidateFormCityTuik2() {

    var EmteaId = $("#drpemtias2 :selected").val()
    var EmteaGroupId = $("#drpemtiagroups2 :selected").val()
    var EmteaTypeId = $("#drpemtiatypes2 :selected").val()
    var CityId = $("#drpcities2 :selected").val()
    var TuikValue = $("#tuikvalue2").val()
    var GuessValue = $("#guessvalue2").val()

    if (EmteaId != "-1" && EmteaGroupId != "-1" && EmteaTypeId != "-1" && TuikValue != "" && GuessValue != "" && CityId != "") {
        return true
    }
    else {
        ChangeColor(EmteaId, "drpemtias2")
        ChangeColor(EmteaGroupId, "drpemtiagroups2")
        ChangeColor(EmteaTypeId, "drpemtiatypes2")
        ChangeColor(CityId, "drpcities2")
        ChangeColor(TuikValue, "tuikvalue2")
        ChangeColor(GuessValue, "guessvalue2")
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
function EditEmtea(id, AddedTime) {
    GlobalEmteaId = id
    GlobalEmteaAddedTime = AddedTime
    $.post("/Admin/GetEmtea", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#emteacode2").val(model.Veri.EmteaCode)
            $("#emteaname2").val(model.Veri.EmteaName)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.EmteaCode}  kodlu Emtia'yı Düzenle `)
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
        title: "Emtia Güncelle",
        text: "Emtia Güncellensin Mi ?",
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
            AddedTime: GlobalEmteaAddedTime

        }

        if (CheckValidateForm2()) {
            $.post("/Admin/UpdateEmtea", { emtea: emtea }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Emtia Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#emteacode2').val('')
                    $('#emteaname2').val('')
                    GlobalEmteaId = null
                    GlobalEmteaAddedTime = null


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
function SoftDeleteEmtea(Id, EmteaCode, EmteaName, AddedTime) {
    GlobalEmteaId = Id,
        GlobalEmteaAddedTime = AddedTime
    swal({
        title: "Sil ?",
        text: `${EmteaCode} kodlu Emtia Silinsin mi ?`,
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
                EmteaName: EmteaName,
                AdddedTime: GlobalEmteaAddedTime

            }

            $.post("/Admin/DeleteEmtea", { emtea: emtea, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtia Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                    GlobalEmteaId = null
                    GlobalEmteaAddedTime = null
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}
function EditEmteaGroup(id, EmteaGrupAd, AddedTime) {
    GlobalEmteaGroupId = id
    GlobalEmteaGroupAddedTime = AddedTime
    $.post("/Admin/GetEmteaGroup", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#drpemtias2").val(model.Veri.EmteaId)
            $("#emteagroupname2").val(model.Veri.GroupName)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.GroupName}  isimli Emtia Gurubu Düzenle `)
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
        title: "Emtia Gurup Güncelle",
        text: "Emtia Gurup Güncellensin Mi ?",
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
            AddedTime: GlobalEmteaGroupAddedTime
        }

        if (CheckValidateFormEg2()) {
            $.post("/Admin/UpdateEmteaGroup", { emteagroup: emteagroup }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Emtia Gurup Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#emteagroupname2').val('')
                    GlobalEmteaGroupId = null
                    GlobalEmteaGroupAddedTime = null

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
function SoftDeleteEmteaGroup(Id, GroupName, EmteaId, AddedTime) {
    GlobalEmteaGroupId = Id,
        GlobalEmteaId = EmteaId,
        GlobalEmteaGroupAddedTime = AddedTime
    swal({
        title: "Sil ?",
        text: `${GroupName} isimli Emtia Gurup Silinsin mi ?`,
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
                GroupName: GroupName,
                AddedTime: GlobalEmteaGroupAddedTime

            }

            $.post("/Admin/DeleteEmteaGroup", { emteagroup: emteagroup, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtia Gurup Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                    GlobalEmteaGroupId = null
                    GlobalEmteaGroupAddedTime = null
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}
function EditEmteaType(id, EmteaId, AddedTime) {
    GlobalEmteaTypeId = id
    GlobalEmteaTypeAddedTime = AddedTime
    EmteaChange2(EmteaId)
    $.post("/Admin/GetEmteaType", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#drpemtias2").val(model.Veri.EmteaId)

            $("#drpemtiagroups2").val(model.Veri.EmteaGroupId)
            $("#emteatypename2").val(model.Veri.EmteaTypeName)
            $("#emteatypecode2").val(model.Veri.EmteaTypeCode)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.EmteaTypeName}  isimli Emtia Tipi Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")



        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function SoftDeleteEmteaType(Id, EmteaGroupId, EmteaTypeName, AddedTime) {
    GlobalEmteaGroupId = EmteaGroupId,
        GlobalEmteaTypeId = Id
    GlobalEmteaTypeAddedTime = AddedTime
    swal({
        title: "Sil ?",
        text: `${EmteaTypeName} isimli Emtia Tipi Silinsin mi ?`,
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
                EmteaTypeName: EmteaTypeName,
                AddedTime: GlobalEmteaTypeAddedTime,

            }

            $.post("/Admin/DeleteEmteaType", { emteatype: emteatype, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtia Tipi Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                    GlobalEmteaTypeId = null
                    GlobalEmteaGroupId = null
                    GlobalEmteaTypeAddedTime = null
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
        title: "Emtia Tipi Güncelle",
        text: "Emtia Tipi Güncellensin Mi ?",
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
            EmteaTypeCode: $("#emteatypecode2").val(),
            AddedTime: GlobalEmteaTypeAddedTime

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
                    $('#emteatypcode2').val('')
                    GlobalEmteaTypeId = null
                    GlobalEmteaTypeAddedTime = null
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
function EditEmteaTypeGroup(id, EmteaId, EmteaGroupId, AddedTime) {
    GlobalEmteaTypeGroupId = id
    GlobalAddedTime = AddedTime
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

            $("#usermodeltitleEditSiparis").html(`${model.Veri.EmteaTypeGroupName}  isimli Emtia Tip Gurubu Düzenle `)
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
        title: "Emtia Tip Gurubu Güncelle",
        text: "Emtia Gurubu Güncellensin Mi ?",
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
            AddedTime: GlobalAddedTime
        }

        if (CheckValidateFormEmteaTypeGroup2()) {
            $.post("/Admin/UpdateEmteaTypeGroup", { emteatypegroup: emteatypegroup }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Emtia Tip Gurubu Güncelle", model.Mesaj, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#drpemtiagroups2').val('')
                    $('#drpemtiatypes2').val('')
                    $('#emteatypegroupname2').val('')
                    GlobalEmteaTypeGroupId = null
                    GlobalAddedTime = null
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
function SoftDeleteEmteaTypeGroup(Id, EmteaTypeId, EmteaTypeGroupName, AddedTime) {
    GlobalEmteaTypeId = EmteaTypeId,
        GlobalEmteaTypeGroupId = Id
    GlobalAddedTime = AddedTime
    swal({
        title: "Sil ?",
        text: `${EmteaTypeGroupName} isimli Emtia Tip Gurubu Silinsin mi ?`,
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
                EmteaTypeGroupName: EmteaTypeGroupName,
                AddedTime: GlobalAddedTime

            }

            $.post("/Admin/DeleteEmteaTypeGroup", { emteatypegroup: emteatypegroup, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Emtia Tip Gurubu Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                    GlobalEmteaTypeId = null
                    GlobalEmteaTypeGroupId = null
                    GlobalAddedTime = null
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
                                    <td><span class="detailname">Emtia Kodu</span> :</td><td><span class="detailvalue">${model.Veri.EmteaCode}</span> </td>
                                    <td><span class="detailname">Emtia Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaName}</span> </td>
                                                                       
                                </tr>
                                <tr>
                                    <td><span class="detailname">Emtia Gurup Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaGroupName}</span> </td>
                                    <td><span class="detailname">Emtia Tip Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaTypeName}</span> </td>
                                                                       
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
                    SweetAlertMesaj("Tüik Verisini Güncelle", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#drpemtiagroups2').val('')
                    $('#drpemtiatypes2').val('')
                    $('#drpsubes2').val('')
                    $('#tuikvalue2').val('')
                    $('#guessvalue2').val('')
                    GlobalTuikSubeId = null
                    GlobalTuikUserId = null
                    GlobalAddedTime = null
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
                IsCity: false,
                EmteaTypeId: GlobalEmteaTypeId,
                EmteaId: GlobalEmteaId,
                EmteaGroupId: GlobalEmteaGroupId,
                AddUserId: UserId,
                SubeId: SubeId
            }

            $.post("/Admin/DeleteTuikData", { tuik: tuik, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Tüik Verisi Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                    GlobalEmteaTypeId = null
                    GlobalEmteaGroupId = null
                    GlobalEmteaId = null
                    GlobalTuikSubeId = null
                    GlobalSubeId = null
                    GlobalTuikUserId = null
                }
                else {
                    swal("Hata !", model.ErrorMessage, "error");
                    this.showLoaderOnConfirm = false
                }
            })
        }
    );
}
function TuikCityDetail(id) {

    $.post("/Admin/GetDetailTuikCity", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {
            var table = CreatDetailTableCity(model)
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
function CreatDetailTableCity(model) {
    var table = "";

    table = `<table class="table table-striped" id="detailTable">
                            <tbody>
                               <tr>
                                   
                                     <td colspan="2"><span class="detailname">Eklenme Tarihi</span> :<span class="detailvalue ">${model.Veri.AddedTime}</span> </td>
                                      <td><span class="detailname">Şube Adı</span> :</td><td><span class="detailvalue">${model.Veri.CitiName}</span> </td>                          
                                </tr>

                                <tr>
                                    <td><span class="detailname">Emtia Kodu</span> :</td><td><span class="detailvalue">${model.Veri.EmteaCode}</span> </td>
                                    <td><span class="detailname">Emtia Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaName}</span> </td>
                                                                       
                                </tr>
                                <tr>
                                    <td><span class="detailname">Emtia Gurup Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaGroupName}</span> </td>
                                    <td><span class="detailname">Emtia Tip Adı</span> :</td><td><span class="detailvalue">${model.Veri.EmteaTypeName}</span> </td>
                                                                       
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
function UpdateTuikCityData() {


    swal({
        title: "Tüik İl Verisi Güncelle",
        text: "Tüik İl Verisi Güncellensin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var tuik = {
            Id: GlobalTuikCityId,
            AddUserId: GlobalTuikUserId,
            EmteaId: $("#drpemtias2").val(),
            EmteaGroupId: $("#drpemtiagroups2").val(),
            EmteaTypeId: $("#drpemtiatypes2").val(),
            CityId: $("#drpcities2").val(),
            TuikValue: $("#tuikvalue2").val(),
            GuessValue: $("#guessvalue2").val(),
            AddedTime: GlobalAddedTime
        }

        if (CheckValidateFormCityTuik2()) {
            $.post("/Admin/UpdateTuikCity", { tuik: tuik }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Tüik İl Verisini Güncelle", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#drpemtias2').val('')
                    $('#drpemtiagroups2').val('')
                    $('#drpemtiatypes2').val('')
                    $('#drpcities2').val('')
                    $('#tuikvalue2').val('')
                    $('#guessvalue2').val('')
                    GlobalTuikCityId = null
                    GlobalTuikUserId = null
                    GlobalAddedTime = null
                }
                else {

                    SweetAlertMesaj("Tüik İl Verisi Güncelle", model.messages, "error", "Kapat", "btn-danger")

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
function EditTuikCityData(id, EmteaId, EmteaGroupId, SubeName, TuikYear, UserId, AddedTime) {
    GlobalTuikCityId = id
    GlobalTuikUserId = UserId
    GlobalAddedTime = AddedTime
    EmteaChange2(EmteaId)
    EmteaGroupChange2(EmteaGroupId)
    $.post("/Admin/GetTuikCity", { id: id }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#drpemtias2").val(model.Veri.EmteaId)
            $("#drpemtiagroups2").val(model.Veri.EmteaGroupId)
            $("#drpemtiatypes2").val(model.Veri.EmteaTypeId)
            $("#drpcities2").val(model.Veri.CityId)

            $("#tuikvalue2").val(model.Veri.TuikValue)
            $("#guessvalue2").val(model.Veri.GuessValue)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.CityName + " " + model.Veri.TuikYear}  Tüik İl Verisini Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")



        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function EditUser(UserId, SubeId, UserRoleId, Name, Surname, AddedTime) {
    GlobalUserId = UserId
    GlobalAddedTime = AddedTime

    $.post("/Admin/GetUser", { id: UserId }, (res) => {
        $("#loadPanel").dxLoadPanel("instance").show();
        var model = JSON.parse(res)

        if (model.BasariliMi) {

            $("#username2").val(model.Veri.Name)
            $("#surname2").val(model.Veri.Surname)
            $("#drpemtiatypes2").val(model.Veri.EmteaTypeId)
            $("#sicilnumber2").val(model.Veri.SicilNumber)

            $("#domainusername2").val(model.Veri.DomainUserName)
            $("#drpsubes2").val(model.Veri.SubeId)
            $("#drproles2").val(model.Veri.UserRoleId)
            $("#title2").val(model.Veri.Title)
            $("#email2").val(model.Veri.Email)

            $("#usermodeltitleEditSiparis").html(`${model.Veri.Name + " " + model.Veri.Surname}  Kullanıcısını Düzenle `)
            $("#loadPanel").dxLoadPanel("instance").hide();
            $("#EditModal").modal("show")



        }
        else {
            $("#loadPanel").dxLoadPanel("instance").hide();
            swal("Hata !", model.ErrorMessage, "error");
        }
    })
}
function UpdateUser() {


    swal({
        title: "Kullanıcı Güncelle",
        text: "Kullanıcı Güncellensin Mi ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {

        var user = {
            UserId: GlobalUserId,
            Name: $("#username2").val(),
            Surname: $("#surname2").val(),
            SicilNumber: $("#sicilnumber2").val(),
            DomainUserName: $("#domainusername2").val(),
            SubeId: $("#drpsubes2").val(),
            Title: $("#title2").val(),
            Email: $("#email2").val(),
            UserRoleId: $("#drproles2").val(),
            AddedTime: GlobalAddedTime
        }

        if (CheckValidateFormUser2()) {
            $.post("/Admin/UpdateHpUser", { user: user }, function (res) {
                var model = JSON.parse(res);

                if (model.success) {
                    SweetAlertMesaj("Kullanıcı Güncelle", model.messages, "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    $("#EditModal").modal("hide")
                    loadpanel.hide()

                    $('#username2').val('')
                    $('#surname2').val('')
                    $('#sicilnumber2').val('')
                    $('#domainusername2').val('')
                    $('#drpsubes2').val('')
                    $('#title2').val('')
                    $('#email2').val('')
                    $('#drproles2').val('')
                    GlobalUserId = null
                    GlobalAddedTime = null
                }
                else {

                    SweetAlertMesaj("Kullanıcı Güncelle", model.messages, "error", "Kapat", "btn-danger")

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
function SoftDeleteUser(UserId, SubeId, UserRoleId, Name, Surname, AddedTime) {
    GlobalUserId = UserId,

        swal({
            title: "Sil ?",
            text: `Kullanıcı Silinsin mi ?`,
            type: "info",
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
            confirmButtonText: "Tamam",
            cancelButtonText: "İptal",

        },
            function () {

                var user = {
                    UserId: GlobalUserId,
                    IsActive: false,
                    IsDomain: true,
                    UserRoleId: UserRoleId,
                    Name: Name,
                    Surname: Surname,
                    AddedTime: AddedTime,
                    SubeId: SubeId
                }

                $.post("/Admin/DeleteUser", { user: user, }, (res) => {

                    var model = JSON.parse(res)

                    if (model.success) {
                        SweetAlertMesaj("Silme", "Kullanıcı Silinmiştir !", "success", "Kapat", "btn-success")
                        $("#GridContainer").dxDataGrid("instance").refresh();
                        this.showLoaderOnConfirm = false
                        GlobalUserId = null
                    }
                    else {
                        swal("Hata !", model.ErrorMessage, "error");
                        this.showLoaderOnConfirm = false
                    }
                })
            }
        );
}
function SoftDeleteTuikCityData(id, EmteaId, EmteaGroupId, EmteaTypeId, CityId, UserId) {
    GlobalEmteaTypeId = EmteaTypeId,
        GlobalEmteaGroupId = EmteaGroupId
    GlobalEmteaId = EmteaId
    GlobalTuikCityId = id
    GlobalCityId = CityId
    GlobalTuikUserId = UserId
    swal({
        title: "Sil ?",
        text: `Tüik İl Verisi Silinsin mi ?`,
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",

    },
        function () {

            var tuik = {
                Id: GlobalTuikCityId,
                IsActive: false,
                IsCity: true,
                EmteaTypeId: GlobalEmteaTypeId,
                EmteaId: GlobalEmteaId,
                EmteaGroupId: GlobalEmteaGroupId,
                AddUserId: UserId,
                CityId: GlobalCityId
            }

            $.post("/Admin/DeleteTuikData", { tuik: tuik, }, (res) => {

                var model = JSON.parse(res)

                if (model.success) {
                    SweetAlertMesaj("Silme", "Tüik İl Verisi Silinmiştir !", "success", "Kapat", "btn-success")
                    $("#GridContainer").dxDataGrid("instance").refresh();
                    this.showLoaderOnConfirm = false
                    GlobalTuikCityId = null
                    GlobalEmteaTypeId = null
                    GlobalEmteaGroupId = null
                    GlobalCityId = null
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

