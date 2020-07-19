function SaveEmtea() {

    swal({
        title: "Sipariş Oluştur",
        text: "Sipariş oluşturulsun mu ?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
    }, function () {


        var kdvpriceFormat = kdvTutar.toString
        var order = {
            StoreName: $("#Order_StoreName").val(),
            TotalPrice: toplamTutar.toString().replace('.', ','),
            StartDate: $("#Order_StartDate").val(),
            StartClock: $("#Order_StartClock").val(),
            EndDate: $("#Order_EndDate").val(),
            EndClock: $("#Order_EndClock").val(),
            OrderBaseType: "1",
            CustomerId: $("#firmaid :selected").val(),
            ComponentId: $("#compomnetid :selected").val(),
            DemandAmount: $("#Order_DemandAmount").val(),
            GivenAmount: "0",
            TotalAmount: "0",
            Explain: $("#Order_Explain").val(),
            OrderStatus: "1",
            TruckCount: "0",
            TruckAmount: "0",
            IsSale: true,
            KdvRateId: $("#kdv :selected").val(),
            BorsaRateId: $("#borsa :selected").val(),
            KdvPrice: kdvTutar.toString().replace('.', ','),
            BorsaPrice: borsaTutar.toString().replace('.', ','),
            OrderTypeId: $("#ordertypeid :selected").val(),
            ConfirmNumber: $("#Order_ConfirmNumber").val(),

        }


        if (CheckValidateForm()) {
            $.post("/order/CreateSaleOrder", { order: order }, function (res) {
                var model = JSON.parse(JSON.stringify(res));

                if (model.success) {
                    swal("Siparişiz Başarıyla Oluşturuldu ! Sipariş Numaranız" + " " + model.ordernumber);
                    var href = "<a href='/order/WaitingConfirmOrderList'>Tamam</a>";
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