var GiftVoucherSave = ({
    Control: {
        txtExpiryDate: ".txtExpiryDate",
        divGiftVoucher: "#divGiftVoucher",
        txtPercentage: ".txtPercentage",
        txtFixDiscountAmount: ".txtFixDiscountAmount",
        hfGiftVoucherType: "#hfGiftVoucherType",
        ValidationMsg: ".field-validation-error",
        ddlGiftVoucherType: ".ddlGiftVoucherType",
        txtMaxDiscountAmount: ".txtMaxDiscountAmount",
        divGiftVoucherImage: "#divGiftVoucherImage",
        btnSubmit: ".btnSubmit",
        frmGiftvoucher: ".frmGiftvoucher",
        hfImage: "#Image",
        btnslimbtnremove: ".slim-btn-remove",
    },
    SelectGiftVoucherType: function (val) {
        $(GiftVoucherSave.Control.ValidationMsg).find("span").html("");
        if (val == '2') {
            $(GiftVoucherSave.Control.txtFixDiscountAmount).attr("disabled", "disabled");
            $(GiftVoucherSave.Control.txtFixDiscountAmount).val('');

            $(GiftVoucherSave.Control.txtPercentage).removeAttr("disabled");
        }
        else {
            $(GiftVoucherSave.Control.txtPercentage).attr("disabled", "disabled");
            $(GiftVoucherSave.Control.txtPercentage).val('');

            $(GiftVoucherSave.Control.txtFixDiscountAmount).removeAttr("disabled");
        }
    },
});

$(document).ready(function () {

    $(GiftVoucherSave.Control.txtExpiryDate).datepicker({ startDate: Date(), format: "dd/mm/yyyy", clearBtn: true });

    GiftVoucherSave.SelectGiftVoucherType($(GiftVoucherSave.Control.hfGiftVoucherType).val());

    $(document).on("change", $(GiftVoucherSave.Control.ddlGiftVoucherType), function (event) {
        GiftVoucherSave.SelectGiftVoucherType($(GiftVoucherSave.Control.ddlGiftVoucherType).val());
    });
    $(document).on("click", GiftVoucherSave.Control.btnSubmit, function () {
        if ($(GiftVoucherSave.Control.frmGiftvoucher).valid()) {
            if ($(GiftVoucherSave.Control.hfImage).val() != '' || $('input[name=GiftVoucherImage]').val() != '') {
                $(GiftVoucherSave.Control.frmGiftvoucher).submit();
            }
            else {
                $(GiftVoucherSave.Control.divGiftVoucherImage).text("Please enter Image.")
            }
        }
    });
    $(document).on("click", GiftVoucherSave.Control.btnslimbtnremove, function () {
        $(GiftVoucherSave.Control.hfImage).val('');
    });
})