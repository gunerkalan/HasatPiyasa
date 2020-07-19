/*$(function () {
    $("#GridContainer").dxDataGrid({
       export: {
            enabled: true
        }
    });

})*/
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
                    swal("Emtea Kaydedildi !");
                    $(".confirm").html("")
                    $(".confirm").append(href)
                    $(".confirm a").css("color", "white")
                    $(".confirm a").width($(".confirm").width())
                }
                else {
                    swal("Hata :" + model.messages);
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
function ChangeColor(value, v) {

    if (value == "0" || value == "") {
        $("#" + v).css("border", "1px solid red")
    }
    else {
        $("#" + v).css("border", "0px")
    }

}

$("body").on("hide.bs.modal", "#confirmModal", () => {
    $("#GridContainer").dxDataGrid("instance").refresh();
})

